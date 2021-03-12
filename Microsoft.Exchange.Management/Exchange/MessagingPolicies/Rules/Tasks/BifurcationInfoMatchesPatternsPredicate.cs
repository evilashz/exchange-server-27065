using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC8 RID: 3016
	[Serializable]
	public abstract class BifurcationInfoMatchesPatternsPredicate : BifurcationInfoPredicate
	{
		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x0600719C RID: 29084
		// (set) Token: 0x0600719D RID: 29085
		public abstract Pattern[] Patterns { get; set; }

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x0600719E RID: 29086 RVA: 0x001CF75E File Offset: 0x001CD95E
		// (set) Token: 0x0600719F RID: 29087 RVA: 0x001CF766 File Offset: 0x001CD966
		public bool UseLegacyRegex
		{
			get
			{
				return this.useLegacyRegex;
			}
			set
			{
				this.useLegacyRegex = value;
			}
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x001CF76F File Offset: 0x001CD96F
		internal override void Reset()
		{
			this.patterns = null;
			this.UseLegacyRegex = false;
			base.Reset();
		}

		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x060071A1 RID: 29089 RVA: 0x001CF785 File Offset: 0x001CD985
		internal override string Description
		{
			get
			{
				return this.LocalizedStringDescription(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildPatternStringList(this.Patterns), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x060071A2 RID: 29090
		internal abstract BifurcationInfoMatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x060071A3 RID: 29091 RVA: 0x001CF7B7 File Offset: 0x001CD9B7
		protected override void ValidateRead(List<ValidationError> errors)
		{
			MatchesPatternsPredicate.ValidateReadMatchesPatternsPredicate(this.patterns, this.UseLegacyRegex, base.Name, errors);
			base.ValidateRead(errors);
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x001CF7EC File Offset: 0x001CD9EC
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from p in this.patterns
			select Utils.QuoteCmdletParameter(p.ToString())).ToArray<string>());
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x001CF825 File Offset: 0x001CDA25
		internal override void SuppressPiiData()
		{
			this.Patterns = Utils.RedactPatterns(this.Patterns);
		}

		// Token: 0x04003A40 RID: 14912
		private bool useLegacyRegex;

		// Token: 0x04003A41 RID: 14913
		protected Pattern[] patterns;

		// Token: 0x02000BC9 RID: 3017
		// (Invoke) Token: 0x060071A9 RID: 29097
		internal delegate LocalizedString LocalizedStringDescriptionDelegate(string patterns);
	}
}
