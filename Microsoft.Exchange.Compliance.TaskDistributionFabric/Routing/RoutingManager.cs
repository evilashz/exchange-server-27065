using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x0200001A RID: 26
	internal class RoutingManager : IRoutingManager
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000040C2 File Offset: 0x000022C2
		public void SendMessage(ComplianceMessage message)
		{
			RoutingCache.Instance.SendMessage(message);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000040D0 File Offset: 0x000022D0
		public bool ReceiveMessage(ComplianceMessage message)
		{
			MessageLogger.Instance.LogMessageReceived(message);
			bool result = true;
			RoutingCache.Instance.ReceiveMessage(message, out result);
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000040F9 File Offset: 0x000022F9
		public void ReturnMessage(ComplianceMessage message)
		{
			RoutingCache.Instance.ReturnMessage(message);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004107 File Offset: 0x00002307
		public void ProcessedMessage(ComplianceMessage message)
		{
			MessageLogger.Instance.LogMessageProcessed(message);
			RoutingCache.Instance.ProcessedMessage(message);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004120 File Offset: 0x00002320
		public void DispatchedMessage(ComplianceMessage message)
		{
			MessageLogger.Instance.LogMessageDispatched(message);
			RoutingCache.Instance.DispatchedMessage(message);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004138 File Offset: 0x00002338
		public void RecordResult(ComplianceMessage message, Func<ResultBase, ResultBase> commitFunction)
		{
			RoutingCache.Instance.RecordResult(message, commitFunction);
		}
	}
}
