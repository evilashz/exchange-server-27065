using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	internal class XsoApptExceptionUtcStartTimeProperty : XsoProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x000721A2 File Offset: 0x000703A2
		public XsoApptExceptionUtcStartTimeProperty(PropertyType t) : base(null, t)
		{
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x000721AC File Offset: 0x000703AC
		public ExDateTime DateTime
		{
			get
			{
				CalendarItemOccurrence calendarItemOccurrence = (CalendarItemOccurrence)base.XsoItem;
				return ExTimeZone.UtcTimeZone.ConvertDateTime(calendarItemOccurrence.OriginalStartTime);
			}
		}
	}
}
