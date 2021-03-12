using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetBirthdayCalendarView : EntityCommand<IBirthdayEvents, IEnumerable<BirthdayEvent>>
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023CC File Offset: 0x000005CC
		public GetBirthdayCalendarView(IBirthdayEvents scope, ExDateTime rangeStart, ExDateTime rangeEnd)
		{
			if (rangeStart > rangeEnd)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Range start ({0}) must not be greater than range end ({1})", new object[]
				{
					rangeStart,
					rangeEnd
				}), "rangeStart");
			}
			this.Scope = scope;
			this.rangeStart = rangeStart;
			this.rangeEnd = rangeEnd;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002431 File Offset: 0x00000631
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.BirthdayEventDataProviderTracer;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002438 File Offset: 0x00000638
		protected override IEnumerable<BirthdayEvent> OnExecute()
		{
			return this.Scope.BirthdayEventDataProvider.GetBirthdayCalendarView(this.rangeStart, this.rangeEnd);
		}

		// Token: 0x04000006 RID: 6
		private readonly ExDateTime rangeStart;

		// Token: 0x04000007 RID: 7
		private readonly ExDateTime rangeEnd;
	}
}
