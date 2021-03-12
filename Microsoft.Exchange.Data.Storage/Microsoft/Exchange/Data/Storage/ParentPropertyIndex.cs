using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B2 RID: 946
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ParentPropertyIndex
	{
		// Token: 0x0400183E RID: 6206
		public static readonly int AppointmentRecurrenceBlob = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.AppointmentRecurrenceBlob);

		// Token: 0x0400183F RID: 6207
		public static readonly int AppointmentRecurring = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.AppointmentRecurring);

		// Token: 0x04001840 RID: 6208
		public static readonly int IsRecurring = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.IsRecurring);

		// Token: 0x04001841 RID: 6209
		public static readonly int IsException = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.IsException);

		// Token: 0x04001842 RID: 6210
		public static readonly int TimeZoneBlob = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.TimeZoneBlob);

		// Token: 0x04001843 RID: 6211
		public static readonly int RecurrencePattern = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.RecurrencePattern);

		// Token: 0x04001844 RID: 6212
		public static readonly int RecurrenceType = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.MeetingReplyForwardProperties, InternalSchema.MapiRecurrenceType);
	}
}
