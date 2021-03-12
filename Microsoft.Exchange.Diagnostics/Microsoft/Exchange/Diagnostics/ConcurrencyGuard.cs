using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000116 RID: 278
	public class ConcurrencyGuard : IConcurrencyGuard
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00020826 File Offset: 0x0001EA26
		public string GuardName
		{
			get
			{
				return this.guardName;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002082E File Offset: 0x0001EA2E
		public int MaxConcurrency
		{
			get
			{
				return this.maxConcurrency;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00020836 File Offset: 0x0001EA36
		public bool TrainingMode
		{
			get
			{
				return this.trainingMode;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00020840 File Offset: 0x0001EA40
		public ConcurrencyGuard(string guardName, int maxConcurrency, bool useTrainingMode = false, Action<ConcurrencyGuard, string, object> onIncrementDelegate = null, Action<ConcurrencyGuard, string, object> onDecrementDelegate = null, Action<ConcurrencyGuard, string, object> onNearThresholdDelegate = null, Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException> onRejectDelegate = null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("guardName", guardName);
			this.guardName = guardName;
			this.maxConcurrency = maxConcurrency;
			this.concurrencyWarningThreshold = maxConcurrency - maxConcurrency / 20;
			this.onDecrementDelegate = onDecrementDelegate;
			this.onIncrementDelegate = onIncrementDelegate;
			this.onNearThresholdDelegate = onNearThresholdDelegate;
			this.onRejectDelegate = onRejectDelegate;
			this.trainingMode = useTrainingMode;
			this.buckets.Add("_Default", new ConcurrencyGuard.RefCount());
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000208CB File Offset: 0x0001EACB
		public static string FormatGuardBucketName(IConcurrencyGuard guard, string bucketName)
		{
			ArgumentValidator.ThrowIfNull("guard", guard);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("bucketName", bucketName);
			return guard.GuardName + "(\"" + bucketName + "\")";
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000208F9 File Offset: 0x0001EAF9
		public long GetCurrentValue()
		{
			return this.GetCurrentValue("_Default");
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00020908 File Offset: 0x0001EB08
		public long GetCurrentValue(string bucketName)
		{
			ConcurrencyGuard.RefCount refCount;
			if (this.TryGetRefCount(bucketName, out refCount))
			{
				return refCount.Count;
			}
			return 0L;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002092C File Offset: 0x0001EB2C
		private bool TryGetRefCount(string bucketName, out ConcurrencyGuard.RefCount refCount)
		{
			try
			{
				this.instanceLock.EnterReadLock();
				this.buckets.TryGetValue(bucketName, out refCount);
			}
			finally
			{
				try
				{
					this.instanceLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return refCount != null;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002098C File Offset: 0x0001EB8C
		public long Increment(object stateObject = null)
		{
			return this.Increment("_Default", null);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002099C File Offset: 0x0001EB9C
		public long Increment(string bucketName, object stateObject = null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("bucketName", bucketName);
			ConcurrencyGuard.RefCount refCount;
			if (!this.TryGetRefCount(bucketName, out refCount))
			{
				try
				{
					this.instanceLock.EnterWriteLock();
					if (!this.buckets.TryGetValue(bucketName, out refCount) && refCount == null)
					{
						refCount = new ConcurrencyGuard.RefCount();
						this.buckets.Add(bucketName, refCount);
					}
				}
				finally
				{
					try
					{
						this.instanceLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			long count = refCount.Count;
			ExTraceGlobals.ConcurrencyGuardTracer.TraceDebug<string, long, int>((long)this.GetHashCode(), "[ConcurrencyGuard::Increment]: Guard {0} is at {1}/{2}.", ConcurrencyGuard.FormatGuardBucketName(this, bucketName), count, this.MaxConcurrency);
			if (this.onNearThresholdDelegate != null && count > (long)this.concurrencyWarningThreshold)
			{
				this.onNearThresholdDelegate(this, bucketName, stateObject);
			}
			if (count + 1L > (long)this.MaxConcurrency)
			{
				ExTraceGlobals.ConcurrencyGuardTracer.TraceError<string, long, int>((long)this.GetHashCode(), "[ConcurrencyGuard::Increment]: Guard {0} is at concurrency limit {1}/{2} - REJECTING.", ConcurrencyGuard.FormatGuardBucketName(this, bucketName), count, this.MaxConcurrency);
				MaxConcurrencyReachedException ex = new MaxConcurrencyReachedException(this, bucketName);
				if (this.onRejectDelegate != null)
				{
					this.onRejectDelegate(this, bucketName, stateObject, ex);
				}
				if (!this.TrainingMode)
				{
					throw ex;
				}
			}
			refCount.Increment();
			if (this.onIncrementDelegate != null)
			{
				this.onIncrementDelegate(this, bucketName, stateObject);
			}
			return count;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00020AE4 File Offset: 0x0001ECE4
		public long Decrement(object stateObject = null)
		{
			return this.Decrement("_Default", null);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00020AF4 File Offset: 0x0001ECF4
		public long Decrement(string bucketName, object stateObject = null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("bucketName", bucketName);
			ConcurrencyGuard.RefCount refCount = null;
			if (!this.TryGetRefCount(bucketName, out refCount))
			{
				return 0L;
			}
			if (refCount.Count > 0L)
			{
				long num = refCount.Decrement();
				ExTraceGlobals.ConcurrencyGuardTracer.TraceDebug<string, long, int>((long)this.GetHashCode(), "[ConcurrencyGuard::Decrement]: Guard {0} is at {1}/{2}.", ConcurrencyGuard.FormatGuardBucketName(this, bucketName), num, this.MaxConcurrency);
				if (this.onDecrementDelegate != null)
				{
					this.onDecrementDelegate(this, bucketName, stateObject);
				}
				return num;
			}
			bool flag = false;
			refCount.Reset();
			if (flag)
			{
				throw new ApplicationException("Cannot decrement guard " + ConcurrencyGuard.FormatGuardBucketName(this, bucketName) + " below 0. This usually indicates a bug in your code.");
			}
			return 0L;
		}

		// Token: 0x04000572 RID: 1394
		public const string DefaultBucketName = "_Default";

		// Token: 0x04000573 RID: 1395
		private readonly string guardName;

		// Token: 0x04000574 RID: 1396
		private readonly int maxConcurrency;

		// Token: 0x04000575 RID: 1397
		private readonly bool trainingMode;

		// Token: 0x04000576 RID: 1398
		private readonly int concurrencyWarningThreshold;

		// Token: 0x04000577 RID: 1399
		private readonly Action<ConcurrencyGuard, string, object> onIncrementDelegate;

		// Token: 0x04000578 RID: 1400
		private readonly Action<ConcurrencyGuard, string, object> onDecrementDelegate;

		// Token: 0x04000579 RID: 1401
		private readonly Action<ConcurrencyGuard, string, object> onNearThresholdDelegate;

		// Token: 0x0400057A RID: 1402
		private readonly Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException> onRejectDelegate;

		// Token: 0x0400057B RID: 1403
		private Dictionary<string, ConcurrencyGuard.RefCount> buckets = new Dictionary<string, ConcurrencyGuard.RefCount>(1);

		// Token: 0x0400057C RID: 1404
		private readonly ReaderWriterLockSlim instanceLock = new ReaderWriterLockSlim();

		// Token: 0x02000117 RID: 279
		private class RefCount
		{
			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000817 RID: 2071 RVA: 0x00020B92 File Offset: 0x0001ED92
			public long Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x00020B9A File Offset: 0x0001ED9A
			public long Increment()
			{
				return Interlocked.Increment(ref this.count);
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x00020BA7 File Offset: 0x0001EDA7
			public long Decrement()
			{
				return Interlocked.Decrement(ref this.count);
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x00020BB4 File Offset: 0x0001EDB4
			public void Reset()
			{
				lock (this)
				{
					this.count = 0L;
				}
			}

			// Token: 0x0400057D RID: 1405
			private long count;
		}
	}
}
