using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200019B RID: 411
	internal class DependencyEntry
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x00042EBC File Offset: 0x000410BC
		internal DependencyEntry(string featureName, string dependencyFeatureName, GetFeatureValue getFeatureValue, GetDependencyValue getDependencyValue, SetDependencyValue setDependencyValue)
		{
			if (featureName == null)
			{
				throw new ArgumentNullException("featureName");
			}
			if (dependencyFeatureName == null)
			{
				throw new ArgumentNullException("dependencyFeatureName");
			}
			if (getFeatureValue == null)
			{
				throw new ArgumentNullException("getFeatureValue");
			}
			if (getDependencyValue == null)
			{
				throw new ArgumentNullException("getDependencyValue");
			}
			if (setDependencyValue == null)
			{
				throw new ArgumentNullException("setDependencyValue");
			}
			this.FeatureName = featureName;
			this.DependencyFeatureName = dependencyFeatureName;
			this.GetFeatureValue = getFeatureValue;
			this.GetDependencyValue = getDependencyValue;
			this.SetDependencyValue = setDependencyValue;
		}

		// Token: 0x04000714 RID: 1812
		internal readonly string FeatureName;

		// Token: 0x04000715 RID: 1813
		internal readonly string DependencyFeatureName;

		// Token: 0x04000716 RID: 1814
		internal readonly GetFeatureValue GetFeatureValue;

		// Token: 0x04000717 RID: 1815
		internal readonly GetDependencyValue GetDependencyValue;

		// Token: 0x04000718 RID: 1816
		internal readonly SetDependencyValue SetDependencyValue;
	}
}
