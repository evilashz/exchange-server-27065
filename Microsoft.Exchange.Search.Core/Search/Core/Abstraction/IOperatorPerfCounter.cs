using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000032 RID: 50
	internal interface IOperatorPerfCounter
	{
		// Token: 0x06000116 RID: 278
		void IncrementPerfcounter(OperatorPerformanceCounter performanceCounter);

		// Token: 0x06000117 RID: 279
		void IncrementPerfcounterBy(OperatorPerformanceCounter performanceCounter, long value);

		// Token: 0x06000118 RID: 280
		void DecrementPerfcounter(OperatorPerformanceCounter performanceCounter);
	}
}
