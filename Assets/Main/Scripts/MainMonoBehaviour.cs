using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProInputSystem;
using System;

public class MainMonoBehaviour : MonoBehaviour
{
    const float HALF_PI = (Mathf.PI*0.5f);
    const float PI_X2 = (Mathf.PI*2);
    const float PI_45D = PI_X2/8;
    static readonly Vector3 zero = new Vector3(0,0,0);

    [SerializeField]
    float FLAME_SPEED = .1f;
    [SerializeField]
    float FLAME_HIT = .1f;
    [SerializeField]
    float FLAME_EFFECT_HIT = .1f;

    [SerializeField]
    float JOYSTICK_RUN = .6f;
    [SerializeField]
    float CAM_SHAKE_TIME = .2f;
    [SerializeField]
    float CAM_SHAKE_RECOVER = .2f;
    [SerializeField]
    float CAM_SHAKE_TIME_INTERV = 1/24; //movie fps

    [SerializeField]
    float CHAR_SPEED = 7;
    [SerializeField]
    float CAM_SHAKE_POWER = 1;
    [SerializeField]
    float CHAR_SPEED_RUN = 7;
    [SerializeField]
    float CAM_LIMIT_Z_NEG = -51;
    [SerializeField]
    float CAM_LIMIT_Z_POS = 51;
    [SerializeField]
    float CAM_LIMIT_X_NEG = -51;
    [SerializeField]
    float CAM_LIMIT_X_POS = 51;
    [SerializeField]
    Vector3 CHAR_SIZE = new Vector3(10,10,10);
   

    [SerializeField]
    GameObject character;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    GameObject mainCamContainer;
    [SerializeField]
    AnimationManager dropAnimationManager;

    [SerializeField]
    float deltaModifier = 1;
    [SerializeField]
    float difficulty = 1;

    Vector3 ogCamCharComparison = new Vector3();
    List<Obstacle> obstacles;
    List<Parent> parents;
    CharacterState charState = CharacterState.IDLE;
    float preCharX, preCharZ;
    float deltaRun;
    float
        charX,
        charY,
        charZ,
        camX,
        camY,
        camZ;
    float shakeScreenTime = 0;

    bool hitActionButton;
    bool ifHitPending = false;

    void Start(){
        ProInput.Init();

        //Obj load
        obstacles = AllOfClass.PickAllOf<Obstacle>();
        parents = AllOfClass.PickAllOf<Parent>();

        //Camera setup
        ogCamCharComparison =  character.transform.localPosition - mainCamContainer.transform.localPosition;

        //init
        charState = CharacterState.IDLE; // CHANGE FOR OP
        ManageAnimations();
    }
   
    void Update() {
        float deltaBase = Time.deltaTime;
        deltaRun = deltaBase * deltaModifier;
        ProInput.UpdateInput(deltaRun, debug: true);

        Vector3 charPosition = character.transform.localPosition;
        Vector3 camPosition = mainCamContainer.transform.localPosition;


        AnalogueInput stick = ProInput.GlobalActionStick;

        charX = charPosition.x;
        charY = charPosition.y;
        charZ = charPosition.z;

        camX = camPosition.x;
        camY = camPosition.y;
        camZ = camPosition.z;

        preCharX = charX;
        preCharZ = charZ;

        hitActionButton = ProInput.A;

        if (hitActionButton) {
            charState = CharacterState.HEAD_BONK;
        }

        if (stick.IsActive() && !hitActionButton) {
            float stickAngle = stick.Angle;
            float stickCos = Mathf.Cos(stickAngle);
            float stickSin = Mathf.Sin(stickAngle);

            float realJoystickPress = (stick.Distance-stick.DeadZone)/(1-stick.DeadZone);
            if (realJoystickPress> JOYSTICK_RUN) {
                charX += stickCos * CHAR_SPEED_RUN * deltaRun;
                charZ -= stickSin * CHAR_SPEED_RUN * deltaRun;
            } else {
                charX += stickCos * CHAR_SPEED * deltaRun;
                charZ -= stickSin * CHAR_SPEED * deltaRun;
            }
            charState = CharacterState.WALK;
        }

        if (!stick.IsActive() && !hitActionButton) {
            charState = CharacterState.IDLE;
        }
        
        Flames();
        DealWithCollisions();
        ManageCamera();
        ManageAnimations();

        Vector3 newPos = new Vector3(charX, charY, charZ);
        character.transform.localPosition = newPos;
        Vector3 newcamPos = new Vector3(camX, camY, camZ);
        mainCamContainer.transform.localPosition = newcamPos;
    }

