using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C27 RID: 3111
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarItemSchema : CalendarItemInstanceSchema
	{
		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x06006E92 RID: 28306 RVA: 0x001DBCD8 File Offset: 0x001D9ED8
		public new static CalendarItemSchema Instance
		{
			get
			{
				if (CalendarItemSchema.instance == null)
				{
					CalendarItemSchema.instance = new CalendarItemSchema();
				}
				return CalendarItemSchema.instance;
			}
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x06006E93 RID: 28307 RVA: 0x001DBCF0 File Offset: 0x001D9EF0
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					List<PropertyRule> list = base.PropertyRules.Concat(CalendarItemSchema.CalendarItemPropertyRules).ToList<PropertyRule>();
					list.AddRange(CalendarItemSchema.PropertyChangesTrackingMetadataRules);
					this.propertyRulesCache = new SequenceCompositePropertyRule[]
					{
						new SequenceCompositePropertyRule(string.Empty, null, list.ToArray())
					};
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x001DBD4E File Offset: 0x001D9F4E
		protected override void AddConstraints(List<StoreObjectConstraint> constraints)
		{
			base.AddConstraints(constraints);
			constraints.Add(new RecurrenceBlobConstraint());
		}

		// Token: 0x06006E95 RID: 28309 RVA: 0x001DBD62 File Offset: 0x001D9F62
		protected override void CoreObjectUpdateAllAttachmentsHidden(CoreItem coreItem)
		{
			CalendarItem.CoreObjectUpdateAllAttachmentsHidden(coreItem);
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x001DBD7C File Offset: 0x001D9F7C
		// Note: this type is marked as 'beforefieldinit'.
		static CalendarItemSchema()
		{
			PropertyRule[] array = new PropertyRule[4];
			array[0] = PropertyRuleLibrary.DefaultOwnerAppointmentId;
			array[1] = PropertyRuleLibrary.DefaultRecurrencePattern;
			array[2] = PropertyRuleLibrary.DefaultIsAllDayEvent;
			array[3] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(40095U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.DefaultInvitedForCalendarItem,
				PropertyRuleLibrary.DefaultIsExceptionFromItemClass,
				PropertyRuleLibrary.DefaultAppointmentStateFromItemClass,
				PropertyRuleLibrary.RecurrenceBlobToFlags,
				PropertyRuleLibrary.RecurringTimeZone,
				PropertyRuleLibrary.GlobalObjectIdOnRecurringMaster,
				PropertyRuleLibrary.DefaultCleanGlobalObjectIdFromGlobalObjectId,
				PropertyRuleLibrary.CalendarOriginatorId,
				PropertyRuleLibrary.RemoveAppointmentMadeRecurrentFromSeriesRule,
				PropertyRuleLibrary.DefaultOrganizerForAppointments,
				PropertyRuleLibrary.CalendarViewProperties
			});
			CalendarItemSchema.CalendarItemPropertyRules = array;
			CalendarItemSchema.PropertyChangesTrackingMetadataRules = new PropertyRule[]
			{
				PropertyRuleLibrary.MasterPropertyOverrideProtection,
				PropertyRuleLibrary.PropertyChangeMetadataTracking,
				PropertyRuleLibrary.CleanupSeriesOperationFlagsProperty
			};
		}

		// Token: 0x040041F6 RID: 16886
		[Autoload]
		public static readonly StorePropertyDefinition LastExecutedCalendarInteropAction = InternalSchema.LastExecutedCalendarInteropAction;

		// Token: 0x040041F7 RID: 16887
		[Autoload]
		public static readonly StorePropertyDefinition InstanceCreationIndex = InternalSchema.InstanceCreationIndex;

		// Token: 0x040041F8 RID: 16888
		public static readonly StorePropertyDefinition HasExceptionalInboxReminders = InternalSchema.HasExceptionalInboxReminders;

		// Token: 0x040041F9 RID: 16889
		[Autoload]
		public static readonly StorePropertyDefinition SeriesMasterId = InternalSchema.SeriesMasterId;

		// Token: 0x040041FA RID: 16890
		[Autoload]
		public static readonly StorePropertyDefinition PropertyChangeMetadataProcessingFlags = InternalSchema.PropertyChangeMetadataProcessingFlags;

		// Token: 0x040041FB RID: 16891
		[Autoload]
		public static readonly StorePropertyDefinition ViewStartTime = InternalSchema.ViewStartTime;

		// Token: 0x040041FC RID: 16892
		[Autoload]
		public static readonly StorePropertyDefinition ViewEndTime = InternalSchema.ViewEndTime;

		// Token: 0x040041FD RID: 16893
		private static readonly PropertyRule[] CalendarItemPropertyRules;

		// Token: 0x040041FE RID: 16894
		private static readonly PropertyRule[] PropertyChangesTrackingMetadataRules;

		// Token: 0x040041FF RID: 16895
		private static CalendarItemSchema instance;

		// Token: 0x04004200 RID: 16896
		private ICollection<PropertyRule> propertyRulesCache;
	}
}
