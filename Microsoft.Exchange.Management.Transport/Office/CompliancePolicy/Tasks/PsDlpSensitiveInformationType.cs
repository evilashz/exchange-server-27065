using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Classification;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public sealed class PsDlpSensitiveInformationType
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x000265DC File Offset: 0x000247DC
		public PsDlpSensitiveInformationType(RuleDefinitionDetails ruleDefinitionDetails)
		{
			ArgumentValidator.ThrowIfNull("ruleDefinitionDetails", ruleDefinitionDetails);
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<KeyValuePair<string, CLASSIFICATION_DEFINITION_DETAILS>>("LocalizableDetails", ruleDefinitionDetails.LocalizableDetails);
			this.Id = ruleDefinitionDetails.RuleId;
			this.Name = ruleDefinitionDetails.LocalizableDetails.Values.First<CLASSIFICATION_DEFINITION_DETAILS>().DefinitionName;
			this.Description = ruleDefinitionDetails.LocalizableDetails.Values.First<CLASSIFICATION_DEFINITION_DETAILS>().Description;
			this.Publisher = ruleDefinitionDetails.LocalizableDetails.Values.First<CLASSIFICATION_DEFINITION_DETAILS>().PublisherName;
			this.RecommendedConfidence = ruleDefinitionDetails.RecommendedConfidence;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00026673 File Offset: 0x00024873
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0002667B File Offset: 0x0002487B
		public Guid Id { get; private set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00026684 File Offset: 0x00024884
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0002668C File Offset: 0x0002488C
		public string Name { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00026695 File Offset: 0x00024895
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0002669D File Offset: 0x0002489D
		public string Description { get; private set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x000266A6 File Offset: 0x000248A6
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x000266AE File Offset: 0x000248AE
		public int RecommendedConfidence { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x000266B7 File Offset: 0x000248B7
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x000266BF File Offset: 0x000248BF
		public string Publisher { get; private set; }

		// Token: 0x040003F4 RID: 1012
		internal const string DefaultLocale = "en-us";
	}
}
