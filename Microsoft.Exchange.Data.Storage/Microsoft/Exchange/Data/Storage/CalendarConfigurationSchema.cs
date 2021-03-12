using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020009FE RID: 2558
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarConfigurationSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x0400343A RID: 13370
		public static readonly XsoDictionaryPropertyDefinition AutomateProcessing = new XsoDictionaryPropertyDefinition("Calendar", "AutomateProcessing", ExchangeObjectVersion.Exchange2007, typeof(CalendarProcessingFlags), PropertyDefinitionFlags.None, CalendarProcessingFlags.AutoUpdate, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400343B RID: 13371
		public static readonly XsoDictionaryPropertyDefinition AllowConflicts = new XsoDictionaryPropertyDefinition("Calendar", "AllowConflicts", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400343C RID: 13372
		public static readonly XsoDictionaryPropertyDefinition BookingWindowInDays = new XsoDictionaryPropertyDefinition("Calendar", "BookingWindowInDays", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 180, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1080)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x0400343D RID: 13373
		public static readonly XsoDictionaryPropertyDefinition MaximumDurationInMinutes = new XsoDictionaryPropertyDefinition("Calendar", "MaximumDurationInMinutes", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 1440, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x0400343E RID: 13374
		public static readonly XsoDictionaryPropertyDefinition AllowRecurringMeetings = new XsoDictionaryPropertyDefinition("Calendar", "AllowRecurringMeetings", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400343F RID: 13375
		public static readonly XsoDictionaryPropertyDefinition EnforceSchedulingHorizon = new XsoDictionaryPropertyDefinition("Calendar", "EnforceSchedulingHorizon", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003440 RID: 13376
		public static readonly XsoDictionaryPropertyDefinition ScheduleOnlyDuringWorkHours = new XsoDictionaryPropertyDefinition("Calendar", "ScheduleOnlyDuringWorkHours", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003441 RID: 13377
		public static readonly XsoDictionaryPropertyDefinition ConflictPercentageAllowed = new XsoDictionaryPropertyDefinition("Calendar", "ConflictPercentageAllowed", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 100)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04003442 RID: 13378
		public static readonly XsoDictionaryPropertyDefinition MaximumConflictInstances = new XsoDictionaryPropertyDefinition("Calendar", "MaximumConflictInstances", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04003443 RID: 13379
		public static readonly XsoDictionaryPropertyDefinition ForwardRequestsToDelegates = new XsoDictionaryPropertyDefinition("Calendar", "ForwardRequestsToDelegates", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003444 RID: 13380
		public static readonly XsoDictionaryPropertyDefinition DeleteAttachments = new XsoDictionaryPropertyDefinition("Calendar", "DeleteAttachments", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003445 RID: 13381
		public static readonly XsoDictionaryPropertyDefinition DeleteComments = new XsoDictionaryPropertyDefinition("Calendar", "DeleteComments", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003446 RID: 13382
		public static readonly XsoDictionaryPropertyDefinition RemovePrivateProperty = new XsoDictionaryPropertyDefinition("Calendar", "RemovePrivateProperty", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003447 RID: 13383
		public static readonly XsoDictionaryPropertyDefinition DeleteSubject = new XsoDictionaryPropertyDefinition("Calendar", "DeleteSubject", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003448 RID: 13384
		public static readonly XsoDictionaryPropertyDefinition DisableReminders = new XsoDictionaryPropertyDefinition("Calendar", "DisableReminders", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003449 RID: 13385
		public static readonly XsoDictionaryPropertyDefinition AddOrganizerToSubject = new XsoDictionaryPropertyDefinition("Calendar", "AddOrganizerToSubject", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400344A RID: 13386
		public static readonly XsoDictionaryPropertyDefinition DeleteNonCalendarItems = new XsoDictionaryPropertyDefinition("Calendar", "DeleteNonCalendarItems", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400344B RID: 13387
		public static readonly XsoDictionaryPropertyDefinition TentativePendingApproval = new XsoDictionaryPropertyDefinition("Calendar", "TentativePendingApproval", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400344C RID: 13388
		public static readonly XsoDictionaryPropertyDefinition EnableResponseDetails = new XsoDictionaryPropertyDefinition("Calendar", "EnableResponseDetails", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400344D RID: 13389
		public static readonly XsoDictionaryPropertyDefinition OrganizerInfo = new XsoDictionaryPropertyDefinition("Calendar", "OrganizerInfo", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400344E RID: 13390
		public static readonly XsoDictionaryPropertyDefinition RequestOutOfPolicy = new XsoDictionaryPropertyDefinition("Calendar", "RequestOutOfPolicy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400344F RID: 13391
		public static readonly XsoDictionaryPropertyDefinition AllRequestOutOfPolicy = new XsoDictionaryPropertyDefinition("Calendar", "AllRequestOutOfPolicy", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003450 RID: 13392
		public static readonly XsoDictionaryPropertyDefinition BookInPolicy = new XsoDictionaryPropertyDefinition("Calendar", "BookInPolicy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003451 RID: 13393
		public static readonly XsoDictionaryPropertyDefinition AllBookInPolicy = new XsoDictionaryPropertyDefinition("Calendar", "AllBookInPolicy", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003452 RID: 13394
		public static readonly XsoDictionaryPropertyDefinition RequestInPolicy = new XsoDictionaryPropertyDefinition("Calendar", "RequestInPolicy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003453 RID: 13395
		public static readonly XsoDictionaryPropertyDefinition AllRequestInPolicy = new XsoDictionaryPropertyDefinition("Calendar", "AllRequestInPolicy", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003454 RID: 13396
		public static readonly XsoDictionaryPropertyDefinition AddAdditionalResponse = new XsoDictionaryPropertyDefinition("Calendar", "AddAdditionalResponse", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003455 RID: 13397
		public static readonly XsoDictionaryPropertyDefinition AdditionalResponse = new XsoDictionaryPropertyDefinition("Calendar", "AdditionalResponse", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003456 RID: 13398
		public static readonly XsoDictionaryPropertyDefinition RemoveOldMeetingMessages = new XsoDictionaryPropertyDefinition("Calendar", "calAssistNoiseReduction", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003457 RID: 13399
		public static readonly XsoDictionaryPropertyDefinition AddNewRequestsTentatively = new XsoDictionaryPropertyDefinition("Calendar", "calAssistAddNewItems", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003458 RID: 13400
		public static readonly XsoDictionaryPropertyDefinition ProcessExternalMeetingMessages = new XsoDictionaryPropertyDefinition("Calendar", "calAssistProcessExternal", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003459 RID: 13401
		public static readonly XsoDictionaryPropertyDefinition DefaultReminderTime = new XsoDictionaryPropertyDefinition("Calendar", "piRemindDefault", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 15, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400345A RID: 13402
		public static readonly XsoDictionaryPropertyDefinition RemoveForwardedMeetingNotifications = new XsoDictionaryPropertyDefinition("Calendar", "RemoveForwardedMeetingNotifications", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400345B RID: 13403
		public static readonly SimpleProviderPropertyDefinition ResourceDelegates = new SimpleProviderPropertyDefinition(string.Empty, ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400345C RID: 13404
		public static readonly XsoDictionaryPropertyDefinition SkipProcessing = new XsoDictionaryPropertyDefinition("Calendar", "SkipProcessing", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400345D RID: 13405
		public static readonly XsoDictionaryPropertyDefinition BookInPolicyLegDN = new XsoDictionaryPropertyDefinition("Calendar", "BookInPolicyLegDN", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400345E RID: 13406
		public static readonly XsoDictionaryPropertyDefinition RequestInPolicyLegDN = new XsoDictionaryPropertyDefinition("Calendar", "RequestInPolicyLegDN", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400345F RID: 13407
		public static readonly XsoDictionaryPropertyDefinition RequestOutOfPolicyLegDN = new XsoDictionaryPropertyDefinition("Calendar", "RequestOutOfPolicyLegDN", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
