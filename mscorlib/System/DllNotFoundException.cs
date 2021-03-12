using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DC RID: 220
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x0002BC4F File Offset: 0x00029E4F
		[__DynamicallyInvokable]
		public DllNotFoundException() : base(Environment.GetResourceString("Arg_DllNotFoundException"))
		{
			base.SetErrorCode(-2146233052);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0002BC6C File Offset: 0x00029E6C
		[__DynamicallyInvokable]
		public DllNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2146233052);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0002BC80 File Offset: 0x00029E80
		[__DynamicallyInvokable]
		public DllNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233052);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002BC95 File Offset: 0x00029E95
		protected DllNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
