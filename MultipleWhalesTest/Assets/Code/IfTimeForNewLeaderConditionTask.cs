using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions{

	public class IfTimeForNewLeaderConditionTask : ConditionTask{

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
		float lv = agentBlackboard.GetVariableValue<float>("LeaderValue");
		//checks if the whale's call timer has come
		//if it has, take the whale into the next state, do the ping (not in this script) and reset the whale's leadervalue to full
		if(lv <= 0){
		agentBlackboard.SetVariableValue("LeaderValue",100f);
			return true;
			
			}
			else{
				return false;
			}
		}
	}
}