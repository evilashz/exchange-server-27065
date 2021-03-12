using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class MeetingExistenceConsistencyCheckResult : PrimaryConsistencyCheckResult
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000C4B9 File Offset: 0x0000A6B9
		private MeetingExistenceConsistencyCheckResult(ConsistencyCheckType checkType, string checkDescription) : base(checkType, checkDescription, true)
		{
			this.ItemIsFound = false;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C4CB File Offset: 0x0000A6CB
		internal override bool ShouldTerminate
		{
			get
			{
				return base.ShouldTerminate || !this.ItemIsFound;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		internal new static MeetingExistenceConsistencyCheckResult CreateInstance(ConsistencyCheckType checkType, string checkDescription)
		{
			return new MeetingExistenceConsistencyCheckResult(checkType, checkDescription);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000C4F1 File Offset: 0x0000A6F1
		internal bool ItemIsFound { get; set; }
	}
}
