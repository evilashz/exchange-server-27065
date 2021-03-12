using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000237 RID: 567
	[Serializable]
	internal class XsoUtcDateTimeProperty : XsoProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x0007B6A6 File Offset: 0x000798A6
		public XsoUtcDateTimeProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0007B6AF File Offset: 0x000798AF
		public XsoUtcDateTimeProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0007B6B9 File Offset: 0x000798B9
		public XsoUtcDateTimeProperty(StorePropertyDefinition propertyDef, PropertyDefinition[] prefetchPropDef) : base(propertyDef, prefetchPropDef)
		{
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0007B6C3 File Offset: 0x000798C3
		public virtual ExDateTime DateTime
		{
			get
			{
				return ExTimeZone.UtcTimeZone.ConvertDateTime((ExDateTime)base.XsoItem.TryGetProperty(base.PropertyDef));
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0007B6E8 File Offset: 0x000798E8
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			if (PropertyState.SetToDefault == srcProperty.State)
			{
				throw new ConversionException("Object type does not support setting to default");
			}
			ExDateTime exDateTime = ((IDateTimeProperty)srcProperty).DateTime;
			exDateTime = ExTimeZone.UtcTimeZone.ConvertDateTime(exDateTime);
			base.XsoItem[base.PropertyDef] = exDateTime;
		}
	}
}
