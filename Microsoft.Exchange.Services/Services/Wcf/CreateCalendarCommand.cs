using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000949 RID: 2377
	internal sealed class CreateCalendarCommand : ServiceCommand<CalendarActionFolderIdResponse>
	{
		// Token: 0x060044B3 RID: 17587 RVA: 0x000ED4F0 File Offset: 0x000EB6F0
		public CreateCalendarCommand(CallContext callContext, string newCalendarName, string parentGroupGuid, string emailAddress) : base(callContext)
		{
			this.newCalendarName = newCalendarName;
			this.parentGroupGuid = parentGroupGuid;
			this.emailAddress = emailAddress;
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000ED510 File Offset: 0x000EB710
		protected override CalendarActionFolderIdResponse InternalExecute()
		{
			CalendarActionFolderIdResponse result;
			try
			{
				result = new CreateCalendar(base.MailboxIdentityMailboxSession, this.newCalendarName, this.parentGroupGuid, this.emailAddress, base.CallContext.ADRecipientSessionContext.GetADRecipientSession()).Execute();
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.CreateCalendarCallTracer.TraceError((long)this.GetHashCode(), "StoragePermanentException thrown while trying to create calendar with name: {0}. ParentGroupGuid: {1}, ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					(this.newCalendarName == null) ? "is null" : this.newCalendarName,
					(this.parentGroupGuid == null) ? "is null" : this.parentGroupGuid,
					ex.Message,
					ex.StackTrace
				});
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionUnableToCreateCalendarFolder);
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.CreateCalendarCallTracer.TraceError((long)this.GetHashCode(), "StorageTransientException thrown while trying to create calendar with name: {0}. ParentGroupGuid: {1}, ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					(this.newCalendarName == null) ? "is null" : this.newCalendarName,
					(this.parentGroupGuid == null) ? "is null" : this.parentGroupGuid,
					ex2.Message,
					ex2.StackTrace
				});
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionUnableToCreateCalendarFolder);
			}
			return result;
		}

		// Token: 0x0400280B RID: 10251
		private readonly string newCalendarName;

		// Token: 0x0400280C RID: 10252
		private readonly string parentGroupGuid;

		// Token: 0x0400280D RID: 10253
		private readonly string emailAddress;
	}
}
