using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C8B RID: 3211
	[Cmdlet("Get", "MailboxExportRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxExportRequest : GetRequest<MailboxExportRequestIdParameter, MailboxExportRequest>
	{
		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x06007B79 RID: 31609 RVA: 0x001FA648 File Offset: 0x001F8848
		// (set) Token: 0x06007B7A RID: 31610 RVA: 0x001FA650 File Offset: 0x001F8850
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		[ValidateNotNull]
		public MailboxOrMailUserIdParameter Mailbox
		{
			get
			{
				return base.InternalMailbox;
			}
			set
			{
				base.InternalMailbox = value;
			}
		}

		// Token: 0x17002642 RID: 9794
		// (get) Token: 0x06007B7B RID: 31611 RVA: 0x001FA659 File Offset: 0x001F8859
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.MailboxExport;
			}
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x001FA65C File Offset: 0x001F885C
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
