using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000993 RID: 2451
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentLibraryInfo
	{
		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x00176FE3 File Offset: 0x001751E3
		// (set) Token: 0x06005A5F RID: 23135 RVA: 0x00176FEB File Offset: 0x001751EB
		public StoreObjectId FolderId { get; private set; }

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x00176FF4 File Offset: 0x001751F4
		// (set) Token: 0x06005A61 RID: 23137 RVA: 0x00176FFC File Offset: 0x001751FC
		public Guid SharepointId { get; private set; }

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x00177005 File Offset: 0x00175205
		// (set) Token: 0x06005A63 RID: 23139 RVA: 0x0017700D File Offset: 0x0017520D
		public string SharepointUrl { get; private set; }

		// Token: 0x06005A64 RID: 23140 RVA: 0x00177016 File Offset: 0x00175216
		public DocumentLibraryInfo(StoreObjectId folderId, Guid sharepointId, string sharepointUrl)
		{
			this.FolderId = folderId;
			this.SharepointId = sharepointId;
			this.SharepointUrl = sharepointUrl;
		}
	}
}
