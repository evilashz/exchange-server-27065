using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000E0 RID: 224
	internal class DeletedObjectSchema : ADObjectSchema
	{
		// Token: 0x04000465 RID: 1125
		public static readonly ADPropertyDefinition IsDeleted = new ADPropertyDefinition("IsDeleted", ExchangeObjectVersion.Exchange2003, typeof(bool), "isDeleted", ADPropertyDefinitionFlags.FilterOnly, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.IsDeleted, null);

		// Token: 0x04000466 RID: 1126
		public static readonly ADPropertyDefinition EndOfList = SharedPropertyDefinitions.EndOfList;

		// Token: 0x04000467 RID: 1127
		public static readonly ADPropertyDefinition Cookie = SharedPropertyDefinitions.Cookie;

		// Token: 0x04000468 RID: 1128
		public static ADPropertyDefinition LastKnownParent = new ADPropertyDefinition("LastKnownParent", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "lastKnownParent", ADPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
