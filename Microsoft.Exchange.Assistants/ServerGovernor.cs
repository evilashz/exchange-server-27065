using System;
using Microsoft.Exchange.Assistants.EventLog;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000078 RID: 120
	internal sealed class ServerGovernor : ThrottleGovernor
	{
		// Token: 0x0600037F RID: 895 RVA: 0x000114B6 File Offset: 0x0000F6B6
		public ServerGovernor(string name, Throttle throttle) : base(null, throttle)
		{
			this.name = name;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000114C7 File Offset: 0x0000F6C7
		public override string ToString()
		{
			return "ServerGovernor for " + this.name;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000114D9 File Offset: 0x0000F6D9
		protected override bool IsFailureRelevant(AITransientException exception)
		{
			return exception is TransientServerException;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000114E4 File Offset: 0x0000F6E4
		protected override void Log30MinuteWarning(AITransientException exception, TimeSpan nextRetryInterval)
		{
			base.LogEvent(AssistantsEventLogConstants.Tuple_ServerGovernorFailure, null, new object[]
			{
				this,
				base.LastRunTime.ToLocalTime(),
				nextRetryInterval,
				exception
			});
		}

		// Token: 0x040001FF RID: 511
		private string name;
	}
}
