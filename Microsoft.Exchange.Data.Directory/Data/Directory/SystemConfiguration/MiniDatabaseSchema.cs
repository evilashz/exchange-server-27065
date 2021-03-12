using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E0 RID: 1248
	internal class MiniDatabaseSchema : ADObjectSchema
	{
		// Token: 0x040025C3 RID: 9667
		public static readonly ADPropertyDefinition Server = DatabaseSchema.Server;

		// Token: 0x040025C4 RID: 9668
		public static readonly ADPropertyDefinition ServerName = DatabaseSchema.ServerName;

		// Token: 0x040025C5 RID: 9669
		public static readonly ADPropertyDefinition MasterServerOrAvailabilityGroup = DatabaseSchema.MasterServerOrAvailabilityGroup;

		// Token: 0x040025C6 RID: 9670
		public static readonly ADPropertyDefinition UsnChanged = SharedPropertyDefinitions.UsnChanged;
	}
}
