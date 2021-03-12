using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200096B RID: 2411
	internal sealed class SetCalendarGroupOrderCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x06004543 RID: 17731 RVA: 0x000F26C4 File Offset: 0x000F08C4
		public SetCalendarGroupOrderCommand(CallContext callContext, string groupToPosition, string beforeGroup) : base(callContext)
		{
			this.command = new SetCalendarGroupOrder(base.MailboxIdentityMailboxSession, groupToPosition, beforeGroup);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000F26E0 File Offset: 0x000F08E0
		protected override CalendarActionResponse InternalExecute()
		{
			return this.command.Execute();
		}

		// Token: 0x04002852 RID: 10322
		private readonly SetCalendarGroupOrder command;
	}
}
