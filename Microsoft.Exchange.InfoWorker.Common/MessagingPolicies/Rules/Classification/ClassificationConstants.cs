using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Classification
{
	// Token: 0x0200015F RID: 351
	internal static class ClassificationConstants
	{
		// Token: 0x04000757 RID: 1879
		public const string ADCollectionName = "ClassificationDefinitions";

		// Token: 0x04000758 RID: 1880
		public const string ADPackagePartName = "config";

		// Token: 0x04000759 RID: 1881
		public const string VersionElementName = "Version";

		// Token: 0x0400075A RID: 1882
		public static readonly List<string> DataClassificationElementNames = new List<string>
		{
			"Entity",
			"Affinity"
		};
	}
}
