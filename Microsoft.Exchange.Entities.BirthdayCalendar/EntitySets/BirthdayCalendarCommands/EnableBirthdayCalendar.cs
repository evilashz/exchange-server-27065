using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayCalendarCommands
{
	// Token: 0x02000014 RID: 20
	internal class EnableBirthdayCalendar : EntityCommand<BirthdayCalendars, BirthdayCalendar>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000035F6 File Offset: 0x000017F6
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.EnableBirthdayCalendarTracer;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000035FD File Offset: 0x000017FD
		protected override BirthdayCalendar OnExecute()
		{
			throw new NotImplementedException();
		}
	}
}
