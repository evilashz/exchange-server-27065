using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000EB RID: 235
	public interface ITimerCounter : IDisposable
	{
		// Token: 0x060006B1 RID: 1713
		void Start();

		// Token: 0x060006B2 RID: 1714
		long Stop();
	}
}
