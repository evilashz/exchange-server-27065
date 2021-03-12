using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F6 RID: 246
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		// Token: 0x06000F0D RID: 3853 RVA: 0x0002EDB1 File Offset: 0x0002CFB1
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException() : base(Environment.GetResourceString("Arg_InsufficientExecutionStackException"))
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002EDCE File Offset: 0x0002CFCE
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException(string message) : base(message)
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002EDE2 File Offset: 0x0002CFE2
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002EDF7 File Offset: 0x0002CFF7
		private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
