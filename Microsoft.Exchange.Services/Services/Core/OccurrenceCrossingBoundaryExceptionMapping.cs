using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000223 RID: 547
	internal class OccurrenceCrossingBoundaryExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x000454F9 File Offset: 0x000436F9
		public OccurrenceCrossingBoundaryExceptionMapping() : base(typeof(OccurrenceCrossingBoundaryException), ResponseCodeType.ErrorOccurrenceCrossingBoundary, CoreResources.IDs.ErrorOccurrenceCrossingBoundary)
		{
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00045518 File Offset: 0x00043718
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			OccurrenceCrossingBoundaryException ex = base.VerifyExceptionType<OccurrenceCrossingBoundaryException>(exception);
			return new Dictionary<string, string>
			{
				{
					"AdjacentOccurrenceOriginalStartTime",
					ex.NeighborInfo.OriginalStartTime.ToString()
				},
				{
					"AdjacentOccurrenceStartTime",
					ex.NeighborInfo.StartTime.ToString()
				},
				{
					"AdjacentOccurrenceEndTime",
					ex.NeighborInfo.EndTime.ToString()
				},
				{
					"ModifiedOccurrenceOriginalStartTime",
					ex.OccurrenceInfo.OriginalStartTime.ToString()
				},
				{
					"ModifiedOccurrenceStartTime",
					ex.OccurrenceInfo.StartTime.ToString()
				},
				{
					"ModifiedOccurrenceEndTime",
					ex.OccurrenceInfo.EndTime.ToString()
				}
			};
		}

		// Token: 0x04000AF1 RID: 2801
		private const string AdjacentOccurrenceOriginalStartTime = "AdjacentOccurrenceOriginalStartTime";

		// Token: 0x04000AF2 RID: 2802
		private const string AdjacentOccurrenceStartTime = "AdjacentOccurrenceStartTime";

		// Token: 0x04000AF3 RID: 2803
		private const string AdjacentOccurrenceEndTime = "AdjacentOccurrenceEndTime";

		// Token: 0x04000AF4 RID: 2804
		private const string ModifiedOccurrenceOriginalStartTime = "ModifiedOccurrenceOriginalStartTime";

		// Token: 0x04000AF5 RID: 2805
		private const string ModifiedOccurrenceStartTime = "ModifiedOccurrenceStartTime";

		// Token: 0x04000AF6 RID: 2806
		private const string ModifiedOccurrenceEndTime = "ModifiedOccurrenceEndTime";
	}
}
