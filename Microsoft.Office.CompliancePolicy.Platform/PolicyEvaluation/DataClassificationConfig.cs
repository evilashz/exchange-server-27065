using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.Classification;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000CC RID: 204
	internal sealed class DataClassificationConfig
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		internal DataClassificationConfig(List<KeyValuePair<string, string>> config, IClassificationRuleStore classificationConfig = null)
		{
			this.Id = Guid.Empty;
			this.MinCount = DataClassificationConfig.MinAllowedCount;
			this.MaxCount = DataClassificationConfig.IgnoreMaxCount;
			this.MinConfidence = DataClassificationConfig.UseRecommendedMinConfidence;
			this.MaxConfidence = DataClassificationConfig.MaxAllowedConfidence;
			for (int i = 0; i < config.Count; i++)
			{
				KeyValuePair<string, string> keyValuePair = config[i];
				if (string.Compare(keyValuePair.Key, DataClassificationConfig.IdKey, true) == 0)
				{
					if (classificationConfig != null)
					{
						try
						{
							RuleDefinitionDetails ruleDetails = classificationConfig.GetRuleDetails(keyValuePair.Value, null);
							this.Id = ruleDetails.RuleId;
							goto IL_174;
						}
						catch (ClassificationRuleStorePermanentException innerException)
						{
							throw new CompliancePolicyValidationException(string.Format("invalid {0} for data classification!", DataClassificationConfig.IdKey), innerException);
						}
					}
					Guid id;
					if (Guid.TryParse(keyValuePair.Value, out id))
					{
						this.Id = id;
					}
				}
				else if (string.Compare(keyValuePair.Key, DataClassificationConfig.MinCountKey, true) == 0)
				{
					this.MinCount = Convert.ToInt32(keyValuePair.Value);
				}
				else if (string.Compare(keyValuePair.Key, DataClassificationConfig.MaxCountKey, true) == 0)
				{
					this.MaxCount = Convert.ToInt32(keyValuePair.Value);
				}
				else if (string.Compare(keyValuePair.Key, DataClassificationConfig.MinConfidenceKey, true) == 0)
				{
					this.MinConfidence = Convert.ToInt32(keyValuePair.Value);
				}
				else
				{
					if (string.Compare(keyValuePair.Key, DataClassificationConfig.MaxConfidenceKey, true) != 0)
					{
						throw new CompliancePolicyValidationException(keyValuePair.Key + " is not supported by data classification!");
					}
					this.MaxConfidence = Convert.ToInt32(keyValuePair.Value);
				}
				IL_174:;
			}
			if (this.Id == Guid.Empty)
			{
				throw new CompliancePolicyValidationException(string.Format("invalid {0} for data classification!", DataClassificationConfig.IdKey));
			}
			if (this.MinCount < DataClassificationConfig.MinAllowedCount)
			{
				throw new CompliancePolicyValidationException(string.Format("invalid {0} for data classification!", DataClassificationConfig.MinCountKey));
			}
			if (this.MaxCount != DataClassificationConfig.IgnoreMaxCount && this.MaxCount < this.MinCount)
			{
				throw new CompliancePolicyValidationException(string.Format("invalid {0} for data classification!", DataClassificationConfig.MaxCountKey));
			}
			if (this.MinConfidence != DataClassificationConfig.UseRecommendedMinConfidence && this.MinConfidence < DataClassificationConfig.MinAllowedConfidence)
			{
				throw new CompliancePolicyValidationException(string.Format("invalid {0} for data classification!", DataClassificationConfig.MinConfidenceKey));
			}
			if (this.MaxConfidence < DataClassificationConfig.MinAllowedConfidence || this.MaxConfidence < this.MinConfidence || this.MaxConfidence > DataClassificationConfig.MaxAllowedConfidence)
			{
				throw new CompliancePolicyValidationException(string.Format("invalid {0} for data classification!", DataClassificationConfig.MaxConfidenceKey));
			}
			config.Clear();
			config.Add(new KeyValuePair<string, string>(DataClassificationConfig.IdKey, this.Id.ToString()));
			config.Add(new KeyValuePair<string, string>(DataClassificationConfig.MinCountKey, this.MinCount.ToString()));
			config.Add(new KeyValuePair<string, string>(DataClassificationConfig.MaxCountKey, this.MaxCount.ToString()));
			config.Add(new KeyValuePair<string, string>(DataClassificationConfig.MinConfidenceKey, this.MinConfidence.ToString()));
			config.Add(new KeyValuePair<string, string>(DataClassificationConfig.MaxConfidenceKey, this.MaxConfidence.ToString()));
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000101D0 File Offset: 0x0000E3D0
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x000101D8 File Offset: 0x0000E3D8
		internal Guid Id { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000101E1 File Offset: 0x0000E3E1
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x000101E9 File Offset: 0x0000E3E9
		internal int MinCount { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000101F2 File Offset: 0x0000E3F2
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x000101FA File Offset: 0x0000E3FA
		internal int MaxCount { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00010203 File Offset: 0x0000E403
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001020B File Offset: 0x0000E40B
		internal int MinConfidence { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00010214 File Offset: 0x0000E414
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001021C File Offset: 0x0000E41C
		internal int MaxConfidence { get; private set; }

		// Token: 0x06000541 RID: 1345 RVA: 0x00010228 File Offset: 0x0000E428
		internal bool Matches(ClassificationResult classificationResult, IClassificationRuleStore classificationStore)
		{
			int num = this.MinConfidence;
			if (num == DataClassificationConfig.UseRecommendedMinConfidence)
			{
				RuleDefinitionDetails ruleDetails = classificationStore.GetRuleDetails(this.Id.ToString(), null);
				num = ruleDetails.RecommendedConfidence;
			}
			return this.Id == classificationResult.ClassificationId && (this.MaxCount == DataClassificationConfig.IgnoreMaxCount || this.MaxCount >= classificationResult.Count) && this.MinCount <= classificationResult.Count && this.MaxConfidence >= classificationResult.Confidence && num <= classificationResult.Confidence;
		}

		// Token: 0x04000322 RID: 802
		internal static readonly int MinAllowedCount = 1;

		// Token: 0x04000323 RID: 803
		internal static readonly int IgnoreMaxCount = -1;

		// Token: 0x04000324 RID: 804
		internal static readonly int UseRecommendedMinConfidence = -1;

		// Token: 0x04000325 RID: 805
		internal static readonly int MinAllowedConfidence = 1;

		// Token: 0x04000326 RID: 806
		internal static readonly int MaxAllowedConfidence = 100;

		// Token: 0x04000327 RID: 807
		internal static readonly string IdKey = "id";

		// Token: 0x04000328 RID: 808
		internal static readonly string MinCountKey = "minCount";

		// Token: 0x04000329 RID: 809
		internal static readonly string MaxCountKey = "maxCount";

		// Token: 0x0400032A RID: 810
		internal static readonly string MinConfidenceKey = "minConfidence";

		// Token: 0x0400032B RID: 811
		internal static readonly string MaxConfidenceKey = "maxConfidence";
	}
}
