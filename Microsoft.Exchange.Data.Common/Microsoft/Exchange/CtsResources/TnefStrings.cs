using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x020000DF RID: 223
	internal static class TnefStrings
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x00030330 File Offset: 0x0002E530
		static TnefStrings()
		{
			TnefStrings.stringIDs.Add(149645149U, "WriterPropertyNameEmptyOrTooLong");
			TnefStrings.stringIDs.Add(2720608910U, "ReaderComplianceInvalidMessageClassNotZeroTerminated");
			TnefStrings.stringIDs.Add(718178560U, "ReaderInvalidOperationReadNextRowOnlyInRecipientTable");
			TnefStrings.stringIDs.Add(2814570743U, "ReaderInvalidOperationCannotCloseParentWhileChildOpen");
			TnefStrings.stringIDs.Add(2548454096U, "ReaderComplianceInvalidAttributeChecksum");
			TnefStrings.stringIDs.Add(2472716582U, "WriterInvalidOperationTextAfterRawData");
			TnefStrings.stringIDs.Add(2826628516U, "WriterInvalidOperationStartRowNotInRecipientTable");
			TnefStrings.stringIDs.Add(2746482960U, "CountTooLarge");
			TnefStrings.stringIDs.Add(3854182917U, "WriterInvalidOperationInvalidPropertyType");
			TnefStrings.stringIDs.Add(3999265701U, "ReaderInvalidOperationMustBeInARow");
			TnefStrings.stringIDs.Add(116892624U, "ReaderComplianceInvalidTnefVersion");
			TnefStrings.stringIDs.Add(3194581355U, "StreamDoesNotSupportSeek");
			TnefStrings.stringIDs.Add(2802240034U, "ReaderInvalidOperationMustBeAtTheBeginningOfProperty");
			TnefStrings.stringIDs.Add(2125705981U, "ReaderComplianceInvalidPropertyTypeObjectInRecipientTable");
			TnefStrings.stringIDs.Add(3713767861U, "WriterInvalidOperationUnicodeRawValueForLegacyAttribute");
			TnefStrings.stringIDs.Add(3183309601U, "ReaderComplianceInvalidPropertyValueDate");
			TnefStrings.stringIDs.Add(2193753292U, "ReaderComplianceInvalidPropertyValueCount");
			TnefStrings.stringIDs.Add(1030605270U, "ReaderInvalidOperationCannotConvertValue");
			TnefStrings.stringIDs.Add(45144793U, "ReaderComplianceInvalidAttributeLength");
			TnefStrings.stringIDs.Add(4260198087U, "ReaderComplianceTooDeepEmbedding");
			TnefStrings.stringIDs.Add(1889035737U, "ReaderInvalidOperationTextPropertyTooLong");
			TnefStrings.stringIDs.Add(585424572U, "WriterInvalidOperationNotObjectProperty");
			TnefStrings.stringIDs.Add(1579220719U, "ReaderComplianceInvalidFrom");
			TnefStrings.stringIDs.Add(340503501U, "WriterInvalidOperationInvalidValueType");
			TnefStrings.stringIDs.Add(1982026044U, "ReaderComplianceInvalidNamedPropertyNameLength");
			TnefStrings.stringIDs.Add(8456632U, "WriterInvalidOperationObjectInRecipientTable");
			TnefStrings.stringIDs.Add(138477889U, "ReaderComplianceInvalidPropertyTypeMvBoolean");
			TnefStrings.stringIDs.Add(2301033583U, "WriterNotSupportedCannotAddThisPropertyToAttributeMoreThanOnce");
			TnefStrings.stringIDs.Add(1676581361U, "ReaderComplianceInvalidPropertyCount");
			TnefStrings.stringIDs.Add(32688031U, "WriterNotSupportedNotOneOffEntryId");
			TnefStrings.stringIDs.Add(3093568444U, "ReaderInvalidOperationRawAfterProp");
			TnefStrings.stringIDs.Add(3871797491U, "ReaderComplianceInvalidPropertyLengthObject");
			TnefStrings.stringIDs.Add(354067927U, "WriterInvalidMessageCodepage");
			TnefStrings.stringIDs.Add(3347133450U, "ReaderComplianceInvalidTnefSignature");
			TnefStrings.stringIDs.Add(1984727737U, "StreamDoesNotSupportRead");
			TnefStrings.stringIDs.Add(3184266628U, "ReaderInvalidOperationNotObjectProperty");
			TnefStrings.stringIDs.Add(3282557718U, "StreamDoesNotSupportWrite");
			TnefStrings.stringIDs.Add(560728928U, "WriterNotSupportedInvalidRecipientInformation");
			TnefStrings.stringIDs.Add(1718248948U, "ReaderComplianceInvalidPropertyType");
			TnefStrings.stringIDs.Add(1590522975U, "CountOutOfRange");
			TnefStrings.stringIDs.Add(251475407U, "WriterInvalidOperationMoreThanOneValueForSingleValuedProperty");
			TnefStrings.stringIDs.Add(801076374U, "ReaderComplianceInvalidPropertyTypeError");
			TnefStrings.stringIDs.Add(2890237940U, "ReaderComplianceInvalidSchedulePlus");
			TnefStrings.stringIDs.Add(2874206193U, "WriterInvalidOperationMvObject");
			TnefStrings.stringIDs.Add(18124002U, "ReaderComplianceInvalidMessageCodepage");
			TnefStrings.stringIDs.Add(278456090U, "ReaderComplianceInvalidOemCodepageAttributeLength");
			TnefStrings.stringIDs.Add(4267063105U, "ReaderComplianceInvalidConversationId");
			TnefStrings.stringIDs.Add(1375655382U, "ReaderComplianceInvalidRowCount");
			TnefStrings.stringIDs.Add(1658289410U, "WriterInvalidOperationNotStringProperty");
			TnefStrings.stringIDs.Add(2086337489U, "ReaderInvalidOperationMustBeAtTheBeginningOfAttribute");
			TnefStrings.stringIDs.Add(924619888U, "ReaderComplianceInvalidPropertyLength");
			TnefStrings.stringIDs.Add(1481845710U, "ReaderComplianceInvalidDateOrTimeValue");
			TnefStrings.stringIDs.Add(1473863903U, "ReaderComplianceInvalidComputedPropertyLength");
			TnefStrings.stringIDs.Add(3713247674U, "WriterInvalidOperationStartNormalPropertyWithName");
			TnefStrings.stringIDs.Add(1090996482U, "ReaderInvalidOperationMustBeInAttribute");
			TnefStrings.stringIDs.Add(3553141442U, "ReaderComplianceInvalidNamedPropertyNameNotZeroTerminated");
			TnefStrings.stringIDs.Add(684398287U, "WriterInvalidOperationValueSizeInvalidForType");
			TnefStrings.stringIDs.Add(5681399U, "WriterInvalidOperation");
			TnefStrings.stringIDs.Add(2819832296U, "WriterNotSupportedCannotAddAnyPropertyToAttribute");
			TnefStrings.stringIDs.Add(1226301788U, "IndexOutOfRange");
			TnefStrings.stringIDs.Add(2425723870U, "ReaderInvalidOperationNotNamedProperty");
			TnefStrings.stringIDs.Add(82425455U, "ReaderComplianceAttributeValueOverflow");
			TnefStrings.stringIDs.Add(3129121212U, "ReaderInvalidOperationPropAfterRaw");
			TnefStrings.stringIDs.Add(3752416298U, "WriterNotSupportedCannotAddThisPropertyToAttribute");
			TnefStrings.stringIDs.Add(3721266330U, "WriterNotSupportedInvalidPropertyType");
			TnefStrings.stringIDs.Add(2410231701U, "ReaderInvalidOperationPropRawAfterText");
			TnefStrings.stringIDs.Add(151827623U, "ReaderInvalidOperationNotSeekableCannotUseRewind");
			TnefStrings.stringIDs.Add(3822473392U, "ReaderComplianceInvalidPropertyTypeMvObject");
			TnefStrings.stringIDs.Add(3882862631U, "ReaderComplianceInvalidAttributeLevel");
			TnefStrings.stringIDs.Add(373431081U, "WriterNotSupportedUnicodeOneOffEntryId");
			TnefStrings.stringIDs.Add(2522592264U, "ReaderInvalidOperationStreamOffsetForAComputedValue");
			TnefStrings.stringIDs.Add(2591161396U, "ReaderInvalidOperationRowsOnlyInRecipientTable");
			TnefStrings.stringIDs.Add(184454741U, "ReaderInvalidOperationPropTextAfterRaw");
			TnefStrings.stringIDs.Add(3432269739U, "WriterNotSupportedLegacyAttributeTooLong");
			TnefStrings.stringIDs.Add(2216356239U, "ReaderInvalidOperationPropertyRawValueTooLong");
			TnefStrings.stringIDs.Add(476021462U, "WriterNotSupportedMallformedEntryId");
			TnefStrings.stringIDs.Add(2801890310U, "WriterInvalidOperationRawDataAfterText");
			TnefStrings.stringIDs.Add(4002053520U, "ReaderInvalidOperationMustBeInPropertyValue");
			TnefStrings.stringIDs.Add(3597881315U, "ReaderInvalidOperationChildActive");
			TnefStrings.stringIDs.Add(1547596720U, "ReaderComplianceInvalidTnefVersionAttributeLength");
			TnefStrings.stringIDs.Add(1208559273U, "ReaderInvalidOperationNotEmbeddedMessage");
			TnefStrings.stringIDs.Add(585789129U, "WriterInvalidOperationStartNamedPropertyNoName");
			TnefStrings.stringIDs.Add(861262429U, "WriterInvalidOperationChildActive");
			TnefStrings.stringIDs.Add(1975716038U, "InvalidMessageCodePage");
			TnefStrings.stringIDs.Add(1236142761U, "WriterInvalidOperationNamedPropertyInLegacyAttribute");
			TnefStrings.stringIDs.Add(2236203585U, "ReaderInvalidOperationMustBeInProperty");
			TnefStrings.stringIDs.Add(3590683541U, "OffsetOutOfRange");
			TnefStrings.stringIDs.Add(2393972388U, "ReaderComplianceInvalidMessageClassLength");
			TnefStrings.stringIDs.Add(332698909U, "WriterInvalidOperationValueTooLongForType");
			TnefStrings.stringIDs.Add(1784846278U, "WriterNotSupportedInvalidMessageClass");
			TnefStrings.stringIDs.Add(690917892U, "WriterNotSupportedNotEnoughInformationForAttribute");
			TnefStrings.stringIDs.Add(2998725655U, "ReaderComplianceTnefTruncated");
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00030A9C File Offset: 0x0002EC9C
		public static string WriterPropertyNameEmptyOrTooLong
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterPropertyNameEmptyOrTooLong");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00030AAD File Offset: 0x0002ECAD
		public static string ReaderComplianceInvalidMessageClassNotZeroTerminated
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidMessageClassNotZeroTerminated");
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00030ABE File Offset: 0x0002ECBE
		public static string ReaderInvalidOperationReadNextRowOnlyInRecipientTable
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationReadNextRowOnlyInRecipientTable");
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00030ACF File Offset: 0x0002ECCF
		public static string ReaderInvalidOperationCannotCloseParentWhileChildOpen
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationCannotCloseParentWhileChildOpen");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00030AE0 File Offset: 0x0002ECE0
		public static string ReaderComplianceInvalidAttributeChecksum
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidAttributeChecksum");
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00030AF1 File Offset: 0x0002ECF1
		public static string WriterInvalidOperationTextAfterRawData
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationTextAfterRawData");
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00030B02 File Offset: 0x0002ED02
		public static string WriterInvalidOperationStartRowNotInRecipientTable
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationStartRowNotInRecipientTable");
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00030B13 File Offset: 0x0002ED13
		public static string CountTooLarge
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("CountTooLarge");
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00030B24 File Offset: 0x0002ED24
		public static string WriterInvalidOperationInvalidPropertyType
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationInvalidPropertyType");
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00030B35 File Offset: 0x0002ED35
		public static string ReaderInvalidOperationMustBeInARow
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationMustBeInARow");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00030B46 File Offset: 0x0002ED46
		public static string ReaderComplianceInvalidTnefVersion
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidTnefVersion");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00030B57 File Offset: 0x0002ED57
		public static string StreamDoesNotSupportSeek
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("StreamDoesNotSupportSeek");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00030B68 File Offset: 0x0002ED68
		public static string ReaderInvalidOperationMustBeAtTheBeginningOfProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationMustBeAtTheBeginningOfProperty");
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00030B79 File Offset: 0x0002ED79
		public static string ReaderComplianceInvalidPropertyTypeObjectInRecipientTable
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyTypeObjectInRecipientTable");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00030B8A File Offset: 0x0002ED8A
		public static string WriterInvalidOperationUnicodeRawValueForLegacyAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationUnicodeRawValueForLegacyAttribute");
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00030B9B File Offset: 0x0002ED9B
		public static string ReaderComplianceInvalidPropertyValueDate
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyValueDate");
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00030BAC File Offset: 0x0002EDAC
		public static string ReaderComplianceInvalidPropertyValueCount
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyValueCount");
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00030BBD File Offset: 0x0002EDBD
		public static string ReaderInvalidOperationCannotConvertValue
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationCannotConvertValue");
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00030BCE File Offset: 0x0002EDCE
		public static string ReaderComplianceInvalidAttributeLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidAttributeLength");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00030BDF File Offset: 0x0002EDDF
		public static string ReaderComplianceTooDeepEmbedding
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceTooDeepEmbedding");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00030BF0 File Offset: 0x0002EDF0
		public static string ReaderInvalidOperationTextPropertyTooLong
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationTextPropertyTooLong");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00030C01 File Offset: 0x0002EE01
		public static string WriterInvalidOperationNotObjectProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationNotObjectProperty");
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00030C12 File Offset: 0x0002EE12
		public static string ReaderComplianceInvalidFrom
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidFrom");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00030C23 File Offset: 0x0002EE23
		public static string WriterInvalidOperationInvalidValueType
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationInvalidValueType");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00030C34 File Offset: 0x0002EE34
		public static string ReaderComplianceInvalidNamedPropertyNameLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidNamedPropertyNameLength");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00030C45 File Offset: 0x0002EE45
		public static string WriterInvalidOperationObjectInRecipientTable
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationObjectInRecipientTable");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00030C56 File Offset: 0x0002EE56
		public static string ReaderComplianceInvalidPropertyTypeMvBoolean
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyTypeMvBoolean");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00030C67 File Offset: 0x0002EE67
		public static string WriterNotSupportedCannotAddThisPropertyToAttributeMoreThanOnce
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedCannotAddThisPropertyToAttributeMoreThanOnce");
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00030C78 File Offset: 0x0002EE78
		public static string ReaderComplianceInvalidPropertyCount
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyCount");
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00030C89 File Offset: 0x0002EE89
		public static string WriterNotSupportedNotOneOffEntryId
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedNotOneOffEntryId");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00030C9A File Offset: 0x0002EE9A
		public static string ReaderInvalidOperationRawAfterProp
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationRawAfterProp");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00030CAB File Offset: 0x0002EEAB
		public static string ReaderComplianceInvalidPropertyLengthObject
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyLengthObject");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00030CBC File Offset: 0x0002EEBC
		public static string WriterInvalidMessageCodepage
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidMessageCodepage");
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00030CCD File Offset: 0x0002EECD
		public static string ReaderComplianceInvalidTnefSignature
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidTnefSignature");
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00030CDE File Offset: 0x0002EEDE
		public static string StreamDoesNotSupportRead
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("StreamDoesNotSupportRead");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00030CEF File Offset: 0x0002EEEF
		public static string ReaderInvalidOperationNotObjectProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationNotObjectProperty");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00030D00 File Offset: 0x0002EF00
		public static string StreamDoesNotSupportWrite
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("StreamDoesNotSupportWrite");
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00030D11 File Offset: 0x0002EF11
		public static string WriterNotSupportedInvalidRecipientInformation
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedInvalidRecipientInformation");
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00030D22 File Offset: 0x0002EF22
		public static string ReaderComplianceInvalidPropertyType
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyType");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00030D33 File Offset: 0x0002EF33
		public static string CountOutOfRange
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("CountOutOfRange");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00030D44 File Offset: 0x0002EF44
		public static string WriterInvalidOperationMoreThanOneValueForSingleValuedProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationMoreThanOneValueForSingleValuedProperty");
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00030D55 File Offset: 0x0002EF55
		public static string ReaderComplianceInvalidPropertyTypeError
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyTypeError");
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00030D66 File Offset: 0x0002EF66
		public static string ReaderComplianceInvalidSchedulePlus
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidSchedulePlus");
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00030D77 File Offset: 0x0002EF77
		public static string WriterInvalidOperationMvObject
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationMvObject");
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x00030D88 File Offset: 0x0002EF88
		public static string ReaderComplianceInvalidMessageCodepage
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidMessageCodepage");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00030D99 File Offset: 0x0002EF99
		public static string ReaderComplianceInvalidOemCodepageAttributeLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidOemCodepageAttributeLength");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00030DAA File Offset: 0x0002EFAA
		public static string ReaderComplianceInvalidConversationId
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidConversationId");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00030DBB File Offset: 0x0002EFBB
		public static string ReaderComplianceInvalidRowCount
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidRowCount");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00030DCC File Offset: 0x0002EFCC
		public static string WriterInvalidOperationNotStringProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationNotStringProperty");
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00030DDD File Offset: 0x0002EFDD
		public static string ReaderInvalidOperationMustBeAtTheBeginningOfAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationMustBeAtTheBeginningOfAttribute");
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00030DEE File Offset: 0x0002EFEE
		public static string ReaderComplianceInvalidPropertyLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyLength");
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00030DFF File Offset: 0x0002EFFF
		public static string ReaderComplianceInvalidDateOrTimeValue
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidDateOrTimeValue");
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00030E10 File Offset: 0x0002F010
		public static string ReaderComplianceInvalidComputedPropertyLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidComputedPropertyLength");
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00030E21 File Offset: 0x0002F021
		public static string WriterInvalidOperationStartNormalPropertyWithName
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationStartNormalPropertyWithName");
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00030E32 File Offset: 0x0002F032
		public static string ReaderInvalidOperationMustBeInAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationMustBeInAttribute");
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00030E43 File Offset: 0x0002F043
		public static string ReaderComplianceInvalidNamedPropertyNameNotZeroTerminated
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidNamedPropertyNameNotZeroTerminated");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00030E54 File Offset: 0x0002F054
		public static string WriterInvalidOperationValueSizeInvalidForType
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationValueSizeInvalidForType");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00030E65 File Offset: 0x0002F065
		public static string WriterInvalidOperation
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperation");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00030E76 File Offset: 0x0002F076
		public static string WriterNotSupportedCannotAddAnyPropertyToAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedCannotAddAnyPropertyToAttribute");
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00030E87 File Offset: 0x0002F087
		public static string IndexOutOfRange
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("IndexOutOfRange");
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00030E98 File Offset: 0x0002F098
		public static string ReaderInvalidOperationNotNamedProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationNotNamedProperty");
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00030EA9 File Offset: 0x0002F0A9
		public static string ReaderComplianceAttributeValueOverflow
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceAttributeValueOverflow");
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00030EBA File Offset: 0x0002F0BA
		public static string ReaderInvalidOperationPropAfterRaw
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationPropAfterRaw");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00030ECB File Offset: 0x0002F0CB
		public static string WriterNotSupportedCannotAddThisPropertyToAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedCannotAddThisPropertyToAttribute");
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00030EDC File Offset: 0x0002F0DC
		public static string WriterNotSupportedInvalidPropertyType
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedInvalidPropertyType");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00030EED File Offset: 0x0002F0ED
		public static string ReaderInvalidOperationPropRawAfterText
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationPropRawAfterText");
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00030EFE File Offset: 0x0002F0FE
		public static string ReaderInvalidOperationNotSeekableCannotUseRewind
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationNotSeekableCannotUseRewind");
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00030F0F File Offset: 0x0002F10F
		public static string ReaderComplianceInvalidPropertyTypeMvObject
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidPropertyTypeMvObject");
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00030F20 File Offset: 0x0002F120
		public static string ReaderComplianceInvalidAttributeLevel
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidAttributeLevel");
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00030F31 File Offset: 0x0002F131
		public static string WriterNotSupportedUnicodeOneOffEntryId
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedUnicodeOneOffEntryId");
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00030F42 File Offset: 0x0002F142
		public static string ReaderInvalidOperationStreamOffsetForAComputedValue
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationStreamOffsetForAComputedValue");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00030F53 File Offset: 0x0002F153
		public static string ReaderInvalidOperationRowsOnlyInRecipientTable
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationRowsOnlyInRecipientTable");
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00030F64 File Offset: 0x0002F164
		public static string ReaderInvalidOperationPropTextAfterRaw
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationPropTextAfterRaw");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00030F75 File Offset: 0x0002F175
		public static string WriterNotSupportedLegacyAttributeTooLong
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedLegacyAttributeTooLong");
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00030F86 File Offset: 0x0002F186
		public static string ReaderInvalidOperationPropertyRawValueTooLong
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationPropertyRawValueTooLong");
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00030F97 File Offset: 0x0002F197
		public static string WriterNotSupportedMallformedEntryId
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedMallformedEntryId");
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00030FA8 File Offset: 0x0002F1A8
		public static string WriterInvalidOperationRawDataAfterText
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationRawDataAfterText");
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00030FB9 File Offset: 0x0002F1B9
		public static string ReaderInvalidOperationMustBeInPropertyValue
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationMustBeInPropertyValue");
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00030FCA File Offset: 0x0002F1CA
		public static string ReaderInvalidOperationChildActive
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationChildActive");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00030FDB File Offset: 0x0002F1DB
		public static string ReaderComplianceInvalidTnefVersionAttributeLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidTnefVersionAttributeLength");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00030FEC File Offset: 0x0002F1EC
		public static string ReaderInvalidOperationNotEmbeddedMessage
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationNotEmbeddedMessage");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00030FFD File Offset: 0x0002F1FD
		public static string WriterInvalidOperationStartNamedPropertyNoName
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationStartNamedPropertyNoName");
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0003100E File Offset: 0x0002F20E
		public static string WriterInvalidOperationChildActive
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationChildActive");
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0003101F File Offset: 0x0002F21F
		public static string InvalidMessageCodePage
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("InvalidMessageCodePage");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00031030 File Offset: 0x0002F230
		public static string WriterInvalidOperationNamedPropertyInLegacyAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationNamedPropertyInLegacyAttribute");
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00031041 File Offset: 0x0002F241
		public static string ReaderInvalidOperationMustBeInProperty
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderInvalidOperationMustBeInProperty");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00031052 File Offset: 0x0002F252
		public static string OffsetOutOfRange
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("OffsetOutOfRange");
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00031063 File Offset: 0x0002F263
		public static string ReaderComplianceInvalidMessageClassLength
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceInvalidMessageClassLength");
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00031074 File Offset: 0x0002F274
		public static string WriterInvalidOperationValueTooLongForType
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterInvalidOperationValueTooLongForType");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00031085 File Offset: 0x0002F285
		public static string WriterNotSupportedInvalidMessageClass
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedInvalidMessageClass");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00031096 File Offset: 0x0002F296
		public static string WriterNotSupportedNotEnoughInformationForAttribute
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("WriterNotSupportedNotEnoughInformationForAttribute");
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000310A7 File Offset: 0x0002F2A7
		public static string ReaderComplianceTnefTruncated
		{
			get
			{
				return TnefStrings.ResourceManager.GetString("ReaderComplianceTnefTruncated");
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000310B8 File Offset: 0x0002F2B8
		public static string GetLocalizedString(TnefStrings.IDs key)
		{
			return TnefStrings.ResourceManager.GetString(TnefStrings.stringIDs[(uint)key]);
		}

		// Token: 0x0400076D RID: 1901
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(92);

		// Token: 0x0400076E RID: 1902
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.TnefStrings", typeof(TnefStrings).GetTypeInfo().Assembly);

		// Token: 0x020000E0 RID: 224
		public enum IDs : uint
		{
			// Token: 0x04000770 RID: 1904
			WriterPropertyNameEmptyOrTooLong = 149645149U,
			// Token: 0x04000771 RID: 1905
			ReaderComplianceInvalidMessageClassNotZeroTerminated = 2720608910U,
			// Token: 0x04000772 RID: 1906
			ReaderInvalidOperationReadNextRowOnlyInRecipientTable = 718178560U,
			// Token: 0x04000773 RID: 1907
			ReaderInvalidOperationCannotCloseParentWhileChildOpen = 2814570743U,
			// Token: 0x04000774 RID: 1908
			ReaderComplianceInvalidAttributeChecksum = 2548454096U,
			// Token: 0x04000775 RID: 1909
			WriterInvalidOperationTextAfterRawData = 2472716582U,
			// Token: 0x04000776 RID: 1910
			WriterInvalidOperationStartRowNotInRecipientTable = 2826628516U,
			// Token: 0x04000777 RID: 1911
			CountTooLarge = 2746482960U,
			// Token: 0x04000778 RID: 1912
			WriterInvalidOperationInvalidPropertyType = 3854182917U,
			// Token: 0x04000779 RID: 1913
			ReaderInvalidOperationMustBeInARow = 3999265701U,
			// Token: 0x0400077A RID: 1914
			ReaderComplianceInvalidTnefVersion = 116892624U,
			// Token: 0x0400077B RID: 1915
			StreamDoesNotSupportSeek = 3194581355U,
			// Token: 0x0400077C RID: 1916
			ReaderInvalidOperationMustBeAtTheBeginningOfProperty = 2802240034U,
			// Token: 0x0400077D RID: 1917
			ReaderComplianceInvalidPropertyTypeObjectInRecipientTable = 2125705981U,
			// Token: 0x0400077E RID: 1918
			WriterInvalidOperationUnicodeRawValueForLegacyAttribute = 3713767861U,
			// Token: 0x0400077F RID: 1919
			ReaderComplianceInvalidPropertyValueDate = 3183309601U,
			// Token: 0x04000780 RID: 1920
			ReaderComplianceInvalidPropertyValueCount = 2193753292U,
			// Token: 0x04000781 RID: 1921
			ReaderInvalidOperationCannotConvertValue = 1030605270U,
			// Token: 0x04000782 RID: 1922
			ReaderComplianceInvalidAttributeLength = 45144793U,
			// Token: 0x04000783 RID: 1923
			ReaderComplianceTooDeepEmbedding = 4260198087U,
			// Token: 0x04000784 RID: 1924
			ReaderInvalidOperationTextPropertyTooLong = 1889035737U,
			// Token: 0x04000785 RID: 1925
			WriterInvalidOperationNotObjectProperty = 585424572U,
			// Token: 0x04000786 RID: 1926
			ReaderComplianceInvalidFrom = 1579220719U,
			// Token: 0x04000787 RID: 1927
			WriterInvalidOperationInvalidValueType = 340503501U,
			// Token: 0x04000788 RID: 1928
			ReaderComplianceInvalidNamedPropertyNameLength = 1982026044U,
			// Token: 0x04000789 RID: 1929
			WriterInvalidOperationObjectInRecipientTable = 8456632U,
			// Token: 0x0400078A RID: 1930
			ReaderComplianceInvalidPropertyTypeMvBoolean = 138477889U,
			// Token: 0x0400078B RID: 1931
			WriterNotSupportedCannotAddThisPropertyToAttributeMoreThanOnce = 2301033583U,
			// Token: 0x0400078C RID: 1932
			ReaderComplianceInvalidPropertyCount = 1676581361U,
			// Token: 0x0400078D RID: 1933
			WriterNotSupportedNotOneOffEntryId = 32688031U,
			// Token: 0x0400078E RID: 1934
			ReaderInvalidOperationRawAfterProp = 3093568444U,
			// Token: 0x0400078F RID: 1935
			ReaderComplianceInvalidPropertyLengthObject = 3871797491U,
			// Token: 0x04000790 RID: 1936
			WriterInvalidMessageCodepage = 354067927U,
			// Token: 0x04000791 RID: 1937
			ReaderComplianceInvalidTnefSignature = 3347133450U,
			// Token: 0x04000792 RID: 1938
			StreamDoesNotSupportRead = 1984727737U,
			// Token: 0x04000793 RID: 1939
			ReaderInvalidOperationNotObjectProperty = 3184266628U,
			// Token: 0x04000794 RID: 1940
			StreamDoesNotSupportWrite = 3282557718U,
			// Token: 0x04000795 RID: 1941
			WriterNotSupportedInvalidRecipientInformation = 560728928U,
			// Token: 0x04000796 RID: 1942
			ReaderComplianceInvalidPropertyType = 1718248948U,
			// Token: 0x04000797 RID: 1943
			CountOutOfRange = 1590522975U,
			// Token: 0x04000798 RID: 1944
			WriterInvalidOperationMoreThanOneValueForSingleValuedProperty = 251475407U,
			// Token: 0x04000799 RID: 1945
			ReaderComplianceInvalidPropertyTypeError = 801076374U,
			// Token: 0x0400079A RID: 1946
			ReaderComplianceInvalidSchedulePlus = 2890237940U,
			// Token: 0x0400079B RID: 1947
			WriterInvalidOperationMvObject = 2874206193U,
			// Token: 0x0400079C RID: 1948
			ReaderComplianceInvalidMessageCodepage = 18124002U,
			// Token: 0x0400079D RID: 1949
			ReaderComplianceInvalidOemCodepageAttributeLength = 278456090U,
			// Token: 0x0400079E RID: 1950
			ReaderComplianceInvalidConversationId = 4267063105U,
			// Token: 0x0400079F RID: 1951
			ReaderComplianceInvalidRowCount = 1375655382U,
			// Token: 0x040007A0 RID: 1952
			WriterInvalidOperationNotStringProperty = 1658289410U,
			// Token: 0x040007A1 RID: 1953
			ReaderInvalidOperationMustBeAtTheBeginningOfAttribute = 2086337489U,
			// Token: 0x040007A2 RID: 1954
			ReaderComplianceInvalidPropertyLength = 924619888U,
			// Token: 0x040007A3 RID: 1955
			ReaderComplianceInvalidDateOrTimeValue = 1481845710U,
			// Token: 0x040007A4 RID: 1956
			ReaderComplianceInvalidComputedPropertyLength = 1473863903U,
			// Token: 0x040007A5 RID: 1957
			WriterInvalidOperationStartNormalPropertyWithName = 3713247674U,
			// Token: 0x040007A6 RID: 1958
			ReaderInvalidOperationMustBeInAttribute = 1090996482U,
			// Token: 0x040007A7 RID: 1959
			ReaderComplianceInvalidNamedPropertyNameNotZeroTerminated = 3553141442U,
			// Token: 0x040007A8 RID: 1960
			WriterInvalidOperationValueSizeInvalidForType = 684398287U,
			// Token: 0x040007A9 RID: 1961
			WriterInvalidOperation = 5681399U,
			// Token: 0x040007AA RID: 1962
			WriterNotSupportedCannotAddAnyPropertyToAttribute = 2819832296U,
			// Token: 0x040007AB RID: 1963
			IndexOutOfRange = 1226301788U,
			// Token: 0x040007AC RID: 1964
			ReaderInvalidOperationNotNamedProperty = 2425723870U,
			// Token: 0x040007AD RID: 1965
			ReaderComplianceAttributeValueOverflow = 82425455U,
			// Token: 0x040007AE RID: 1966
			ReaderInvalidOperationPropAfterRaw = 3129121212U,
			// Token: 0x040007AF RID: 1967
			WriterNotSupportedCannotAddThisPropertyToAttribute = 3752416298U,
			// Token: 0x040007B0 RID: 1968
			WriterNotSupportedInvalidPropertyType = 3721266330U,
			// Token: 0x040007B1 RID: 1969
			ReaderInvalidOperationPropRawAfterText = 2410231701U,
			// Token: 0x040007B2 RID: 1970
			ReaderInvalidOperationNotSeekableCannotUseRewind = 151827623U,
			// Token: 0x040007B3 RID: 1971
			ReaderComplianceInvalidPropertyTypeMvObject = 3822473392U,
			// Token: 0x040007B4 RID: 1972
			ReaderComplianceInvalidAttributeLevel = 3882862631U,
			// Token: 0x040007B5 RID: 1973
			WriterNotSupportedUnicodeOneOffEntryId = 373431081U,
			// Token: 0x040007B6 RID: 1974
			ReaderInvalidOperationStreamOffsetForAComputedValue = 2522592264U,
			// Token: 0x040007B7 RID: 1975
			ReaderInvalidOperationRowsOnlyInRecipientTable = 2591161396U,
			// Token: 0x040007B8 RID: 1976
			ReaderInvalidOperationPropTextAfterRaw = 184454741U,
			// Token: 0x040007B9 RID: 1977
			WriterNotSupportedLegacyAttributeTooLong = 3432269739U,
			// Token: 0x040007BA RID: 1978
			ReaderInvalidOperationPropertyRawValueTooLong = 2216356239U,
			// Token: 0x040007BB RID: 1979
			WriterNotSupportedMallformedEntryId = 476021462U,
			// Token: 0x040007BC RID: 1980
			WriterInvalidOperationRawDataAfterText = 2801890310U,
			// Token: 0x040007BD RID: 1981
			ReaderInvalidOperationMustBeInPropertyValue = 4002053520U,
			// Token: 0x040007BE RID: 1982
			ReaderInvalidOperationChildActive = 3597881315U,
			// Token: 0x040007BF RID: 1983
			ReaderComplianceInvalidTnefVersionAttributeLength = 1547596720U,
			// Token: 0x040007C0 RID: 1984
			ReaderInvalidOperationNotEmbeddedMessage = 1208559273U,
			// Token: 0x040007C1 RID: 1985
			WriterInvalidOperationStartNamedPropertyNoName = 585789129U,
			// Token: 0x040007C2 RID: 1986
			WriterInvalidOperationChildActive = 861262429U,
			// Token: 0x040007C3 RID: 1987
			InvalidMessageCodePage = 1975716038U,
			// Token: 0x040007C4 RID: 1988
			WriterInvalidOperationNamedPropertyInLegacyAttribute = 1236142761U,
			// Token: 0x040007C5 RID: 1989
			ReaderInvalidOperationMustBeInProperty = 2236203585U,
			// Token: 0x040007C6 RID: 1990
			OffsetOutOfRange = 3590683541U,
			// Token: 0x040007C7 RID: 1991
			ReaderComplianceInvalidMessageClassLength = 2393972388U,
			// Token: 0x040007C8 RID: 1992
			WriterInvalidOperationValueTooLongForType = 332698909U,
			// Token: 0x040007C9 RID: 1993
			WriterNotSupportedInvalidMessageClass = 1784846278U,
			// Token: 0x040007CA RID: 1994
			WriterNotSupportedNotEnoughInformationForAttribute = 690917892U,
			// Token: 0x040007CB RID: 1995
			ReaderComplianceTnefTruncated = 2998725655U
		}
	}
}
