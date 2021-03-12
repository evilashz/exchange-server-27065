using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x020009F5 RID: 2549
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserConfigurationObjectSchema : ObjectSchema
	{
		// Token: 0x0400341A RID: 13338
		public static readonly SimplePropertyDefinition ExchangeVersion = new SimplePropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.None, ExchangeObjectVersion.Exchange2003, ExchangeObjectVersion.Exchange2003, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400341B RID: 13339
		public static readonly SimplePropertyDefinition ExchangePrincipal = new SimplePropertyDefinition("ExchangePrincipal", ExchangeObjectVersion.Exchange2010, typeof(ExchangePrincipal), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400341C RID: 13340
		public static readonly SimplePropertyDefinition ObjectState = new SimplePropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2003, typeof(ObjectState), PropertyDefinitionFlags.None, Microsoft.Exchange.Data.ObjectState.New, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
