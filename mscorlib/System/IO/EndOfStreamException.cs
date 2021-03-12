using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000181 RID: 385
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x0004BAD1 File Offset: 0x00049CD1
		[__DynamicallyInvokable]
		public EndOfStreamException() : base(Environment.GetResourceString("Arg_EndOfStreamException"))
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0004BAEE File Offset: 0x00049CEE
		[__DynamicallyInvokable]
		public EndOfStreamException(string message) : base(message)
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0004BB02 File Offset: 0x00049D02
		[__DynamicallyInvokable]
		public EndOfStreamException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0004BB17 File Offset: 0x00049D17
		protected EndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
