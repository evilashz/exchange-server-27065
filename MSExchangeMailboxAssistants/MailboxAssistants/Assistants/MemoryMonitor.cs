using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200001B RID: 27
	internal sealed class MemoryMonitor
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000542C File Offset: 0x0000362C
		public void Start(ulong threshold, uint numberOfSamples, TimeSpan samplingDelay, TimeSpan samplingPeriod, MemoryMonitor.OnThresholdReachedDelegate onThresholdReached)
		{
			this.threshold = threshold * 1048576UL;
			this.averagePrivateBytesInUse = new MemoryMonitor.MovingAverage(numberOfSamples);
			this.onThresholdReached = onThresholdReached;
			this.samplingTimer = new Timer(new TimerCallback(this.SampleMemoryUsage), null, samplingDelay, samplingPeriod);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000546C File Offset: 0x0000366C
		public void Stop()
		{
			lock (this.locker)
			{
				this.samplingTimer.Dispose();
				this.samplingTimer = null;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000054B8 File Offset: 0x000036B8
		private void SampleMemoryUsage(object state)
		{
			ulong privateBytesUsage = MemoryMonitor.GetPrivateBytesUsage();
			if (privateBytesUsage > this.threshold)
			{
				lock (this.locker)
				{
					privateBytesUsage = MemoryMonitor.GetPrivateBytesUsage();
					if (privateBytesUsage > this.threshold)
					{
						GC.Collect();
					}
				}
				if (privateBytesUsage > this.threshold)
				{
					privateBytesUsage = MemoryMonitor.GetPrivateBytesUsage();
				}
			}
			if (privateBytesUsage == 0UL)
			{
				return;
			}
			ulong num = this.averagePrivateBytesInUse.Update(privateBytesUsage);
			if (num > this.threshold)
			{
				lock (this.locker)
				{
					if (this.samplingTimer != null)
					{
						this.samplingTimer.Dispose();
						this.samplingTimer = null;
						this.onThresholdReached(num);
					}
				}
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005594 File Offset: 0x00003794
		private static ulong GetPrivateBytesUsage()
		{
			ulong result;
			using (SafeProcessHandle currentProcess = NativeMethods.GetCurrentProcess())
			{
				NativeMethods.ProcessMemoryCounterEx processMemoryCounterEx;
				if (NativeMethods.GetProcessMemoryInfo(currentProcess, out processMemoryCounterEx, NativeMethods.ProcessMemoryCounterEx.Size))
				{
					result = processMemoryCounterEx.privateUsage.ToUInt64();
				}
				else
				{
					ExTraceGlobals.AssistantBaseTracer.TraceError<int>(0L, "Failed to GetProcessMemoryInfo. Error: {0}", Marshal.GetLastWin32Error());
					result = 0UL;
				}
			}
			return result;
		}

		// Token: 0x040000FF RID: 255
		internal const ulong MEGABYTE = 1048576UL;

		// Token: 0x04000100 RID: 256
		private object locker = new object();

		// Token: 0x04000101 RID: 257
		private Timer samplingTimer;

		// Token: 0x04000102 RID: 258
		private MemoryMonitor.MovingAverage averagePrivateBytesInUse;

		// Token: 0x04000103 RID: 259
		private MemoryMonitor.OnThresholdReachedDelegate onThresholdReached;

		// Token: 0x04000104 RID: 260
		private ulong threshold;

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x060000D7 RID: 215
		public delegate void OnThresholdReachedDelegate(ulong privateBytesInUse);

		// Token: 0x0200001D RID: 29
		private sealed class MovingAverage
		{
			// Token: 0x060000DA RID: 218 RVA: 0x0000560F File Offset: 0x0000380F
			public MovingAverage(uint maxNumberOfSamples)
			{
				this.maxNumberOfSamples = maxNumberOfSamples;
				this.samples = new Queue<ulong>((int)this.maxNumberOfSamples);
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00005630 File Offset: 0x00003830
			public ulong Update(ulong sample)
			{
				ulong result;
				lock (this.samples)
				{
					this.samples.Enqueue(sample);
					while ((long)this.samples.Count > (long)((ulong)this.maxNumberOfSamples))
					{
						this.samplesTotal -= this.samples.Dequeue();
					}
					this.samplesTotal += sample;
					result = this.samplesTotal / (ulong)((long)this.samples.Count);
				}
				return result;
			}

			// Token: 0x04000105 RID: 261
			private uint maxNumberOfSamples;

			// Token: 0x04000106 RID: 262
			private ulong samplesTotal;

			// Token: 0x04000107 RID: 263
			private Queue<ulong> samples;
		}
	}
}
