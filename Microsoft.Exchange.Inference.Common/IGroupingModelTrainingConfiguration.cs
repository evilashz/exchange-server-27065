using System;
using Microsoft.Exchange.Inference.Common.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000015 RID: 21
	internal interface IGroupingModelTrainingConfiguration
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000084 RID: 132
		ILogConfig GroupingModelTrainingStatusLogConfig { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000085 RID: 133
		int MaxNumberOfDaysToQueryForFirstRun { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000086 RID: 134
		double GroupPseudocountPruningThreshold { get; }
	}
}
