using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000096 RID: 150
	internal class ContainsDataClassificationPredicate : PredicateCondition
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00016078 File Offset: 0x00014278
		public ContainsDataClassificationPredicate(Property property, ShortList<ShortList<KeyValuePair<string, string>>> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!typeof(IEnumerable<DiscoveredDataClassification>).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(TransportRulesStrings.DataClassificationPropertyRequired(this.Name));
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x000160C6 File Offset: 0x000142C6
		public override string Name
		{
			get
			{
				return "containsDataClassification";
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000160CD File Offset: 0x000142CD
		public override Version MinimumVersion
		{
			get
			{
				return ContainsDataClassificationPredicate.DataClassificationBaseVersion;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000160D4 File Offset: 0x000142D4
		protected override Value BuildValue(ShortList<ShortList<KeyValuePair<string, string>>> entries, RulesCreationContext creationContext)
		{
			if (entries.Count == 0)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
			foreach (ShortList<KeyValuePair<string, string>> keyValueParameters in entries)
			{
				this.targetClassifications.Add(new TargetDataClassification(keyValueParameters));
			}
			return Value.CreateValue(entries);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00016174 File Offset: 0x00014374
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			BaseTransportRulesEvaluationContext baseTransportRulesEvaluationContext = (BaseTransportRulesEvaluationContext)baseContext;
			if (baseTransportRulesEvaluationContext == null)
			{
				throw new ArgumentException("context is either null or not of type: BaseTransportRulesEvaluationContext");
			}
			baseTransportRulesEvaluationContext.PredicateName = this.Name;
			bool flag = false;
			IEnumerable<DiscoveredDataClassification> enumerable = (IEnumerable<DiscoveredDataClassification>)base.Property.GetValue(baseTransportRulesEvaluationContext);
			List<DiscoveredDataClassification> list = new List<DiscoveredDataClassification>();
			HashSet<string> hashSet = new HashSet<string>();
			using (IEnumerator<DiscoveredDataClassification> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DiscoveredDataClassification discoveredClassification = enumerator.Current;
					if (this.targetClassifications.Any((TargetDataClassification x) => x.Matches(discoveredClassification)))
					{
						flag = true;
						list.Add(discoveredClassification);
						foreach (DataClassificationSourceInfo dataClassificationSourceInfo in discoveredClassification.MatchingSourceInfos)
						{
							if (!string.IsNullOrEmpty(dataClassificationSourceInfo.TopLevelSourceName))
							{
								hashSet.Add(dataClassificationSourceInfo.TopLevelSourceName);
							}
						}
					}
				}
			}
			base.UpdateEvaluationHistory(baseContext, flag, (from c in list
			select c.ClassificationName).ToList<string>(), 0);
			base.UpdateEvaluationHistory(baseContext, flag, (from c in list
			select c.Id).ToList<string>(), 2);
			base.UpdateEvaluationHistory(baseContext, flag, hashSet.ToList<string>(), 1);
			return flag;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00016318 File Offset: 0x00014518
		public override void GetSupplementalData(SupplementalData data)
		{
			foreach (TargetDataClassification targetDataClassification in this.targetClassifications)
			{
				data.Add("DataClassification", new KeyValuePair<string, string>(targetDataClassification.Id, targetDataClassification.OpaqueData));
			}
		}

		// Token: 0x0400027A RID: 634
		internal static readonly Version DataClassificationBaseVersion = new Version("15.00.0003.01");

		// Token: 0x0400027B RID: 635
		private readonly List<TargetDataClassification> targetClassifications = new List<TargetDataClassification>();

		// Token: 0x02000097 RID: 151
		internal enum MatchResultTypes
		{
			// Token: 0x0400027F RID: 639
			ClassificationNames,
			// Token: 0x04000280 RID: 640
			TopLevelSourceNames,
			// Token: 0x04000281 RID: 641
			ClassificationIds
		}
	}
}
