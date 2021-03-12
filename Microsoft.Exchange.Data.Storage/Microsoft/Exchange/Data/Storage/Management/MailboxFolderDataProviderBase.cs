using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A62 RID: 2658
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxFolderDataProviderBase : XsoMailboxDataProviderBase
	{
		// Token: 0x06006111 RID: 24849 RVA: 0x00199584 File Offset: 0x00197784
		public MailboxFolderDataProviderBase(ADSessionSettings adSessionSettings, ADUser mailboxOwner, ISecurityAccessToken userToken, string action) : base(adSessionSettings, mailboxOwner, userToken, action)
		{
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x00199591 File Offset: 0x00197791
		public MailboxFolderDataProviderBase(ADSessionSettings adSessionSettings, ADUser mailboxOwner, string action) : base(adSessionSettings, mailboxOwner, action)
		{
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x0019959C File Offset: 0x0019779C
		internal MailboxFolderDataProviderBase()
		{
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x001995A4 File Offset: 0x001977A4
		public StoreObjectId ResolveStoreObjectIdFromFolderPath(MapiFolderPath folderPath)
		{
			Util.ThrowOnNullArgument(folderPath, "folderPath");
			StoreObjectId storeObjectId;
			if (folderPath.IsNonIpmPath)
			{
				storeObjectId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration);
			}
			else
			{
				storeObjectId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			}
			if (folderPath.Depth <= 0)
			{
				return storeObjectId;
			}
			foreach (string propertyValue in folderPath)
			{
				QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, propertyValue);
				using (Folder folder = Folder.Bind(base.MailboxSession, storeObjectId, MailboxFolderDataProviderBase.FolderQueryReturnColumns))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, MailboxFolderDataProviderBase.FolderQuerySorts, MailboxFolderDataProviderBase.FolderQueryReturnColumns))
					{
						if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
						{
							return null;
						}
						object[][] rows = queryResult.GetRows(1);
						storeObjectId = ((VersionedId)rows[0][0]).ObjectId;
					}
				}
			}
			return storeObjectId;
		}

		// Token: 0x04003716 RID: 14102
		private static readonly PropertyDefinition[] FolderQueryReturnColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName
		};

		// Token: 0x04003717 RID: 14103
		private static readonly SortBy[] FolderQuerySorts = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x02000A63 RID: 2659
		private enum FolderQueryReturnColumnIndex
		{
			// Token: 0x04003719 RID: 14105
			Id,
			// Token: 0x0400371A RID: 14106
			DisplayName
		}
	}
}
