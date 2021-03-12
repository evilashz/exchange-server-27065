using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005F6 RID: 1526
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		// Token: 0x0600479C RID: 18332 RVA: 0x00102F8B File Offset: 0x0010118B
		[__DynamicallyInvokable]
		public TargetParameterCountException() : base(Environment.GetResourceString("Arg_TargetParameterCountException"))
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x00102FA8 File Offset: 0x001011A8
		[__DynamicallyInvokable]
		public TargetParameterCountException(string message) : base(message)
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x00102FBC File Offset: 0x001011BC
		[__DynamicallyInvokable]
		public TargetParameterCountException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00102FD1 File Offset: 0x001011D1
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
