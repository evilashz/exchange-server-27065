using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000950 RID: 2384
	internal sealed class DeleteCalendarGroupCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x060044C7 RID: 17607 RVA: 0x000EE1B9 File Offset: 0x000EC3B9
		public DeleteCalendarGroupCommand(CallContext callContext, string groupItemId) : base(callContext)
		{
			this.groupItemId = groupItemId;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000EE1CC File Offset: 0x000EC3CC
		protected override CalendarActionResponse InternalExecute()
		{
			if (string.IsNullOrEmpty(this.groupItemId))
			{
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceError((long)this.GetHashCode(), "ItemId provided is null or empty.");
				return new CalendarActionGroupIdResponse(CalendarActionError.CalendarActionInvalidItemId);
			}
			CalendarActionResponse result;
			try
			{
				MailboxSession mailboxIdentityMailboxSession = base.MailboxIdentityMailboxSession;
				this.groupItemId = this.CheckBackWardCompatibilityAndConvertId(this.groupItemId, mailboxIdentityMailboxSession);
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Attempting delete the calendar group with item Id: '{0}'", this.groupItemId);
				EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
				ICalendarGroups calendarGroups = entitiesHelper.GetCalendarGroups(mailboxIdentityMailboxSession);
				entitiesHelper.Execute(new Action<string, CommandContext>(calendarGroups.Delete), mailboxIdentityMailboxSession, BasicTypes.Item, this.groupItemId, null);
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Deleted calendar group with item Id: '{0}'", this.groupItemId);
				result = new CalendarActionResponse();
			}
			catch (CannotDeleteSpecialCalendarGroupException ex)
			{
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceError((long)this.GetHashCode(), "Trying to delete special calendar group. GroupItemId: {0} GroupName: {1} ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					(this.groupItemId == null) ? "is null" : this.groupItemId,
					ex.GroupName,
					ex.Message,
					ex.StackTrace
				});
				result = new CalendarActionResponse(CalendarActionError.CalendarActionInvalidGroupTypeForDeletion);
			}
			catch (CalendarGroupIsNotEmptyException ex2)
			{
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceError((long)this.GetHashCode(), "Trying to delete calendar group having calendar(s). GroupItemId: {0} GroupName: {1} ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					(this.groupItemId == null) ? "is null" : this.groupItemId,
					ex2.GroupName,
					ex2.Message,
					ex2.StackTrace
				});
				result = new CalendarActionResponse(CalendarActionError.CalendarActionCannotDeleteGroupStillHasChildren);
			}
			catch (ObjectNotFoundException ex3)
			{
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceError<string, string, string>((long)this.GetHashCode(), "ObjectNotFoundException thrown while trying to delete calendar group. GroupItemId: {0} ExceptionInfo: {1}. CallStack: {2}", (this.groupItemId == null) ? "is null" : this.groupItemId, ex3.Message, ex3.StackTrace);
				result = new CalendarActionResponse();
			}
			catch (StoragePermanentException ex4)
			{
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceError<string, string, string>((long)this.GetHashCode(), "StoragePermanentException thrown while trying to delete calendar group. GroupItemId: {0} ExceptionInfo: {1}. CallStack: {2}", (this.groupItemId == null) ? "is null" : this.groupItemId, ex4.Message, ex4.StackTrace);
				result = new CalendarActionResponse(CalendarActionError.CalendarActionUnableToDeleteCalendarGroup);
			}
			catch (StorageTransientException ex5)
			{
				ExTraceGlobals.DeleteCalendarGroupCallTracer.TraceError<string, string, string>((long)this.GetHashCode(), "StorageTransientException thrown while trying to delete calendar group. GroupItemId: {0} ExceptionInfo: {1}. CallStack: {2}", (this.groupItemId == null) ? "is null" : this.groupItemId, ex5.Message, ex5.StackTrace);
				result = new CalendarActionResponse(CalendarActionError.CalendarActionUnableToDeleteCalendarGroup);
			}
			return result;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x000EE478 File Offset: 0x000EC678
		private string CheckBackWardCompatibilityAndConvertId(string id, MailboxSession session)
		{
			Guid groupClassId;
			if (Guid.TryParse(id, out groupClassId))
			{
				using (Microsoft.Exchange.Data.Storage.CalendarGroup calendarGroup = Microsoft.Exchange.Data.Storage.CalendarGroup.Bind(session, groupClassId))
				{
					Microsoft.Exchange.Services.Core.Types.ItemId itemId = IdConverter.ConvertStoreItemIdToItemId(calendarGroup.StoreObjectId, session);
					return itemId.Id;
				}
				return id;
			}
			return id;
		}

		// Token: 0x04002815 RID: 10261
		private string groupItemId;
	}
}
