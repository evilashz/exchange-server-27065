using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.SmartCategorization
{
	// Token: 0x02000100 RID: 256
	internal abstract class FailureAnalyserBase : IFailureAnalyser
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0002EE48 File Offset: 0x0002D048
		public FailureDetails Analyse(RequestContext requestContext, RequestFailureContext requestFailureContext)
		{
			if (requestFailureContext == null)
			{
				throw new ArgumentNullException("requestFailureContext");
			}
			switch (requestFailureContext.FailurePoint)
			{
			case RequestFailureContext.RequestFailurePoint.FrontEnd:
				return this.AnalyseCafeFailure(requestFailureContext);
			case RequestFailureContext.RequestFailurePoint.BackEnd:
				return this.AnalyseBackendFailure(requestFailureContext);
			default:
				return new FailureDetails();
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002EE91 File Offset: 0x0002D091
		protected virtual FailureDetails AnalyseCafeFailure(RequestFailureContext requestFailureContext)
		{
			return new FailureDetails();
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002EE98 File Offset: 0x0002D098
		protected virtual FailureDetails AnalyseBackendFailure(RequestFailureContext requestFailureContext)
		{
			return new FailureDetails();
		}
	}
}
