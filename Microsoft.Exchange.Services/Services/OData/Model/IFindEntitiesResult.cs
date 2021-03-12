using System;
using System.Collections;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC1 RID: 3777
	public interface IFindEntitiesResult : IEnumerable
	{
		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x0600622F RID: 25135
		int TotalCount { get; }
	}
}
