using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV121
{
	// Token: 0x020001C5 RID: 453
	internal class ExceptionPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x000668A9 File Offset: 0x00064AA9
		public ExceptionPrototypeSchemaState() : base(null)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000668C0 File Offset: 0x00064AC0
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Calendar:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "DtStamp", true),
				new XsoUtcDateTimeProperty(StoreObjectSchema.CreationTime)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "StartTime", true),
				new XsoUtcDateTimeProperty(CalendarItemInstanceSchema.StartTime)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", true),
				new XsoStringProperty(ItemSchema.Subject)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Location", true),
				new XsoPersistentStringProperty(CalendarItemBaseSchema.Location)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "EndTime", true),
				new XsoUtcDateTimeProperty(CalendarItemInstanceSchema.EndTime)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncContentProperty(xmlNodeNamespace2, "Body", false),
				new XsoContentProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMultiValuedStringProperty(xmlNodeNamespace, "Categories", "Category", true),
				new XsoCategoriesProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "Sensitivity", true),
				new XsoSensitivityProperty(ItemSchema.Sensitivity)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "ExceptionStartTime", true),
				new XsoApptExceptionUtcStartTimeProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBusyStatusProperty(xmlNodeNamespace, "BusyStatus", true),
				new XsoBusyStatusProperty(BusyType.Free)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace, "AllDayEvent", true),
				new XsoBooleanProperty(CalendarItemBaseSchema.MapiIsAllDayEvent)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncReminderProperty(xmlNodeNamespace, "Reminder", true),
				new XsoReminderOffsetProperty()
			});
		}
	}
}
