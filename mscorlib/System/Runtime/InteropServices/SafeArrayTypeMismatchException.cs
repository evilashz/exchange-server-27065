using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094B RID: 2379
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		// Token: 0x06006146 RID: 24902 RVA: 0x0014C4CA File Offset: 0x0014A6CA
		[__DynamicallyInvokable]
		public SafeArrayTypeMismatchException() : base(Environment.GetResourceString("Arg_SafeArrayTypeMismatchException"))
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0014C4E7 File Offset: 0x0014A6E7
		[__DynamicallyInvokable]
		public SafeArrayTypeMismatchException(string message) : base(message)
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0014C4FB File Offset: 0x0014A6FB
		[__DynamicallyInvokable]
		public SafeArrayTypeMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0014C510 File Offset: 0x0014A710
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
