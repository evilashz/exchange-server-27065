using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001AE RID: 430
	internal class CalendarConfigurationCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x0002EAA8 File Offset: 0x0002CCA8
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"AutomateProcessing",
				"RemoveForwardedMeetingNotifications",
				"RemoveOldMeetingMessages",
				"AddNewRequestsTentatively",
				"ProcessExternalMeetingMessages",
				"AllowConflicts",
				"AllowRecurringMeetings",
				"ScheduleOnlyDuringWorkHours",
				"EnforceSchedulingHorizon",
				"ForwardRequestsToDelegates",
				"BookingWindowInDays",
				"MaximumDurationInMinutes",
				"MaximumConflictInstances",
				"ConflictPercentageAllowed",
				"ResourceDelegates",
				"TentativePendingApproval",
				"AddAdditionalResponse",
				"OrganizerInfo",
				"RemovePrivateProperty",
				"AddOrganizerToSubject",
				"DeleteNonCalendarItems",
				"DeleteSubject",
				"DeleteComments",
				"DeleteAttachments",
				"AdditionalResponse",
				"AllBookInPolicy",
				"AllRequestInPolicy",
				"BookInPolicy",
				"RequestInPolicy",
				"AllRequestOutOfPolicy",
				"RequestOutOfPolicy",
				"MailboxOwnerId"
			};
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002EBE0 File Offset: 0x0002CDE0
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "RemoveOldMeetingMessages")
			{
				configObject.propertyBag[CalendarConfigurationSchema.RemoveOldMeetingMessages] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, CalendarConfigurationSchema.RemoveOldMeetingMessages.Type);
				return;
			}
			if (propertyName == "AddNewRequestsTentatively")
			{
				configObject.propertyBag[CalendarConfigurationSchema.AddNewRequestsTentatively] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, CalendarConfigurationSchema.AddNewRequestsTentatively.Type);
				return;
			}
			if (propertyName == "ProcessExternalMeetingMessages")
			{
				configObject.propertyBag[CalendarConfigurationSchema.ProcessExternalMeetingMessages] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, CalendarConfigurationSchema.ProcessExternalMeetingMessages.Type);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
