using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000037 RID: 55
	internal static class RuleUtils
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00006980 File Offset: 0x00004B80
		internal static bool CompareStringValues(object leftValue, object rightValue, IStringComparer comparer, ConditionEvaluationMode evaluationMode = ConditionEvaluationMode.Optimized, IList<string> matchingRightValues = null)
		{
			if (leftValue == null && rightValue == null)
			{
				if (evaluationMode == ConditionEvaluationMode.Full && matchingRightValues != null)
				{
					matchingRightValues.Add(string.Empty);
				}
				return true;
			}
			if (leftValue == null || rightValue == null)
			{
				return false;
			}
			string text = leftValue as string;
			if (text != null)
			{
				string text2 = rightValue as string;
				if (text2 != null)
				{
					bool flag = comparer.Equals(text2, text);
					if (flag && evaluationMode == ConditionEvaluationMode.Full && matchingRightValues != null)
					{
						matchingRightValues.Add(text2);
					}
					return flag;
				}
				IEnumerable<string> enumerable = rightValue as IEnumerable<string>;
				if (enumerable != null)
				{
					return RuleUtils.MatchAny(enumerable, text, comparer, evaluationMode, matchingRightValues);
				}
			}
			IEnumerable<string> enumerable2 = leftValue as IEnumerable<string>;
			if (enumerable2 != null)
			{
				string text3 = rightValue as string;
				if (text3 != null)
				{
					bool flag2 = RuleUtils.MatchAny(enumerable2, text3, comparer, ConditionEvaluationMode.Optimized, null);
					if (flag2 && evaluationMode == ConditionEvaluationMode.Full && matchingRightValues != null)
					{
						matchingRightValues.Add(text3);
					}
					return flag2;
				}
				IEnumerable<string> enumerable3 = rightValue as IEnumerable<string>;
				if (enumerable3 != null)
				{
					return RuleUtils.MatchAny(enumerable2, enumerable3, comparer, evaluationMode, matchingRightValues);
				}
			}
			throw new InvalidOperationException("Only string values are supported!");
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006A5C File Offset: 0x00004C5C
		internal static bool MatchAny(IEnumerable<string> stringCollection, string str, IStringComparer comparer, ConditionEvaluationMode mode, IList<string> matchingCollectionItems)
		{
			if (stringCollection == null)
			{
				return false;
			}
			bool result = false;
			foreach (string text in stringCollection)
			{
				if (comparer.Equals(text, str))
				{
					if (mode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
					result = true;
					matchingCollectionItems.Add(text);
				}
			}
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006AC4 File Offset: 0x00004CC4
		internal static bool MatchAny(IEnumerable<string> listX, IEnumerable<string> listY, IStringComparer comparer, ConditionEvaluationMode mode, IList<string> matchingCollectionItems)
		{
			if (listX == null || listY == null)
			{
				return false;
			}
			bool result = false;
			foreach (string y in listX)
			{
				foreach (string text in listY)
				{
					if (comparer.Equals(text, y))
					{
						if (mode == ConditionEvaluationMode.Optimized)
						{
							return true;
						}
						result = true;
						matchingCollectionItems.Add(text);
					}
				}
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006B68 File Offset: 0x00004D68
		public static string EscapeSpecialRegexCharacters(string input)
		{
			foreach (KeyValuePair<string, string> keyValuePair in RuleUtils.RegexEscapeMapping)
			{
				input = input.Replace(keyValuePair.Key, keyValuePair.Value);
			}
			return input;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006BCC File Offset: 0x00004DCC
		public static bool TryParseNullableDateTimeUtc(string input, out DateTime? outputDate)
		{
			if (string.IsNullOrEmpty(input))
			{
				outputDate = null;
				return true;
			}
			DateTime value;
			bool flag = DateTime.TryParse(input, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeUniversal, out value);
			if (flag)
			{
				outputDate = new DateTime?(value);
				return true;
			}
			outputDate = null;
			return false;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006C14 File Offset: 0x00004E14
		public static string DateTimeToUtcString(DateTime input)
		{
			return input.ToUniversalTime().ToString("u", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006C3A File Offset: 0x00004E3A
		internal static bool TryParseBool(string input, bool defaultValue, out bool output)
		{
			if (string.IsNullOrEmpty(input))
			{
				output = defaultValue;
				return true;
			}
			if (bool.TryParse(input, out output))
			{
				return true;
			}
			output = defaultValue;
			return false;
		}

		// Token: 0x040000AE RID: 174
		public const int FixedObjectOverhead = 18;

		// Token: 0x040000AF RID: 175
		public static readonly Dictionary<string, string> RegexEscapeMapping = new Dictionary<string, string>
		{
			{
				"\\",
				"\\\\"
			},
			{
				"|",
				"\\|"
			},
			{
				"$",
				"\\$"
			},
			{
				"^",
				"\\^"
			},
			{
				"*",
				"\\*"
			},
			{
				"(",
				"\\("
			},
			{
				")",
				"\\)"
			}
		};
	}
}
