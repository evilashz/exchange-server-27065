using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000235 RID: 565
	[Serializable]
	internal class XsoSensitivityProperty : XsoIntegerProperty
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x0007B5CF File Offset: 0x000797CF
		public XsoSensitivityProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0007B5D8 File Offset: 0x000797D8
		public XsoSensitivityProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0007B5E4 File Offset: 0x000797E4
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			if (!(base.XsoItem is CalendarItemBase) && !(base.XsoItem is Task))
			{
				throw new UnexpectedTypeException("CalendarItemBase or Task", base.XsoItem);
			}
			Item item = (Item)base.XsoItem;
			if (!(item.TryGetProperty(base.PropertyDef) is PropertyError))
			{
				item.Sensitivity = Sensitivity.Normal;
				return;
			}
		}
	}
}
