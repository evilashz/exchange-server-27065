using System;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000006 RID: 6
	public class RpcHttpConnectionRegistrationInternalException : RpcHttpConnectionRegistrationException
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000229A File Offset: 0x0000049A
		public RpcHttpConnectionRegistrationInternalException()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A2 File Offset: 0x000004A2
		public RpcHttpConnectionRegistrationInternalException(string message) : base(message)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022AB File Offset: 0x000004AB
		public RpcHttpConnectionRegistrationInternalException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
