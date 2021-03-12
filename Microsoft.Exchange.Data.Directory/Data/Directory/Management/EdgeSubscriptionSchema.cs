using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006FC RID: 1788
	internal sealed class EdgeSubscriptionSchema : ADPresentationSchema
	{
		// Token: 0x06005443 RID: 21571 RVA: 0x001319DD File Offset: 0x0012FBDD
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADObjectSchema>();
		}
	}
}
