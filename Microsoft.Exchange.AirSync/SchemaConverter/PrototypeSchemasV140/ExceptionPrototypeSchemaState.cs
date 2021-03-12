using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D4 RID: 468
	internal class ExceptionPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x0006B4D2 File Offset: 0x000696D2
		public ExceptionPrototypeSchemaState() : base(null)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0006B4E8 File Offset: 0x000696E8
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
				new XsoSubjectProperty()
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
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", false),
				new XsoContent14Property()
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
			base.AddProperty(new IProperty[]
			{
				new AirSyncExtendedAttendeesProperty(xmlNodeNamespace, "Attendees", true),
				new XsoExtendedAttendeesProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "AppointmentReplyTime", true),
				new XsoUtcDateTimeProperty(CalendarItemBaseSchema.AppointmentReplyTime, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "ResponseType", true),
				new XsoIntegerProperty(CalendarItemBaseSchema.ResponseType, PropertyType.ReadOnly)
			});
		}
	}
}
