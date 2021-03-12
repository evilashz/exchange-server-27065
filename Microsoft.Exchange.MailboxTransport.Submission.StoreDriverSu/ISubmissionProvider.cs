using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000026 RID: 38
	internal interface ISubmissionProvider
	{
		// Token: 0x06000194 RID: 404
		MailSubmissionResult SubmitMessage(string serverDN, Guid mailboxGuid, Guid mdbGuid, string databaseName, long eventCounter, byte[] entryId, byte[] parentEntryId, string serverFqdn, IPAddress networkAddressBytes, DateTime originalCreateTime, bool isPublicFolder, TenantPartitionHint tenantHint, string mailboxHopLatency, QuarantineHandler quarantineHandler, SubmissionPoisonHandler submissionPoisonHandler, LatencyTracker latencyTracker);
	}
}
