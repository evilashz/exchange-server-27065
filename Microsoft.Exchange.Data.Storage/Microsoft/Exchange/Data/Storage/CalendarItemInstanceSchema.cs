using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C25 RID: 3109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItemInstanceSchema : CalendarItemBaseSchema
	{
		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06006E87 RID: 28295 RVA: 0x001DBA5A File Offset: 0x001D9C5A
		public new static CalendarItemInstanceSchema Instance
		{
			get
			{
				if (CalendarItemInstanceSchema.instance == null)
				{
					CalendarItemInstanceSchema.instance = new CalendarItemInstanceSchema();
				}
				return CalendarItemInstanceSchema.instance;
			}
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x001DBA72 File Offset: 0x001D9C72
		protected override void AddConstraints(List<StoreObjectConstraint> constraints)
		{
			base.AddConstraints(constraints);
			constraints.Add(CalendarItemInstanceSchema.StartTimeMustBeLessThanOrEqualToEndTimeConstraint);
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x06006E89 RID: 28297 RVA: 0x001DBA86 File Offset: 0x001D9C86
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					this.propertyRulesCache = CalendarItemInstanceSchema.CalendarItemInstancePropertyRules.Concat(base.PropertyRules);
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x001DBABC File Offset: 0x001D9CBC
		// Note: this type is marked as 'beforefieldinit'.
		static CalendarItemInstanceSchema()
		{
			PropertyRule[] array = new PropertyRule[1];
			array[0] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(52508U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.NativeStartTimeForCalendar,
				PropertyRuleLibrary.NativeEndTimeForCalendar,
				PropertyRuleLibrary.StartTimeEndTimeToDuration,
				PropertyRuleLibrary.NativeStartTimeToReminderTime,
				PropertyRuleLibrary.DefaultReminderNextTimeFromStartTimeAndOffset,
				PropertyRuleLibrary.ClipEndTimeForSingleMeeting,
				PropertyRuleLibrary.ClipStartTimeForSingleMeeting
			});
			CalendarItemInstanceSchema.CalendarItemInstancePropertyRules = array;
		}

		// Token: 0x040041E3 RID: 16867
		private static CalendarItemInstanceSchema instance = null;

		// Token: 0x040041E4 RID: 16868
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition EndTime = InternalSchema.EndTime;

		// Token: 0x040041E5 RID: 16869
		[Autoload]
		public static readonly StorePropertyDefinition PropertyChangeMetadata = InternalSchema.PropertyChangeMetadata;

		// Token: 0x040041E6 RID: 16870
		[Autoload]
		public static readonly StorePropertyDefinition PropertyChangeMetadataRaw = InternalSchema.PropertyChangeMetadataRaw;

		// Token: 0x040041E7 RID: 16871
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition StartTime = InternalSchema.StartTime;

		// Token: 0x040041E8 RID: 16872
		public static readonly StorePropertyDefinition StartWallClock = InternalSchema.StartWallClock;

		// Token: 0x040041E9 RID: 16873
		public static readonly StorePropertyDefinition EndWallClock = InternalSchema.EndWallClock;

		// Token: 0x040041EA RID: 16874
		public static readonly PropertyComparisonConstraint StartTimeMustBeLessThanOrEqualToEndTimeConstraint = new PropertyComparisonConstraint(InternalSchema.StartTime, InternalSchema.EndTime, ComparisonOperator.LessThanOrEqual);

		// Token: 0x040041EB RID: 16875
		[Autoload]
		internal static readonly StorePropertyDefinition MapiPRStartDate = InternalSchema.MapiPRStartDate;

		// Token: 0x040041EC RID: 16876
		[Autoload]
		internal static readonly StorePropertyDefinition MapiPREndDate = InternalSchema.MapiPREndDate;

		// Token: 0x040041ED RID: 16877
		private static readonly PropertyRule[] CalendarItemInstancePropertyRules;

		// Token: 0x040041EE RID: 16878
		private ICollection<PropertyRule> propertyRulesCache;
	}
}