    CharacterState oldCharacterState = CharacterState.IDLE  ;
    private void ManageAnimations() {
        if (charState != oldCharacterState) {  
            oldCharacterState = charState;
            int animId = (int)charState;
            dropAnimationManager.JumpTo(Drop.anims[animId]);
        }
    }

    private void Flames() {
        bool anyHitsCollisionOrButton = false;
        int parentNum = parents.Count;
        for (int i = 0; i<parentNum; i++) {
            Parent parent = parents[i];

            if (parent.active) {
                parent.flame.percentage += deltaRun* FLAME_SPEED*difficulty;
                Transform obsTrans = parent.transform;
                Vector3 obsPos = parent.flamePivot.transform.localPosition;
                Vector3 obsScale = obsTrans.localScale;
                float obsX = obsPos.x;
                float obsZ = obsPos.z;
                float obsWidthHalf = obsScale.x*.5f + CHAR_SIZE.x*.5f;
                float obsDepthHalf = obsScale.z*.5f + CHAR_SIZE.z*.5f;
                if (charX > obsX - obsWidthHalf &&
                    charX < obsX + obsWidthHalf &&
                    charZ > obsZ - obsDepthHalf &&
                    charZ < obsZ + obsDepthHalf) {
                    anyHitsCollisionOrButton = true;
                    if (hitActionButton) {
                        if (ifHitPending) {
                            ifHitPending = false;
                            print("HIT PARENT");
                            ShakeCam();
                            parent.flame.percentage -= FLAME_HIT;
                            parent.flame.actualFlame += FLAME_EFFECT_HIT;
                            if (parent.flame.percentage>1) {
                                parent.flame.percentage = 1;
                            }
                            
                        }
                    }
                }
                if (parent.flame.percentage<0) {
                    parent.active = false;
                }
            }
            parent.flame.ShowFlames(deltaRun);
        }
       
        if (hitActionButton) {
            anyHitsCollisionOrButton = true;
            ifHitPending = false;
        }
        if (!hitActionButton) {
            ifHitPending = true;
        }
    }

    private void ManageCamera() {
        camX = charX-ogCamCharComparison.x;
        camY = charY-ogCamCharComparison.y;
        camZ = charZ-ogCamCharComparison.z;
        if (camZ<CAM_LIMIT_Z_NEG) {
            camZ = CAM_LIMIT_Z_NEG;
        }
        if (camZ>CAM_LIMIT_Z_POS) {
            camZ = CAM_LIMIT_Z_POS;
        }
        if (camX<CAM_LIMIT_X_NEG) {
            camX = CAM_LIMIT_X_NEG;
        }
        if (camX>CAM_LIMIT_X_POS) {
            camX = CAM_LIMIT_X_POS;
        }
        camX = (camX+(charX-ogCamCharComparison.x))*.5f;
        camZ = (camZ+(charZ-ogCamCharComparison.z))*.5f;

        if (shakeScreenTime>=0) {
            Vector3 camInternalCam = mainCam.transform.localPosition;
            if (camInternalCam.y == 0) {
                camInternalCam.y = CAM_SHAKE_POWER;
            } else {
                if (shakeScreenTime>CAM_SHAKE_TIME_INTERV) {
                    shakeScreenTime = 0;
                    if (camInternalCam.y == CAM_SHAKE_POWER) {
                        camInternalCam.y = -CAM_SHAKE_POWER;
                    } else {
                        camInternalCam.y = CAM_SHAKE_POWER;
                    }
                } else {
                    shakeScreenTime += deltaRun;
                }
            }
            mainCam.transform.localPosition = new Vector3(camInternalCam.x, camInternalCam.y, camInternalCam.z);
            shakeScreenTime -= CAM_SHAKE_RECOVER;            
        } else {
            mainCam.transform.localPosition = zero;
        }

    }

    public void ShakeCam() {
        shakeScreenTime = CAM_SHAKE_TIME;
    }

    private void DealWithCollisions() {
        int obstacleNum = obstacles.Count;
        for (int i = 0; i<obstacleNum; i++) {
            Obstacle obstacle = obstacles[i];
            if (obstacle.type == ObstacleType.CUBE) {
                CubeCollision(obstacle);
            } else {
                CircleCollision(obstacle);
            }
        }
    }

