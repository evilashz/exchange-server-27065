using System;
using Microsoft.Exchange.Assistants.EventLog;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002F RID: 47
	internal sealed class DatabaseGovernor : ThrottleGovernor
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000073EB File Offset: 0x000055EB
		public DatabaseGovernor(string name, Governor parentGovernor, Throttle throttle) : base(parentGovernor, throttle)
		{
			this.name = name;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000073FC File Offset: 0x000055FC
		public override string ToString()
		{
			return "DatabaseGovernor for " + this.name;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000740E File Offset: 0x0000560E
		protected override bool IsFailureRelevant(AITransientException exception)
		{
			return exception is TransientDatabaseException;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000741C File Offset: 0x0000561C
		protected override void Log30MinuteWarning(AITransientException exception, TimeSpan nextRetryInterval)
		{
			base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseGovernorFailure, null, new object[]
			{
				this,
				base.LastRunTime.ToLocalTime(),
				nextRetryInterval,
				exception
			});
		}

		// Token: 0x04000146 RID: 326
		private string name;
	}
}
