using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class FollowerActionTask : ActionTask{


	Vector3 Magnitude;
	float num;
	float num2;
	float num3;
	bool comingInFromLeft;
	bool comingInFromLeft2;
	bool following;
	float orbitRange = 3.5f;
	Rigidbody rb2;

	LayerMask mask;
Collider[] hitColliders;
float closestDistance = Mathf.Infinity;
		int currentClosestCollider = 0;
		int iterator = 0;
		bool doACarrotCheck = true;
	

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
		Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
			GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
			//makes sure the whale is only looking for carrots
		mask = LayerMask.GetMask("Carrot");
		hitColliders = Physics.OverlapSphere(w.transform.position,100,mask);


			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
		
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){



			Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
			GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
			Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
	
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		float lv = agentBlackboard.GetVariableValue<float>("LeaderValue");
	//while there is only one carrot in this scene, I set it up so that there could be more pods, and the whales will make a list of all of them, and go to the closest one when entering this behaviour
		if(doACarrotCheck){
		foreach(Collider hitCollider in hitColliders)
		{
	
		
		float currentDistance = Vector3.Distance(hitCollider.transform.position,w.transform.position);
		if(currentDistance<closestDistance){
			closestDistance = currentDistance;
			currentClosestCollider = iterator;
			Debug.Log(currentClosestCollider);
		}
		iterator +=1;
		}
		doACarrotCheck = false;
		}

		//need the carrot's speed to change the whale's in the same way
		rb2 = hitColliders[currentClosestCollider].GetComponent<Rigidbody>();
		//need the distance to evaluate the space between the carrot and whale
		Magnitude = new Vector3 (w.transform.position.x - hitColliders[currentClosestCollider].transform.position.x,w.transform.position.y - hitColliders[currentClosestCollider].transform.position.y, w.transform.position.z - hitColliders[currentClosestCollider].transform.position.z);
			num = Vector3.Distance(w.transform.position,hitColliders[currentClosestCollider].transform.position);
		 num2 = Mathf.Atan2(Magnitude.z, Magnitude.x);
		 num3 = Mathf.Atan2(Magnitude.y, Magnitude.x);
		 
		if(num > orbitRange){
			following = false;
		}
		if(num <= orbitRange && following == false){
		following = true;
	
				}
				if(following){
				//if the whale is in the range to swim as a pod, depending on where the carrot is looking (described in carrot on a stick script), changes the whale's orientation accordingly
				//makes the whale's velocity the same as the carrot
					rb.velocity = rb2.velocity;
					
					if(hitColliders[currentClosestCollider].transform.forward.x <-0.5){
					w.transform.rotation = Quaternion.Euler(0,270,0);
					}
					if(hitColliders[currentClosestCollider].transform.forward.x >0.5){
					w.transform.rotation = Quaternion.Euler(0,90,0);
					}
					if(hitColliders[currentClosestCollider].transform.forward.z <-0.5){
					w.transform.rotation = Quaternion.Euler(0,180,0);
					}
					if(hitColliders[currentClosestCollider].transform.forward.z >0.5){
					 w.transform.rotation = Quaternion.Euler(0,0,0);
					}

				}
				else{
				//same code as in eat, where if the whale is outside the main behaviour's required range, 
				//it has to move closer first, but because of vertical movement, this is more complicated, uses quadrants to determine where the whale is in relation to the pod, and have it's orientation work properly according to this
				 if(w.transform.position.z <hitColliders[currentClosestCollider].transform.position.z && w.transform.position.x < hitColliders[currentClosestCollider].transform.position.x){
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
		  num3 = Mathf.Atan2(Magnitude.y, Magnitude.z);
		 w.transform.rotation = Quaternion.Euler(0,-num2 * Mathf.Rad2Deg,num3 * Mathf.Rad2Deg);
		 }
		 //check
		  if(w.transform.position.z > hitColliders[currentClosestCollider].transform.position.z && w.transform.position.x > hitColliders[currentClosestCollider].transform.position.x){
		  num3 = Mathf.Atan2(Magnitude.y, Magnitude.z);
		 w.transform.rotation = Quaternion.Euler(0,-num2 * Mathf.Rad2Deg,num3 * Mathf.Rad2Deg);
		 }
		 rb.velocity = -1 *w.transform.right  ;
		 }
		 //ticks down the oxygen so that the whale can leave this state
		 agentBlackboard.SetVariableValue("oxygen",o2 -0.01f);
		 //resets the leader value while swimming in this state
			agentBlackboard.SetVariableValue("LeaderValue",100f);

		}

		//resets everything when the whale leaves this task.
		//Called when the task is disabled.
		protected override void OnStop(){
			 closestDistance = Mathf.Infinity;
		 currentClosestCollider = 0;
		 iterator = 0;
	bool doACarrotCheck = true;
	 Magnitude = Vector3.zero;
	 num = 0;
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}