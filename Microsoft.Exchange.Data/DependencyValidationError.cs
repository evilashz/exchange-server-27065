using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200021C RID: 540
	public class DependencyValidationError : ValidationError
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x00039C9B File Offset: 0x00037E9B
		public DependencyValidationError(string feature, bool featureValue, string dependencyFeatureName, bool dependencyFeatureValue) : base(DataStrings.DependencyCheckFailed(feature, featureValue.ToString(), dependencyFeatureName, dependencyFeatureValue.ToString()))
		{
		}
	}
}
