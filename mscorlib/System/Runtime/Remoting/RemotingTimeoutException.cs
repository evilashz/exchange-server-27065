using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x0200079C RID: 1948
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		// Token: 0x060054FE RID: 21758 RVA: 0x0012CB11 File Offset: 0x0012AD11
		public RemotingTimeoutException() : base(RemotingTimeoutException._nullMessage)
		{
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0012CB1E File Offset: 0x0012AD1E
		public RemotingTimeoutException(string message) : base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0012CB32 File Offset: 0x0012AD32
		public RemotingTimeoutException(string message, Exception InnerException) : base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0012CB47 File Offset: 0x0012AD47
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040026D5 RID: 9941
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
