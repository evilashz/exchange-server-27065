using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000582 RID: 1410
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		// Token: 0x0600424B RID: 16971 RVA: 0x000F5D71 File Offset: 0x000F3F71
		[__DynamicallyInvokable]
		public AmbiguousMatchException() : base(Environment.GetResourceString("RFLCT.Ambiguous"))
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x000F5D8E File Offset: 0x000F3F8E
		[__DynamicallyInvokable]
		public AmbiguousMatchException(string message) : base(message)
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x000F5DA2 File Offset: 0x000F3FA2
		[__DynamicallyInvokable]
		public AmbiguousMatchException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000F5DB7 File Offset: 0x000F3FB7
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
