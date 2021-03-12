using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B19 RID: 2841
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TaskGroup
	{
		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06005097 RID: 20631 RVA: 0x00109C3F File Offset: 0x00107E3F
		// (set) Token: 0x06005098 RID: 20632 RVA: 0x00109C47 File Offset: 0x00107E47
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x00109C50 File Offset: 0x00107E50
		// (set) Token: 0x0600509A RID: 20634 RVA: 0x00109C58 File Offset: 0x00107E58
		[DataMember]
		public string GroupId { get; set; }

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x00109C61 File Offset: 0x00107E61
		// (set) Token: 0x0600509C RID: 20636 RVA: 0x00109C69 File Offset: 0x00107E69
		[DataMember]
		public string GroupName { get; set; }

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x00109C72 File Offset: 0x00107E72
		// (set) Token: 0x0600509E RID: 20638 RVA: 0x00109C7A File Offset: 0x00107E7A
		[DataMember]
		public TaskGroupType GroupType { get; set; }

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x00109C83 File Offset: 0x00107E83
		// (set) Token: 0x060050A0 RID: 20640 RVA: 0x00109C8B File Offset: 0x00107E8B
		[DataMember]
		public TaskFolderEntry[] TaskFolders { get; set; }
	}
}
