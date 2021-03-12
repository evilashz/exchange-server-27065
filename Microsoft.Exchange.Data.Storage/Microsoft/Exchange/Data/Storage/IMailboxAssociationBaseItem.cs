using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D4 RID: 2004
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxAssociationBaseItem : IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06004B1C RID: 19228
		// (set) Token: 0x06004B1D RID: 19229
		string LegacyDN { get; set; }

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06004B1E RID: 19230
		// (set) Token: 0x06004B1F RID: 19231
		string ExternalId { get; set; }

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06004B20 RID: 19232
		// (set) Token: 0x06004B21 RID: 19233
		SmtpAddress SmtpAddress { get; set; }

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06004B22 RID: 19234
		// (set) Token: 0x06004B23 RID: 19235
		bool IsMember { get; set; }

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06004B24 RID: 19236
		// (set) Token: 0x06004B25 RID: 19237
		bool ShouldEscalate { get; set; }

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06004B26 RID: 19238
		// (set) Token: 0x06004B27 RID: 19239
		bool IsAutoSubscribed { get; set; }

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06004B28 RID: 19240
		// (set) Token: 0x06004B29 RID: 19241
		bool IsPin { get; set; }

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06004B2A RID: 19242
		// (set) Token: 0x06004B2B RID: 19243
		ExDateTime JoinDate { get; set; }

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06004B2C RID: 19244
		// (set) Token: 0x06004B2D RID: 19245
		string SyncedIdentityHash { get; set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06004B2E RID: 19246
		// (set) Token: 0x06004B2F RID: 19247
		int CurrentVersion { get; set; }

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06004B30 RID: 19248
		// (set) Token: 0x06004B31 RID: 19249
		int SyncedVersion { get; set; }

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06004B32 RID: 19250
		// (set) Token: 0x06004B33 RID: 19251
		string LastSyncError { get; set; }

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06004B34 RID: 19252
		// (set) Token: 0x06004B35 RID: 19253
		int SyncAttempts { get; set; }

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06004B36 RID: 19254
		// (set) Token: 0x06004B37 RID: 19255
		string SyncedSchemaVersion { get; set; }
	}
}
