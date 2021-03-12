using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C2 RID: 194
	internal class ADMiniUserSchema
	{
		// Token: 0x040003EC RID: 1004
		public static readonly HygienePropertyDefinition UserIdProp = new HygienePropertyDefinition("UserId", typeof(ADObjectId));

		// Token: 0x040003ED RID: 1005
		public static readonly PropertyDefinition TenantIdProp = new HygienePropertyDefinition("TenantId", typeof(ADObjectId));

		// Token: 0x040003EE RID: 1006
		public static readonly PropertyDefinition ConfigurationIdProp = new HygienePropertyDefinition("ConfigurationId", typeof(ADObjectId));

		// Token: 0x040003EF RID: 1007
		public static readonly HygienePropertyDefinition NetIdProp = new HygienePropertyDefinition("NetId", typeof(NetID));
	}
}
