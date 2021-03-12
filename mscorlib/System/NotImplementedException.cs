using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000115 RID: 277
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class NotImplementedException : SystemException
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x000315B1 File Offset: 0x0002F7B1
		[__DynamicallyInvokable]
		public NotImplementedException() : base(Environment.GetResourceString("Arg_NotImplementedException"))
		{
			base.SetErrorCode(-2147467263);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000315CE File Offset: 0x0002F7CE
		[__DynamicallyInvokable]
		public NotImplementedException(string message) : base(message)
		{
			base.SetErrorCode(-2147467263);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000315E2 File Offset: 0x0002F7E2
		[__DynamicallyInvokable]
		public NotImplementedException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467263);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000315F7 File Offset: 0x0002F7F7
		protected NotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
