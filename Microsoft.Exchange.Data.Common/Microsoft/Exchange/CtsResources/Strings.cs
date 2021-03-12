using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x02000016 RID: 22
	internal static class Strings
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000421C File Offset: 0x0000241C
		static Strings()
		{
			Strings.stringIDs.Add(3398506793U, "HeaderReaderCannotBeUsedInThisState");
			Strings.stringIDs.Add(3277623286U, "CannotLoadIntoNonEmptyDocument");
			Strings.stringIDs.Add(1258373174U, "NewChildNotMimeParameter");
			Strings.stringIDs.Add(327877968U, "RefChildIsNotMyChild");
			Strings.stringIDs.Add(134484231U, "CantSetValueOfRfc2231ContinuationSegment");
			Strings.stringIDs.Add(1744024036U, "ChildrenCannotBeAddedToReceivedHeader");
			Strings.stringIDs.Add(3057104001U, "ParameterReaderNotInitialized");
			Strings.stringIDs.Add(238701657U, "CannotWriteHeaderValueMoreThanOnce");
			Strings.stringIDs.Add(3480118652U, "OldChildIsNotMyChild");
			Strings.stringIDs.Add(1858092871U, "CannotWriteEmptyOrNullBoundary");
			Strings.stringIDs.Add(64440155U, "CannotReadContentWhileStreamIsActive");
			Strings.stringIDs.Add(973871481U, "BinHexNotSupportedForThisMethod");
			Strings.stringIDs.Add(3780024871U, "RefHeaderIsNotMyChild");
			Strings.stringIDs.Add(1065856182U, "UnicodeMimeHeaderReceivedNotSupported");
			Strings.stringIDs.Add(3506183213U, "CachingModeSourceButStreamCannotSeek");
			Strings.stringIDs.Add(1909781391U, "LoadingStopped");
			Strings.stringIDs.Add(1572349307U, "CantGetValueOfRfc2231ContinuationSegment");
			Strings.stringIDs.Add(2512231815U, "HeaderReaderNotInitialized");
			Strings.stringIDs.Add(3307514334U, "MimeHandlerErrorNotEmbeddedMessage");
			Strings.stringIDs.Add(2491124350U, "CannotWriteRecipientsHere");
			Strings.stringIDs.Add(1665262320U, "ChildrenCannotBeAddedToTextHeader");
			Strings.stringIDs.Add(2556127565U, "ModifyingRawContentOfMultipartNotSupported");
			Strings.stringIDs.Add(3341745715U, "NewChildIsNotARecipient");
			Strings.stringIDs.Add(265752974U, "CantCopyToDifferentObjectType");
			Strings.stringIDs.Add(4072557241U, "NonMultiPartPartsCannotHaveChildren");
			Strings.stringIDs.Add(1119528296U, "CannotMixReadRawContentAndReadContent");
			Strings.stringIDs.Add(3324675010U, "AddingChildrenToAsciiTextHeader");
			Strings.stringIDs.Add(3693127890U, "NewChildNotRecipientOrGroup");
			Strings.stringIDs.Add(2413759299U, "StrictComplianceViolation");
			Strings.stringIDs.Add(2605804358U, "RootPartCantHaveAParent");
			Strings.stringIDs.Add(325536703U, "InternalMimeError");
			Strings.stringIDs.Add(2332969818U, "UnrecognizedTransferEncodingUsed");
			Strings.stringIDs.Add(520133541U, "OperationNotValidInThisReaderState");
			Strings.stringIDs.Add(110823742U, "NewChildNotMimeHeader");
			Strings.stringIDs.Add(2924274099U, "CannotWriteUnicodeHeaderValue");
			Strings.stringIDs.Add(2752555714U, "MimeHandlerErrorMoreThanOneOuterContentPushStream");
			Strings.stringIDs.Add(21266136U, "CannotDecodeContentStream");
			Strings.stringIDs.Add(1044739797U, "InvalidBoundary");
			Strings.stringIDs.Add(2360968430U, "StreamMustSupportRead");
			Strings.stringIDs.Add(1018032548U, "CurrentPartIsNotEmbeddedMessage");
			Strings.stringIDs.Add(2197652894U, "CannotWriteGroupEndHere");
			Strings.stringIDs.Add(1904569722U, "MimeVersionRequiredForMultipart");
			Strings.stringIDs.Add(4119843803U, "ErrorBeforeFirst");
			Strings.stringIDs.Add(3885506491U, "CantSetRawValueOfRfc2231ContinuationSegment");
			Strings.stringIDs.Add(3099580008U, "StreamMustAllowRead");
			Strings.stringIDs.Add(1597481536U, "ReaderIsNotPositionedOnHeaderWithParameters");
			Strings.stringIDs.Add(2028330664U, "NewChildCannotHaveDifferentParent");
			Strings.stringIDs.Add(1693234793U, "CannotWriteGroupStartHere");
			Strings.stringIDs.Add(4251577240U, "CannotWriteHeadersHere");
			Strings.stringIDs.Add(1786178430U, "HeaderCannotHaveParameters");
			Strings.stringIDs.Add(2213004504U, "ParametersCannotHaveChildNodes");
			Strings.stringIDs.Add(746087816U, "HeaderReaderIsNotPositionedOnAHeader");
			Strings.stringIDs.Add(929328884U, "CurrentAddressIsGroupAndCannotHaveEmail");
			Strings.stringIDs.Add(631385300U, "AddressParserNotInitialized");
			Strings.stringIDs.Add(3697700598U, "PartContentIsBeingWritten");
			Strings.stringIDs.Add(479601644U, "MultipartCannotContainContent");
			Strings.stringIDs.Add(1300692199U, "NewChildIsNotAPart");
			Strings.stringIDs.Add(3782248328U, "RecipientsCannotHaveChildNodes");
			Strings.stringIDs.Add(1601383848U, "ContentAlreadyWritten");
			Strings.stringIDs.Add(1687756747U, "CannotAddChildrenToMimeHeaderDate");
			Strings.stringIDs.Add(161660084U, "CannotGetLoadStreamMoreThanOnce");
			Strings.stringIDs.Add(1406718177U, "ValueTooLong");
			Strings.stringIDs.Add(557180799U, "CannotDetermineHeaderNameFromId");
			Strings.stringIDs.Add(340926097U, "DocumentCloneNotSupportedInThisState");
			Strings.stringIDs.Add(1844115139U, "InvalidHeaderId");
			Strings.stringIDs.Add(1224345327U, "AddressReaderIsNotPositionedOnAddress");
			Strings.stringIDs.Add(3344340751U, "ParameterReaderIsNotPositionedOnParameter");
			Strings.stringIDs.Add(3424442414U, "ErrorAfterLast");
			Strings.stringIDs.Add(1536364454U, "CannotWriteParametersHere");
			Strings.stringIDs.Add(526908964U, "CannotWriteAfterFlush");
			Strings.stringIDs.Add(2286678017U, "UnicodeMimeHeaderAddressNotSupported");
			Strings.stringIDs.Add(42821332U, "CannotWriteParametersOnThisHeader");
			Strings.stringIDs.Add(1074868404U, "CannotStartPartHere");
			Strings.stringIDs.Add(239464507U, "CannotEndPartHere");
			Strings.stringIDs.Add(2034730061U, "ThisPartBelongsToSubtreeOfNewChild");
			Strings.stringIDs.Add(4140427462U, "StreamMustSupportWriting");
			Strings.stringIDs.Add(1264059714U, "HeaderCannotHaveAddresses");
			Strings.stringIDs.Add(1817020120U, "OnlyOneOuterContentPushStreamAllowed");
			Strings.stringIDs.Add(3644237080U, "AddressReaderNotInitialized");
			Strings.stringIDs.Add(3716863546U, "ReaderIsNotPositionedOnAddressHeader");
			Strings.stringIDs.Add(2657804913U, "EmbeddedMessageReaderNeedsToBeClosedFirst");
			Strings.stringIDs.Add(1462963282U, "CannotWritePartContentNow");
			Strings.stringIDs.Add(2748764102U, "StreamNoLongerValid");
			Strings.stringIDs.Add(892057174U, "BareLinefeedRejected");
			Strings.stringIDs.Add(3956216251U, "AddressReaderIsNotPositionedOnAGroup");
			Strings.stringIDs.Add(1674677912U, "CannotWriteHeaderValueHere");
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004910 File Offset: 0x00002B10
		public static string HeaderReaderCannotBeUsedInThisState
		{
			get
			{
				return Strings.ResourceManager.GetString("HeaderReaderCannotBeUsedInThisState");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004921 File Offset: 0x00002B21
		public static string CannotLoadIntoNonEmptyDocument
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotLoadIntoNonEmptyDocument");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004932 File Offset: 0x00002B32
		public static string NewChildNotMimeParameter
		{
			get
			{
				return Strings.ResourceManager.GetString("NewChildNotMimeParameter");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004943 File Offset: 0x00002B43
		public static string RefChildIsNotMyChild
		{
			get
			{
				return Strings.ResourceManager.GetString("RefChildIsNotMyChild");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004954 File Offset: 0x00002B54
		public static string CantSetValueOfRfc2231ContinuationSegment
		{
			get
			{
				return Strings.ResourceManager.GetString("CantSetValueOfRfc2231ContinuationSegment");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004965 File Offset: 0x00002B65
		public static string ChildrenCannotBeAddedToReceivedHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("ChildrenCannotBeAddedToReceivedHeader");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004976 File Offset: 0x00002B76
		public static string ParameterReaderNotInitialized
		{
			get
			{
				return Strings.ResourceManager.GetString("ParameterReaderNotInitialized");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004987 File Offset: 0x00002B87
		public static string CannotWriteHeaderValueMoreThanOnce
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteHeaderValueMoreThanOnce");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004998 File Offset: 0x00002B98
		public static string OldChildIsNotMyChild
		{
			get
			{
				return Strings.ResourceManager.GetString("OldChildIsNotMyChild");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000049A9 File Offset: 0x00002BA9
		public static string CannotWriteEmptyOrNullBoundary
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteEmptyOrNullBoundary");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000049BA File Offset: 0x00002BBA
		public static string CannotReadContentWhileStreamIsActive
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotReadContentWhileStreamIsActive");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000049CB File Offset: 0x00002BCB
		public static string BinHexNotSupportedForThisMethod
		{
			get
			{
				return Strings.ResourceManager.GetString("BinHexNotSupportedForThisMethod");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000049DC File Offset: 0x00002BDC
		public static string RefHeaderIsNotMyChild
		{
			get
			{
				return Strings.ResourceManager.GetString("RefHeaderIsNotMyChild");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000049ED File Offset: 0x00002BED
		public static string UnicodeMimeHeaderReceivedNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("UnicodeMimeHeaderReceivedNotSupported");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000049FE File Offset: 0x00002BFE
		public static string CachingModeSourceButStreamCannotSeek
		{
			get
			{
				return Strings.ResourceManager.GetString("CachingModeSourceButStreamCannotSeek");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004A0F File Offset: 0x00002C0F
		public static string LoadingStopped
		{
			get
			{
				return Strings.ResourceManager.GetString("LoadingStopped");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004A20 File Offset: 0x00002C20
		public static string CantGetValueOfRfc2231ContinuationSegment
		{
			get
			{
				return Strings.ResourceManager.GetString("CantGetValueOfRfc2231ContinuationSegment");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004A31 File Offset: 0x00002C31
		public static string HeaderReaderNotInitialized
		{
			get
			{
				return Strings.ResourceManager.GetString("HeaderReaderNotInitialized");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004A42 File Offset: 0x00002C42
		public static string MimeHandlerErrorNotEmbeddedMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("MimeHandlerErrorNotEmbeddedMessage");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004A53 File Offset: 0x00002C53
		public static string CannotWriteRecipientsHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteRecipientsHere");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004A64 File Offset: 0x00002C64
		public static string ChildrenCannotBeAddedToTextHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("ChildrenCannotBeAddedToTextHeader");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004A75 File Offset: 0x00002C75
		public static string ModifyingRawContentOfMultipartNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ModifyingRawContentOfMultipartNotSupported");
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004A86 File Offset: 0x00002C86
		public static string NewChildIsNotARecipient
		{
			get
			{
				return Strings.ResourceManager.GetString("NewChildIsNotARecipient");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004A97 File Offset: 0x00002C97
		public static string CantCopyToDifferentObjectType
		{
			get
			{
				return Strings.ResourceManager.GetString("CantCopyToDifferentObjectType");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public static string NonMultiPartPartsCannotHaveChildren
		{
			get
			{
				return Strings.ResourceManager.GetString("NonMultiPartPartsCannotHaveChildren");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004AB9 File Offset: 0x00002CB9
		public static string CannotMixReadRawContentAndReadContent
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotMixReadRawContentAndReadContent");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004ACA File Offset: 0x00002CCA
		public static string AddingChildrenToAsciiTextHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("AddingChildrenToAsciiTextHeader");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004ADB File Offset: 0x00002CDB
		public static string NewChildNotRecipientOrGroup
		{
			get
			{
				return Strings.ResourceManager.GetString("NewChildNotRecipientOrGroup");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004AEC File Offset: 0x00002CEC
		public static string StrictComplianceViolation
		{
			get
			{
				return Strings.ResourceManager.GetString("StrictComplianceViolation");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004AFD File Offset: 0x00002CFD
		public static string RootPartCantHaveAParent
		{
			get
			{
				return Strings.ResourceManager.GetString("RootPartCantHaveAParent");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004B0E File Offset: 0x00002D0E
		public static string InternalMimeError
		{
			get
			{
				return Strings.ResourceManager.GetString("InternalMimeError");
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004B1F File Offset: 0x00002D1F
		public static string PartNestingTooDeep(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("PartNestingTooDeep"), actual, limit);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004B41 File Offset: 0x00002D41
		public static string UnrecognizedTransferEncodingUsed
		{
			get
			{
				return Strings.ResourceManager.GetString("UnrecognizedTransferEncodingUsed");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004B52 File Offset: 0x00002D52
		public static string OperationNotValidInThisReaderState
		{
			get
			{
				return Strings.ResourceManager.GetString("OperationNotValidInThisReaderState");
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004B63 File Offset: 0x00002D63
		public static string TooManyHeaderBytes(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("TooManyHeaderBytes"), actual, limit);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004B85 File Offset: 0x00002D85
		public static string NewChildNotMimeHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("NewChildNotMimeHeader");
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004B96 File Offset: 0x00002D96
		public static string InvalidHeaderName(string name, int position)
		{
			return string.Format(Strings.ResourceManager.GetString("InvalidHeaderName"), name, position);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004BB3 File Offset: 0x00002DB3
		public static string CannotWriteUnicodeHeaderValue
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteUnicodeHeaderValue");
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public static string MimeHandlerErrorMoreThanOneOuterContentPushStream
		{
			get
			{
				return Strings.ResourceManager.GetString("MimeHandlerErrorMoreThanOneOuterContentPushStream");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004BD5 File Offset: 0x00002DD5
		public static string CannotDecodeContentStream
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotDecodeContentStream");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004BE6 File Offset: 0x00002DE6
		public static string InvalidBoundary
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidBoundary");
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004BF7 File Offset: 0x00002DF7
		public static string InputStreamTooLong(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("InputStreamTooLong"), actual, limit);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004C19 File Offset: 0x00002E19
		public static string StreamMustSupportRead
		{
			get
			{
				return Strings.ResourceManager.GetString("StreamMustSupportRead");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004C2A File Offset: 0x00002E2A
		public static string CurrentPartIsNotEmbeddedMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("CurrentPartIsNotEmbeddedMessage");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004C3B File Offset: 0x00002E3B
		public static string CannotWriteGroupEndHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteGroupEndHere");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004C4C File Offset: 0x00002E4C
		public static string MimeVersionRequiredForMultipart
		{
			get
			{
				return Strings.ResourceManager.GetString("MimeVersionRequiredForMultipart");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004C5D File Offset: 0x00002E5D
		public static string ErrorBeforeFirst
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorBeforeFirst");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004C6E File Offset: 0x00002E6E
		public static string CantSetRawValueOfRfc2231ContinuationSegment
		{
			get
			{
				return Strings.ResourceManager.GetString("CantSetRawValueOfRfc2231ContinuationSegment");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004C7F File Offset: 0x00002E7F
		public static string StreamMustAllowRead
		{
			get
			{
				return Strings.ResourceManager.GetString("StreamMustAllowRead");
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004C90 File Offset: 0x00002E90
		public static string LengthExceeded(int sum, int length)
		{
			return string.Format(Strings.ResourceManager.GetString("LengthExceeded"), sum, length);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004CB2 File Offset: 0x00002EB2
		public static string ReaderIsNotPositionedOnHeaderWithParameters
		{
			get
			{
				return Strings.ResourceManager.GetString("ReaderIsNotPositionedOnHeaderWithParameters");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004CC3 File Offset: 0x00002EC3
		public static string NewChildCannotHaveDifferentParent
		{
			get
			{
				return Strings.ResourceManager.GetString("NewChildCannotHaveDifferentParent");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public static string CannotWriteGroupStartHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteGroupStartHere");
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public static string TooManyParameters(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("TooManyParameters"), actual, limit);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004D07 File Offset: 0x00002F07
		public static string TooManyParts(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("TooManyParts"), actual, limit);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004D29 File Offset: 0x00002F29
		public static string CannotWriteHeadersHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteHeadersHere");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004D3A File Offset: 0x00002F3A
		public static string HeaderCannotHaveParameters
		{
			get
			{
				return Strings.ResourceManager.GetString("HeaderCannotHaveParameters");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004D4B File Offset: 0x00002F4B
		public static string ParametersCannotHaveChildNodes
		{
			get
			{
				return Strings.ResourceManager.GetString("ParametersCannotHaveChildNodes");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004D5C File Offset: 0x00002F5C
		public static string HeaderReaderIsNotPositionedOnAHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("HeaderReaderIsNotPositionedOnAHeader");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004D6D File Offset: 0x00002F6D
		public static string CurrentAddressIsGroupAndCannotHaveEmail
		{
			get
			{
				return Strings.ResourceManager.GetString("CurrentAddressIsGroupAndCannotHaveEmail");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004D7E File Offset: 0x00002F7E
		public static string AddressParserNotInitialized
		{
			get
			{
				return Strings.ResourceManager.GetString("AddressParserNotInitialized");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004D8F File Offset: 0x00002F8F
		public static string PartContentIsBeingWritten
		{
			get
			{
				return Strings.ResourceManager.GetString("PartContentIsBeingWritten");
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public static string TooManyAddressItems(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("TooManyAddressItems"), actual, limit);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public static string MultipartCannotContainContent
		{
			get
			{
				return Strings.ResourceManager.GetString("MultipartCannotContainContent");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004DD3 File Offset: 0x00002FD3
		public static string NewChildIsNotAPart
		{
			get
			{
				return Strings.ResourceManager.GetString("NewChildIsNotAPart");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public static string RecipientsCannotHaveChildNodes
		{
			get
			{
				return Strings.ResourceManager.GetString("RecipientsCannotHaveChildNodes");
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004DF5 File Offset: 0x00002FF5
		public static string ThisNodeDoesNotSupportCloning(string type)
		{
			return string.Format(Strings.ResourceManager.GetString("ThisNodeDoesNotSupportCloning"), type);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004E0C File Offset: 0x0000300C
		public static string ContentAlreadyWritten
		{
			get
			{
				return Strings.ResourceManager.GetString("ContentAlreadyWritten");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004E1D File Offset: 0x0000301D
		public static string CannotAddChildrenToMimeHeaderDate
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotAddChildrenToMimeHeaderDate");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004E2E File Offset: 0x0000302E
		public static string CannotGetLoadStreamMoreThanOnce
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotGetLoadStreamMoreThanOnce");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004E3F File Offset: 0x0000303F
		public static string ValueTooLong
		{
			get
			{
				return Strings.ResourceManager.GetString("ValueTooLong");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004E50 File Offset: 0x00003050
		public static string CannotDetermineHeaderNameFromId
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotDetermineHeaderNameFromId");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004E61 File Offset: 0x00003061
		public static string DocumentCloneNotSupportedInThisState
		{
			get
			{
				return Strings.ResourceManager.GetString("DocumentCloneNotSupportedInThisState");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004E72 File Offset: 0x00003072
		public static string InvalidHeaderId
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidHeaderId");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004E83 File Offset: 0x00003083
		public static string AddressReaderIsNotPositionedOnAddress
		{
			get
			{
				return Strings.ResourceManager.GetString("AddressReaderIsNotPositionedOnAddress");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004E94 File Offset: 0x00003094
		public static string ParameterReaderIsNotPositionedOnParameter
		{
			get
			{
				return Strings.ResourceManager.GetString("ParameterReaderIsNotPositionedOnParameter");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004EA5 File Offset: 0x000030A5
		public static string ErrorAfterLast
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorAfterLast");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004EB6 File Offset: 0x000030B6
		public static string CannotWriteParametersHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteParametersHere");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004EC7 File Offset: 0x000030C7
		public static string CannotWriteAfterFlush
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteAfterFlush");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004ED8 File Offset: 0x000030D8
		public static string UnicodeMimeHeaderAddressNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("UnicodeMimeHeaderAddressNotSupported");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004EE9 File Offset: 0x000030E9
		public static string CannotWriteParametersOnThisHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteParametersOnThisHeader");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004EFA File Offset: 0x000030FA
		public static string CannotStartPartHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotStartPartHere");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004F0B File Offset: 0x0000310B
		public static string CannotEndPartHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotEndPartHere");
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004F1C File Offset: 0x0000311C
		public static string TooManyTextValueBytes(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("TooManyTextValueBytes"), actual, limit);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004F3E File Offset: 0x0000313E
		public static string NameNotValidForThisHeaderType(string name, string typeName, string correctTypeName)
		{
			return string.Format(Strings.ResourceManager.GetString("NameNotValidForThisHeaderType"), name, typeName, correctTypeName);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004F57 File Offset: 0x00003157
		public static string TooManyHeaders(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("TooManyHeaders"), actual, limit);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004F79 File Offset: 0x00003179
		public static string ThisPartBelongsToSubtreeOfNewChild
		{
			get
			{
				return Strings.ResourceManager.GetString("ThisPartBelongsToSubtreeOfNewChild");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004F8A File Offset: 0x0000318A
		public static string StreamMustSupportWriting
		{
			get
			{
				return Strings.ResourceManager.GetString("StreamMustSupportWriting");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004F9B File Offset: 0x0000319B
		public static string HeaderCannotHaveAddresses
		{
			get
			{
				return Strings.ResourceManager.GetString("HeaderCannotHaveAddresses");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004FAC File Offset: 0x000031AC
		public static string OnlyOneOuterContentPushStreamAllowed
		{
			get
			{
				return Strings.ResourceManager.GetString("OnlyOneOuterContentPushStreamAllowed");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004FBD File Offset: 0x000031BD
		public static string AddressReaderNotInitialized
		{
			get
			{
				return Strings.ResourceManager.GetString("AddressReaderNotInitialized");
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004FCE File Offset: 0x000031CE
		public static string EmbeddedNestingTooDeep(int actual, int limit)
		{
			return string.Format(Strings.ResourceManager.GetString("EmbeddedNestingTooDeep"), actual, limit);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004FF0 File Offset: 0x000031F0
		public static string ReaderIsNotPositionedOnAddressHeader
		{
			get
			{
				return Strings.ResourceManager.GetString("ReaderIsNotPositionedOnAddressHeader");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005001 File Offset: 0x00003201
		public static string EmbeddedMessageReaderNeedsToBeClosedFirst
		{
			get
			{
				return Strings.ResourceManager.GetString("EmbeddedMessageReaderNeedsToBeClosedFirst");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005012 File Offset: 0x00003212
		public static string CannotWritePartContentNow
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWritePartContentNow");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005023 File Offset: 0x00003223
		public static string StreamNoLongerValid
		{
			get
			{
				return Strings.ResourceManager.GetString("StreamNoLongerValid");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005034 File Offset: 0x00003234
		public static string BareLinefeedRejected
		{
			get
			{
				return Strings.ResourceManager.GetString("BareLinefeedRejected");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005045 File Offset: 0x00003245
		public static string AddressReaderIsNotPositionedOnAGroup
		{
			get
			{
				return Strings.ResourceManager.GetString("AddressReaderIsNotPositionedOnAGroup");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005056 File Offset: 0x00003256
		public static string CannotWriteHeaderValueHere
		{
			get
			{
				return Strings.ResourceManager.GetString("CannotWriteHeaderValueHere");
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005067 File Offset: 0x00003267
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x0400003E RID: 62
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(86);

		// Token: 0x0400003F RID: 63
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000017 RID: 23
		public enum IDs : uint
		{
			// Token: 0x04000041 RID: 65
			HeaderReaderCannotBeUsedInThisState = 3398506793U,
			// Token: 0x04000042 RID: 66
			CannotLoadIntoNonEmptyDocument = 3277623286U,
			// Token: 0x04000043 RID: 67
			NewChildNotMimeParameter = 1258373174U,
			// Token: 0x04000044 RID: 68
			RefChildIsNotMyChild = 327877968U,
			// Token: 0x04000045 RID: 69
			CantSetValueOfRfc2231ContinuationSegment = 134484231U,
			// Token: 0x04000046 RID: 70
			ChildrenCannotBeAddedToReceivedHeader = 1744024036U,
			// Token: 0x04000047 RID: 71
			ParameterReaderNotInitialized = 3057104001U,
			// Token: 0x04000048 RID: 72
			CannotWriteHeaderValueMoreThanOnce = 238701657U,
			// Token: 0x04000049 RID: 73
			OldChildIsNotMyChild = 3480118652U,
			// Token: 0x0400004A RID: 74
			CannotWriteEmptyOrNullBoundary = 1858092871U,
			// Token: 0x0400004B RID: 75
			CannotReadContentWhileStreamIsActive = 64440155U,
			// Token: 0x0400004C RID: 76
			BinHexNotSupportedForThisMethod = 973871481U,
			// Token: 0x0400004D RID: 77
			RefHeaderIsNotMyChild = 3780024871U,
			// Token: 0x0400004E RID: 78
			UnicodeMimeHeaderReceivedNotSupported = 1065856182U,
			// Token: 0x0400004F RID: 79
			CachingModeSourceButStreamCannotSeek = 3506183213U,
			// Token: 0x04000050 RID: 80
			LoadingStopped = 1909781391U,
			// Token: 0x04000051 RID: 81
			CantGetValueOfRfc2231ContinuationSegment = 1572349307U,
			// Token: 0x04000052 RID: 82
			HeaderReaderNotInitialized = 2512231815U,
			// Token: 0x04000053 RID: 83
			MimeHandlerErrorNotEmbeddedMessage = 3307514334U,
			// Token: 0x04000054 RID: 84
			CannotWriteRecipientsHere = 2491124350U,
			// Token: 0x04000055 RID: 85
			ChildrenCannotBeAddedToTextHeader = 1665262320U,
			// Token: 0x04000056 RID: 86
			ModifyingRawContentOfMultipartNotSupported = 2556127565U,
			// Token: 0x04000057 RID: 87
			NewChildIsNotARecipient = 3341745715U,
			// Token: 0x04000058 RID: 88
			CantCopyToDifferentObjectType = 265752974U,
			// Token: 0x04000059 RID: 89
			NonMultiPartPartsCannotHaveChildren = 4072557241U,
			// Token: 0x0400005A RID: 90
			CannotMixReadRawContentAndReadContent = 1119528296U,
			// Token: 0x0400005B RID: 91
			AddingChildrenToAsciiTextHeader = 3324675010U,
			// Token: 0x0400005C RID: 92
			NewChildNotRecipientOrGroup = 3693127890U,
			// Token: 0x0400005D RID: 93
			StrictComplianceViolation = 2413759299U,
			// Token: 0x0400005E RID: 94
			RootPartCantHaveAParent = 2605804358U,
			// Token: 0x0400005F RID: 95
			InternalMimeError = 325536703U,
			// Token: 0x04000060 RID: 96
			UnrecognizedTransferEncodingUsed = 2332969818U,
			// Token: 0x04000061 RID: 97
			OperationNotValidInThisReaderState = 520133541U,
			// Token: 0x04000062 RID: 98
			NewChildNotMimeHeader = 110823742U,
			// Token: 0x04000063 RID: 99
			CannotWriteUnicodeHeaderValue = 2924274099U,
			// Token: 0x04000064 RID: 100
			MimeHandlerErrorMoreThanOneOuterContentPushStream = 2752555714U,
			// Token: 0x04000065 RID: 101
			CannotDecodeContentStream = 21266136U,
			// Token: 0x04000066 RID: 102
			InvalidBoundary = 1044739797U,
			// Token: 0x04000067 RID: 103
			StreamMustSupportRead = 2360968430U,
			// Token: 0x04000068 RID: 104
			CurrentPartIsNotEmbeddedMessage = 1018032548U,
			// Token: 0x04000069 RID: 105
			CannotWriteGroupEndHere = 2197652894U,
			// Token: 0x0400006A RID: 106
			MimeVersionRequiredForMultipart = 1904569722U,
			// Token: 0x0400006B RID: 107
			ErrorBeforeFirst = 4119843803U,
			// Token: 0x0400006C RID: 108
			CantSetRawValueOfRfc2231ContinuationSegment = 3885506491U,
			// Token: 0x0400006D RID: 109
			StreamMustAllowRead = 3099580008U,
			// Token: 0x0400006E RID: 110
			ReaderIsNotPositionedOnHeaderWithParameters = 1597481536U,
			// Token: 0x0400006F RID: 111
			NewChildCannotHaveDifferentParent = 2028330664U,
			// Token: 0x04000070 RID: 112
			CannotWriteGroupStartHere = 1693234793U,
			// Token: 0x04000071 RID: 113
			CannotWriteHeadersHere = 4251577240U,
			// Token: 0x04000072 RID: 114
			HeaderCannotHaveParameters = 1786178430U,
			// Token: 0x04000073 RID: 115
			ParametersCannotHaveChildNodes = 2213004504U,
			// Token: 0x04000074 RID: 116
			HeaderReaderIsNotPositionedOnAHeader = 746087816U,
			// Token: 0x04000075 RID: 117
			CurrentAddressIsGroupAndCannotHaveEmail = 929328884U,
			// Token: 0x04000076 RID: 118
			AddressParserNotInitialized = 631385300U,
			// Token: 0x04000077 RID: 119
			PartContentIsBeingWritten = 3697700598U,
			// Token: 0x04000078 RID: 120
			MultipartCannotContainContent = 479601644U,
			// Token: 0x04000079 RID: 121
			NewChildIsNotAPart = 1300692199U,
			// Token: 0x0400007A RID: 122
			RecipientsCannotHaveChildNodes = 3782248328U,
			// Token: 0x0400007B RID: 123
			ContentAlreadyWritten = 1601383848U,
			// Token: 0x0400007C RID: 124
			CannotAddChildrenToMimeHeaderDate = 1687756747U,
			// Token: 0x0400007D RID: 125
			CannotGetLoadStreamMoreThanOnce = 161660084U,
			// Token: 0x0400007E RID: 126
			ValueTooLong = 1406718177U,
			// Token: 0x0400007F RID: 127
			CannotDetermineHeaderNameFromId = 557180799U,
			// Token: 0x04000080 RID: 128
			DocumentCloneNotSupportedInThisState = 340926097U,
			// Token: 0x04000081 RID: 129
			InvalidHeaderId = 1844115139U,
			// Token: 0x04000082 RID: 130
			AddressReaderIsNotPositionedOnAddress = 1224345327U,
			// Token: 0x04000083 RID: 131
			ParameterReaderIsNotPositionedOnParameter = 3344340751U,
			// Token: 0x04000084 RID: 132
			ErrorAfterLast = 3424442414U,
			// Token: 0x04000085 RID: 133
			CannotWriteParametersHere = 1536364454U,
			// Token: 0x04000086 RID: 134
			CannotWriteAfterFlush = 526908964U,
			// Token: 0x04000087 RID: 135
			UnicodeMimeHeaderAddressNotSupported = 2286678017U,
			// Token: 0x04000088 RID: 136
			CannotWriteParametersOnThisHeader = 42821332U,
			// Token: 0x04000089 RID: 137
			CannotStartPartHere = 1074868404U,
			// Token: 0x0400008A RID: 138
			CannotEndPartHere = 239464507U,
			// Token: 0x0400008B RID: 139
			ThisPartBelongsToSubtreeOfNewChild = 2034730061U,
			// Token: 0x0400008C RID: 140
			StreamMustSupportWriting = 4140427462U,
			// Token: 0x0400008D RID: 141
			HeaderCannotHaveAddresses = 1264059714U,
			// Token: 0x0400008E RID: 142
			OnlyOneOuterContentPushStreamAllowed = 1817020120U,
			// Token: 0x0400008F RID: 143
			AddressReaderNotInitialized = 3644237080U,
			// Token: 0x04000090 RID: 144
			ReaderIsNotPositionedOnAddressHeader = 3716863546U,
			// Token: 0x04000091 RID: 145
			EmbeddedMessageReaderNeedsToBeClosedFirst = 2657804913U,
			// Token: 0x04000092 RID: 146
			CannotWritePartContentNow = 1462963282U,
			// Token: 0x04000093 RID: 147
			StreamNoLongerValid = 2748764102U,
			// Token: 0x04000094 RID: 148
			BareLinefeedRejected = 892057174U,
			// Token: 0x04000095 RID: 149
			AddressReaderIsNotPositionedOnAGroup = 3956216251U,
			// Token: 0x04000096 RID: 150
			CannotWriteHeaderValueHere = 1674677912U
		}

		// Token: 0x02000018 RID: 24
		private enum ParamIDs
		{
			// Token: 0x04000098 RID: 152
			PartNestingTooDeep,
			// Token: 0x04000099 RID: 153
			TooManyHeaderBytes,
			// Token: 0x0400009A RID: 154
			InvalidHeaderName,
			// Token: 0x0400009B RID: 155
			InputStreamTooLong,
			// Token: 0x0400009C RID: 156
			LengthExceeded,
			// Token: 0x0400009D RID: 157
			TooManyParameters,
			// Token: 0x0400009E RID: 158
			TooManyParts,
			// Token: 0x0400009F RID: 159
			TooManyAddressItems,
			// Token: 0x040000A0 RID: 160
			ThisNodeDoesNotSupportCloning,
			// Token: 0x040000A1 RID: 161
			TooManyTextValueBytes,
			// Token: 0x040000A2 RID: 162
			NameNotValidForThisHeaderType,
			// Token: 0x040000A3 RID: 163
			TooManyHeaders,
			// Token: 0x040000A4 RID: 164
			EmbeddedNestingTooDeep
		}
	}
}
