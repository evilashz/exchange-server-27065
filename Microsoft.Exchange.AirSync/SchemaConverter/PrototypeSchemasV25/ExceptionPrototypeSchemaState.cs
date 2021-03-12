using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV25
{
	// Token: 0x020001CE RID: 462
	internal class ExceptionPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001300 RID: 4864 RVA: 0x00069559 File Offset: 0x00067759
		public ExceptionPrototypeSchemaState() : base(null)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00069570 File Offset: 0x00067770
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Calendar:";
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
				new AirSyncStringProperty(xmlNodeNamespace, "OrganizerName", false),
				new XsoOrganizerNameProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OrganizerEmail", false),
				new XsoOrganizerEmailProperty()
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
				new AirSyncBodyProperty(xmlNodeNamespace, "Body", "BodyTruncated", null, false, true, true),
				new XsoBodyProperty()
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
