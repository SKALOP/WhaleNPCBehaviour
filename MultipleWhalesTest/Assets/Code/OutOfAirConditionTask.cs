using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions{

	public class OutOfAirConditionTask : ConditionTask{

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable(){
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable(){
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck(){
		Blackboard agentBlackboard = agent.GetComponent<Blackboard>();
		//continously checks if the whale is out of air
		//if it is, take the whale to the surfacing state
		float o2 = agentBlackboard.GetVariableValue<float>("oxygen");
		if(o2 <= 0){
		
			return true;
			}
			else{
				return false;
			}
		}
	}
}