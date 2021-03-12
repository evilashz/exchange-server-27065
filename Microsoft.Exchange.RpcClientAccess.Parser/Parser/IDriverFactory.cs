using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000098 RID: 152
	internal interface IDriverFactory
	{
		// Token: 0x060003A9 RID: 937
		IRopDriver CreateIRopDriver(IConnectionHandler connectionHandler, IConnectionInformation connectionInformation);
	}
}
