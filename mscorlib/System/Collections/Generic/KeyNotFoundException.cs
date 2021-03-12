using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004AD RID: 1197
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class KeyNotFoundException : SystemException, ISerializable
	{
		// Token: 0x060039C4 RID: 14788 RVA: 0x000DB020 File Offset: 0x000D9220
		[__DynamicallyInvokable]
		public KeyNotFoundException() : base(Environment.GetResourceString("Arg_KeyNotFound"))
		{
			base.SetErrorCode(-2146232969);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000DB03D File Offset: 0x000D923D
		[__DynamicallyInvokable]
		public KeyNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2146232969);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000DB051 File Offset: 0x000D9251
		[__DynamicallyInvokable]
		public KeyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146232969);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000DB066 File Offset: 0x000D9266
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
