using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C83 RID: 3203
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingMessageInstanceSchema : MeetingMessageSchema
	{
		// Token: 0x0600703F RID: 28735 RVA: 0x001F1524 File Offset: 0x001EF724
		protected MeetingMessageInstanceSchema()
		{
			base.AddDependencies(new Schema[]
			{
				CalendarItemSchema.Instance
			});
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06007040 RID: 28736 RVA: 0x001F154D File Offset: 0x001EF74D
		public new static MeetingMessageInstanceSchema Instance
		{
			get
			{
				if (MeetingMessageInstanceSchema.instance == null)
				{
					MeetingMessageInstanceSchema.instance = new MeetingMessageInstanceSchema();
				}
				return MeetingMessageInstanceSchema.instance;
			}
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x001F1565 File Offset: 0x001EF765
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			CalendarItemBase.CoreObjectUpdateLocationAddress(coreItem);
			base.CoreObjectUpdate(coreItem, operation);
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06007042 RID: 28738 RVA: 0x001F1575 File Offset: 0x001EF775
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				return base.PropertyRules.Concat(MeetingMessageInstanceSchema.MeetingMessageInstanceSchemaPropertyRules);
			}
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x001F15A8 File Offset: 0x001EF7A8
		// Note: this type is marked as 'beforefieldinit'.
		static MeetingMessageInstanceSchema()
		{
			PropertyRule[] array = new PropertyRule[4];
			array[0] = PropertyRuleLibrary.DefaultCleanGlobalObjectIdFromGlobalObjectId;
			array[1] = PropertyRuleLibrary.LocationLidWhere;
			array[2] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(56479U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.NativeStartTimeForMessage,
				PropertyRuleLibrary.NativeStartTimeToReminderTime,
				PropertyRuleLibrary.DefaultReminderNextTimeFromStartTimeAndOffset,
				PropertyRuleLibrary.NativeEndTimeForMessage,
				PropertyRuleLibrary.ClipEndTimeForSingleMeeting,
				PropertyRuleLibrary.ClipStartTimeForSingleMeeting
			});
			array[3] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(44191U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.DefaultInvitedForMeetingMessage,
				PropertyRuleLibrary.DefaultAppointmentStateFromItemClass,
				PropertyRuleLibrary.SchedulePlusPropertiesToRecurrenceBlob,
				PropertyRuleLibrary.RecurrenceBlobToFlags
			});
			MeetingMessageInstanceSchema.MeetingMessageInstanceSchemaPropertyRules = array;
			MeetingMessageInstanceSchema.instance = null;
		}

		// Token: 0x04004D3A RID: 19770
		[Autoload]
		public static readonly StorePropertyDefinition OwnerAppointmentID = InternalSchema.OwnerAppointmentID;

		// Token: 0x04004D3B RID: 19771
		[Autoload]
		internal static readonly StorePropertyDefinition RecurrencePattern = InternalSchema.RecurrencePattern;

		// Token: 0x04004D3C RID: 19772
		[Autoload]
		internal static readonly StorePropertyDefinition RecurrenceType = InternalSchema.CalculatedRecurrenceType;

		// Token: 0x04004D3D RID: 19773
		[Autoload]
		internal static readonly StorePropertyDefinition ResponseState = InternalSchema.ResponseState;

		// Token: 0x04004D3E RID: 19774
		[Autoload]
		public static readonly StorePropertyDefinition CalendarProcessingSteps = InternalSchema.CalendarProcessingSteps;

		// Token: 0x04004D3F RID: 19775
		[Autoload]
		public static readonly StorePropertyDefinition OriginalMeetingType = InternalSchema.OriginalMeetingType;

		// Token: 0x04004D40 RID: 19776
		[Autoload]
		public static readonly StorePropertyDefinition SideEffects = InternalSchema.SideEffects;

		// Token: 0x04004D41 RID: 19777
		[Autoload]
		public static readonly StorePropertyDefinition IsProcessed = InternalSchema.IsProcessed;

		// Token: 0x04004D42 RID: 19778
		[Autoload]
		public static readonly StorePropertyDefinition MapiStartTime = InternalSchema.MapiStartTime;

		// Token: 0x04004D43 RID: 19779
		[Autoload]
		internal static readonly StorePropertyDefinition MapiEndTime = InternalSchema.MapiEndTime;

		// Token: 0x04004D44 RID: 19780
		[Autoload]
		internal static readonly StorePropertyDefinition MapiPRStartDate = InternalSchema.MapiPRStartDate;

		// Token: 0x04004D45 RID: 19781
		[Autoload]
		internal static readonly StorePropertyDefinition MapiPREndDate = InternalSchema.MapiPREndDate;

		// Token: 0x04004D46 RID: 19782
		[Autoload]
		internal static readonly StorePropertyDefinition StartRecurTime = InternalSchema.StartRecurTime;

		// Token: 0x04004D47 RID: 19783
		[Autoload]
		internal static readonly StorePropertyDefinition StartRecurDate = InternalSchema.StartRecurDate;

		// Token: 0x04004D48 RID: 19784
		[Autoload]
		internal static readonly StorePropertyDefinition EndRecureDate = InternalSchema.EndRecurDate;

		// Token: 0x04004D49 RID: 19785
		[Autoload]
		internal static readonly StorePropertyDefinition EndRecurTime = InternalSchema.EndRecurTime;

		// Token: 0x04004D4A RID: 19786
		[Autoload]
		internal static readonly StorePropertyDefinition LidSingleInvite = InternalSchema.LidSingleInvite;

		// Token: 0x04004D4B RID: 19787
		[Autoload]
		internal static readonly StorePropertyDefinition LidDayInterval = InternalSchema.LidDayInterval;

		// Token: 0x04004D4C RID: 19788
		[Autoload]
		internal static readonly StorePropertyDefinition LidWeekInterval = InternalSchema.LidWeekInterval;

		// Token: 0x04004D4D RID: 19789
		[Autoload]
		internal static readonly StorePropertyDefinition LidMonthInterval = InternalSchema.LidMonthInterval;

		// Token: 0x04004D4E RID: 19790
		[Autoload]
		internal static readonly StorePropertyDefinition LidYearInterval = InternalSchema.LidYearInterval;

		// Token: 0x04004D4F RID: 19791
		[Autoload]
		internal static readonly StorePropertyDefinition LidDayOfWeekMask = InternalSchema.LidDayOfWeekMask;

		// Token: 0x04004D50 RID: 19792
		[Autoload]
		internal static readonly StorePropertyDefinition LidDayOfMonthMask = InternalSchema.LidDayOfMonthMask;

		// Token: 0x04004D51 RID: 19793
		[Autoload]
		internal static readonly StorePropertyDefinition LidMonthOfYearMask = InternalSchema.LidMonthOfYearMask;

		// Token: 0x04004D52 RID: 19794
		[Autoload]
		internal static readonly StorePropertyDefinition LidFirstDayOfWeek = InternalSchema.LidFirstDayOfWeek;

		// Token: 0x04004D53 RID: 19795
		[Autoload]
		internal static readonly StorePropertyDefinition LidRecurType = InternalSchema.LidRecurType;

		// Token: 0x04004D54 RID: 19796
		[Autoload]
		internal static readonly StorePropertyDefinition LidTimeZone = InternalSchema.LidTimeZone;

		// Token: 0x04004D55 RID: 19797
		[Autoload]
		public static readonly StorePropertyDefinition GlobalObjectId = InternalSchema.GlobalObjectId;

		// Token: 0x04004D56 RID: 19798
		[Autoload]
		internal static readonly StorePropertyDefinition MasterGlobalObjectId = InternalSchema.MasterGlobalObjectId;

		// Token: 0x04004D57 RID: 19799
		[Autoload]
		internal static readonly StorePropertyDefinition LidWhere = InternalSchema.LidWhere;

		// Token: 0x04004D58 RID: 19800
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentRecurrenceBlob = InternalSchema.AppointmentRecurrenceBlob;

		// Token: 0x04004D59 RID: 19801
		[Autoload]
		public static readonly StorePropertyDefinition IsException = InternalSchema.IsException;

		// Token: 0x04004D5A RID: 19802
		[Autoload]
		public static readonly StorePropertyDefinition PropertyChangeMetadataRaw = InternalSchema.PropertyChangeMetadataRaw;

		// Token: 0x04004D5B RID: 19803
		private static readonly PropertyRule[] MeetingMessageInstanceSchemaPropertyRules;

		// Token: 0x04004D5C RID: 19804
		private static MeetingMessageInstanceSchema instance;
	}
}
