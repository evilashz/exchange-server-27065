using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000773 RID: 1907
	internal interface IExceptionAnalyzer
	{
		// Token: 0x060025BB RID: 9659
		void Analyze(TestId currentTestStep, HttpWebRequestWrapper request, HttpWebResponseWrapper response, Exception exception, IResponseTracker responseTracker, Action<ScenarioException> trackingDelegate);

		// Token: 0x060025BC RID: 9660
		RequestTarget GetRequestTarget(HttpWebRequestWrapper request);

		// Token: 0x060025BD RID: 9661
		HttpWebResponseWrapperException VerifyResponse(HttpWebRequestWrapper request, HttpWebResponseWrapper response, CafeErrorPageValidationRules cafeErrorPageValidationRules);

		// Token: 0x060025BE RID: 9662
		ScenarioException GetExceptionForScenarioTimeout(TimeSpan maxAllowedTime, TimeSpan totalTime, ResponseTrackerItem item);

		// Token: 0x060025BF RID: 9663
		List<string> GetHostNames(RequestTarget requestTarget);
	}
}
