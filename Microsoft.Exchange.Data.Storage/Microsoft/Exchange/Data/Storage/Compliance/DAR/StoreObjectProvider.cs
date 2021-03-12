using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x02000460 RID: 1120
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StoreObjectProvider : TenantStoreDataProvider
	{
		// Token: 0x060031F4 RID: 12788 RVA: 0x000CCD67 File Offset: 0x000CAF67
		public StoreObjectProvider(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000CCD70 File Offset: 0x000CAF70
		public IEnumerable<T> FindPaged<T>(SearchFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize, params ProviderPropertyDefinition[] properties) where T : IConfigurable, new()
		{
			FolderId rootId2 = null;
			if (rootId != null)
			{
				rootId2 = new FolderId(((EwsStoreObjectId)rootId).EwsObjectId.UniqueId);
			}
			return this.InternalFindPaged<T>(filter, rootId2, deepSearch, (sortBy == null) ? null : new SortBy[]
			{
				sortBy
			}, pageSize, properties);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000CCDBC File Offset: 0x000CAFBC
		protected override FolderId GetDefaultFolder()
		{
			if (this.containerFolderId == null)
			{
				this.containerFolderId = base.GetOrCreateFolder("DarTasks", new FolderId(10, new Mailbox(base.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString()))).Id;
			}
			return this.containerFolderId;
		}

		// Token: 0x04001B02 RID: 6914
		public const string ContainerFolderName = "DarTasks";

		// Token: 0x04001B03 RID: 6915
		private FolderId containerFolderId;
	}
}
