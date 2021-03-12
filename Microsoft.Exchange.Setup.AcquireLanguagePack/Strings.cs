using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000025 RID: 37
	internal static class Strings
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000049E8 File Offset: 0x00002BE8
		static Strings()
		{
			Strings.stringIDs.Add(2736429093U, "VerifyingUpdates");
			Strings.stringIDs.Add(168100618U, "BaseXMLFileNotLoaded");
			Strings.stringIDs.Add(1799593368U, "VerifyingMsps");
			Strings.stringIDs.Add(1395665443U, "InvalidLanguageBundle");
			Strings.stringIDs.Add(2925052803U, "ErrorInLog");
			Strings.stringIDs.Add(686568928U, "NotEnoughDiskSpace");
			Strings.stringIDs.Add(2061069263U, "SignatureFailed");
			Strings.stringIDs.Add(871634395U, "UnableToFindBuildVersion");
			Strings.stringIDs.Add(3339504604U, "VerifyingLangPackBundle");
			Strings.stringIDs.Add(509984218U, "CheckForAvailableSpace");
			Strings.stringIDs.Add(3065873541U, "IncompatibleBundle");
			Strings.stringIDs.Add(3541939918U, "ErrorCreatingRegKey");
			Strings.stringIDs.Add(4285450436U, "UnableToDownload");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004B28 File Offset: 0x00002D28
		public static LocalizedString fWLinkNotFound(string exVersion, string pathToLPVersioning)
		{
			return new LocalizedString("fWLinkNotFound", "ExA75EC1", false, true, Strings.ResourceManager, new object[]
			{
				exVersion,
				pathToLPVersioning
			});
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004B5C File Offset: 0x00002D5C
		public static LocalizedString URLCantBeReached(string URL)
		{
			return new LocalizedString("URLCantBeReached", "ExE32C68", false, true, Strings.ResourceManager, new object[]
			{
				URL
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004B8B File Offset: 0x00002D8B
		public static LocalizedString VerifyingUpdates
		{
			get
			{
				return new LocalizedString("VerifyingUpdates", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004BAC File Offset: 0x00002DAC
		public static LocalizedString NotExist(string name)
		{
			return new LocalizedString("NotExist", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004BDC File Offset: 0x00002DDC
		public static LocalizedString WrongFileType(string name)
		{
			return new LocalizedString("WrongFileType", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004C0C File Offset: 0x00002E0C
		public static LocalizedString InvalidFieldDataSize(uint field)
		{
			return new LocalizedString("InvalidFieldDataSize", "", false, false, Strings.ResourceManager, new object[]
			{
				field
			});
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004C40 File Offset: 0x00002E40
		public static LocalizedString BaseXMLFileNotLoaded
		{
			get
			{
				return new LocalizedString("BaseXMLFileNotLoaded", "ExBA9350", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004C60 File Offset: 0x00002E60
		public static LocalizedString LPVersioningExtractionFailed(string pathToBundle)
		{
			return new LocalizedString("LPVersioningExtractionFailed", "Ex586BC5", false, true, Strings.ResourceManager, new object[]
			{
				pathToBundle
			});
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004C8F File Offset: 0x00002E8F
		public static LocalizedString VerifyingMsps
		{
			get
			{
				return new LocalizedString("VerifyingMsps", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004CAD File Offset: 0x00002EAD
		public static LocalizedString InvalidLanguageBundle
		{
			get
			{
				return new LocalizedString("InvalidLanguageBundle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004CCB File Offset: 0x00002ECB
		public static LocalizedString ErrorInLog
		{
			get
			{
				return new LocalizedString("ErrorInLog", "Ex37776C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004CEC File Offset: 0x00002EEC
		public static LocalizedString SignatureVerificationFailed(string pathToFile)
		{
			return new LocalizedString("SignatureVerificationFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				pathToFile
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004D1C File Offset: 0x00002F1C
		public static LocalizedString IsNullOrEmpty(string name)
		{
			return new LocalizedString("IsNullOrEmpty", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static LocalizedString MspValidationFailedOn(string name)
		{
			return new LocalizedString("MspValidationFailedOn", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004D7B File Offset: 0x00002F7B
		public static LocalizedString NotEnoughDiskSpace
		{
			get
			{
				return new LocalizedString("NotEnoughDiskSpace", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004D9C File Offset: 0x00002F9C
		public static LocalizedString SignatureFailed1(string path)
		{
			return new LocalizedString("SignatureFailed1", "ExEAD8BF", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004DCC File Offset: 0x00002FCC
		public static LocalizedString InvalidSaveToPath(string saveToPath)
		{
			return new LocalizedString("InvalidSaveToPath", "Ex48B774", false, true, Strings.ResourceManager, new object[]
			{
				saveToPath
			});
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004DFB File Offset: 0x00002FFB
		public static LocalizedString SignatureFailed
		{
			get
			{
				return new LocalizedString("SignatureFailed", "ExE5C374", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004E19 File Offset: 0x00003019
		public static LocalizedString UnableToFindBuildVersion
		{
			get
			{
				return new LocalizedString("UnableToFindBuildVersion", "ExB342F6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004E38 File Offset: 0x00003038
		public static LocalizedString UnableToFindBuildVersion1(string xmlPath)
		{
			return new LocalizedString("UnableToFindBuildVersion1", "Ex0A6FF5", false, true, Strings.ResourceManager, new object[]
			{
				xmlPath
			});
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004E67 File Offset: 0x00003067
		public static LocalizedString VerifyingLangPackBundle
		{
			get
			{
				return new LocalizedString("VerifyingLangPackBundle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004E88 File Offset: 0x00003088
		public static LocalizedString InsufficientDiskSpace(double requiredSize)
		{
			return new LocalizedString("InsufficientDiskSpace", "ExA1C4F0", false, true, Strings.ResourceManager, new object[]
			{
				requiredSize
			});
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004EBC File Offset: 0x000030BC
		public static LocalizedString CheckForAvailableSpace
		{
			get
			{
				return new LocalizedString("CheckForAvailableSpace", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004EDC File Offset: 0x000030DC
		public static LocalizedString InvalidFile(string fileName)
		{
			return new LocalizedString("InvalidFile", "", false, false, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004F0B File Offset: 0x0000310B
		public static LocalizedString IncompatibleBundle
		{
			get
			{
				return new LocalizedString("IncompatibleBundle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004F29 File Offset: 0x00003129
		public static LocalizedString ErrorCreatingRegKey
		{
			get
			{
				return new LocalizedString("ErrorCreatingRegKey", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004F47 File Offset: 0x00003147
		public static LocalizedString UnableToDownload
		{
			get
			{
				return new LocalizedString("UnableToDownload", "Ex86FA31", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004F65 File Offset: 0x00003165
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000065 RID: 101
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(13);

		// Token: 0x04000066 RID: 102
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.AcquireLanguagePack.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000026 RID: 38
		public enum IDs : uint
		{
			// Token: 0x04000068 RID: 104
			VerifyingUpdates = 2736429093U,
			// Token: 0x04000069 RID: 105
			BaseXMLFileNotLoaded = 168100618U,
			// Token: 0x0400006A RID: 106
			VerifyingMsps = 1799593368U,
			// Token: 0x0400006B RID: 107
			InvalidLanguageBundle = 1395665443U,
			// Token: 0x0400006C RID: 108
			ErrorInLog = 2925052803U,
			// Token: 0x0400006D RID: 109
			NotEnoughDiskSpace = 686568928U,
			// Token: 0x0400006E RID: 110
			SignatureFailed = 2061069263U,
			// Token: 0x0400006F RID: 111
			UnableToFindBuildVersion = 871634395U,
			// Token: 0x04000070 RID: 112
			VerifyingLangPackBundle = 3339504604U,
			// Token: 0x04000071 RID: 113
			CheckForAvailableSpace = 509984218U,
			// Token: 0x04000072 RID: 114
			IncompatibleBundle = 3065873541U,
			// Token: 0x04000073 RID: 115
			ErrorCreatingRegKey = 3541939918U,
			// Token: 0x04000074 RID: 116
			UnableToDownload = 4285450436U
		}

		// Token: 0x02000027 RID: 39
		private enum ParamIDs
		{
			// Token: 0x04000076 RID: 118
			fWLinkNotFound,
			// Token: 0x04000077 RID: 119
			URLCantBeReached,
			// Token: 0x04000078 RID: 120
			NotExist,
			// Token: 0x04000079 RID: 121
			WrongFileType,
			// Token: 0x0400007A RID: 122
			InvalidFieldDataSize,
			// Token: 0x0400007B RID: 123
			LPVersioningExtractionFailed,
			// Token: 0x0400007C RID: 124
			SignatureVerificationFailed,
			// Token: 0x0400007D RID: 125
			IsNullOrEmpty,
			// Token: 0x0400007E RID: 126
			MspValidationFailedOn,
			// Token: 0x0400007F RID: 127
			SignatureFailed1,
			// Token: 0x04000080 RID: 128
			InvalidSaveToPath,
			// Token: 0x04000081 RID: 129
			UnableToFindBuildVersion1,
			// Token: 0x04000082 RID: 130
			InsufficientDiskSpace,
			// Token: 0x04000083 RID: 131
			InvalidFile
		}
	}
}
