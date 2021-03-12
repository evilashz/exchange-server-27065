using System;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200002B RID: 43
	internal interface IIndexStatusStore
	{
		// Token: 0x060000E5 RID: 229
		IndexStatus SetIndexStatus(Guid databaseGuid, int mailboxToCrawl, VersionInfo version);

		// Token: 0x060000E6 RID: 230
		IndexStatus SetIndexStatus(Guid databaseGuid, ContentIndexStatusType indexingState, IndexStatusErrorCode errorCode, VersionInfo version, string seedingSource);

		// Token: 0x060000E7 RID: 231
		IndexStatus GetIndexStatus(Guid databaseGuid);

		// Token: 0x060000E8 RID: 232
		void UpdateIndexStatus(Guid databaseGuid, IndexStatusIndex indexStatusIndex, long value);
	}
}
