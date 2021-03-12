using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal class HandleCache : DisposeTrackableBase
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002D56 File Offset: 0x00000F56
		public HandleCache()
		{
			this.cache = new Dictionary<long, HandleCache.HandleRec>();
			this.nextHandleValue = 1L;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002D7C File Offset: 0x00000F7C
		public T GetObject<T>(long handle) where T : class
		{
			MrsTracer.ProxyService.Function("HandleCache.GetObject<{1}>({0})", new object[]
			{
				handle,
				typeof(T).Name
			});
			T result;
			lock (this.locker)
			{
				HandleCache.HandleRec handleRec = this.FindObject(handle, HandleCache.FindObjectOptions.MustExist);
				T t = handleRec.Obj as T;
				if (t == null)
				{
					MrsTracer.ProxyService.Error("Handle {0} has wrong type: {1}, expected {2}.", new object[]
					{
						handle,
						handleRec.Obj.GetType().Name,
						typeof(T).Name
					});
					throw new InvalidHandleTypePermanentException(handle, handleRec.Obj.GetType().Name, typeof(T).Name);
				}
				result = t;
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E84 File Offset: 0x00001084
		public long GetParentHandle(long handle)
		{
			MrsTracer.ProxyService.Function("HandleCache.GetParentHandle({0})", new object[]
			{
				handle
			});
			long parentHandle;
			lock (this.locker)
			{
				HandleCache.HandleRec handleRec = this.FindObject(handle, HandleCache.FindObjectOptions.MustExist);
				parentHandle = handleRec.ParentHandle;
			}
			return parentHandle;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002EF4 File Offset: 0x000010F4
		public long AddObject(object obj, long parentHandle)
		{
			MrsTracer.ProxyService.Function("HandleCache.AddObject({0})", new object[]
			{
				obj.GetType().Name
			});
			long handle;
			lock (this.locker)
			{
				HandleCache.HandleRec handleRec = new HandleCache.HandleRec();
				handleRec.Obj = obj;
				handleRec.ParentHandle = parentHandle;
				handleRec.Handle = this.nextHandleValue;
				this.nextHandleValue += 1L;
				this.cache.Add(handleRec.Handle, handleRec);
				MrsTracer.ProxyService.Debug("HandleCache.AddObject({0}) returns {1}", new object[]
				{
					obj.GetType().Name,
					handleRec.Handle
				});
				handle = handleRec.Handle;
			}
			return handle;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002FD8 File Offset: 0x000011D8
		public void ReleaseObject(long handle)
		{
			MrsTracer.ProxyService.Function("HandleCache.ReleaseObject({0})", new object[]
			{
				handle
			});
			lock (this.locker)
			{
				HandleCache.HandleRec handleRec = this.FindObject(handle, HandleCache.FindObjectOptions.MayBeAbsent);
				if (handleRec != null)
				{
					this.RemoveFromCache(handleRec);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003048 File Offset: 0x00001248
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.locker)
				{
					if (this.cache.Count > 0)
					{
						MrsTracer.ProxyService.Error("HandleCache being disposed with {0} open handles", new object[]
						{
							this.cache.Count
						});
						while (this.cache.Count > 0)
						{
							HandleCache.HandleRec rec = this.cache.Values.First<HandleCache.HandleRec>();
							this.RemoveFromCache(rec);
						}
					}
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000030E8 File Offset: 0x000012E8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<HandleCache>(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000030F8 File Offset: 0x000012F8
		private void RemoveFromCache(HandleCache.HandleRec rec)
		{
			Lazy<List<HandleCache.HandleRec>> lazy = new Lazy<List<HandleCache.HandleRec>>(() => new List<HandleCache.HandleRec>());
			foreach (HandleCache.HandleRec handleRec in this.cache.Values)
			{
				if (handleRec.ParentHandle == rec.Handle)
				{
					lazy.Value.Add(handleRec);
				}
			}
			if (lazy.IsValueCreated)
			{
				foreach (HandleCache.HandleRec rec2 in lazy.Value)
				{
					this.RemoveFromCache(rec2);
				}
			}
			IDisposable disposable = rec.Obj as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.cache.Remove(rec.Handle);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000031FC File Offset: 0x000013FC
		private HandleCache.HandleRec FindObject(long handle, HandleCache.FindObjectOptions options)
		{
			HandleCache.HandleRec result;
			if (this.cache.TryGetValue(handle, out result))
			{
				return result;
			}
			if ((options & HandleCache.FindObjectOptions.MustExist) != HandleCache.FindObjectOptions.MayBeAbsent)
			{
				MrsTracer.ProxyService.Error("Handle {0} is not found in MRS handle cache.", new object[]
				{
					handle
				});
				throw new HandleNotFoundPermanentException(handle);
			}
			return null;
		}

		// Token: 0x04000016 RID: 22
		public const long NoParent = -1L;

		// Token: 0x04000017 RID: 23
		private Dictionary<long, HandleCache.HandleRec> cache;

		// Token: 0x04000018 RID: 24
		private long nextHandleValue;

		// Token: 0x04000019 RID: 25
		private object locker = new object();

		// Token: 0x02000005 RID: 5
		[Flags]
		private enum FindObjectOptions
		{
			// Token: 0x0400001C RID: 28
			MayBeAbsent = 0,
			// Token: 0x0400001D RID: 29
			MustExist = 1
		}

		// Token: 0x02000006 RID: 6
		private class HandleRec
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600001F RID: 31 RVA: 0x00003248 File Offset: 0x00001448
			// (set) Token: 0x06000020 RID: 32 RVA: 0x00003250 File Offset: 0x00001450
			public long Handle { get; set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000021 RID: 33 RVA: 0x00003259 File Offset: 0x00001459
			// (set) Token: 0x06000022 RID: 34 RVA: 0x00003261 File Offset: 0x00001461
			public long ParentHandle { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000023 RID: 35 RVA: 0x0000326A File Offset: 0x0000146A
			// (set) Token: 0x06000024 RID: 36 RVA: 0x00003272 File Offset: 0x00001472
			public object Obj { get; set; }
		}
	}
}
