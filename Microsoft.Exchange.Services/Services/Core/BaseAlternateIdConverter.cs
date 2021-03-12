using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B6 RID: 182
	internal abstract class BaseAlternateIdConverter
	{
		// Token: 0x060004F0 RID: 1264
		internal abstract CanonicalConvertedId Parse(AlternateId id);

		// Token: 0x060004F1 RID: 1265
		internal abstract CanonicalConvertedId Parse(AlternatePublicFolderId id);

		// Token: 0x060004F2 RID: 1266
		internal abstract CanonicalConvertedId Parse(AlternatePublicFolderItemId id);

		// Token: 0x060004F3 RID: 1267
		internal abstract string ConvertStoreObjectIdToString(StoreObjectId storeObjectId);

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F4 RID: 1268
		internal abstract IdFormat IdFormat { get; }

		// Token: 0x060004F5 RID: 1269
		internal abstract AlternateIdBase Format(CanonicalConvertedId canonicalId);
	}
}
