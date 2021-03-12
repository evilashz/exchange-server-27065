using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Dataflow
{
	// Token: 0x02000011 RID: 17
	internal abstract class MessageProcessorBase
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003280 File Offset: 0x00001480
		public virtual Task<ComplianceMessage> ProcessMessage(ComplianceMessage message)
		{
			Task<ComplianceMessage> result;
			try
			{
				MessageLogger.Instance.LogMessageProcessing(message);
				result = this.ProcessMessageInternal(message);
			}
			finally
			{
				MessageLogger.Instance.LogMessageProcessed(message);
			}
			return result;
		}

		// Token: 0x0600004B RID: 75
		protected abstract Task<ComplianceMessage> ProcessMessageInternal(ComplianceMessage message);
	}
}
