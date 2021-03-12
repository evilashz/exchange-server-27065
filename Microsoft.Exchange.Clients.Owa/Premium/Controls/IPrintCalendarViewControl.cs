using System;
using System.IO;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000394 RID: 916
	public interface IPrintCalendarViewControl
	{
		// Token: 0x060022B9 RID: 8889
		void RenderView(TextWriter writer);

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060022BA RID: 8890
		string DateDescription { get; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060022BB RID: 8891
		string CalendarName { get; }

		// Token: 0x060022BC RID: 8892
		ExDateTime[] GetEffectiveDates();
	}
}
