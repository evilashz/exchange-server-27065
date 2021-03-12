using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Scheduler.Processing;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001AE RID: 430
	internal class CategorizerAdapterComponent : ITransportComponent, IMessageProcessor
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0004E810 File Offset: 0x0004CA10
		public CategorizerAdapterComponent(CategorizerComponent categorizerComponent, IMessageDepotComponent messageDepotComponent)
		{
			this.categorizerComponent = categorizerComponent;
			this.messageDepotComponent = messageDepotComponent;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004E826 File Offset: 0x0004CA26
		public void Load()
		{
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004E828 File Offset: 0x0004CA28
		public void Unload()
		{
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004E82A File Offset: 0x0004CA2A
		public string OnUnhandledException(Exception e)
		{
			return string.Empty;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004E864 File Offset: 0x0004CA64
		public void Process(ISchedulableMessage message)
		{
			AcquireResult acquireResult;
			if (this.messageDepotComponent.MessageDepot.TryAcquire(message.Id, out acquireResult))
			{
				MessageEnvelope messageEnvelope = acquireResult.ItemWrapper.Item.MessageEnvelope;
				TransportMailItem mailItem = TransportMailItem.FromMessageEnvelope(messageEnvelope, LatencyComponent.CategorizerOnSubmittedMessage);
				ThrottlingContext throttlingContext;
				StandaloneJob standaloneJob = (StandaloneJob)CategorizerJobsUtil.SetupNewJob(mailItem, this.categorizerComponent.Stages, (QueuedRecipientsByAgeToken recipientByAgeToken, ThrottlingContext context, IList<StageInfo> stages) => StandaloneJob.NewJob(mailItem, acquireResult.Token, context, recipientByAgeToken, stages), out throttlingContext);
				if (standaloneJob != null)
				{
					standaloneJob.RunToCompletion();
					CategorizerJobsUtil.DoneProcessing(standaloneJob.GetQueuedRecipientsByAgeToken());
				}
				if (!standaloneJob.RootMailItemDeferred)
				{
					this.messageDepotComponent.MessageDepot.Release(message.Id, acquireResult.Token);
				}
			}
		}

		// Token: 0x04000A1D RID: 2589
		private readonly CategorizerComponent categorizerComponent;

		// Token: 0x04000A1E RID: 2590
		private readonly IMessageDepotComponent messageDepotComponent;
	}
}
