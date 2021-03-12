using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x02000130 RID: 304
	internal interface ILegacyResourceHealthProvider
	{
		// Token: 0x06000A5F RID: 2655
		void Update(ConstraintCheckResultType constraintResult, ConstraintCheckAgent agent, LocalizedString failureReason);

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A60 RID: 2656
		ConstraintCheckResultType ConstraintResult { get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A61 RID: 2657
		ConstraintCheckAgent Agent { get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A62 RID: 2658
		LocalizedString FailureReason { get; }
	}
}
