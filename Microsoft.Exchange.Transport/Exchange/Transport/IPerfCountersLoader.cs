using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200005C RID: 92
	internal interface IPerfCountersLoader
	{
		// Token: 0x060002F3 RID: 755
		void AddCounterToGetExchangeDiagnostics(Type counterType, string counterName);
	}
}
