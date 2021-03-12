using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B18 RID: 2840
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TaskFolderEntry
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x0600508C RID: 20620 RVA: 0x00109BE2 File Offset: 0x00107DE2
		// (set) Token: 0x0600508D RID: 20621 RVA: 0x00109BEA File Offset: 0x00107DEA
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x00109BF3 File Offset: 0x00107DF3
		// (set) Token: 0x0600508F RID: 20623 RVA: 0x00109BFB File Offset: 0x00107DFB
		[DataMember]
		public string FolderName { get; set; }

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06005090 RID: 20624 RVA: 0x00109C04 File Offset: 0x00107E04
		// (set) Token: 0x06005091 RID: 20625 RVA: 0x00109C0C File Offset: 0x00107E0C
		[DataMember]
		public string ParentGroupId { get; set; }

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06005092 RID: 20626 RVA: 0x00109C15 File Offset: 0x00107E15
		// (set) Token: 0x06005093 RID: 20627 RVA: 0x00109C1D File Offset: 0x00107E1D
		[DataMember]
		public FolderId TaskFolderId { get; set; }

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06005094 RID: 20628 RVA: 0x00109C26 File Offset: 0x00107E26
		// (set) Token: 0x06005095 RID: 20629 RVA: 0x00109C2E File Offset: 0x00107E2E
		[DataMember]
		public TaskFolderEntryType TaskFolderEntryType { get; set; }
	}
}
