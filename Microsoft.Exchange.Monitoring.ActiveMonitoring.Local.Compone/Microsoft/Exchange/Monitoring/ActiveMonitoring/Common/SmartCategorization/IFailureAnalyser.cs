using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.SmartCategorization
{
	// Token: 0x020000FF RID: 255
	internal interface IFailureAnalyser
	{
		// Token: 0x060007C5 RID: 1989
		FailureDetails Analyse(RequestContext requestContext, RequestFailureContext requestFailureContext);
	}
}
