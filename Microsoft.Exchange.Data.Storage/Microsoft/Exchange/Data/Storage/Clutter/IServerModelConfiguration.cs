using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x0200043C RID: 1084
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IServerModelConfiguration
	{
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x0600306D RID: 12397
		int MaxModelVersion { get; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x0600306E RID: 12398
		int MinModelVersion { get; }

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x0600306F RID: 12399
		int NumberOfVersionCrumbsToRecord { get; }

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003070 RID: 12400
		bool AllowTrainingOnMutipleModelVersions { get; }

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003071 RID: 12401
		int NumberOfModelVersionToTrain { get; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003072 RID: 12402
		IEnumerable<int> BlockedModelVersions { get; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003073 RID: 12403
		IEnumerable<int> ClassificationModelVersions { get; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003074 RID: 12404
		IEnumerable<int> DeprecatedModelVersions { get; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003075 RID: 12405
		double ProbabilityBehaviourSwitchPerWeek { get; }

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003076 RID: 12406
		double SymmetricNoise { get; }
	}
}
