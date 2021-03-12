using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Scheduler.Processing;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A0 RID: 416
	internal class SchedulerAdapterComponent : ITransportComponent
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x000493C1 File Offset: 0x000475C1
		public SchedulerAdapterComponent(IMessageDepotComponent messageDepotComponent, IProcessingSchedulerComponent processingSchedulerComponent)
		{
			ArgumentValidator.ThrowIfNull("messageDepotComponent", messageDepotComponent);
			ArgumentValidator.ThrowIfNull("processingSchedulerComponent", processingSchedulerComponent);
			this.messageDepotComponent = messageDepotComponent;
			this.processingSchedulerComponent = processingSchedulerComponent;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000493F0 File Offset: 0x000475F0
		private void HandleActiveMessage(MessageEventArgs args)
		{
			if (args.ItemWrapper.State == MessageDepotItemState.Ready)
			{
				IEnumerable<IMessageScope> messageScopes = this.GetMessageScopes(args.ItemWrapper.Item.MessageEnvelope);
				SchedulableMailItem message = new SchedulableMailItem(args.ItemWrapper.Item.Id, args.ItemWrapper.Item.MessageEnvelope, messageScopes, args.ItemWrapper.Item.ArrivalTime);
				this.scheduler.Submit(message);
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00049464 File Offset: 0x00047664
		private IEnumerable<IMessageScope> GetMessageScopes(MessageEnvelope messageEnvelope)
		{
			return new List<IMessageScope>
			{
				new PriorityScope(messageEnvelope.DeliveryPriority),
				new TenantScope(messageEnvelope.ExternalOrganizationId)
			};
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004949A File Offset: 0x0004769A
		public void Load()
		{
			if (this.messageDepotComponent.Enabled)
			{
				this.scheduler = this.processingSchedulerComponent.ProcessingScheduler;
				this.messageDepotComponent.MessageDepot.SubscribeToActivatedEvent(MessageDepotItemStage.Submission, new MessageActivatedEventHandler(this.HandleActiveMessage));
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000494D7 File Offset: 0x000476D7
		public void Unload()
		{
			if (this.messageDepotComponent.Enabled)
			{
				this.messageDepotComponent.MessageDepot.UnsubscribeFromActivatedEvent(MessageDepotItemStage.Submission, new MessageActivatedEventHandler(this.HandleActiveMessage));
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00049503 File Offset: 0x00047703
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x04000982 RID: 2434
		private readonly IProcessingSchedulerComponent processingSchedulerComponent;

		// Token: 0x04000983 RID: 2435
		private readonly IMessageDepotComponent messageDepotComponent;

		// Token: 0x04000984 RID: 2436
		private IProcessingScheduler scheduler;
	}
}
