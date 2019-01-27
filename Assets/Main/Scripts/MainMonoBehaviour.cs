using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProInputSystem;
using System;
using UnityEngine.SceneManagement;

public class MainMonoBehaviour:MonoBehaviour {
    const float HALF_PI = (Mathf.PI*0.5f);
    const float PI_X2 = (Mathf.PI*2);
    const float PI_45D = PI_X2/8;
    static readonly Vector3 zero = new Vector3(0, 0, 0);

    public AudioSource song;

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
    float CAM_SHAKE_TIME_DEATH = 1f;
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
    Vector3 CHAR_SIZE = new Vector3(10, 10, 10);
    [SerializeField]
    float LIGHT_TIME_SECONDS = 5;
    [SerializeField]
    float LIGHT_ALPHA_LIMIT = .5f;
    [SerializeField]
    float LIGHT_BONUS_TIME = .5f;
    [SerializeField]
    float LIGHT_LIMIT_LIFESPAN = 10f;
    [SerializeField]
    float LIGHT_SHINE_DOWN = 1f;
    [SerializeField]
    float CAM_SHAKE_PAUSE = 10f;

    [SerializeField]
    float FLAME_SHOWS_UP_ALPHA = .5f;

    // ONE OF ... EACH FRAME
    [SerializeField]
    float FLAME_POSSIBILITY_Z1 = 60;
    [SerializeField]
    float FLAME_POSSIBILITY_Z2 = 60;
    [SerializeField]
    float FLAME_POSSIBILITY_Z3 = 60;
    [SerializeField]
    float FLAME_POSSIBILITY_Z4 = 60;

    [SerializeField]
    int LEVEL_ONE_QUOTA = 10;
    [SerializeField]
    float LEVEL_TWO_QUOTA = 20;
    [SerializeField]
    float LEVEL_THREE_QUOTA = 30;
    [SerializeField]
    float LEVEL_FOUR_QUOTA = 40;
    [SerializeField]
    float WIN_QUOTA = 50;

    [SerializeField]
    int killCount = 0;

    [SerializeField]
    GameObject character;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    GameObject mainCamContainer;
    [SerializeField]
    GameObject particlesHappy;
    [SerializeField]
    GameObject homeReminder;
    [SerializeField]
    GameObject winReminder;

    [SerializeField]
    AnimationManager dropAnimationManager;

    [SerializeField]
    float deltaModifier = 1;
    [SerializeField]
    int difficulty = 1;


    Vector3 ogCamCharComparison = new Vector3();
    List<Obstacle> obstacles;
    List<Parent> flames;

    CharacterState oldCharacterState = CharacterState.IDLE;
    CharacterState charState = CharacterState.IDLE;
    float preCharX, preCharZ;
    float deltaRun;
    float deltaTimer;
    bool gameStart = false;

    float
        charX,
        charY,
        charZ,
        camX,
        camY,
        camZ;

    float shakeScreenTime = 0;
    bool controlBlock = true;
    bool hitActionButton;
    bool ifHitPending = false;

    void Start() {
        ProInput.Init();

        //Obj load
        obstacles = AllOfClass.PickAllOf<Obstacle>();
        flames = AllOfClass.PickAllOf<Parent>();

        //setups
        SetupFlames();
        flames[0].Activate();
        gameStart = false;

        particlesHappy.SetActive(false);
        //Camera setup
        ogCamCharComparison =  character.transform.localPosition - mainCamContainer.transform.localPosition;
        homeReminder.SetActive(false);
        winReminder.SetActive(false);
        //init
        charState = CharacterState.IDLE; // CHANGE FOR OP
        ManageAnimations();
        StartCoroutine("GameInit");
    }

    IEnumerator GameInit() {
        yield return new WaitForSeconds(4);
        song.Play();
        controlBlock = false;
    }

