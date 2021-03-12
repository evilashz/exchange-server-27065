using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BAE RID: 2990
	[Serializable]
	public abstract class ContainsWordsPredicate : TransportRulePredicate
	{
		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x060070BA RID: 28858
		// (set) Token: 0x060070BB RID: 28859
		public abstract Word[] Words { get; set; }

		// Token: 0x060070BC RID: 28860 RVA: 0x001CD482 File Offset: 0x001CB682
		internal override void Reset()
		{
			this.words = null;
			base.Reset();
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x060070BD RID: 28861 RVA: 0x001CD491 File Offset: 0x001CB691
		internal override string Description
		{
			get
			{
				return this.LocalizedStringDescription(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildWordStringList(this.Words), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x060070BE RID: 28862
		protected abstract ContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x060070BF RID: 28863 RVA: 0x001CD4C3 File Offset: 0x001CB6C3
		protected override void ValidateRead(List<ValidationError> errors)
		{
			ContainsWordsPredicate.ValidateReadContainsWordsPredicate(this.words, base.Name, errors);
			base.ValidateRead(errors);
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x001CD4F2 File Offset: 0x001CB6F2
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from w in this.Words
			select Utils.QuoteCmdletParameter(w.ToString())).ToArray<string>());
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x001CD52C File Offset: 0x001CB72C
		internal static void ValidateReadContainsWordsPredicate(Word[] words, string name, List<ValidationError> errors)
		{
			if (words == null || words.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, name));
				return;
			}
			foreach (Word word in words)
			{
				string value = word.Value;
				int index;
				if (!string.IsNullOrEmpty(value) && !Utils.CheckIsUnicodeStringWellFormed(value, out index))
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)value[index]), name));
					break;
				}
			}
		}

		// Token: 0x060070C2 RID: 28866 RVA: 0x001CD5AC File Offset: 0x001CB7AC
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Words = SuppressingPiiData.Redact(this.Words, out array, out array2);
		}

		// Token: 0x04003A21 RID: 14881
		protected Word[] words;

		// Token: 0x02000BAF RID: 2991
		// (Invoke) Token: 0x060070C6 RID: 28870
		protected delegate LocalizedString LocalizedStringDescriptionDelegate(string words);
	}
}
