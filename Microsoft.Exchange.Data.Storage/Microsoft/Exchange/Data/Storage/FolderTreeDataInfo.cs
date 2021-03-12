using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200039E RID: 926
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FolderTreeDataInfo
	{
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000A401F File Offset: 0x000A221F
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x000A4027 File Offset: 0x000A2227
		public VersionedId Id { get; private set; }

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x000A4030 File Offset: 0x000A2230
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x000A4038 File Offset: 0x000A2238
		public byte[] Ordinal { get; private set; }

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000A4041 File Offset: 0x000A2241
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x000A4049 File Offset: 0x000A2249
		public ExDateTime LastModifiedTime { get; private set; }

		// Token: 0x06002933 RID: 10547 RVA: 0x000A4052 File Offset: 0x000A2252
		public FolderTreeDataInfo(VersionedId id, byte[] ordinal, ExDateTime lastModifiedTime)
		{
			Util.ThrowOnNullArgument(id, "id");
			this.Id = id;
			this.Ordinal = ordinal;
			this.LastModifiedTime = lastModifiedTime;
		}
	}
}
