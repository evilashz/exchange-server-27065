using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Ranking
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationFeature : IRankingFeature
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020D0 File Offset: 0x000002D0
		internal ConversationFeature(IList<PropertyDefinition> supportingProperties, FeatureValueCalculator calculator)
		{
			this.supportingProperties = supportingProperties;
			this.calculator = calculator;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E6 File Offset: 0x000002E6
		public IList<PropertyDefinition> SupportingProperties
		{
			get
			{
				return this.supportingProperties;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020EE File Offset: 0x000002EE
		public double FeatureValue(object conversation)
		{
			return this.calculator((IStorePropertyBag)conversation);
		}

		// Token: 0x04000001 RID: 1
		private readonly IList<PropertyDefinition> supportingProperties;

		// Token: 0x04000002 RID: 2
		private readonly FeatureValueCalculator calculator;
	}
}
