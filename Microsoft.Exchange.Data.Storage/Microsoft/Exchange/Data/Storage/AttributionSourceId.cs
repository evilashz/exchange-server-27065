using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200048D RID: 1165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AttributionSourceId
	{
		// Token: 0x060033B7 RID: 13239 RVA: 0x000D210A File Offset: 0x000D030A
		private AttributionSourceId(object sourceId)
		{
			this.sourceId = sourceId;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000D2119 File Offset: 0x000D0319
		public static AttributionSourceId CreateFrom(StoreId contactStoreId)
		{
			ArgumentValidator.ThrowIfNull("contactStoreId", contactStoreId);
			return new AttributionSourceId(contactStoreId);
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000D212C File Offset: 0x000D032C
		public static AttributionSourceId CreateFrom(Guid adObjectIdGuid)
		{
			ArgumentValidator.ThrowIfEmpty("adObjectIdGuid", adObjectIdGuid);
			return new AttributionSourceId(adObjectIdGuid);
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000D2144 File Offset: 0x000D0344
		public bool IsStoreId
		{
			get
			{
				return this.sourceId is StoreId;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000D2154 File Offset: 0x000D0354
		public bool IsADObjectIdGuid
		{
			get
			{
				return this.sourceId is Guid;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000D2164 File Offset: 0x000D0364
		public StoreId StoreId
		{
			get
			{
				return (StoreId)this.sourceId;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000D2171 File Offset: 0x000D0371
		public Guid ADObjectIdGuid
		{
			get
			{
				return (Guid)this.sourceId;
			}
		}

		// Token: 0x04001BDA RID: 7130
		private readonly object sourceId;
	}
}
