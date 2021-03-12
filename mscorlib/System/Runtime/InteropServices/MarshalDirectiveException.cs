using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000925 RID: 2341
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		// Token: 0x06006090 RID: 24720 RVA: 0x00149A10 File Offset: 0x00147C10
		[__DynamicallyInvokable]
		public MarshalDirectiveException() : base(Environment.GetResourceString("Arg_MarshalDirectiveException"))
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x00149A2D File Offset: 0x00147C2D
		[__DynamicallyInvokable]
		public MarshalDirectiveException(string message) : base(message)
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x00149A41 File Offset: 0x00147C41
		[__DynamicallyInvokable]
		public MarshalDirectiveException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x00149A56 File Offset: 0x00147C56
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
