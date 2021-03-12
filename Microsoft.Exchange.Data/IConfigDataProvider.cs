using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000223 RID: 547
	public interface IConfigDataProvider
	{
		// Token: 0x06001327 RID: 4903
		IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new();

		// Token: 0x06001328 RID: 4904
		IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new();

		// Token: 0x06001329 RID: 4905
		IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new();

		// Token: 0x0600132A RID: 4906
		void Save(IConfigurable instance);

		// Token: 0x0600132B RID: 4907
		void Delete(IConfigurable instance);

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600132C RID: 4908
		string Source { get; }
	}
}
