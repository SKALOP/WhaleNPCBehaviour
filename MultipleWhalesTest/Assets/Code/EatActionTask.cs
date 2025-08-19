using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class EatActionTask : ActionTask{
	LayerMask mask ;
	float closestDistance = Mathf.Infinity;
		int currentClosestCollider = 0;
		int iterator = 0;
		Collider[] hitColliders ;
		Vector3 Magnitude;
		float num;
		bool spinning = false;
		float num2;
		public float orbitRange;
		bool doAPlanktonCheck = true;
		float num3;
		bool comingInFromLeft = false;
		bool comingInFromLeft2 = false;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			
			Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
		Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		//make sure the check is only looking at planktons
		mask =  LayerMask.GetMask("Plankton");
		//get an array of all the plankton within a 100 unit radius from the whale
			 hitColliders = Physics.OverlapSphere(w.transform.position,100,mask);
			 //randomizes how far out the whale rotates around the plankton
			 orbitRange = Random.Range(1.5f,3.5f);
		
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		float lv = agentBlackboard.GetVariableValue<float>("LeaderValue");


		

			 return null;
			 
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
		Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
		Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		
		
	

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
		Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
		Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		float lv = agentBlackboard.GetVariableValue<float>("LeaderValue");
	
			//finds all the planktons in the game
		//determines the closest one to the whale to use
		if(doAPlanktonCheck){
			foreach(Collider hitCollider in hitColliders)
		{
		
		
		float currentDistance = Vector3.Distance(hitCollider.transform.position,w.transform.position);
		if(currentDistance<closestDistance){
			closestDistance = currentDistance;
			currentClosestCollider = iterator;
		
		}
		iterator +=1;
		}
		doAPlanktonCheck = false;
		}


		Magnitude = new Vector3 (w.transform.position.x - hitColliders[currentClosestCollider].transform.position.x,w.transform.position.y - hitColliders[currentClosestCollider].transform.position.y, w.transform.position.z - hitColliders[currentClosestCollider].transform.position.z);
			num = Vector3.Distance(w.transform.position,hitColliders[currentClosestCollider].transform.position);
		 num2 = Mathf.Atan2(Magnitude.z, Magnitude.x);
		 num3 = Mathf.Atan2(Magnitude.y, Magnitude.x);
		//sets the orientation of the whale in a certain way upon entering the orbit range, depending on what direction the whale is entering the range from, that tries to have it angle itself around the plankton in a believable way
		if(num <= orbitRange && spinning == false){
		spinning = true;
		w.transform.rotation = Quaternion.Euler(0,0,0);
				w.transform.rotation = Quaternion.Euler(0,315,0);
				if(comingInFromLeft){
				w.transform.rotation = Quaternion.Euler(0,-315 + 180,0);

				}
				if(comingInFromLeft2){
				w.transform.rotation = Quaternion.Euler(0,-315 + 90,0);
		
				}
				rb.velocity = Vector3.zero;
	
			}
			if( spinning == true){
		
		//makes the whale rotate around the plankton
		rb.velocity = Vector3.zero;
			w.transform.RotateAround(hitColliders[currentClosestCollider].transform.position, -Vector3.up, 0.1f );
			if(comingInFromLeft){
				w.transform.RotateAround(hitColliders[currentClosestCollider].transform.position, -Vector3.up, -0.1f );
				comingInFromLeft = false;
			}
			if(comingInFromLeft2){
				w.transform.RotateAround(hitColliders[currentClosestCollider].transform.position, -Vector3.up, -0.1f );
				comingInFromLeft2 = false;
				}
		
			}
			else
			{

	//if the whale isn't within orbit range, then it has to move towards it. This code controls the whale's orientation towards the plankton 
	//This is harder since the whale can be above or below the plankton too. The long code below helps determine what quadrant the whale is in in relation to the plankton, and changes it's orientation to make it look realistic accordingly, 
	//and have the whale look right at the plankton as it swims
				Magnitude = new Vector3 (w.transform.position.x - hitColliders[currentClosestCollider].transform.position.x,w.transform.position.y - hitColliders[currentClosestCollider].transform.position.y, w.transform.position.z - hitColliders[currentClosestCollider].transform.position.z);
		 num3 = Mathf.Atan2(Magnitude.y, Magnitude.z);
		 if(w.transform.position.z < hitColliders[currentClosestCollider].transform.position.z && w.transform.position.x < hitColliders[currentClosestCollider].transform.position.x){
		 num3 = Mathf.Atan2(Magnitude.y, -Magnitude.z);
		 w.transform.rotation = Quaternion.Euler(0,-num2 * Mathf.Rad2Deg,num3 * Mathf.Rad2Deg);
		 comingInFromLeft = true;
		 }
		  if(w.transform.position.z < hitColliders[currentClosestCollider].transform.position.z && w.transform.position.x > hitColliders[currentClosestCollider].transform.position.x){
		  num3 = Mathf.Atan2(Magnitude.y, -Magnitude.z);
		 w.transform.rotation = Quaternion.Euler(0,-num2 * Mathf.Rad2Deg,num3 * Mathf.Rad2Deg);
		 comingInFromLeft2 = true;
		 }
		 //check
		  if(w.transform.position.z > hitColliders[currentClosestCollider].transform.position.z && w.transform.position.x < hitColliders[currentClosestCollider].transform.position.x){
		 w.transform.rotation = Quaternion.Euler(0,-num2 * Mathf.Rad2Deg,num3 * Mathf.Rad2Deg);
		 }
		 //check
		  if(w.transform.position.z > hitColliders[currentClosestCollider].transform.position.z && w.transform.position.x > hitColliders[currentClosestCollider].transform.position.x){
		 w.transform.rotation = Quaternion.Euler(0,-num2 * Mathf.Rad2Deg,num3 * Mathf.Rad2Deg);
		 }

		rb.velocity = -1 *w.transform.right  ;
			}
			agentBlackboard.SetVariableValue("oxygen",o2 -0.01f);
			agentBlackboard.SetVariableValue("LeaderValue",lv -0.01f);
		}


		//makes sure the values are reset for the next time it attempts to eat, it won't use the old closest plankton area.
		//Called when the task is disabled.
		protected override void OnStop(){
		 closestDistance = Mathf.Infinity;
		 currentClosestCollider = 0;
		 iterator = 0;
		spinning = false;
		doAPlanktonCheck = true;
	 Magnitude = Vector3.zero;
	 num = 0;
	 num2 = 0;
	 num3 = 0;
	
		}

		//Called when the task is paused.
		protected override void OnPause(){
			 closestDistance = Mathf.Infinity;
		currentClosestCollider = 0;
		 iterator = 0;
		spinning = false;
		doAPlanktonCheck = true;
		 Magnitude = Vector3.zero;
	num = 0;
		num2 = 0;
		num3 = 0;
		}
	}
}