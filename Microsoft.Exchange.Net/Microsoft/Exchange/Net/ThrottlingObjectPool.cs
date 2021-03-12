using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C98 RID: 3224
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThrottlingObjectPool<DataType> : DisposeTrackableBase where DataType : new()
	{
		// Token: 0x060046FF RID: 18175 RVA: 0x000BEEEF File Offset: 0x000BD0EF
		public ThrottlingObjectPool() : this(Environment.ProcessorCount)
		{
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000BEEFC File Offset: 0x000BD0FC
		public ThrottlingObjectPool(int initialAllocatedObjects) : this(initialAllocatedObjects, int.MaxValue)
		{
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000BEF0C File Offset: 0x000BD10C
		public ThrottlingObjectPool(int initialAllocatedObjects, int maximumPoolSize)
		{
			if (initialAllocatedObjects < 0)
			{
				throw new ArgumentOutOfRangeException("initialAllocatedObjects", initialAllocatedObjects, "Should not be negative.");
			}
			if (maximumPoolSize < 1)
			{
				throw new ArgumentOutOfRangeException("maximumPoolSize", maximumPoolSize, "Needs be be >= 1.");
			}
			if (maximumPoolSize < initialAllocatedObjects)
			{
				throw new ArgumentOutOfRangeException("maximumPoolSize", maximumPoolSize, "Needs to be >= initialAllocatedObjects.");
			}
			this.poolCapacity = maximumPoolSize;
			int val = Math.Max(16, initialAllocatedObjects);
			int capacity = Math.Min(maximumPoolSize, val);
			this.bufferPool = new Stack<DataType>(capacity);
			for (int i = 0; i < initialAllocatedObjects; i++)
			{
				this.Release(this.AllocateNewObject());
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004702 RID: 18178 RVA: 0x000BEFCB File Offset: 0x000BD1CB
		// (set) Token: 0x06004703 RID: 18179 RVA: 0x000BEFD3 File Offset: 0x000BD1D3
		public int TotalBuffersAllocated { get; private set; }

		// Token: 0x06004704 RID: 18180 RVA: 0x000BEFDC File Offset: 0x000BD1DC
		public DataType Acquire()
		{
			return this.Acquire(0);
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x000BEFE8 File Offset: 0x000BD1E8
		public DataType Acquire(int waitCycle)
		{
			base.CheckDisposed();
			DataType result = default(DataType);
			bool flag = false;
			try
			{
				while (!flag)
				{
					flag = Monitor.TryEnter(this.acquireMonitor, waitCycle);
					if (flag)
					{
						if (this.poolNotEmptyEvent.WaitOne(waitCycle))
						{
							lock (this.bufferPool)
							{
								result = this.bufferPool.Pop();
								if (this.bufferPool.Count == 0)
								{
									this.poolNotEmptyEvent.Reset();
								}
								continue;
							}
						}
						return this.AllocateNewObject();
					}
					if (!this.throttlingObjectCreation)
					{
						lock (this.createMonitor)
						{
							if (!this.throttlingObjectCreation)
							{
								try
								{
									this.throttlingObjectCreation = true;
									return this.AllocateNewObject();
								}
								finally
								{
									if (waitCycle > 0)
									{
										this.timer = new System.Timers.Timer((double)waitCycle);
										this.timer.AutoReset = false;
										this.timer.Elapsed += this.ReleaseObjectCreationThrottle;
										this.timer.Enabled = true;
									}
									else
									{
										this.throttlingObjectCreation = false;
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.acquireMonitor);
				}
			}
			return result;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x000BF188 File Offset: 0x000BD388
		public void Release(DataType value)
		{
			base.CheckDisposed();
			if (this.bufferPool.Count < this.poolCapacity)
			{
				lock (this.bufferPool)
				{
					this.bufferPool.Push(value);
					if (1 == this.bufferPool.Count)
					{
						this.poolNotEmptyEvent.Set();
					}
					return;
				}
			}
			IDisposable disposable = value as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x000BF218 File Offset: 0x000BD418
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ThrottlingObjectPool<DataType>>(this);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x000BF220 File Offset: 0x000BD420
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.ReleaseObjectCreationThrottle(null, null);
				if (this.poolNotEmptyEvent != null)
				{
					this.poolNotEmptyEvent.Close();
				}
				lock (this.bufferPool)
				{
					while (this.bufferPool.Count > 0)
					{
						DataType dataType = this.bufferPool.Pop();
						IDisposable disposable = dataType as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x000BF2AC File Offset: 0x000BD4AC
		private DataType AllocateNewObject()
		{
			this.TotalBuffersAllocated++;
			if (default(DataType) != null)
			{
				return default(DataType);
			}
			return Activator.CreateInstance<DataType>();
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x000BF2E8 File Offset: 0x000BD4E8
		private void ReleaseObjectCreationThrottle(object source, ElapsedEventArgs eventArgs)
		{
			lock (this.createMonitor)
			{
				this.throttlingObjectCreation = false;
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
			}
		}

		// Token: 0x04003C26 RID: 15398
		private readonly Stack<DataType> bufferPool;

		// Token: 0x04003C27 RID: 15399
		private readonly object acquireMonitor = new object();

		// Token: 0x04003C28 RID: 15400
		private readonly object createMonitor = new object();

		// Token: 0x04003C29 RID: 15401
		private readonly ManualResetEvent poolNotEmptyEvent = new ManualResetEvent(false);

		// Token: 0x04003C2A RID: 15402
		private bool throttlingObjectCreation;

		// Token: 0x04003C2B RID: 15403
		private int poolCapacity;

		// Token: 0x04003C2C RID: 15404
		private System.Timers.Timer timer;
	}
}
