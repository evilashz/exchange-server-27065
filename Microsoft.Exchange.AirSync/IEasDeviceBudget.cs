using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200028A RID: 650
	internal interface IEasDeviceBudget : IStandardBudget, IBudget, IDisposable
	{
		// Token: 0x060017EC RID: 6124
		void AddInteractiveCall();

		// Token: 0x060017ED RID: 6125
		void AddCall();

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060017EE RID: 6126
		float Percentage { get; }
	}
}
