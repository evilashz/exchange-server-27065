using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000147 RID: 327
	[DataContract]
	public enum RatingMovieEntry
	{
		// Token: 0x040004E1 RID: 1249
		[EnumMember]
		DontAllow,
		// Token: 0x040004E2 RID: 1250
		[EnumMember]
		AllowAll = 1000,
		// Token: 0x040004E3 RID: 1251
		[EnumMember]
		USRatingG = 100,
		// Token: 0x040004E4 RID: 1252
		[EnumMember]
		USRatingPG = 200,
		// Token: 0x040004E5 RID: 1253
		[EnumMember]
		USRatingPG13 = 300,
		// Token: 0x040004E6 RID: 1254
		[EnumMember]
		USRatingR = 400,
		// Token: 0x040004E7 RID: 1255
		[EnumMember]
		USRatingNC17 = 500,
		// Token: 0x040004E8 RID: 1256
		[EnumMember]
		AURatingG = 100,
		// Token: 0x040004E9 RID: 1257
		[EnumMember]
		AURatingPG = 200,
		// Token: 0x040004EA RID: 1258
		[EnumMember]
		AURatingM = 350,
		// Token: 0x040004EB RID: 1259
		[EnumMember]
		AURatingMA15plus = 375,
		// Token: 0x040004EC RID: 1260
		[EnumMember]
		AURatingR18plus = 400,
		// Token: 0x040004ED RID: 1261
		[EnumMember]
		CARatingG = 100,
		// Token: 0x040004EE RID: 1262
		[EnumMember]
		CARatingPG = 200,
		// Token: 0x040004EF RID: 1263
		[EnumMember]
		CARating14A = 325,
		// Token: 0x040004F0 RID: 1264
		[EnumMember]
		CARating18A = 400,
		// Token: 0x040004F1 RID: 1265
		[EnumMember]
		CARatingR = 500,
		// Token: 0x040004F2 RID: 1266
		[EnumMember]
		DERatingab0Jahren = 75,
		// Token: 0x040004F3 RID: 1267
		[EnumMember]
		DERatingab6Jahren = 100,
		// Token: 0x040004F4 RID: 1268
		[EnumMember]
		DERatingab12Jahren = 200,
		// Token: 0x040004F5 RID: 1269
		[EnumMember]
		DERatingab16Jahren = 500,
		// Token: 0x040004F6 RID: 1270
		[EnumMember]
		DERatingab18Jahren = 600,
		// Token: 0x040004F7 RID: 1271
		[EnumMember]
		FRRating10minus = 100,
		// Token: 0x040004F8 RID: 1272
		[EnumMember]
		FRRating12minus = 200,
		// Token: 0x040004F9 RID: 1273
		[EnumMember]
		FRRating16minus = 500,
		// Token: 0x040004FA RID: 1274
		[EnumMember]
		FRRating18minus = 600,
		// Token: 0x040004FB RID: 1275
		[EnumMember]
		IERatingG = 100,
		// Token: 0x040004FC RID: 1276
		[EnumMember]
		IERatingPG = 200,
		// Token: 0x040004FD RID: 1277
		[EnumMember]
		IERating12 = 300,
		// Token: 0x040004FE RID: 1278
		[EnumMember]
		IERating15 = 350,
		// Token: 0x040004FF RID: 1279
		[EnumMember]
		IERating16 = 375,
		// Token: 0x04000500 RID: 1280
		[EnumMember]
		IERating18 = 400,
		// Token: 0x04000501 RID: 1281
		[EnumMember]
		JPRatingG = 100,
		// Token: 0x04000502 RID: 1282
		[EnumMember]
		JPRatingPG12 = 200,
		// Token: 0x04000503 RID: 1283
		[EnumMember]
		JPRatingRdash15 = 300,
		// Token: 0x04000504 RID: 1284
		[EnumMember]
		JPRatingRdash18 = 400,
		// Token: 0x04000505 RID: 1285
		[EnumMember]
		NZRatingG = 100,
		// Token: 0x04000506 RID: 1286
		[EnumMember]
		NZRatingPG = 200,
		// Token: 0x04000507 RID: 1287
		[EnumMember]
		NZRatingM = 300,
		// Token: 0x04000508 RID: 1288
		[EnumMember]
		NZRatingR13 = 325,
		// Token: 0x04000509 RID: 1289
		[EnumMember]
		NZRatingR15 = 350,
		// Token: 0x0400050A RID: 1290
		[EnumMember]
		NZRatingR16 = 375,
		// Token: 0x0400050B RID: 1291
		[EnumMember]
		NZRatingR18 = 400,
		// Token: 0x0400050C RID: 1292
		[EnumMember]
		NZRatingR = 500,
		// Token: 0x0400050D RID: 1293
		[EnumMember]
		NZRatingRP16 = 600,
		// Token: 0x0400050E RID: 1294
		[EnumMember]
		GBRatingU = 100,
		// Token: 0x0400050F RID: 1295
		[EnumMember]
		GBRatingUc = 150,
		// Token: 0x04000510 RID: 1296
		[EnumMember]
		GBRatingPG = 200,
		// Token: 0x04000511 RID: 1297
		[EnumMember]
		GBRating12 = 300,
		// Token: 0x04000512 RID: 1298
		[EnumMember]
		GBRating12A = 325,
		// Token: 0x04000513 RID: 1299
		[EnumMember]
		GBRating15 = 350,
		// Token: 0x04000514 RID: 1300
		[EnumMember]
		GBRating18 = 400
	}
}
