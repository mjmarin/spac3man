namespace QuestSystem{
	public class Quest {
		public string description{get;set;}
		public bool completed{get;set;}
		public int currentAmount{get;set;}
		public int requiredAmount{get;set;}

		public void Evaluate(){
			if(currentAmount >= requiredAmount){
				Completed();
			}
		}

		public void Completed(){
			completed = true;
		}
	}

}
