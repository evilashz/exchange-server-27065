using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200018F RID: 399
	public enum ValueFormat : byte
	{
		// Token: 0x0400097D RID: 2429
		TypeShift = 3,
		// Token: 0x0400097E RID: 2430
		TypeMask = 248,
		// Token: 0x0400097F RID: 2431
		FormatModifierMask = 7,
		// Token: 0x04000980 RID: 2432
		FormatModifierShift = 0,
		// Token: 0x04000981 RID: 2433
		Null = 0,
		// Token: 0x04000982 RID: 2434
		Boolean = 8,
		// Token: 0x04000983 RID: 2435
		Int16 = 16,
		// Token: 0x04000984 RID: 2436
		Int32 = 24,
		// Token: 0x04000985 RID: 2437
		Int64 = 32,
		// Token: 0x04000986 RID: 2438
		Single = 40,
		// Token: 0x04000987 RID: 2439
		Double = 48,
		// Token: 0x04000988 RID: 2440
		DateTime = 56,
		// Token: 0x04000989 RID: 2441
		Guid = 64,
		// Token: 0x0400098A RID: 2442
		String = 72,
		// Token: 0x0400098B RID: 2443
		Binary = 80,
		// Token: 0x0400098C RID: 2444
		Reserved2 = 104,
		// Token: 0x0400098D RID: 2445
		Reserved1 = 112,
		// Token: 0x0400098E RID: 2446
		Reference = 120,
		// Token: 0x0400098F RID: 2447
		MVFlag = 128,
		// Token: 0x04000990 RID: 2448
		MVInt16 = 144,
		// Token: 0x04000991 RID: 2449
		MVInt32 = 152,
		// Token: 0x04000992 RID: 2450
		MVInt64 = 160,
		// Token: 0x04000993 RID: 2451
		MVSingle = 168,
		// Token: 0x04000994 RID: 2452
		MVDouble = 176,
		// Token: 0x04000995 RID: 2453
		MVDateTime = 184,
		// Token: 0x04000996 RID: 2454
		MVGuid = 192,
		// Token: 0x04000997 RID: 2455
		MVString = 200,
		// Token: 0x04000998 RID: 2456
		MVBinary = 208,
		// Token: 0x04000999 RID: 2457
		LengthMask = 3,
		// Token: 0x0400099A RID: 2458
		LengthZero = 0,
		// Token: 0x0400099B RID: 2459
		LengthSizeOneByte,
		// Token: 0x0400099C RID: 2460
		LengthSizeTwoBytes,
		// Token: 0x0400099D RID: 2461
		LengthSizeFourBytes,
		// Token: 0x0400099E RID: 2462
		StringEncodingMask,
		// Token: 0x0400099F RID: 2463
		StringEncodingUcs16 = 0,
		// Token: 0x040009A0 RID: 2464
		StringEncodingUtf8 = 4,
		// Token: 0x040009A1 RID: 2465
		IntegerSizeMask = 7,
		// Token: 0x040009A2 RID: 2466
		IntegerSizeZero = 0,
		// Token: 0x040009A3 RID: 2467
		IntegerSizeOneByte,
		// Token: 0x040009A4 RID: 2468
		IntegerSizeTwoBytes,
		// Token: 0x040009A5 RID: 2469
		IntegerSizeFourBytes,
		// Token: 0x040009A6 RID: 2470
		IntegerSizeEightBytes,
		// Token: 0x040009A7 RID: 2471
		BooleanFalse = 0,
		// Token: 0x040009A8 RID: 2472
		BooleanTrue,
		// Token: 0x040009A9 RID: 2473
		ReferenceZero = 4
	}
}