    private void CircleCollision(Obstacle obstacle) {
        Transform obsTrans = obstacle.transform;
        Vector3 obsPos = obsTrans.localPosition;
        Vector3 obsScale = obsTrans.localScale;
        float obsX = obsPos.x;
        float obsZ = obsPos.z;
        float obsRadiusHalf = obsScale.x + CHAR_SIZE.x*.5f;

        float xDif = charX - obsX;
        float zDif = charZ - obsZ;
        float angleCharCircle = Mathf.Atan2(zDif, xDif);
        float distCharCircle = Mathf.Sqrt(xDif*xDif + zDif*zDif);

        if (distCharCircle < obsRadiusHalf) {
            float difference = obsRadiusHalf - distCharCircle;
            charX += Mathf.Cos(angleCharCircle)*difference;
            charZ += Mathf.Sin(angleCharCircle)*difference;
            while (distCharCircle < obsRadiusHalf - 0.3f) {
                charX += Mathf.Cos(angleCharCircle)*.01f;
                charZ += Mathf.Sin(angleCharCircle)*.01f;
            }
        }
    }

    private void CubeCollision(Obstacle obstacle) {
        Transform obsTrans = obstacle.transform;
        Vector3 obsPos = obsTrans.localPosition;
        Vector3 obsScale = obsTrans.localScale;
        float obsX = obsPos.x;
        float obsZ = obsPos.z;
        float obsWidthHalf = obsScale.x*.5f + CHAR_SIZE.x*.5f;
        float obsDepthHalf = obsScale.z*.5f + CHAR_SIZE.z*.5f;

        if (charX > obsX - obsWidthHalf &&
            charX < obsX + obsWidthHalf &&
            charZ > obsZ - obsDepthHalf &&
            charZ < obsZ + obsDepthHalf) {

            float xDif = charX - obsX;
            float zDif = charZ - obsZ;
            float angleCharBox = Mathf.Atan2(zDif, xDif);

            float quadCut = Mathf.Atan2(obsDepthHalf, obsWidthHalf);
            float angleResess = (Mathf.PI - quadCut - HALF_PI)*2;
            float remainingCut = quadCut + angleResess;

            QuadProblem quadrantProblem = 0;
            if (angleCharBox <= remainingCut &&
                angleCharBox > quadCut) {
                angleCharBox = PI_45D*2;
                quadrantProblem = QuadProblem.UP;
            } else if (angleCharBox > remainingCut) {
                angleCharBox = PI_45D*4;
                quadrantProblem = QuadProblem.RIGHT;
            } else if (angleCharBox < -remainingCut) {
                angleCharBox = PI_45D*4;
                quadrantProblem = QuadProblem.RIGHT;
            } else if (angleCharBox >= -remainingCut &&
                 angleCharBox < -quadCut) {
                angleCharBox = PI_45D*6;
                quadrantProblem = QuadProblem.DOWN;
            } else if (angleCharBox >= -quadCut) {
                angleCharBox = 0;
                quadrantProblem = QuadProblem.LEFT;
            } else if (angleCharBox >= quadCut) {
                angleCharBox = 0;
                quadrantProblem = QuadProblem.LEFT;
            }

            switch (quadrantProblem) {
                case QuadProblem.UP:
                    if (charZ-preCharZ<0) {
                        charZ = preCharZ;
                    }
                    break;
                case QuadProblem.DOWN:
                    if (charZ-preCharZ>0) {
                        charZ = preCharZ;
                    }
                    break;
                case QuadProblem.LEFT:
                    if (charX-preCharX<0) {
                        charX = preCharX;
                    }
                    break;
                case QuadProblem.RIGHT:
                    if (charX-preCharX>0) {
                        charX = preCharX;
                    }
                    break;
            }
            float widthH = (obsWidthHalf-.3f);
            float depthH = (obsDepthHalf-.3f);
            if (charX > obsX - widthH &&
               charX < obsX +  widthH &&
               charZ > obsZ - depthH &&
               charZ < obsZ + depthH) {
                switch (quadrantProblem) {
                    case QuadProblem.UP:
                        charZ += 0.1f;
                        break;
                    case QuadProblem.DOWN:
                        charZ -= 0.1f;
                        break;
                    case QuadProblem.LEFT:
                        charX += 0.1f;
                        break;
                    case QuadProblem.RIGHT:
                        charX -= 0.1f;
                        break;
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(character.transform.localPosition, CHAR_SIZE);
    }
}

enum QuadProblem {
    LEFT = 0,
    UP = 1,
    RIGHT =2,
    DOWN = 3
}

public enum CharacterState {
    IDLE = 0,
    WALK = 1,
    HEAD_BONK = 2,
    INTRO_SLEEP = 3,
    INTRO_WAKING_UP = 4,
    NULL = 0
}

public static class Drop {
    public const string
    IDLE = "drop_idle",
    WALK = "drop_walk",
    HEAD_BONK = "drop_head",
    INTRO_SLEEP = "sleep_idle",
    INTRO_WAKING_UP = "wake_up_wake_up_mister_drop";
    public static readonly string[] anims = new string[]{
        IDLE,WALK,HEAD_BONK,INTRO_SLEEP,INTRO_WAKING_UP
    };
}