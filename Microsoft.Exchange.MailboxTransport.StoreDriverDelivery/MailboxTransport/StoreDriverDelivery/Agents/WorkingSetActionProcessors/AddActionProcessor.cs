using System;
using Microsoft.Exchange.Data.Storage.WorkingSet.SignalApiEx;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.WorkingSetActionProcessors
{
	// Token: 0x020000D2 RID: 210
	internal class AddActionProcessor : AbstractActionProcessor
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0002398C File Offset: 0x00021B8C
		public AddActionProcessor(ActionProcessorFactory actionProcessorFactory) : base(actionProcessorFactory)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00023998 File Offset: 0x00021B98
		public override void Process(StoreDriverDeliveryEventArgsImpl argsImpl, Action action, int traceId)
		{
			AbstractActionProcessor.Tracer.TraceDebug((long)traceId, "WorkingSetAgent.AddActionProcessor.Process: entering");
			if (action.Type == "DeletePartition")
			{
				this.actionProcessorFactory.GetActionProcessor(ActionProcessorType.DeletePartitionProcesor).Process(argsImpl, action, traceId);
				return;
			}
			if (action.Item.GetType() == typeof(ExchangeItem))
			{
				this.actionProcessorFactory.GetActionProcessor(ActionProcessorType.AddExchangeItemProcessor).Process(argsImpl, action, traceId);
				return;
			}
			WorkingSet.ItemsNotSupported.Increment();
			throw new NotSupportedException(string.Format("Item type not supported - {0} - {1}", action.Item.Identifier, action.Item.GetType()));
		}
	}
}
