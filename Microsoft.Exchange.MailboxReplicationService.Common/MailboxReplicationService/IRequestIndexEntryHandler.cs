using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D6 RID: 470
	internal interface IRequestIndexEntryHandler
	{
		// Token: 0x06001365 RID: 4965
		IRequestIndexEntry CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, RequestIndexId requestIndexId);

		// Token: 0x06001366 RID: 4966
		IRequestIndexEntry CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, IConfigurationSession session);

		// Token: 0x06001367 RID: 4967
		void Delete(RequestIndexEntryProvider requestIndexEntryProvider, IRequestIndexEntry instance);

		// Token: 0x06001368 RID: 4968
		IRequestIndexEntry[] Find(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy);

		// Token: 0x06001369 RID: 4969
		IEnumerable<IRequestIndexEntry> FindPaged(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize);

		// Token: 0x0600136A RID: 4970
		IRequestIndexEntry Read(RequestIndexEntryProvider requestIndexEntryProvider, RequestIndexEntryObjectId identity);

		// Token: 0x0600136B RID: 4971
		void Save(RequestIndexEntryProvider requestIndexEntryProvider, IRequestIndexEntry instance);
	}
}
