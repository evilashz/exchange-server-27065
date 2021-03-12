using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004E5 RID: 1253
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		// Token: 0x06003BF4 RID: 15348 RVA: 0x000E16EA File Offset: 0x000DF8EA
		[__DynamicallyInvokable]
		public SynchronizationLockException() : base(Environment.GetResourceString("Arg_SynchronizationLockException"))
		{
			base.SetErrorCode(-2146233064);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000E1707 File Offset: 0x000DF907
		[__DynamicallyInvokable]
		public SynchronizationLockException(string message) : base(message)
		{
			base.SetErrorCode(-2146233064);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000E171B File Offset: 0x000DF91B
		[__DynamicallyInvokable]
		public SynchronizationLockException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233064);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000E1730 File Offset: 0x000DF930
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
