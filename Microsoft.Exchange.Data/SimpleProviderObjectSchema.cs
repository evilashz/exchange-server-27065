using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000123 RID: 291
	internal class SimpleProviderObjectSchema : ObjectSchema
	{
		// Token: 0x0400063A RID: 1594
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(ObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400063B RID: 1595
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.None, ExchangeObjectVersion.Exchange2003, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400063C RID: 1596
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2003, typeof(ObjectState), PropertyDefinitionFlags.TaskPopulated, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
