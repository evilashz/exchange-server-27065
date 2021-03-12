using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003CF RID: 975
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OccurrenceInfo
	{
		// Token: 0x06002BDA RID: 11226 RVA: 0x000AE9DF File Offset: 0x000ACBDF
		internal OccurrenceInfo(VersionedId versionedId, ExDateTime occurrenceDateId, ExDateTime originalStartTime, ExDateTime startTime, ExDateTime endTime)
		{
			this.OriginalStartTime = originalStartTime;
			this.OccurrenceDateId = occurrenceDateId;
			this.VersionedId = versionedId;
			this.StartTime = startTime;
			this.EndTime = endTime;
		}

		// Token: 0x040018A7 RID: 6311
		public readonly VersionedId VersionedId;

		// Token: 0x040018A8 RID: 6312
		public readonly ExDateTime OccurrenceDateId;

		// Token: 0x040018A9 RID: 6313
		public readonly ExDateTime OriginalStartTime;

		// Token: 0x040018AA RID: 6314
		public readonly ExDateTime StartTime;

		// Token: 0x040018AB RID: 6315
		public readonly ExDateTime EndTime;

		// Token: 0x040018AC RID: 6316
		public DifferencesBetweenBlobAndAttach BlobDifferences;
	}
}
