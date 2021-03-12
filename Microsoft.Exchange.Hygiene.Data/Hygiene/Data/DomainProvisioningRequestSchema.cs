using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000CF RID: 207
	internal class DomainProvisioningRequestSchema
	{
		// Token: 0x04000430 RID: 1072
		internal static readonly HygienePropertyDefinition OrganizationalUnitRoot = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x04000431 RID: 1073
		internal static readonly HygienePropertyDefinition DomainName = new HygienePropertyDefinition("DomainName", typeof(string));

		// Token: 0x04000432 RID: 1074
		public static readonly HygienePropertyDefinition RequestType = new HygienePropertyDefinition("RequestType", typeof(string));

		// Token: 0x04000433 RID: 1075
		public static readonly HygienePropertyDefinition RequestFlags = new HygienePropertyDefinition("RequestFlags", typeof(DomainProvisioningRequestFlags), DomainProvisioningRequestFlags.Default, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
