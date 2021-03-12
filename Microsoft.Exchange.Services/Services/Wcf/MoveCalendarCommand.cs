using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000960 RID: 2400
	internal sealed class MoveCalendarCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x06004516 RID: 17686 RVA: 0x000F0D00 File Offset: 0x000EEF00
		public MoveCalendarCommand(CallContext callContext, FolderId calendarToMove, string parentGroupId, FolderId calendarBefore) : base(callContext)
		{
			this.calendarToMove = calendarToMove;
			this.parentGroupId = parentGroupId;
			this.calendarBefore = calendarBefore;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000F0D20 File Offset: 0x000EEF20
		protected override CalendarActionResponse InternalExecute()
		{
			if (!this.VerifyFolderId(this.calendarToMove, "calendarToMove") || (this.calendarBefore != null && !this.VerifyFolderId(this.calendarBefore, "calendarBefore")))
			{
				return new CalendarActionResponse(CalendarActionError.CalendarActionInvalidItemId);
			}
			StoreObjectId asStoreObjectId = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.calendarToMove).GetAsStoreObjectId();
			StoreObjectId storeObjectId = (this.calendarBefore == null) ? null : base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.calendarBefore).GetAsStoreObjectId();
			return new MoveCalendar(base.MailboxIdentityMailboxSession, asStoreObjectId, this.parentGroupId, storeObjectId).Execute();
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000F0DB4 File Offset: 0x000EEFB4
		private bool VerifyFolderId(FolderId idToVerify, string folderIdName)
		{
			if (idToVerify == null || string.IsNullOrEmpty(idToVerify.Id) || string.IsNullOrEmpty(idToVerify.ChangeKey))
			{
				ExTraceGlobals.SetCalendarColorCallTracer.TraceError<string, string, string>((long)this.GetHashCode(), "Invalid calendar folderid ({0}) supplied. FolderId.Id: {1}, FolderId.ChangeKey: {2}", folderIdName, (idToVerify == null || idToVerify.Id == null) ? "is null" : idToVerify.Id, (idToVerify == null || idToVerify.ChangeKey == null) ? "is null" : idToVerify.ChangeKey);
				return false;
			}
			return true;
		}

		// Token: 0x04002831 RID: 10289
		private readonly FolderId calendarToMove;

		// Token: 0x04002832 RID: 10290
		private readonly string parentGroupId;

		// Token: 0x04002833 RID: 10291
		private readonly FolderId calendarBefore;
	}
}