    IEnumerator GameEnd() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("intro");
    }

    void Update() {
        float deltaBase = Time.deltaTime;
        deltaRun = deltaBase * deltaModifier;
        deltaTimer = deltaBase;
        ProInput.UpdateInput(deltaRun, debug: true);

        Vector3 charPosition = character.transform.localPosition;
        Vector3 camPosition = mainCamContainer.transform.localPosition;

        charX = charPosition.x;
        charY = charPosition.y;
        charZ = charPosition.z;

        camX = camPosition.x;
        camY = camPosition.y;
        camZ = camPosition.z;

        preCharX = charX;
        preCharZ = charZ;

        hitActionButton = ProInput.A;

        HandleControls();

        Flames();
        DealWithCollisions();
        ManageCamera();
        ManageAnimations();

        Vector3 newPos = new Vector3(charX, charY, charZ);
        character.transform.localPosition = newPos;
        Vector3 newcamPos = new Vector3(camX, camY, camZ);
        mainCamContainer.transform.localPosition = newcamPos;
    }

    private void HandleControls() {
        if (controlBlock)
            return;

        if (hitActionButton) {
            charState = CharacterState.HEAD_BONK;
        }

        AnalogueInput stick = ProInput.GlobalActionStick;

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
    }

    private void ManageAnimations() {
        if (charState != oldCharacterState) {
            oldCharacterState = charState;
            int animId = (int)charState;
            dropAnimationManager.JumpTo(Drop.anims[animId]);
        }
    }

    
    private void Flames() {
        bool anyHitsCollisionOrButton = false;
        bool happy = false;
        int parentNum = flames.Count;
        for (int i = 0; i<parentNum; i++) {
            Parent flameObj = flames[i];
            Transform obsTrans = flameObj.transform;
            Vector3 obsPos = flameObj.flamePivot.transform.localPosition;
            Vector3 obsScale = obsTrans.localScale;
            float obsX = obsPos.x;
            float obsZ = obsPos.z;
            float obsWidthHalf = obsScale.x*.5f + CHAR_SIZE.x*.5f;
            float obsDepthHalf = obsScale.z*.5f + CHAR_SIZE.z*.5f;

            bool inFlameZone = (charX > obsX - obsWidthHalf &&
                charX < obsX + obsWidthHalf &&
                charZ > obsZ - obsDepthHalf &&
                charZ < obsZ + obsDepthHalf);

            switch (flameObj.state) {
                case FlameState.OFF:
                    flameObj.timer = 0;
                    flameObj.lifeSpanTimer = 0;
                    float possibilities = FLAME_POSSIBILITY_Z1;
                    switch (difficulty) {
                        case 1:
                            possibilities = FLAME_POSSIBILITY_Z1;
                            break;
                        case 2:
                            possibilities = FLAME_POSSIBILITY_Z2;
                            break;
                        case 3:
                            possibilities = FLAME_POSSIBILITY_Z3;
                            break;
                        case 4:
                            possibilities = FLAME_POSSIBILITY_Z4;
                            break;
                        case 5:
                            possibilities = FLAME_POSSIBILITY_Z4;
                            break;
                        default:
                            possibilities = 999999999999999;
                            break;
                    }
                    if (gameStart) {
                        if (Mathf.RoundToInt(UnityEngine.Random.value*possibilities) == 0) {
                            flameObj.Activate();
                        }
                    }
                    break;
                    //light active states
                case FlameState.LIGHT_DOWN:
                case FlameState.FIRE_ON:
                    if (gameStart) {
                        flameObj.lifeSpanTimer += deltaTimer;
                        if (flameObj.lifeSpanTimer > LIGHT_LIMIT_LIFESPAN-LIGHT_SHINE_DOWN) {
                            float perc = (1-(flameObj.lifeSpanTimer-(LIGHT_LIMIT_LIFESPAN-LIGHT_SHINE_DOWN))/LIGHT_SHINE_DOWN)*LIGHT_ALPHA_LIMIT;
                            flameObj.shine.color = new Vector4(perc, perc, perc, 1);
                        }
                   

                        if (flameObj.lifeSpanTimer > LIGHT_LIMIT_LIFESPAN) {
                            if (flameObj.state != FlameState.FIRE_ON) {
                                flameObj.ResetFlame();
                                flameObj.state = FlameState.OFF;
                            }
                        }
                    }
                    break;
                case FlameState.CUTSCENE_ACTIVE:
                    break;
                case FlameState.OH_NO:
                    break;
            }
            switch (flameObj.state) {
                case FlameState.LIGHT_DOWN:
                    flameObj.timer += deltaTimer;
                    if (inFlameZone && flameObj.timer > LIGHT_TIME_SECONDS - LIGHT_BONUS_TIME) {
                        flameObj.timer = LIGHT_TIME_SECONDS - LIGHT_BONUS_TIME;
                    }
                    if (flameObj.timer > LIGHT_TIME_SECONDS) {
                        //if not winned
                        if (difficulty == 5 && killCount>WIN_QUOTA) {
                        } else {
                            flameObj.state = FlameState.FIRE_ON;
                            gameStart = true;
                            flameObj.flame.actualFlame = FLAME_SHOWS_UP_ALPHA;
                            ShakeCam(CAM_SHAKE_TIME, true);
                        }
                       
                    }
                    float shinePerc = flameObj.timer;
                    if (shinePerc > LIGHT_ALPHA_LIMIT) {
                        shinePerc = LIGHT_ALPHA_LIMIT;
                    }
                    if (flameObj.lifeSpanTimer <= LIGHT_LIMIT_LIFESPAN-LIGHT_SHINE_DOWN) {
                        flameObj.shine.color = new Vector4(shinePerc, shinePerc, shinePerc, 1);
                    }
                    if (inFlameZone) {
                        happy = true;
                    }
                    break;
                case FlameState.FIRE_ON:
                    
                    flameObj.flame.percentage += deltaTimer* FLAME_SPEED;
                    if (inFlameZone) {
                        anyHitsCollisionOrButton = true;
                        if (hitActionButton) {
                            if (ifHitPending) {
                                ifHitPending = false;
                                ShakeCam(CAM_SHAKE_TIME);
                                flameObj.flame.percentage -= FLAME_HIT;
                                flameObj.flame.actualFlame += FLAME_EFFECT_HIT;                                
                                if (flameObj.flame.percentage<0) {
                                    flameObj.state = FlameState.LIGHT_DOWN;
                                    flameObj.timer = LIGHT_TIME_SECONDS - LIGHT_BONUS_TIME;
                                    KilledOne();
                                }
                            }
                        }
                    }
                    if (flameObj.flame.percentage>1) {
                        flameObj.flame.percentage = 1;
                        flameObj.state = FlameState.OH_NO;                        
                        SceneManager.LoadScene("Game_over");
                        flameObj.flame.actualFlame = 100;
                        ShakeCam(CAM_SHAKE_TIME_DEATH);
                    }
                    if (flameObj.flame.percentage<0) {
                        flameObj.active = false;
                    }
                    break;
                case FlameState.OH_NO:
                    break;
                case FlameState.CUTSCENE_ACTIVE:
                    break;
            }
            flameObj.flame.ShowFlames(deltaTimer);
        }

        if (hitActionButton) {
            anyHitsCollisionOrButton = true;
            ifHitPending = false;
        }
        if (!hitActionButton) {
            ifHitPending = true;
        }
        if (happy) {
            particlesHappy.SetActive(true);
        } else {
            particlesHappy.SetActive(false);
        }
    }
    int killsSinceHomeMessage = 0;
    private void KilledOne() {
        bool levelSwap = false;
        killCount++;
        if (difficulty == 1 && killCount>LEVEL_ONE_QUOTA) {
            levelSwap = true;
            difficulty++;
        }
        if (difficulty == 2 && killCount>LEVEL_TWO_QUOTA) {
            levelSwap = true;
            difficulty++;
        }
        if (difficulty == 3 && killCount>LEVEL_THREE_QUOTA) {
            levelSwap = true;
            difficulty++;
        }
        if (difficulty == 4 && killCount>LEVEL_FOUR_QUOTA) {
            levelSwap = true;
            difficulty++;
        }
        if (difficulty == 5 && killCount>WIN_QUOTA) {
            if (winReminder.activeInHierarchy == false) {
                StartCoroutine("GameEnd");
            }
            homeReminder.SetActive(false);
            winReminder.SetActive(true);            
        }
        if (levelSwap) {
            homeReminder.SetActive(true);
            killsSinceHomeMessage = killCount;
        }
        if (killCount > killsSinceHomeMessage + 1) {
            homeReminder.SetActive(false);
        }
    }

    private void SetupFlames() {
        int parentNum = flames.Count;
        for (int i = 0; i<parentNum; i++) {
            Parent parent = flames[i];
            parent.ResetFlame();
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
                    shakeScreenTime += deltaTimer;
                }
            }
            mainCam.transform.localPosition = new Vector3(camInternalCam.x, camInternalCam.y, camInternalCam.z);
            shakeScreenTime -= CAM_SHAKE_RECOVER;
            if (cameShakeWithPause) {
                deltaModifier = .4f;
            }
        } else {
            deltaModifier = 1;
            mainCam.transform.localPosition = zero;
        }

    }

    bool cameShakeWithPause = false;

    public void ShakeCam(float shake, bool pause = false) {
        shakeScreenTime = shake;
        cameShakeWithPause = pause;
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
                xDif = charX - obsX;
                zDif = charZ - obsZ;
                angleCharCircle = Mathf.Atan2(zDif, xDif);
                distCharCircle = Mathf.Sqrt(xDif*xDif + zDif*zDif);
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