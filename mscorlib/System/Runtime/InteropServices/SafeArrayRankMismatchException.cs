using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094A RID: 2378
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		// Token: 0x06006142 RID: 24898 RVA: 0x0014C47A File Offset: 0x0014A67A
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException() : base(Environment.GetResourceString("Arg_SafeArrayRankMismatchException"))
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0014C497 File Offset: 0x0014A697
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException(string message) : base(message)
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x0014C4AB File Offset: 0x0014A6AB
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x0014C4C0 File Offset: 0x0014A6C0
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
