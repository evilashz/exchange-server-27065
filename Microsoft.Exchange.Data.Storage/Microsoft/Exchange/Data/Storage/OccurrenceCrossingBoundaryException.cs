using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200075C RID: 1884
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OccurrenceCrossingBoundaryException : RecurrenceException
	{
		// Token: 0x06004864 RID: 18532 RVA: 0x00130EFD File Offset: 0x0012F0FD
		public OccurrenceCrossingBoundaryException(OccurrenceInfo occurrenceInfo, OccurrenceInfo neighborInfo, LocalizedString message, bool isSameDayInOrganizerTimeZone) : base(message, null)
		{
			this.OccurrenceInfo = occurrenceInfo;
			this.NeighborInfo = neighborInfo;
			this.IsSameDayInOrganizerTimeZone = isSameDayInOrganizerTimeZone;
		}

		// Token: 0x04002750 RID: 10064
		public readonly OccurrenceInfo OccurrenceInfo;

		// Token: 0x04002751 RID: 10065
		public readonly OccurrenceInfo NeighborInfo;

		// Token: 0x04002752 RID: 10066
		public readonly bool IsSameDayInOrganizerTimeZone;
	}
}
