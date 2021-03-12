using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB6 RID: 3254
	[Cmdlet("Get", "PublicFolderMailboxMigrationRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderMailboxMigrationRequest : GetRequest<PublicFolderMailboxMigrationRequestIdParameter, PublicFolderMailboxMigrationRequest>
	{
		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x06007CD7 RID: 31959 RVA: 0x001FF0F3 File Offset: 0x001FD2F3
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.PublicFolderMailboxMigration;
			}
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x001FF0F8 File Offset: 0x001FD2F8
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

		// Token: 0x04003DA0 RID: 15776
		private const string TaskNoun = "PublicFolderMailboxMigrationRequest";
	}
}
