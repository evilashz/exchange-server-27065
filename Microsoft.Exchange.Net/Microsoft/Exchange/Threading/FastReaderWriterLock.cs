using System;
using System.Threading;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B05 RID: 2821
	internal class FastReaderWriterLock
	{
		// Token: 0x06003CA9 RID: 15529 RVA: 0x0009E02C File Offset: 0x0009C22C
		public void AcquireReaderLock(int millisecondsTimeout)
		{
			this.myLock.Enter();
			while (this.owners < 0 || this.numWriteWaiters != 0U)
			{
				if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
				}
				else
				{
					this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
				}
			}
			this.owners++;
			this.myLock.Exit();
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x0009E09C File Offset: 0x0009C29C
		public void AcquireWriterLock(int millisecondsTimeout)
		{
			this.myLock.Enter();
			while (this.owners != 0)
			{
				if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else
				{
					this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
				}
			}
			this.owners = -1;
			this.myLock.Exit();
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x0009E0FC File Offset: 0x0009C2FC
		public void ReleaseReaderLock()
		{
			this.myLock.Enter();
			this.owners--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x0009E11D File Offset: 0x0009C31D
		public void ReleaseWriterLock()
		{
			this.myLock.Enter();
			this.owners = 0;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x0009E138 File Offset: 0x0009C338
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.myLock.Exit();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.myLock.Enter();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Close();
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x0009E180 File Offset: 0x0009C380
		private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			bool flag = false;
			this.myLock.Exit();
			try
			{
				if (!waitEvent.WaitOne(millisecondsTimeout, false))
				{
					throw new TimeoutException("ReaderWriterLock timeout expired");
				}
				flag = true;
			}
			finally
			{
				this.myLock.Enter();
				numWaiters -= 1U;
				if (!flag)
				{
					this.myLock.Exit();
				}
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.numWriteWaiters != 0U)
			{
				if (this.owners == 0)
				{
					this.myLock.Exit();
					this.writeEvent.Set();
					return;
				}
				this.myLock.Exit();
				return;
			}
			else
			{
				if (this.numReadWaiters == 0U)
				{
					this.myLock.Exit();
					return;
				}
				if (this.owners >= 0)
				{
					this.myLock.Exit();
					this.readEvent.Set();
					return;
				}
				this.myLock.Exit();
				return;
			}
		}

		// Token: 0x0400354E RID: 13646
		private SpinLock myLock;

		// Token: 0x0400354F RID: 13647
		private int owners;

		// Token: 0x04003550 RID: 13648
		private uint numWriteWaiters;

		// Token: 0x04003551 RID: 13649
		private uint numReadWaiters;

		// Token: 0x04003552 RID: 13650
		private EventWaitHandle writeEvent;

		// Token: 0x04003553 RID: 13651
		private EventWaitHandle readEvent;
	}
}
