using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000EA RID: 234
	public enum AttributeSyntax
	{
		// Token: 0x0400048E RID: 1166
		Boolean = 1,
		// Token: 0x0400048F RID: 1167
		Integer,
		// Token: 0x04000490 RID: 1168
		Sid = 4,
		// Token: 0x04000491 RID: 1169
		Octet = 4,
		// Token: 0x04000492 RID: 1170
		ObjectIdentifier = 6,
		// Token: 0x04000493 RID: 1171
		Enumeration = 10,
		// Token: 0x04000494 RID: 1172
		DeliveryMechanism = 10,
		// Token: 0x04000495 RID: 1173
		ExportInformationLevel = 10,
		// Token: 0x04000496 RID: 1174
		PreferredDeliveryMethod = 10,
		// Token: 0x04000497 RID: 1175
		Numeric = 18,
		// Token: 0x04000498 RID: 1176
		Printable,
		// Token: 0x04000499 RID: 1177
		Teletex,
		// Token: 0x0400049A RID: 1178
		IA5 = 22,
		// Token: 0x0400049B RID: 1179
		UTCTime,
		// Token: 0x0400049C RID: 1180
		GeneralizedTime,
		// Token: 0x0400049D RID: 1181
		CaseSensitive = 27,
		// Token: 0x0400049E RID: 1182
		Unicode = 64,
		// Token: 0x0400049F RID: 1183
		Interval,
		// Token: 0x040004A0 RID: 1184
		LargeInteger = 65,
		// Token: 0x040004A1 RID: 1185
		NTSecDesc,
		// Token: 0x040004A2 RID: 1186
		AccessPoint = 127,
		// Token: 0x040004A3 RID: 1187
		DNBinary = 127,
		// Token: 0x040004A4 RID: 1188
		DNString = 127,
		// Token: 0x040004A5 RID: 1189
		DSDN = 127,
		// Token: 0x040004A6 RID: 1190
		ORName = 127,
		// Token: 0x040004A7 RID: 1191
		PresentationAddress = 127,
		// Token: 0x040004A8 RID: 1192
		ReplicaLink = 127
	}
}
