using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000112 RID: 274
	[ComVisible(true)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		// Token: 0x0600106E RID: 4206 RVA: 0x00031420 File Offset: 0x0002F620
		public MulticastNotSupportedException() : base(Environment.GetResourceString("Arg_MulticastNotSupportedException"))
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003143D File Offset: 0x0002F63D
		public MulticastNotSupportedException(string message) : base(message)
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00031451 File Offset: 0x0002F651
		public MulticastNotSupportedException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00031466 File Offset: 0x0002F666
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
