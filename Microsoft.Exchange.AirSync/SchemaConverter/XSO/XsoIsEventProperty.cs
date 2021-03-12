using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000220 RID: 544
	internal class XsoIsEventProperty : XsoBooleanProperty
	{
		// Token: 0x060014BB RID: 5307 RVA: 0x00078498 File Offset: 0x00076698
		public XsoIsEventProperty() : base(CalendarItemBaseSchema.IsEvent, new PropertyDefinition[]
		{
			CalendarItemBaseSchema.MapiIsAllDayEvent
		})
		{
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000784C0 File Offset: 0x000766C0
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.XsoItem[CalendarItemBaseSchema.MapiIsAllDayEvent] = ((IBooleanProperty)srcProperty).BooleanData;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000784E4 File Offset: 0x000766E4
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			if (!(base.XsoItem.TryGetProperty(CalendarItemBaseSchema.MapiIsAllDayEvent) is PropertyError))
			{
				base.XsoItem.DeleteProperties(new PropertyDefinition[]
				{
					CalendarItemBaseSchema.MapiIsAllDayEvent
				});
			}
		}
	}
}
