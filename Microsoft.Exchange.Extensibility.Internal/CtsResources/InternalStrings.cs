using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x0200002F RID: 47
	internal static class InternalStrings
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00009F74 File Offset: 0x00008174
		static InternalStrings()
		{
			InternalStrings.stringIDs.Add(431048246U, "SettingPositionDoesntMakeSense");
			InternalStrings.stringIDs.Add(4254659307U, "NonZeroPositionDoesntMakeSense");
			InternalStrings.stringIDs.Add(4279611434U, "CacheStreamCannotWrite");
			InternalStrings.stringIDs.Add(3780970154U, "FixedStreamCannotReadOrSeek");
			InternalStrings.stringIDs.Add(398319694U, "CacheStreamCannotReadOrSeek");
			InternalStrings.stringIDs.Add(1426955056U, "TooManyEntriesInApplefile");
			InternalStrings.stringIDs.Add(4142370976U, "WrongOffsetsInApplefile");
			InternalStrings.stringIDs.Add(882916622U, "FixedStreamCannotWrite");
			InternalStrings.stringIDs.Add(797792475U, "UnexpectedEndOfStream");
			InternalStrings.stringIDs.Add(4435227U, "CannotAccessClosedStream");
			InternalStrings.stringIDs.Add(426059850U, "MapEntryNoComma");
			InternalStrings.stringIDs.Add(1826387763U, "FixedStreamIsNull");
			InternalStrings.stringIDs.Add(158433569U, "LengthNotSupportedDuringReads");
			InternalStrings.stringIDs.Add(4281132505U, "WrongAppleMagicNumber");
			InternalStrings.stringIDs.Add(2260635443U, "NoStorageProperty");
			InternalStrings.stringIDs.Add(1686645306U, "WrongAppleVersionNumber");
			InternalStrings.stringIDs.Add(3682676966U, "MacBinWrongFilename");
			InternalStrings.stringIDs.Add(173345003U, "MergedLengthNotSupportedOnNonseekableCacheStream");
			InternalStrings.stringIDs.Add(2969036038U, "SeekingSupportedToBeginningOnly");
			InternalStrings.stringIDs.Add(2544842942U, "WrongMacBinHeader");
			InternalStrings.stringIDs.Add(182340944U, "ArgumentInvalidOffLen");
			InternalStrings.stringIDs.Add(2565209827U, "BadMapEntry");
			InternalStrings.stringIDs.Add(242522856U, "NoMapLoaded");
			InternalStrings.stringIDs.Add(3647337301U, "NoBackingStore");
			InternalStrings.stringIDs.Add(963287759U, "UnsupportedMapDataVersion");
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A1A4 File Offset: 0x000083A4
		public static string SettingPositionDoesntMakeSense
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("SettingPositionDoesntMakeSense");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A1B5 File Offset: 0x000083B5
		public static string NonZeroPositionDoesntMakeSense
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("NonZeroPositionDoesntMakeSense");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A1C6 File Offset: 0x000083C6
		public static string CacheStreamCannotWrite
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("CacheStreamCannotWrite");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A1D7 File Offset: 0x000083D7
		public static string FixedStreamCannotReadOrSeek
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("FixedStreamCannotReadOrSeek");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public static string CacheStreamCannotReadOrSeek
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("CacheStreamCannotReadOrSeek");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A1F9 File Offset: 0x000083F9
		public static string TooManyEntriesInApplefile
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("TooManyEntriesInApplefile");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A20A File Offset: 0x0000840A
		public static string WrongOffsetsInApplefile
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("WrongOffsetsInApplefile");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A21B File Offset: 0x0000841B
		public static string FixedStreamCannotWrite
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("FixedStreamCannotWrite");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A22C File Offset: 0x0000842C
		public static string UnexpectedEndOfStream
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("UnexpectedEndOfStream");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A23D File Offset: 0x0000843D
		public static string CannotAccessClosedStream
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("CannotAccessClosedStream");
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A24E File Offset: 0x0000844E
		public static string InvalidMimePart(string part)
		{
			return string.Format(InternalStrings.ResourceManager.GetString("InvalidMimePart"), part);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A265 File Offset: 0x00008465
		public static string MapEntryNoComma
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("MapEntryNoComma");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A276 File Offset: 0x00008476
		public static string FixedStreamIsNull
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("FixedStreamIsNull");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A287 File Offset: 0x00008487
		public static string LengthNotSupportedDuringReads
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("LengthNotSupportedDuringReads");
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A298 File Offset: 0x00008498
		public static string InvalidMimeMapEntry(string entry, string innerString)
		{
			return string.Format(InternalStrings.ResourceManager.GetString("InvalidMimeMapEntry"), entry, innerString);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public static string WrongAppleMagicNumber
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("WrongAppleMagicNumber");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A2C1 File Offset: 0x000084C1
		public static string NoStorageProperty
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("NoStorageProperty");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A2D2 File Offset: 0x000084D2
		public static string WrongAppleVersionNumber
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("WrongAppleVersionNumber");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A2E3 File Offset: 0x000084E3
		public static string MacBinWrongFilename
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("MacBinWrongFilename");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public static string MergedLengthNotSupportedOnNonseekableCacheStream
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("MergedLengthNotSupportedOnNonseekableCacheStream");
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A305 File Offset: 0x00008505
		public static string CorruptMapData(int checkNumber)
		{
			return string.Format(InternalStrings.ResourceManager.GetString("CorruptMapData"), checkNumber);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000A321 File Offset: 0x00008521
		public static string SeekingSupportedToBeginningOnly
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("SeekingSupportedToBeginningOnly");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A332 File Offset: 0x00008532
		public static string WrongMacBinHeader
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("WrongMacBinHeader");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A343 File Offset: 0x00008543
		public static string ArgumentInvalidOffLen
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("ArgumentInvalidOffLen");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A354 File Offset: 0x00008554
		public static string BadMapEntry
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("BadMapEntry");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A365 File Offset: 0x00008565
		public static string NoMapLoaded
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("NoMapLoaded");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A376 File Offset: 0x00008576
		public static string NoBackingStore
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("NoBackingStore");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A387 File Offset: 0x00008587
		public static string UnsupportedMapDataVersion
		{
			get
			{
				return InternalStrings.ResourceManager.GetString("UnsupportedMapDataVersion");
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A398 File Offset: 0x00008598
		public static string GetLocalizedString(InternalStrings.IDs key)
		{
			return InternalStrings.ResourceManager.GetString(InternalStrings.stringIDs[(uint)key]);
		}

		// Token: 0x040001A8 RID: 424
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(25);

		// Token: 0x040001A9 RID: 425
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.InternalStrings", typeof(InternalStrings).GetTypeInfo().Assembly);

		// Token: 0x02000030 RID: 48
		public enum IDs : uint
		{
			// Token: 0x040001AB RID: 427
			SettingPositionDoesntMakeSense = 431048246U,
			// Token: 0x040001AC RID: 428
			NonZeroPositionDoesntMakeSense = 4254659307U,
			// Token: 0x040001AD RID: 429
			CacheStreamCannotWrite = 4279611434U,
			// Token: 0x040001AE RID: 430
			FixedStreamCannotReadOrSeek = 3780970154U,
			// Token: 0x040001AF RID: 431
			CacheStreamCannotReadOrSeek = 398319694U,
			// Token: 0x040001B0 RID: 432
			TooManyEntriesInApplefile = 1426955056U,
			// Token: 0x040001B1 RID: 433
			WrongOffsetsInApplefile = 4142370976U,
			// Token: 0x040001B2 RID: 434
			FixedStreamCannotWrite = 882916622U,
			// Token: 0x040001B3 RID: 435
			UnexpectedEndOfStream = 797792475U,
			// Token: 0x040001B4 RID: 436
			CannotAccessClosedStream = 4435227U,
			// Token: 0x040001B5 RID: 437
			MapEntryNoComma = 426059850U,
			// Token: 0x040001B6 RID: 438
			FixedStreamIsNull = 1826387763U,
			// Token: 0x040001B7 RID: 439
			LengthNotSupportedDuringReads = 158433569U,
			// Token: 0x040001B8 RID: 440
			WrongAppleMagicNumber = 4281132505U,
			// Token: 0x040001B9 RID: 441
			NoStorageProperty = 2260635443U,
			// Token: 0x040001BA RID: 442
			WrongAppleVersionNumber = 1686645306U,
			// Token: 0x040001BB RID: 443
			MacBinWrongFilename = 3682676966U,
			// Token: 0x040001BC RID: 444
			MergedLengthNotSupportedOnNonseekableCacheStream = 173345003U,
			// Token: 0x040001BD RID: 445
			SeekingSupportedToBeginningOnly = 2969036038U,
			// Token: 0x040001BE RID: 446
			WrongMacBinHeader = 2544842942U,
			// Token: 0x040001BF RID: 447
			ArgumentInvalidOffLen = 182340944U,
			// Token: 0x040001C0 RID: 448
			BadMapEntry = 2565209827U,
			// Token: 0x040001C1 RID: 449
			NoMapLoaded = 242522856U,
			// Token: 0x040001C2 RID: 450
			NoBackingStore = 3647337301U,
			// Token: 0x040001C3 RID: 451
			UnsupportedMapDataVersion = 963287759U
		}

		// Token: 0x02000031 RID: 49
		private enum ParamIDs
		{
			// Token: 0x040001C5 RID: 453
			InvalidMimePart,
			// Token: 0x040001C6 RID: 454
			InvalidMimeMapEntry,
			// Token: 0x040001C7 RID: 455
			CorruptMapData
		}
	}
}
