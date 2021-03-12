using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000089 RID: 137
	[DataContract]
	public class ConversationsConfiguration : MessagingConfigurationBase
	{
		// Token: 0x06001B97 RID: 7063 RVA: 0x00057536 File Offset: 0x00055736
		public ConversationsConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0005753F File Offset: 0x0005573F
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x00057556 File Offset: 0x00055756
		[DataMember]
		public string ConversationSortOrder
		{
			get
			{
				return base.MailboxMessageConfiguration.ConversationSortOrder.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00057560 File Offset: 0x00055760
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x00057585 File Offset: 0x00055785
		[DataMember]
		public string ShowConversationAsTree
		{
			get
			{
				return base.MailboxMessageConfiguration.ShowConversationAsTree.ToString().ToLowerInvariant();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0005758C File Offset: 0x0005578C
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x00057599 File Offset: 0x00055799
		[DataMember]
		public bool HideDeletedItems
		{
			get
			{
				return base.MailboxMessageConfiguration.HideDeletedItems;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
