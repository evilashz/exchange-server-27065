using System;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000005 RID: 5
	public class RpcHttpConnectionRegistrationException : Exception
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000225E File Offset: 0x0000045E
		public RpcHttpConnectionRegistrationException()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002266 File Offset: 0x00000466
		public RpcHttpConnectionRegistrationException(string message) : base(message)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000226F File Offset: 0x0000046F
		public RpcHttpConnectionRegistrationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002279 File Offset: 0x00000479
		public RpcHttpConnectionRegistrationException(string message, int errorCode) : base(message)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002289 File Offset: 0x00000489
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002291 File Offset: 0x00000491
		public int ErrorCode { get; set; }
	}
}
