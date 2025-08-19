using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class SurfaceActionTask : ActionTask{

	float rotationangle;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
				Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
			Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		//creates a random orientation value for the whale when it leaves
		rotationangle = Random.Range(0,360);
		//on starting, make the whale rotate upwards at a 45 degree and in a random sideways direction
		
		w.transform.Rotate(0, Random.Range(90, 271), -45);
		//move the whale in that direction
		rb.velocity = -w.transform.right * 1;
		//this simulates the whale going up, and makes sure each whale doesn't go up in the same direction
		return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
	Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
		Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		w.transform.rotation = Quaternion.Euler(0,rotationangle,0);

		w.transform.rotation = Quaternion.Euler(0,rotationangle, -45);
		rb.velocity = -w.transform.right * 1;
		//if the whale reaches the surface, make it look downward, reset it's oxygen to full, and take the whale to the wander state
	if(w.transform.position.y >= 14){
	w.transform.Rotate(0, Random.Range(90, 271), 45);
		agentBlackboard.SetVariableValue("oxygen",100f);

			EndAction(true);
			}
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
	
		}

		//Called when the task is disabled.
		protected override void OnStop(){
		
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}