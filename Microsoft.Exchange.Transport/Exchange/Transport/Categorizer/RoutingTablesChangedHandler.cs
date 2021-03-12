using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000238 RID: 568
	// (Invoke) Token: 0x060018F6 RID: 6390
	internal delegate void RoutingTablesChangedHandler(IMailRouter eventSource, DateTime newRoutingTablesTimestamp, bool routesChanged);
}
