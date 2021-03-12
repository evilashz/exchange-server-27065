using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICrossServerDiagnostics
	{
		// Token: 0x06000058 RID: 88
		void BlockCrossServerCall(ExRpcConnectionInfo connectionInfo);

		// Token: 0x06000059 RID: 89
		void BlockCrossServerCall(ExRpcConnectionInfo connectionInfo, string mailboxDescription);

		// Token: 0x0600005A RID: 90
		void BlockMonitoringCrossServerCall(ExRpcConnectionInfo connectionInfo);

		// Token: 0x0600005B RID: 91
		void LogInfoWatson(ExRpcConnectionInfo connectionInfo);

		// Token: 0x0600005C RID: 92
		void TraceCrossServerCall(string serverDn);
	}
}
