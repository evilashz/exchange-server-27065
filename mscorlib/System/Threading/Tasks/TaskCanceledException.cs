using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x02000546 RID: 1350
	[__DynamicallyInvokable]
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		// Token: 0x0600406C RID: 16492 RVA: 0x000F02B4 File Offset: 0x000EE4B4
		[__DynamicallyInvokable]
		public TaskCanceledException() : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"))
		{
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000F02C6 File Offset: 0x000EE4C6
		[__DynamicallyInvokable]
		public TaskCanceledException(string message) : base(message)
		{
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x000F02CF File Offset: 0x000EE4CF
		[__DynamicallyInvokable]
		public TaskCanceledException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000F02DC File Offset: 0x000EE4DC
		[__DynamicallyInvokable]
		public TaskCanceledException(Task task) : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"), (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this.m_canceledTask = task;
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x000F0314 File Offset: 0x000EE514
		protected TaskCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000F031E File Offset: 0x000EE51E
		[__DynamicallyInvokable]
		public Task Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_canceledTask;
			}
		}

		// Token: 0x04001AA6 RID: 6822
		[NonSerialized]
		private Task m_canceledTask;
	}
}
