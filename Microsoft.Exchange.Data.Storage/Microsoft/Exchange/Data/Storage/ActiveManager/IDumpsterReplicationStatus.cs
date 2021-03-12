using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x0200031D RID: 797
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDumpsterReplicationStatus
	{
		// Token: 0x060023AD RID: 9133
		Guid? GetDatabaseGuidFromDN(string distinguishedName);

		// Token: 0x060023AE RID: 9134
		ConstraintCheckResultType CheckReplicationHealthConstraint(Guid databaseGuid, out TimeSpan waitTime, out LocalizedString failureReason, out ConstraintCheckAgent agent);

		// Token: 0x060023AF RID: 9135
		ConstraintCheckResultType CheckReplicationFlushed(Guid databaseGuid, DateTime utcCommitTime, out LocalizedString failureReason);
	}
}
