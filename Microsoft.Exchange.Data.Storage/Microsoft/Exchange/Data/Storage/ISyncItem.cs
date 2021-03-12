using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E58 RID: 3672
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncItem : IDisposeTrackable, IDisposable
	{
		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x06007F38 RID: 32568
		ISyncItemId Id { get; }

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x06007F39 RID: 32569
		ISyncWatermark Watermark { get; }

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x06007F3A RID: 32570
		object NativeItem { get; }

		// Token: 0x06007F3B RID: 32571
		void Load();

		// Token: 0x06007F3C RID: 32572
		void Save();

		// Token: 0x06007F3D RID: 32573
		bool IsItemInFilter(QueryFilter filter);
	}
}
