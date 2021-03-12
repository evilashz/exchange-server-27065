using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BD4 RID: 3028
	[Serializable]
	public class HeaderContainsPredicate : SinglePropertyContainsPredicate, IEquatable<HeaderContainsPredicate>
	{
		// Token: 0x06007200 RID: 29184 RVA: 0x001D02C3 File Offset: 0x001CE4C3
		public HeaderContainsPredicate() : base("Message.Headers")
		{
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x001D02D0 File Offset: 0x001CE4D0
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words) + this.messageHeader.GetHashCode();
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x001D02EF File Offset: 0x001CE4EF
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as HeaderContainsPredicate)));
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x001D0328 File Offset: 0x001CE528
		public bool Equals(HeaderContainsPredicate other)
		{
			if (this.Words == null)
			{
				return other.Words == null && this.MessageHeader.Equals(other.MessageHeader);
			}
			return this.MessageHeader.Equals(other.MessageHeader) && this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x06007204 RID: 29188 RVA: 0x001D0385 File Offset: 0x001CE585
		// (set) Token: 0x06007205 RID: 29189 RVA: 0x001D038D File Offset: 0x001CE58D
		[LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		[ConditionParameterName("HeaderContainsMessageHeader")]
		[ExceptionParameterName("ExceptIfHeaderContainsMessageHeader")]
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

		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x06007206 RID: 29190 RVA: 0x001D0396 File Offset: 0x001CE596
		// (set) Token: 0x06007207 RID: 29191 RVA: 0x001D039E File Offset: 0x001CE59E
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[ExceptionParameterName("ExceptIfHeaderContainsWords")]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("HeaderContainsWords")]
		public override Word[] Words
		{
			get
			{
				return this.words;
			}
			set
			{
				this.words = value;
			}
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x06007208 RID: 29192 RVA: 0x001D03A7 File Offset: 0x001CE5A7
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x06007209 RID: 29193 RVA: 0x001D03B0 File Offset: 0x001CE5B0
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionHeaderContains(this.MessageHeader.ToString(), RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildWordStringList(this.Words), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x0600720A RID: 29194 RVA: 0x001D03FC File Offset: 0x001CE5FC
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!(predicateCondition.Property is HeaderProperty))
			{
				return null;
			}
			HeaderContainsPredicate headerContainsPredicate = (HeaderContainsPredicate)SinglePropertyContainsPredicate.CreateFromInternalCondition<HeaderContainsPredicate>(condition, predicateCondition.Property.Name);
			if (headerContainsPredicate == null)
			{
				return null;
			}
			try
			{
				headerContainsPredicate.MessageHeader = new HeaderName(predicateCondition.Property.Name);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return headerContainsPredicate;
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x001D0478 File Offset: 0x001CE678
		internal override void Reset()
		{
			this.messageHeader = HeaderName.Empty;
			base.Reset();
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x001D048B File Offset: 0x001CE68B
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.MessageHeader == HeaderName.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x001D04C0 File Offset: 0x001CE6C0
		internal override Property CreateProperty()
		{
			return TransportRuleParser.Instance.CreateProperty("Message.Headers:" + this.MessageHeader.ToString());
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x001D04F8 File Offset: 0x001CE6F8
		internal override string ToCmdletParameter(bool isException = false)
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				isException ? "ExceptIfHeaderContainsMessageHeader" : "HeaderContainsMessageHeader",
				Utils.QuoteCmdletParameter(this.MessageHeader.ToString()),
				isException ? "ExceptIfHeaderContainsWords" : "HeaderContainsWords",
				this.GetPredicateParameters()
			});
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x001D0560 File Offset: 0x001CE760
		internal override void SuppressPiiData()
		{
			this.MessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName>(RuleSchema.HeaderContainsMessageHeader, this.MessageHeader);
			this.Words = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.HeaderContainsWords, this.Words);
		}

		// Token: 0x04003A52 RID: 14930
		private HeaderName messageHeader;
	}
}
