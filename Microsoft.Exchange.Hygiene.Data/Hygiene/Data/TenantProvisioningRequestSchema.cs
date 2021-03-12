using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000108 RID: 264
	internal class TenantProvisioningRequestSchema
	{
		// Token: 0x04000553 RID: 1363
		internal static readonly HygienePropertyDefinition OrganizationalUnitRoot = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x04000554 RID: 1364
		public static readonly HygienePropertyDefinition RequestType = new HygienePropertyDefinition("RequestType", typeof(string));

		// Token: 0x04000555 RID: 1365
		public static readonly HygienePropertyDefinition RequestFlags = new HygienePropertyDefinition("RequestFlags", typeof(TenantProvisioningRequestFlags), TenantProvisioningRequestFlags.Default, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000556 RID: 1366
		public static readonly HygienePropertyDefinition MigrateToRegion = new HygienePropertyDefinition("MigrateToRegion", typeof(string));

		// Token: 0x04000557 RID: 1367
		public static readonly HygienePropertyDefinition MigrateToInstance = new HygienePropertyDefinition("MigrateToInstance", typeof(string));
	}
}
