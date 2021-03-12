using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x0200079A RID: 1946
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		// Token: 0x060054F4 RID: 21748 RVA: 0x0012CA59 File Offset: 0x0012AC59
		public RemotingException() : base(RemotingException._nullMessage)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0012CA71 File Offset: 0x0012AC71
		public RemotingException(string message) : base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0012CA85 File Offset: 0x0012AC85
		public RemotingException(string message, Exception InnerException) : base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0012CA9A File Offset: 0x0012AC9A
		protected RemotingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040026D3 RID: 9939
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
