using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class PrimaryConsistencyCheckResult : ConsistencyCheckResult
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000C462 File Offset: 0x0000A662
		protected PrimaryConsistencyCheckResult(ConsistencyCheckType checkType, string checkDescription, bool terminateIfNotPassed) : base(checkType, checkDescription)
		{
			this.terminationOverride = false;
			this.terminateIfNotPassed = terminateIfNotPassed;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C47A File Offset: 0x0000A67A
		internal static PrimaryConsistencyCheckResult CreateInstance(ConsistencyCheckType checkType, string checkDescription, bool terminateIfNotPassed)
		{
			return new PrimaryConsistencyCheckResult(checkType, checkDescription, terminateIfNotPassed);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000C484 File Offset: 0x0000A684
		internal virtual bool ShouldTerminate
		{
			get
			{
				return this.terminationOverride || (this.terminateIfNotPassed && base.Status != CheckStatusType.Passed);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		internal void TerminateCheck(string error)
		{
			this.terminationOverride = true;
			if (error != null)
			{
				base.ErrorString = error;
			}
		}

		// Token: 0x04000107 RID: 263
		private readonly bool terminateIfNotPassed;

		// Token: 0x04000108 RID: 264
		private bool terminationOverride;
	}
}
