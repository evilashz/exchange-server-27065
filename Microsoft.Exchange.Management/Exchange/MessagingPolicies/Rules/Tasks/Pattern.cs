using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.TextMatching;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B5E RID: 2910
	[Serializable]
	public struct Pattern : IComparable, ISerializable
	{
		// Token: 0x06006AE9 RID: 27369 RVA: 0x001B6620 File Offset: 0x001B4820
		public Pattern(string input)
		{
			this = default(Pattern);
			this.isLegacyRegex = true;
			this.ignoreMaxLength = false;
			if (this.IsValid(input))
			{
				try
				{
					Pattern.ValidatePattern(input, this.isLegacyRegex, false);
					this.value = input;
				}
				catch (ArgumentException)
				{
				}
				if (string.IsNullOrEmpty(this.value))
				{
					this.isLegacyRegex = false;
					Pattern.ValidatePattern(input, this.isLegacyRegex, false);
				}
				this.value = input;
				return;
			}
			throw new ValidationArgumentException(Strings.ErrorPatternIsTooLong(input, 128), null);
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x001B66B0 File Offset: 0x001B48B0
		private Pattern(SerializationInfo info, StreamingContext context)
		{
			this = new Pattern((string)info.GetValue("value", typeof(string)), false, false);
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x001B66D4 File Offset: 0x001B48D4
		internal Pattern(string input, bool useLegacyRegex, bool ignoreMaxLength = false)
		{
			this = default(Pattern);
			this.isLegacyRegex = useLegacyRegex;
			this.ignoreMaxLength = ignoreMaxLength;
			if (this.IsValid(input))
			{
				Pattern.ValidatePattern(input, this.isLegacyRegex, false);
				this.value = input;
				return;
			}
			throw new ValidationArgumentException(Strings.ErrorPatternIsTooLong(input, 128), null);
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x06006AEC RID: 27372 RVA: 0x001B6728 File Offset: 0x001B4928
		public static Pattern Empty
		{
			get
			{
				return default(Pattern);
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x06006AED RID: 27373 RVA: 0x001B673E File Offset: 0x001B493E
		public string Value
		{
			get
			{
				if (this.IsValid(this.value))
				{
					Pattern.ValidatePattern(this.value, this.isLegacyRegex, false);
					return this.value;
				}
				throw new ValidationArgumentException(Strings.ErrorPatternIsTooLong(this.value, 128), null);
			}
		}

		// Token: 0x06006AEE RID: 27374 RVA: 0x001B677D File Offset: 0x001B497D
		public static Pattern Parse(string s, bool useLegacyRegex = true)
		{
			return new Pattern(s, useLegacyRegex, false);
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x001B6787 File Offset: 0x001B4987
		public static bool operator ==(Pattern a, Pattern b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x001B679C File Offset: 0x001B499C
		public static bool operator !=(Pattern a, Pattern b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x001B67B1 File Offset: 0x001B49B1
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x001B67C4 File Offset: 0x001B49C4
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value;
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x001B67DA File Offset: 0x001B49DA
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x001B67FA File Offset: 0x001B49FA
		public override bool Equals(object obj)
		{
			return obj is Pattern && this.Equals((Pattern)obj);
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x001B6812 File Offset: 0x001B4A12
		public bool Equals(Pattern obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x001B6828 File Offset: 0x001B4A28
		public int CompareTo(object obj)
		{
			if (!(obj is Pattern))
			{
				throw new ArgumentException("Parameter is not of type Pattern.");
			}
			return string.Compare(this.value, ((Pattern)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x001B6862 File Offset: 0x001B4A62
		private bool IsValid(string input)
		{
			return input != null && (this.ignoreMaxLength || input.Length <= 128);
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x001B6884 File Offset: 0x001B4A84
		public static void ValidatePattern(string input, bool useLegacyRegex, bool ignoreConstraints = false)
		{
			if (input == null)
			{
				throw new ValidationArgumentException(Strings.Pattern, null);
			}
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ValidationArgumentException(Strings.PatternIsWhiteSpace, null);
			}
			if (useLegacyRegex)
			{
				RegexParser regexParser = new RegexParser(input, true);
				try
				{
					regexParser.Parse();
					return;
				}
				catch (TextMatchingParsingException ex)
				{
					throw new ValidationArgumentException(ex.LocalizedString, null);
				}
			}
			try
			{
				new MatchFactory().CreateRegex(input, CaseSensitivityMode.Insensitive, MatchRegexOptions.ExplicitCaptures);
			}
			catch (ArgumentException innerException)
			{
				throw new ValidationArgumentException(Strings.Pattern, innerException);
			}
			if (!ignoreConstraints)
			{
				Pattern.ValidatePatternDoesNotBeginOrEndWithWildcards(input);
				Pattern.ValidatePatternDoesNotContainGroupsOrAssertionsWithWildcards(input);
				Pattern.ValidatePatternDoesNotContainMultiMatchOnGroupsOrAssertions(input);
				Pattern.ValidatePatternDoesNotHaveSequentialIdenticalMultiMatches(input);
				Pattern.ValidatePatternDoesNotContainEmptyAlternations(input);
			}
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x001B6930 File Offset: 0x001B4B30
		internal static void ValidatePatternDoesNotBeginOrEndWithWildcards(string input)
		{
			Regex regex = new Regex(Pattern.beginsWithWildcardMultiMatchPattern);
			Regex regex2 = new Regex("^\\^\\.(\\*|\\+)");
			if (regex.IsMatch(input) || regex2.IsMatch(input))
			{
				throw new ValidationArgumentException(Strings.ErrorPatternCannotBeginWithWildcardMultiMatch, null);
			}
			Regex regex3 = new Regex(Pattern.endsWithWildcardMultiMatchPattern);
			Regex regex4 = new Regex(Pattern.endsWithWildcardMultiMatchAnchoredPattern);
			Match match = regex3.Match(input);
			Match match2 = regex4.Match(input);
			if ((match.Success && !Pattern.IsSequenceLengthOdd(match.Groups["LeadingBackslashes"].Value)) || (match2.Success && !Pattern.IsSequenceLengthOdd(match2.Groups["LeadingBackslashes"].Value)))
			{
				throw new ValidationArgumentException(Strings.ErrorPatternCannotEndWithWildcardMultiMatch, null);
			}
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x001B69F1 File Offset: 0x001B4BF1
		internal static void ValidatePatternDoesNotContainGroupsOrAssertionsWithWildcards(string input)
		{
			if (Regex.IsMatch(Pattern.StripBackslashPairs(input), Pattern.groupOrAssertionContainsMultiMatchWildcard))
			{
				throw new ValidationArgumentException(Strings.ErrorPatternCannotContainGroupOrAssertionWithMultiMatchWildcard, null);
			}
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x001B6A11 File Offset: 0x001B4C11
		internal static void ValidatePatternDoesNotContainMultiMatchOnGroupsOrAssertions(string input)
		{
			if (Regex.IsMatch(Pattern.StripBackslashPairs(input), Pattern.unboundedGroupRepeater))
			{
				throw new ValidationArgumentException(Strings.ErrorPatternCannotContainMultiMatchOnGroupOrAssertion, null);
			}
		}

		// Token: 0x06006AFC RID: 27388 RVA: 0x001B6A34 File Offset: 0x001B4C34
		internal static void ValidatePatternDoesNotHaveSequentialIdenticalMultiMatches(string input)
		{
			Regex regex = new Regex(Pattern.sequentialIdenticalMultiMatches);
			MatchCollection matchCollection = regex.Matches(input);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text = match.Groups["FirstCharacter"].Value;
				if (Pattern.IsSequenceLengthOdd(match.Groups["LeadingBackslashes"].Value))
				{
					text = "\\" + text;
				}
				string text2 = match.Groups["SecondCharacter"].Value;
				if (text != "\\" && (text == text2 || text == "." || text2 == "."))
				{
					throw new ValidationArgumentException(Strings.ErrorPatternCannotContainSequentialIdenticalMultiMatchPatterns, null);
				}
			}
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x001B6B34 File Offset: 0x001B4D34
		internal static void ValidatePatternDoesNotContainEmptyAlternations(string input)
		{
			string input2 = Pattern.StripBackslashPairs(input);
			if (Regex.IsMatch(input2, Pattern.beginsWithEmptyAlternation) || Regex.IsMatch(input2, Pattern.endsWithEmptyAlternation))
			{
				throw new ValidationArgumentException(Strings.ErrorPatternCannotContainEmptyAlternations, null);
			}
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x001B6B6E File Offset: 0x001B4D6E
		internal void ValidatePatternDoesNotExceedCpuTimeLimit(long limit)
		{
			if (!Utils.IsRegexExecutionTimeWithinLimit(this.value, limit, false))
			{
				throw new ValidationArgumentException(Strings.ErrorPatternIsTooExpensive(this.value), null);
			}
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x001B6BBA File Offset: 0x001B4DBA
		private static void ThrowValidationArgumentExceptionIfMatchesAndDoesNotLeadWithEscapeCharacter(Regex regex, string input, LocalizedString errorMessage)
		{
			if (regex.Matches(input).Cast<Match>().Any((Match match) => match.Success && !Pattern.IsSequenceLengthOdd(match.Groups["LeadingBackslashes"].Value)))
			{
				throw new ValidationArgumentException(errorMessage, null);
			}
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x001B6BF4 File Offset: 0x001B4DF4
		private static string StripBackslashPairs(string input)
		{
			return Regex.Replace(input, Pattern.backslashPair, string.Empty);
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x001B6C06 File Offset: 0x001B4E06
		internal static bool IsSequenceLengthOdd(string sequence)
		{
			return sequence.Length % 2 == 1;
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x001B6C13 File Offset: 0x001B4E13
		internal void SuppressPiiData()
		{
			this.value = SuppressingPiiData.Redact(this.value);
		}

		// Token: 0x040036D6 RID: 14038
		public const int MaxLength = 128;

		// Token: 0x040036D7 RID: 14039
		public const string AllowedCharacters = ".";

		// Token: 0x040036D8 RID: 14040
		private const string leadingBackslashesGroupName = "LeadingBackslashes";

		// Token: 0x040036D9 RID: 14041
		private const string beginsWithWildcardMultiMatchAnchoredPattern = "^\\^\\.(\\*|\\+)";

		// Token: 0x040036DA RID: 14042
		private string value;

		// Token: 0x040036DB RID: 14043
		private readonly bool isLegacyRegex;

		// Token: 0x040036DC RID: 14044
		private readonly bool ignoreMaxLength;

		// Token: 0x040036DD RID: 14045
		private static readonly string leadingBackslashesGroupPattern = string.Format("(?<{0}>\\\\*)", "LeadingBackslashes");

		// Token: 0x040036DE RID: 14046
		private static readonly string boundedRepeaterWithMinimalLowerBoundPattern = "((\\{(?<!\\\\\\{))[01],\\d*(\\}(?<!\\\\\\})))";

		// Token: 0x040036DF RID: 14047
		private static readonly string beginsWithWildcardMultiMatchPattern = string.Format("^\\.(\\*|\\+|{0})", Pattern.boundedRepeaterWithMinimalLowerBoundPattern);

		// Token: 0x040036E0 RID: 14048
		private static readonly string endsWithWildcardMultiMatchPattern = string.Format("{0}\\.(\\*|\\+|{1})$", Pattern.leadingBackslashesGroupPattern, Pattern.boundedRepeaterWithMinimalLowerBoundPattern);

		// Token: 0x040036E1 RID: 14049
		private static readonly string endsWithWildcardMultiMatchAnchoredPattern = string.Format("{0}\\.(\\*|\\+)\\$$", Pattern.leadingBackslashesGroupPattern);

		// Token: 0x040036E2 RID: 14050
		private static string groupOrAssertionContainsMultiMatchWildcard = "\\((?<!\\\\\\().*?\\.(?<!\\\\\\.)" + string.Format("([\\*\\+]|{0})", Pattern.boundedRepeaterWithMinimalLowerBoundPattern) + ".*?\\)(?<!\\\\\\))";

		// Token: 0x040036E3 RID: 14051
		private static readonly string sequentialIdenticalMultiMatches = string.Format("{0}(?<FirstCharacter>.)(\\*|\\+)(?<SecondCharacter>\\\\?.)(\\*|\\+)", Pattern.leadingBackslashesGroupPattern);

		// Token: 0x040036E4 RID: 14052
		private static string backslashPair = "\\\\\\\\";

		// Token: 0x040036E5 RID: 14053
		private static readonly string beginsWithEmptyAlternation = "(\\(|^)(?<!\\\\\\()\\|(?<!\\\\\\|)";

		// Token: 0x040036E6 RID: 14054
		private static readonly string endsWithEmptyAlternation = "\\|(?<!\\\\\\|)(\\)|$)(?<!\\\\\\))";

		// Token: 0x040036E7 RID: 14055
		private static readonly string unboundedGroupRepeater = "\\((?<!\\\\\\().*?\\)(?<!\\\\\\))(\\*|\\+)";

		// Token: 0x040036E8 RID: 14056
		internal static readonly string BoundedRepeaterPattern = "((\\{(?<!\\\\\\{))\\d+(,\\d*)?(\\}(?<!\\\\\\})))";
	}
}
