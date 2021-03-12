using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BBA RID: 3002
	[Serializable]
	public class AttachmentContainsWordsPredicate : ContainsWordsPredicate, IEquatable<AttachmentContainsWordsPredicate>
	{
		// Token: 0x06007113 RID: 28947 RVA: 0x001CDED7 File Offset: 0x001CC0D7
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x001CDEE4 File Offset: 0x001CC0E4
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentContainsWordsPredicate)));
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x001CDF1D File Offset: 0x001CC11D
		public bool Equals(AttachmentContainsWordsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x06007116 RID: 28950 RVA: 0x001CDF42 File Offset: 0x001CC142
		// (set) Token: 0x06007117 RID: 28951 RVA: 0x001CDF4A File Offset: 0x001CC14A
		[ExceptionParameterName("ExceptIfAttachmentContainsWords")]
		[ConditionParameterName("AttachmentContainsWords")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
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

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x06007118 RID: 28952 RVA: 0x001CDF53 File Offset: 0x001CC153
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAttachmentContainsWords);
			}
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x001CDF64 File Offset: 0x001CC164
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("attachmentContainsWords"))
			{
				return null;
			}
			Word[] array = new Word[predicateCondition.Value.RawValues.Count];
			for (int i = 0; i < predicateCondition.Value.RawValues.Count; i++)
			{
				try
				{
					array[i] = new Word(predicateCondition.Value.RawValues[i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			return new AttachmentContainsWordsPredicate
			{
				Words = array
			};
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x001CE014 File Offset: 0x001CC214
		internal override void Reset()
		{
			this.words = null;
			base.Reset();
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x001CE024 File Offset: 0x001CC224
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Word word in this.words)
			{
				shortList.Add(word.ToString());
			}
			return TransportRuleParser.Instance.CreatePredicate("attachmentContainsWords", null, shortList);
		}
	}
}
