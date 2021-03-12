using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200010D RID: 269
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x00031045 File Offset: 0x0002F245
		[__DynamicallyInvokable]
		public MethodAccessException() : base(Environment.GetResourceString("Arg_MethodAccessException"))
		{
			base.SetErrorCode(-2146233072);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00031062 File Offset: 0x0002F262
		[__DynamicallyInvokable]
		public MethodAccessException(string message) : base(message)
		{
			base.SetErrorCode(-2146233072);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00031076 File Offset: 0x0002F276
		[__DynamicallyInvokable]
		public MethodAccessException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233072);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003108B File Offset: 0x0002F28B
		protected MethodAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
