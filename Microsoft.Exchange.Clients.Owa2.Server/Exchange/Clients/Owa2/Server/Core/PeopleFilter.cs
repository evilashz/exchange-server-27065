using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200020F RID: 527
	[DataContract]
	public class PeopleFilter
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00048EF6 File Offset: 0x000470F6
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00048EFE File Offset: 0x000470FE
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00048F07 File Offset: 0x00047107
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00048F0F File Offset: 0x0004710F
		[DataMember]
		public BaseFolderId FolderId { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00048F18 File Offset: 0x00047118
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00048F20 File Offset: 0x00047120
		[DataMember]
		public BaseFolderId ParentFolderId { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00048F29 File Offset: 0x00047129
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00048F31 File Offset: 0x00047131
		[DataMember]
		public int TotalCount { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00048F3A File Offset: 0x0004713A
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00048F42 File Offset: 0x00047142
		[DataMember]
		internal int SortGroupPriority { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00048F4B File Offset: 0x0004714B
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00048F53 File Offset: 0x00047153
		[DataMember]
		internal bool IsReadOnly { get; set; }
	}
}
