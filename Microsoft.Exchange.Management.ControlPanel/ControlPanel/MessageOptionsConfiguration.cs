using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000099 RID: 153
	[DataContract]
	public class MessageOptionsConfiguration : MessagingConfigurationBase
	{
		// Token: 0x06001BDF RID: 7135 RVA: 0x00057A7B File Offset: 0x00055C7B
		public MessageOptionsConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00057A84 File Offset: 0x00055C84
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x00057A9B File Offset: 0x00055C9B
		[DataMember]
		public string AfterMoveOrDeleteBehavior
		{
			get
			{
				return base.MailboxMessageConfiguration.AfterMoveOrDeleteBehavior.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00057AA2 File Offset: 0x00055CA2
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x00057AAF File Offset: 0x00055CAF
		[DataMember]
		public int NewItemNotification
		{
			get
			{
				return (int)base.MailboxMessageConfiguration.NewItemNotification;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00057AB6 File Offset: 0x00055CB6
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00057AC3 File Offset: 0x00055CC3
		[DataMember]
		public bool EmptyDeletedItemsOnLogoff
		{
			get
			{
				return base.MailboxMessageConfiguration.EmptyDeletedItemsOnLogoff;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00057ACA File Offset: 0x00055CCA
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x00057AD7 File Offset: 0x00055CD7
		[DataMember]
		public bool CheckForForgottenAttachments
		{
			get
			{
				return base.MailboxMessageConfiguration.CheckForForgottenAttachments;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
