using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000978 RID: 2424
	internal sealed class DisableBirthdayCalendarCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x0600458A RID: 17802 RVA: 0x000F41EA File Offset: 0x000F23EA
		public DisableBirthdayCalendarCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x000F41F3 File Offset: 0x000F23F3
		protected override CalendarActionResponse InternalExecute()
		{
			BirthdayCalendar.DisableBirthdayCalendar(base.MailboxIdentityMailboxSession);
			return new CalendarActionResponse();
		}
	}
}
