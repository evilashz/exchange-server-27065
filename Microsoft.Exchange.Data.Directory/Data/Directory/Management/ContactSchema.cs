using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006ED RID: 1773
	internal class ContactSchema : OrgPersonPresentationObjectSchema
	{
		// Token: 0x060052FC RID: 21244 RVA: 0x001302B3 File Offset: 0x0012E4B3
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADContactSchema>();
		}

		// Token: 0x0400382F RID: 14383
		public static readonly ADPropertyDefinition OrganizationalUnit = ADRecipientSchema.OrganizationalUnit;
	}
}
