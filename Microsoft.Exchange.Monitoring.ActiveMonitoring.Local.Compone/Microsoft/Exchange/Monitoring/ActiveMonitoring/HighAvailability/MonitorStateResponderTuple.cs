using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000197 RID: 407
	internal struct MonitorStateResponderTuple
	{
		// Token: 0x040008FC RID: 2300
		public MonitorStateTransition MonitorState;

		// Token: 0x040008FD RID: 2301
		public ResponderDefinition Responder;
	}
}
