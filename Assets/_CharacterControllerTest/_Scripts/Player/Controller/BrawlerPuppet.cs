using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;
using Brawler;


public class BrawlerPuppet : MonoBehaviour {

        public BehaviourPuppet puppet;
        public AnimatorCharacterController animatorController;
        public CharacterMovement characterMovement;

    public void OnLoseBalance()
    {


        characterMovement.enabled = false;
        animatorController.enabled = false;
        puppet.puppetMaster.targetAnimator.SetLayerWeight(1, 0);
    }

    public void OnRegainBalance()
    {

        characterMovement.enabled = true;
        animatorController.enabled = true;
        puppet.puppetMaster.targetAnimator.SetLayerWeight(1, 1f);
    }
}
