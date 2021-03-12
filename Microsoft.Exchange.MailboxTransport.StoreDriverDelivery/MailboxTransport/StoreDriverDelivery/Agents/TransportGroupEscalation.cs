using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TransportGroupEscalation : GroupEscalation
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00014ECB File Offset: 0x000130CB
		public TransportGroupEscalation(MbxTransportMailItem mbxTransportMailItem, IXSOFactory xsoFactory, IGroupEscalationFlightInfo groupEscalationFlightInfo, IMailboxUrls mailboxUrls) : base(xsoFactory, groupEscalationFlightInfo, mailboxUrls)
		{
			this.mbxTransportMailItem = mbxTransportMailItem;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00014EE0 File Offset: 0x000130E0
		protected override void SendEscalateMessage(IMessageItem escalatedMessage)
		{
			escalatedMessage.CommitReplyTo();
			using (MemorySubmissionItem memorySubmissionItem = new MemorySubmissionItem((MessageItem)escalatedMessage, this.mbxTransportMailItem.OrganizationId))
			{
				memorySubmissionItem.Submit(MessageTrackingSource.AGENT, new MemorySubmissionItem.OnConvertedToTransportMailItemDelegate(this.TransportMailItemHandler), this.mbxTransportMailItem);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00014F40 File Offset: 0x00013140
		private bool TransportMailItemHandler(TransportMailItem mailItem, bool isValid)
		{
			return isValid;
		}

		// Token: 0x04000262 RID: 610
		private readonly MbxTransportMailItem mbxTransportMailItem;
	}
}
