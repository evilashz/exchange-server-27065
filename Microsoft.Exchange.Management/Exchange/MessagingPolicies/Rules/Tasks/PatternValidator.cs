using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B5F RID: 2911
	internal class PatternValidator
	{
		// Token: 0x06006B05 RID: 27397 RVA: 0x001B6D08 File Offset: 0x001B4F08
		internal static IEnumerable<ValidationError> ValidatePatterns(IEnumerable<Pattern> patterns, string supplementalInfo, bool useLegacyRegex)
		{
			if (patterns == null)
			{
				return new List<ValidationError>
				{
					new RulePhrase.RulePhraseValidationError(Strings.ArgumentNotSet, supplementalInfo)
				};
			}
			return PatternValidator.ValidatePatterns(from pattern in patterns
			select pattern.Value, supplementalInfo, useLegacyRegex);
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x001B6D5C File Offset: 0x001B4F5C
		internal static IEnumerable<ValidationError> ValidatePatterns(IEnumerable<string> patterns, string supplementalInfo, bool useLegacyRegex)
		{
			List<ValidationError> list = new List<ValidationError>();
			if (patterns == null || !patterns.Any<string>())
			{
				list.Add(new RulePhrase.RulePhraseValidationError(Strings.ArgumentNotSet, supplementalInfo));
				return list;
			}
			foreach (string text in patterns)
			{
				if (!string.IsNullOrEmpty(text))
				{
					int index = -1;
					if (!Utils.CheckIsUnicodeStringWellFormed(text, out index))
					{
						list.Add(new RulePhrase.RulePhraseValidationError(Strings.CommentsHaveInvalidChars((int)text[index]), supplementalInfo));
						continue;
					}
				}
				try
				{
					Pattern.ValidatePattern(text, useLegacyRegex, false);
				}
				catch (ArgumentException)
				{
					list.Add(new RulePhrase.RulePhraseValidationError(Strings.InvalidRegex(text), supplementalInfo));
				}
			}
			if (useLegacyRegex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in patterns)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("|");
					}
					stringBuilder.Append("(");
					stringBuilder.Append(value);
					stringBuilder.Append(")");
				}
				try
				{
					Pattern.ValidatePattern(stringBuilder.ToString(), useLegacyRegex, false);
				}
				catch (ArgumentException)
				{
					list.Add(new RulePhrase.RulePhraseValidationError(Strings.InvalidRegex(stringBuilder.ToString()), supplementalInfo));
				}
			}
			if (!list.Any<ValidationError>())
			{
				return null;
			}
			return list;
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x001B6ED8 File Offset: 0x001B50D8
		internal static IEnumerable<ValidationError> ValidateAdAttributePatterns(IEnumerable<Pattern> patterns, string supplementalInfo, bool useLegacyRegex)
		{
			List<ValidationError> list = new List<ValidationError>();
			if (patterns == null || !patterns.Any<Pattern>())
			{
				list.Add(new RulePhrase.RulePhraseValidationError(Strings.ArgumentNotSet, supplementalInfo));
				return list;
			}
			foreach (Pattern pattern in patterns)
			{
				int num = pattern.Value.IndexOf(':');
				if (num >= 0)
				{
					string text = pattern.Value.Substring(0, num).Trim().ToLowerInvariant();
					if (!TransportUtils.GetDisclaimerMacroLookupTable().ContainsKey(text))
					{
						list.Add(new RulePhrase.RulePhraseValidationError(Strings.InvalidMacroName(text), supplementalInfo));
					}
					else
					{
						string text2 = pattern.Value.Substring(num + 1);
						string[] patterns2 = text2.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						IEnumerable<ValidationError> enumerable = PatternValidator.ValidatePatterns(patterns2, supplementalInfo, useLegacyRegex);
						if (enumerable != null)
						{
							list.AddRange(enumerable);
						}
					}
				}
				else
				{
					list.Add(new RulePhrase.RulePhraseValidationError(Strings.MacroNameNotSpecified(pattern.Value), supplementalInfo));
				}
			}
			if (!list.Any<ValidationError>())
			{
				return null;
			}
			return list;
		}
	}
}
