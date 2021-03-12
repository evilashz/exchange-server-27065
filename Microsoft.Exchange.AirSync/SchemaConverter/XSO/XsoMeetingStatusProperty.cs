using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000227 RID: 551
	[Serializable]
	internal class XsoMeetingStatusProperty : XsoIntegerProperty
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x000792FA File Offset: 0x000774FA
		public XsoMeetingStatusProperty(StorePropertyDefinition propertyDef) : base(propertyDef, PropertyType.ReadOnly)
		{
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00079304 File Offset: 0x00077504
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}
	}
}
