using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003D0 RID: 976
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExceptionInfo : OccurrenceInfo
	{
		// Token: 0x06002BDB RID: 11227 RVA: 0x000AEA0C File Offset: 0x000ACC0C
		public ExceptionInfo(VersionedId versionedId, ExDateTime occurrenceDateId, ExDateTime originalStartTime, ExDateTime startTime, ExDateTime endTime, ModificationType modificationType, MemoryPropertyBag propertyBag) : base(versionedId, occurrenceDateId, originalStartTime, startTime, endTime)
		{
			this.ModificationType = modificationType;
			this.PropertyBag = propertyBag;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000AEA2C File Offset: 0x000ACC2C
		internal ExceptionInfo(VersionedId versionedId, ExceptionInfo exceptionInfo) : base(versionedId, exceptionInfo.OccurrenceDateId, exceptionInfo.OriginalStartTime, exceptionInfo.StartTime, exceptionInfo.EndTime)
		{
			this.PropertyBag = new MemoryPropertyBag(exceptionInfo.PropertyBag);
			this.ModificationType = exceptionInfo.ModificationType;
			this.BlobDifferences = exceptionInfo.BlobDifferences;
		}

		// Token: 0x040018AD RID: 6317
		public readonly MemoryPropertyBag PropertyBag;

		// Token: 0x040018AE RID: 6318
		public ModificationType ModificationType;
	}
}
