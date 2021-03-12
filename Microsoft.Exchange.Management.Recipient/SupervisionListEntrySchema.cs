using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000110 RID: 272
	internal class SupervisionListEntrySchema : ObjectSchema
	{
		// Token: 0x040003D1 RID: 977
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(SupervisionListEntryId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003D2 RID: 978
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003D3 RID: 979
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003D4 RID: 980
		public static readonly SimpleProviderPropertyDefinition EntryName = new SimpleProviderPropertyDefinition("EntryName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003D5 RID: 981
		public static readonly SimpleProviderPropertyDefinition Tag = new SimpleProviderPropertyDefinition("Tag", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040003D6 RID: 982
		public static readonly SimpleProviderPropertyDefinition RecipientType = new SimpleProviderPropertyDefinition("RecipientType", ExchangeObjectVersion.Exchange2010, typeof(SupervisionRecipientType), PropertyDefinitionFlags.None, SupervisionRecipientType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
