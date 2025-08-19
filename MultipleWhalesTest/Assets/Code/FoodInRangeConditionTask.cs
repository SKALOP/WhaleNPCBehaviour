using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions{

	public class FoodInRangeConditionTask : ConditionTask{
	float VisionRange = 5;
	LayerMask mask;
Collider[] hitColliders;
float closestDistance = Mathf.Infinity;
		int currentClosestCollider = 0;
		int iterator = 0;
		Vector3 Magnitude;
		float num;
		bool doAPlanktonCheck = true;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
		Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		//make sure we will only be looking at plankton objects
		mask =  LayerMask.GetMask("Plankton");
		//get every plankton within a 100 unity radius
			 hitColliders = Physics.OverlapSphere(w.transform.position,100,mask);
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable(){
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable(){
			 closestDistance = Mathf.Infinity;
		 currentClosestCollider = 0;
		 iterator = 0;
	bool doAPlanktonCheck = true;
	 Magnitude = Vector3.zero;
	 num = 0;
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck(){
		Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
	
		//finds all the planktons in the game
		//determines the closest one to the whale to use
	if(doAPlanktonCheck){
		foreach(Collider hitCollider in hitColliders)
		{
		Debug.Log("Finding Nearest Food");
		
		float currentDistance = Vector3.Distance(hitCollider.transform.position,w.transform.position);
		if(currentDistance<closestDistance){
			closestDistance = currentDistance;
			currentClosestCollider = iterator;
			
		}
		iterator +=1;
		}
		doAPlanktonCheck = false;
		}
		//find the space between the two objects
		num = Vector3.Distance(w.transform.position,hitColliders[currentClosestCollider].transform.position);

		//if they are close enough, take the whale to the eat task
			if(num <= VisionRange)
            {
			w.transform.rotation = Quaternion.Euler(0,0,0);
				return true;
            }
            else
            {
				return false;
            }
		}
	}
}