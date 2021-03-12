using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200002C RID: 44
	public sealed class RuleDefinitionDetails
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003191 File Offset: 0x00001391
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003199 File Offset: 0x00001399
		public int RecommendedConfidence
		{
			get
			{
				return this.recommendedConfidence;
			}
			set
			{
				ArgumentValidator.ThrowIfZeroOrNegative("RecommendedConfidence", value);
				this.recommendedConfidence = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000031AD File Offset: 0x000013AD
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000031B5 File Offset: 0x000013B5
		public Guid RuleId { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000031BE File Offset: 0x000013BE
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000031C6 File Offset: 0x000013C6
		public IDictionary<string, CLASSIFICATION_DEFINITION_DETAILS> LocalizableDetails { get; set; }

		// Token: 0x06000099 RID: 153 RVA: 0x000031CF File Offset: 0x000013CF
		internal RuleDefinitionDetails Clone()
		{
			return (RuleDefinitionDetails)base.MemberwiseClone();
		}

		// Token: 0x04000050 RID: 80
		private int recommendedConfidence;
	}
}
