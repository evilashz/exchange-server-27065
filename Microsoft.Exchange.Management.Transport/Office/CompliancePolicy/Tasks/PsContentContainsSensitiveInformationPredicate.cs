using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.Classification;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D6 RID: 214
	public sealed class PsContentContainsSensitiveInformationPredicate : PsComplianceRulePredicateBase
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x00024A37 File Offset: 0x00022C37
		public PsContentContainsSensitiveInformationPredicate()
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00024A40 File Offset: 0x00022C40
		public PsContentContainsSensitiveInformationPredicate(IEnumerable<Hashtable> dataClassifications)
		{
			IClassificationRuleStore instance = InMemoryClassificationRuleStore.GetInstance();
			try
			{
				this.EnginePredicate = new ContentContainsSensitiveInformationPredicate(dataClassifications.Select(new Func<Hashtable, List<KeyValuePair<string, string>>>(PsContentContainsSensitiveInformationPredicate.HashtableToLowerCasedDictionary)).ToList<List<KeyValuePair<string, string>>>(), instance);
			}
			catch (CompliancePolicyValidationException innerException)
			{
				throw new InvalidContentContainsSensitiveInformationException(Strings.InvalidSensitiveInformationParameterValue, innerException);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00024A9C File Offset: 0x00022C9C
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00024AA4 File Offset: 0x00022CA4
		internal ContentContainsSensitiveInformationPredicate EnginePredicate { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00024AB0 File Offset: 0x00022CB0
		internal Hashtable[] DataClassifications
		{
			get
			{
				IClassificationRuleStore instance = InMemoryClassificationRuleStore.GetInstance();
				IList<Hashtable> list = new List<Hashtable>(this.EnginePredicate.ClassificationConfig.Values.Count);
				foreach (DataClassificationConfig dataClassificationConfig in this.EnginePredicate.ClassificationConfig.Values)
				{
					RuleDefinitionDetails ruleDetails = instance.GetRuleDetails(dataClassificationConfig.Id.ToString(), CultureInfo.CurrentCulture.ToString());
					if (ruleDetails.LocalizableDetails == null || !ruleDetails.LocalizableDetails.Any<KeyValuePair<string, CLASSIFICATION_DEFINITION_DETAILS>>())
					{
						ruleDetails = instance.GetRuleDetails(dataClassificationConfig.Id.ToString(), "en-us");
					}
					list.Add(new Hashtable
					{
						{
							"id",
							dataClassificationConfig.Id
						},
						{
							"name",
							ruleDetails.LocalizableDetails.Values.First<CLASSIFICATION_DEFINITION_DETAILS>().DefinitionName
						},
						{
							"mincount",
							dataClassificationConfig.MinCount.ToString("G")
						},
						{
							"maxcount",
							dataClassificationConfig.MaxCount.ToString("G")
						},
						{
							"minconfidence",
							dataClassificationConfig.MinConfidence.ToString("G")
						},
						{
							"maxconfidence",
							dataClassificationConfig.MaxConfidence.ToString("G")
						}
					});
				}
				return list.ToArray<Hashtable>();
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00024C64 File Offset: 0x00022E64
		internal override PredicateCondition ToEnginePredicate()
		{
			return this.EnginePredicate;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00024C6C File Offset: 0x00022E6C
		internal static PsContentContainsSensitiveInformationPredicate FromEnginePredicate(ContentContainsSensitiveInformationPredicate condition)
		{
			return new PsContentContainsSensitiveInformationPredicate
			{
				EnginePredicate = condition
			};
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00024CB5 File Offset: 0x00022EB5
		internal static List<KeyValuePair<string, string>> HashtableToLowerCasedDictionary(Hashtable hashtable)
		{
			ArgumentValidator.ThrowIfNull("hashtable", hashtable);
			return (from DictionaryEntry kvp in hashtable
			select new KeyValuePair<string, string>(PsContentContainsSensitiveInformationPredicate.CmdletParameterNameToEngineKeyMapping[((string)kvp.Key).ToLower()], (string)kvp.Value)).ToList<KeyValuePair<string, string>>();
		}

		// Token: 0x040003A3 RID: 931
		internal const string IdParameter = "id";

		// Token: 0x040003A4 RID: 932
		internal const string NameParameter = "name";

		// Token: 0x040003A5 RID: 933
		internal const string MinCountParameter = "mincount";

		// Token: 0x040003A6 RID: 934
		internal const string MaxCountParameter = "maxcount";

		// Token: 0x040003A7 RID: 935
		internal const string MinConfidenceParameter = "minconfidence";

		// Token: 0x040003A8 RID: 936
		internal const string MaxConfidenceParameter = "maxconfidence";

		// Token: 0x040003A9 RID: 937
		internal const string DefaultLocale = "en-us";

		// Token: 0x040003AA RID: 938
		internal static readonly Dictionary<string, string> CmdletParameterNameToEngineKeyMapping = new Dictionary<string, string>
		{
			{
				"name",
				DataClassificationConfig.IdKey
			},
			{
				"mincount",
				DataClassificationConfig.MinCountKey
			},
			{
				"maxcount",
				DataClassificationConfig.MaxCountKey
			},
			{
				"minconfidence",
				DataClassificationConfig.MinConfidenceKey
			},
			{
				"maxconfidence",
				DataClassificationConfig.MaxConfidenceKey
			}
		};
	}
}
