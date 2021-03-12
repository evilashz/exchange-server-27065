using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000DB RID: 219
	internal static class RuleConstants
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x00010E74 File Offset: 0x0000F074
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

		// Token: 0x0600058C RID: 1420 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		internal static T TryParseEnum<T>(string enumText, T defaultValue) where T : struct
		{
			T result;
			if (string.IsNullOrEmpty(enumText) || !Enum.TryParse<T>(enumText, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00010ED4 File Offset: 0x0000F0D4
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

		// Token: 0x0400034D RID: 845
		internal const string TagRules = "rules";

		// Token: 0x0400034E RID: 846
		internal const string TagRule = "rule";

		// Token: 0x0400034F RID: 847
		internal const string TagVersion = "version";

		// Token: 0x04000350 RID: 848
		internal const string TagCondition = "condition";

		// Token: 0x04000351 RID: 849
		internal const string TagAction = "action";

		// Token: 0x04000352 RID: 850
		internal const string TagTrue = "true";

		// Token: 0x04000353 RID: 851
		internal const string TagFalse = "false";

		// Token: 0x04000354 RID: 852
		internal const string TagNot = "not";

		// Token: 0x04000355 RID: 853
		internal const string TagAnd = "and";

		// Token: 0x04000356 RID: 854
		internal const string TagOr = "or";

		// Token: 0x04000357 RID: 855
		internal const string TagArgument = "argument";

		// Token: 0x04000358 RID: 856
		internal const string TagGreaterThan = "greaterThan";

		// Token: 0x04000359 RID: 857
		internal const string TagLessThan = "lessThan";

		// Token: 0x0400035A RID: 858
		internal const string TagGreaterThanOrEqual = "greaterThanOrEqual";

		// Token: 0x0400035B RID: 859
		internal const string TagLessThanOrEqual = "lessThanOrEqual";

		// Token: 0x0400035C RID: 860
		internal const string TagEqual = "equal";

		// Token: 0x0400035D RID: 861
		internal const string TagNameValuesPairConfiguration = "NameValuesPairConfiguration";

		// Token: 0x0400035E RID: 862
		internal const string TagNotEqual = "notEqual";

		// Token: 0x0400035F RID: 863
		internal const string TagContains = "contains";

		// Token: 0x04000360 RID: 864
		internal const string TagMatches = "matches";

		// Token: 0x04000361 RID: 865
		internal const string TagMatchesRegex = "matchesRegex";

		// Token: 0x04000362 RID: 866
		internal const string TagIs = "is";

		// Token: 0x04000363 RID: 867
		internal const string TagExists = "exists";

		// Token: 0x04000364 RID: 868
		internal const string TagNotExists = "notExists";

		// Token: 0x04000365 RID: 869
		internal const string TagQueryMatch = "queryMatch";

		// Token: 0x04000366 RID: 870
		internal const string TagTextQueryMatch = "textQueryMatch";

		// Token: 0x04000367 RID: 871
		internal const string TagValue = "value";

		// Token: 0x04000368 RID: 872
		internal const string TagKeyValueCollection = "keyValues";

		// Token: 0x04000369 RID: 873
		internal const string TagKeyValue = "keyValue";

		// Token: 0x0400036A RID: 874
		internal const string AttributeKey = "key";

		// Token: 0x0400036B RID: 875
		internal const string TagList = "list";

		// Token: 0x0400036C RID: 876
		internal const string TagPartner = "partner";

		// Token: 0x0400036D RID: 877
		internal const string TagHold = "Hold";

		// Token: 0x0400036E RID: 878
		internal const string TagRetentionExpire = "RetentionExpire";

		// Token: 0x0400036F RID: 879
		internal const string TagRetentionRecycle = "RetentionRecycle";

		// Token: 0x04000370 RID: 880
		internal const string TypeInteger = "integer";

		// Token: 0x04000371 RID: 881
		internal const string TypeString = "string";

		// Token: 0x04000372 RID: 882
		internal const string TagRuleCOllectionName = "rulesVersioned";

		// Token: 0x04000373 RID: 883
		internal const string AttributeName = "name";

		// Token: 0x04000374 RID: 884
		internal const string AttributeExternalName = "externalName";

		// Token: 0x04000375 RID: 885
		internal const string AttributeComments = "comments";

		// Token: 0x04000376 RID: 886
		internal const string AttributeValue = "value";

		// Token: 0x04000377 RID: 887
		internal const string AttributeProperty = "property";

		// Token: 0x04000378 RID: 888
		internal const string AttributeType = "type";

		// Token: 0x04000379 RID: 889
		internal const string AttributeSupplementalInfo = "suppl";

		// Token: 0x0400037A RID: 890
		internal const string AttributeEnabled = "enabled";

		// Token: 0x0400037B RID: 891
		internal const string AttributeImmutableId = "id";

		// Token: 0x0400037C RID: 892
		internal const string AttributeExpiryDate = "expiryDate";

		// Token: 0x0400037D RID: 893
		internal const string AttributeActivationDate = "activationDate";

		// Token: 0x0400037E RID: 894
		internal const string AttributeException = "exception";

		// Token: 0x0400037F RID: 895
		internal const string AttributeMode = "mode";

		// Token: 0x04000380 RID: 896
		internal const string AttributeSubType = "subType";

		// Token: 0x04000381 RID: 897
		internal const string ErrorAction = "errorAction";

		// Token: 0x04000382 RID: 898
		internal const string EnabledTrue = "true";

		// Token: 0x04000383 RID: 899
		internal const string EnabledFalse = "false";

		// Token: 0x04000384 RID: 900
		internal const string RequiredMinimumVersion = "requiredMinVersion";

		// Token: 0x04000385 RID: 901
		internal const string TagAuditOperations = "auditOperations";

		// Token: 0x04000386 RID: 902
		internal const string TagBlockAccess = "BlockAccess";

		// Token: 0x04000387 RID: 903
		internal const string TagGenerateIncidentReportAction = "GenerateIncidentReport";

		// Token: 0x04000388 RID: 904
		internal const string TagNotifyAuthorsAction = "NotifyAuthors";

		// Token: 0x04000389 RID: 905
		internal const string TagContentMetadataContains = "contentMetadataContains";

		// Token: 0x0400038A RID: 906
		internal const string TagContentContainsDataClassification = "containsDataClassification";

		// Token: 0x020000DC RID: 220
		public static class RuleTagNames
		{
			// Token: 0x0400038B RID: 907
			internal const string Tags = "tags";

			// Token: 0x0400038C RID: 908
			internal const string Tag = "tag";

			// Token: 0x0400038D RID: 909
			internal const string NameAttribute = "name";

			// Token: 0x0400038E RID: 910
			internal const string TypeAttribute = "type";
		}
	}
}
