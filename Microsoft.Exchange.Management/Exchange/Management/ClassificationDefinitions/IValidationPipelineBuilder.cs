using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200086B RID: 2155
	internal interface IValidationPipelineBuilder
	{
		// Token: 0x06004A75 RID: 19061
		void BuildCoreValidators();

		// Token: 0x06004A76 RID: 19062
		void BuildSupplementaryValidators();

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06004A77 RID: 19063
		IEnumerable<IClassificationRuleCollectionValidator> Result { get; }
	}
}
