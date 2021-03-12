using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006D4 RID: 1748
	internal sealed class GlobalAddressListSchema : AddressListBaseSchema
	{
		// Token: 0x060050D7 RID: 20695 RVA: 0x0012C42D File Offset: 0x0012A62D
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<AddressBookBaseSchema>();
		}

		// Token: 0x040036FC RID: 14076
		public static readonly ADPropertyDefinition IsDefaultGlobalAddressList = AddressBookBaseSchema.IsDefaultGlobalAddressList;

		// Token: 0x040036FD RID: 14077
		public new static readonly ADPropertyDefinition Name = AddressListBaseSchema.Name;
	}
}
