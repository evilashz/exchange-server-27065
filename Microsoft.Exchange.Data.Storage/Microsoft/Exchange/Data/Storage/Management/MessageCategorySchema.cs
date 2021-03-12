using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A7F RID: 2687
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MessageCategorySchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x040037BA RID: 14266
		public const string NameParameterName = "Name";

		// Token: 0x040037BB RID: 14267
		public const string ColorParameterName = "Color";

		// Token: 0x040037BC RID: 14268
		public const string GuidParameterName = "Guid";

		// Token: 0x040037BD RID: 14269
		public const string IdentityParameterName = "Identity";

		// Token: 0x040037BE RID: 14270
		public const string CategoryIdentityParameterName = "CategoryIdentity";

		// Token: 0x040037BF RID: 14271
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new List<PropertyDefinitionConstraint>(CategorySchema.Name.Constraints).ToArray());

		// Token: 0x040037C0 RID: 14272
		public static readonly SimpleProviderPropertyDefinition Color = new SimpleProviderPropertyDefinition("Color", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, new List<PropertyDefinitionConstraint>(CategorySchema.Color.Constraints).ToArray());

		// Token: 0x040037C1 RID: 14273
		public static readonly SimpleProviderPropertyDefinition Guid = new SimpleProviderPropertyDefinition("Guid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037C2 RID: 14274
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(MessageCategoryId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageCategorySchema.Guid,
			XsoMailboxConfigurationObjectSchema.MailboxOwnerId
		}, null, new GetterDelegate(MessageCategory.IdentityGetter), null);
	}
}
