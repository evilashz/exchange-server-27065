using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004E0 RID: 1248
	internal sealed class PinnableBufferCache
	{
		// Token: 0x06003BAB RID: 15275 RVA: 0x000E0848 File Offset: 0x000DEA48
		public PinnableBufferCache(string cacheName, int numberOfElements) : this(cacheName, () => new byte[numberOfElements])
		{
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000E0875 File Offset: 0x000DEA75
		public byte[] AllocateBuffer()
		{
			return (byte[])this.Allocate();
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000E0882 File Offset: 0x000DEA82
		public void FreeBuffer(byte[] buffer)
		{
			this.Free(buffer);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000E088C File Offset: 0x000DEA8C
		[SecuritySafeCritical]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal PinnableBufferCache(string cacheName, Func<object> factory)
		{
			this.m_NotGen2 = new List<object>(16);
			this.m_factory = factory;
			string variable = "PinnableBufferCache_" + cacheName + "_Disabled";
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable(variable);
				if (environmentVariable != null)
				{
					PinnableBufferCacheEventSource.Log.DebugMessage("Creating " + cacheName + " PinnableBufferCacheDisabled=" + environmentVariable);
					int num = environmentVariable.IndexOf(cacheName, StringComparison.OrdinalIgnoreCase);
					if (0 <= num)
					{
						PinnableBufferCacheEventSource.Log.DebugMessage("Disabling " + cacheName);
						return;
					}
				}
			}
			catch
			{
			}
			string variable2 = "PinnableBufferCache_" + cacheName + "_MinCount";
			try
			{
				string environmentVariable2 = Environment.GetEnvironmentVariable(variable2);
				if (environmentVariable2 != null && int.TryParse(environmentVariable2, out this.m_minBufferCount))
				{
					this.CreateNewBuffers();
				}
			}
			catch
			{
			}
			PinnableBufferCacheEventSource.Log.Create(cacheName);
			this.m_CacheName = cacheName;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000E0984 File Offset: 0x000DEB84
		[SecuritySafeCritical]
		internal object Allocate()
		{
			if (this.m_CacheName == null)
			{
				return this.m_factory();
			}
			object obj;
			if (!this.m_FreeList.TryPop(out obj))
			{
				this.Restock(out obj);
			}
			if (PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				int num = Interlocked.Increment(ref this.m_numAllocCalls);
				if (num >= 1024)
				{
					lock (this)
					{
						int num2 = Interlocked.Exchange(ref this.m_numAllocCalls, 0);
						if (num2 >= 1024)
						{
							int num3 = 0;
							foreach (object obj2 in this.m_FreeList)
							{
								if (GC.GetGeneration(obj2) < GC.MaxGeneration)
								{
									num3++;
								}
							}
							PinnableBufferCacheEventSource.Log.WalkFreeListResult(this.m_CacheName, this.m_FreeList.Count, num3);
						}
					}
				}
				PinnableBufferCacheEventSource.Log.AllocateBuffer(this.m_CacheName, PinnableBufferCacheEventSource.AddressOf(obj), obj.GetHashCode(), GC.GetGeneration(obj), this.m_FreeList.Count);
			}
			return obj;
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000E0AC0 File Offset: 0x000DECC0
		[SecuritySafeCritical]
		internal void Free(object buffer)
		{
			if (this.m_CacheName == null)
			{
				return;
			}
			if (PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				PinnableBufferCacheEventSource.Log.FreeBuffer(this.m_CacheName, PinnableBufferCacheEventSource.AddressOf(buffer), buffer.GetHashCode(), this.m_FreeList.Count);
			}
			if (buffer == null)
			{
				if (PinnableBufferCacheEventSource.Log.IsEnabled())
				{
					PinnableBufferCacheEventSource.Log.FreeBufferNull(this.m_CacheName, this.m_FreeList.Count);
				}
				return;
			}
			if (this.m_gen1CountAtLastRestock + 3 > GC.CollectionCount(GC.MaxGeneration - 1))
			{
				lock (this)
				{
					if (GC.GetGeneration(buffer) < GC.MaxGeneration)
					{
						this.m_moreThanFreeListNeeded = true;
						PinnableBufferCacheEventSource.Log.FreeBufferStillTooYoung(this.m_CacheName, this.m_NotGen2.Count);
						this.m_NotGen2.Add(buffer);
						this.m_gen1CountAtLastRestock = GC.CollectionCount(GC.MaxGeneration - 1);
						return;
					}
				}
			}
			this.m_FreeList.Push(buffer);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000E0BD0 File Offset: 0x000DEDD0
		[SecuritySafeCritical]
		private void Restock(out object returnBuffer)
		{
			lock (this)
			{
				if (!this.m_FreeList.TryPop(out returnBuffer))
				{
					if (this.m_restockSize == 0)
					{
						Gen2GcCallback.Register(new Func<object, bool>(PinnableBufferCache.Gen2GcCallbackFunc), this);
					}
					this.m_moreThanFreeListNeeded = true;
					PinnableBufferCacheEventSource.Log.AllocateBufferFreeListEmpty(this.m_CacheName, this.m_NotGen2.Count);
					if (this.m_NotGen2.Count == 0)
					{
						this.CreateNewBuffers();
					}
					int index = this.m_NotGen2.Count - 1;
					if (GC.GetGeneration(this.m_NotGen2[index]) < GC.MaxGeneration && GC.GetGeneration(this.m_NotGen2[0]) == GC.MaxGeneration)
					{
						index = 0;
					}
					returnBuffer = this.m_NotGen2[index];
					this.m_NotGen2.RemoveAt(index);
					if (PinnableBufferCacheEventSource.Log.IsEnabled() && GC.GetGeneration(returnBuffer) < GC.MaxGeneration)
					{
						PinnableBufferCacheEventSource.Log.AllocateBufferFromNotGen2(this.m_CacheName, this.m_NotGen2.Count);
					}
					if (!this.AgePendingBuffers() && this.m_NotGen2.Count == this.m_restockSize / 2)
					{
						PinnableBufferCacheEventSource.Log.DebugMessage("Proactively adding more buffers to aging pool");
						this.CreateNewBuffers();
					}
				}
			}
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000E0D34 File Offset: 0x000DEF34
		[SecuritySafeCritical]
		private bool AgePendingBuffers()
		{
			if (this.m_gen1CountAtLastRestock < GC.CollectionCount(GC.MaxGeneration - 1))
			{
				int num = 0;
				List<object> list = new List<object>();
				PinnableBufferCacheEventSource.Log.AllocateBufferAged(this.m_CacheName, this.m_NotGen2.Count);
				for (int i = 0; i < this.m_NotGen2.Count; i++)
				{
					object obj = this.m_NotGen2[i];
					if (GC.GetGeneration(obj) >= GC.MaxGeneration)
					{
						this.m_FreeList.Push(obj);
						num++;
					}
					else
					{
						list.Add(obj);
					}
				}
				PinnableBufferCacheEventSource.Log.AgePendingBuffersResults(this.m_CacheName, num, list.Count);
				this.m_NotGen2 = list;
				return true;
			}
			return false;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000E0DE8 File Offset: 0x000DEFE8
		private void CreateNewBuffers()
		{
			if (this.m_restockSize == 0)
			{
				this.m_restockSize = 4;
			}
			else if (this.m_restockSize < 16)
			{
				this.m_restockSize = 16;
			}
			else if (this.m_restockSize < 256)
			{
				this.m_restockSize *= 2;
			}
			else if (this.m_restockSize < 4096)
			{
				this.m_restockSize = this.m_restockSize * 3 / 2;
			}
			else
			{
				this.m_restockSize = 4096;
			}
			if (this.m_minBufferCount > this.m_buffersUnderManagement)
			{
				this.m_restockSize = Math.Max(this.m_restockSize, this.m_minBufferCount - this.m_buffersUnderManagement);
			}
			PinnableBufferCacheEventSource.Log.AllocateBufferCreatingNewBuffers(this.m_CacheName, this.m_buffersUnderManagement, this.m_restockSize);
			for (int i = 0; i < this.m_restockSize; i++)
			{
				object item = this.m_factory();
				object obj = new object();
				this.m_NotGen2.Add(item);
			}
			this.m_buffersUnderManagement += this.m_restockSize;
			this.m_gen1CountAtLastRestock = GC.CollectionCount(GC.MaxGeneration - 1);
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000E0EFD File Offset: 0x000DF0FD
		[SecuritySafeCritical]
		private static bool Gen2GcCallbackFunc(object targetObj)
		{
			return ((PinnableBufferCache)targetObj).TrimFreeListIfNeeded();
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000E0F0C File Offset: 0x000DF10C
		[SecuritySafeCritical]
		private bool TrimFreeListIfNeeded()
		{
			int tickCount = Environment.TickCount;
			int num = tickCount - this.m_msecNoUseBeyondFreeListSinceThisTime;
			PinnableBufferCacheEventSource.Log.TrimCheck(this.m_CacheName, this.m_buffersUnderManagement, this.m_moreThanFreeListNeeded, num);
			if (this.m_moreThanFreeListNeeded)
			{
				this.m_moreThanFreeListNeeded = false;
				this.m_trimmingExperimentInProgress = false;
				this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
				return true;
			}
			if (0 <= num && num < 10000)
			{
				return true;
			}
			lock (this)
			{
				if (this.m_moreThanFreeListNeeded)
				{
					this.m_moreThanFreeListNeeded = false;
					this.m_trimmingExperimentInProgress = false;
					this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
					return true;
				}
				int count = this.m_FreeList.Count;
				if (this.m_NotGen2.Count > 0)
				{
					if (!this.m_trimmingExperimentInProgress)
					{
						PinnableBufferCacheEventSource.Log.TrimFlush(this.m_CacheName, this.m_buffersUnderManagement, count, this.m_NotGen2.Count);
						this.AgePendingBuffers();
						this.m_trimmingExperimentInProgress = true;
						return true;
					}
					PinnableBufferCacheEventSource.Log.TrimFree(this.m_CacheName, this.m_buffersUnderManagement, count, this.m_NotGen2.Count);
					this.m_buffersUnderManagement -= this.m_NotGen2.Count;
					int num2 = this.m_buffersUnderManagement / 4;
					if (num2 < this.m_restockSize)
					{
						this.m_restockSize = Math.Max(num2, 16);
					}
					this.m_NotGen2.Clear();
					this.m_trimmingExperimentInProgress = false;
					return true;
				}
				else
				{
					int num3 = count / 4 + 1;
					if (count * 15 <= this.m_buffersUnderManagement || this.m_buffersUnderManagement - num3 <= this.m_minBufferCount)
					{
						PinnableBufferCacheEventSource.Log.TrimFreeSizeOK(this.m_CacheName, this.m_buffersUnderManagement, count);
						return true;
					}
					PinnableBufferCacheEventSource.Log.TrimExperiment(this.m_CacheName, this.m_buffersUnderManagement, count, num3);
					for (int i = 0; i < num3; i++)
					{
						object item;
						if (this.m_FreeList.TryPop(out item))
						{
							this.m_NotGen2.Add(item);
						}
					}
					this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
					this.m_trimmingExperimentInProgress = true;
				}
			}
			return true;
		}

		// Token: 0x0400190F RID: 6415
		private const int DefaultNumberOfBuffers = 16;

		// Token: 0x04001910 RID: 6416
		private string m_CacheName;

		// Token: 0x04001911 RID: 6417
		private Func<object> m_factory;

		// Token: 0x04001912 RID: 6418
		private ConcurrentStack<object> m_FreeList = new ConcurrentStack<object>();

		// Token: 0x04001913 RID: 6419
		private List<object> m_NotGen2;

		// Token: 0x04001914 RID: 6420
		private int m_gen1CountAtLastRestock;

		// Token: 0x04001915 RID: 6421
		private int m_msecNoUseBeyondFreeListSinceThisTime;

		// Token: 0x04001916 RID: 6422
		private bool m_moreThanFreeListNeeded;

		// Token: 0x04001917 RID: 6423
		private int m_buffersUnderManagement;

		// Token: 0x04001918 RID: 6424
		private int m_restockSize;

		// Token: 0x04001919 RID: 6425
		private bool m_trimmingExperimentInProgress;

		// Token: 0x0400191A RID: 6426
		private int m_minBufferCount;

		// Token: 0x0400191B RID: 6427
		private int m_numAllocCalls;
	}
}
