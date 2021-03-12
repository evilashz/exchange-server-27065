using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC6 RID: 3014
	[Serializable]
	public abstract class BifurcationInfoContainsWordsPredicate : BifurcationInfoPredicate
	{
		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x0600718E RID: 29070
		// (set) Token: 0x0600718F RID: 29071
		public abstract Word[] Words { get; set; }

		// Token: 0x06007190 RID: 29072 RVA: 0x001CF68B File Offset: 0x001CD88B
		internal override void Reset()
		{
			this.words = null;
			base.Reset();
		}

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x06007191 RID: 29073 RVA: 0x001CF69A File Offset: 0x001CD89A
		internal override string Description
		{
			get
			{
				return this.LocalizedStringDescription(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildWordStringList(this.Words), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x06007192 RID: 29074
		internal abstract BifurcationInfoContainsWordsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x06007193 RID: 29075 RVA: 0x001CF6CC File Offset: 0x001CD8CC
		protected override void ValidateRead(List<ValidationError> errors)
		{
			ContainsWordsPredicate.ValidateReadContainsWordsPredicate(this.words, base.Name, errors);
			base.ValidateRead(errors);
		}

		// Token: 0x06007194 RID: 29076 RVA: 0x001CF6FB File Offset: 0x001CD8FB
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from w in this.Words
			select Utils.QuoteCmdletParameter(w.ToString())).ToArray<string>());
		}

		// Token: 0x06007195 RID: 29077 RVA: 0x001CF734 File Offset: 0x001CD934
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Words = SuppressingPiiData.Redact(this.Words, out array, out array2);
		}

		// Token: 0x04003A3E RID: 14910
		protected Word[] words;

		// Token: 0x02000BC7 RID: 3015
		// (Invoke) Token: 0x06007199 RID: 29081
		internal delegate LocalizedString LocalizedStringDescriptionDelegate(string words);
	}
}
