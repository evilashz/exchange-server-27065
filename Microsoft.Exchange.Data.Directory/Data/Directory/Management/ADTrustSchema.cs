using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006DC RID: 1756
	internal class ADTrustSchema : ADPresentationSchema
	{
		// Token: 0x0600513D RID: 20797 RVA: 0x0012CAD7 File Offset: 0x0012ACD7
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADObjectSchema>();
		}
	}
}
