using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F8 RID: 1784
	internal class ADDomainControllerSchema : ADPresentationSchema
	{
		// Token: 0x060053EB RID: 21483 RVA: 0x00131464 File Offset: 0x0012F664
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADObjectSchema>();
		}
	}
}
