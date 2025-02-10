using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
   public GameObject cam_1;
   public GameObject cam_2;
   public int Manager;
   public CinemachineVirtualCamera cCamera_1;
   public CinemachineVirtualCamera cCamera_2;
   public Transform corinne;
   public Transform flute;
   [SerializeField] private CinemachineBrain brain;
   
   public void ChangeCamera()
   {
      GetComponent<Animator>().SetTrigger("Switch");
   }

    void Start()
   {
      brain = Camera.main.GetComponent<CinemachineBrain>();
   }

   public void ManageCamera()
   {
      if (Manager == 0)
      {
         //cCamera_1.Follow = flute;
        // Camera_2();
         Manager = 1;
         cCamera_1.Follow = corinne;
         cCamera_1.LookAt = corinne;
      }
      else
      {
        // cCamera_1.Follow = corinne;
         //Camera_1();
         Manager = 0;
         cCamera_1.Follow = flute;
         cCamera_1.LookAt = flute;
      }
   }

   public void SwitchToCamera1()
   {
      cCamera_1.enabled = true;
      cCamera_2.enabled = false;
   }
   public void SwitchToCamera2()
   {
      cCamera_1.enabled = false;
      cCamera_2.enabled = true;
   }
   void Camera_1()
   {
      cam_1.SetActive(true);
      cam_2.SetActive(false);
   }

   void Camera_2()
   {
      cam_1.SetActive(false);
      cam_2.SetActive(true);
   }
}
