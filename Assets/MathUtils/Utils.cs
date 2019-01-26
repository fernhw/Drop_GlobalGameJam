using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MathUtils {


    public static class Utils {


        public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour {
            T component = obj.GetComponent<T>();
            if (component == null) {
                Debug.LogError("Expected to find component of type"
                   + typeof(T) + " but found none", obj);
            }
            return component;
        }

        public static float VectorDistance(float xDifference, float yDifference) {
            return Mathf.Sqrt(xDifference*xDifference +yDifference*yDifference);
        }

        public static List<I> AllOfClass<I>() where I : class {
            MonoBehaviour[] monoBehaviours = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
            List<I> list = new List<I>();

            foreach (MonoBehaviour behaviour in monoBehaviours) {
                I component = behaviour.GetComponent(typeof(I)) as I;

                if (component != null) {
                    list.Add(component);
                }
            }

            return list;
        }


		public static Vector3 boundToVector3(Vector3 boundedVector, Vector3 targetVector, Vector3 limits) {

			if (boundedVector.x < targetVector.x - limits.x) {
				boundedVector.x = targetVector.x - limits.x;
			}

			if (boundedVector.x > targetVector.x + limits.x) {
				boundedVector.x = targetVector.x + limits.x;
			}

			if (boundedVector.z < targetVector.z - limits.z) {
				boundedVector.z = targetVector.z - limits.z;
			}

			if (boundedVector.z > targetVector.z + limits.z) {
				boundedVector.z = targetVector.z + limits.z;
			}

			if (boundedVector.y < targetVector.y - limits.y) {
				boundedVector.y = targetVector.y - limits.y;
			}

			if (boundedVector.y > targetVector.y + limits.y) {
				boundedVector.y = targetVector.y + limits.y;
			}
			return boundedVector;
		}


		public static void SetX(this Transform transform, float x) {
            Vector3 newPosition =
               new Vector3(x, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }


        public static void SetY(this Transform transform, float y) {
            Vector3 newPosition =
               new Vector3(transform.position.x, y, transform.position.z);
            transform.position = newPosition;
        }


        public static void SetZ(this Transform transform, float z) {
            Vector3 newPosition =
               new Vector3(transform.position.x, transform.position.y, z);
            transform.position = newPosition;
        }


        public static void gotoAndStop(this Animator animator, float frame, float numberOfFrames, string idOfAnimation) {
            animator.Play(idOfAnimation, 0, frame / (numberOfFrames));

            animator.speed = 0;
        }


        public static void gotoAndStop(this Animator animator, float percentage, string idOfAnimation) {
            animator.Play(idOfAnimation, 0, percentage);
            animator.speed = 0;
        }


		public static Vector3 lagger(Vector3 oldVector, Vector3 newVector, float speed, float deltaTime) {
			float targX;
			float targY;
			float targZ;
			targX = oldVector.x + (newVector.x - oldVector.x) * deltaTime * speed;
			targY = oldVector.y + (newVector.y - oldVector.y) * deltaTime * speed;
            targZ = oldVector.z + (newVector.z - oldVector.z) * deltaTime * speed;
			return new Vector3(targX, targY, targZ);
		}


		public static float lagger(float oldVector, float newVector, float speed, float deltaTime) {
			float final = oldVector + (newVector - oldVector) * deltaTime * speed;
			return final;
		}

        
        public static string stringPadding(string text, int miniumSize, int bleed = 0) {
            string holdText = text;
            int stringSizePad = miniumSize - text.Length;
            if (stringSizePad < 0) {
                stringSizePad = 0;
            }
            for (int i = 0; i<stringSizePad; i++) {
                if (i< bleed) {
                    holdText = " " + holdText;
                } else {
                    holdText = holdText + " ";
                }

            }
            return holdText;
        }


    }
}