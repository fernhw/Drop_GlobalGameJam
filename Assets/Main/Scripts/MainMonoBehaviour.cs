using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProInputSystem;

public class MainMonoBehaviour : MonoBehaviour
{
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
    GameObject character;
    [SerializeField]
    Camera camera;

    [SerializeField]
    float deltaModifier = 1;

    Vector3 ogCamCharComparison = new Vector3();

    void Start(){
        ProInput.Init();

        //Camera setup
        ogCamCharComparison =  character.transform.localPosition - camera.transform.localPosition;
    }
    
    void Update(){
        float deltaBase = Time.deltaTime;
        float deltaRun = deltaBase * deltaModifier;
        ProInput.UpdateInput(deltaRun, debug:true);

        Vector3 charPosition = character.transform.localPosition;
        Vector3 camPosition = camera.transform.localPosition;

        AnalogueInput stick = ProInput.GlobalActionStick;
       

        float
            charX = charPosition.x,
            charY = charPosition.y,
            charZ = charPosition.z,
            camX = camPosition.x,
            camY = camPosition.y,
            camZ = camPosition.z
            ;
        
        print(stick.Distance);
        if (stick.IsActive()) {
            float stickAngle = stick.Angle;
            float stickCos = Mathf.Cos(stickAngle);
            float stickSin = Mathf.Sin(stickAngle);
          
            float realJoystickPress = (stick.Distance-stick.DeadZone)/(1-stick.DeadZone);
            print(realJoystickPress);
            if (realJoystickPress> JOYSTICK_RUN) {
                charX += stickCos * CHAR_SPEED_RUN * deltaRun;
                charZ -= stickSin * CHAR_SPEED_RUN * deltaRun;
            } else {
                charX += stickCos * CHAR_SPEED * deltaRun;
                charZ -= stickSin * CHAR_SPEED * deltaRun;
            }
        }

        //Correction

        //Camera
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

        Vector3 newPos = new Vector3(charX, charY, charZ);
        character.transform.localPosition = newPos;
        Vector3 newcamPos = new Vector3(camX, camY, camZ);
        camera.transform.localPosition = newcamPos;
    }

    private void OnDrawGizmos() {
        
    }
}
