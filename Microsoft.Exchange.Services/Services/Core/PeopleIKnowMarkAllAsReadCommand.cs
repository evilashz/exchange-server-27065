using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200034E RID: 846
	internal sealed class PeopleIKnowMarkAllAsReadCommand : MarkAllItemsAsRead
	{
		// Token: 0x060017D0 RID: 6096 RVA: 0x0007FC1B File Offset: 0x0007DE1B
		public PeopleIKnowMarkAllAsReadCommand(CallContext callContext, MarkAllItemsAsReadRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0007FC25 File Offset: 0x0007DE25
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			this.fromFilter = base.Request.FromFilter;
			ServiceCommandBase.ThrowIfNullOrEmpty(this.fromFilter, "fromFilter", "PeopleIKnowMarkAllAsReadCommand::PreExecuteCommand");
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0007FC54 File Offset: 0x0007DE54
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.folderIds[0]);
			StoreId defaultFolderId = idAndSession.Session.GetDefaultFolderId(DefaultFolderType.FromFavoriteSenders);
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "PeopleIKnowMarkAllAsReadCommand.Execute calling PeopleIKnowMarkAllAsRead with fromFilter: {0}", this.fromFilter);
			new PeopleIKnowMarkAllAsRead(mailboxSession, defaultFolderId, this.fromFilter, this.supressReadReceipts, this.tracer).Execute();
			this.objectsChanged++;
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x04001003 RID: 4099
		private string fromFilter;
	}
}
