using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009CF RID: 2511
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CalendarReminderTimeSpanConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x00182994 File Offset: 0x00180B94
		public TimeSpan MaxTimeSpan
		{
			get
			{
				return CalendarReminderTimeSpanConstraint.MaxCalendarReminderTimeSpan;
			}
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x0018299C File Offset: 0x00180B9C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			TimeSpan t = (TimeSpan)value;
			if (t < TimeSpan.Zero)
			{
				return new PropertyConstraintViolationError(ServerStrings.ErrorCalendarReminderNegative(value.ToString()), propertyDefinition, value, this);
			}
			if (t > CalendarReminderTimeSpanConstraint.MaxCalendarReminderTimeSpan)
			{
				return new PropertyConstraintViolationError(ServerStrings.ErrorCalendarReminderTooLarge(value.ToString()), propertyDefinition, value, this);
			}
			if (t.Seconds != 0 || t.Milliseconds != 0)
			{
				return new PropertyConstraintViolationError(ServerStrings.ErrorCalendarReminderNotMinutes(value.ToString()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x040032FE RID: 13054
		private static readonly TimeSpan MaxCalendarReminderTimeSpan = new TimeSpan(1059202, 23, 59, 0);
	}
}
