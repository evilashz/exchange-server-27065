using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000123 RID: 291
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		// Token: 0x060010EB RID: 4331 RVA: 0x00032E0D File Offset: 0x0003100D
		[__DynamicallyInvokable]
		public PlatformNotSupportedException() : base(Environment.GetResourceString("Arg_PlatformNotSupported"))
		{
			base.SetErrorCode(-2146233031);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00032E2A File Offset: 0x0003102A
		[__DynamicallyInvokable]
		public PlatformNotSupportedException(string message) : base(message)
		{
			base.SetErrorCode(-2146233031);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00032E3E File Offset: 0x0003103E
		[__DynamicallyInvokable]
		public PlatformNotSupportedException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233031);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00032E53 File Offset: 0x00031053
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
