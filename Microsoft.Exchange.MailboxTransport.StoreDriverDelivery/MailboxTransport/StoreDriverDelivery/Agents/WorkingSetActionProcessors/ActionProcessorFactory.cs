using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.WorkingSetActionProcessors
{
	// Token: 0x020000D0 RID: 208
	internal class ActionProcessorFactory
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0002390C File Offset: 0x00021B0C
		internal ActionProcessorFactory(WorkingSetAgent workingSetAgent)
		{
			this.actionsProcessors = new Dictionary<ActionProcessorType, IActionProcessor>
			{
				{
					ActionProcessorType.AddActionProcessor,
					new AddActionProcessor(this)
				},
				{
					ActionProcessorType.AddExchangeItemProcessor,
					new AddExchangeItemProcessor(this)
				},
				{
					ActionProcessorType.DeletePartitionProcesor,
					new DeletePartitionProcessor(this)
				}
			};
			this.WorkingSetAgent = workingSetAgent;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002395A File Offset: 0x00021B5A
		internal IActionProcessor GetActionProcessor(ActionProcessorType actionProcessorType)
		{
			if (this.actionsProcessors.ContainsKey(actionProcessorType))
			{
				return this.actionsProcessors[actionProcessorType];
			}
			throw new NotSupportedException(string.Format("Action processor type not supported - {0}", actionProcessorType));
		}

		// Token: 0x04000379 RID: 889
		private readonly IDictionary<ActionProcessorType, IActionProcessor> actionsProcessors;

		// Token: 0x0400037A RID: 890
		internal readonly WorkingSetAgent WorkingSetAgent;
	}
}
