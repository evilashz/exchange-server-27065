using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002A1 RID: 673
	internal class CalendarVDirRequestEventInspector : RequestEventInspectorBase
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x00096674 File Offset: 0x00094874
		internal override void Init()
		{
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00096676 File Offset: 0x00094876
		internal override void OnBeginRequest(object sender, EventArgs e, out bool stopExecution)
		{
			stopExecution = false;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0009667B File Offset: 0x0009487B
		internal override void OnPostAuthorizeRequest(object sender, EventArgs e)
		{
			CalendarVDirRequestDispatcher.DispatchRequest(OwaContext.Current);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00096687 File Offset: 0x00094887
		internal override void OnEndRequest(OwaContext owaContext)
		{
		}
	}
}
