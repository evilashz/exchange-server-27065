using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000F0 RID: 240
	[Flags]
	internal enum ADPropertyDefinitionFlags
	{
		// Token: 0x040004D5 RID: 1237
		None = 0,
		// Token: 0x040004D6 RID: 1238
		ReadOnly = 1,
		// Token: 0x040004D7 RID: 1239
		MultiValued = 2,
		// Token: 0x040004D8 RID: 1240
		Calculated = 4,
		// Token: 0x040004D9 RID: 1241
		FilterOnly = 8,
		// Token: 0x040004DA RID: 1242
		Mandatory = 16,
		// Token: 0x040004DB RID: 1243
		PersistDefaultValue = 32,
		// Token: 0x040004DC RID: 1244
		WriteOnce = 64,
		// Token: 0x040004DD RID: 1245
		Binary = 128,
		// Token: 0x040004DE RID: 1246
		TaskPopulated = 256,
		// Token: 0x040004DF RID: 1247
		DoNotProvisionalClone = 512,
		// Token: 0x040004E0 RID: 1248
		ValidateInFirstOrganization = 1024,
		// Token: 0x040004E1 RID: 1249
		DoNotValidate = 2048,
		// Token: 0x040004E2 RID: 1250
		BackLink = 4096,
		// Token: 0x040004E3 RID: 1251
		Ranged = 8192,
		// Token: 0x040004E4 RID: 1252
		ValidateInSharedConfig = 16384,
		// Token: 0x040004E5 RID: 1253
		ForestSpecific = 32768,
		// Token: 0x040004E6 RID: 1254
		NonADProperty = 65536
	}
}
