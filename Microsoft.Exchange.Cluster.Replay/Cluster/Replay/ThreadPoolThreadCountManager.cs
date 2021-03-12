using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000162 RID: 354
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThreadPoolThreadCountManager : IThreadPoolThreadCountManager
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0003D6C4 File Offset: 0x0003B8C4
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ReplayManagerTracer;
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003D6CB File Offset: 0x0003B8CB
		public ThreadPoolThreadCountManager()
		{
			this.m_baselineThreadCount = new ThreadPoolThreadCount();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003D6EC File Offset: 0x0003B8EC
		public static int GetProcessThreadCount()
		{
			int count;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				count = currentProcess.Threads.Count;
			}
			return count;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003D728 File Offset: 0x0003B928
		public bool SetMinThreads(int workerThreads, int? completionPortThreads, bool force)
		{
			return this.SetMinThreadsInternal(workerThreads, completionPortThreads, force, true);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003D734 File Offset: 0x0003B934
		public bool IncrementMinThreadsBy(int incWorkerThreadsBy, int? incCompletionPortThreadsBy)
		{
			bool result;
			lock (this.m_locker)
			{
				ThreadPoolThreadCount threadPoolThreadCount = new ThreadPoolThreadCount();
				ThreadPoolThreadCountManager.Tracer.TraceDebug<int, int?>((long)this.GetHashCode(), "IncrementMinThreadsBy() called: incWorkerThreadsBy={0}, incCompletionPortThreadsBy={1}", incWorkerThreadsBy, incCompletionPortThreadsBy);
				result = this.SetMinThreadsInternal(threadPoolThreadCount.MinWorkerThreads + incWorkerThreadsBy, (incCompletionPortThreadsBy == null) ? null : (threadPoolThreadCount.MinCompletionPortThreads + incCompletionPortThreadsBy), true, false);
			}
			return result;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		public bool DecrementMinThreadsBy(int decWorkerThreadsBy, int? decCompletionPortThreadsBy)
		{
			return this.IncrementMinThreadsBy(-1 * decWorkerThreadsBy, (decCompletionPortThreadsBy == null) ? null : (-1 * decCompletionPortThreadsBy));
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003D834 File Offset: 0x0003BA34
		public bool Reset(bool force)
		{
			bool result;
			lock (this.m_locker)
			{
				bool flag2 = this.ResetInternal(force, this.m_baselineThreadCount);
				if (force)
				{
					this.m_onlyForceAllowed = false;
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003D88C File Offset: 0x0003BA8C
		private bool SetMinThreadsInternal(int workerThreads, int? completionPortThreads, bool force, bool setBaseline)
		{
			bool result;
			lock (this.m_locker)
			{
				bool flag2 = true;
				ThreadPoolThreadCount threadPoolThreadCount = null;
				if (!this.m_onlyForceAllowed || force)
				{
					this.m_onlyForceAllowed = force;
					threadPoolThreadCount = new ThreadPoolThreadCount();
					int num = completionPortThreads ?? threadPoolThreadCount.MinCompletionPortThreads;
					int processorCount = Environment.ProcessorCount;
					int num2 = Math.Max(workerThreads, processorCount);
					num = Math.Max(num, processorCount);
					if (num2 != threadPoolThreadCount.MinWorkerThreads || num != threadPoolThreadCount.MinCompletionPortThreads)
					{
						ThreadPoolThreadCountManager.Tracer.TraceDebug((long)this.GetHashCode(), "SetMinThreads() called: workerThreads={0}, completionPortThreads={1}, force={2}, setBaseline={3}", new object[]
						{
							workerThreads,
							completionPortThreads,
							force,
							setBaseline
						});
						bool flag3 = false;
						int num3 = num2;
						int num4 = num;
						if (num2 > threadPoolThreadCount.MaxWorkerThreads)
						{
							flag3 = true;
							num3 = 2 * num2;
						}
						if (num > threadPoolThreadCount.MaxCompletionPortThreads)
						{
							flag3 = true;
							num4 = 2 * num;
						}
						if (flag3)
						{
							if (!ThreadPool.SetMaxThreads(num3, num4))
							{
								ThreadPoolThreadCountManager.Tracer.TraceError<int, int>((long)this.GetHashCode(), "SetMinThreads(): ThreadPool max threads *FAILED* to increase to: workerThreads,completionPortThreads = {0},{1}", num3, num4);
								goto IL_1A6;
							}
							ThreadPoolThreadCountManager.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "SetMinThreads(): ThreadPool max threads increased to: workerThreads,completionPortThreads = {0},{1}", num3, num4);
						}
						if (!ThreadPool.SetMinThreads(num2, num))
						{
							ThreadPoolThreadCountManager.Tracer.TraceError<int, int>((long)this.GetHashCode(), "SetMinThreads(): ThreadPool min threads *FAILED* to increase to: workerThreads,completionPortThreads = {0},{1}", num2, num);
							goto IL_1A6;
						}
						ThreadPoolThreadCountManager.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "SetMinThreads(): ThreadPool min threads increased to: workerThreads,completionPortThreads = {0},{1}", num2, num);
					}
					if (setBaseline)
					{
						threadPoolThreadCount = new ThreadPoolThreadCount();
						this.m_baselineThreadCount = threadPoolThreadCount;
					}
					return true;
				}
				flag2 = false;
				ThreadPoolThreadCountManager.Tracer.TraceDebug((long)this.GetHashCode(), "SetMinThreads() bailing since m_onlyForceAllowed == true, but it was called with force='false'");
				IL_1A6:
				if (flag2)
				{
					this.ResetInternal(force, threadPoolThreadCount);
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003DA7C File Offset: 0x0003BC7C
		private bool ResetInternal(bool force, ThreadPoolThreadCount resetTo)
		{
			bool result = true;
			if (this.m_onlyForceAllowed && !force)
			{
				result = false;
			}
			else
			{
				if (!ThreadPool.SetMinThreads(resetTo.MinWorkerThreads, resetTo.MinCompletionPortThreads))
				{
					result = false;
				}
				if (!ThreadPool.SetMaxThreads(resetTo.MaxWorkerThreads, resetTo.MaxCompletionPortThreads))
				{
					result = false;
				}
				ThreadPoolThreadCountManager.Tracer.TraceDebug<ThreadPoolThreadCount>((long)this.GetHashCode(), "ResetInternal(): ThreadPool set back to: {0}", resetTo);
			}
			return result;
		}

		// Token: 0x040005D8 RID: 1496
		private bool m_onlyForceAllowed;

		// Token: 0x040005D9 RID: 1497
		private ThreadPoolThreadCount m_baselineThreadCount;

		// Token: 0x040005DA RID: 1498
		private object m_locker = new object();
	}
}
