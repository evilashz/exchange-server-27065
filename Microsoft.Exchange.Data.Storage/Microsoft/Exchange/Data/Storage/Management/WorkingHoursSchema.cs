using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009F6 RID: 2550
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WorkingHoursSchema : UserConfigurationObjectSchema
	{
		// Token: 0x0400341D RID: 13341
		public static readonly SimplePropertyDefinition WorkDays = new SimplePropertyDefinition("WorkDays", ExchangeObjectVersion.Exchange2007, typeof(DaysOfWeek), PropertyDefinitionFlags.None, DaysOfWeek.Weekdays, DaysOfWeek.Weekdays, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400341E RID: 13342
		public static readonly SimplePropertyDefinition WorkingHoursStartTime = new SimplePropertyDefinition("WorkingHoursStartTime", ExchangeObjectVersion.Exchange2007, typeof(TimeSpan), PropertyDefinitionFlags.None, new TimeSpan(0, 8, 0, 0), new TimeSpan(0, 8, 0, 0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<TimeSpan>(TimeSpan.FromDays(0.0), TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L))
		});

		// Token: 0x0400341F RID: 13343
		public static readonly SimplePropertyDefinition WorkingHoursEndTime = new SimplePropertyDefinition("WorkingHoursEndTime", ExchangeObjectVersion.Exchange2007, typeof(TimeSpan), PropertyDefinitionFlags.None, new TimeSpan(0, 17, 0, 0), new TimeSpan(0, 17, 0, 0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<TimeSpan>(TimeSpan.FromDays(0.0), TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L))
		});

		// Token: 0x04003420 RID: 13344
		public static readonly SimplePropertyDefinition WorkingHoursTimeZone = new SimplePropertyDefinition("WorkingHoursTimeZone", ExchangeObjectVersion.Exchange2010, typeof(ExTimeZoneValue), PropertyDefinitionFlags.None, ExTimeZoneValue.Parse("Pacific Standard Time"), ExTimeZoneValue.Parse("Pacific Standard Time"), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
