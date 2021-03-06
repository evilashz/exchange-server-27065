using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportMailSubmitter : MailSubmitter
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00007BC7 File Offset: 0x00005DC7
		private TransportMailSubmitter()
		{
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007BCF File Offset: 0x00005DCF
		public static TransportMailSubmitter Instance
		{
			get
			{
				return TransportMailSubmitter.instance;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public override TransportMailItem CreateNewMail()
		{
			return TransportMailItem.NewMailItem(LatencyComponent.ContentAggregation);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007BEE File Offset: 0x00005DEE
		public override Stream GetWriteStream(TransportMailItem mailItem)
		{
			return mailItem.OpenMimeWriteStream(MimeLimits.Default);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007BFC File Offset: 0x00005DFC
		public override Exception SubmitMail(string componentId, SyncLogSession syncLogSession, TransportMailItem mailItem, ISyncEmail syncEmail, IList<string> recipients, string deliveryFolder, Guid subscriptionGuid, string cloudId, string cloudVersion, DateTime originalReceivedTime, MsgTrackReceiveInfo msgTrackInfo)
		{
			if (msgTrackInfo == null)
			{
				throw new ArgumentNullException("msgTrackInfo");
			}
			HeaderFirewall.Filter(mailItem.RootPart.Headers, TransportMailSubmitter.restrictedHeaderSet);
			mailItem.PerfCounterAttribution = AggregationComponent.Name;
			mailItem.AuthMethod = MultilevelAuthMechanism.None;
			mailItem.ReceiveConnectorName = AggregationComponent.Name;
			MultilevelAuth.EnsureSecurityAttributes(mailItem, SubmitAuthCategory.Anonymous, MultilevelAuthMechanism.None, null);
			mailItem.RootPart.Headers.RemoveAll("X-Auto-Response-Suppress");
			mailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-Auto-Response-Suppress", TransportMailSubmitter.AutoResponseSuppressAll));
			mailItem.From = RoutingAddress.NullReversePath;
			DateHeader dateHeader = new DateHeader("Date", mailItem.DateReceived);
			ReceivedHeader newChild = new ReceivedHeader(componentId, null, ComputerInformation.DnsFullyQualifiedDomainName, null, null, "Microsoft Exchange Server", null, null, dateHeader.Value);
			mailItem.RootPart.Headers.PrependChild(newChild);
			Header header = mailItem.RootPart.Headers.FindFirst(HeaderId.MessageId);
			Exception ex = null;
			if (header == null)
			{
				Header header2 = Header.Create(HeaderId.MessageId);
				string value;
				if (!TransportMailSubmitter.TryGenerateMessageId(subscriptionGuid, deliveryFolder, cloudId, out value, out ex))
				{
					syncLogSession.LogDebugging((TSLID)1112UL, "Failed to generate message id {0}", new object[]
					{
						ex
					});
					return ex;
				}
				header2.Value = value;
				mailItem.RootPart.Headers.AppendChild(header2);
			}
			if (!string.IsNullOrEmpty(deliveryFolder))
			{
				mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-DeliveryFolder", deliveryFolder));
			}
			mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-Sharing-Instance-Guid", subscriptionGuid.ToString()));
			if (!string.IsNullOrEmpty(cloudId))
			{
				mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-Cloud-Id", cloudId));
			}
			if (!string.IsNullOrEmpty(cloudVersion))
			{
				mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-Cloud-Version", cloudVersion));
			}
			mailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", Util.FormatOrganizationalMessageArrivalTime(mailItem.DateReceived)));
			mailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Original-Received-Time", Util.FormatOrganizationalMessageArrivalTime(originalReceivedTime)));
			mailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Dsn-Version", "12"));
			if (syncEmail.IsRead != null && syncEmail.IsRead.Value)
			{
				mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-DeliverAsRead", string.Empty));
			}
			if (syncEmail.SyncMessageResponseType != null)
			{
				switch (syncEmail.SyncMessageResponseType.Value)
				{
				case SyncMessageResponseType.Replied:
					mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-MailReplied", string.Empty));
					break;
				case SyncMessageResponseType.Forwarded:
					mailItem.RootPart.Headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-MailForwarded", string.Empty));
					break;
				}
			}
			if (syncEmail.Importance != null)
			{
				mailItem.RootPart.Headers.RemoveAll(HeaderId.Importance);
				Header header3 = Header.Create(HeaderId.Importance);
				header3.Value = syncEmail.Importance.Value.ToString();
				mailItem.RootPart.Headers.AppendChild(header3);
			}
			mailItem.Recipients.Clear();
			for (int i = 0; i < recipients.Count; i++)
			{
				mailItem.Recipients.Add(recipients[i]);
			}
			syncLogSession.LogDebugging((TSLID)116UL, "Beginning tracking latency on item", new object[0]);
			LatencyTracker.BeginTrackLatency(LatencyComponent.ContentAggregationMailItemCommit, mailItem.LatencyTracker);
			syncLogSession.LogDebugging((TSLID)117UL, "Beginning commit for receive", new object[0]);
			IAsyncResult asyncResult = mailItem.BeginCommitForReceive(new AsyncCallback(this.OnMailSubmitted), null);
			if (!asyncResult.IsCompleted)
			{
				syncLogSession.LogDebugging((TSLID)228UL, "waiting for commitForReceieve to finish", new object[0]);
				asyncResult.AsyncWaitHandle.WaitOne();
				syncLogSession.LogDebugging((TSLID)235UL, "CommitForReceieve finished", new object[0]);
			}
			mailItem.EndCommitForReceive(asyncResult, out ex);
			syncLogSession.LogDebugging((TSLID)236UL, "EndCommitForReceive invoked and resulted in exception {0}", new object[]
			{
				ex
			});
			LatencyTracker.EndTrackLatency(LatencyComponent.ContentAggregationMailItemCommit, mailItem.LatencyTracker);
			syncLogSession.LogDebugging((TSLID)237UL, "EndTrackLatency invoked", new object[0]);
			if (ex == null)
			{
				syncLogSession.LogDebugging((TSLID)1114UL, "Tracking RECEIVE", new object[0]);
				MessageTrackingLog.TrackReceive(MessageTrackingSource.AGGREGATION, mailItem, msgTrackInfo);
				syncLogSession.LogDebugging((TSLID)239UL, "Tracking latency", new object[0]);
				LatencyTracker.TrackPreProcessLatency(LatencyComponent.ContentAggregation, mailItem.LatencyTracker, mailItem.DateReceived);
				syncLogSession.LogDebugging((TSLID)240UL, "Submitting message to categorizer", new object[0]);
				Components.CategorizerComponent.EnqueueSubmittedMessage(mailItem);
				syncLogSession.LogDebugging((TSLID)324UL, "Done enqueuing.", new object[0]);
				base.TriggerMailSubmittedEvent(this, null);
			}
			return ex;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008148 File Offset: 0x00006348
		internal static bool TryGenerateMessageId(Guid subscriptionGuid, string deliveryFolder, string cloudId, out string messageId, out Exception exception)
		{
			messageId = null;
			exception = null;
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(TransportMailSubmitter.TransportSyncMessageIdentifier);
			stringBuilder.Append(TransportMailSubmitter.MessageIdSeparator);
			stringBuilder.Append(subscriptionGuid.GetHashCode().ToString("X"));
			stringBuilder.Append(TransportMailSubmitter.MessageIdSeparator);
			stringBuilder.Append(string.IsNullOrEmpty(deliveryFolder) ? string.Empty : deliveryFolder.GetHashCode().ToString("X"));
			stringBuilder.Append(TransportMailSubmitter.MessageIdSeparator);
			stringBuilder.Append(cloudId.GetHashCode().ToString("X"));
			string id = stringBuilder.ToString();
			try
			{
				messageId = SyncUtilities.GenerateMessageId(id);
			}
			catch (SocketException innerException)
			{
				exception = new MessageIdGenerationTransientException(innerException);
				return false;
			}
			return true;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008230 File Offset: 0x00006430
		private void OnMailSubmitted(object state)
		{
		}

		// Token: 0x040000E3 RID: 227
		private const int EstimatedGeneratedMessageId = 128;

		// Token: 0x040000E4 RID: 228
		private const string DeliveryFolderHeaderName = "X-MS-Exchange-Organization-DeliveryFolder";

		// Token: 0x040000E5 RID: 229
		private const string AutoResponseSuppressHeaderName = "X-Auto-Response-Suppress";

		// Token: 0x040000E6 RID: 230
		private const string OriginalArrivalTimeHeaderName = "X-MS-Exchange-Organization-OriginalArrivalTime";

		// Token: 0x040000E7 RID: 231
		private const string SharingInstanceGuidHeaderName = "X-MS-Exchange-Organization-Sharing-Instance-Guid";

		// Token: 0x040000E8 RID: 232
		private const string CloudIdHeaderName = "X-MS-Exchange-Organization-Cloud-Id";

		// Token: 0x040000E9 RID: 233
		private const string CloudVersionHeaderName = "X-MS-Exchange-Organization-Cloud-Version";

		// Token: 0x040000EA RID: 234
		private const string ServerId = "Microsoft Exchange Server";

		// Token: 0x040000EB RID: 235
		private const string DsnVersion = "12";

		// Token: 0x040000EC RID: 236
		internal static readonly char MessageIdSeparator = '-';

		// Token: 0x040000ED RID: 237
		internal static readonly string TransportSyncMessageIdentifier = "TransportSync";

		// Token: 0x040000EE RID: 238
		private static readonly string AutoResponseSuppressAll = AutoResponseSuppress.All.ToString();

		// Token: 0x040000EF RID: 239
		private static readonly TransportMailSubmitter instance = new TransportMailSubmitter();

		// Token: 0x040000F0 RID: 240
		private static readonly RestrictedHeaderSet restrictedHeaderSet = RestrictedHeaderSet.Organization | RestrictedHeaderSet.Forest;
	}
}
