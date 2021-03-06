using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV20
{
	// Token: 0x020001C7 RID: 455
	internal class CalendarPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x00066D11 File Offset: 0x00064F11
		public CalendarPrototypeSchemaState() : base(CalendarPrototypeSchemaState.supportedClassFilter, new ExceptionPrototypeSchemaState())
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00066D30 File Offset: 0x00064F30
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return CalendarPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00066D38 File Offset: 0x00064F38
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Calendar:";
			AirSyncBodyProperty airSyncBodyProperty = new AirSyncBodyProperty(xmlNodeNamespace, "Body", "Rtf", "BodyTruncated", null, false, true, true, true);
			base.AddProperty(new IProperty[]
			{
				new AirSyncBinaryTimeZoneProperty(xmlNodeNamespace, "TimeZone", true),
				new XsoTimeZoneProperty(PropertyType.ReadAndRequiredForWrite)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "DtStamp", false),
				new XsoUtcDtStampProperty(PropertyType.ReadAndRequiredForWrite)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "StartTime", true),
				new XsoLocalDateTimeProperty(CalendarItemInstanceSchema.StartTime, null, PropertyType.ReadAndRequiredForWrite)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", true),
				new XsoStringProperty(ItemSchema.Subject)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "UID", false),
				new XsoUidProperty(CalendarItemBaseSchema.GlobalObjectId, PropertyType.ReadAndRequiredForWrite)
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
				new AirSyncAttendeesProperty(xmlNodeNamespace, "Attendees", false),
				new XsoAttendeesProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Location", true),
				new XsoStringProperty(CalendarItemBaseSchema.Location)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "EndTime", true),
				new XsoLocalDateTimeProperty(CalendarItemInstanceSchema.EndTime, CalendarItemBaseSchema.EndTimeZone)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Calendar, true, 20),
				new XsoRecurrenceProperty(TypeOfRecurrence.Calendar, 20)
			});
			base.AddProperty(new IProperty[]
			{
				airSyncBodyProperty,
				new XsoBodyProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMultiValuedStringProperty(xmlNodeNamespace, "Categories", "Category", false),
				new XsoCategoriesProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "Sensitivity", false),
				new XsoSensitivityProperty(ItemSchema.Sensitivity, PropertyType.ReadAndRequiredForWrite)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBusyStatusProperty(xmlNodeNamespace, "BusyStatus", false),
				new XsoBusyStatusProperty(BusyType.Free, PropertyType.ReadAndRequiredForWrite)
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
				new AirSyncExceptionsProperty(xmlNodeNamespace, "Exceptions", true),
				new XsoExceptionsProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncRtfBodyProperty(xmlNodeNamespace, "Rtf", false, airSyncBodyProperty),
				new XsoBodyProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "MeetingStatus", false),
				new XsoMeetingStatusProperty(CalendarItemBaseSchema.AppointmentState)
			});
		}

		// Token: 0x04000B86 RID: 2950
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.APPOINTMENT"
		};

		// Token: 0x04000B87 RID: 2951
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(CalendarPrototypeSchemaState.supportedClassTypes);
	}
}
