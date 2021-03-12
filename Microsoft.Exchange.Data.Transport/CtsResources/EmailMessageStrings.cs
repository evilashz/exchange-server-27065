using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x020000B4 RID: 180
	internal static class EmailMessageStrings
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00008FF0 File Offset: 0x000071F0
		static EmailMessageStrings()
		{
			EmailMessageStrings.stringIDs.Add(1282546634U, "ReferenceBodyDoesNotBelongToSameMessage");
			EmailMessageStrings.stringIDs.Add(4174367109U, "CannotAccessMapiPropsFromPureMimeMessage");
			EmailMessageStrings.stringIDs.Add(2479849522U, "ErrorInit");
			EmailMessageStrings.stringIDs.Add(1426955056U, "TooManyEntriesInApplefile");
			EmailMessageStrings.stringIDs.Add(392243280U, "MimeDocumentRootPartMustNotBeNull");
			EmailMessageStrings.stringIDs.Add(4142370976U, "WrongOffsetsInApplefile");
			EmailMessageStrings.stringIDs.Add(1857526624U, "CannotCreateAlternativeBody");
			EmailMessageStrings.stringIDs.Add(3435085534U, "CanOnlyAddInlineAttachmentsToHtmlBody");
			EmailMessageStrings.stringIDs.Add(4119843803U, "ErrorBeforeFirst");
			EmailMessageStrings.stringIDs.Add(3424442414U, "ErrorAfterLast");
			EmailMessageStrings.stringIDs.Add(2804270193U, "CannotAttachEmbeddedMapiMessageToMime");
			EmailMessageStrings.stringIDs.Add(764847365U, "BodyExistsInTnefMessage");
			EmailMessageStrings.stringIDs.Add(1685978706U, "NotSupportedForRtfBody");
			EmailMessageStrings.stringIDs.Add(368316767U, "CanOnlyAddInlineAttachmentForHtmlBody");
			EmailMessageStrings.stringIDs.Add(797792475U, "UnexpectedEndOfStream");
			EmailMessageStrings.stringIDs.Add(4122747589U, "CannotSetEmbeddedMessageForTnefAttachment");
			EmailMessageStrings.stringIDs.Add(4281132505U, "WrongAppleMagicNumber");
			EmailMessageStrings.stringIDs.Add(3934212261U, "ContentTypeCannotBeMultipart");
			EmailMessageStrings.stringIDs.Add(1586749494U, "CollectionHasChanged");
			EmailMessageStrings.stringIDs.Add(4139915868U, "AttachmentRemovedFromMessage");
			EmailMessageStrings.stringIDs.Add(2815148485U, "DigestCanOnlyContainMessage822Attachments");
			EmailMessageStrings.stringIDs.Add(2676324379U, "RecipientAlreadyHasParent");
			EmailMessageStrings.stringIDs.Add(1686645306U, "WrongAppleVersionNumber");
			EmailMessageStrings.stringIDs.Add(3682676966U, "MacBinWrongFilename");
			EmailMessageStrings.stringIDs.Add(532813113U, "CannotAddAttachment");
			EmailMessageStrings.stringIDs.Add(414550776U, "NoBodyForInlineAttachment");
			EmailMessageStrings.stringIDs.Add(1975150156U, "TnefIsMissingAttachRenderData");
			EmailMessageStrings.stringIDs.Add(2369512451U, "CannotSetEmbeddedMessageForNonMessageRfc822Attachment");
			EmailMessageStrings.stringIDs.Add(2544842942U, "WrongMacBinHeader");
			EmailMessageStrings.stringIDs.Add(3779231310U, "BodyAlreadyHasParent");
			EmailMessageStrings.stringIDs.Add(2792487344U, "CollectionIsReadOnly");
			EmailMessageStrings.stringIDs.Add(2839669472U, "ChangingDntNotSupportedForEmbeddedTnefMessages");
			EmailMessageStrings.stringIDs.Add(2005747751U, "CannotWriteBodyDoesNotExist");
			EmailMessageStrings.stringIDs.Add(665063789U, "CannotSetNativePropertyForMimeRecipient");
			EmailMessageStrings.stringIDs.Add(182340944U, "ArgumentInvalidOffLen");
			EmailMessageStrings.stringIDs.Add(2700714933U, "TnefContainsMultipleStreams");
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000092FC File Offset: 0x000074FC
		public static string ReferenceBodyDoesNotBelongToSameMessage
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ReferenceBodyDoesNotBelongToSameMessage");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000930D File Offset: 0x0000750D
		public static string CannotAccessMapiPropsFromPureMimeMessage
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotAccessMapiPropsFromPureMimeMessage");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000931E File Offset: 0x0000751E
		public static string ErrorInit
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ErrorInit");
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000932F File Offset: 0x0000752F
		public static string UnsupportedBodyType(string value)
		{
			return string.Format(EmailMessageStrings.ResourceManager.GetString("UnsupportedBodyType"), value);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00009346 File Offset: 0x00007546
		public static string TooManyEntriesInApplefile
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("TooManyEntriesInApplefile");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00009357 File Offset: 0x00007557
		public static string MimeDocumentRootPartMustNotBeNull
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("MimeDocumentRootPartMustNotBeNull");
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00009368 File Offset: 0x00007568
		public static string WrongOffsetsInApplefile
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("WrongOffsetsInApplefile");
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00009379 File Offset: 0x00007579
		public static string EntryLengthTooBigInApplefile(long length)
		{
			return string.Format(EmailMessageStrings.ResourceManager.GetString("EntryLengthTooBigInApplefile"), length);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00009395 File Offset: 0x00007595
		public static string CannotCreateAlternativeBody
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotCreateAlternativeBody");
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000093A6 File Offset: 0x000075A6
		public static string CannotCreateSpecifiedBodyFormat(string format)
		{
			return string.Format(EmailMessageStrings.ResourceManager.GetString("CannotCreateSpecifiedBodyFormat"), format);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000093BD File Offset: 0x000075BD
		public static string CanOnlyAddInlineAttachmentsToHtmlBody
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CanOnlyAddInlineAttachmentsToHtmlBody");
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000093CE File Offset: 0x000075CE
		public static string ErrorBeforeFirst
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ErrorBeforeFirst");
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000093DF File Offset: 0x000075DF
		public static string ErrorAfterLast
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ErrorAfterLast");
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000093F0 File Offset: 0x000075F0
		public static string CannotAttachEmbeddedMapiMessageToMime
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotAttachEmbeddedMapiMessageToMime");
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00009401 File Offset: 0x00007601
		public static string BodyExistsInTnefMessage
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("BodyExistsInTnefMessage");
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00009412 File Offset: 0x00007612
		public static string NotSupportedForRtfBody
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("NotSupportedForRtfBody");
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00009423 File Offset: 0x00007623
		public static string CanOnlyAddInlineAttachmentForHtmlBody
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CanOnlyAddInlineAttachmentForHtmlBody");
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00009434 File Offset: 0x00007634
		public static string InvalidCharset(string charsetName)
		{
			return string.Format(EmailMessageStrings.ResourceManager.GetString("InvalidCharset"), charsetName);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000944B File Offset: 0x0000764B
		public static string UnexpectedEndOfStream
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("UnexpectedEndOfStream");
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000945C File Offset: 0x0000765C
		public static string CannotSetEmbeddedMessageForTnefAttachment
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotSetEmbeddedMessageForTnefAttachment");
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000946D File Offset: 0x0000766D
		public static string WrongAppleMagicNumber
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("WrongAppleMagicNumber");
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000947E File Offset: 0x0000767E
		public static string ContentTypeCannotBeMultipart
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ContentTypeCannotBeMultipart");
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000948F File Offset: 0x0000768F
		public static string CollectionHasChanged
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CollectionHasChanged");
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000094A0 File Offset: 0x000076A0
		public static string AttachmentRemovedFromMessage
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("AttachmentRemovedFromMessage");
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000094B1 File Offset: 0x000076B1
		public static string DigestCanOnlyContainMessage822Attachments
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("DigestCanOnlyContainMessage822Attachments");
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000094C2 File Offset: 0x000076C2
		public static string RecipientAlreadyHasParent
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("RecipientAlreadyHasParent");
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000094D3 File Offset: 0x000076D3
		public static string WrongAppleVersionNumber
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("WrongAppleVersionNumber");
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000094E4 File Offset: 0x000076E4
		public static string NestingTooDeep(int actual, int limit)
		{
			return string.Format(EmailMessageStrings.ResourceManager.GetString("NestingTooDeep"), actual, limit);
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00009506 File Offset: 0x00007706
		public static string MacBinWrongFilename
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("MacBinWrongFilename");
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00009517 File Offset: 0x00007717
		public static string CannotAddAttachment
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotAddAttachment");
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00009528 File Offset: 0x00007728
		public static string NoBodyForInlineAttachment
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("NoBodyForInlineAttachment");
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00009539 File Offset: 0x00007739
		public static string TnefIsMissingAttachRenderData
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("TnefIsMissingAttachRenderData");
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000954A File Offset: 0x0000774A
		public static string CannotSetEmbeddedMessageForNonMessageRfc822Attachment
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotSetEmbeddedMessageForNonMessageRfc822Attachment");
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000955B File Offset: 0x0000775B
		public static string WrongMacBinHeader
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("WrongMacBinHeader");
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000956C File Offset: 0x0000776C
		public static string BodyAlreadyHasParent
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("BodyAlreadyHasParent");
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000957D File Offset: 0x0000777D
		public static string CollectionIsReadOnly
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CollectionIsReadOnly");
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000958E File Offset: 0x0000778E
		public static string ChangingDntNotSupportedForEmbeddedTnefMessages
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ChangingDntNotSupportedForEmbeddedTnefMessages");
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000959F File Offset: 0x0000779F
		public static string CannotWriteBodyDoesNotExist
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotWriteBodyDoesNotExist");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000095B0 File Offset: 0x000077B0
		public static string CannotSetNativePropertyForMimeRecipient
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("CannotSetNativePropertyForMimeRecipient");
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000095C1 File Offset: 0x000077C1
		public static string ArgumentInvalidOffLen
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("ArgumentInvalidOffLen");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000095D2 File Offset: 0x000077D2
		public static string TnefContainsMultipleStreams
		{
			get
			{
				return EmailMessageStrings.ResourceManager.GetString("TnefContainsMultipleStreams");
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000095E3 File Offset: 0x000077E3
		public static string InvalidBodyTypeForThisMessage(string value)
		{
			return string.Format(EmailMessageStrings.ResourceManager.GetString("InvalidBodyTypeForThisMessage"), value);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000095FA File Offset: 0x000077FA
		public static string GetLocalizedString(EmailMessageStrings.IDs key)
		{
			return EmailMessageStrings.ResourceManager.GetString(EmailMessageStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000226 RID: 550
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(36);

		// Token: 0x04000227 RID: 551
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.EmailMessageStrings", typeof(EmailMessageStrings).GetTypeInfo().Assembly);

		// Token: 0x020000B5 RID: 181
		public enum IDs : uint
		{
			// Token: 0x04000229 RID: 553
			ReferenceBodyDoesNotBelongToSameMessage = 1282546634U,
			// Token: 0x0400022A RID: 554
			CannotAccessMapiPropsFromPureMimeMessage = 4174367109U,
			// Token: 0x0400022B RID: 555
			ErrorInit = 2479849522U,
			// Token: 0x0400022C RID: 556
			TooManyEntriesInApplefile = 1426955056U,
			// Token: 0x0400022D RID: 557
			MimeDocumentRootPartMustNotBeNull = 392243280U,
			// Token: 0x0400022E RID: 558
			WrongOffsetsInApplefile = 4142370976U,
			// Token: 0x0400022F RID: 559
			CannotCreateAlternativeBody = 1857526624U,
			// Token: 0x04000230 RID: 560
			CanOnlyAddInlineAttachmentsToHtmlBody = 3435085534U,
			// Token: 0x04000231 RID: 561
			ErrorBeforeFirst = 4119843803U,
			// Token: 0x04000232 RID: 562
			ErrorAfterLast = 3424442414U,
			// Token: 0x04000233 RID: 563
			CannotAttachEmbeddedMapiMessageToMime = 2804270193U,
			// Token: 0x04000234 RID: 564
			BodyExistsInTnefMessage = 764847365U,
			// Token: 0x04000235 RID: 565
			NotSupportedForRtfBody = 1685978706U,
			// Token: 0x04000236 RID: 566
			CanOnlyAddInlineAttachmentForHtmlBody = 368316767U,
			// Token: 0x04000237 RID: 567
			UnexpectedEndOfStream = 797792475U,
			// Token: 0x04000238 RID: 568
			CannotSetEmbeddedMessageForTnefAttachment = 4122747589U,
			// Token: 0x04000239 RID: 569
			WrongAppleMagicNumber = 4281132505U,
			// Token: 0x0400023A RID: 570
			ContentTypeCannotBeMultipart = 3934212261U,
			// Token: 0x0400023B RID: 571
			CollectionHasChanged = 1586749494U,
			// Token: 0x0400023C RID: 572
			AttachmentRemovedFromMessage = 4139915868U,
			// Token: 0x0400023D RID: 573
			DigestCanOnlyContainMessage822Attachments = 2815148485U,
			// Token: 0x0400023E RID: 574
			RecipientAlreadyHasParent = 2676324379U,
			// Token: 0x0400023F RID: 575
			WrongAppleVersionNumber = 1686645306U,
			// Token: 0x04000240 RID: 576
			MacBinWrongFilename = 3682676966U,
			// Token: 0x04000241 RID: 577
			CannotAddAttachment = 532813113U,
			// Token: 0x04000242 RID: 578
			NoBodyForInlineAttachment = 414550776U,
			// Token: 0x04000243 RID: 579
			TnefIsMissingAttachRenderData = 1975150156U,
			// Token: 0x04000244 RID: 580
			CannotSetEmbeddedMessageForNonMessageRfc822Attachment = 2369512451U,
			// Token: 0x04000245 RID: 581
			WrongMacBinHeader = 2544842942U,
			// Token: 0x04000246 RID: 582
			BodyAlreadyHasParent = 3779231310U,
			// Token: 0x04000247 RID: 583
			CollectionIsReadOnly = 2792487344U,
			// Token: 0x04000248 RID: 584
			ChangingDntNotSupportedForEmbeddedTnefMessages = 2839669472U,
			// Token: 0x04000249 RID: 585
			CannotWriteBodyDoesNotExist = 2005747751U,
			// Token: 0x0400024A RID: 586
			CannotSetNativePropertyForMimeRecipient = 665063789U,
			// Token: 0x0400024B RID: 587
			ArgumentInvalidOffLen = 182340944U,
			// Token: 0x0400024C RID: 588
			TnefContainsMultipleStreams = 2700714933U
		}

		// Token: 0x020000B6 RID: 182
		private enum ParamIDs
		{
			// Token: 0x0400024E RID: 590
			UnsupportedBodyType,
			// Token: 0x0400024F RID: 591
			EntryLengthTooBigInApplefile,
			// Token: 0x04000250 RID: 592
			CannotCreateSpecifiedBodyFormat,
			// Token: 0x04000251 RID: 593
			InvalidCharset,
			// Token: 0x04000252 RID: 594
			NestingTooDeep,
			// Token: 0x04000253 RID: 595
			InvalidBodyTypeForThisMessage
		}
	}
}
