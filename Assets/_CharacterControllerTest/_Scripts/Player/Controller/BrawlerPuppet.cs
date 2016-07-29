using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;
using Brawler;


public class BrawlerPuppet : MonoBehaviour {

        public BehaviourPuppet puppet;
      //  public AnimatorCharacterController animatorCharacterController;
     //  public CharacterMovement characterMovement;
       
        

    public void OnLoseBalance()
    {


    //  characterMovement.enabled = false;
        Debug.Log("Character Input OFF");
        //   animatorCharacterController.enabled = false;
        puppet.puppetMaster.targetAnimator.enabled = false;
    }

    public void OnRegainBalance()
    {

      //  characterMovement.enabled = true;
        Debug.Log("Character Input ON");
        //   animatorCharacterController.enabled = true;
        puppet.puppetMaster.targetAnimator.enabled = true;
    }
}
