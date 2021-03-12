using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarItemFields
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00006650 File Offset: 0x00004850
		internal static GlobalObjectId GetGlobalObjectId(CalendarItemType remoteItem)
		{
			if (remoteItem != null && remoteItem.ExtendedProperty != null)
			{
				foreach (ExtendedPropertyType extendedPropertyType in remoteItem.ExtendedProperty)
				{
					if (extendedPropertyType.ExtendedFieldURI != null && extendedPropertyType.Item != null && string.Compare(extendedPropertyType.ExtendedFieldURI.PropertySetId, CalendarItemFields.PSETIDMeeting.ToString(), StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyId == CalendarItemFields.GlobalObjectIdProp.PropertyId && extendedPropertyType.ExtendedFieldURI.PropertyType == CalendarItemFields.GlobalObjectIdProp.PropertyType)
					{
						return new GlobalObjectId(Convert.FromBase64String((string)extendedPropertyType.Item));
					}
				}
			}
			return null;
		}

		// Token: 0x0400004D RID: 77
		private const int CleanGlobalObjectIdPropertyId = 35;

		// Token: 0x0400004E RID: 78
		private const int GlobalObjectIdPropertyId = 3;

		// Token: 0x0400004F RID: 79
		private const int AppointmentExtractTimePropertyId = 33325;

		// Token: 0x04000050 RID: 80
		private const int AppointmentExtractVersionPropertyId = 33324;

		// Token: 0x04000051 RID: 81
		private const int AppointmentRecurrenceBlobPropertyId = 33302;

		// Token: 0x04000052 RID: 82
		private const int TimeZoneBlobPropertyId = 33331;

		// Token: 0x04000053 RID: 83
		private const int TimeZoneDefinitionStartPropertyId = 33374;

		// Token: 0x04000054 RID: 84
		private const int TimeZoneDefinitionEndPropertyId = 33375;

		// Token: 0x04000055 RID: 85
		private const int TimeZoneDefinitionRecurringPropertyId = 33376;

		// Token: 0x04000056 RID: 86
		private const int OwnerCriticalChangeTimePropertyId = 26;

		// Token: 0x04000057 RID: 87
		private const int AttendeeCriticalChangeTimePropertyId = 1;

		// Token: 0x04000058 RID: 88
		private const int ItemVersionPropertyId = 22;

		// Token: 0x04000059 RID: 89
		private const int AppointmentRecurringPropertyId = 33315;

		// Token: 0x0400005A RID: 90
		private const int IsExceptionPropertyId = 10;

		// Token: 0x0400005B RID: 91
		private const string OwnerAppointmentIDPropertyTag = "0x0062";

		// Token: 0x0400005C RID: 92
		private const string CreationTimePropertyTag = "0x3007";

		// Token: 0x0400005D RID: 93
		private const string ItemClassPropertyTag = "0x001A";

		// Token: 0x0400005E RID: 94
		internal static readonly Guid PSETIDAppointment = new Guid("{00062002-0000-0000-C000-000000000046}");

		// Token: 0x0400005F RID: 95
		internal static readonly Guid PSETIDMeeting = new Guid("{6ED8DA90-450B-101B-98DA-00AA003F1305}");

		// Token: 0x04000060 RID: 96
		internal static readonly Guid PSETIDCalendarAssistant = new Guid("{11000E07-B51B-40D6-AF21-CAA85EDAB1D0}");

		// Token: 0x04000061 RID: 97
		internal static readonly PathToExtendedFieldType CleanGlobalObjectIdProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDMeeting.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 35,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x04000062 RID: 98
		internal static readonly PathToExtendedFieldType GlobalObjectIdProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDMeeting.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 3,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x04000063 RID: 99
		internal static readonly PathToExtendedFieldType AppointmentExtractTimeProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33325,
			PropertyType = MapiPropertyTypeType.SystemTime
		};

		// Token: 0x04000064 RID: 100
		internal static readonly PathToExtendedFieldType AppointmentExtractVersionProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33324,
			PropertyType = MapiPropertyTypeType.Long
		};

		// Token: 0x04000065 RID: 101
		internal static readonly PathToExtendedFieldType AppointmentRecurrenceBlobProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33302,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x04000066 RID: 102
		internal static readonly PathToExtendedFieldType TimeZoneBlobProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33331,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x04000067 RID: 103
		internal static readonly PathToExtendedFieldType TimeZoneDefinitionStartProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33374,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x04000068 RID: 104
		internal static readonly PathToExtendedFieldType TimeZoneDefinitionEndProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33375,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x04000069 RID: 105
		internal static readonly PathToExtendedFieldType TimeZoneDefinitionRecurringProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33376,
			PropertyType = MapiPropertyTypeType.Binary
		};

		// Token: 0x0400006A RID: 106
		internal static readonly PathToExtendedFieldType OwnerCriticalChangeTimeProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDMeeting.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 26,
			PropertyType = MapiPropertyTypeType.SystemTime
		};

		// Token: 0x0400006B RID: 107
		internal static readonly PathToExtendedFieldType AttendeeCriticalChangeTimeProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDMeeting.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 1,
			PropertyType = MapiPropertyTypeType.SystemTime
		};

		// Token: 0x0400006C RID: 108
		internal static readonly PathToExtendedFieldType ItemVersionProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDCalendarAssistant.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 22,
			PropertyType = MapiPropertyTypeType.Integer
		};

		// Token: 0x0400006D RID: 109
		internal static readonly PathToExtendedFieldType OwnerAppointmentIDProp = new PathToExtendedFieldType
		{
			PropertyTag = "0x0062",
			PropertyType = MapiPropertyTypeType.Integer
		};

		// Token: 0x0400006E RID: 110
		internal static readonly PathToExtendedFieldType CreationTimeProp = new PathToExtendedFieldType
		{
			PropertyTag = "0x3007",
			PropertyType = MapiPropertyTypeType.SystemTime
		};

		// Token: 0x0400006F RID: 111
		internal static readonly PathToExtendedFieldType ItemClassProp = new PathToExtendedFieldType
		{
			PropertyTag = "0x001A",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000070 RID: 112
		internal static readonly PathToExtendedFieldType AppointmentRecurringProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDAppointment.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 33315,
			PropertyType = MapiPropertyTypeType.Boolean
		};

		// Token: 0x04000071 RID: 113
		internal static readonly PathToExtendedFieldType IsExceptionProp = new PathToExtendedFieldType
		{
			PropertySetId = CalendarItemFields.PSETIDMeeting.ToString(),
			PropertyIdSpecified = true,
			PropertyId = 10,
			PropertyType = MapiPropertyTypeType.Boolean
		};

		// Token: 0x04000072 RID: 114
		internal static readonly ItemResponseShapeType CalendarQueryShape = new ItemResponseShapeType
		{
			BaseShape = DefaultShapeNamesType.IdOnly,
			IncludeMimeContent = false,
			IncludeMimeContentSpecified = false,
			BodyType = BodyTypeResponseType.HTML,
			BodyTypeSpecified = true,
			AdditionalProperties = new BasePathToElementType[]
			{
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarCalendarItemType
				}
			}
		};

		// Token: 0x04000073 RID: 115
		internal static readonly ItemResponseShapeType CalendarItemShape = new ItemResponseShapeType
		{
			BaseShape = DefaultShapeNamesType.IdOnly,
			IncludeMimeContent = false,
			IncludeMimeContentSpecified = false,
			BodyType = BodyTypeResponseType.HTML,
			BodyTypeSpecified = true,
			AdditionalProperties = new BasePathToElementType[]
			{
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.itemItemId
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.itemSubject
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarStart
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarEnd
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarLocation
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarCalendarItemType
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarMyResponseType
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarIsResponseRequested
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarAppointmentReplyTime
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarAppointmentState
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarAppointmentSequenceNumber
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarTimeZone
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarStartTimeZone
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarEndTimeZone
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarRequiredAttendees
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.calendarOptionalAttendees
				},
				new PathToUnindexedFieldType
				{
					FieldURI = UnindexedFieldURIType.itemLastModifiedTime
				},
				CalendarItemFields.CleanGlobalObjectIdProp,
				CalendarItemFields.GlobalObjectIdProp,
				CalendarItemFields.AppointmentExtractTimeProp,
				CalendarItemFields.AppointmentExtractVersionProp,
				CalendarItemFields.AppointmentRecurrenceBlobProp,
				CalendarItemFields.TimeZoneBlobProp,
				CalendarItemFields.TimeZoneDefinitionStartProp,
				CalendarItemFields.TimeZoneDefinitionEndProp,
				CalendarItemFields.TimeZoneDefinitionRecurringProp,
				CalendarItemFields.OwnerCriticalChangeTimeProp,
				CalendarItemFields.AttendeeCriticalChangeTimeProp,
				CalendarItemFields.ItemVersionProp,
				CalendarItemFields.OwnerAppointmentIDProp,
				CalendarItemFields.CreationTimeProp,
				CalendarItemFields.ItemClassProp,
				CalendarItemFields.AppointmentRecurringProp,
				CalendarItemFields.IsExceptionProp
			}
		};
	}
}
