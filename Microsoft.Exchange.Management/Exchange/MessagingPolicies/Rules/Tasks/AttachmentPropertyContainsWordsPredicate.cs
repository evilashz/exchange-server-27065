using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC2 RID: 3010
	[Serializable]
	public class AttachmentPropertyContainsWordsPredicate : ContainsWordsPredicate, IEquatable<AttachmentPropertyContainsWordsPredicate>
	{
		// Token: 0x06007162 RID: 29026 RVA: 0x001CE8F2 File Offset: 0x001CCAF2
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x06007163 RID: 29027 RVA: 0x001CE8FF File Offset: 0x001CCAFF
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentPropertyContainsWordsPredicate)));
		}

		// Token: 0x06007164 RID: 29028 RVA: 0x001CE938 File Offset: 0x001CCB38
		public bool Equals(AttachmentPropertyContainsWordsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x06007165 RID: 29029 RVA: 0x001CE95D File Offset: 0x001CCB5D
		// (set) Token: 0x06007166 RID: 29030 RVA: 0x001CE965 File Offset: 0x001CCB65
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[ConditionParameterName("AttachmentPropertyContainsWords")]
		[ExceptionParameterName("ExceptIfAttachmentPropertyContainsWords")]
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

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x06007167 RID: 29031 RVA: 0x001CE96E File Offset: 0x001CCB6E
		protected override ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new ContainsWordsPredicate.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAttachmentPropertyContainsWords);
			}
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x001CE98C File Offset: 0x001CCB8C
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Words == null || this.Words.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			List<string> list = (from w in this.Words
			select w.ToString()).ToList<string>();
			List<KeyValuePair<string, List<string>>> source = AttachmentPropertyContainsPredicate.ParsePredicateParameters(list);
			if (!source.Any<KeyValuePair<string, List<string>>>())
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.AttachmentMetadataPropertyNotSpecified(string.Join(", ", list)), base.Name));
				return;
			}
			Word[] words = this.Words;
			int i = 0;
			while (i < words.Length)
			{
				Word word = words[i];
				string value = word.Value;
				string[] array = value.Split(new char[]
				{
					':'
				});
				if (array.Length < 2 || (array.Length >= 2 && (string.IsNullOrWhiteSpace(array[0]) || string.IsNullOrWhiteSpace(array[1]))))
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.AttachmentMetadataPropertyNotSpecified(value), base.Name));
				}
				else
				{
					string[] source2 = array[1].Trim().Split(new char[]
					{
						','
					});
					if (!source2.Any(new Func<string, bool>(string.IsNullOrWhiteSpace)))
					{
						i++;
						continue;
					}
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.AttachmentMetadataParameterContainsEmptyWords(value), base.Name));
				}
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x001CEB0C File Offset: 0x001CCD0C
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("attachmentPropertyContains"))
			{
				return null;
			}
			AttachmentPropertyContainsWordsPredicate attachmentPropertyContainsWordsPredicate = new AttachmentPropertyContainsWordsPredicate();
			attachmentPropertyContainsWordsPredicate.Words = (from w in predicateCondition.Value.RawValues
			select new Word(w)).ToArray<Word>();
			return attachmentPropertyContainsWordsPredicate;
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x001CEB80 File Offset: 0x001CCD80
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (Word word in this.words)
			{
				shortList.Add(word.ToString());
			}
			return TransportRuleParser.Instance.CreatePredicate("attachmentPropertyContains", null, shortList);
		}
	}
}
