using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200011E RID: 286
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		// Token: 0x060010D6 RID: 4310 RVA: 0x00032C57 File Offset: 0x00030E57
		[__DynamicallyInvokable]
		public OverflowException() : base(Environment.GetResourceString("Arg_OverflowException"))
		{
			base.SetErrorCode(-2146233066);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00032C74 File Offset: 0x00030E74
		[__DynamicallyInvokable]
		public OverflowException(string message) : base(message)
		{
			base.SetErrorCode(-2146233066);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00032C88 File Offset: 0x00030E88
		[__DynamicallyInvokable]
		public OverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233066);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00032C9D File Offset: 0x00030E9D
		protected OverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
