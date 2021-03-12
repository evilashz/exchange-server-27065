using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000134 RID: 308
	internal abstract class ResubmitHelper
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x00031014 File Offset: 0x0002F214
		protected ResubmitHelper(ResubmitReason resubmitReason, MessageTrackingSource messageTrackingSource, string sourceContext, NextHopSolutionKey scopingSolutionKey, Trace traceComponent)
		{
			if (traceComponent == null)
			{
				throw new ArgumentNullException("traceComponent");
			}
			this.resubmitReason = resubmitReason;
			this.messageTrackingSource = messageTrackingSource;
			this.sourceContext = (sourceContext ?? string.Empty);
			this.scopingSolutionKey = scopingSolutionKey;
			this.traceComponent = traceComponent;
			this.serverVersion = Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00031083 File Offset: 0x0002F283
		public ResubmitReason ResubmitReason
		{
			get
			{
				return this.resubmitReason;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0003108B File Offset: 0x0002F28B
		protected Trace TraceComponent
		{
			get
			{
				return this.traceComponent;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00031093 File Offset: 0x0002F293
		protected MessageTrackingSource MessageTrackingSource
		{
			get
			{
				return this.messageTrackingSource;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003109B File Offset: 0x0002F29B
		protected string SourceContext
		{
			get
			{
				return this.sourceContext;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000310A4 File Offset: 0x0002F2A4
		protected bool IsScopedToSolution
		{
			get
			{
				return !this.scopingSolutionKey.Equals(NextHopSolutionKey.Empty);
			}
		}

		// Token: 0x06000D91 RID: 3473
		protected abstract string GetComponentNameForReceivedHeader();

		// Token: 0x06000D92 RID: 3474 RVA: 0x000310C8 File Offset: 0x0002F2C8
		protected NextHopSolution GetScopingSolution(TransportMailItem mailItem)
		{
			if (!this.IsScopedToSolution)
			{
				return null;
			}
			NextHopSolution result;
			if (!mailItem.NextHopSolutions.TryGetValue(this.scopingSolutionKey, out result))
			{
				throw new InvalidOperationException("Scoped resubmit did not find a solution for key: " + this.scopingSolutionKey);
			}
			return result;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00031110 File Offset: 0x0002F310
		public void Resubmit(IEnumerable<TransportMailItem> mailItems)
		{
			if (mailItems == null)
			{
				throw new ArgumentNullException("mailItems");
			}
			foreach (TransportMailItem mailItem in mailItems)
			{
				this.Resubmit(mailItem, null);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00031168 File Offset: 0x0002F368
		public virtual void Resubmit(TransportMailItem mailItem, Action<TransportMailItem> processClonedItemDelegate = null)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			TransportMailItem transportMailItem;
			lock (mailItem)
			{
				NextHopSolution scopingSolution = this.GetScopingSolution(mailItem);
				IEnumerable<MailRecipient> recipients;
				int count;
				if (scopingSolution == null)
				{
					recipients = mailItem.Recipients;
					count = mailItem.Recipients.Count;
				}
				else
				{
					recipients = scopingSolution.Recipients;
					count = scopingSolution.Recipients.Count;
				}
				int num = 0;
				int num2 = 0;
				transportMailItem = null;
				List<MailRecipient> list = null;
				foreach (MailRecipient mailRecipient in recipients.ToArray<MailRecipient>())
				{
					if (!this.IsDeleted(mailRecipient))
					{
						ResubmitHelper.RecipientAction recipientAction = this.DetermineRecipientAction(mailRecipient);
						if (recipientAction == ResubmitHelper.RecipientAction.Copy || recipientAction == ResubmitHelper.RecipientAction.Move)
						{
							if (transportMailItem == null)
							{
								transportMailItem = this.CloneWithoutRecipients(mailItem);
								if (processClonedItemDelegate != null)
								{
									processClonedItemDelegate(transportMailItem);
								}
							}
							if (recipientAction == ResubmitHelper.RecipientAction.Copy)
							{
								num++;
								this.ProcessRecipient(mailRecipient.CopyTo(transportMailItem));
							}
							else
							{
								if (list == null)
								{
									list = new List<MailRecipient>(count);
								}
								list.Add(mailRecipient);
							}
						}
					}
				}
				int num3;
				if (list != null)
				{
					foreach (MailRecipient mailRecipient2 in list)
					{
						mailRecipient2.MoveTo(transportMailItem);
						this.ProcessRecipient(mailRecipient2);
					}
					num3 = list.Count;
					list.Clear();
					list = null;
				}
				else
				{
					num3 = 0;
				}
				if (transportMailItem == null)
				{
					this.traceComponent.TraceDebug<long, ResubmitReason>((long)this.GetHashCode(), "Mail Item '{0}' not resubmitted since no recipients were chosen for the clone.  Resubmit Reason is '{1}'", mailItem.RecordId, this.resubmitReason);
				}
				else
				{
					this.traceComponent.TraceDebug((long)this.GetHashCode(), "Mail Item '{0}' resubmitted as mail item '{1}'.  '{2}' recipients were copied, '{3}' recipients were moved, '{4}' recipients were reused and '{5}' recipients were ignored.  Resubmit Reason is '{6}'", new object[]
					{
						mailItem.RecordId,
						transportMailItem.RecordId,
						num,
						num3,
						num2,
						count - (num + num3 + num2),
						this.resubmitReason
					});
					string componentNameForReceivedHeader = this.GetComponentNameForReceivedHeader();
					if (!string.IsNullOrEmpty(componentNameForReceivedHeader))
					{
						DateHeader dateHeader = new DateHeader("Date", DateTime.UtcNow);
						string value = dateHeader.Value;
						string localServerTcpInfo = ResubmitHelper.GetLocalServerTcpInfo(transportMailItem);
						ReceivedHeader newChild = new ReceivedHeader(SmtpReceiveServer.ServerName, localServerTcpInfo, SmtpReceiveServer.ServerName, localServerTcpInfo, null, componentNameForReceivedHeader, this.serverVersion.ToString(), null, value);
						transportMailItem.MimeDocument.RootPart.Headers.PrependChild(newChild);
						SystemProbe.TracePass<string>(transportMailItem, "Transport", "Message resubmitted for component '{0}'", componentNameForReceivedHeader);
					}
					else
					{
						SystemProbe.TracePass<string>(transportMailItem, "Transport", "Message resubmitted for reason '{0}'", this.resubmitReason.ToString());
					}
					transportMailItem.CommitLazy();
					MessageTrackingLog.TrackResubmit(this.messageTrackingSource, transportMailItem, mailItem, this.sourceContext);
				}
			}
			if (transportMailItem != null)
			{
				this.TrackLatency(transportMailItem);
				transportMailItem.RouteForHighAvailability = true;
				Components.CategorizerComponent.EnqueueSubmittedMessage(transportMailItem);
				this.UpdatePerformanceCounters();
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00031480 File Offset: 0x0002F680
		protected virtual bool IsDeleted(MailRecipient recipient)
		{
			return !recipient.IsActive;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003148C File Offset: 0x0002F68C
		protected virtual TransportMailItem CloneWithoutRecipients(TransportMailItem mailItem)
		{
			NextHopSolution scopingSolution = this.GetScopingSolution(mailItem);
			TransportMailItem transportMailItem;
			if (Components.TransportAppConfig.QueueDatabase.CloneInOriginalGeneration)
			{
				transportMailItem = mailItem.CreateNewCopyWithoutRecipients(scopingSolution);
			}
			else
			{
				transportMailItem = mailItem.NewCloneWithoutRecipients(false, null, scopingSolution);
			}
			Resolver.ClearResolverAndTransportSettings(transportMailItem);
			transportMailItem.ShadowServerContext = null;
			transportMailItem.ShadowServerDiscardId = null;
			if (transportMailItem.PrioritizationReason == "ShadowRedundancy")
			{
				transportMailItem.Priority = DeliveryPriority.Normal;
			}
			return transportMailItem;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000314F4 File Offset: 0x0002F6F4
		protected virtual ResubmitHelper.RecipientAction DetermineRecipientAction(MailRecipient recipient)
		{
			return ResubmitHelper.RecipientAction.Copy;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000314F8 File Offset: 0x0002F6F8
		protected virtual void ProcessRecipient(MailRecipient recipient)
		{
			Resolver.ClearResolverProperties(recipient);
			recipient.DeliveryTime = null;
			recipient.DeliveredDestination = null;
			recipient.PrimaryServerFqdnGuid = null;
			recipient.RetryCount = 0;
			recipient.Status = Status.Ready;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00031536 File Offset: 0x0002F736
		protected virtual void TrackLatency(TransportMailItem mailItemToResubmit)
		{
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00031538 File Offset: 0x0002F738
		protected virtual void UpdatePerformanceCounters()
		{
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003153C File Offset: 0x0002F73C
		private static string GetLocalServerTcpInfo(TransportMailItem mailItem)
		{
			ReceivedHeader receivedHeader = mailItem.MimeDocument.RootPart.Headers.FindFirst(HeaderId.Received) as ReceivedHeader;
			if (receivedHeader == null)
			{
				return null;
			}
			return receivedHeader.ByTcpInfo;
		}

		// Token: 0x040005CE RID: 1486
		private readonly Trace traceComponent;

		// Token: 0x040005CF RID: 1487
		private readonly ResubmitReason resubmitReason;

		// Token: 0x040005D0 RID: 1488
		private readonly MessageTrackingSource messageTrackingSource;

		// Token: 0x040005D1 RID: 1489
		private readonly string sourceContext;

		// Token: 0x040005D2 RID: 1490
		private readonly Version serverVersion;

		// Token: 0x040005D3 RID: 1491
		private readonly NextHopSolutionKey scopingSolutionKey;

		// Token: 0x02000135 RID: 309
		protected enum RecipientAction
		{
			// Token: 0x040005D5 RID: 1493
			None,
			// Token: 0x040005D6 RID: 1494
			Copy,
			// Token: 0x040005D7 RID: 1495
			Move
		}
	}
}
