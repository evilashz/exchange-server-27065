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
	// Token: 0x0200094C RID: 2380
	internal sealed class CreateCalendarGroupCommand : ServiceCommand<CalendarActionGroupIdResponse>
	{
		// Token: 0x060044BD RID: 17597 RVA: 0x000EDC5C File Offset: 0x000EBE5C
		public CreateCalendarGroupCommand(CallContext callContext, string newGroupName) : base(callContext)
		{
			this.newGroupName = newGroupName;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000EDC6C File Offset: 0x000EBE6C
		protected override CalendarActionGroupIdResponse InternalExecute()
		{
			CalendarActionGroupIdResponse result;
			try
			{
				EntitiesHelper entitiesHelper = new EntitiesHelper(base.CallContext);
				ICalendarGroups calendarGroups = entitiesHelper.GetCalendarGroups(base.MailboxIdentityMailboxSession);
				Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup input = new Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup
				{
					Name = this.newGroupName
				};
				Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup calendarGroup = entitiesHelper.Execute<Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup, Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup>(new Func<Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup, CommandContext, Microsoft.Exchange.Entities.DataModel.Calendaring.CalendarGroup>(calendarGroups.Create), base.MailboxIdentityMailboxSession, BasicTypes.Item, input);
				result = new CalendarActionGroupIdResponse(calendarGroup.ClassId, new Microsoft.Exchange.Services.Core.Types.ItemId(calendarGroup.Id, calendarGroup.ChangeKey));
			}
			catch (InvalidCalendarGroupNameException)
			{
				ExTraceGlobals.CreateCalendarGroupCallTracer.TraceError<string>((long)this.GetHashCode(), "New groupname is null or empty: '{0}'", this.newGroupName ?? "is null");
				result = new CalendarActionGroupIdResponse(CalendarActionError.CalendarActionInvalidGroupName);
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.CreateCalendarGroupCallTracer.TraceError<string, string, string>((long)this.GetHashCode(), "StoragePermanentException thrown while trying to create calendar group. GroupName: {0}. ExceptionInfo: {1}. CallStack: {2}", this.newGroupName ?? "is null", ex.Message, ex.StackTrace);
				result = new CalendarActionGroupIdResponse(CalendarActionError.CalendarActionCannotCreateGroup);
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.CreateCalendarGroupCallTracer.TraceError<string, string, string>((long)this.GetHashCode(), "StorageTransientException thrown while trying to create calendar group. GroupName: {0}. ExceptionInfo: {1}. CallStack: {2}", this.newGroupName ?? "is null", ex2.Message, ex2.StackTrace);
				result = new CalendarActionGroupIdResponse(CalendarActionError.CalendarActionCannotCreateGroup);
			}
			return result;
		}

		// Token: 0x04002812 RID: 10258
		private readonly string newGroupName;
	}
}
