using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200094D RID: 2381
	internal sealed class DeleteCalendarCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x060044BF RID: 17599 RVA: 0x000EDDC0 File Offset: 0x000EBFC0
		public DeleteCalendarCommand(CallContext callContext, Microsoft.Exchange.Services.Core.Types.ItemId itemId) : base(callContext)
		{
			this.itemId = itemId;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000EDDD0 File Offset: 0x000EBFD0
		protected override CalendarActionResponse InternalExecute()
		{
			if (this.itemId == null || string.IsNullOrEmpty(this.itemId.Id) || string.IsNullOrEmpty(this.itemId.ChangeKey))
			{
				ExTraceGlobals.DeleteCalendarCallTracer.TraceError<string, string>((long)this.GetHashCode(), "FolderId provided is invalid. FolderId.Id: '{0}' FolderId.ChangeKey: '{1}'", (this.itemId == null || this.itemId.Id == null) ? "is null" : this.itemId.Id, (this.itemId == null || this.itemId.ChangeKey == null) ? "is null" : this.itemId.ChangeKey);
				return new CalendarActionResponse(CalendarActionError.CalendarActionInvalidItemId);
			}
			CalendarActionResponse result;
			try
			{
				IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(this.itemId);
				result = new DeleteCalendar(base.MailboxIdentityMailboxSession, idAndSession.Id).Execute();
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.DeleteCalendarCallTracer.TraceError((long)this.GetHashCode(), "StoragePermanentException thrown while trying to delete calendar. ItemId.Id: {0}, ItemId.ChangeKey: {1} ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					this.itemId.Id,
					this.itemId.ChangeKey,
					ex.Message,
					ex.StackTrace
				});
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionCannotDeleteCalendar);
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.DeleteCalendarCallTracer.TraceError((long)this.GetHashCode(), "StorageTransientException thrown while trying to delete calendar. ItemId.Id: {0}, ItemId.ChangeKey: {1} ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					this.itemId.Id,
					this.itemId.ChangeKey,
					ex2.Message,
					ex2.StackTrace
				});
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionCannotDeleteCalendar);
			}
			return result;
		}

		// Token: 0x04002813 RID: 10259
		private readonly Microsoft.Exchange.Services.Core.Types.ItemId itemId;
	}
}
