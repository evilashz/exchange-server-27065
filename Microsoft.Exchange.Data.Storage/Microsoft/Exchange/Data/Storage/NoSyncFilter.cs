using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E27 RID: 3623
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NoSyncFilter : QueryBasedSyncFilter
	{
		// Token: 0x06007D6B RID: 32107 RVA: 0x00229980 File Offset: 0x00227B80
		public NoSyncFilter() : base(null, null)
		{
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x0022998A File Offset: 0x00227B8A
		public override string Id
		{
			get
			{
				return "NoSyncFilter";
			}
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x00229991 File Offset: 0x00227B91
		public override bool IsItemInFilter(ISyncItemId id)
		{
			return true;
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x00229994 File Offset: 0x00227B94
		public override void UpdateFilterState(SyncOperation syncOperation)
		{
			ServerManifestEntry value = new ServerManifestEntry(syncOperation.Id);
			base.EntriesInFilter[syncOperation.Id] = value;
		}
	}
}
