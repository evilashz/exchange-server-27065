using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200017D RID: 381
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class DirectoryNotFoundException : IOException
	{
		// Token: 0x06001782 RID: 6018 RVA: 0x0004B5CB File Offset: 0x000497CB
		[__DynamicallyInvokable]
		public DirectoryNotFoundException() : base(Environment.GetResourceString("Arg_DirectoryNotFoundException"))
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004B5E8 File Offset: 0x000497E8
		[__DynamicallyInvokable]
		public DirectoryNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0004B5FC File Offset: 0x000497FC
		[__DynamicallyInvokable]
		public DirectoryNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0004B611 File Offset: 0x00049811
		protected DirectoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
