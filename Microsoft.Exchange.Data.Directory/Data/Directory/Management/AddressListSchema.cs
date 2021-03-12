using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006D5 RID: 1749
	internal sealed class AddressListSchema : AddressListBaseSchema
	{
		// Token: 0x060050DA RID: 20698 RVA: 0x0012C452 File Offset: 0x0012A652
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<AddressBookBaseSchema>();
		}

		// Token: 0x040036FE RID: 14078
		public static readonly ADPropertyDefinition Container = AddressBookBaseSchema.Container;

		// Token: 0x040036FF RID: 14079
		public static readonly ADPropertyDefinition Path = AddressBookBaseSchema.Path;

		// Token: 0x04003700 RID: 14080
		public static readonly ADPropertyDefinition DisplayName = AddressBookBaseSchema.DisplayName;

		// Token: 0x04003701 RID: 14081
		public new static readonly ADPropertyDefinition Name = AddressListBaseSchema.Name;
	}
}
