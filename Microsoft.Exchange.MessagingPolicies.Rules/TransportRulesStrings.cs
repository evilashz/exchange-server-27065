using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000A5 RID: 165
	internal static class TransportRulesStrings
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x000170A0 File Offset: 0x000152A0
		static TransportRulesStrings()
		{
			TransportRulesStrings.stringIDs.Add(1496915101U, "No");
			TransportRulesStrings.stringIDs.Add(3611143976U, "IncidentReportFalsePositiveLine");
			TransportRulesStrings.stringIDs.Add(3658274981U, "RuleNotInAd");
			TransportRulesStrings.stringIDs.Add(2679419348U, "IncidentReportBccLine");
			TransportRulesStrings.stringIDs.Add(1273971392U, "IncidentReportDataClassificationLine");
			TransportRulesStrings.stringIDs.Add(2547444043U, "InvalidPriority");
			TransportRulesStrings.stringIDs.Add(1190951733U, "InvalidFilteringServiceResult");
			TransportRulesStrings.stringIDs.Add(104102616U, "IncidentReportSubjectLine");
			TransportRulesStrings.stringIDs.Add(497835657U, "FailedToLoadAttachmentFilteringConfigOnStartup");
			TransportRulesStrings.stringIDs.Add(3336243037U, "IncidentReportDataClassifications");
			TransportRulesStrings.stringIDs.Add(3453011747U, "IncidentReportSender");
			TransportRulesStrings.stringIDs.Add(4028858231U, "InvalidDataClassification");
			TransportRulesStrings.stringIDs.Add(3712824958U, "SenderAddressLocationHeaderOrEnvelope");
			TransportRulesStrings.stringIDs.Add(101970590U, "IncidentReportDlpPolicyLine");
			TransportRulesStrings.stringIDs.Add(1778028774U, "IncidentReportActionLine");
			TransportRulesStrings.stringIDs.Add(1905970586U, "IncidentReportBcc");
			TransportRulesStrings.stringIDs.Add(3729543170U, "IncidentReportOverride");
			TransportRulesStrings.stringIDs.Add(3959763416U, "IncidentReportOverrideLine");
			TransportRulesStrings.stringIDs.Add(429892252U, "IncidentReportCc");
			TransportRulesStrings.stringIDs.Add(2422663214U, "IncidentReportIdMatchLine");
			TransportRulesStrings.stringIDs.Add(90811307U, "SenderAddressLocationHeader");
			TransportRulesStrings.stringIDs.Add(29398792U, "IncidentReportDoNotIncludeOriginalMail");
			TransportRulesStrings.stringIDs.Add(3226449092U, "IncidentReportIdMatch");
			TransportRulesStrings.stringIDs.Add(1864226526U, "IncidentReportCcLine");
			TransportRulesStrings.stringIDs.Add(750089942U, "IncidentReportJustificationLine");
			TransportRulesStrings.stringIDs.Add(3384738962U, "IncidentReportIdContext");
			TransportRulesStrings.stringIDs.Add(3955406766U, "IncidentReportRecipients");
			TransportRulesStrings.stringIDs.Add(1755226983U, "IncidentReportSeverity");
			TransportRulesStrings.stringIDs.Add(3488352230U, "IncidentReportFalsePositive");
			TransportRulesStrings.stringIDs.Add(1389339898U, "IncidentReportIncludeOriginalMail");
			TransportRulesStrings.stringIDs.Add(2794317127U, "IncidentReportValue");
			TransportRulesStrings.stringIDs.Add(414074715U, "UnableToUpdateRuleInAd");
			TransportRulesStrings.stringIDs.Add(1090675464U, "IncidentReportRuleDetections");
			TransportRulesStrings.stringIDs.Add(3021629903U, "Yes");
			TransportRulesStrings.stringIDs.Add(3700793673U, "IncidentReportToLine");
			TransportRulesStrings.stringIDs.Add(2156460713U, "IncidentReportSeverityLine");
			TransportRulesStrings.stringIDs.Add(492774025U, "IncidentReportRuleHitLine");
			TransportRulesStrings.stringIDs.Add(594567544U, "IncidentReportConfidenceLine");
			TransportRulesStrings.stringIDs.Add(4096407337U, "IncidentReportSenderLine");
			TransportRulesStrings.stringIDs.Add(863777729U, "IncidentReportRecommendedMinimumConfidenceLine");
			TransportRulesStrings.stringIDs.Add(234820282U, "IncidentReportMessageIdLine");
			TransportRulesStrings.stringIDs.Add(3943613787U, "IncidentReportAttachOriginalMail");
			TransportRulesStrings.stringIDs.Add(4271028853U, "IncidentReportMoreRecipients");
			TransportRulesStrings.stringIDs.Add(2110678538U, "IncidentReportSubject");
			TransportRulesStrings.stringIDs.Add(1898574625U, "IncidentReportCountLine");
			TransportRulesStrings.stringIDs.Add(4151534495U, "IncidentReportDisclaimer");
			TransportRulesStrings.stringIDs.Add(2536257678U, "SenderAddressLocationEnvelope");
			TransportRulesStrings.stringIDs.Add(4076992413U, "IncidentReportIdMatchValue");
			TransportRulesStrings.stringIDs.Add(3534659841U, "IncidentReportContextLine");
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000174B0 File Offset: 0x000156B0
		public static LocalizedString FailedToRegisterForConfigChangeNotification(string agentName)
		{
			return new LocalizedString("FailedToRegisterForConfigChangeNotification", "Ex07B14C", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000174DF File Offset: 0x000156DF
		public static LocalizedString No
		{
			get
			{
				return new LocalizedString("No", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000174FD File Offset: 0x000156FD
		public static LocalizedString IncidentReportFalsePositiveLine
		{
			get
			{
				return new LocalizedString("IncidentReportFalsePositiveLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001751B File Offset: 0x0001571B
		public static LocalizedString RuleNotInAd
		{
			get
			{
				return new LocalizedString("RuleNotInAd", "ExC86500", false, true, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001753C File Offset: 0x0001573C
		public static LocalizedString ErrorInvokingFilteringService(int errorCode, string errorDescription)
		{
			return new LocalizedString("ErrorInvokingFilteringService", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				errorCode,
				errorDescription
			});
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00017574 File Offset: 0x00015774
		public static LocalizedString IncidentReportBccLine
		{
			get
			{
				return new LocalizedString("IncidentReportBccLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00017594 File Offset: 0x00015794
		public static LocalizedString InvalidReportDestinationArgument(object destination)
		{
			return new LocalizedString("InvalidReportDestinationArgument", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				destination
			});
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000175C3 File Offset: 0x000157C3
		public static LocalizedString IncidentReportDataClassificationLine
		{
			get
			{
				return new LocalizedString("IncidentReportDataClassificationLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000175E1 File Offset: 0x000157E1
		public static LocalizedString InvalidPriority
		{
			get
			{
				return new LocalizedString("InvalidPriority", "ExDE496A", false, true, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000175FF File Offset: 0x000157FF
		public static LocalizedString InvalidFilteringServiceResult
		{
			get
			{
				return new LocalizedString("InvalidFilteringServiceResult", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00017620 File Offset: 0x00015820
		public static LocalizedString InvalidPropertyValueType(string name)
		{
			return new LocalizedString("InvalidPropertyValueType", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001764F File Offset: 0x0001584F
		public static LocalizedString IncidentReportSubjectLine
		{
			get
			{
				return new LocalizedString("IncidentReportSubjectLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0001766D File Offset: 0x0001586D
		public static LocalizedString FailedToLoadAttachmentFilteringConfigOnStartup
		{
			get
			{
				return new LocalizedString("FailedToLoadAttachmentFilteringConfigOnStartup", "Ex0A986B", false, true, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001768B File Offset: 0x0001588B
		public static LocalizedString IncidentReportDataClassifications
		{
			get
			{
				return new LocalizedString("IncidentReportDataClassifications", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000176AC File Offset: 0x000158AC
		public static LocalizedString DataClassificationPropertyRequired(string name)
		{
			return new LocalizedString("DataClassificationPropertyRequired", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000176DB File Offset: 0x000158DB
		public static LocalizedString IncidentReportSender
		{
			get
			{
				return new LocalizedString("IncidentReportSender", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000176F9 File Offset: 0x000158F9
		public static LocalizedString InvalidDataClassification
		{
			get
			{
				return new LocalizedString("InvalidDataClassification", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00017718 File Offset: 0x00015918
		public static LocalizedString FailedToReadTransportSettingsConfiguration(string agentName)
		{
			return new LocalizedString("FailedToReadTransportSettingsConfiguration", "Ex00F967", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00017747 File Offset: 0x00015947
		public static LocalizedString SenderAddressLocationHeaderOrEnvelope
		{
			get
			{
				return new LocalizedString("SenderAddressLocationHeaderOrEnvelope", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00017765 File Offset: 0x00015965
		public static LocalizedString IncidentReportDlpPolicyLine
		{
			get
			{
				return new LocalizedString("IncidentReportDlpPolicyLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017784 File Offset: 0x00015984
		public static LocalizedString InvalidKeywordInTransportRule(string keyword)
		{
			return new LocalizedString("InvalidKeywordInTransportRule", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				keyword
			});
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000177B4 File Offset: 0x000159B4
		public static LocalizedString RuleCollectionNotInAd(string name)
		{
			return new LocalizedString("RuleCollectionNotInAd", "Ex736C93", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000177E4 File Offset: 0x000159E4
		public static LocalizedString CannotRemoveHeader(string name)
		{
			return new LocalizedString("CannotRemoveHeader", "Ex8D72A0", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00017814 File Offset: 0x00015A14
		public static LocalizedString InvalidAddress(string address)
		{
			return new LocalizedString("InvalidAddress", "ExF18033", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00017843 File Offset: 0x00015A43
		public static LocalizedString IncidentReportActionLine
		{
			get
			{
				return new LocalizedString("IncidentReportActionLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00017864 File Offset: 0x00015A64
		public static LocalizedString IpMatchPropertyRequired(string name)
		{
			return new LocalizedString("IpMatchPropertyRequired", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00017893 File Offset: 0x00015A93
		public static LocalizedString IncidentReportBcc
		{
			get
			{
				return new LocalizedString("IncidentReportBcc", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000178B1 File Offset: 0x00015AB1
		public static LocalizedString IncidentReportOverride
		{
			get
			{
				return new LocalizedString("IncidentReportOverride", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000178D0 File Offset: 0x00015AD0
		public static LocalizedString DomainIsPropertyRequired(string name)
		{
			return new LocalizedString("DomainIsPropertyRequired", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00017900 File Offset: 0x00015B00
		public static LocalizedString JournalingTargetDGEmptyDescription(string distributionGroup)
		{
			return new LocalizedString("JournalingTargetDGEmptyDescription", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				distributionGroup
			});
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001792F File Offset: 0x00015B2F
		public static LocalizedString IncidentReportOverrideLine
		{
			get
			{
				return new LocalizedString("IncidentReportOverrideLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00017950 File Offset: 0x00015B50
		public static LocalizedString AttachmentReadError(string error)
		{
			return new LocalizedString("AttachmentReadError", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00017980 File Offset: 0x00015B80
		public static LocalizedString MessageBodyReadFailure(string error)
		{
			return new LocalizedString("MessageBodyReadFailure", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000179AF File Offset: 0x00015BAF
		public static LocalizedString IncidentReportCc
		{
			get
			{
				return new LocalizedString("IncidentReportCc", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000179D0 File Offset: 0x00015BD0
		public static LocalizedString CannotSetHeader(string name, string value)
		{
			return new LocalizedString("CannotSetHeader", "Ex92ED94", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00017A04 File Offset: 0x00015C04
		public static LocalizedString InvalidAttachmentPropertyParameter(string name)
		{
			return new LocalizedString("InvalidAttachmentPropertyParameter", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00017A33 File Offset: 0x00015C33
		public static LocalizedString IncidentReportIdMatchLine
		{
			get
			{
				return new LocalizedString("IncidentReportIdMatchLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00017A51 File Offset: 0x00015C51
		public static LocalizedString SenderAddressLocationHeader
		{
			get
			{
				return new LocalizedString("SenderAddressLocationHeader", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00017A6F File Offset: 0x00015C6F
		public static LocalizedString IncidentReportDoNotIncludeOriginalMail
		{
			get
			{
				return new LocalizedString("IncidentReportDoNotIncludeOriginalMail", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00017A8D File Offset: 0x00015C8D
		public static LocalizedString IncidentReportIdMatch
		{
			get
			{
				return new LocalizedString("IncidentReportIdMatch", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00017AAB File Offset: 0x00015CAB
		public static LocalizedString IncidentReportCcLine
		{
			get
			{
				return new LocalizedString("IncidentReportCcLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017ACC File Offset: 0x00015CCC
		public static LocalizedString InvalidHeaderName(string name)
		{
			return new LocalizedString("InvalidHeaderName", "ExC37D6F", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00017AFB File Offset: 0x00015CFB
		public static LocalizedString IncidentReportJustificationLine
		{
			get
			{
				return new LocalizedString("IncidentReportJustificationLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00017B1C File Offset: 0x00015D1C
		public static LocalizedString FailedToInitializeRMEnvironment(string agentName)
		{
			return new LocalizedString("FailedToInitializeRMEnvironment", "Ex668D19", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00017B4B File Offset: 0x00015D4B
		public static LocalizedString IncidentReportIdContext
		{
			get
			{
				return new LocalizedString("IncidentReportIdContext", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00017B69 File Offset: 0x00015D69
		public static LocalizedString IncidentReportRecipients
		{
			get
			{
				return new LocalizedString("IncidentReportRecipients", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00017B88 File Offset: 0x00015D88
		public static LocalizedString FipsMatchingContentRetrievalFailure(uint contentId)
		{
			return new LocalizedString("FipsMatchingContentRetrievalFailure", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				contentId
			});
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00017BBC File Offset: 0x00015DBC
		public static LocalizedString IncidentReportSeverity
		{
			get
			{
				return new LocalizedString("IncidentReportSeverity", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00017BDC File Offset: 0x00015DDC
		public static LocalizedString JournalingTargetDGNotFoundDescription(string distributionGroup)
		{
			return new LocalizedString("JournalingTargetDGNotFoundDescription", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				distributionGroup
			});
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00017C0C File Offset: 0x00015E0C
		public static LocalizedString BodyReadError(string error)
		{
			return new LocalizedString("BodyReadError", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00017C3C File Offset: 0x00015E3C
		public static LocalizedString InvalidReconciliationGuid(string guid)
		{
			return new LocalizedString("InvalidReconciliationGuid", "ExAD165A", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00017C6B File Offset: 0x00015E6B
		public static LocalizedString IncidentReportFalsePositive
		{
			get
			{
				return new LocalizedString("IncidentReportFalsePositive", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00017C89 File Offset: 0x00015E89
		public static LocalizedString IncidentReportIncludeOriginalMail
		{
			get
			{
				return new LocalizedString("IncidentReportIncludeOriginalMail", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00017CA8 File Offset: 0x00015EA8
		public static LocalizedString IFilterProcessingExceptionMessage(string fileName)
		{
			return new LocalizedString("IFilterProcessingExceptionMessage", "Ex70F2FF", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00017CD8 File Offset: 0x00015ED8
		public static LocalizedString InvalidRegexInTransportRule(string regex)
		{
			return new LocalizedString("InvalidRegexInTransportRule", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				regex
			});
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00017D07 File Offset: 0x00015F07
		public static LocalizedString IncidentReportValue
		{
			get
			{
				return new LocalizedString("IncidentReportValue", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00017D25 File Offset: 0x00015F25
		public static LocalizedString UnableToUpdateRuleInAd
		{
			get
			{
				return new LocalizedString("UnableToUpdateRuleInAd", "ExF9ADCB", false, true, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00017D43 File Offset: 0x00015F43
		public static LocalizedString IncidentReportRuleDetections
		{
			get
			{
				return new LocalizedString("IncidentReportRuleDetections", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00017D64 File Offset: 0x00015F64
		public static LocalizedString InvalidNotifySenderTypeArgument(object destination)
		{
			return new LocalizedString("InvalidNotifySenderTypeArgument", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				destination
			});
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00017D93 File Offset: 0x00015F93
		public static LocalizedString Yes
		{
			get
			{
				return new LocalizedString("Yes", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00017DB1 File Offset: 0x00015FB1
		public static LocalizedString IncidentReportToLine
		{
			get
			{
				return new LocalizedString("IncidentReportToLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00017DCF File Offset: 0x00015FCF
		public static LocalizedString IncidentReportSeverityLine
		{
			get
			{
				return new LocalizedString("IncidentReportSeverityLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public static LocalizedString FileNameAndExtentionRetrievalFailure(string error)
		{
			return new LocalizedString("FileNameAndExtentionRetrievalFailure", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00017E1F File Offset: 0x0001601F
		public static LocalizedString IncidentReportRuleHitLine
		{
			get
			{
				return new LocalizedString("IncidentReportRuleHitLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00017E3D File Offset: 0x0001603D
		public static LocalizedString IncidentReportConfidenceLine
		{
			get
			{
				return new LocalizedString("IncidentReportConfidenceLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00017E5B File Offset: 0x0001605B
		public static LocalizedString IncidentReportSenderLine
		{
			get
			{
				return new LocalizedString("IncidentReportSenderLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00017E79 File Offset: 0x00016079
		public static LocalizedString IncidentReportRecommendedMinimumConfidenceLine
		{
			get
			{
				return new LocalizedString("IncidentReportRecommendedMinimumConfidenceLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00017E97 File Offset: 0x00016097
		public static LocalizedString IncidentReportMessageIdLine
		{
			get
			{
				return new LocalizedString("IncidentReportMessageIdLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00017EB5 File Offset: 0x000160B5
		public static LocalizedString IncidentReportAttachOriginalMail
		{
			get
			{
				return new LocalizedString("IncidentReportAttachOriginalMail", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017ED4 File Offset: 0x000160D4
		public static LocalizedString InvalidAuditSeverityLevel(string name)
		{
			return new LocalizedString("InvalidAuditSeverityLevel", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00017F04 File Offset: 0x00016104
		public static LocalizedString InvalidTransportRuleEventSourceType(string typeName)
		{
			return new LocalizedString("InvalidTransportRuleEventSourceType", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00017F33 File Offset: 0x00016133
		public static LocalizedString IncidentReportMoreRecipients
		{
			get
			{
				return new LocalizedString("IncidentReportMoreRecipients", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00017F51 File Offset: 0x00016151
		public static LocalizedString IncidentReportSubject
		{
			get
			{
				return new LocalizedString("IncidentReportSubject", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00017F6F File Offset: 0x0001616F
		public static LocalizedString IncidentReportCountLine
		{
			get
			{
				return new LocalizedString("IncidentReportCountLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00017F8D File Offset: 0x0001618D
		public static LocalizedString IncidentReportDisclaimer
		{
			get
			{
				return new LocalizedString("IncidentReportDisclaimer", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00017FAB File Offset: 0x000161AB
		public static LocalizedString SenderAddressLocationEnvelope
		{
			get
			{
				return new LocalizedString("SenderAddressLocationEnvelope", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00017FC9 File Offset: 0x000161C9
		public static LocalizedString IncidentReportIdMatchValue
		{
			get
			{
				return new LocalizedString("IncidentReportIdMatchValue", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00017FE8 File Offset: 0x000161E8
		public static LocalizedString FailedToLoadRuleCollection(string agentName)
		{
			return new LocalizedString("FailedToLoadRuleCollection", "ExE802B6", false, true, TransportRulesStrings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00018017 File Offset: 0x00016217
		public static LocalizedString IncidentReportContextLine
		{
			get
			{
				return new LocalizedString("IncidentReportContextLine", "", false, false, TransportRulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00018038 File Offset: 0x00016238
		public static LocalizedString IncidentReportMessageSubject(string ruleName)
		{
			return new LocalizedString("IncidentReportMessageSubject", "", false, false, TransportRulesStrings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00018067 File Offset: 0x00016267
		public static LocalizedString GetLocalizedString(TransportRulesStrings.IDs key)
		{
			return new LocalizedString(TransportRulesStrings.stringIDs[(uint)key], TransportRulesStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400028E RID: 654
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(49);

		// Token: 0x0400028F RID: 655
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessagingPolicies.TransportRulesStrings", typeof(TransportRulesStrings).GetTypeInfo().Assembly);

		// Token: 0x020000A6 RID: 166
		public enum IDs : uint
		{
			// Token: 0x04000291 RID: 657
			No = 1496915101U,
			// Token: 0x04000292 RID: 658
			IncidentReportFalsePositiveLine = 3611143976U,
			// Token: 0x04000293 RID: 659
			RuleNotInAd = 3658274981U,
			// Token: 0x04000294 RID: 660
			IncidentReportBccLine = 2679419348U,
			// Token: 0x04000295 RID: 661
			IncidentReportDataClassificationLine = 1273971392U,
			// Token: 0x04000296 RID: 662
			InvalidPriority = 2547444043U,
			// Token: 0x04000297 RID: 663
			InvalidFilteringServiceResult = 1190951733U,
			// Token: 0x04000298 RID: 664
			IncidentReportSubjectLine = 104102616U,
			// Token: 0x04000299 RID: 665
			FailedToLoadAttachmentFilteringConfigOnStartup = 497835657U,
			// Token: 0x0400029A RID: 666
			IncidentReportDataClassifications = 3336243037U,
			// Token: 0x0400029B RID: 667
			IncidentReportSender = 3453011747U,
			// Token: 0x0400029C RID: 668
			InvalidDataClassification = 4028858231U,
			// Token: 0x0400029D RID: 669
			SenderAddressLocationHeaderOrEnvelope = 3712824958U,
			// Token: 0x0400029E RID: 670
			IncidentReportDlpPolicyLine = 101970590U,
			// Token: 0x0400029F RID: 671
			IncidentReportActionLine = 1778028774U,
			// Token: 0x040002A0 RID: 672
			IncidentReportBcc = 1905970586U,
			// Token: 0x040002A1 RID: 673
			IncidentReportOverride = 3729543170U,
			// Token: 0x040002A2 RID: 674
			IncidentReportOverrideLine = 3959763416U,
			// Token: 0x040002A3 RID: 675
			IncidentReportCc = 429892252U,
			// Token: 0x040002A4 RID: 676
			IncidentReportIdMatchLine = 2422663214U,
			// Token: 0x040002A5 RID: 677
			SenderAddressLocationHeader = 90811307U,
			// Token: 0x040002A6 RID: 678
			IncidentReportDoNotIncludeOriginalMail = 29398792U,
			// Token: 0x040002A7 RID: 679
			IncidentReportIdMatch = 3226449092U,
			// Token: 0x040002A8 RID: 680
			IncidentReportCcLine = 1864226526U,
			// Token: 0x040002A9 RID: 681
			IncidentReportJustificationLine = 750089942U,
			// Token: 0x040002AA RID: 682
			IncidentReportIdContext = 3384738962U,
			// Token: 0x040002AB RID: 683
			IncidentReportRecipients = 3955406766U,
			// Token: 0x040002AC RID: 684
			IncidentReportSeverity = 1755226983U,
			// Token: 0x040002AD RID: 685
			IncidentReportFalsePositive = 3488352230U,
			// Token: 0x040002AE RID: 686
			IncidentReportIncludeOriginalMail = 1389339898U,
			// Token: 0x040002AF RID: 687
			IncidentReportValue = 2794317127U,
			// Token: 0x040002B0 RID: 688
			UnableToUpdateRuleInAd = 414074715U,
			// Token: 0x040002B1 RID: 689
			IncidentReportRuleDetections = 1090675464U,
			// Token: 0x040002B2 RID: 690
			Yes = 3021629903U,
			// Token: 0x040002B3 RID: 691
			IncidentReportToLine = 3700793673U,
			// Token: 0x040002B4 RID: 692
			IncidentReportSeverityLine = 2156460713U,
			// Token: 0x040002B5 RID: 693
			IncidentReportRuleHitLine = 492774025U,
			// Token: 0x040002B6 RID: 694
			IncidentReportConfidenceLine = 594567544U,
			// Token: 0x040002B7 RID: 695
			IncidentReportSenderLine = 4096407337U,
			// Token: 0x040002B8 RID: 696
			IncidentReportRecommendedMinimumConfidenceLine = 863777729U,
			// Token: 0x040002B9 RID: 697
			IncidentReportMessageIdLine = 234820282U,
			// Token: 0x040002BA RID: 698
			IncidentReportAttachOriginalMail = 3943613787U,
			// Token: 0x040002BB RID: 699
			IncidentReportMoreRecipients = 4271028853U,
			// Token: 0x040002BC RID: 700
			IncidentReportSubject = 2110678538U,
			// Token: 0x040002BD RID: 701
			IncidentReportCountLine = 1898574625U,
			// Token: 0x040002BE RID: 702
			IncidentReportDisclaimer = 4151534495U,
			// Token: 0x040002BF RID: 703
			SenderAddressLocationEnvelope = 2536257678U,
			// Token: 0x040002C0 RID: 704
			IncidentReportIdMatchValue = 4076992413U,
			// Token: 0x040002C1 RID: 705
			IncidentReportContextLine = 3534659841U
		}

		// Token: 0x020000A7 RID: 167
		private enum ParamIDs
		{
			// Token: 0x040002C3 RID: 707
			FailedToRegisterForConfigChangeNotification,
			// Token: 0x040002C4 RID: 708
			ErrorInvokingFilteringService,
			// Token: 0x040002C5 RID: 709
			InvalidReportDestinationArgument,
			// Token: 0x040002C6 RID: 710
			InvalidPropertyValueType,
			// Token: 0x040002C7 RID: 711
			DataClassificationPropertyRequired,
			// Token: 0x040002C8 RID: 712
			FailedToReadTransportSettingsConfiguration,
			// Token: 0x040002C9 RID: 713
			InvalidKeywordInTransportRule,
			// Token: 0x040002CA RID: 714
			RuleCollectionNotInAd,
			// Token: 0x040002CB RID: 715
			CannotRemoveHeader,
			// Token: 0x040002CC RID: 716
			InvalidAddress,
			// Token: 0x040002CD RID: 717
			IpMatchPropertyRequired,
			// Token: 0x040002CE RID: 718
			DomainIsPropertyRequired,
			// Token: 0x040002CF RID: 719
			JournalingTargetDGEmptyDescription,
			// Token: 0x040002D0 RID: 720
			AttachmentReadError,
			// Token: 0x040002D1 RID: 721
			MessageBodyReadFailure,
			// Token: 0x040002D2 RID: 722
			CannotSetHeader,
			// Token: 0x040002D3 RID: 723
			InvalidAttachmentPropertyParameter,
			// Token: 0x040002D4 RID: 724
			InvalidHeaderName,
			// Token: 0x040002D5 RID: 725
			FailedToInitializeRMEnvironment,
			// Token: 0x040002D6 RID: 726
			FipsMatchingContentRetrievalFailure,
			// Token: 0x040002D7 RID: 727
			JournalingTargetDGNotFoundDescription,
			// Token: 0x040002D8 RID: 728
			BodyReadError,
			// Token: 0x040002D9 RID: 729
			InvalidReconciliationGuid,
			// Token: 0x040002DA RID: 730
			IFilterProcessingExceptionMessage,
			// Token: 0x040002DB RID: 731
			InvalidRegexInTransportRule,
			// Token: 0x040002DC RID: 732
			InvalidNotifySenderTypeArgument,
			// Token: 0x040002DD RID: 733
			FileNameAndExtentionRetrievalFailure,
			// Token: 0x040002DE RID: 734
			InvalidAuditSeverityLevel,
			// Token: 0x040002DF RID: 735
			InvalidTransportRuleEventSourceType,
			// Token: 0x040002E0 RID: 736
			FailedToLoadRuleCollection,
			// Token: 0x040002E1 RID: 737
			IncidentReportMessageSubject
		}
	}
}
