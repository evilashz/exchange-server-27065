using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BB2 RID: 2994
	[Serializable]
	public abstract class MatchesPatternsPredicate : TransportRulePredicate
	{
		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x060070D5 RID: 28885
		// (set) Token: 0x060070D6 RID: 28886
		public abstract Pattern[] Patterns { get; set; }

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x060070D7 RID: 28887 RVA: 0x001CD7D5 File Offset: 0x001CB9D5
		// (set) Token: 0x060070D8 RID: 28888 RVA: 0x001CD7DD File Offset: 0x001CB9DD
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

		// Token: 0x060070D9 RID: 28889 RVA: 0x001CD7E6 File Offset: 0x001CB9E6
		internal override void Reset()
		{
			this.patterns = null;
			this.UseLegacyRegex = false;
			base.Reset();
		}

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x060070DA RID: 28890 RVA: 0x001CD7FC File Offset: 0x001CB9FC
		internal override string Description
		{
			get
			{
				return this.LocalizedStringDescription(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildPatternStringList(this.Patterns), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x060070DB RID: 28891
		internal abstract MatchesPatternsPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x060070DC RID: 28892 RVA: 0x001CD82E File Offset: 0x001CBA2E
		protected override void ValidateRead(List<ValidationError> errors)
		{
			MatchesPatternsPredicate.ValidateReadMatchesPatternsPredicate(this.patterns, this.UseLegacyRegex, base.Name, errors);
			base.ValidateRead(errors);
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x001CD863 File Offset: 0x001CBA63
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from p in this.patterns
			select Utils.QuoteCmdletParameter(p.ToString())).ToArray<string>());
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x001CD89C File Offset: 0x001CBA9C
		internal static void ValidateReadMatchesPatternsPredicate(Pattern[] patterns, bool useLegacyRegex, string name, List<ValidationError> errors)
		{
			if (patterns == null || patterns.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, name));
				return;
			}
			foreach (Pattern pattern in patterns)
			{
				string value = pattern.Value;
				if (!string.IsNullOrEmpty(value))
				{
					int index;
					if (!Utils.CheckIsUnicodeStringWellFormed(value, out index))
					{
						errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)value[index]), name));
					}
					else
					{
						try
						{
							Pattern.ValidatePattern(value, useLegacyRegex, false);
						}
						catch (ValidationArgumentException ex)
						{
							LocalizedString description = ValidationError.CombineErrorDescriptions(new List<ValidationError>
							{
								new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidRegex(value), name),
								new RulePhrase.RulePhraseValidationError(ex.LocalizedString, name)
							});
							errors.Add(new RulePhrase.RulePhraseValidationError(description, name));
						}
						catch (ArgumentException)
						{
							errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidRegex(value), name));
						}
					}
				}
			}
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x001CD9A8 File Offset: 0x001CBBA8
		internal override void SuppressPiiData()
		{
			this.Patterns = Utils.RedactPatterns(this.Patterns);
		}

		// Token: 0x04003A25 RID: 14885
		private bool useLegacyRegex;

		// Token: 0x04003A26 RID: 14886
		protected Pattern[] patterns;

		// Token: 0x02000BB3 RID: 2995
		// (Invoke) Token: 0x060070E3 RID: 28899
		internal delegate LocalizedString LocalizedStringDescriptionDelegate(string patterns);
	}
}
