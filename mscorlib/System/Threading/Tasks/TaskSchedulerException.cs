using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x02000547 RID: 1351
	[__DynamicallyInvokable]
	[Serializable]
	public class TaskSchedulerException : Exception
	{
		// Token: 0x06004072 RID: 16498 RVA: 0x000F0326 File Offset: 0x000EE526
		[__DynamicallyInvokable]
		public TaskSchedulerException() : base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"))
		{
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000F0338 File Offset: 0x000EE538
		[__DynamicallyInvokable]
		public TaskSchedulerException(string message) : base(message)
		{
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x000F0341 File Offset: 0x000EE541
		[__DynamicallyInvokable]
		public TaskSchedulerException(Exception innerException) : base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"), innerException)
		{
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000F0354 File Offset: 0x000EE554
		[__DynamicallyInvokable]
		public TaskSchedulerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x000F035E File Offset: 0x000EE55E
		protected TaskSchedulerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
