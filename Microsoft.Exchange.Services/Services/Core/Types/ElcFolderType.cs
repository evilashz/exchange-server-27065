using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E0 RID: 992
	[XmlType(TypeName = "ElcFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "ElcFolderType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public enum ElcFolderType
	{
		// Token: 0x04001240 RID: 4672
		Calendar = 1,
		// Token: 0x04001241 RID: 4673
		Contacts,
		// Token: 0x04001242 RID: 4674
		DeletedItems,
		// Token: 0x04001243 RID: 4675
		Drafts,
		// Token: 0x04001244 RID: 4676
		Inbox,
		// Token: 0x04001245 RID: 4677
		JunkEmail,
		// Token: 0x04001246 RID: 4678
		Journal,
		// Token: 0x04001247 RID: 4679
		Notes,
		// Token: 0x04001248 RID: 4680
		Outbox,
		// Token: 0x04001249 RID: 4681
		SentItems,
		// Token: 0x0400124A RID: 4682
		Tasks,
		// Token: 0x0400124B RID: 4683
		All,
		// Token: 0x0400124C RID: 4684
		ManagedCustomFolder,
		// Token: 0x0400124D RID: 4685
		RssSubscriptions,
		// Token: 0x0400124E RID: 4686
		SyncIssues,
		// Token: 0x0400124F RID: 4687
		ConversationHistory,
		// Token: 0x04001250 RID: 4688
		Personal,
		// Token: 0x04001251 RID: 4689
		RecoverableItems,
		// Token: 0x04001252 RID: 4690
		NonIpmRoot
	}
}
