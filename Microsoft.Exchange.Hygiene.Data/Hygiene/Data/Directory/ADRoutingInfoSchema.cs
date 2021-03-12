using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C3 RID: 195
	internal class ADRoutingInfoSchema : ADObjectSchema
	{
		// Token: 0x040003F0 RID: 1008
		public static readonly HygienePropertyDefinition IdProp = new HygienePropertyDefinition("ADFfoRoutingInfo.Id", typeof(ADObject));
	}
}
