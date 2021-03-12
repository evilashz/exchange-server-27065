using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C26 RID: 3110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarItemOccurrenceSchema : CalendarItemInstanceSchema
	{
		// Token: 0x06006E8D RID: 28301 RVA: 0x001DBBB4 File Offset: 0x001D9DB4
		private CalendarItemOccurrenceSchema()
		{
			base.AddDependencies(new Schema[]
			{
				AttachmentSchema.Instance
			});
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x06006E8E RID: 28302 RVA: 0x001DBBDD File Offset: 0x001D9DDD
		public new static CalendarItemOccurrenceSchema Instance
		{
			get
			{
				if (CalendarItemOccurrenceSchema.instance == null)
				{
					CalendarItemOccurrenceSchema.instance = new CalendarItemOccurrenceSchema();
				}
				return CalendarItemOccurrenceSchema.instance;
			}
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x06006E8F RID: 28303 RVA: 0x001DBBF8 File Offset: 0x001D9DF8
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					List<PropertyRule> list = base.PropertyRules.ToList<PropertyRule>();
					list.AddRange(CalendarItemOccurrenceSchema.CalendarItemOccurrencePropertyRules);
					list.Add(PropertyRuleLibrary.PropertyChangeMetadataTracking);
					this.propertyRulesCache = list.ToArray();
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x001DBC50 File Offset: 0x001D9E50
		// Note: this type is marked as 'beforefieldinit'.
		static CalendarItemOccurrenceSchema()
		{
			PropertyRule[] array = new PropertyRule[3];
			array[0] = PropertyRuleLibrary.DefaultInvitedForCalendarItem;
			array[1] = PropertyRuleLibrary.ExceptionalBodyFromBody;
			array[2] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(64671U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.DefaultIsExceptionFromItemClass,
				PropertyRuleLibrary.RecurrenceBlobToFlags,
				PropertyRuleLibrary.CalendarOriginatorId
			});
			CalendarItemOccurrenceSchema.CalendarItemOccurrencePropertyRules = array;
		}

		// Token: 0x040041F0 RID: 16880
		[Autoload]
		public static readonly StorePropertyDefinition IsSeriesCancelled = InternalSchema.IsSeriesCancelled;

		// Token: 0x040041F1 RID: 16881
		internal static readonly PropertyDefinition ExceptionReplaceTime = InternalSchema.ExceptionReplaceTime;

		// Token: 0x040041F2 RID: 16882
		private static readonly PropertyRule[] CalendarItemOccurrencePropertyRules;

		// Token: 0x040041F3 RID: 16883
		private static CalendarItemOccurrenceSchema instance;

		// Token: 0x040041F4 RID: 16884
		private ICollection<PropertyRule> propertyRulesCache;
	}
}
