using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000091 RID: 145
	internal class ImportJob : MergeJob
	{
		// Token: 0x06000744 RID: 1860 RVA: 0x000319F0 File Offset: 0x0002FBF0
		public override void Initialize(TransactionalRequestJob importRequest)
		{
			base.Initialize(importRequest);
			base.RequestJobIdentity = importRequest.Identity.ToString();
			Guid targetExchangeGuid = importRequest.TargetExchangeGuid;
			string orgID = (importRequest.OrganizationId != null && importRequest.OrganizationId.OrganizationalUnit != null) ? (importRequest.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			LocalizedString sourceTracingID = MrsStrings.PstTracingId(importRequest.FilePath);
			LocalizedString targetTracingID = importRequest.TargetIsArchive ? MrsStrings.ArchiveMailboxTracingId(orgID, targetExchangeGuid) : MrsStrings.PrimaryMailboxTracingId(orgID, targetExchangeGuid);
			MailboxCopierFlags flags = MailboxCopierFlags.Merge | MailboxCopierFlags.SourceIsPST;
			base.MailboxMerger = new MailboxMerger(Guid.Empty, targetExchangeGuid, importRequest, this, flags, sourceTracingID, targetTracingID);
		}
	}
}
