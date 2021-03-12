using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x0200006A RID: 106
	public abstract class DarTaskAggregateProvider
	{
		// Token: 0x060002F6 RID: 758
		public abstract DarTaskAggregate Find(string scopeId, string taskType);

		// Token: 0x060002F7 RID: 759
		public abstract IEnumerable<DarTaskAggregate> FindAll(string scopeId);

		// Token: 0x060002F8 RID: 760
		public abstract void Save(DarTaskAggregate taskAggregate);

		// Token: 0x060002F9 RID: 761
		public abstract void Delete(string scopeId, string id);
	}
}
