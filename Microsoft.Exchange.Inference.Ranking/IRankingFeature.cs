using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Inference.Ranking
{
	// Token: 0x02000003 RID: 3
	public interface IRankingFeature
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5
		IList<PropertyDefinition> SupportingProperties { get; }

		// Token: 0x06000006 RID: 6
		double FeatureValue(object item);
	}
}
