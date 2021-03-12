using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E8 RID: 1000
	[XmlType(TypeName = "DistinguishedFolderIdNameType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum DistinguishedFolderIdName
	{
		// Token: 0x0400126D RID: 4717
		none,
		// Token: 0x0400126E RID: 4718
		calendar,
		// Token: 0x0400126F RID: 4719
		contacts,
		// Token: 0x04001270 RID: 4720
		deleteditems,
		// Token: 0x04001271 RID: 4721
		drafts,
		// Token: 0x04001272 RID: 4722
		inbox,
		// Token: 0x04001273 RID: 4723
		journal,
		// Token: 0x04001274 RID: 4724
		notes,
		// Token: 0x04001275 RID: 4725
		outbox,
		// Token: 0x04001276 RID: 4726
		sentitems,
		// Token: 0x04001277 RID: 4727
		tasks,
		// Token: 0x04001278 RID: 4728
		msgfolderroot,
		// Token: 0x04001279 RID: 4729
		publicfoldersroot,
		// Token: 0x0400127A RID: 4730
		root,
		// Token: 0x0400127B RID: 4731
		junkemail,
		// Token: 0x0400127C RID: 4732
		searchfolders,
		// Token: 0x0400127D RID: 4733
		voicemail,
		// Token: 0x0400127E RID: 4734
		recoverableitemsroot,
		// Token: 0x0400127F RID: 4735
		recoverableitemsdeletions,
		// Token: 0x04001280 RID: 4736
		recoverableitemsversions,
		// Token: 0x04001281 RID: 4737
		recoverableitemspurges,
		// Token: 0x04001282 RID: 4738
		archiveroot,
		// Token: 0x04001283 RID: 4739
		archivemsgfolderroot,
		// Token: 0x04001284 RID: 4740
		archivedeleteditems,
		// Token: 0x04001285 RID: 4741
		archiveinbox,
		// Token: 0x04001286 RID: 4742
		archiverecoverableitemsroot,
		// Token: 0x04001287 RID: 4743
		archiverecoverableitemsdeletions,
		// Token: 0x04001288 RID: 4744
		archiverecoverableitemsversions,
		// Token: 0x04001289 RID: 4745
		archiverecoverableitemspurges,
		// Token: 0x0400128A RID: 4746
		syncissues,
		// Token: 0x0400128B RID: 4747
		conflicts,
		// Token: 0x0400128C RID: 4748
		localfailures,
		// Token: 0x0400128D RID: 4749
		serverfailures,
		// Token: 0x0400128E RID: 4750
		recipientcache,
		// Token: 0x0400128F RID: 4751
		quickcontacts,
		// Token: 0x04001290 RID: 4752
		conversationhistory,
		// Token: 0x04001291 RID: 4753
		adminauditlogs,
		// Token: 0x04001292 RID: 4754
		todosearch,
		// Token: 0x04001293 RID: 4755
		mycontacts,
		// Token: 0x04001294 RID: 4756
		directory,
		// Token: 0x04001295 RID: 4757
		imcontactlist,
		// Token: 0x04001296 RID: 4758
		peopleconnect,
		// Token: 0x04001297 RID: 4759
		internalsubmission,
		// Token: 0x04001298 RID: 4760
		fromfavoritesenders,
		// Token: 0x04001299 RID: 4761
		clutter,
		// Token: 0x0400129A RID: 4762
		favorites,
		// Token: 0x0400129B RID: 4763
		unifiedinbox,
		// Token: 0x0400129C RID: 4764
		workingset
	}
}
