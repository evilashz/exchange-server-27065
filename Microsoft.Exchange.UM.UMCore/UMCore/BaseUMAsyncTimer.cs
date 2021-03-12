using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000030 RID: 48
	internal abstract class BaseUMAsyncTimer : DisposableBase
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00009AF5 File Offset: 0x00007CF5
		internal BaseUMAsyncTimer(BaseUMCallSession session, BaseUMAsyncTimer.UMTimerCallback callback, int dueTime)
		{
			this.callback = callback;
			this.timer = new Timer(new TimerCallback(this.TimerExpired), session, dueTime * 1000, -1);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009B25 File Offset: 0x00007D25
		internal bool IsActive
		{
			get
			{
				return this.timer != null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009B32 File Offset: 0x00007D32
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00009B3A File Offset: 0x00007D3A
		internal BaseUMAsyncTimer.UMTimerCallback Callback
		{
			get
			{
				return this.callback;
			}
			set
			{
				this.callback = value;
			}
		}

		// Token: 0x060001EE RID: 494
		internal abstract void TimerExpired(object state);

		// Token: 0x060001EF RID: 495 RVA: 0x00009B43 File Offset: 0x00007D43
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009B5A File Offset: 0x00007D5A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BaseUMAsyncTimer>(this);
		}

		// Token: 0x0400009E RID: 158
		private Timer timer;

		// Token: 0x0400009F RID: 159
		private BaseUMAsyncTimer.UMTimerCallback callback;

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060001F2 RID: 498
		internal delegate void UMTimerCallback(BaseUMCallSession session);
	}
}
