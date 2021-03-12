using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008F RID: 143
	internal class ExportJob : MergeJob
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00031368 File Offset: 0x0002F568
		public override void Initialize(TransactionalRequestJob exportRequest)
		{
			base.Initialize(exportRequest);
			base.RequestJobIdentity = exportRequest.Identity.ToString();
			Guid sourceExchangeGuid = exportRequest.SourceExchangeGuid;
			string orgID = (exportRequest.OrganizationId != null && exportRequest.OrganizationId.OrganizationalUnit != null) ? (exportRequest.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			LocalizedString sourceTracingID = exportRequest.SourceIsArchive ? MrsStrings.ArchiveMailboxTracingId(orgID, sourceExchangeGuid) : MrsStrings.PrimaryMailboxTracingId(orgID, sourceExchangeGuid);
			LocalizedString targetTracingID = MrsStrings.PstTracingId(exportRequest.FilePath);
			MailboxCopierFlags flags = MailboxCopierFlags.Merge | MailboxCopierFlags.TargetIsPST;
			base.MailboxMerger = new MailboxMerger(sourceExchangeGuid, Guid.Empty, exportRequest, this, flags, sourceTracingID, targetTracingID);
		}
	}
}
