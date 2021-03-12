using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200097A RID: 2426
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetBirthdayCalendarViewCommand : SingleStepServiceCommand<GetBirthdayCalendarViewRequest, GetBirthdayCalendarViewResponseMessage>
	{
		// Token: 0x0600458E RID: 17806 RVA: 0x000F42AC File Offset: 0x000F24AC
		public GetBirthdayCalendarViewCommand(CallContext callContext, GetBirthdayCalendarViewRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000F42B6 File Offset: 0x000F24B6
		private static Microsoft.Exchange.Services.Core.Types.ItemId GetEwsContactIdFrom(IBirthdayEventInternal eventEntity, MailboxSession session)
		{
			return IdConverter.PersonaIdFromStoreId(eventEntity.ContactId, new MailboxId(session));
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x000F42C9 File Offset: 0x000F24C9
		private static Microsoft.Exchange.Services.Core.Types.ItemId GetEwsPersonIdFrom(IBirthdayEventInternal eventEntity, IStoreSession session)
		{
			return new Microsoft.Exchange.Services.Core.Types.ItemId(IdConverter.PersonIdToEwsId(session.MailboxGuid, eventEntity.PersonId), null);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000F42E2 File Offset: 0x000F24E2
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetBirthdayCalendarViewResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x000F43B0 File Offset: 0x000F25B0
		internal override ServiceResult<GetBirthdayCalendarViewResponseMessage> Execute()
		{
			if (!VariantConfiguration.GetSnapshot(base.CallContext.AccessingADUser.GetContext(null), null, null).OwaServer.XOWABirthdayAssistant.Enabled)
			{
				throw new InvalidOperationException("The user is not on the birthday flight");
			}
			CalendarPageView calendarPageView = new CalendarPageView();
			calendarPageView.StartDateString = base.Request.StartRange;
			calendarPageView.EndDateString = base.Request.EndRange;
			MailboxSession session = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			IEnumerable<Microsoft.Exchange.Entities.DataModel.BirthdayCalendar.BirthdayEvent> birthdayCalendarView = new BirthdaysContainer(session, null).Events.GetBirthdayCalendarView(calendarPageView.StartDateEx, calendarPageView.EndDateEx);
			IEnumerable<Microsoft.Exchange.Services.Wcf.Types.BirthdayEvent> source = from eventEntity in birthdayCalendarView
			select new Microsoft.Exchange.Services.Wcf.Types.BirthdayEvent
			{
				Name = eventEntity.Subject,
				PersonId = GetBirthdayCalendarViewCommand.GetEwsPersonIdFrom(eventEntity, session),
				ContactId = GetBirthdayCalendarViewCommand.GetEwsContactIdFrom(eventEntity, session),
				Birthday = ExDateTimeConverter.ToOffsetXsdDateTime(eventEntity.Birthday.AddMinutes(-eventEntity.Birthday.Bias.TotalMinutes), eventEntity.Birthday.TimeZone),
				Attribution = eventEntity.Attribution,
				IsWritable = eventEntity.IsWritable
			};
			return new ServiceResult<GetBirthdayCalendarViewResponseMessage>(new GetBirthdayCalendarViewResponseMessage
			{
				BirthdayEvents = source.ToArray<Microsoft.Exchange.Services.Wcf.Types.BirthdayEvent>()
			});
		}
	}
}
