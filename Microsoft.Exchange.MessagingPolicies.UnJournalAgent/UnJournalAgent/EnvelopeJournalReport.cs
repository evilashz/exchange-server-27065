using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000018 RID: 24
	internal class EnvelopeJournalReport
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003F55 File Offset: 0x00002155
		public EnvelopeJournalReport(AddressInfo senderAddress, List<AddressInfo> recipientAddresses, string messageIdString, bool defective)
		{
			this.sender = senderAddress;
			this.recipients = recipientAddresses;
			this.messageId = messageIdString;
			this.defective = defective;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003F90 File Offset: 0x00002190
		public AddressInfo Sender
		{
			get
			{
				if (this.validSenderJournalArchiveAddressIsSet)
				{
					return this.SenderJournalArchiveAddress;
				}
				return this.sender;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003FA7 File Offset: 0x000021A7
		public AddressInfo EnvelopeSender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003FAF File Offset: 0x000021AF
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003FB8 File Offset: 0x000021B8
		public AddressInfo SenderJournalArchiveAddress
		{
			get
			{
				return this.journalArchiveAddress;
			}
			set
			{
				this.journalArchiveAddress = value;
				if (this.journalArchiveAddress != null)
				{
					RoutingAddress address = this.journalArchiveAddress.Address;
					if (this.journalArchiveAddress.Address.IsValid && this.journalArchiveAddress.Address != RoutingAddress.NullReversePath)
					{
						this.validSenderJournalArchiveAddressIsSet = true;
					}
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004013 File Offset: 0x00002213
		public bool SenderJournalArchiveAddressIsValid
		{
			get
			{
				return this.validSenderJournalArchiveAddressIsSet;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000401B File Offset: 0x0000221B
		public List<AddressInfo> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004023 File Offset: 0x00002223
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000402B File Offset: 0x0000222B
		public bool Defective
		{
			get
			{
				return this.defective;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004033 File Offset: 0x00002233
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000403B File Offset: 0x0000223B
		public List<RoutingAddress> ExternalOrUnprovisionedRecipients
		{
			get
			{
				return this.externalOrUnprovisionedRecipients;
			}
			set
			{
				this.externalOrUnprovisionedRecipients = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004044 File Offset: 0x00002244
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000404C File Offset: 0x0000224C
		public List<RoutingAddress> DistributionLists
		{
			get
			{
				return this.distributionlists;
			}
			set
			{
				this.distributionlists = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004055 File Offset: 0x00002255
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000405D File Offset: 0x0000225D
		public bool IsSenderInternal
		{
			get
			{
				return this.isSenderInternal;
			}
			set
			{
				this.isSenderInternal = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004066 File Offset: 0x00002266
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000406E File Offset: 0x0000226E
		public Attachment EmbeddedMessageAttachment
		{
			get
			{
				return this.embeddedMessageAttachment;
			}
			set
			{
				this.embeddedMessageAttachment = value;
			}
		}

		// Token: 0x040000B1 RID: 177
		private readonly string messageId;

		// Token: 0x040000B2 RID: 178
		private readonly bool defective;

		// Token: 0x040000B3 RID: 179
		private AddressInfo sender;

		// Token: 0x040000B4 RID: 180
		private AddressInfo journalArchiveAddress;

		// Token: 0x040000B5 RID: 181
		private List<AddressInfo> recipients;

		// Token: 0x040000B6 RID: 182
		private List<RoutingAddress> externalOrUnprovisionedRecipients = new List<RoutingAddress>();

		// Token: 0x040000B7 RID: 183
		private List<RoutingAddress> distributionlists = new List<RoutingAddress>();

		// Token: 0x040000B8 RID: 184
		private bool isSenderInternal;

		// Token: 0x040000B9 RID: 185
		private Attachment embeddedMessageAttachment;

		// Token: 0x040000BA RID: 186
		private bool validSenderJournalArchiveAddressIsSet;
	}
}
