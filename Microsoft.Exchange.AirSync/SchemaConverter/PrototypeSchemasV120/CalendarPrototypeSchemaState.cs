using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV120
{
	// Token: 0x020001BD RID: 445
	internal class CalendarPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x00063A41 File Offset: 0x00061C41
		public CalendarPrototypeSchemaState() : base(CalendarPrototypeSchemaState.supportedClassFilter, new ExceptionPrototypeSchemaState())
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00063A60 File Offset: 0x00061C60
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return CalendarPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00063A68 File Offset: 0x00061C68
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Calendar:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncBinaryTimeZoneProperty(xmlNodeNamespace, "TimeZone", true),
				new XsoTimeZoneProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "DtStamp", true),
				new XsoUtcDtStampProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "StartTime", true),
				new XsoLocalDateTimeProperty(CalendarItemInstanceSchema.StartTime, null)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", true),
				new XsoStringProperty(ItemSchema.Subject)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "UID", true),
				new XsoUidProperty(CalendarItemBaseSchema.GlobalObjectId)
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
				new AirSyncExtendedAttendeesProperty(xmlNodeNamespace, "Attendees", false),
				new XsoExtendedAttendeesProperty()
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
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Calendar, true, 120),
				new XsoRecurrenceProperty(TypeOfRecurrence.Calendar, 120)
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
				new AirSyncExceptionsProperty(xmlNodeNamespace, "Exceptions", true),
				new XsoExceptionsProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "MeetingStatus", false),
				new XsoMeetingStatusProperty(CalendarItemBaseSchema.AppointmentState)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace2, "NativeBodyType", false),
				new XsoNativeBodyProperty()
			});
		}

		// Token: 0x04000B62 RID: 2914
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.APPOINTMENT"
		};

		// Token: 0x04000B63 RID: 2915
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(CalendarPrototypeSchemaState.supportedClassTypes);
	}
}
