using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PrimaryConsistencyCheckChain : ConsistencyCheckChain<PrimaryConsistencyCheckResult>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000035D7 File Offset: 0x000017D7
		internal PrimaryConsistencyCheckChain(int capacity, MeetingComparisonResult totalResult) : base(capacity, totalResult)
		{
			this.ShouldTerminate = false;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000035E8 File Offset: 0x000017E8
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000035F0 File Offset: 0x000017F0
		internal bool ShouldTerminate { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x000035FC File Offset: 0x000017FC
		internal void PerformChecks(CalendarValidationContext context)
		{
			base.PerformChecks();
			if (!this.ShouldTerminate)
			{
				MeetingExistenceCheck check = new MeetingExistenceCheck(context);
				base.PerformCheck<MeetingExistenceConsistencyCheckResult>(check);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003626 File Offset: 0x00001826
		protected override bool ShouldContinue(PrimaryConsistencyCheckResult lastCheckResult)
		{
			this.ShouldTerminate = lastCheckResult.ShouldTerminate;
			return !this.ShouldTerminate;
		}
	}
}
