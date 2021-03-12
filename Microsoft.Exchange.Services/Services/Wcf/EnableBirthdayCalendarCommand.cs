using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000979 RID: 2425
	internal sealed class EnableBirthdayCalendarCommand : ServiceCommand<CalendarActionFolderIdResponse>
	{
		// Token: 0x0600458C RID: 17804 RVA: 0x000F4205 File Offset: 0x000F2405
		public EnableBirthdayCalendarCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000F4210 File Offset: 0x000F2410
		protected override CalendarActionFolderIdResponse InternalExecute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.MailboxIdentityMailboxSession;
			if (VariantConfiguration.GetSnapshot(base.CallContext.AccessingADUser.GetContext(null), null, null).OwaServer.XOWABirthdayAssistant.Enabled)
			{
				BirthdayCalendar.EnableBirthdayCalendar(mailboxIdentityMailboxSession);
				StoreObjectId birthdayCalendarFolderId = BirthdayCalendar.GetBirthdayCalendarFolderId(mailboxIdentityMailboxSession);
				FolderId folderId = IdConverter.ConvertStoreFolderIdToFolderId(birthdayCalendarFolderId, mailboxIdentityMailboxSession);
				using (CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.BindFromCalendarFolder(mailboxIdentityMailboxSession, birthdayCalendarFolderId))
				{
					Microsoft.Exchange.Services.Core.Types.ItemId calendarEntryId = IdConverter.ConvertStoreItemIdToItemId(calendarGroupEntry.CalendarGroupEntryId, mailboxIdentityMailboxSession);
					return new CalendarActionFolderIdResponse(folderId, calendarEntryId);
				}
			}
			return new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionNotInBirthdayCalendarFlight);
		}
	}
}
