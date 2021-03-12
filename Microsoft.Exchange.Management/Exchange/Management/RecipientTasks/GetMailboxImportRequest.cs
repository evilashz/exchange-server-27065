using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C92 RID: 3218
	[Cmdlet("Get", "MailboxImportRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxImportRequest : GetRequest<MailboxImportRequestIdParameter, MailboxImportRequest>
	{
		// Token: 0x17002655 RID: 9813
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x001FAEB0 File Offset: 0x001F90B0
		// (set) Token: 0x06007BB0 RID: 31664 RVA: 0x001FAEB8 File Offset: 0x001F90B8
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
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

		// Token: 0x17002656 RID: 9814
		// (get) Token: 0x06007BB1 RID: 31665 RVA: 0x001FAEC1 File Offset: 0x001F90C1
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.MailboxImport;
			}
		}

		// Token: 0x06007BB2 RID: 31666 RVA: 0x001FAEC4 File Offset: 0x001F90C4
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
