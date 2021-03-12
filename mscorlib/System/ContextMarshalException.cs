using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000CA RID: 202
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00025063 File Offset: 0x00023263
		public ContextMarshalException() : base(Environment.GetResourceString("Arg_ContextMarshalException"))
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00025080 File Offset: 0x00023280
		public ContextMarshalException(string message) : base(message)
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00025094 File Offset: 0x00023294
		public ContextMarshalException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000250A9 File Offset: 0x000232A9
		protected ContextMarshalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
