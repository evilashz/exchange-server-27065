using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200022D RID: 557
	internal class XsoPersistentStringProperty : XsoStringProperty
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x00079518 File Offset: 0x00077718
		public XsoPersistentStringProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00079521 File Offset: 0x00077721
		public XsoPersistentStringProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0007952B File Offset: 0x0007772B
		public XsoPersistentStringProperty(StorePropertyDefinition propertyDef, PropertyType type, params PropertyDefinition[] prefechProperties) : base(propertyDef, type, prefechProperties)
		{
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00079536 File Offset: 0x00077736
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = string.Empty;
		}
	}
}
