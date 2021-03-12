using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000092 RID: 146
	internal class RestoreJob : MergeJob
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x00031AA4 File Offset: 0x0002FCA4
		public override void Initialize(TransactionalRequestJob restoreRequest)
		{
			base.Initialize(restoreRequest);
			base.RequestJobIdentity = restoreRequest.Identity.ToString();
			Guid targetExchangeGuid = restoreRequest.TargetExchangeGuid;
			Guid sourceExchangeGuid = restoreRequest.SourceExchangeGuid;
			string orgID = (restoreRequest.OrganizationId != null && restoreRequest.OrganizationId.OrganizationalUnit != null) ? (restoreRequest.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			string dbName = (restoreRequest.SourceDatabase != null) ? restoreRequest.SourceDatabase.Name : ((restoreRequest.RemoteDatabaseGuid != null) ? restoreRequest.RemoteDatabaseGuid.Value.ToString() : string.Empty);
			LocalizedString sourceTracingID = MrsStrings.RestoreMailboxTracingId(dbName, restoreRequest.SourceExchangeGuid);
			LocalizedString targetTracingID = restoreRequest.TargetIsArchive ? MrsStrings.ArchiveMailboxTracingId(orgID, targetExchangeGuid) : MrsStrings.PrimaryMailboxTracingId(orgID, targetExchangeGuid);
			MailboxCopierFlags flags = MailboxCopierFlags.Merge;
			base.MailboxMerger = new MailboxMerger(sourceExchangeGuid, targetExchangeGuid, restoreRequest, this, flags, sourceTracingID, targetTracingID);
		}
	}
}
