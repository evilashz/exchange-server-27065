using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002BC RID: 700
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAttachment : IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001D46 RID: 7494
		AttachmentType AttachmentType { get; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001D47 RID: 7495
		bool IsContactPhoto { get; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001D48 RID: 7496
		AttachmentId Id { get; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001D49 RID: 7497
		// (set) Token: 0x06001D4A RID: 7498
		string ContentType { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001D4B RID: 7499
		string CalculatedContentType { get; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001D4C RID: 7500
		string DisplayName { get; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001D4D RID: 7501
		string FileExtension { get; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001D4E RID: 7502
		// (set) Token: 0x06001D4F RID: 7503
		string FileName { get; set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001D50 RID: 7504
		// (set) Token: 0x06001D51 RID: 7505
		bool IsInline { get; set; }

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001D52 RID: 7506
		ExDateTime LastModifiedTime { get; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001D53 RID: 7507
		long Size { get; }

		// Token: 0x06001D54 RID: 7508
		void Save();
	}
}
