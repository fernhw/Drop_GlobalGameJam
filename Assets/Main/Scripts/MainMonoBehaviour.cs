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


    [SerializeField]
    float JOYSTICK_RUN = .6f;  
    [SerializeField]
    float CHAR_SPEED = 7;
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
    Camera camera;

    [SerializeField]
    float deltaModifier = 1;

    Vector3 ogCamCharComparison = new Vector3();
    List<Obstacle> obstacles;
    List<Parent> parents;
    void Start(){
        ProInput.Init();

        //Obj load
        obstacles = AllOfClass.PickAllOf<Obstacle>();
        parents = AllOfClass.PickAllOf<Parent>();

        //Camera setup
        ogCamCharComparison =  character.transform.localPosition - camera.transform.localPosition;
    }

    float preCharX, preCharZ;

    float
        charX,
        charY,
        charZ,
        camX,
        camY,
        camZ;

    void Update() {
        float deltaBase = Time.deltaTime;
        float deltaRun = deltaBase * deltaModifier;
        ProInput.UpdateInput(deltaRun, debug: true);

        Vector3 charPosition = character.transform.localPosition;
        Vector3 camPosition = camera.transform.localPosition;

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
        }

        if (hitActionButton) {

        }

        Parents();
        Collisions();
        ManageCamera();

        Vector3 newPos = new Vector3(charX, charY, charZ);
        character.transform.localPosition = newPos;
        Vector3 newcamPos = new Vector3(camX, camY, camZ);
        camera.transform.localPosition = newcamPos;
    }
    bool hitActionButton;
    bool ifHitPending = false;
    private void Parents() {
        bool anyHitsCollisionOrButton = false;
        int parentNum = parents.Count;    
        for (int i = 0; i<parentNum; i++) {
            Parent parent = parents[i];
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
                    }
                }
            }
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
    }

    private void Collisions() {
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
                print(quadrantProblem.ToString());
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