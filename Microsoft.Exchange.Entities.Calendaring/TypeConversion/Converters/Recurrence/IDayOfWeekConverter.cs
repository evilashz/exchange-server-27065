using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000083 RID: 131
	internal interface IDayOfWeekConverter : IConverter<DaysOfWeek, ISet<DayOfWeek>>, IConverter<ISet<DayOfWeek>, DaysOfWeek>
	{
	}
}
