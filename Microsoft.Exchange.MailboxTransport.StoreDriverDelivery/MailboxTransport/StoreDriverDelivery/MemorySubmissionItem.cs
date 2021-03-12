using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.Shared.SubmissionItem;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200000B RID: 11
	internal class MemorySubmissionItem : SubmissionItemBase
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00007381 File Offset: 0x00005581
		public MemorySubmissionItem(MessageItem item, OrganizationId organizationId) : base("Microsoft SMTP Server")
		{
			base.Item = item;
			this.organizationId = organizationId;
			this.submissionTime = DateTime.UtcNow;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000073A7 File Offset: 0x000055A7
		public override string SourceServerFqdn
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.Fqdn;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000073BD File Offset: 0x000055BD
		public override IPAddress SourceServerNetworkAddress
		{
			get
			{
				return StoreDriverDelivery.LocalIPAddress;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000073C4 File Offset: 0x000055C4
		public override DateTime OriginalCreateTime
		{
			get
			{
				return this.submissionTime;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000073CC File Offset: 0x000055CC
		public override bool HasMessageItem
		{
			get
			{
				return base.Item != null;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000073DC File Offset: 0x000055DC
		public void Submit(MessageTrackingSource messageTrackingSource, MemorySubmissionItem.OnConvertedToTransportMailItemDelegate transportMailItemHandler, MbxTransportMailItem relatedMailItem)
		{
			TransportMailItem transportMailItem;
			if (relatedMailItem == null)
			{
				transportMailItem = TransportMailItem.NewMailItem(this.organizationId, LatencyComponent.StoreDriverSubmit, MailDirectionality.Originating, default(Guid));
			}
			else
			{
				transportMailItem = TransportMailItem.NewSideEffectMailItem(relatedMailItem, this.organizationId, LatencyComponent.StoreDriverSubmit, MailDirectionality.Originating, relatedMailItem.ExternalOrganizationId);
			}
			base.CopyContentTo(transportMailItem);
			base.DecorateMessage(transportMailItem);
			base.ApplySecurityAttributesTo(transportMailItem);
			if (relatedMailItem != null)
			{
				transportMailItem.PrioritizationReason = relatedMailItem.PrioritizationReason;
				transportMailItem.Priority = relatedMailItem.Priority;
			}
			SubmissionItemUtils.CopySenderTo(this, transportMailItem);
			List<string> invalidRecipients = null;
			List<string> list = null;
			SubmissionItemUtils.CopyRecipientsTo(this, transportMailItem, null, ref invalidRecipients, ref list);
			ClassificationUtils.PromoteStoreClassifications(transportMailItem.RootPart.Headers);
			SubmissionItemUtils.PatchQuarantineSender(transportMailItem, base.QuarantineOriginalSender);
			bool flag = transportMailItem.Recipients.Count > 0;
			if (relatedMailItem != null)
			{
				MimeInternalHelpers.CopyHeaderBetweenList(relatedMailItem.RootPart.Headers, transportMailItem.RootPart.Headers, "X-MS-Exchange-Moderation-Loop");
			}
			bool flag2 = transportMailItemHandler(transportMailItem, flag);
			if (flag && flag2)
			{
				MsgTrackReceiveInfo msgTrackInfo = new MsgTrackReceiveInfo(StoreDriverDelivery.LocalIPAddress, (relatedMailItem != null) ? new long?(relatedMailItem.RecordId) : null, transportMailItem.MessageTrackingSecurityInfo, invalidRecipients);
				MessageTrackingLog.TrackReceive(messageTrackingSource, transportMailItem, msgTrackInfo);
				Utils.SubmitMailItem(transportMailItem, false);
			}
		}

		// Token: 0x0400007D RID: 125
		private readonly OrganizationId organizationId;

		// Token: 0x0400007E RID: 126
		private readonly DateTime submissionTime;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000136 RID: 310
		public delegate bool OnConvertedToTransportMailItemDelegate(TransportMailItem mailItem, bool isValid);
	}
}
