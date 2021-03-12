using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x02000149 RID: 329
	internal static class TextConvertersStrings
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0006EE20 File Offset: 0x0006D020
		static TextConvertersStrings()
		{
			TextConvertersStrings.stringIDs.Add(1331686521U, "ConverterStreamInInconsistentStare");
			TextConvertersStrings.stringIDs.Add(62892580U, "ConverterReaderInInconsistentStare");
			TextConvertersStrings.stringIDs.Add(2496811423U, "CannotUseConverterReader");
			TextConvertersStrings.stringIDs.Add(1956525104U, "CannotReadFromSource");
			TextConvertersStrings.stringIDs.Add(1226301788U, "IndexOutOfRange");
			TextConvertersStrings.stringIDs.Add(2746482960U, "CountTooLarge");
			TextConvertersStrings.stringIDs.Add(3781059438U, "CallbackTagAlreadyDeleted");
			TextConvertersStrings.stringIDs.Add(1699401981U, "InputDocumentTooComplex");
			TextConvertersStrings.stringIDs.Add(3964025115U, "CannotWriteToDestination");
			TextConvertersStrings.stringIDs.Add(1505296452U, "ConverterWriterInInconsistentStare");
			TextConvertersStrings.stringIDs.Add(1797351840U, "TooManyIterationsToFlushConverter");
			TextConvertersStrings.stringIDs.Add(666646297U, "HtmlNestingTooDeep");
			TextConvertersStrings.stringIDs.Add(3590683541U, "OffsetOutOfRange");
			TextConvertersStrings.stringIDs.Add(1733367593U, "TooManyIterationsToProduceOutput");
			TextConvertersStrings.stringIDs.Add(1265174491U, "AttributeIdIsUnknown");
			TextConvertersStrings.stringIDs.Add(3860747840U, "PropertyNotValidForCodepageConversionMode");
			TextConvertersStrings.stringIDs.Add(1688399336U, "CallbackTagAlreadyWritten");
			TextConvertersStrings.stringIDs.Add(1083457927U, "PriorityListIncludesNonDetectableCodePage");
			TextConvertersStrings.stringIDs.Add(1046064356U, "PropertyNotValidForTextExtractionMode");
			TextConvertersStrings.stringIDs.Add(1551326176U, "CannotSetNegativelength");
			TextConvertersStrings.stringIDs.Add(1194347614U, "AccessShouldBeReadOrWrite");
			TextConvertersStrings.stringIDs.Add(2816828061U, "TagIdIsUnknown");
			TextConvertersStrings.stringIDs.Add(2864662625U, "CannotSeekBeforeBeginning");
			TextConvertersStrings.stringIDs.Add(3121351942U, "AttributeNameIsEmpty");
			TextConvertersStrings.stringIDs.Add(2465464738U, "TagNameIsEmpty");
			TextConvertersStrings.stringIDs.Add(4220967200U, "AttributeNotStarted");
			TextConvertersStrings.stringIDs.Add(787140477U, "TextReaderUnsupported");
			TextConvertersStrings.stringIDs.Add(1758514861U, "SeekUnsupported");
			TextConvertersStrings.stringIDs.Add(2757185013U, "AttributeNotInitialized");
			TextConvertersStrings.stringIDs.Add(548794797U, "BufferSizeValueRange");
			TextConvertersStrings.stringIDs.Add(1705869077U, "CannotUseConverterWriter");
			TextConvertersStrings.stringIDs.Add(1332689101U, "AttributeNotValidForThisContext");
			TextConvertersStrings.stringIDs.Add(2935817164U, "TagNotStarted");
			TextConvertersStrings.stringIDs.Add(1028484033U, "AttributeNotValidInThisState");
			TextConvertersStrings.stringIDs.Add(2521897257U, "EndTagCannotHaveAttributes");
			TextConvertersStrings.stringIDs.Add(856420434U, "InputEncodingRequired");
			TextConvertersStrings.stringIDs.Add(1578381838U, "TagTooLong");
			TextConvertersStrings.stringIDs.Add(1369500743U, "AttributeCollectionNotInitialized");
			TextConvertersStrings.stringIDs.Add(1384542861U, "WriteAfterFlush");
			TextConvertersStrings.stringIDs.Add(2566408865U, "TooManyIterationsToProcessInput");
			TextConvertersStrings.stringIDs.Add(1308081499U, "MaxCharactersCannotBeNegative");
			TextConvertersStrings.stringIDs.Add(3512722007U, "ReadUnsupported");
			TextConvertersStrings.stringIDs.Add(3007555696U, "ContextNotValidInThisState");
			TextConvertersStrings.stringIDs.Add(2995867106U, "AttributeIdInvalid");
			TextConvertersStrings.stringIDs.Add(4233942166U, "TagIdInvalid");
			TextConvertersStrings.stringIDs.Add(1590522975U, "CountOutOfRange");
			TextConvertersStrings.stringIDs.Add(3248066228U, "WriteUnsupported");
			TextConvertersStrings.stringIDs.Add(41501160U, "ParametersCannotBeChangedAfterConverterObjectIsUsed");
			TextConvertersStrings.stringIDs.Add(3015958781U, "CannotWriteWhileCopyPending");
			TextConvertersStrings.stringIDs.Add(2151871687U, "TextWriterUnsupported");
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0006F244 File Offset: 0x0006D444
		public static string ConverterStreamInInconsistentStare
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("ConverterStreamInInconsistentStare");
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0006F255 File Offset: 0x0006D455
		public static string ConverterReaderInInconsistentStare
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("ConverterReaderInInconsistentStare");
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0006F266 File Offset: 0x0006D466
		public static string CreateFileFailed(string filePath)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("CreateFileFailed"), filePath);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0006F27D File Offset: 0x0006D47D
		public static string LengthExceeded(int sum, int length)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("LengthExceeded"), sum, length);
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0006F29F File Offset: 0x0006D49F
		public static string CannotUseConverterReader
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotUseConverterReader");
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0006F2B0 File Offset: 0x0006D4B0
		public static string CannotReadFromSource
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotReadFromSource");
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0006F2C1 File Offset: 0x0006D4C1
		public static string InvalidConfigurationBoolean(int propertyId)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("InvalidConfigurationBoolean"), propertyId);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0006F2DD File Offset: 0x0006D4DD
		public static string IndexOutOfRange
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("IndexOutOfRange");
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0006F2EE File Offset: 0x0006D4EE
		public static string CountTooLarge
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CountTooLarge");
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0006F2FF File Offset: 0x0006D4FF
		public static string CallbackTagAlreadyDeleted
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CallbackTagAlreadyDeleted");
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0006F310 File Offset: 0x0006D510
		public static string InputDocumentTooComplex
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("InputDocumentTooComplex");
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0006F321 File Offset: 0x0006D521
		public static string CannotWriteToDestination
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotWriteToDestination");
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0006F332 File Offset: 0x0006D532
		public static string ConverterWriterInInconsistentStare
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("ConverterWriterInInconsistentStare");
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0006F343 File Offset: 0x0006D543
		public static string InvalidConfigurationInteger(int propertyId)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("InvalidConfigurationInteger"), propertyId);
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0006F35F File Offset: 0x0006D55F
		public static string TooManyIterationsToFlushConverter
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TooManyIterationsToFlushConverter");
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0006F370 File Offset: 0x0006D570
		public static string HtmlNestingTooDeep
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("HtmlNestingTooDeep");
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0006F381 File Offset: 0x0006D581
		public static string OffsetOutOfRange
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("OffsetOutOfRange");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0006F392 File Offset: 0x0006D592
		public static string TooManyIterationsToProduceOutput
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TooManyIterationsToProduceOutput");
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0006F3A3 File Offset: 0x0006D5A3
		public static string AttributeIdIsUnknown
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeIdIsUnknown");
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0006F3B4 File Offset: 0x0006D5B4
		public static string PropertyNotValidForCodepageConversionMode
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("PropertyNotValidForCodepageConversionMode");
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0006F3C5 File Offset: 0x0006D5C5
		public static string CallbackTagAlreadyWritten
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CallbackTagAlreadyWritten");
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0006F3D6 File Offset: 0x0006D5D6
		public static string PriorityListIncludesNonDetectableCodePage
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("PriorityListIncludesNonDetectableCodePage");
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0006F3E7 File Offset: 0x0006D5E7
		public static string CannotWriteOtherTagsInsideElement(string elementName)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("CannotWriteOtherTagsInsideElement"), elementName);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0006F3FE File Offset: 0x0006D5FE
		public static string PropertyNotValidForTextExtractionMode
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("PropertyNotValidForTextExtractionMode");
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0006F40F File Offset: 0x0006D60F
		public static string CannotSetNegativelength
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotSetNegativelength");
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0006F420 File Offset: 0x0006D620
		public static string AccessShouldBeReadOrWrite
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AccessShouldBeReadOrWrite");
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0006F431 File Offset: 0x0006D631
		public static string TagIdIsUnknown
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TagIdIsUnknown");
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0006F442 File Offset: 0x0006D642
		public static string CannotSeekBeforeBeginning
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotSeekBeforeBeginning");
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0006F453 File Offset: 0x0006D653
		public static string AttributeNameIsEmpty
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeNameIsEmpty");
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0006F464 File Offset: 0x0006D664
		public static string TagNameIsEmpty
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TagNameIsEmpty");
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0006F475 File Offset: 0x0006D675
		public static string AttributeNotStarted
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeNotStarted");
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0006F486 File Offset: 0x0006D686
		public static string TextReaderUnsupported
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TextReaderUnsupported");
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0006F497 File Offset: 0x0006D697
		public static string SeekUnsupported
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("SeekUnsupported");
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0006F4A8 File Offset: 0x0006D6A8
		public static string AttributeNotInitialized
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeNotInitialized");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0006F4B9 File Offset: 0x0006D6B9
		public static string BufferSizeValueRange
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("BufferSizeValueRange");
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0006F4CA File Offset: 0x0006D6CA
		public static string CannotUseConverterWriter
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotUseConverterWriter");
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0006F4DB File Offset: 0x0006D6DB
		public static string AttributeNotValidForThisContext
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeNotValidForThisContext");
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0006F4EC File Offset: 0x0006D6EC
		public static string TagNotStarted
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TagNotStarted");
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0006F4FD File Offset: 0x0006D6FD
		public static string AttributeNotValidInThisState
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeNotValidInThisState");
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0006F50E File Offset: 0x0006D70E
		public static string EndTagCannotHaveAttributes
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("EndTagCannotHaveAttributes");
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0006F51F File Offset: 0x0006D71F
		public static string InputEncodingRequired
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("InputEncodingRequired");
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0006F530 File Offset: 0x0006D730
		public static string TagTooLong
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TagTooLong");
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0006F541 File Offset: 0x0006D741
		public static string AttributeCollectionNotInitialized
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeCollectionNotInitialized");
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0006F552 File Offset: 0x0006D752
		public static string InvalidConfigurationStream(int propertyId)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("InvalidConfigurationStream"), propertyId);
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0006F56E File Offset: 0x0006D76E
		public static string WriteAfterFlush
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("WriteAfterFlush");
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0006F57F File Offset: 0x0006D77F
		public static string TooManyIterationsToProcessInput
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TooManyIterationsToProcessInput");
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0006F590 File Offset: 0x0006D790
		public static string MaxCharactersCannotBeNegative
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("MaxCharactersCannotBeNegative");
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0006F5A1 File Offset: 0x0006D7A1
		public static string ReadUnsupported
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("ReadUnsupported");
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0006F5B2 File Offset: 0x0006D7B2
		public static string ContextNotValidInThisState
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("ContextNotValidInThisState");
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0006F5C3 File Offset: 0x0006D7C3
		public static string AttributeIdInvalid
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("AttributeIdInvalid");
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0006F5D4 File Offset: 0x0006D7D4
		public static string TagIdInvalid
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TagIdInvalid");
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0006F5E5 File Offset: 0x0006D7E5
		public static string CountOutOfRange
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CountOutOfRange");
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0006F5F6 File Offset: 0x0006D7F6
		public static string WriteUnsupported
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("WriteUnsupported");
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0006F607 File Offset: 0x0006D807
		public static string ParametersCannotBeChangedAfterConverterObjectIsUsed
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("ParametersCannotBeChangedAfterConverterObjectIsUsed");
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0006F618 File Offset: 0x0006D818
		public static string CannotWriteWhileCopyPending
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("CannotWriteWhileCopyPending");
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0006F629 File Offset: 0x0006D829
		public static string TextWriterUnsupported
		{
			get
			{
				return TextConvertersStrings.ResourceManager.GetString("TextWriterUnsupported");
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0006F63A File Offset: 0x0006D83A
		public static string DocumentGrowingExcessively(int ratio)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("DocumentGrowingExcessively"), ratio);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0006F656 File Offset: 0x0006D856
		public static string InvalidCodePage(int codePage)
		{
			return string.Format(TextConvertersStrings.ResourceManager.GetString("InvalidCodePage"), codePage);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0006F672 File Offset: 0x0006D872
		public static string GetLocalizedString(TextConvertersStrings.IDs key)
		{
			return TextConvertersStrings.ResourceManager.GetString(TextConvertersStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000F21 RID: 3873
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(50);

		// Token: 0x04000F22 RID: 3874
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.TextConvertersStrings", typeof(TextConvertersStrings).GetTypeInfo().Assembly);

		// Token: 0x0200014A RID: 330
		public enum IDs : uint
		{
			// Token: 0x04000F24 RID: 3876
			ConverterStreamInInconsistentStare = 1331686521U,
			// Token: 0x04000F25 RID: 3877
			ConverterReaderInInconsistentStare = 62892580U,
			// Token: 0x04000F26 RID: 3878
			CannotUseConverterReader = 2496811423U,
			// Token: 0x04000F27 RID: 3879
			CannotReadFromSource = 1956525104U,
			// Token: 0x04000F28 RID: 3880
			IndexOutOfRange = 1226301788U,
			// Token: 0x04000F29 RID: 3881
			CountTooLarge = 2746482960U,
			// Token: 0x04000F2A RID: 3882
			CallbackTagAlreadyDeleted = 3781059438U,
			// Token: 0x04000F2B RID: 3883
			InputDocumentTooComplex = 1699401981U,
			// Token: 0x04000F2C RID: 3884
			CannotWriteToDestination = 3964025115U,
			// Token: 0x04000F2D RID: 3885
			ConverterWriterInInconsistentStare = 1505296452U,
			// Token: 0x04000F2E RID: 3886
			TooManyIterationsToFlushConverter = 1797351840U,
			// Token: 0x04000F2F RID: 3887
			HtmlNestingTooDeep = 666646297U,
			// Token: 0x04000F30 RID: 3888
			OffsetOutOfRange = 3590683541U,
			// Token: 0x04000F31 RID: 3889
			TooManyIterationsToProduceOutput = 1733367593U,
			// Token: 0x04000F32 RID: 3890
			AttributeIdIsUnknown = 1265174491U,
			// Token: 0x04000F33 RID: 3891
			PropertyNotValidForCodepageConversionMode = 3860747840U,
			// Token: 0x04000F34 RID: 3892
			CallbackTagAlreadyWritten = 1688399336U,
			// Token: 0x04000F35 RID: 3893
			PriorityListIncludesNonDetectableCodePage = 1083457927U,
			// Token: 0x04000F36 RID: 3894
			PropertyNotValidForTextExtractionMode = 1046064356U,
			// Token: 0x04000F37 RID: 3895
			CannotSetNegativelength = 1551326176U,
			// Token: 0x04000F38 RID: 3896
			AccessShouldBeReadOrWrite = 1194347614U,
			// Token: 0x04000F39 RID: 3897
			TagIdIsUnknown = 2816828061U,
			// Token: 0x04000F3A RID: 3898
			CannotSeekBeforeBeginning = 2864662625U,
			// Token: 0x04000F3B RID: 3899
			AttributeNameIsEmpty = 3121351942U,
			// Token: 0x04000F3C RID: 3900
			TagNameIsEmpty = 2465464738U,
			// Token: 0x04000F3D RID: 3901
			AttributeNotStarted = 4220967200U,
			// Token: 0x04000F3E RID: 3902
			TextReaderUnsupported = 787140477U,
			// Token: 0x04000F3F RID: 3903
			SeekUnsupported = 1758514861U,
			// Token: 0x04000F40 RID: 3904
			AttributeNotInitialized = 2757185013U,
			// Token: 0x04000F41 RID: 3905
			BufferSizeValueRange = 548794797U,
			// Token: 0x04000F42 RID: 3906
			CannotUseConverterWriter = 1705869077U,
			// Token: 0x04000F43 RID: 3907
			AttributeNotValidForThisContext = 1332689101U,
			// Token: 0x04000F44 RID: 3908
			TagNotStarted = 2935817164U,
			// Token: 0x04000F45 RID: 3909
			AttributeNotValidInThisState = 1028484033U,
			// Token: 0x04000F46 RID: 3910
			EndTagCannotHaveAttributes = 2521897257U,
			// Token: 0x04000F47 RID: 3911
			InputEncodingRequired = 856420434U,
			// Token: 0x04000F48 RID: 3912
			TagTooLong = 1578381838U,
			// Token: 0x04000F49 RID: 3913
			AttributeCollectionNotInitialized = 1369500743U,
			// Token: 0x04000F4A RID: 3914
			WriteAfterFlush = 1384542861U,
			// Token: 0x04000F4B RID: 3915
			TooManyIterationsToProcessInput = 2566408865U,
			// Token: 0x04000F4C RID: 3916
			MaxCharactersCannotBeNegative = 1308081499U,
			// Token: 0x04000F4D RID: 3917
			ReadUnsupported = 3512722007U,
			// Token: 0x04000F4E RID: 3918
			ContextNotValidInThisState = 3007555696U,
			// Token: 0x04000F4F RID: 3919
			AttributeIdInvalid = 2995867106U,
			// Token: 0x04000F50 RID: 3920
			TagIdInvalid = 4233942166U,
			// Token: 0x04000F51 RID: 3921
			CountOutOfRange = 1590522975U,
			// Token: 0x04000F52 RID: 3922
			WriteUnsupported = 3248066228U,
			// Token: 0x04000F53 RID: 3923
			ParametersCannotBeChangedAfterConverterObjectIsUsed = 41501160U,
			// Token: 0x04000F54 RID: 3924
			CannotWriteWhileCopyPending = 3015958781U,
			// Token: 0x04000F55 RID: 3925
			TextWriterUnsupported = 2151871687U
		}

		// Token: 0x0200014B RID: 331
		private enum ParamIDs
		{
			// Token: 0x04000F57 RID: 3927
			CreateFileFailed,
			// Token: 0x04000F58 RID: 3928
			LengthExceeded,
			// Token: 0x04000F59 RID: 3929
			InvalidConfigurationBoolean,
			// Token: 0x04000F5A RID: 3930
			InvalidConfigurationInteger,
			// Token: 0x04000F5B RID: 3931
			CannotWriteOtherTagsInsideElement,
			// Token: 0x04000F5C RID: 3932
			InvalidConfigurationStream,
			// Token: 0x04000F5D RID: 3933
			DocumentGrowingExcessively,
			// Token: 0x04000F5E RID: 3934
			InvalidCodePage
		}
	}
}
