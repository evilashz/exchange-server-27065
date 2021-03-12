using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000321 RID: 801
	internal sealed class GetReminders : SingleStepServiceCommand<GetRemindersRequest, ReminderType[]>
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x00077158 File Offset: 0x00075358
		public GetReminders(CallContext callContext, GetRemindersRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00077162 File Offset: 0x00075362
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetRemindersResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0007718C File Offset: 0x0007538C
		internal override ServiceResult<ReminderType[]> Execute()
		{
			ExDateTime reminderWindowStart = new ExDateTime(EWSSettings.RequestTimeZone, base.Request.BeginTime);
			if (!string.IsNullOrEmpty(base.Request.BeginTimeString))
			{
				reminderWindowStart = ExDateTime.ParseISO(EWSSettings.RequestTimeZone, base.Request.BeginTimeString);
			}
			ExDateTime exDateTime = new ExDateTime(EWSSettings.RequestTimeZone, base.Request.EndTime);
			if (!string.IsNullOrEmpty(base.Request.EndTimeString))
			{
				exDateTime = ExDateTime.ParseISO(EWSSettings.RequestTimeZone, base.Request.EndTimeString);
			}
			else if (exDateTime <= ExDateTime.MinValue.AddDays(1.0))
			{
				exDateTime = ExDateTime.MaxValue;
			}
			GetRemindersInternal getRemindersInternal = new GetRemindersInternal(base.MailboxIdentityMailboxSession, reminderWindowStart, exDateTime, base.Request.MaxItems, base.Request.ReminderType, base.Request.ReminderGroupType);
			return new ServiceResult<ReminderType[]>(getRemindersInternal.Execute(ExDateTime.Now));
		}
	}
}
