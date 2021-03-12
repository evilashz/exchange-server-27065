using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000005 RID: 5
	internal interface ICountAndRatePairCounter
	{
		// Token: 0x06000026 RID: 38
		void AddValue(long value);

		// Token: 0x06000027 RID: 39
		void UpdateAverage();

		// Token: 0x06000028 RID: 40
		void GetDiagnosticInfo(XElement parent);
	}
}
