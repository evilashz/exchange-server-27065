using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Classification;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000CA RID: 202
	public class ContentContainsSensitiveInformationPredicate : PredicateCondition
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000F840 File Offset: 0x0000DA40
		public ContentContainsSensitiveInformationPredicate(List<List<KeyValuePair<string, string>>> entries, IClassificationRuleStore classificationStore)
		{
			ArgumentValidator.ThrowIfNull("classificationStore", classificationStore);
			if (entries != null)
			{
				if (!entries.Any((List<KeyValuePair<string, string>> entry) => entry == null))
				{
					this.classificationStore = classificationStore;
					base.Property = new Property("Item.ClassificationDiscovered", typeof(IDictionary<Guid, ClassificationResult>));
					base.Value = this.BuildValue(entries);
					return;
				}
			}
			throw new ArgumentNullException("entries");
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000F8BF File Offset: 0x0000DABF
		internal ContentContainsSensitiveInformationPredicate(List<List<KeyValuePair<string, string>>> entries) : base(new Property("Item.ClassificationDiscovered", typeof(IDictionary<Guid, ClassificationResult>)), entries)
		{
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000F8DC File Offset: 0x0000DADC
		public override string Name
		{
			get
			{
				return "containsDataClassification";
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000F8E3 File Offset: 0x0000DAE3
		public override Version MinimumVersion
		{
			get
			{
				return ContentContainsSensitiveInformationPredicate.minVersion;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000F8EA File Offset: 0x0000DAEA
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000F8F2 File Offset: 0x0000DAF2
		internal IDictionary<Guid, DataClassificationConfig> ClassificationConfig { get; private set; }

		// Token: 0x06000529 RID: 1321 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			if (!(base.Property.Type == typeof(IDictionary<Guid, ClassificationResult>)))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' is in inconsitent state due to unknown property '{1}'", context.CurrentRule.Name, base.Property.Name));
			}
			IDictionary<Guid, ClassificationResult> dictionary = base.Property.GetValue(context) as IDictionary<Guid, ClassificationResult>;
			IDictionary<Guid, DataClassificationConfig> classificationConfig = this.ClassificationConfig;
			if (classificationConfig == null || !classificationConfig.Any<KeyValuePair<Guid, DataClassificationConfig>>())
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' contains an invalid property '{1}'", context.CurrentRule.Name, base.Property.Name));
			}
			ArgumentValidator.ThrowIfNull("context.ClassificationStore", context.ClassificationStore);
			if (dictionary != null && dictionary.Any<KeyValuePair<Guid, ClassificationResult>>())
			{
				foreach (Guid key in classificationConfig.Keys)
				{
					if (dictionary.ContainsKey(key) && dictionary[key] != null && classificationConfig[key].Matches(dictionary[key], context.ClassificationStore))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000FA28 File Offset: 0x0000DC28
		protected override Value BuildValue(List<List<KeyValuePair<string, string>>> entries)
		{
			if (!entries.Any<List<KeyValuePair<string, string>>>())
			{
				throw new CompliancePolicyValidationException("entries can not be empty for ContentContainsDataClassificationPredicate!");
			}
			IDictionary<Guid, DataClassificationConfig> dictionary = new Dictionary<Guid, DataClassificationConfig>();
			foreach (List<KeyValuePair<string, string>> config in entries)
			{
				DataClassificationConfig dataClassificationConfig = new DataClassificationConfig(config, this.classificationStore);
				if (dictionary.ContainsKey(dataClassificationConfig.Id))
				{
					throw new CompliancePolicyValidationException(string.Format("Duplicate data classification found: {0}", dataClassificationConfig.Id.ToString()));
				}
				dictionary.Add(dataClassificationConfig.Id, dataClassificationConfig);
			}
			this.ClassificationConfig = dictionary;
			return Value.CreateValue(entries);
		}

		// Token: 0x04000319 RID: 793
		private static readonly Version minVersion = new Version("1.00.0002.000");

		// Token: 0x0400031A RID: 794
		private readonly IClassificationRuleStore classificationStore;
	}
}
