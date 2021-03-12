using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000458 RID: 1112
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EwsStoreObjectSchema : ObjectSchema
	{
		// Token: 0x04001AAC RID: 6828
		public static readonly EwsStoreObjectPropertyDefinition AlternativeId = new EwsStoreObjectPropertyDefinition("AlternativeId", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, null, ExtendedEwsStoreObjectSchema.AlternativeId);

		// Token: 0x04001AAD RID: 6829
		public static readonly EwsStoreObjectPropertyDefinition Identity = new EwsStoreObjectPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2007, typeof(EwsStoreObjectId), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.Id);

		// Token: 0x04001AAE RID: 6830
		public static readonly EwsStoreObjectPropertyDefinition ItemClass = new EwsStoreObjectPropertyDefinition("ItemClass", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, null, ItemSchema.ItemClass, delegate(Item item, object value)
		{
			item.ItemClass = (string)value;
		});

		// Token: 0x04001AAF RID: 6831
		public static readonly SimplePropertyDefinition ObjectState = new SimplePropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2007, typeof(ObjectState), PropertyDefinitionFlags.None, Microsoft.Exchange.Data.ObjectState.New, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001AB0 RID: 6832
		public static readonly EwsStoreObjectPropertyDefinition PolicyTag = new EwsStoreObjectPropertyDefinition("PolicyTag", ExchangeObjectVersion.Exchange2007, typeof(Guid?), PropertyDefinitionFlags.None, null, null, ItemSchema.PolicyTag, delegate(Item item, object value)
		{
			item.PolicyTag = (PolicyTag)value;
		});

		// Token: 0x04001AB1 RID: 6833
		public static readonly EwsStoreObjectPropertyDefinition ExchangeVersion = new EwsStoreObjectPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2007, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.PersistDefaultValue, ExchangeObjectVersion.Exchange2007, ExchangeObjectVersion.Exchange2007, ExtendedEwsStoreObjectSchema.ExchangeVersion);
	}
}
