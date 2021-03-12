using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006D6 RID: 1750
	internal sealed class SystemAddressListSchema : AddressListBaseSchema
	{
		// Token: 0x060050DD RID: 20701 RVA: 0x0012C48B File Offset: 0x0012A68B
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<AddressBookBaseSchema>();
		}

		// Token: 0x04003702 RID: 14082
		public new static readonly ADPropertyDefinition Name = AddressListBaseSchema.Name;
	}
}
