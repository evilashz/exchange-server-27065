using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200001B RID: 27
	internal sealed class DsnSchedulerComponent : ITransportComponent
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000036EC File Offset: 0x000018EC
		public void SetLoadTimeDependencies(IMessageDepotComponent msgDepotComponent, IDsnGeneratorComponent dsnGeneratorComponent, IOrarGeneratorComponent orarGeneratorComponent, IMessageTrackingLog msgTrackingLog, ITransportConfiguration transportConfiguration)
		{
			ArgumentValidator.ThrowIfNull("msgDepotComponent", msgDepotComponent);
			ArgumentValidator.ThrowIfNull("dsnGeneratorComponent", dsnGeneratorComponent);
			ArgumentValidator.ThrowIfNull("orarGeneratorComponent", orarGeneratorComponent);
			ArgumentValidator.ThrowIfNull("msgTrackingLog", msgTrackingLog);
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			this.messageDepotComponent = msgDepotComponent;
			this.dsnGenerator = dsnGeneratorComponent;
			this.orarGenerator = orarGeneratorComponent;
			this.msgTrackingLog = msgTrackingLog;
			this.transportConfiguration = transportConfiguration;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003758 File Offset: 0x00001958
		public void Load()
		{
			if (!this.messageDepotComponent.Enabled)
			{
				return;
			}
			IMessageDepot messageDepot = this.messageDepotComponent.MessageDepot;
			messageDepot.SubscribeToDelayedEvent(MessageDepotItemStage.Submission, new MessageEventHandler(this.AddToDelayDsnQueue));
			messageDepot.SubscribeToExpiredEvent(MessageDepotItemStage.Submission, new MessageEventHandler(this.AddToExpiredNdrQueue));
			messageDepot.SubscribeToRemovedEvent(MessageDepotItemStage.Submission, new MessageRemovedEventHandler(this.AddToDeletedNdrQueue));
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.TimedUpdate), null, DsnSchedulerComponent.RefreshTimeInterval);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000037D8 File Offset: 0x000019D8
		public void Unload()
		{
			if (!this.messageDepotComponent.Enabled)
			{
				return;
			}
			IMessageDepot messageDepot = this.messageDepotComponent.MessageDepot;
			messageDepot.UnsubscribeFromDelayedEvent(MessageDepotItemStage.Submission, new MessageEventHandler(this.AddToDelayDsnQueue));
			messageDepot.UnsubscribeFromExpiredEvent(MessageDepotItemStage.Submission, new MessageEventHandler(this.AddToExpiredNdrQueue));
			messageDepot.UnsubscribeFromRemovedEvent(MessageDepotItemStage.Submission, new MessageRemovedEventHandler(this.AddToDeletedNdrQueue));
			this.refreshTimer.Dispose(false);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003844 File Offset: 0x00001A44
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003848 File Offset: 0x00001A48
		private static void AddPendingWork(MessageEventArgs args, ConcurrentQueue<IMessageDepotItem> queue)
		{
			ArgumentValidator.ThrowIfNull("args", args);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper", args.ItemWrapper);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper.Item", args.ItemWrapper.Item);
			queue.Enqueue(args.ItemWrapper.Item);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003898 File Offset: 0x00001A98
		private void TimedUpdate(object state)
		{
			if (!this.messageDepotComponent.Enabled)
			{
				return;
			}
			IMessageDepotItem msgDepotItem;
			while (this.itemsForDelayDsn.TryDequeue(out msgDepotItem))
			{
				this.GenerateDelayDsn(msgDepotItem);
			}
			while (this.itemsForExpiredNdr.TryDequeue(out msgDepotItem))
			{
				this.GenerateExpiredNdr(msgDepotItem);
			}
			MessageRemovedEventArgs messageRemovedEventArgs;
			while (this.itemsForDeletedNdr.TryDequeue(out messageRemovedEventArgs))
			{
				this.GenerateDeletedDsn(messageRemovedEventArgs);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000038F9 File Offset: 0x00001AF9
		private void AddToDelayDsnQueue(MessageEventArgs args)
		{
			DsnSchedulerComponent.AddPendingWork(args, this.itemsForDelayDsn);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003907 File Offset: 0x00001B07
		private void AddToExpiredNdrQueue(MessageEventArgs args)
		{
			DsnSchedulerComponent.AddPendingWork(args, this.itemsForExpiredNdr);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003915 File Offset: 0x00001B15
		private void AddToDeletedNdrQueue(MessageRemovedEventArgs args)
		{
			if (args.Reason == MessageRemovalReason.Deleted)
			{
				this.itemsForDeletedNdr.Enqueue(args);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000392C File Offset: 0x00001B2C
		private void GenerateDeletedDsn(MessageRemovedEventArgs messageRemovedEventArgs)
		{
			IMessageDepotItem item = messageRemovedEventArgs.ItemWrapper.Item;
			TransportMailItem transportMailItem = (TransportMailItem)item.MessageObject;
			DsnSchedulerComponent.ValidateRecipientCache(transportMailItem);
			if (messageRemovedEventArgs.GenerateNdr)
			{
				foreach (MailRecipient mailRecipient in transportMailItem.Recipients.AllUnprocessed)
				{
					mailRecipient.DsnNeeded = DsnFlags.Failure;
				}
				this.dsnGenerator.GenerateDSNs(transportMailItem, transportMailItem.Recipients);
			}
			MessageTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.ADMIN, transportMailItem, transportMailItem.Recipients, null);
			transportMailItem.Ack(AckStatus.Fail, AckReason.MessageDeletedByAdmin, transportMailItem.Recipients, null);
			transportMailItem.ReleaseFromActive();
			transportMailItem.CommitLazy();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000039E4 File Offset: 0x00001BE4
		private void GenerateDelayDsn(IMessageDepotItem msgDepotItem)
		{
			TransportMailItem transportMailItem = (TransportMailItem)msgDepotItem.MessageObject;
			DsnSchedulerComponent.ValidateRecipientCache(transportMailItem);
			bool flag = false;
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				if (mailRecipient.IsDelayDsnNeeded)
				{
					flag = true;
					mailRecipient.DsnNeeded = DsnFlags.Delay;
					mailRecipient.SmtpResponse = SmtpResponse.MessageDelayed;
				}
			}
			if (flag)
			{
				transportMailItem.CommitLazy();
				this.dsnGenerator.GenerateDSNs(transportMailItem, transportMailItem.Recipients);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A78 File Offset: 0x00001C78
		private static void ValidateRecipientCache(TransportMailItem mailItem)
		{
			if (mailItem.ADRecipientCache == null)
			{
				ADOperationResult adoperationResult = MultiTenantTransport.TryCreateADRecipientCache(mailItem);
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError(string.Format("Error {0} when creating recipient cache for message {1}. Falling back to first org", adoperationResult.Exception, MultiTenantTransport.ToString(mailItem)), new object[0]);
					MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(mailItem, OrganizationId.ForestWideOrgId);
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AC8 File Offset: 0x00001CC8
		private void GenerateExpiredNdr(IMessageDepotItem msgDepotItem)
		{
			TransportMailItem transportMailItem = (TransportMailItem)msgDepotItem.MessageObject;
			IMessageDepot messageDepot = this.messageDepotComponent.MessageDepot;
			AcquireResult acquireResult;
			if (!messageDepot.TryAcquire(msgDepotItem.Id, out acquireResult))
			{
				return;
			}
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients.AllUnprocessed)
			{
				mailRecipient.Ack(AckStatus.Fail, AckReason.MessageExpired);
			}
			this.orarGenerator.GenerateOrarMessage(transportMailItem, true);
			this.dsnGenerator.GenerateDSNs(transportMailItem);
			LatencyFormatter latencyFormatter = new LatencyFormatter(transportMailItem, this.transportConfiguration.LocalServer.TransportServer.Fqdn, true);
			this.msgTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.QUEUE, "Queue=Submission", transportMailItem, transportMailItem.Recipients, null, SmtpResponse.Empty, latencyFormatter);
			transportMailItem.ReleaseFromActiveMaterializedLazy();
			messageDepot.Release(msgDepotItem.Id, acquireResult.Token);
		}

		// Token: 0x04000040 RID: 64
		private static readonly TimeSpan RefreshTimeInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000041 RID: 65
		private readonly ConcurrentQueue<IMessageDepotItem> itemsForDelayDsn = new ConcurrentQueue<IMessageDepotItem>();

		// Token: 0x04000042 RID: 66
		private readonly ConcurrentQueue<IMessageDepotItem> itemsForExpiredNdr = new ConcurrentQueue<IMessageDepotItem>();

		// Token: 0x04000043 RID: 67
		private readonly ConcurrentQueue<MessageRemovedEventArgs> itemsForDeletedNdr = new ConcurrentQueue<MessageRemovedEventArgs>();

		// Token: 0x04000044 RID: 68
		private IMessageTrackingLog msgTrackingLog;

		// Token: 0x04000045 RID: 69
		private IDsnGeneratorComponent dsnGenerator;

		// Token: 0x04000046 RID: 70
		private IOrarGeneratorComponent orarGenerator;

		// Token: 0x04000047 RID: 71
		private IMessageDepotComponent messageDepotComponent;

		// Token: 0x04000048 RID: 72
		private ITransportConfiguration transportConfiguration;

		// Token: 0x04000049 RID: 73
		private GuardedTimer refreshTimer;
	}
}
