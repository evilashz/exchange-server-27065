using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000218 RID: 536
	internal class XsoEvmStringProperty : XsoStringProperty
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x00076B4A File Offset: 0x00074D4A
		public XsoEvmStringProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
			base.SupportedItemClasses = Constants.EvmSupportedItemClassPrefixes;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00076B5E File Offset: 0x00074D5E
		public XsoEvmStringProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
			base.SupportedItemClasses = Constants.EvmSupportedItemClassPrefixes;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00076B73 File Offset: 0x00074D73
		public XsoEvmStringProperty(StorePropertyDefinition propertyDef, PropertyType type, params PropertyDefinition[] prefechProperties) : base(propertyDef, type, prefechProperties)
		{
			base.SupportedItemClasses = Constants.EvmSupportedItemClassPrefixes;
		}
	}
}
