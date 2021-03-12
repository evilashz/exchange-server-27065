using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000034 RID: 52
	public sealed class CalendarSchema : StorageEntitySchema
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000037EB File Offset: 0x000019EB
		public CalendarSchema()
		{
			base.RegisterPropertyDefinition(CalendarSchema.StaticColorProperty);
			base.RegisterPropertyDefinition(CalendarSchema.StaticNameProperty);
			base.RegisterPropertyDefinition(CalendarSchema.StaticCalendarFolderStoreIdProperty);
			base.RegisterPropertyDefinition(CalendarSchema.StaticRecordKeyProperty);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000381F File Offset: 0x00001A1F
		public TypedPropertyDefinition<CalendarColor> ColorProperty
		{
			get
			{
				return CalendarSchema.StaticColorProperty;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003826 File Offset: 0x00001A26
		public TypedPropertyDefinition<string> NameProperty
		{
			get
			{
				return CalendarSchema.StaticNameProperty;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000382D File Offset: 0x00001A2D
		internal TypedPropertyDefinition<StoreId> CalendarFolderStoreIdProperty
		{
			get
			{
				return CalendarSchema.StaticCalendarFolderStoreIdProperty;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003834 File Offset: 0x00001A34
		internal TypedPropertyDefinition<byte[]> RecordKeyProperty
		{
			get
			{
				return CalendarSchema.StaticRecordKeyProperty;
			}
		}

		// Token: 0x04000070 RID: 112
		private static readonly TypedPropertyDefinition<StoreId> StaticCalendarFolderStoreIdProperty = new TypedPropertyDefinition<StoreId>("Calendar.InternalCalendarFolderStoreId", null, true);

		// Token: 0x04000071 RID: 113
		private static readonly TypedPropertyDefinition<CalendarColor> StaticColorProperty = new TypedPropertyDefinition<CalendarColor>("Calendar.Color", CalendarColor.Auto, true);

		// Token: 0x04000072 RID: 114
		private static readonly TypedPropertyDefinition<string> StaticNameProperty = new TypedPropertyDefinition<string>("Calendar.Name", null, true);

		// Token: 0x04000073 RID: 115
		private static readonly TypedPropertyDefinition<byte[]> StaticRecordKeyProperty = new TypedPropertyDefinition<byte[]>("Calendar.RecordKey", null, true);
	}
}
