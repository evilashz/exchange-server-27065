using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.MailboxTransport.StoreDriver
{
	// Token: 0x02000007 RID: 7
	internal class MemorySubmissionItem : SubmissionItem
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000379D File Offset: 0x0000199D
		public MemorySubmissionItem(MessageItem item, OrganizationId organizationId) : base("Microsoft SMTP Server")
		{
			base.Item = item;
			this.organizationId = organizationId;
			this.submissionTime = DateTime.UtcNow;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000037C3 File Offset: 0x000019C3
		public override string SourceServerFqdn
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.Fqdn;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000037D9 File Offset: 0x000019D9
		public override IPAddress SourceServerNetworkAddress
		{
			get
			{
				return StoreDriver.LocalIPAddress;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000037E0 File Offset: 0x000019E0
		public override DateTime OriginalCreateTime
		{
			get
			{
				return this.submissionTime;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000037E8 File Offset: 0x000019E8
		public override bool HasMessageItem
		{
			get
			{
				return base.Item != null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000037F6 File Offset: 0x000019F6
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000037FD File Offset: 0x000019FD
		public override TenantPartitionHint TenantPartitionHint
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003804 File Offset: 0x00001A04
		public void Submit(MessageTrackingSource messageTrackingSource, MemorySubmissionItem.OnConvertedToTransportMailItemDelegate transportMailItemHandler, RoutedMailItem relatedMailItem)
		{
			TransportMailItem transportMailItem;
			if (relatedMailItem == null)
			{
				transportMailItem = TransportMailItem.NewMailItem(this.organizationId, LatencyComponent.StoreDriverSubmit, MailDirectionality.Undefined, default(Guid));
			}
			else
			{
				transportMailItem = TransportMailItem.NewSideEffectMailItem(relatedMailItem, this.organizationId, LatencyComponent.StoreDriverSubmit, MailDirectionality.Undefined, default(Guid));
			}
			base.CopyContentTo(transportMailItem);
			base.DecorateMessage(transportMailItem);
			base.ApplySecurityAttributesTo(transportMailItem);
			if (relatedMailItem != null)
			{
				transportMailItem.PrioritizationReason = relatedMailItem.PrioritizationReason;
				transportMailItem.Priority = relatedMailItem.Priority;
			}
			MailItemSubmitter.CopySenderTo(this, transportMailItem);
			List<string> invalidRecipients = null;
			List<string> list = null;
			MailItemSubmitter.CopyRecipientsTo(this, transportMailItem, null, ref invalidRecipients, ref list);
			ClassificationUtils.PromoteStoreClassifications(transportMailItem.RootPart.Headers);
			MailItemSubmitter.PatchQuarantineSender(transportMailItem, base.QuarantineOriginalSender);
			bool flag = transportMailItem.Recipients.Count > 0;
			if (relatedMailItem != null)
			{
				MimeInternalHelpers.CopyHeaderBetweenList(relatedMailItem.RootPart.Headers, transportMailItem.RootPart.Headers, "X-MS-Exchange-Moderation-Loop");
			}
			bool flag2 = transportMailItemHandler(transportMailItem, flag);
			if (flag && flag2)
			{
				transportMailItem.CommitLazy();
				MsgTrackReceiveInfo msgTrackInfo = new MsgTrackReceiveInfo(StoreDriver.LocalIPAddress, (relatedMailItem != null) ? new long?(relatedMailItem.RecordId) : null, transportMailItem.MessageTrackingSecurityInfo, invalidRecipients);
				MessageTrackingLog.TrackReceive(messageTrackingSource, transportMailItem, msgTrackInfo);
				Components.CategorizerComponent.EnqueueSubmittedMessage(transportMailItem);
			}
		}

		// Token: 0x04000026 RID: 38
		private OrganizationId organizationId;

		// Token: 0x04000027 RID: 39
		private DateTime submissionTime;

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x06000056 RID: 86
		public delegate bool OnConvertedToTransportMailItemDelegate(TransportMailItem mailItem, bool isValid);
	}
}
