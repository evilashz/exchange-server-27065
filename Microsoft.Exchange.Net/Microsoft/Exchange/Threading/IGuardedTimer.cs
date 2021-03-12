using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B06 RID: 2822
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IGuardedTimer : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003CB0 RID: 15536
		bool Change(int dueTime, int period);

		// Token: 0x06003CB1 RID: 15537
		bool Change(long dueTime, long period);

		// Token: 0x06003CB2 RID: 15538
		bool Change(TimeSpan dueTime, TimeSpan period);

		// Token: 0x06003CB3 RID: 15539
		void Pause();

		// Token: 0x06003CB4 RID: 15540
		void Continue();

		// Token: 0x06003CB5 RID: 15541
		void Continue(TimeSpan dueTime, TimeSpan period);

		// Token: 0x06003CB6 RID: 15542
		void Dispose(bool wait);
	}
}
