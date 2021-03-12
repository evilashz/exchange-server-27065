using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E8 RID: 1768
	internal class ClientAccessServerSchema : ADPresentationSchema
	{
		// Token: 0x060052B5 RID: 21173 RVA: 0x0012FB36 File Offset: 0x0012DD36
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ServerSchema>();
		}

		// Token: 0x040037DC RID: 14300
		public static readonly ADPropertyDefinition ClientAccessArray = ServerSchema.ClientAccessArray;
	}
}
