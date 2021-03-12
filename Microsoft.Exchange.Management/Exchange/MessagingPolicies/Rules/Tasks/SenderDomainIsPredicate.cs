using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE8 RID: 3048
	[Serializable]
	public class SenderDomainIsPredicate : TransportRulePredicate, IEquatable<SenderDomainIsPredicate>
	{
		// Token: 0x060072E1 RID: 29409 RVA: 0x001D3C3F File Offset: 0x001D1E3F
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x001D3C4C File Offset: 0x001D1E4C
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SenderDomainIsPredicate)));
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x001D3C85 File Offset: 0x001D1E85
		public bool Equals(SenderDomainIsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x060072E4 RID: 29412 RVA: 0x001D3CAA File Offset: 0x001D1EAA
		// (set) Token: 0x060072E5 RID: 29413 RVA: 0x001D3CB2 File Offset: 0x001D1EB2
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("SenderDomainIs")]
		[ExceptionParameterName("ExceptIfSenderDomainIs")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] Words
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

		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x060072E6 RID: 29414 RVA: 0x001D3CCC File Offset: 0x001D1ECC
		internal override string Description
		{
			get
			{
				List<string> stringValues = (from word in this.Words
				select word.ToString()).ToList<string>();
				string domains = RuleDescription.BuildDescriptionStringFromStringArray(stringValues, RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength);
				return RulesTasksStrings.RuleDescriptionSenderDomainIs(domains);
			}
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x001D3D29 File Offset: 0x001D1F29
		internal override void Reset()
		{
			this.words = null;
			base.Reset();
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x001D3D38 File Offset: 0x001D1F38
		protected override void ValidateRead(List<ValidationError> errors)
		{
			ContainsWordsPredicate.ValidateReadContainsWordsPredicate(this.words, base.Name, errors);
			base.ValidateRead(errors);
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x001D3D67 File Offset: 0x001D1F67
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from w in this.Words
			select Utils.QuoteCmdletParameter(w.ToString())).ToArray<string>());
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x001D3DB0 File Offset: 0x001D1FB0
		internal override Condition ToInternalCondition()
		{
			ShortList<string> valueEntries = new ShortList<string>(from x in this.Words
			select x.ToString());
			return TransportRuleParser.Instance.CreatePredicate("domainIs", TransportRuleParser.Instance.CreateProperty("Message.SenderDomain"), valueEntries);
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x001D3E14 File Offset: 0x001D2014
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			DomainIsPredicate domainIsPredicate = condition as DomainIsPredicate;
			if (domainIsPredicate == null || !domainIsPredicate.Property.Name.Equals("Message.SenderDomain"))
			{
				return null;
			}
			object value = domainIsPredicate.Value.GetValue(null);
			IEnumerable<string> source;
			if (value is string)
			{
				source = new List<string>
				{
					value as string
				};
			}
			else
			{
				source = (IEnumerable<string>)value;
			}
			SenderDomainIsPredicate senderDomainIsPredicate = new SenderDomainIsPredicate();
			senderDomainIsPredicate.Words = (from s in source
			select new Word(s)).ToArray<Word>();
			return senderDomainIsPredicate;
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x001D3EAE File Offset: 0x001D20AE
		internal override void SuppressPiiData()
		{
			this.Words = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.HeaderContainsWords, this.Words);
		}

		// Token: 0x04003A7D RID: 14973
		internal const string InternalPropertyName = "Message.SenderDomain";

		// Token: 0x04003A7E RID: 14974
		protected Word[] words;
	}
}
