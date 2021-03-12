using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000704 RID: 1796
	internal class ExchangeRoleEntryPresentationSchema : ADPresentationSchema
	{
		// Token: 0x0600548B RID: 21643 RVA: 0x00132054 File Offset: 0x00130254
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ExchangeRoleSchema>();
		}

		// Token: 0x040038C1 RID: 14529
		public static readonly ADPropertyDefinition Role = ADObjectSchema.Id;
	}
}
