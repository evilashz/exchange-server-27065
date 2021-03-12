using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000502 RID: 1282
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OscFolderCreateResult
	{
		// Token: 0x0600378D RID: 14221 RVA: 0x000DFB75 File Offset: 0x000DDD75
		public OscFolderCreateResult(StoreObjectId folderId, bool created)
		{
			ArgumentValidator.ThrowIfNull("folderId", folderId);
			this.FolderId = folderId;
			this.Created = created;
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x000DFB96 File Offset: 0x000DDD96
		// (set) Token: 0x0600378F RID: 14223 RVA: 0x000DFB9E File Offset: 0x000DDD9E
		public StoreObjectId FolderId { get; private set; }

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000DFBA7 File Offset: 0x000DDDA7
		// (set) Token: 0x06003791 RID: 14225 RVA: 0x000DFBAF File Offset: 0x000DDDAF
		public bool Created { get; private set; }

		// Token: 0x06003792 RID: 14226 RVA: 0x000DFBB8 File Offset: 0x000DDDB8
		public override string ToString()
		{
			return string.Format("{{ FolderId: {0};  Created: {1} }}", this.FolderId, this.Created);
		}
	}
}
