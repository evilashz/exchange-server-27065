using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Inference.Ranking
{
	// Token: 0x02000005 RID: 5
	public interface IRankingModel
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10
		HashSet<PropertyDefinition> Dependencies { get; }

		// Token: 0x0600000B RID: 11
		double Rank(object item);
	}
}
