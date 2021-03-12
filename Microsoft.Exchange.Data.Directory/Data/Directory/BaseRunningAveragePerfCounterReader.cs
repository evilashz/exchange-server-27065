using System;
using System.Timers;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A1 RID: 2465
	internal abstract class BaseRunningAveragePerfCounterReader : DisposeTrackableBase, IPerfCounterReader
	{
		// Token: 0x060071AC RID: 29100 RVA: 0x0017885C File Offset: 0x00176A5C
		public BaseRunningAveragePerfCounterReader(ushort numberOfSamples, uint intervalTime)
		{
			if (intervalTime < 100U)
			{
				throw new ArgumentException("intervalTime must be greater than 100 milliseconds", "intervalTime");
			}
			this.cachedValue = new RunningAverageFloat(numberOfSamples);
			this.AcquireCounter();
			this.timer = new Timer(intervalTime);
			this.timer.Elapsed += this.HandleTimer;
			this.timer.Start();
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x001788D3 File Offset: 0x00176AD3
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.timer.Dispose();
			}
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x001788E3 File Offset: 0x00176AE3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BaseRunningAveragePerfCounterReader>(this);
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x001788EB File Offset: 0x00176AEB
		public float GetValue()
		{
			return this.cachedValue.Value;
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x001788F8 File Offset: 0x00176AF8
		protected virtual void HandleTimer(object sender, ElapsedEventArgs e)
		{
			if (!this.BeforeRead())
			{
				return;
			}
			lock (this.lockObject)
			{
				float newValue = this.ReadCounter();
				this.cachedValue.Update(newValue);
			}
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x00178950 File Offset: 0x00176B50
		protected virtual bool BeforeRead()
		{
			return true;
		}

		// Token: 0x060071B2 RID: 29106
		protected abstract bool AcquireCounter();

		// Token: 0x060071B3 RID: 29107
		protected abstract float ReadCounter();

		// Token: 0x040049A6 RID: 18854
		private RunningAverageFloat cachedValue;

		// Token: 0x040049A7 RID: 18855
		private Timer timer;

		// Token: 0x040049A8 RID: 18856
		private object lockObject = new object();
	}
}
