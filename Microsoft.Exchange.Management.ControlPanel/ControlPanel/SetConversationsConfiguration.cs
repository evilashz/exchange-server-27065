using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200008B RID: 139
	[DataContract]
	public class SetConversationsConfiguration : SetMessagingConfigurationBase
	{
		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000575B6 File Offset: 0x000557B6
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x000575C8 File Offset: 0x000557C8
		[DataMember]
		public string ConversationSortOrder
		{
			get
			{
				return (string)base["ConversationSortOrder"];
			}
			set
			{
				base["ConversationSortOrder"] = value;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x000575D6 File Offset: 0x000557D6
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x000575F2 File Offset: 0x000557F2
		[DataMember]
		public bool ShowConversationAsTree
		{
			get
			{
				return (bool)(base["ShowConversationAsTree"] ?? false);
			}
			set
			{
				base["ShowConversationAsTree"] = value;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00057605 File Offset: 0x00055805
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x00057621 File Offset: 0x00055821
		[DataMember]
		public bool HideDeletedItems
		{
			get
			{
				return (bool)(base["HideDeletedItems"] ?? false);
			}
			set
			{
				base["HideDeletedItems"] = value;
			}
		}
	}
}
