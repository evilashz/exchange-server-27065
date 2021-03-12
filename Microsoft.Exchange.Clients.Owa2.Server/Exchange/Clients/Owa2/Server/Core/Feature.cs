using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D3 RID: 211
	[Flags]
	public enum Feature : ulong
	{
		// Token: 0x040004A5 RID: 1189
		GlobalAddressList = 1UL,
		// Token: 0x040004A6 RID: 1190
		Calendar = 2UL,
		// Token: 0x040004A7 RID: 1191
		Contacts = 4UL,
		// Token: 0x040004A8 RID: 1192
		Tasks = 8UL,
		// Token: 0x040004A9 RID: 1193
		Journal = 16UL,
		// Token: 0x040004AA RID: 1194
		StickyNotes = 32UL,
		// Token: 0x040004AB RID: 1195
		PublicFolders = 64UL,
		// Token: 0x040004AC RID: 1196
		Organization = 128UL,
		// Token: 0x040004AD RID: 1197
		Notifications = 256UL,
		// Token: 0x040004AE RID: 1198
		RichClient = 512UL,
		// Token: 0x040004AF RID: 1199
		SpellChecker = 1024UL,
		// Token: 0x040004B0 RID: 1200
		SMime = 2048UL,
		// Token: 0x040004B1 RID: 1201
		SearchFolders = 4096UL,
		// Token: 0x040004B2 RID: 1202
		Signature = 8192UL,
		// Token: 0x040004B3 RID: 1203
		Rules = 16384UL,
		// Token: 0x040004B4 RID: 1204
		Themes = 32768UL,
		// Token: 0x040004B5 RID: 1205
		JunkEMail = 65536UL,
		// Token: 0x040004B6 RID: 1206
		UMIntegration = 131072UL,
		// Token: 0x040004B7 RID: 1207
		WssIntegrationFromPublicComputer = 262144UL,
		// Token: 0x040004B8 RID: 1208
		WssIntegrationFromPrivateComputer = 524288UL,
		// Token: 0x040004B9 RID: 1209
		UncIntegrationFromPublicComputer = 1048576UL,
		// Token: 0x040004BA RID: 1210
		UncIntegrationFromPrivateComputer = 2097152UL,
		// Token: 0x040004BB RID: 1211
		EasMobileOptions = 4194304UL,
		// Token: 0x040004BC RID: 1212
		ExplicitLogon = 8388608UL,
		// Token: 0x040004BD RID: 1213
		AddressLists = 16777216UL,
		// Token: 0x040004BE RID: 1214
		Dumpster = 33554432UL,
		// Token: 0x040004BF RID: 1215
		ChangePassword = 67108864UL,
		// Token: 0x040004C0 RID: 1216
		InstantMessage = 134217728UL,
		// Token: 0x040004C1 RID: 1217
		TextMessage = 268435456UL,
		// Token: 0x040004C2 RID: 1218
		OWALight = 536870912UL,
		// Token: 0x040004C3 RID: 1219
		DelegateAccess = 1073741824UL,
		// Token: 0x040004C4 RID: 1220
		Irm = 2147483648UL,
		// Token: 0x040004C5 RID: 1221
		ForceSaveAttachmentFiltering = 4294967296UL,
		// Token: 0x040004C6 RID: 1222
		Silverlight = 8589934592UL,
		// Token: 0x040004C7 RID: 1223
		DisplayPhotos = 2199023255552UL,
		// Token: 0x040004C8 RID: 1224
		SetPhoto = 4398046511104UL,
		// Token: 0x040004C9 RID: 1225
		PredictedActions = 35184372088832UL,
		// Token: 0x040004CA RID: 1226
		UserDiagnosticEnabled = 140737488355328UL,
		// Token: 0x040004CB RID: 1227
		FacebookEnabled = 281474976710656UL,
		// Token: 0x040004CC RID: 1228
		LinkedInEnabled = 562949953421312UL,
		// Token: 0x040004CD RID: 1229
		WacExternalServicesEnabled = 1125899906842624UL,
		// Token: 0x040004CE RID: 1230
		WacOMEXEnabled = 2251799813685248UL,
		// Token: 0x040004CF RID: 1231
		WebPartsDefaultOrigin = 4503599627370496UL,
		// Token: 0x040004D0 RID: 1232
		WebPartsEnableOrigins = 9007199254740992UL,
		// Token: 0x040004D1 RID: 1233
		ReportJunkEmail = 36028797018963968UL,
		// Token: 0x040004D2 RID: 1234
		GroupCreationEnabled = 72057594037927936UL,
		// Token: 0x040004D3 RID: 1235
		SkipCreateUnifiedGroupCustomSharepointClassification = 144115188075855872UL,
		// Token: 0x040004D4 RID: 1236
		All = 18446744073709551615UL
	}
}
