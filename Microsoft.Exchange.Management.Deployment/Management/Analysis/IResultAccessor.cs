using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000041 RID: 65
	internal interface IResultAccessor
	{
		// Token: 0x060001B7 RID: 439
		void SetSource(AnalysisMember source);

		// Token: 0x060001B8 RID: 440
		void SetParent(Result parent);

		// Token: 0x060001B9 RID: 441
		void SetStartTime(ExDateTime startTime);

		// Token: 0x060001BA RID: 442
		void SetStopTime(ExDateTime stopTime);
	}
}
