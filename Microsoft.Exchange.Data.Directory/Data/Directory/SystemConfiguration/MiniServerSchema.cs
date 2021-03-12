using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E6 RID: 1254
	internal class MiniServerSchema : ADObjectSchema
	{
		// Token: 0x040025DA RID: 9690
		public static readonly ADPropertyDefinition Fqdn = ServerSchema.Fqdn;

		// Token: 0x040025DB RID: 9691
		public static readonly ADPropertyDefinition VersionNumber = ServerSchema.VersionNumber;

		// Token: 0x040025DC RID: 9692
		public static readonly ADPropertyDefinition IsE14OrLater = ServerSchema.IsE14OrLater;

		// Token: 0x040025DD RID: 9693
		public static readonly ADPropertyDefinition MajorVersion = ServerSchema.MajorVersion;

		// Token: 0x040025DE RID: 9694
		public static readonly ADPropertyDefinition AdminDisplayVersion = ServerSchema.AdminDisplayVersion;

		// Token: 0x040025DF RID: 9695
		public static readonly ADPropertyDefinition ServerSite = ServerSchema.ServerSite;
	}
}
