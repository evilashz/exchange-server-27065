using System;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000048 RID: 72
	internal interface IDataConnection
	{
		// Token: 0x0600023E RID: 574
		int OnDataReceived(byte[] buffer, int offset, int size);
	}
}
