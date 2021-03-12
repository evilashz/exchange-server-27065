using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000AA RID: 170
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArithmeticException : SystemException
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x0001F6A2 File Offset: 0x0001D8A2
		[__DynamicallyInvokable]
		public ArithmeticException() : base(Environment.GetResourceString("Arg_ArithmeticException"))
		{
			base.SetErrorCode(-2147024362);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001F6BF File Offset: 0x0001D8BF
		[__DynamicallyInvokable]
		public ArithmeticException(string message) : base(message)
		{
			base.SetErrorCode(-2147024362);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001F6D3 File Offset: 0x0001D8D3
		[__DynamicallyInvokable]
		public ArithmeticException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024362);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001F6E8 File Offset: 0x0001D8E8
		protected ArithmeticException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
