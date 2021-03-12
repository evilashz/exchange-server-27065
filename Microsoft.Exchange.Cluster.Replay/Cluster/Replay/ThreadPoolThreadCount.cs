using System;
using System.Threading;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000161 RID: 353
	internal class ThreadPoolThreadCount
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0003D50C File Offset: 0x0003B70C
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x0003D514 File Offset: 0x0003B714
		public int MinWorkerThreads { get; private set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0003D51D File Offset: 0x0003B71D
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0003D525 File Offset: 0x0003B725
		public int MinCompletionPortThreads { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0003D52E File Offset: 0x0003B72E
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0003D536 File Offset: 0x0003B736
		public int MaxWorkerThreads { get; private set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0003D53F File Offset: 0x0003B73F
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0003D547 File Offset: 0x0003B747
		public int MaxCompletionPortThreads { get; private set; }

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003D550 File Offset: 0x0003B750
		public ThreadPoolThreadCount()
		{
			int minWorkerThreads;
			int minCompletionPortThreads;
			int maxWorkerThreads;
			int maxCompletionPortThreads;
			this.GetMinMaxThreads(out minWorkerThreads, out minCompletionPortThreads, out maxWorkerThreads, out maxCompletionPortThreads);
			this.MinWorkerThreads = minWorkerThreads;
			this.MinCompletionPortThreads = minCompletionPortThreads;
			this.MaxWorkerThreads = maxWorkerThreads;
			this.MaxCompletionPortThreads = maxCompletionPortThreads;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003D58D File Offset: 0x0003B78D
		public ThreadPoolThreadCount(int minw, int minc, int maxw, int maxc)
		{
			this.MinWorkerThreads = minw;
			this.MinCompletionPortThreads = minc;
			this.MaxWorkerThreads = maxw;
			this.MaxCompletionPortThreads = maxc;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003D5B2 File Offset: 0x0003B7B2
		private void GetMinMaxThreads(out int minWorkerThreads, out int minCompletionPortThreads, out int maxWorkerThreads, out int maxCompletionPortThreads)
		{
			ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
			ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003D5C4 File Offset: 0x0003B7C4
		public override string ToString()
		{
			return string.Format("[MinWorkerThreads={0},MinCompletionPortThreads={1},MaxWorkerThreads={2},MaxCompletionPortThreads={3}]", new object[]
			{
				this.MinWorkerThreads,
				this.MinCompletionPortThreads,
				this.MaxWorkerThreads,
				this.MaxCompletionPortThreads
			});
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003D61B File Offset: 0x0003B81B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003D624 File Offset: 0x0003B824
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ThreadPoolThreadCount threadPoolThreadCount = obj as ThreadPoolThreadCount;
			return !(threadPoolThreadCount == null) && this.Equals(threadPoolThreadCount);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003D650 File Offset: 0x0003B850
		public bool Equals(ThreadPoolThreadCount other)
		{
			return other != null && (this.MinWorkerThreads == other.MinWorkerThreads && this.MinCompletionPortThreads == other.MinCompletionPortThreads && this.MaxWorkerThreads == other.MaxWorkerThreads) && this.MaxCompletionPortThreads == other.MaxCompletionPortThreads;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003D69C File Offset: 0x0003B89C
		public static bool operator ==(ThreadPoolThreadCount tc1, ThreadPoolThreadCount tc2)
		{
			return object.ReferenceEquals(tc1, tc2) || (tc1 != null && tc2 != null && tc1.Equals(tc2));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		public static bool operator !=(ThreadPoolThreadCount tc1, ThreadPoolThreadCount tc2)
		{
			return !(tc1 == tc2);
		}
	}
}
