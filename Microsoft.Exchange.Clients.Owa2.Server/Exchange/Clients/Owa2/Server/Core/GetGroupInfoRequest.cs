using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000209 RID: 521
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetGroupInfoRequest
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00048D47 File Offset: 0x00046F47
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x00048D4F File Offset: 0x00046F4F
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00048D58 File Offset: 0x00046F58
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x00048D60 File Offset: 0x00046F60
		[DataMember]
		public string AdObjectId { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00048D69 File Offset: 0x00046F69
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00048D71 File Offset: 0x00046F71
		[DataMember]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00048D7A File Offset: 0x00046F7A
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00048D82 File Offset: 0x00046F82
		[DataMember]
		public IndexedPageView Paging { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00048D8B File Offset: 0x00046F8B
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x00048D93 File Offset: 0x00046F93
		[DataMember]
		public GetGroupResultSet ResultSet { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00048D9C File Offset: 0x00046F9C
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x00048DA4 File Offset: 0x00046FA4
		[DataMember]
		public TargetFolderId ParentFolderId { get; set; }
	}
}
