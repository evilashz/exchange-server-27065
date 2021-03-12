using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D6 RID: 214
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x0002A43E File Offset: 0x0002863E
		[__DynamicallyInvokable]
		public DivideByZeroException() : base(Environment.GetResourceString("Arg_DivideByZero"))
		{
			base.SetErrorCode(-2147352558);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002A45B File Offset: 0x0002865B
		[__DynamicallyInvokable]
		public DivideByZeroException(string message) : base(message)
		{
			base.SetErrorCode(-2147352558);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002A46F File Offset: 0x0002866F
		[__DynamicallyInvokable]
		public DivideByZeroException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147352558);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002A484 File Offset: 0x00028684
		protected DivideByZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
