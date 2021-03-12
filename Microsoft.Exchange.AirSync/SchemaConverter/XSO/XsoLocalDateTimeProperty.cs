using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	internal class XsoLocalDateTimeProperty : XsoProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x060014C1 RID: 5313 RVA: 0x000785EE File Offset: 0x000767EE
		public XsoLocalDateTimeProperty(StorePropertyDefinition timestampDef, StorePropertyDefinition timeZoneDef) : base(timestampDef)
		{
			this.timeZoneDef = timeZoneDef;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000785FE File Offset: 0x000767FE
		public XsoLocalDateTimeProperty(StorePropertyDefinition timestampDef, StorePropertyDefinition timeZoneDef, PropertyType type) : base(timestampDef, type)
		{
			this.timeZoneDef = timeZoneDef;
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0007860F File Offset: 0x0007680F
		public ExDateTime DateTime
		{
			get
			{
				return ExTimeZone.UtcTimeZone.ConvertDateTime((ExDateTime)base.XsoItem.TryGetProperty(base.PropertyDef));
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00078634 File Offset: 0x00076834
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			if (PropertyState.SetToDefault == srcProperty.State)
			{
				throw new ConversionException("Object type does not support setting to default");
			}
			ExTimeZone exTimeZone = null;
			if (this.timeZoneDef != null)
			{
				exTimeZone = base.XsoItem.GetValueOrDefault<ExTimeZone>(this.timeZoneDef);
			}
			if (exTimeZone == null || exTimeZone.Equals(ExTimeZone.UtcTimeZone))
			{
				exTimeZone = TimeZoneHelper.GetPromotedTimeZoneFromItem(base.XsoItem as Item);
			}
			ExDateTime dateTime = ((IDateTimeProperty)srcProperty).DateTime;
			ExDateTime exDateTime = exTimeZone.ConvertDateTime(dateTime);
			base.XsoItem[base.PropertyDef] = exDateTime;
		}

		// Token: 0x04000C75 RID: 3189
		private StorePropertyDefinition timeZoneDef;
	}
}
