using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BBB RID: 3003
	[Serializable]
	public class AttachmentExtensionMatchesWordsPredicate : TransportRulePredicate, IEquatable<AttachmentExtensionMatchesWordsPredicate>
	{
		// Token: 0x0600711D RID: 28957 RVA: 0x001CE08C File Offset: 0x001CC28C
		public AttachmentExtensionMatchesWordsPredicate()
		{
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x001CE094 File Offset: 0x001CC294
		public AttachmentExtensionMatchesWordsPredicate(Word[] words)
		{
			this.Words = words;
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x001CE0A3 File Offset: 0x001CC2A3
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<Word>(this.Words);
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x001CE0B0 File Offset: 0x001CC2B0
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentExtensionMatchesWordsPredicate)));
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x001CE0E9 File Offset: 0x001CC2E9
		public bool Equals(AttachmentExtensionMatchesWordsPredicate other)
		{
			if (this.Words == null)
			{
				return null == other.Words;
			}
			return this.Words.SequenceEqual(other.Words);
		}

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x06007122 RID: 28962 RVA: 0x001CE10E File Offset: 0x001CC30E
		// (set) Token: 0x06007123 RID: 28963 RVA: 0x001CE116 File Offset: 0x001CC316
		[LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[ExceptionParameterName("ExceptIfAttachmentExtensionMatchesWords")]
		[LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[ConditionParameterName("AttachmentExtensionMatchesWords")]
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

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x06007124 RID: 28964 RVA: 0x001CE120 File Offset: 0x001CC320
		internal override string Description
		{
			get
			{
				string text = RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildWordStringList(this.Words), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength);
				return RulesTasksStrings.RuleDescriptionAttachmentExtensionMatchesWords(text);
			}
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x001CE164 File Offset: 0x001CC364
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("is") || !predicateCondition.Property.Name.Equals("Message.AttachmentExtensions"))
			{
				return null;
			}
			Word[] array = (from value in predicateCondition.Value.RawValues
			select new Word(value)).ToArray<Word>();
			return new AttachmentExtensionMatchesWordsPredicate(array);
		}

		// Token: 0x06007126 RID: 28966 RVA: 0x001CE1F0 File Offset: 0x001CC3F0
		internal override Condition ToInternalCondition()
		{
			ShortList<string> valueEntries = new ShortList<string>(from word in this.words
			select word.Value);
			return TransportRuleParser.Instance.CreatePredicate("is", TransportRuleParser.Instance.CreateProperty("Message.AttachmentExtensions"), valueEntries);
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x001CE24A File Offset: 0x001CC44A
		internal override void Reset()
		{
			this.Words = null;
			base.Reset();
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x001CE259 File Offset: 0x001CC459
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Words == null || this.Words.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007129 RID: 28969 RVA: 0x001CE29F File Offset: 0x001CC49F
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from w in this.Words
			select Utils.QuoteCmdletParameter(w.ToString())).ToArray<string>());
		}

		// Token: 0x0600712A RID: 28970 RVA: 0x001CE2D8 File Offset: 0x001CC4D8
		internal override void SuppressPiiData()
		{
			this.Words = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.AttachmentExtensionMatchesWords, this.Words);
		}

		// Token: 0x04003A32 RID: 14898
		private Word[] words;
	}
}
