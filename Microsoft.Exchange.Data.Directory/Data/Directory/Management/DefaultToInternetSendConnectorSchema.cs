using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F0 RID: 1776
	internal sealed class DefaultToInternetSendConnectorSchema : ADPresentationSchema
	{
		// Token: 0x06005353 RID: 21331 RVA: 0x001307F9 File Offset: 0x0012E9F9
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADObjectSchema>();
		}
	}
}
