using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200077B RID: 1915
	internal class ExternalUserCalendarResponseShape : ExternalUserResponseShape
	{
		// Token: 0x06003938 RID: 14648 RVA: 0x000CA598 File Offset: 0x000C8798
		private static List<PropertyPath> CreateList(List<PropertyPath> originalList, params PropertyPath[] additionalProperties)
		{
			List<PropertyPath> list = new List<PropertyPath>(originalList);
			foreach (PropertyPath item in additionalProperties)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000CA5C8 File Offset: 0x000C87C8
		protected override List<PropertyPath> PropertiesAllowedForReadAccess
		{
			get
			{
				return ExternalUserCalendarResponseShape.calendarItemReadProps;
			}
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000CA5CF File Offset: 0x000C87CF
		public ExternalUserCalendarResponseShape(Permission permissionGranted)
		{
			base.Permissions = permissionGranted;
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000CA5E0 File Offset: 0x000C87E0
		protected override PropertyPath[] GetPropertiesForCustomPermissions(ItemResponseShape requestedResponseShape)
		{
			CalendarFolderPermission calendarFolderPermission = base.Permissions as CalendarFolderPermission;
			switch (calendarFolderPermission.FreeBusyAccess)
			{
			case FreeBusyAccess.Basic:
				ExTraceGlobals.ExternalUserTracer.TraceDebug<ExternalUserCalendarResponseShape>((long)this.GetHashCode(), "{0}: overriding shape for FreeBusy Basic permissions.", this);
				return ExternalUserResponseShape.GetAllowedProperties(requestedResponseShape, ExternalUserCalendarResponseShape.calendarItemFreeBusyProps);
			case FreeBusyAccess.Details:
				ExTraceGlobals.ExternalUserTracer.TraceDebug<ExternalUserCalendarResponseShape>((long)this.GetHashCode(), "{0}: overriding shape for FreeBusy Detailed permissions.", this);
				return ExternalUserResponseShape.GetAllowedProperties(requestedResponseShape, ExternalUserCalendarResponseShape.calendarItemFreeBusyDetailsProps);
			default:
				return null;
			}
		}

		// Token: 0x04001FE9 RID: 8169
		private static List<PropertyPath> calendarItemFreeBusyProps = new List<PropertyPath>
		{
			ItemSchema.ItemId.PropertyPath,
			CalendarItemSchema.CalendarItemType.PropertyPath,
			CalendarItemSchema.Start.PropertyPath,
			CalendarItemSchema.End.PropertyPath,
			CalendarItemSchema.IsAllDayEvent.PropertyPath,
			CalendarItemSchema.IsCancelled.PropertyPath,
			CalendarItemSchema.IsRecurring.PropertyPath,
			CalendarItemSchema.LegacyFreeBusyStatus.PropertyPath,
			CalendarItemSchema.OrganizerSpecific.Recurrence.PropertyPath,
			CalendarItemSchema.ModifiedOccurrences.PropertyPath,
			CalendarItemSchema.DeletedOccurrences.PropertyPath,
			CalendarItemSchema.Duration.PropertyPath,
			CalendarItemSchema.OrganizerSpecific.StartTimeZone.PropertyPath,
			CalendarItemSchema.OrganizerSpecific.EndTimeZone.PropertyPath,
			CalendarItemSchema.TimeZone.PropertyPath
		};

		// Token: 0x04001FEA RID: 8170
		public static List<PropertyPath> CalendarPropertiesPrivateItem = ExternalUserCalendarResponseShape.CreateList(ExternalUserCalendarResponseShape.calendarItemFreeBusyProps, new PropertyPath[]
		{
			ItemSchema.Sensitivity.PropertyPath
		});

		// Token: 0x04001FEB RID: 8171
		public static List<PropertyPath> CalendarPropertiesPrivateItemWithSubject = ExternalUserCalendarResponseShape.CreateList(ExternalUserCalendarResponseShape.CalendarPropertiesPrivateItem, new PropertyPath[]
		{
			ItemSchema.Subject.PropertyPath
		});

		// Token: 0x04001FEC RID: 8172
		private static List<PropertyPath> calendarItemFreeBusyDetailsProps = ExternalUserCalendarResponseShape.CreateList(ExternalUserCalendarResponseShape.CalendarPropertiesPrivateItem, new PropertyPath[]
		{
			ItemSchema.Subject.PropertyPath,
			CalendarItemSchema.Location.PropertyPath
		});

		// Token: 0x04001FED RID: 8173
		private static List<PropertyPath> calendarItemReadProps = ExternalUserCalendarResponseShape.CreateList(ExternalUserCalendarResponseShape.calendarItemFreeBusyDetailsProps, new PropertyPath[]
		{
			ItemSchema.Body.PropertyPath
		});
	}
}
