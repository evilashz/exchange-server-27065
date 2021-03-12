using System;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002A RID: 42
	internal static class RuleConstants
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004234 File Offset: 0x00002434
		internal static bool TryParseEnabled(string enabledString, out RuleState state)
		{
			if (enabledString != null)
			{
				if (enabledString == "true")
				{
					state = RuleState.Enabled;
					return true;
				}
				if (enabledString == "false")
				{
					state = RuleState.Disabled;
					return true;
				}
			}
			state = RuleState.Enabled;
			return false;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004270 File Offset: 0x00002470
		internal static T TryParseEnum<T>(string enumText, T defaultValue) where T : struct
		{
			T result;
			if (string.IsNullOrEmpty(enumText) || !Enum.TryParse<T>(enumText, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004294 File Offset: 0x00002494
		internal static string StringFromRuleState(RuleState state)
		{
			switch (state)
			{
			case RuleState.Enabled:
				return "true";
			case RuleState.Disabled:
				return "false";
			default:
				throw new InvalidOperationException("Invalid RuleState Enum Value.");
			}
		}

		// Token: 0x04000058 RID: 88
		internal const string TagRules = "rules";

		// Token: 0x04000059 RID: 89
		internal const string TagRule = "rule";

		// Token: 0x0400005A RID: 90
		internal const string TagVersion = "version";

		// Token: 0x0400005B RID: 91
		internal const string TagCondition = "condition";

		// Token: 0x0400005C RID: 92
		internal const string TagAction = "action";

		// Token: 0x0400005D RID: 93
		internal const string TagTrue = "true";

		// Token: 0x0400005E RID: 94
		internal const string TagFalse = "false";

		// Token: 0x0400005F RID: 95
		internal const string TagNot = "not";

		// Token: 0x04000060 RID: 96
		internal const string TagAnd = "and";

		// Token: 0x04000061 RID: 97
		internal const string TagOr = "or";

		// Token: 0x04000062 RID: 98
		internal const string TagArgument = "argument";

		// Token: 0x04000063 RID: 99
		internal const string TagGreaterThan = "greaterThan";

		// Token: 0x04000064 RID: 100
		internal const string TagLessThan = "lessThan";

		// Token: 0x04000065 RID: 101
		internal const string TagGreaterThanOrEqual = "greaterThanOrEqual";

		// Token: 0x04000066 RID: 102
		internal const string TagLessThanOrEqual = "lessThanOrEqual";

		// Token: 0x04000067 RID: 103
		internal const string TagEqual = "equal";

		// Token: 0x04000068 RID: 104
		internal const string TagNotEqual = "notEqual";

		// Token: 0x04000069 RID: 105
		internal const string TagContains = "contains";

		// Token: 0x0400006A RID: 106
		internal const string TagMatches = "matches";

		// Token: 0x0400006B RID: 107
		internal const string TagMatchesRegex = "matchesRegex";

		// Token: 0x0400006C RID: 108
		internal const string TagIs = "is";

		// Token: 0x0400006D RID: 109
		internal const string TagExists = "exists";

		// Token: 0x0400006E RID: 110
		internal const string TagNotExists = "notExists";

		// Token: 0x0400006F RID: 111
		internal const string TagValue = "value";

		// Token: 0x04000070 RID: 112
		internal const string TagKeyValueCollection = "keyValues";

		// Token: 0x04000071 RID: 113
		internal const string TagKeyValue = "keyValue";

		// Token: 0x04000072 RID: 114
		internal const string AttributeKey = "key";

		// Token: 0x04000073 RID: 115
		internal const string TagList = "list";

		// Token: 0x04000074 RID: 116
		internal const string TagPartner = "partner";

		// Token: 0x04000075 RID: 117
		internal const string TypeInteger = "integer";

		// Token: 0x04000076 RID: 118
		internal const string TypeString = "string";

		// Token: 0x04000077 RID: 119
		internal const string AttributeName = "name";

		// Token: 0x04000078 RID: 120
		internal const string AttributeExternalName = "externalName";

		// Token: 0x04000079 RID: 121
		internal const string AttributeComments = "comments";

		// Token: 0x0400007A RID: 122
		internal const string AttributeValue = "value";

		// Token: 0x0400007B RID: 123
		internal const string AttributeProperty = "property";

		// Token: 0x0400007C RID: 124
		internal const string AttributeType = "type";

		// Token: 0x0400007D RID: 125
		internal const string AttributeEnabled = "enabled";

		// Token: 0x0400007E RID: 126
		internal const string AttributeImmutableId = "id";

		// Token: 0x0400007F RID: 127
		internal const string AttributeExpiryDate = "expiryDate";

		// Token: 0x04000080 RID: 128
		internal const string AttributeActivationDate = "activationDate";

		// Token: 0x04000081 RID: 129
		internal const string AttributeException = "exception";

		// Token: 0x04000082 RID: 130
		internal const string AttributeMode = "mode";

		// Token: 0x04000083 RID: 131
		internal const string AttributeSubType = "subType";

		// Token: 0x04000084 RID: 132
		internal const string ErrorAction = "errorAction";

		// Token: 0x04000085 RID: 133
		internal const string EnabledTrue = "true";

		// Token: 0x04000086 RID: 134
		internal const string EnabledFalse = "false";

		// Token: 0x04000087 RID: 135
		internal const string RequiredMinimumVersion = "requiredMinVersion";

		// Token: 0x04000088 RID: 136
		internal const RuleErrorAction DefaultRuleErrorAction = RuleErrorAction.Ignore;

		// Token: 0x0200002B RID: 43
		internal struct RegexOptions
		{
			// Token: 0x04000089 RID: 137
			internal const CaseSensitivityMode CaseSensitivity = CaseSensitivityMode.Insensitive;

			// Token: 0x0400008A RID: 138
			internal const MatchRegexOptions MatchOptions = MatchRegexOptions.ExplicitCaptures;
		}

		// Token: 0x0200002C RID: 44
		public static class RuleTagNames
		{
			// Token: 0x0400008B RID: 139
			internal const string Tags = "tags";

			// Token: 0x0400008C RID: 140
			internal const string Tag = "tag";

			// Token: 0x0400008D RID: 141
			internal const string NameAttribute = "name";

			// Token: 0x0400008E RID: 142
			internal const string TypeAttribute = "type";
		}
	}
}
