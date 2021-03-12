using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093B RID: 2363
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		// Token: 0x0600610C RID: 24844 RVA: 0x0014AC55 File Offset: 0x00148E55
		[__DynamicallyInvokable]
		public InvalidComObjectException() : base(Environment.GetResourceString("Arg_InvalidComObjectException"))
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x0014AC72 File Offset: 0x00148E72
		[__DynamicallyInvokable]
		public InvalidComObjectException(string message) : base(message)
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0014AC86 File Offset: 0x00148E86
		[__DynamicallyInvokable]
		public InvalidComObjectException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x0014AC9B File Offset: 0x00148E9B
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
