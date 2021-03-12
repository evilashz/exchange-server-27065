using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F1 RID: 241
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x0002ED15 File Offset: 0x0002CF15
		[__DynamicallyInvokable]
		public IndexOutOfRangeException() : base(Environment.GetResourceString("Arg_IndexOutOfRangeException"))
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002ED32 File Offset: 0x0002CF32
		[__DynamicallyInvokable]
		public IndexOutOfRangeException(string message) : base(message)
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002ED46 File Offset: 0x0002CF46
		[__DynamicallyInvokable]
		public IndexOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002ED5B File Offset: 0x0002CF5B
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
