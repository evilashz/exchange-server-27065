using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x0200079B RID: 1947
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		// Token: 0x060054F9 RID: 21753 RVA: 0x0012CAB5 File Offset: 0x0012ACB5
		public ServerException() : base(ServerException._nullMessage)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0012CACD File Offset: 0x0012ACCD
		public ServerException(string message) : base(message)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0012CAE1 File Offset: 0x0012ACE1
		public ServerException(string message, Exception InnerException) : base(message, InnerException)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0012CAF6 File Offset: 0x0012ACF6
		internal ServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040026D4 RID: 9940
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
