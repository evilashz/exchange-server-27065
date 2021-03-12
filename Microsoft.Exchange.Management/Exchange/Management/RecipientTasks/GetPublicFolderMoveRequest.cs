using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA6 RID: 3238
	[Cmdlet("Get", "PublicFolderMoveRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderMoveRequest : GetRequest<PublicFolderMoveRequestIdParameter, PublicFolderMoveRequest>
	{
		// Token: 0x17002689 RID: 9865
		// (get) Token: 0x06007C4B RID: 31819 RVA: 0x001FD2C7 File Offset: 0x001FB4C7
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.PublicFolderMove;
			}
		}

		// Token: 0x06007C4C RID: 31820 RVA: 0x001FD2CC File Offset: 0x001FB4CC
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

		// Token: 0x06007C4D RID: 31821 RVA: 0x001FD302 File Offset: 0x001FB502
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			TaskLogger.LogExit();
		}
	}
}
