using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E9 RID: 745
	[DataContract]
	public class SendAddressConfiguration : BaseRow
	{
		// Token: 0x06002D0F RID: 11535 RVA: 0x0008A222 File Offset: 0x00088422
		public SendAddressConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
			this.MailboxMessageConfiguration = mailboxMessageConfiguration;
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x0008A232 File Offset: 0x00088432
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x0008A23F File Offset: 0x0008843F
		[DataMember]
		public string SendAddressDefault
		{
			get
			{
				return this.MailboxMessageConfiguration.SendAddressDefault;
			}
			set
			{
				this.MailboxMessageConfiguration.SendAddressDefault = value;
			}
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x0008A24D File Offset: 0x0008844D
		// (set) Token: 0x06002D13 RID: 11539 RVA: 0x0008A255 File Offset: 0x00088455
		private MailboxMessageConfiguration MailboxMessageConfiguration { get; set; }
	}
}
