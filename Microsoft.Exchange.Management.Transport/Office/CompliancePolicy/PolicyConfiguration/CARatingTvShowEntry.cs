using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200014F RID: 335
	[DataContract]
	public enum CARatingTvShowEntry
	{
		// Token: 0x04000592 RID: 1426
		[EnumMember]
		DontAllow,
		// Token: 0x04000593 RID: 1427
		[EnumMember]
		AllowAll = 1000,
		// Token: 0x04000594 RID: 1428
		[EnumMember]
		USRatingTVY = 100,
		// Token: 0x04000595 RID: 1429
		[EnumMember]
		USRatingTVY7 = 200,
		// Token: 0x04000596 RID: 1430
		[EnumMember]
		USRatingTVG = 300,
		// Token: 0x04000597 RID: 1431
		[EnumMember]
		USRatingTVPG = 400,
		// Token: 0x04000598 RID: 1432
		[EnumMember]
		USRatingTV14 = 500,
		// Token: 0x04000599 RID: 1433
		[EnumMember]
		USRatingTVMA = 600,
		// Token: 0x0400059A RID: 1434
		[EnumMember]
		AURatingP = 100,
		// Token: 0x0400059B RID: 1435
		[EnumMember]
		AURatingC = 200,
		// Token: 0x0400059C RID: 1436
		[EnumMember]
		AURatingG = 300,
		// Token: 0x0400059D RID: 1437
		[EnumMember]
		AURatingPG = 400,
		// Token: 0x0400059E RID: 1438
		[EnumMember]
		AURatingM = 500,
		// Token: 0x0400059F RID: 1439
		[EnumMember]
		AURatingMA15plus = 550,
		// Token: 0x040005A0 RID: 1440
		[EnumMember]
		AURatingAv15plus = 575,
		// Token: 0x040005A1 RID: 1441
		[EnumMember]
		CARatingC = 100,
		// Token: 0x040005A2 RID: 1442
		[EnumMember]
		CARatingC8 = 200,
		// Token: 0x040005A3 RID: 1443
		[EnumMember]
		CARatingG = 300,
		// Token: 0x040005A4 RID: 1444
		[EnumMember]
		CARatingPG = 400,
		// Token: 0x040005A5 RID: 1445
		[EnumMember]
		CARating14plus = 500,
		// Token: 0x040005A6 RID: 1446
		[EnumMember]
		CARating18plus = 600,
		// Token: 0x040005A7 RID: 1447
		[EnumMember]
		DERatingab0Jahren = 75,
		// Token: 0x040005A8 RID: 1448
		[EnumMember]
		DERatingab6Jahren = 100,
		// Token: 0x040005A9 RID: 1449
		[EnumMember]
		DERatingab12Jahren = 200,
		// Token: 0x040005AA RID: 1450
		[EnumMember]
		DERatingab16Jahren = 500,
		// Token: 0x040005AB RID: 1451
		[EnumMember]
		DERatingab18Jahren = 600,
		// Token: 0x040005AC RID: 1452
		[EnumMember]
		FRRating10minus = 100,
		// Token: 0x040005AD RID: 1453
		[EnumMember]
		FRRating12minus = 200,
		// Token: 0x040005AE RID: 1454
		[EnumMember]
		FRRating16minus = 500,
		// Token: 0x040005AF RID: 1455
		[EnumMember]
		FRRating18minus = 600,
		// Token: 0x040005B0 RID: 1456
		[EnumMember]
		IERatingGA = 100,
		// Token: 0x040005B1 RID: 1457
		[EnumMember]
		IERatingCh = 200,
		// Token: 0x040005B2 RID: 1458
		[EnumMember]
		IERatingYA = 400,
		// Token: 0x040005B3 RID: 1459
		[EnumMember]
		IERatingPS = 500,
		// Token: 0x040005B4 RID: 1460
		[EnumMember]
		IERatingMA = 600,
		// Token: 0x040005B5 RID: 1461
		[EnumMember]
		JPRatingExplicitAllowed = 500,
		// Token: 0x040005B6 RID: 1462
		[EnumMember]
		NZRatingG = 200,
		// Token: 0x040005B7 RID: 1463
		[EnumMember]
		NZRatingPGR = 400,
		// Token: 0x040005B8 RID: 1464
		[EnumMember]
		NZRatingAO = 600,
		// Token: 0x040005B9 RID: 1465
		[EnumMember]
		GBRatingCaution = 500
	}
}
