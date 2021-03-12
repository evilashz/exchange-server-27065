using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200053B RID: 1339
	internal interface ITaskCompletionAction
	{
		// Token: 0x0600403E RID: 16446
		void Invoke(Task completingTask);
	}
}
