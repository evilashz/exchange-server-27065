using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200022A RID: 554
	internal class TenantCookieSchema : BaseCookieSchema
	{
		// Token: 0x04000B5D RID: 2909
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("TenantId", typeof(ADObjectId));
	}
}
