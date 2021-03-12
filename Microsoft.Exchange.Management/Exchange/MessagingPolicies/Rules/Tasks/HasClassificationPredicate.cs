using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD1 RID: 3025
	[Serializable]
	public class HasClassificationPredicate : TransportRulePredicate, IEquatable<HasClassificationPredicate>
	{
		// Token: 0x060071E3 RID: 29155 RVA: 0x001CFED0 File Offset: 0x001CE0D0
		public HasClassificationPredicate(IConfigDataProvider session)
		{
			this.session = session;
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x001CFEDF File Offset: 0x001CE0DF
		public override int GetHashCode()
		{
			if (this.Classification != null)
			{
				return this.Classification.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x001CFEF6 File Offset: 0x001CE0F6
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as HasClassificationPredicate)));
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x001CFF2F File Offset: 0x001CE12F
		public bool Equals(HasClassificationPredicate other)
		{
			if (this.Classification == null)
			{
				return null == other.Classification;
			}
			return this.Classification.Equals(other.Classification);
		}

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x060071E7 RID: 29159 RVA: 0x001CFF54 File Offset: 0x001CE154
		// (set) Token: 0x060071E8 RID: 29160 RVA: 0x001CFF5C File Offset: 0x001CE15C
		[ExceptionParameterName("ExceptIfHasClassification")]
		[LocDisplayName(RulesTasksStrings.IDs.ClassificationDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ClassificationDescription)]
		[ConditionParameterName("HasClassification")]
		public ADObjectId Classification
		{
			get
			{
				return this.classification;
			}
			set
			{
				this.classification = value;
			}
		}

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x060071E9 RID: 29161 RVA: 0x001CFF65 File Offset: 0x001CE165
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionHasClassification(Utils.GetClassificationDisplayName(this.Classification, this.session));
			}
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x001CFF84 File Offset: 0x001CE184
		internal static TransportRulePredicate CreateFromInternalConditionWithSession(Condition condition, IConfigDataProvider session)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if ((!predicateCondition.Name.Equals("is") && !predicateCondition.Name.Equals("contains")) || !(predicateCondition.Property is HeaderProperty) || !predicateCondition.Property.Name.Equals("X-MS-Exchange-Organization-Classification") || predicateCondition.Value.RawValues.Count != 1)
			{
				return null;
			}
			return new HasClassificationPredicate(session)
			{
				Classification = Utils.GetClassificationADObjectId(predicateCondition.Value.RawValues[0], session)
			};
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x001D0025 File Offset: 0x001CE225
		internal override void Reset()
		{
			this.classification = null;
			base.Reset();
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x001D0034 File Offset: 0x001CE234
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.classification == null)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x001D005C File Offset: 0x001CE25C
		internal override Condition ToInternalCondition()
		{
			string classificationId = Utils.GetClassificationId(this.classification, this.session);
			if (string.IsNullOrEmpty(classificationId))
			{
				throw new ArgumentException(RulesTasksStrings.InvalidClassification, "Classification");
			}
			Property property = TransportRuleParser.Instance.CreateProperty("Message.Headers:X-MS-Exchange-Organization-Classification");
			ShortList<string> valueEntries = new ShortList<string>
			{
				classificationId
			};
			return TransportRuleParser.Instance.CreatePredicate("contains", property, valueEntries);
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x001D00CD File Offset: 0x001CE2CD
		internal override string GetPredicateParameters()
		{
			return this.Classification.ToString();
		}

		// Token: 0x04003A4E RID: 14926
		[NonSerialized]
		private readonly IConfigDataProvider session;

		// Token: 0x04003A4F RID: 14927
		private ADObjectId classification;
	}
}
