using System;
using System.Threading;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000059 RID: 89
	internal class FastManualResetEvent : IDisposable
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000EF0D File Offset: 0x0000D10D
		public FastManualResetEvent() : this(false)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000EF16 File Offset: 0x0000D116
		public FastManualResetEvent(bool initialSignaledState)
		{
			if (initialSignaledState)
			{
				this.Set();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000EF27 File Offset: 0x0000D127
		public bool IsSignaled
		{
			get
			{
				return this.isSignaled;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000EF30 File Offset: 0x0000D130
		public void Set()
		{
			this.isSignaled = true;
			lock (this)
			{
				if (this.manualResetEvent != null)
				{
					this.manualResetEvent.Set();
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000EF80 File Offset: 0x0000D180
		public void Reset()
		{
			this.isSignaled = false;
			lock (this)
			{
				if (this.manualResetEvent != null)
				{
					this.manualResetEvent.Reset();
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		public void WaitOne()
		{
			if (this.isSignaled)
			{
				return;
			}
			this.GetEvent().WaitOne();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EFE7 File Offset: 0x0000D1E7
		public bool WaitOne(TimeSpan timespan)
		{
			return this.isSignaled || this.GetEvent().WaitOne(timespan, false);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000F000 File Offset: 0x0000D200
		public ManualResetEvent GetEvent()
		{
			if (this.manualResetEvent == null)
			{
				lock (this)
				{
					if (this.manualResetEvent == null)
					{
						this.manualResetEvent = new ManualResetEvent(this.isSignaled);
					}
				}
			}
			return this.manualResetEvent;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000F05C File Offset: 0x0000D25C
		public void Dispose()
		{
			lock (this)
			{
				if (this.manualResetEvent != null)
				{
					IDisposable disposable = this.manualResetEvent;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.manualResetEvent = null;
				}
			}
		}

		// Token: 0x040001B9 RID: 441
		private bool isSignaled;

		// Token: 0x040001BA RID: 442
		private ManualResetEvent manualResetEvent;
	}
}
