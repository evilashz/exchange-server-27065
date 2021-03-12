using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000142 RID: 322
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TimeoutException : SystemException
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x00038742 File Offset: 0x00036942
		[__DynamicallyInvokable]
		public TimeoutException() : base(Environment.GetResourceString("Arg_TimeoutException"))
		{
			base.SetErrorCode(-2146233083);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003875F File Offset: 0x0003695F
		[__DynamicallyInvokable]
		public TimeoutException(string message) : base(message)
		{
			base.SetErrorCode(-2146233083);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00038773 File Offset: 0x00036973
		[__DynamicallyInvokable]
		public TimeoutException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233083);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00038788 File Offset: 0x00036988
		protected TimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
