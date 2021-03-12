using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BE6 RID: 3046
	[Serializable]
	public class SenderAttributeContainsPredicate : ContainsWordsPredicate, IEquatable<SenderAttributeContainsPredicate>
	{
		// Token: 0x060072C9 RID: 29385 RVA: 0x001D3739 File Offset: 0x001D1939
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x001D3746 File Offset: 0x001D1946
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SenderAttributeContainsPredicate)));
		}

		// Token: 0x060072CB RID: 29387 RVA: 0x001D377F File Offset: 0x001D197F
		public bool Equals(SenderAttributeContainsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x001D37A4 File Offset: 0x001D19A4
		// (set) Token: 0x060072CD RID: 29389 RVA: 0x001D37AC File Offset: 0x001D19AC
		[ExceptionParameterName("ExceptIfSenderADAttributeContainsWords")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ConditionParameterName("SenderADAttributeContainsWords")]
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

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x060072CE RID: 29390 RVA: 0x001D37B5 File Offset: 0x001D19B5
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionSenderAttributeContains);
			}
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x001D37C4 File Offset: 0x001D19C4
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("senderAttributeContains"))
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
			return new SenderAttributeContainsPredicate
			{
				Words = array
			};
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x001D3874 File Offset: 0x001D1A74
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Words == null || this.Words.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			Word[] words = this.Words;
			int i = 0;
			while (i < words.Length)
			{
				Word word = words[i];
				string value = word.Value;
				int index;
				if (!string.IsNullOrEmpty(value) && !Utils.CheckIsUnicodeStringWellFormed(value, out index))
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)value[index]), base.Name));
				}
				else
				{
					int num = value.IndexOf(':');
					if (num >= 0)
					{
						string text = value.Substring(0, num).Trim().ToLowerInvariant();
						if (TransportUtils.GetDisclaimerMacroLookupTable().ContainsKey(text))
						{
							i++;
							continue;
						}
						errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidMacroName(text), base.Name));
					}
					else
					{
						errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.MacroNameNotSpecified(value), base.Name));
					}
				}
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x060072D1 RID: 29393 RVA: 0x001D397C File Offset: 0x001D1B7C
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Word word in this.words)
			{
				shortList.Add(word.ToString());
			}
			return TransportRuleParser.Instance.CreatePredicate("senderAttributeContains", null, shortList);
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x001D39D5 File Offset: 0x001D1BD5
		internal override void SuppressPiiData()
		{
			this.Words = Utils.RedactNameValuePairWords(this.Words);
		}
	}
}
