using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD5 RID: 3029
	[Serializable]
	public class HeaderMatchesPredicate : SinglePropertyMatchesPredicate, IEquatable<HeaderMatchesPredicate>
	{
		// Token: 0x06007210 RID: 29200 RVA: 0x001D058E File Offset: 0x001CE78E
		public HeaderMatchesPredicate() : this(true)
		{
		}

		// Token: 0x06007211 RID: 29201 RVA: 0x001D0597 File Offset: 0x001CE797
		public HeaderMatchesPredicate(bool useLegacyRegex) : base("Message.Headers", useLegacyRegex)
		{
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x001D05A8 File Offset: 0x001CE7A8
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Pattern>(this.Patterns) + this.MessageHeader.GetHashCode();
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x001D05D5 File Offset: 0x001CE7D5
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as HeaderMatchesPredicate)));
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x001D0610 File Offset: 0x001CE810
		public bool Equals(HeaderMatchesPredicate other)
		{
			if (this.Patterns == null)
			{
				return other.Patterns == null && this.MessageHeader.Equals(other.MessageHeader);
			}
			return this.MessageHeader.Equals(other.MessageHeader) && this.Patterns.SequenceEqual(other.Patterns);
		}

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x06007215 RID: 29205 RVA: 0x001D066D File Offset: 0x001CE86D
		// (set) Token: 0x06007216 RID: 29206 RVA: 0x001D0675 File Offset: 0x001CE875
		[ExceptionParameterName("ExceptIfHeaderMatchesMessageHeader")]
		[LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[ConditionParameterName("HeaderMatchesMessageHeader")]
		public HeaderName MessageHeader
		{
			get
			{
				return this.messageHeader;
			}
			set
			{
				this.messageHeader = value;
			}
		}

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x001D067E File Offset: 0x001CE87E
		// (set) Token: 0x06007218 RID: 29208 RVA: 0x001D0686 File Offset: 0x001CE886
		[ConditionParameterName("HeaderMatchesPatterns")]
		[ExceptionParameterName("ExceptIfHeaderMatchesPatterns")]
		[LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public override Pattern[] Patterns
		{
			get
			{
				return this.patterns;
			}
			set
			{
				this.patterns = value;
			}
		}

		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x06007219 RID: 29209 RVA: 0x001D068F File Offset: 0x001CE88F
		internal override MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x0600721A RID: 29210 RVA: 0x001D0698 File Offset: 0x001CE898
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionHeaderMatches(this.MessageHeader.ToString(), RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildPatternStringList(this.Patterns), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x001D06E4 File Offset: 0x001CE8E4
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			PredicateCondition predicateCondition = condition as PredicateCondition;
			if (predicateCondition == null || predicateCondition.ConditionType != ConditionType.Predicate || !(predicateCondition.Property is HeaderProperty))
			{
				return null;
			}
			HeaderMatchesPredicate headerMatchesPredicate = (HeaderMatchesPredicate)SinglePropertyMatchesPredicate.CreateFromInternalCondition<HeaderMatchesPredicate>(condition, predicateCondition.Property.Name);
			if (headerMatchesPredicate == null)
			{
				return null;
			}
			try
			{
				headerMatchesPredicate.MessageHeader = new HeaderName(predicateCondition.Property.Name);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return headerMatchesPredicate;
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x001D0760 File Offset: 0x001CE960
		internal override void Reset()
		{
			this.messageHeader = HeaderName.Empty;
			base.Reset();
		}

		// Token: 0x0600721D RID: 29213 RVA: 0x001D0773 File Offset: 0x001CE973
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.MessageHeader == HeaderName.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x001D07A8 File Offset: 0x001CE9A8
		internal override Property CreateProperty()
		{
			return TransportRuleParser.Instance.CreateProperty("Message.Headers:" + this.MessageHeader.ToString());
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x001D07E0 File Offset: 0x001CE9E0
		internal override string ToCmdletParameter(bool isException = false)
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				isException ? "ExceptIfHeaderMatchesMessageHeader" : "HeaderMatchesMessageHeader",
				Utils.QuoteCmdletParameter(this.MessageHeader.ToString()),
				isException ? "ExceptIfHeaderMatchesPatterns" : "HeaderMatchesPatterns",
				this.GetPredicateParameters()
			});
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x001D0848 File Offset: 0x001CEA48
		internal override void SuppressPiiData()
		{
			this.MessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName>(RuleSchema.HeaderMatchesMessageHeader, this.MessageHeader);
			this.Patterns = Utils.RedactPatterns(this.Patterns);
		}

		// Token: 0x04003A53 RID: 14931
		private HeaderName messageHeader;
	}
}
