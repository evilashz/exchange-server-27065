using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C48 RID: 3144
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactProtectedPropertiesSchema : Schema
	{
		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06006F35 RID: 28469 RVA: 0x001DE633 File Offset: 0x001DC833
		public new static ContactProtectedPropertiesSchema Instance
		{
			get
			{
				return ContactProtectedPropertiesSchema.instance;
			}
		}

		// Token: 0x0400424E RID: 16974
		public static readonly StorePropertyDefinition ProtectedEmailAddress = InternalSchema.ProtectedEmailAddress;

		// Token: 0x0400424F RID: 16975
		public static readonly StorePropertyDefinition ProtectedPhoneNumber = InternalSchema.ProtectedPhoneNumber;

		// Token: 0x04004250 RID: 16976
		private static readonly ContactProtectedPropertiesSchema instance = new ContactProtectedPropertiesSchema();
	}
}
