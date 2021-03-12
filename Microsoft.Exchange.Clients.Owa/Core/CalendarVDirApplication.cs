using System;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200029C RID: 668
	internal sealed class CalendarVDirApplication : OwaApplicationBase
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00095E37 File Offset: 0x00094037
		internal override OWAVDirType OwaVDirType
		{
			get
			{
				return OWAVDirType.Calendar;
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00095E3A File Offset: 0x0009403A
		protected override void ExecuteApplicationSpecificStart()
		{
			ExWatson.Register("E12IIS");
			OwaEventRegistry.RegisterHandler(typeof(ProxyEventHandler));
			CalendarViewEventHandler.Register();
			DatePickerEventHandler.Register();
			PrintCalendarEventHandler.Register();
		}
	}
}
