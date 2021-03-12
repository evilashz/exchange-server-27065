using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000742 RID: 1858
	internal class PublicDatabasePresentationObjectSchema : MailEnabledOrgPersonSchema
	{
		// Token: 0x06005A92 RID: 23186 RVA: 0x0013DD9A File Offset: 0x0013BF9A
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADPublicDatabaseSchema>();
		}
	}
}
