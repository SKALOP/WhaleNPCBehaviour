using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class WanderActionTask : ActionTask{
	float timer = 3;
	float rotationangle;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
			GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
			Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		float lv = agentBlackboard.GetVariableValue<float>("LeaderValue");

		//randomize the values at the start of the scene so that not all walls go into certain states at the same time
		agentBlackboard.SetVariableValue("oxygen",Random.Range(75f,175f));
				agentBlackboard.SetVariableValue("LeaderValue",Random.Range(50f,500f));

		return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
		rotationangle = Random.Range(0,360);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
		Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
			GameObject w = agentBlackboard.GetVariableValue<GameObject>("Whale");
			Rigidbody rb = agentBlackboard.GetVariableValue<Rigidbody>("WhaleRigidbody");
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		float lv = agentBlackboard.GetVariableValue<float>("LeaderValue");
		
			
			

			//ticks down a timer, when the timer is done, randomly change the orientation of the whale. Reset the timer to 7 to repeat the process.
			
				timer -= Time.deltaTime;
				if (timer <= 0)
				{
					w.transform.rotation = Quaternion.Euler(0,Random.Range(90, 271), Random.Range(-30,30));
					timer = 7;
				}
				//have the whale continuously move where it looks
				rb.velocity = -w.transform.right * 1;
				//tick down the checks that might move the whale into other behaviours
				agentBlackboard.SetVariableValue("oxygen",o2 -0.01f);
				agentBlackboard.SetVariableValue("LeaderValue",lv -0.01f);

				//makes sure the whale doesn't start wandering above the surface
				if(w.transform.position.y >= 14){
				//if it tries to, make it look downwards instead
				w.transform.rotation = Quaternion.Euler(0, rotationangle, 45);
				}
		
		}

		//Called when the task is disabled.
		protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}