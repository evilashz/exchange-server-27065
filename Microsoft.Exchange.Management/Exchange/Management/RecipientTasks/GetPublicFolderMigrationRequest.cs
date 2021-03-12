using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CAD RID: 3245
	[Cmdlet("Get", "PublicFolderMigrationRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderMigrationRequest : GetRequest<PublicFolderMigrationRequestIdParameter, PublicFolderMigrationRequest>
	{
		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x06007C78 RID: 31864 RVA: 0x001FDB11 File Offset: 0x001FBD11
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.PublicFolderMigration;
			}
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x001FDB14 File Offset: 0x001FBD14
		protected override RequestIndexEntryQueryFilter InternalFilterBuilder()
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = base.InternalFilterBuilder();
			if (requestIndexEntryQueryFilter == null)
			{
				requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter();
			}
			requestIndexEntryQueryFilter.RequestType = this.RequestType;
			requestIndexEntryQueryFilter.IndexId = new RequestIndexId(RequestIndexLocation.AD);
			return requestIndexEntryQueryFilter;
		}
	}
}
