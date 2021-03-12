using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000CB RID: 203
	public class ContentMetadataContainsPredicate : PredicateCondition
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000FAF5 File Offset: 0x0000DCF5
		public ContentMetadataContainsPredicate(List<string> entries) : base(new Property("Item.Metadata", typeof(IDictionary<string, List<string>>)), entries)
		{
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000FB12 File Offset: 0x0000DD12
		public override string Name
		{
			get
			{
				return "contentMetadataContains";
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000FB19 File Offset: 0x0000DD19
		public override Version MinimumVersion
		{
			get
			{
				return ContentMetadataContainsPredicate.minVersion;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000FB20 File Offset: 0x0000DD20
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0000FB28 File Offset: 0x0000DD28
		internal IDictionary<string, List<string>> WordsConfig { get; private set; }

		// Token: 0x06000532 RID: 1330 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			if (!(base.Property.Type == typeof(IDictionary<string, List<string>>)))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' is in inconsitent state due to unknown property '{1}'", context.CurrentRule.Name, base.Property.Name));
			}
			IDictionary<string, List<string>> dictionary = base.Property.GetValue(context) as IDictionary<string, List<string>>;
			IDictionary<string, List<string>> conditionValue = this.WordsConfig;
			if (conditionValue == null || !conditionValue.Any<KeyValuePair<string, List<string>>>())
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' contains an invalid property '{1}'", context.CurrentRule.Name, base.Property.Name));
			}
			if (dictionary != null && dictionary.Any<KeyValuePair<string, List<string>>>())
			{
				using (IEnumerator<string> enumerator = conditionValue.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						if (dictionary.ContainsKey(key) && dictionary[key] != null)
						{
							if (dictionary[key].Any((string propVal) => conditionValue[key].Any((string condVal) => propVal.Equals(condVal, StringComparison.InvariantCultureIgnoreCase))))
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000FD18 File Offset: 0x0000DF18
		protected override Value BuildValue(List<string> entries)
		{
			if (!entries.Any<string>())
			{
				throw new CompliancePolicyValidationException("entries can not be empty for ContentMetadataContainsPredicate!");
			}
			IDictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			foreach (string text in entries)
			{
				int num = text.IndexOf(ContentMetadataContainsPredicate.PropertyAndValueDelimiter);
				if (num <= 0 || num == text.Length - 1)
				{
					throw new CompliancePolicyValidationException("Argument for ContentMetadataContains is in incorrect format");
				}
				string text2 = text.Substring(0, num).Trim();
				if (string.IsNullOrWhiteSpace(text2))
				{
					throw new CompliancePolicyValidationException("Argument for ContentMetadataContains is in incorrect format");
				}
				string[] source = text.Substring(num + ContentMetadataContainsPredicate.PropertyAndValueDelimiter.Length).Split(new string[]
				{
					ContentMetadataContainsPredicate.ValuesDelimiter
				}, StringSplitOptions.RemoveEmptyEntries);
				if (source.Any(new Func<string, bool>(string.IsNullOrWhiteSpace)))
				{
					throw new CompliancePolicyValidationException("Argument for ContentMetadataContains is in incorrect format");
				}
				List<string> list = (from p in source
				select p.Trim()).ToList<string>();
				if (dictionary.ContainsKey(text2))
				{
					dictionary[text2].AddRange(list);
				}
				else
				{
					dictionary.Add(text2, list);
				}
			}
			this.WordsConfig = dictionary;
			return Value.CreateValue(typeof(string[]), entries);
		}

		// Token: 0x0400031D RID: 797
		public static readonly string PropertyAndValueDelimiter = ":";

		// Token: 0x0400031E RID: 798
		public static readonly string ValuesDelimiter = ",";

		// Token: 0x0400031F RID: 799
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}
