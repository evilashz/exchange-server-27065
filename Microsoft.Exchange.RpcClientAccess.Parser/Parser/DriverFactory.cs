using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000099 RID: 153
	internal sealed class DriverFactory : IDriverFactory
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000D598 File Offset: 0x0000B798
		public IRopDriver CreateIRopDriver(IConnectionHandler connectionHandler, IConnectionInformation connectionInformation)
		{
			return new RopDriver(connectionHandler, connectionInformation);
		}
	}
}
