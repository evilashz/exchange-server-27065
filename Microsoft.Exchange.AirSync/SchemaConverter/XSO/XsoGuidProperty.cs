using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001F9 RID: 505
	internal class XsoGuidProperty : XsoProperty, IStringProperty, IProperty
	{
		// Token: 0x060013D4 RID: 5076 RVA: 0x000720E0 File Offset: 0x000702E0
		public XsoGuidProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000720E9 File Offset: 0x000702E9
		public XsoGuidProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000720F3 File Offset: 0x000702F3
		public XsoGuidProperty(StorePropertyDefinition propertyDef, PropertyType type, PropertyDefinition[] prefetchPropDef) : base(propertyDef, type, prefetchPropDef)
		{
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00072100 File Offset: 0x00070300
		public virtual string StringData
		{
			get
			{
				return ((Guid)base.XsoItem.TryGetProperty(base.PropertyDef)).ToString();
			}
		}
	}
}
