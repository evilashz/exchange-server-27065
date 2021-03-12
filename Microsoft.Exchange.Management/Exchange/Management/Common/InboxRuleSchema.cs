using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InboxRuleSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x040003AC RID: 940
		public const string DescriptionTimeZoneParameterName = "DescriptionTimeZone";

		// Token: 0x040003AD RID: 941
		public const string DescriptionTimeFormatParameterName = "DescriptionTimeFormat";

		// Token: 0x040003AE RID: 942
		public const string EnabledParameterName = "Enabled";

		// Token: 0x040003AF RID: 943
		public const string IdentityParameterName = "Identity";

		// Token: 0x040003B0 RID: 944
		public const string InErrorParameterName = "InError";

		// Token: 0x040003B1 RID: 945
		public const string MailboxParameterName = "Mailbox";

		// Token: 0x040003B2 RID: 946
		public const string NameParameterName = "Name";

		// Token: 0x040003B3 RID: 947
		public const string PriorityParameterName = "Priority";

		// Token: 0x040003B4 RID: 948
		public const string RuleIdParameterName = "RuleId";

		// Token: 0x040003B5 RID: 949
		public const string RuleIdentityParameterName = "RuleIdentity";

		// Token: 0x040003B6 RID: 950
		public const string SupportedByTaskParameterName = "SupportedByTask";

		// Token: 0x040003B7 RID: 951
		public const string ExceptIf = "ExceptIf";

		// Token: 0x040003B8 RID: 952
		public const string BodyContainsWordsParameterName = "BodyContainsWords";

		// Token: 0x040003B9 RID: 953
		public const string FlaggedForActionParameterName = "FlaggedForAction";

		// Token: 0x040003BA RID: 954
		public const string FromParameterName = "From";

		// Token: 0x040003BB RID: 955
		public const string FromAddressContainsWordsParameterName = "FromAddressContainsWords";

		// Token: 0x040003BC RID: 956
		public const string HasAttachmentParameterName = "HasAttachment";

		// Token: 0x040003BD RID: 957
		public const string HasClassificationParameterName = "HasClassification";

		// Token: 0x040003BE RID: 958
		public const string HeaderContainsWordsParameterName = "HeaderContainsWords";

		// Token: 0x040003BF RID: 959
		public const string FromSubscriptionParameterName = "FromSubscription";

		// Token: 0x040003C0 RID: 960
		public const string MessageTypeMatchesParameterName = "MessageTypeMatches";

		// Token: 0x040003C1 RID: 961
		public const string MyNameInCcBoxParameterName = "MyNameInCcBox";

		// Token: 0x040003C2 RID: 962
		public const string MyNameInToBoxParameterName = "MyNameInToBox";

		// Token: 0x040003C3 RID: 963
		public const string MyNameInToOrCcBoxParameterName = "MyNameInToOrCcBox";

		// Token: 0x040003C4 RID: 964
		public const string MyNameNotInToBoxParameterName = "MyNameNotInToBox";

		// Token: 0x040003C5 RID: 965
		public const string ReceivedAfterDateParameterName = "ReceivedAfterDate";

		// Token: 0x040003C6 RID: 966
		public const string ReceivedBeforeDateParameterName = "ReceivedBeforeDate";

		// Token: 0x040003C7 RID: 967
		public const string RecipientAddressContainsWordsParameterName = "RecipientAddressContainsWords";

		// Token: 0x040003C8 RID: 968
		public const string SentOnlyToMeParameterName = "SentOnlyToMe";

		// Token: 0x040003C9 RID: 969
		public const string SentToParameterName = "SentTo";

		// Token: 0x040003CA RID: 970
		public const string SubjectContainsWordsParameterName = "SubjectContainsWords";

		// Token: 0x040003CB RID: 971
		public const string SubjectOrBodyContainsWordsParameterName = "SubjectOrBodyContainsWords";

		// Token: 0x040003CC RID: 972
		public const string WithImportanceParameterName = "WithImportance";

		// Token: 0x040003CD RID: 973
		public const string WithinSizeRangeMaximumParameterName = "WithinSizeRangeMaximum";

		// Token: 0x040003CE RID: 974
		public const string WithinSizeRangeMinimumParameterName = "WithinSizeRangeMinimum";

		// Token: 0x040003CF RID: 975
		public const string WithSensitivityParameterName = "WithSensitivity";

		// Token: 0x040003D0 RID: 976
		public const string ApplyCategoryParameterName = "ApplyCategory";

		// Token: 0x040003D1 RID: 977
		public const string CopyToFolderParameterName = "CopyToFolder";

		// Token: 0x040003D2 RID: 978
		public const string DeleteMessageParameterName = "DeleteMessage";

		// Token: 0x040003D3 RID: 979
		public const string ForwardAsAttachmentToParameterName = "ForwardAsAttachmentTo";

		// Token: 0x040003D4 RID: 980
		public const string ForwardToParameterName = "ForwardTo";

		// Token: 0x040003D5 RID: 981
		public const string MarkAsReadParameterName = "MarkAsRead";

		// Token: 0x040003D6 RID: 982
		public const string MarkImportanceParameterName = "MarkImportance";

		// Token: 0x040003D7 RID: 983
		public const string MoveToFolderParameterName = "MoveToFolder";

		// Token: 0x040003D8 RID: 984
		public const string RedirectToParameterName = "RedirectTo";

		// Token: 0x040003D9 RID: 985
		public const string SendTextMessageNotificationToParameterName = "SendTextMessageNotificationTo";

		// Token: 0x040003DA RID: 986
		public const string StopProcessingRulesParameterName = "StopProcessingRules";

		// Token: 0x040003DB RID: 987
		public const int MinimumSizeConstraint = 0;

		// Token: 0x040003DC RID: 988
		public const int MaximumSizeConstraint = 999999;

		// Token: 0x040003DD RID: 989
		public const int MaximumPriority = 2147483637;

		// Token: 0x040003DE RID: 990
		public const int MaximumWordLengthConstraint = 255;

		// Token: 0x040003DF RID: 991
		public static readonly SimpleProviderPropertyDefinition BodyContainsWords = new SimpleProviderPropertyDefinition("BodyContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003E0 RID: 992
		public static readonly SimpleProviderPropertyDefinition ExceptIfBodyContainsWords = new SimpleProviderPropertyDefinition("ExceptIfBodyContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003E1 RID: 993
		public static readonly SimpleProviderPropertyDefinition FlaggedForAction = new SimpleProviderPropertyDefinition("FlaggedForAction", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003E2 RID: 994
		public static readonly SimpleProviderPropertyDefinition ExceptIfFlaggedForAction = new SimpleProviderPropertyDefinition("ExceptIfFlaggedForAction", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003E3 RID: 995
		public static readonly SimpleProviderPropertyDefinition From = new SimpleProviderPropertyDefinition("From", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003E4 RID: 996
		public static readonly SimpleProviderPropertyDefinition ExceptIfFrom = new SimpleProviderPropertyDefinition("ExceptIfFrom", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003E5 RID: 997
		public static readonly SimpleProviderPropertyDefinition FromAddressContainsWords = new SimpleProviderPropertyDefinition("FromAddressContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003E6 RID: 998
		public static readonly SimpleProviderPropertyDefinition ExceptIfFromAddressContainsWords = new SimpleProviderPropertyDefinition("ExceptIfFromAddressContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003E7 RID: 999
		public static readonly SimpleProviderPropertyDefinition HasAttachment = new SimpleProviderPropertyDefinition("HasAttachment", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003E8 RID: 1000
		public static readonly SimpleProviderPropertyDefinition ExceptIfHasAttachment = new SimpleProviderPropertyDefinition("ExceptIfHasAttachment", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003E9 RID: 1001
		public static readonly SimpleProviderPropertyDefinition HasClassification = new SimpleProviderPropertyDefinition("HasClassification", ExchangeObjectVersion.Exchange2010, typeof(MessageClassification[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003EA RID: 1002
		public static readonly SimpleProviderPropertyDefinition ExceptIfHasClassification = new SimpleProviderPropertyDefinition("ExceptIfHasClassification", ExchangeObjectVersion.Exchange2010, typeof(MessageClassification[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003EB RID: 1003
		public static readonly SimpleProviderPropertyDefinition HeaderContainsWords = new SimpleProviderPropertyDefinition("HeaderContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003EC RID: 1004
		public static readonly SimpleProviderPropertyDefinition ExceptIfHeaderContainsWords = new SimpleProviderPropertyDefinition("ExceptIfHeaderContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003ED RID: 1005
		public static readonly SimpleProviderPropertyDefinition FromSubscription = new SimpleProviderPropertyDefinition("FromSubscription", ExchangeObjectVersion.Exchange2010, typeof(AggregationSubscriptionIdentity[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003EE RID: 1006
		public static readonly SimpleProviderPropertyDefinition ExceptIfFromSubscription = new SimpleProviderPropertyDefinition("ExceptIfFromSubscription", ExchangeObjectVersion.Exchange2010, typeof(AggregationSubscriptionIdentity[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003EF RID: 1007
		public static readonly SimpleProviderPropertyDefinition MessageTypeMatches = new SimpleProviderPropertyDefinition("MessageTypeMatches", ExchangeObjectVersion.Exchange2010, typeof(InboxRuleMessageType?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F0 RID: 1008
		public static readonly SimpleProviderPropertyDefinition ExceptIfMessageTypeMatches = new SimpleProviderPropertyDefinition("ExceptIfMessageTypeMatches", ExchangeObjectVersion.Exchange2010, typeof(InboxRuleMessageType?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F1 RID: 1009
		public static readonly SimpleProviderPropertyDefinition MyNameInCcBox = new SimpleProviderPropertyDefinition("MyNameInCcBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F2 RID: 1010
		public static readonly SimpleProviderPropertyDefinition ExceptIfMyNameInCcBox = new SimpleProviderPropertyDefinition("ExceptIfMyNameInCcBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F3 RID: 1011
		public static readonly SimpleProviderPropertyDefinition MyNameInToBox = new SimpleProviderPropertyDefinition("MyNameInToBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F4 RID: 1012
		public static readonly SimpleProviderPropertyDefinition ExceptIfMyNameInToBox = new SimpleProviderPropertyDefinition("ExceptIfMyNameInToBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F5 RID: 1013
		public static readonly SimpleProviderPropertyDefinition MyNameInToOrCcBox = new SimpleProviderPropertyDefinition("MyNameInToOrCcBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F6 RID: 1014
		public static readonly SimpleProviderPropertyDefinition ExceptIfMyNameInToOrCcBox = new SimpleProviderPropertyDefinition("ExceptIfMyNameInToOrCcBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F7 RID: 1015
		public static readonly SimpleProviderPropertyDefinition MyNameNotInToBox = new SimpleProviderPropertyDefinition("MyNameNotInToBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F8 RID: 1016
		public static readonly SimpleProviderPropertyDefinition ExceptIfMyNameNotInToBox = new SimpleProviderPropertyDefinition("ExceptIfMyNameNotInToBox", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003F9 RID: 1017
		public static readonly SimpleProviderPropertyDefinition ReceivedAfterDate = new SimpleProviderPropertyDefinition("ReceivedAfterDate", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003FA RID: 1018
		public static readonly SimpleProviderPropertyDefinition ExceptIfReceivedAfterDate = new SimpleProviderPropertyDefinition("ExceptIfReceivedAfterDate", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003FB RID: 1019
		public static readonly SimpleProviderPropertyDefinition ReceivedBeforeDate = new SimpleProviderPropertyDefinition("ReceivedBeforeDate", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003FC RID: 1020
		public static readonly SimpleProviderPropertyDefinition ExceptIfReceivedBeforeDate = new SimpleProviderPropertyDefinition("ExceptIfReceivedBeforeDate", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003FD RID: 1021
		public static readonly SimpleProviderPropertyDefinition RecipientAddressContainsWords = new SimpleProviderPropertyDefinition("RecipientAddressContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003FE RID: 1022
		public static readonly SimpleProviderPropertyDefinition ExceptIfRecipientAddressContainsWords = new SimpleProviderPropertyDefinition("ExceptIfRecipientAddressContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x040003FF RID: 1023
		public static readonly SimpleProviderPropertyDefinition SentOnlyToMe = new SimpleProviderPropertyDefinition("SentOnlyToMe", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000400 RID: 1024
		public static readonly SimpleProviderPropertyDefinition ExceptIfSentOnlyToMe = new SimpleProviderPropertyDefinition("ExceptIfSentOnlyToMe", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000401 RID: 1025
		public static readonly SimpleProviderPropertyDefinition SentTo = new SimpleProviderPropertyDefinition("SentTo", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000402 RID: 1026
		public static readonly SimpleProviderPropertyDefinition ExceptIfSentTo = new SimpleProviderPropertyDefinition("ExceptIfSentTo", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000403 RID: 1027
		public static readonly SimpleProviderPropertyDefinition SubjectContainsWords = new SimpleProviderPropertyDefinition("SubjectContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x04000404 RID: 1028
		public static readonly SimpleProviderPropertyDefinition ExceptIfSubjectContainsWords = new SimpleProviderPropertyDefinition("ExceptIfSubjectContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x04000405 RID: 1029
		public static readonly SimpleProviderPropertyDefinition SubjectOrBodyContainsWords = new SimpleProviderPropertyDefinition("SubjectOrBodyContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x04000406 RID: 1030
		public static readonly SimpleProviderPropertyDefinition ExceptIfSubjectOrBodyContainsWords = new SimpleProviderPropertyDefinition("ExceptIfSubjectOrBodyContainsWords", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x04000407 RID: 1031
		public static readonly SimpleProviderPropertyDefinition WithImportance = new SimpleProviderPropertyDefinition("WithImportance", ExchangeObjectVersion.Exchange2010, typeof(Importance?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000408 RID: 1032
		public static readonly SimpleProviderPropertyDefinition ExceptIfWithImportance = new SimpleProviderPropertyDefinition("ExceptIfWithImportance", ExchangeObjectVersion.Exchange2010, typeof(Importance?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000409 RID: 1033
		public static readonly SimpleProviderPropertyDefinition WithinSizeRangeMaximum = new SimpleProviderPropertyDefinition("WithinSizeRangeMaximum", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		});

		// Token: 0x0400040A RID: 1034
		public static readonly SimpleProviderPropertyDefinition ExceptIfWithinSizeRangeMaximum = new SimpleProviderPropertyDefinition("ExceptIfWithinSizeRangeMaximum", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		});

		// Token: 0x0400040B RID: 1035
		public static readonly SimpleProviderPropertyDefinition WithinSizeRangeMinimum = new SimpleProviderPropertyDefinition("WithinSizeRangeMinimum", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		});

		// Token: 0x0400040C RID: 1036
		public static readonly SimpleProviderPropertyDefinition ExceptIfWithinSizeRangeMinimum = new SimpleProviderPropertyDefinition("ExceptIfWithinSizeRangeMinimum", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		});

		// Token: 0x0400040D RID: 1037
		public static readonly SimpleProviderPropertyDefinition WithSensitivity = new SimpleProviderPropertyDefinition("WithSensitivity", ExchangeObjectVersion.Exchange2010, typeof(Sensitivity?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400040E RID: 1038
		public static readonly SimpleProviderPropertyDefinition ExceptIfWithSensitivity = new SimpleProviderPropertyDefinition("ExceptIfWithSensitivity", ExchangeObjectVersion.Exchange2010, typeof(Sensitivity?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400040F RID: 1039
		public static readonly SimpleProviderPropertyDefinition ApplyCategory = new SimpleProviderPropertyDefinition("ApplyCategory", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255)
		});

		// Token: 0x04000410 RID: 1040
		public static readonly SimpleProviderPropertyDefinition CopyToFolder = new SimpleProviderPropertyDefinition("CopyToFolder", ExchangeObjectVersion.Exchange2010, typeof(MailboxFolder), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000411 RID: 1041
		public static readonly SimpleProviderPropertyDefinition DeleteMessage = new SimpleProviderPropertyDefinition("DeleteMessage", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000412 RID: 1042
		public static readonly SimpleProviderPropertyDefinition ForwardAsAttachmentTo = new SimpleProviderPropertyDefinition("ForwardAsAttachmentTo", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000413 RID: 1043
		public static readonly SimpleProviderPropertyDefinition ForwardTo = new SimpleProviderPropertyDefinition("ForwardTo", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000414 RID: 1044
		public static readonly SimpleProviderPropertyDefinition MarkAsRead = new SimpleProviderPropertyDefinition("MarkAsRead", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000415 RID: 1045
		public static readonly SimpleProviderPropertyDefinition MarkImportance = new SimpleProviderPropertyDefinition("MarkImportance", ExchangeObjectVersion.Exchange2010, typeof(Importance?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000416 RID: 1046
		public static readonly SimpleProviderPropertyDefinition MoveToFolder = new SimpleProviderPropertyDefinition("MoveToFolder", ExchangeObjectVersion.Exchange2010, typeof(MailboxFolder), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000417 RID: 1047
		public static readonly SimpleProviderPropertyDefinition RedirectTo = new SimpleProviderPropertyDefinition("RedirectTo", ExchangeObjectVersion.Exchange2010, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000418 RID: 1048
		public static readonly SimpleProviderPropertyDefinition SendTextMessageNotificationTo = new SimpleProviderPropertyDefinition("SendTextMessageNotificationTo", ExchangeObjectVersion.Exchange2010, typeof(E164Number), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000419 RID: 1049
		public static readonly SimpleProviderPropertyDefinition StopProcessingRules = new SimpleProviderPropertyDefinition("StopProcessingRules", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400041A RID: 1050
		public static readonly SimpleProviderPropertyDefinition Enabled = new SimpleProviderPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400041B RID: 1051
		public static readonly SimpleProviderPropertyDefinition InError = new SimpleProviderPropertyDefinition("InError", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400041C RID: 1052
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 512)
		});

		// Token: 0x0400041D RID: 1053
		public static readonly SimpleProviderPropertyDefinition Priority = new SimpleProviderPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 2147483637)
		});

		// Token: 0x0400041E RID: 1054
		public static readonly SimpleProviderPropertyDefinition RuleId = new SimpleProviderPropertyDefinition("RuleId", ExchangeObjectVersion.Exchange2010, typeof(RuleId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400041F RID: 1055
		public static readonly SimpleProviderPropertyDefinition SupportedByTask = new SimpleProviderPropertyDefinition("SupportedByTask", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000420 RID: 1056
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(InboxRuleId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			InboxRuleSchema.Name,
			InboxRuleSchema.RuleId,
			XsoMailboxConfigurationObjectSchema.MailboxOwnerId
		}, null, new GetterDelegate(InboxRule.IdentityGetter), null);

		// Token: 0x04000421 RID: 1057
		public static readonly SimpleProviderPropertyDefinition RuleIdentity = new SimpleProviderPropertyDefinition("RuleIdentity", ExchangeObjectVersion.Exchange2010, typeof(ulong?), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			InboxRuleSchema.RuleId
		}, null, new GetterDelegate(InboxRule.RuleIdentityGetter), null);

		// Token: 0x04000422 RID: 1058
		public static readonly SimpleProviderPropertyDefinition MachineToPersonTextMessagingDisabled = new SimpleProviderPropertyDefinition("MachineToPersonTextMessagingDisabled", ExchangeObjectVersion.Exchange2010, typeof(object), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000423 RID: 1059
		public static readonly SimpleProviderPropertyDefinition StoreObjectInError = new SimpleProviderPropertyDefinition("StoreObjectInError", ExchangeObjectVersion.Exchange2010, typeof(object), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
