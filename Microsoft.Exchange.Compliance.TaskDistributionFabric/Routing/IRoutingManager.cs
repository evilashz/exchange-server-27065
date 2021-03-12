using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x02000019 RID: 25
	internal interface IRoutingManager
	{
		// Token: 0x06000061 RID: 97
		bool ReceiveMessage(ComplianceMessage message);

		// Token: 0x06000062 RID: 98
		void SendMessage(ComplianceMessage message);

		// Token: 0x06000063 RID: 99
		void ReturnMessage(ComplianceMessage message);

		// Token: 0x06000064 RID: 100
		void ProcessedMessage(ComplianceMessage message);

		// Token: 0x06000065 RID: 101
		void DispatchedMessage(ComplianceMessage message);

		// Token: 0x06000066 RID: 102
		void RecordResult(ComplianceMessage message, Func<ResultBase, ResultBase> commitFunction);
	}
}
