using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV121
{
	// Token: 0x020001C2 RID: 450
	internal class CalendarPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x060012D4 RID: 4820 RVA: 0x000653A9 File Offset: 0x000635A9
		public CalendarPrototypeSchemaState() : base(CalendarPrototypeSchemaState.supportedClassFilter, new ExceptionPrototypeSchemaState())
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000653C8 File Offset: 0x000635C8
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return CalendarPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000653D0 File Offset: 0x000635D0
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
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Calendar, true, 121),
				new XsoRecurrenceProperty(TypeOfRecurrence.Calendar, 121)
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

		// Token: 0x04000B74 RID: 2932
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.APPOINTMENT"
		};

		// Token: 0x04000B75 RID: 2933
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(CalendarPrototypeSchemaState.supportedClassTypes);
	}
}
