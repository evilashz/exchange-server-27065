using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000911 RID: 2321
	internal sealed class DeletePlaceCommand : ServiceCommand<ServiceResultNone>
	{
		// Token: 0x06004343 RID: 17219 RVA: 0x000E198E File Offset: 0x000DFB8E
		public DeletePlaceCommand(CallContext callContext, DeletePlaceRequest request) : base(callContext)
		{
			this.request = request;
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000E19A0 File Offset: 0x000DFBA0
		protected override ServiceResultNone InternalExecute()
		{
			if (!string.IsNullOrEmpty(this.request.Id))
			{
				MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
				ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ContactSchema.WorkLocationUri, this.request.Id);
				using (Folder folder = Folder.Bind(mailboxIdentityMailboxSession, mailboxIdentityMailboxSession.GetDefaultFolderId(DefaultFolderType.Location)))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, new PropertyDefinition[]
					{
						ItemSchema.Id
					}))
					{
						object[][] rows = queryResult.GetRows(1);
						if (rows.Length == 1)
						{
							VersionedId versionedId = rows[0][0] as VersionedId;
							folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
							{
								versionedId
							});
						}
					}
				}
			}
			return new ServiceResultNone();
		}

		// Token: 0x0400272D RID: 10029
		private readonly DeletePlaceRequest request;
	}
}
