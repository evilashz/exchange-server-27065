using System;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000032 RID: 50
	internal enum EncodingType : byte
	{
		// Token: 0x040001C9 RID: 457
		Unknown,
		// Token: 0x040001CA RID: 458
		Boolean,
		// Token: 0x040001CB RID: 459
		Integer,
		// Token: 0x040001CC RID: 460
		BitString,
		// Token: 0x040001CD RID: 461
		OctetString,
		// Token: 0x040001CE RID: 462
		Null,
		// Token: 0x040001CF RID: 463
		ObjectIdentifier,
		// Token: 0x040001D0 RID: 464
		ObjectDescriptor,
		// Token: 0x040001D1 RID: 465
		External,
		// Token: 0x040001D2 RID: 466
		Real,
		// Token: 0x040001D3 RID: 467
		Enumerated,
		// Token: 0x040001D4 RID: 468
		EmbeddedPdv,
		// Token: 0x040001D5 RID: 469
		Utf8String,
		// Token: 0x040001D6 RID: 470
		RelativeObjectIdentifier,
		// Token: 0x040001D7 RID: 471
		Sequence = 16,
		// Token: 0x040001D8 RID: 472
		Set,
		// Token: 0x040001D9 RID: 473
		NumericString,
		// Token: 0x040001DA RID: 474
		PrintableString,
		// Token: 0x040001DB RID: 475
		TeletexString,
		// Token: 0x040001DC RID: 476
		VideotexString,
		// Token: 0x040001DD RID: 477
		IA5String,
		// Token: 0x040001DE RID: 478
		UtcTime,
		// Token: 0x040001DF RID: 479
		GeneralizedTime,
		// Token: 0x040001E0 RID: 480
		GraphicString,
		// Token: 0x040001E1 RID: 481
		VisibleString,
		// Token: 0x040001E2 RID: 482
		GeneralString,
		// Token: 0x040001E3 RID: 483
		UniversalString,
		// Token: 0x040001E4 RID: 484
		UnrestrictedCharacterString,
		// Token: 0x040001E5 RID: 485
		BMPString
	}
}
