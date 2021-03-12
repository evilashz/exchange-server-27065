using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D0 RID: 464
	internal class CalendarPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x00069A19 File Offset: 0x00067C19
		public CalendarPrototypeSchemaState() : base(CalendarPrototypeSchemaState.supportedClassFilter, new ExceptionPrototypeSchemaState())
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00069A38 File Offset: 0x00067C38
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return CalendarPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00069A40 File Offset: 0x00067C40
		private void CreatePropertyConversionTable()
		{
			bool nodelete = true;
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
				new XsoSubjectProperty()
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
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Calendar, true, 140),
				new XsoRecurrenceProperty(TypeOfRecurrence.Calendar, 140)
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
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace, "DisallowNewTimeProposal", false),
				new XsoBooleanProperty(CalendarItemBaseSchema.DisallowNewTimeProposal)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace, "ResponseRequested", false),
				new XsoBooleanProperty(ItemSchema.IsResponseRequested, nodelete)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "AppointmentReplyTime", false),
				new XsoUtcDateTimeProperty(CalendarItemBaseSchema.AppointmentReplyTime, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "ResponseType", false),
				new XsoIntegerProperty(CalendarItemBaseSchema.ResponseType, PropertyType.ReadOnly)
			});
		}

		// Token: 0x04000B9E RID: 2974
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.APPOINTMENT"
		};

		// Token: 0x04000B9F RID: 2975
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(CalendarPrototypeSchemaState.supportedClassTypes);
	}
}
