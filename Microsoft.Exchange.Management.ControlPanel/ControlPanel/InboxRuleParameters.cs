using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003FC RID: 1020
	[DataContract]
	public abstract class InboxRuleParameters : SetObjectProperties
	{
		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000A2AF5 File Offset: 0x000A0CF5
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000A2AFC File Offset: 0x000A0CFC
		public override string SuppressConfirmParameterName
		{
			get
			{
				return "AlwaysDeleteOutlookRulesBlob";
			}
		}

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000A2B03 File Offset: 0x000A0D03
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x000A2B15 File Offset: 0x000A0D15
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000A2B23 File Offset: 0x000A0D23
		// (set) Token: 0x0600342A RID: 13354 RVA: 0x000A2B35 File Offset: 0x000A0D35
		[DataMember]
		public PeopleIdentity[] From
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["From"]);
			}
			set
			{
				base["From"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000A2B48 File Offset: 0x000A0D48
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x000A2B5A File Offset: 0x000A0D5A
		[DataMember]
		public PeopleIdentity[] SentTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["SentTo"]);
			}
			set
			{
				base["SentTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000A2B70 File Offset: 0x000A0D70
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x000A2B99 File Offset: 0x000A0D99
		[DataMember]
		public Identity FromSubscription
		{
			get
			{
				string value = ((string[])base["FromSubscription"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["FromSubscription"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000A2BAC File Offset: 0x000A0DAC
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x000A2BBE File Offset: 0x000A0DBE
		[DataMember]
		public string[] SubjectContainsWords
		{
			get
			{
				return (string[])base["SubjectContainsWords"];
			}
			set
			{
				base["SubjectContainsWords"] = value;
			}
		}

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000A2BCC File Offset: 0x000A0DCC
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x000A2BDE File Offset: 0x000A0DDE
		[DataMember]
		public string[] SubjectOrBodyContainsWords
		{
			get
			{
				return (string[])base["SubjectOrBodyContainsWords"];
			}
			set
			{
				base["SubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000A2BEC File Offset: 0x000A0DEC
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x000A2BFE File Offset: 0x000A0DFE
		[DataMember]
		public string[] FromAddressContainsWords
		{
			get
			{
				return (string[])base["FromAddressContainsWords"];
			}
			set
			{
				base["FromAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000A2C0C File Offset: 0x000A0E0C
		// (set) Token: 0x06003436 RID: 13366 RVA: 0x000A2C20 File Offset: 0x000A0E20
		[DataMember]
		public bool? MyNameInToOrCcBox
		{
			get
			{
				return (bool?)base["MyNameInToOrCcBox"];
			}
			set
			{
				base["MyNameInToOrCcBox"] = (value ?? false);
			}
		}

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000A2C52 File Offset: 0x000A0E52
		// (set) Token: 0x06003438 RID: 13368 RVA: 0x000A2C64 File Offset: 0x000A0E64
		[DataMember]
		public bool? SentOnlyToMe
		{
			get
			{
				return (bool?)base["SentOnlyToMe"];
			}
			set
			{
				base["SentOnlyToMe"] = (value ?? false);
			}
		}

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000A2C96 File Offset: 0x000A0E96
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x000A2CA8 File Offset: 0x000A0EA8
		[DataMember]
		public bool? MyNameInToBox
		{
			get
			{
				return (bool?)base["MyNameInToBox"];
			}
			set
			{
				base["MyNameInToBox"] = (value ?? false);
			}
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000A2CDA File Offset: 0x000A0EDA
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x000A2CEC File Offset: 0x000A0EEC
		[DataMember]
		public bool? MyNameInCcBox
		{
			get
			{
				return (bool?)base["MyNameInCcBox"];
			}
			set
			{
				base["MyNameInCcBox"] = (value ?? false);
			}
		}

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000A2D1E File Offset: 0x000A0F1E
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x000A2D30 File Offset: 0x000A0F30
		[DataMember]
		public bool? MyNameNotInToBox
		{
			get
			{
				return (bool?)base["MyNameNotInToBox"];
			}
			set
			{
				base["MyNameNotInToBox"] = (value ?? false);
			}
		}

		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000A2D62 File Offset: 0x000A0F62
		// (set) Token: 0x06003440 RID: 13376 RVA: 0x000A2D74 File Offset: 0x000A0F74
		[DataMember]
		public string[] BodyContainsWords
		{
			get
			{
				return (string[])base["BodyContainsWords"];
			}
			set
			{
				base["BodyContainsWords"] = value;
			}
		}

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000A2D82 File Offset: 0x000A0F82
		// (set) Token: 0x06003442 RID: 13378 RVA: 0x000A2D94 File Offset: 0x000A0F94
		[DataMember]
		public string[] RecipientAddressContainsWords
		{
			get
			{
				return (string[])base["RecipientAddressContainsWords"];
			}
			set
			{
				base["RecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000A2DA2 File Offset: 0x000A0FA2
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x000A2DB4 File Offset: 0x000A0FB4
		[DataMember]
		public string[] HeaderContainsWords
		{
			get
			{
				return (string[])base["HeaderContainsWords"];
			}
			set
			{
				base["HeaderContainsWords"] = value;
			}
		}

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000A2DC2 File Offset: 0x000A0FC2
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x000A2DD4 File Offset: 0x000A0FD4
		[DataMember]
		public string WithImportance
		{
			get
			{
				return (string)base["WithImportance"];
			}
			set
			{
				base["WithImportance"] = value;
			}
		}

		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000A2DE2 File Offset: 0x000A0FE2
		// (set) Token: 0x06003448 RID: 13384 RVA: 0x000A2DF4 File Offset: 0x000A0FF4
		[DataMember]
		public string WithSensitivity
		{
			get
			{
				return (string)base["WithSensitivity"];
			}
			set
			{
				base["WithSensitivity"] = value;
			}
		}

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000A2E02 File Offset: 0x000A1002
		// (set) Token: 0x0600344A RID: 13386 RVA: 0x000A2E14 File Offset: 0x000A1014
		[DataMember]
		public bool? HasAttachment
		{
			get
			{
				return (bool?)base["HasAttachment"];
			}
			set
			{
				base["HasAttachment"] = (value ?? false);
			}
		}

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000A2E46 File Offset: 0x000A1046
		// (set) Token: 0x0600344C RID: 13388 RVA: 0x000A2E58 File Offset: 0x000A1058
		[DataMember]
		public string MessageTypeMatches
		{
			get
			{
				return (string)base["MessageTypeMatches"];
			}
			set
			{
				base["MessageTypeMatches"] = value;
			}
		}

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000A2E68 File Offset: 0x000A1068
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x000A2E91 File Offset: 0x000A1091
		[DataMember]
		public Identity HasClassification
		{
			get
			{
				string value = ((string[])base["HasClassification"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["HasClassification"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000A2EA4 File Offset: 0x000A10A4
		// (set) Token: 0x06003450 RID: 13392 RVA: 0x000A2EB6 File Offset: 0x000A10B6
		[DataMember]
		public string FlaggedForAction
		{
			get
			{
				return (string)base["FlaggedForAction"];
			}
			set
			{
				base["FlaggedForAction"] = value;
			}
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000A2EC4 File Offset: 0x000A10C4
		// (set) Token: 0x06003452 RID: 13394 RVA: 0x000A2F10 File Offset: 0x000A1110
		[DataMember]
		public NumberRange WithinSizeRange
		{
			get
			{
				return new NumberRange
				{
					AtLeast = ((ByteQuantifiedSize?)base["WithinSizeRangeMinimum"]).ToKB(),
					AtMost = ((ByteQuantifiedSize?)base["WithinSizeRangeMaximum"]).ToKB()
				};
			}
			set
			{
				base["WithinSizeRangeMinimum"] = ((value == null) ? null : value.AtLeast.ToByteSize());
				base["WithinSizeRangeMaximum"] = ((value == null) ? null : value.AtMost.ToByteSize());
			}
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x06003453 RID: 13395 RVA: 0x000A2F70 File Offset: 0x000A1170
		// (set) Token: 0x06003454 RID: 13396 RVA: 0x000A2FBC File Offset: 0x000A11BC
		[DataMember]
		public DateRange WithinDateRange
		{
			get
			{
				return new DateRange
				{
					BeforeDate = ((ExDateTime?)base["ReceivedBeforeDate"]).ToIdentity(),
					AfterDate = ((ExDateTime?)base["ReceivedAfterDate"]).ToIdentity()
				};
			}
			set
			{
				base["ReceivedBeforeDate"] = ((value == null || value.BeforeDate == null) ? null : value.BeforeDate.RawIdentity.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss"));
				base["ReceivedAfterDate"] = ((value == null || value.AfterDate == null) ? null : value.AfterDate.RawIdentity.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss"));
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000A304B File Offset: 0x000A124B
		// (set) Token: 0x06003456 RID: 13398 RVA: 0x000A305D File Offset: 0x000A125D
		[DataMember]
		public Identity MoveToFolder
		{
			get
			{
				return Identity.FromIdParameter(base["MoveToFolder"]);
			}
			set
			{
				base["MoveToFolder"] = value.ToMailboxFolderIdParameter();
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000A3070 File Offset: 0x000A1270
		// (set) Token: 0x06003458 RID: 13400 RVA: 0x000A3082 File Offset: 0x000A1282
		[DataMember]
		public Identity CopyToFolder
		{
			get
			{
				return Identity.FromIdParameter(base["CopyToFolder"]);
			}
			set
			{
				base["CopyToFolder"] = value.ToMailboxFolderIdParameter();
			}
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000A3095 File Offset: 0x000A1295
		// (set) Token: 0x0600345A RID: 13402 RVA: 0x000A30A8 File Offset: 0x000A12A8
		[DataMember]
		public bool? DeleteMessage
		{
			get
			{
				return (bool?)base["DeleteMessage"];
			}
			set
			{
				base["DeleteMessage"] = (value ?? false);
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000A30DA File Offset: 0x000A12DA
		// (set) Token: 0x0600345C RID: 13404 RVA: 0x000A30EC File Offset: 0x000A12EC
		[DataMember]
		public bool? StopProcessingRules
		{
			get
			{
				return (bool?)base["StopProcessingRules"];
			}
			set
			{
				base["StopProcessingRules"] = (value ?? false);
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000A3120 File Offset: 0x000A1320
		// (set) Token: 0x0600345E RID: 13406 RVA: 0x000A3149 File Offset: 0x000A1349
		[DataMember]
		public Identity ApplyCategory
		{
			get
			{
				string value = ((string[])base["ApplyCategory"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ApplyCategory"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000A315C File Offset: 0x000A135C
		// (set) Token: 0x06003460 RID: 13408 RVA: 0x000A3170 File Offset: 0x000A1370
		[DataMember]
		public bool? MarkAsRead
		{
			get
			{
				return (bool?)base["MarkAsRead"];
			}
			set
			{
				base["MarkAsRead"] = (value ?? false);
			}
		}

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000A31A2 File Offset: 0x000A13A2
		// (set) Token: 0x06003462 RID: 13410 RVA: 0x000A31B4 File Offset: 0x000A13B4
		[DataMember]
		public PeopleIdentity[] ForwardTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ForwardTo"]);
			}
			set
			{
				base["ForwardTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000A31C7 File Offset: 0x000A13C7
		// (set) Token: 0x06003464 RID: 13412 RVA: 0x000A31D9 File Offset: 0x000A13D9
		[DataMember]
		public string SendTextMessageNotificationTo
		{
			get
			{
				return (string)base["SendTextMessageNotificationTo"];
			}
			set
			{
				base["SendTextMessageNotificationTo"] = value;
			}
		}

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000A31E7 File Offset: 0x000A13E7
		// (set) Token: 0x06003466 RID: 13414 RVA: 0x000A31F9 File Offset: 0x000A13F9
		[DataMember]
		public PeopleIdentity[] RedirectTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["RedirectTo"]);
			}
			set
			{
				base["RedirectTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000A320C File Offset: 0x000A140C
		// (set) Token: 0x06003468 RID: 13416 RVA: 0x000A321E File Offset: 0x000A141E
		[DataMember]
		public PeopleIdentity[] ForwardAsAttachmentTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ForwardAsAttachmentTo"]);
			}
			set
			{
				base["ForwardAsAttachmentTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000A3231 File Offset: 0x000A1431
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x000A3243 File Offset: 0x000A1443
		[DataMember]
		public string MarkImportance
		{
			get
			{
				return (string)base["MarkImportance"];
			}
			set
			{
				base["MarkImportance"] = value;
			}
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000A3251 File Offset: 0x000A1451
		// (set) Token: 0x0600346C RID: 13420 RVA: 0x000A3263 File Offset: 0x000A1463
		[DataMember]
		public PeopleIdentity[] ExceptIfFrom
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfFrom"]);
			}
			set
			{
				base["ExceptIfFrom"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000A3276 File Offset: 0x000A1476
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000A3288 File Offset: 0x000A1488
		[DataMember]
		public PeopleIdentity[] ExceptIfSentTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfSentTo"]);
			}
			set
			{
				base["ExceptIfSentTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000A329C File Offset: 0x000A149C
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000A32C5 File Offset: 0x000A14C5
		[DataMember]
		public Identity ExceptIfFromSubscription
		{
			get
			{
				string value = ((string[])base["ExceptIfFromSubscription"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ExceptIfFromSubscription"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000A32D8 File Offset: 0x000A14D8
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x000A32EA File Offset: 0x000A14EA
		[DataMember]
		public string[] ExceptIfSubjectContainsWords
		{
			get
			{
				return (string[])base["ExceptIfSubjectContainsWords"];
			}
			set
			{
				base["ExceptIfSubjectContainsWords"] = value;
			}
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000A32F8 File Offset: 0x000A14F8
		// (set) Token: 0x06003474 RID: 13428 RVA: 0x000A330A File Offset: 0x000A150A
		[DataMember]
		public string[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return (string[])base["ExceptIfSubjectOrBodyContainsWords"];
			}
			set
			{
				base["ExceptIfSubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000A3318 File Offset: 0x000A1518
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x000A332A File Offset: 0x000A152A
		[DataMember]
		public string[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return (string[])base["ExceptIfFromAddressContainsWords"];
			}
			set
			{
				base["ExceptIfFromAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000A3338 File Offset: 0x000A1538
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x000A334C File Offset: 0x000A154C
		[DataMember]
		public bool? ExceptIfMyNameInToOrCcBox
		{
			get
			{
				return (bool?)base["ExceptIfMyNameInToOrCcBox"];
			}
			set
			{
				base["ExceptIfMyNameInToOrCcBox"] = (value ?? false);
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000A337E File Offset: 0x000A157E
		// (set) Token: 0x0600347A RID: 13434 RVA: 0x000A3390 File Offset: 0x000A1590
		[DataMember]
		public bool? ExceptIfSentOnlyToMe
		{
			get
			{
				return (bool?)base["ExceptIfSentOnlyToMe"];
			}
			set
			{
				base["ExceptIfSentOnlyToMe"] = (value ?? false);
			}
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000A33C2 File Offset: 0x000A15C2
		// (set) Token: 0x0600347C RID: 13436 RVA: 0x000A33D4 File Offset: 0x000A15D4
		[DataMember]
		public bool? ExceptIfMyNameInToBox
		{
			get
			{
				return (bool?)base["ExceptIfMyNameInToBox"];
			}
			set
			{
				base["ExceptIfMyNameInToBox"] = (value ?? false);
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000A3406 File Offset: 0x000A1606
		// (set) Token: 0x0600347E RID: 13438 RVA: 0x000A3418 File Offset: 0x000A1618
		[DataMember]
		public bool? ExceptIfMyNameInCcBox
		{
			get
			{
				return (bool?)base["ExceptIfMyNameInCcBox"];
			}
			set
			{
				base["ExceptIfMyNameInCcBox"] = (value ?? false);
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000A344A File Offset: 0x000A164A
		// (set) Token: 0x06003480 RID: 13440 RVA: 0x000A345C File Offset: 0x000A165C
		[DataMember]
		public bool? ExceptIfMyNameNotInToBox
		{
			get
			{
				return (bool?)base["ExceptIfMyNameNotInToBox"];
			}
			set
			{
				base["ExceptIfMyNameNotInToBox"] = (value ?? false);
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000A348E File Offset: 0x000A168E
		// (set) Token: 0x06003482 RID: 13442 RVA: 0x000A34A0 File Offset: 0x000A16A0
		[DataMember]
		public string[] ExceptIfBodyContainsWords
		{
			get
			{
				return (string[])base["ExceptIfBodyContainsWords"];
			}
			set
			{
				base["ExceptIfBodyContainsWords"] = value;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000A34AE File Offset: 0x000A16AE
		// (set) Token: 0x06003484 RID: 13444 RVA: 0x000A34C0 File Offset: 0x000A16C0
		[DataMember]
		public string[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return (string[])base["ExceptIfRecipientAddressContainsWords"];
			}
			set
			{
				base["ExceptIfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000A34CE File Offset: 0x000A16CE
		// (set) Token: 0x06003486 RID: 13446 RVA: 0x000A34E0 File Offset: 0x000A16E0
		[DataMember]
		public string[] ExceptIfHeaderContainsWords
		{
			get
			{
				return (string[])base["ExceptIfHeaderContainsWords"];
			}
			set
			{
				base["ExceptIfHeaderContainsWords"] = value;
			}
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000A34EE File Offset: 0x000A16EE
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x000A3500 File Offset: 0x000A1700
		[DataMember]
		public string ExceptIfWithImportance
		{
			get
			{
				return (string)base["ExceptIfWithImportance"];
			}
			set
			{
				base["ExceptIfWithImportance"] = value;
			}
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000A350E File Offset: 0x000A170E
		// (set) Token: 0x0600348A RID: 13450 RVA: 0x000A3520 File Offset: 0x000A1720
		[DataMember]
		public string ExceptIfWithSensitivity
		{
			get
			{
				return (string)base["ExceptIfWithSensitivity"];
			}
			set
			{
				base["ExceptIfWithSensitivity"] = value;
			}
		}

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000A352E File Offset: 0x000A172E
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x000A3540 File Offset: 0x000A1740
		[DataMember]
		public bool? ExceptIfHasAttachment
		{
			get
			{
				return (bool?)base["ExceptIfHasAttachment"];
			}
			set
			{
				base["ExceptIfHasAttachment"] = (value ?? false);
			}
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000A3572 File Offset: 0x000A1772
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x000A3584 File Offset: 0x000A1784
		[DataMember]
		public string ExceptIfMessageTypeMatches
		{
			get
			{
				return (string)base["ExceptIfMessageTypeMatches"];
			}
			set
			{
				base["ExceptIfMessageTypeMatches"] = value;
			}
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000A3594 File Offset: 0x000A1794
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000A35BD File Offset: 0x000A17BD
		[DataMember]
		public Identity ExceptIfHasClassification
		{
			get
			{
				string value = ((string[])base["ExceptIfHasClassification"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ExceptIfHasClassification"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000A35D0 File Offset: 0x000A17D0
		// (set) Token: 0x06003492 RID: 13458 RVA: 0x000A35E2 File Offset: 0x000A17E2
		[DataMember]
		public string ExceptIfFlaggedForAction
		{
			get
			{
				return (string)base["ExceptIfFlaggedForAction"];
			}
			set
			{
				base["ExceptIfFlaggedForAction"] = value;
			}
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000A35F0 File Offset: 0x000A17F0
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000A363C File Offset: 0x000A183C
		[DataMember]
		public NumberRange ExceptIfWithinSizeRange
		{
			get
			{
				return new NumberRange
				{
					AtLeast = ((ByteQuantifiedSize?)base["ExceptIfWithinSizeRangeMinimum"]).ToKB(),
					AtMost = ((ByteQuantifiedSize?)base["ExceptIfWithinSizeRangeMaximum"]).ToKB()
				};
			}
			set
			{
				base["ExceptIfWithinSizeRangeMinimum"] = ((value == null) ? null : value.AtLeast.ToByteSize());
				base["ExceptIfWithinSizeRangeMaximum"] = ((value == null) ? null : value.AtMost.ToByteSize());
			}
		}

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000A369C File Offset: 0x000A189C
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000A36E8 File Offset: 0x000A18E8
		[DataMember]
		public DateRange ExceptIfWithinDateRange
		{
			get
			{
				return new DateRange
				{
					BeforeDate = ((ExDateTime?)base["ExceptIfReceivedBeforeDate"]).ToIdentity(),
					AfterDate = ((ExDateTime?)base["ExceptIfReceivedAfterDate"]).ToIdentity()
				};
			}
			set
			{
				base["ExceptIfReceivedBeforeDate"] = ((value == null || value.BeforeDate == null) ? null : value.BeforeDate.RawIdentity.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss"));
				base["ExceptIfReceivedAfterDate"] = ((value == null || value.AfterDate == null) ? null : value.AfterDate.RawIdentity.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss"));
			}
		}
	}
}
