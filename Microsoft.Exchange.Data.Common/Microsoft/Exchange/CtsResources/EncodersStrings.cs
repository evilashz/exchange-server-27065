using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x02000087 RID: 135
	internal static class EncodersStrings
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0001F06C File Offset: 0x0001D26C
		static EncodersStrings()
		{
			EncodersStrings.stringIDs.Add(3966390553U, "MacBinFileNameTooLong");
			EncodersStrings.stringIDs.Add(25286151U, "BinHexEncoderInternalError");
			EncodersStrings.stringIDs.Add(217720121U, "BinHexHeaderBadFileNameLength");
			EncodersStrings.stringIDs.Add(1288990070U, "EncStrCannotCloneWriteableStream");
			EncodersStrings.stringIDs.Add(812742886U, "BinHexDecoderLineTooLong");
			EncodersStrings.stringIDs.Add(810486593U, "BinHexDecoderInternalError");
			EncodersStrings.stringIDs.Add(2498997775U, "BinHexDecoderDataCorrupt");
			EncodersStrings.stringIDs.Add(2916696602U, "BinHexDecoderFoundInvalidCharacter");
			EncodersStrings.stringIDs.Add(804376101U, "BinHexHeaderInvalidNameLength");
			EncodersStrings.stringIDs.Add(2503051247U, "MacBinBadVersion");
			EncodersStrings.stringIDs.Add(2749850901U, "BinHexHeaderIncomplete");
			EncodersStrings.stringIDs.Add(450079973U, "BinHexDecoderFileNameTooLong");
			EncodersStrings.stringIDs.Add(2959384831U, "UUDecoderInvalidData");
			EncodersStrings.stringIDs.Add(2345827606U, "EncStrCannotRead");
			EncodersStrings.stringIDs.Add(4253113056U, "BinHexHeaderUnsupportedVersion");
			EncodersStrings.stringIDs.Add(2009660788U, "UUDecoderInvalidDataBadLine");
			EncodersStrings.stringIDs.Add(1330362610U, "BinHexHeaderInvalidCrc");
			EncodersStrings.stringIDs.Add(2479018069U, "BinHexDecoderFirstNonWhitespaceMustBeColon");
			EncodersStrings.stringIDs.Add(3717726462U, "MacBinHeaderMustBe128Long");
			EncodersStrings.stringIDs.Add(67282974U, "EncStrCannotSeek");
			EncodersStrings.stringIDs.Add(3114903713U, "BinHexEncoderDoesNotSupportResourceFork");
			EncodersStrings.stringIDs.Add(3183910392U, "BinHexEncoderDataCorruptCannotFinishEncoding");
			EncodersStrings.stringIDs.Add(2984114443U, "MacBinInvalidData");
			EncodersStrings.stringIDs.Add(2549049936U, "BinHexHeaderTooSmall");
			EncodersStrings.stringIDs.Add(1639523968U, "QPEncoderNoSpaceForLineBreak");
			EncodersStrings.stringIDs.Add(2739587701U, "BinHexDecoderBadResourceForkCrc");
			EncodersStrings.stringIDs.Add(62154753U, "BinHexDecoderLineCorrupt");
			EncodersStrings.stringIDs.Add(2823726815U, "BinHexDecoderBadCrc");
			EncodersStrings.stringIDs.Add(1415490657U, "EncStrCannotWrite");
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		public static string MacBinFileNameTooLong
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("MacBinFileNameTooLong");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001F2FD File Offset: 0x0001D4FD
		public static string BinHexEncoderInternalError
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexEncoderInternalError");
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001F30E File Offset: 0x0001D50E
		public static string EncStrLengthExceeded(int sum, int length)
		{
			return string.Format(EncodersStrings.ResourceManager.GetString("EncStrLengthExceeded"), sum, length);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001F330 File Offset: 0x0001D530
		public static string BinHexHeaderBadFileNameLength
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexHeaderBadFileNameLength");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001F341 File Offset: 0x0001D541
		public static string EncStrCannotCloneWriteableStream
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("EncStrCannotCloneWriteableStream");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001F352 File Offset: 0x0001D552
		public static string BinHexDecoderLineTooLong
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderLineTooLong");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001F363 File Offset: 0x0001D563
		public static string BinHexDecoderInternalError
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderInternalError");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001F374 File Offset: 0x0001D574
		public static string BinHexDecoderDataCorrupt
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderDataCorrupt");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001F385 File Offset: 0x0001D585
		public static string BinHexDecoderFoundInvalidCharacter
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderFoundInvalidCharacter");
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001F396 File Offset: 0x0001D596
		public static string BinHexHeaderInvalidNameLength
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexHeaderInvalidNameLength");
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001F3A7 File Offset: 0x0001D5A7
		public static string ThisEncoderDoesNotSupportCloning(string type)
		{
			return string.Format(EncodersStrings.ResourceManager.GetString("ThisEncoderDoesNotSupportCloning"), type);
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001F3BE File Offset: 0x0001D5BE
		public static string MacBinBadVersion
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("MacBinBadVersion");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001F3CF File Offset: 0x0001D5CF
		public static string BinHexHeaderIncomplete
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexHeaderIncomplete");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		public static string BinHexDecoderFileNameTooLong
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderFileNameTooLong");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001F3F1 File Offset: 0x0001D5F1
		public static string UUDecoderInvalidData
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("UUDecoderInvalidData");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001F402 File Offset: 0x0001D602
		public static string EncStrCannotRead
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("EncStrCannotRead");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001F413 File Offset: 0x0001D613
		public static string BinHexHeaderUnsupportedVersion
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexHeaderUnsupportedVersion");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001F424 File Offset: 0x0001D624
		public static string UUDecoderInvalidDataBadLine
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("UUDecoderInvalidDataBadLine");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001F435 File Offset: 0x0001D635
		public static string BinHexHeaderInvalidCrc
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexHeaderInvalidCrc");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001F446 File Offset: 0x0001D646
		public static string BinHexDecoderFirstNonWhitespaceMustBeColon
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderFirstNonWhitespaceMustBeColon");
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001F457 File Offset: 0x0001D657
		public static string UUEncoderFileNameTooLong(int maxChars)
		{
			return string.Format(EncodersStrings.ResourceManager.GetString("UUEncoderFileNameTooLong"), maxChars);
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001F473 File Offset: 0x0001D673
		public static string MacBinHeaderMustBe128Long
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("MacBinHeaderMustBe128Long");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001F484 File Offset: 0x0001D684
		public static string EncStrCannotSeek
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("EncStrCannotSeek");
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001F495 File Offset: 0x0001D695
		public static string EncStrCannotCloneChildStream(string className)
		{
			return string.Format(EncodersStrings.ResourceManager.GetString("EncStrCannotCloneChildStream"), className);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public static string BinHexEncoderDoesNotSupportResourceFork
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexEncoderDoesNotSupportResourceFork");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001F4BD File Offset: 0x0001D6BD
		public static string BinHexEncoderDataCorruptCannotFinishEncoding
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexEncoderDataCorruptCannotFinishEncoding");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001F4CE File Offset: 0x0001D6CE
		public static string MacBinInvalidData
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("MacBinInvalidData");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001F4DF File Offset: 0x0001D6DF
		public static string BinHexHeaderTooSmall
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexHeaderTooSmall");
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		public static string MacBinIconOffsetTooLarge(int max)
		{
			return string.Format(EncodersStrings.ResourceManager.GetString("MacBinIconOffsetTooLarge"), max);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001F50C File Offset: 0x0001D70C
		public static string QPEncoderNoSpaceForLineBreak
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("QPEncoderNoSpaceForLineBreak");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001F51D File Offset: 0x0001D71D
		public static string BinHexDecoderBadResourceForkCrc
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderBadResourceForkCrc");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001F52E File Offset: 0x0001D72E
		public static string BinHexDecoderLineCorrupt
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderLineCorrupt");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001F53F File Offset: 0x0001D73F
		public static string BinHexDecoderBadCrc
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("BinHexDecoderBadCrc");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001F550 File Offset: 0x0001D750
		public static string EncStrCannotWrite
		{
			get
			{
				return EncodersStrings.ResourceManager.GetString("EncStrCannotWrite");
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001F561 File Offset: 0x0001D761
		public static string GetLocalizedString(EncodersStrings.IDs key)
		{
			return EncodersStrings.ResourceManager.GetString(EncodersStrings.stringIDs[(uint)key]);
		}

		// Token: 0x040003EC RID: 1004
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(29);

		// Token: 0x040003ED RID: 1005
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.EncodersStrings", typeof(EncodersStrings).GetTypeInfo().Assembly);

		// Token: 0x02000088 RID: 136
		public enum IDs : uint
		{
			// Token: 0x040003EF RID: 1007
			MacBinFileNameTooLong = 3966390553U,
			// Token: 0x040003F0 RID: 1008
			BinHexEncoderInternalError = 25286151U,
			// Token: 0x040003F1 RID: 1009
			BinHexHeaderBadFileNameLength = 217720121U,
			// Token: 0x040003F2 RID: 1010
			EncStrCannotCloneWriteableStream = 1288990070U,
			// Token: 0x040003F3 RID: 1011
			BinHexDecoderLineTooLong = 812742886U,
			// Token: 0x040003F4 RID: 1012
			BinHexDecoderInternalError = 810486593U,
			// Token: 0x040003F5 RID: 1013
			BinHexDecoderDataCorrupt = 2498997775U,
			// Token: 0x040003F6 RID: 1014
			BinHexDecoderFoundInvalidCharacter = 2916696602U,
			// Token: 0x040003F7 RID: 1015
			BinHexHeaderInvalidNameLength = 804376101U,
			// Token: 0x040003F8 RID: 1016
			MacBinBadVersion = 2503051247U,
			// Token: 0x040003F9 RID: 1017
			BinHexHeaderIncomplete = 2749850901U,
			// Token: 0x040003FA RID: 1018
			BinHexDecoderFileNameTooLong = 450079973U,
			// Token: 0x040003FB RID: 1019
			UUDecoderInvalidData = 2959384831U,
			// Token: 0x040003FC RID: 1020
			EncStrCannotRead = 2345827606U,
			// Token: 0x040003FD RID: 1021
			BinHexHeaderUnsupportedVersion = 4253113056U,
			// Token: 0x040003FE RID: 1022
			UUDecoderInvalidDataBadLine = 2009660788U,
			// Token: 0x040003FF RID: 1023
			BinHexHeaderInvalidCrc = 1330362610U,
			// Token: 0x04000400 RID: 1024
			BinHexDecoderFirstNonWhitespaceMustBeColon = 2479018069U,
			// Token: 0x04000401 RID: 1025
			MacBinHeaderMustBe128Long = 3717726462U,
			// Token: 0x04000402 RID: 1026
			EncStrCannotSeek = 67282974U,
			// Token: 0x04000403 RID: 1027
			BinHexEncoderDoesNotSupportResourceFork = 3114903713U,
			// Token: 0x04000404 RID: 1028
			BinHexEncoderDataCorruptCannotFinishEncoding = 3183910392U,
			// Token: 0x04000405 RID: 1029
			MacBinInvalidData = 2984114443U,
			// Token: 0x04000406 RID: 1030
			BinHexHeaderTooSmall = 2549049936U,
			// Token: 0x04000407 RID: 1031
			QPEncoderNoSpaceForLineBreak = 1639523968U,
			// Token: 0x04000408 RID: 1032
			BinHexDecoderBadResourceForkCrc = 2739587701U,
			// Token: 0x04000409 RID: 1033
			BinHexDecoderLineCorrupt = 62154753U,
			// Token: 0x0400040A RID: 1034
			BinHexDecoderBadCrc = 2823726815U,
			// Token: 0x0400040B RID: 1035
			EncStrCannotWrite = 1415490657U
		}

		// Token: 0x02000089 RID: 137
		private enum ParamIDs
		{
			// Token: 0x0400040D RID: 1037
			EncStrLengthExceeded,
			// Token: 0x0400040E RID: 1038
			ThisEncoderDoesNotSupportCloning,
			// Token: 0x0400040F RID: 1039
			UUEncoderFileNameTooLong,
			// Token: 0x04000410 RID: 1040
			EncStrCannotCloneChildStream,
			// Token: 0x04000411 RID: 1041
			MacBinIconOffsetTooLarge
		}
	}
}
