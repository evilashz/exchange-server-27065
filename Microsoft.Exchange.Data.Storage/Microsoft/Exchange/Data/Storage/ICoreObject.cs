using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreObject : ICoreState, IValidatable, IDisposeTrackable, IDisposable
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600041B RID: 1051
		ICorePropertyBag PropertyBag { get; }

		// Token: 0x0600041C RID: 1052
		Schema GetCorrectSchemaForStoreObject();

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600041D RID: 1053
		StoreSession Session { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600041E RID: 1054
		StoreObjectId StoreObjectId { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600041F RID: 1055
		StoreObjectId InternalStoreObjectId { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000420 RID: 1056
		VersionedId Id { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000421 RID: 1057
		bool IsDirty { get; }

		// Token: 0x06000422 RID: 1058
		void ResetId();

		// Token: 0x06000423 RID: 1059
		void SetEnableFullValidation(bool enableFullValidation);
	}
}
