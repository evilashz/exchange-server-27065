using System;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000278 RID: 632
	public abstract class UserContextBase : DisposeTrackableBase
	{
		// Token: 0x060014FE RID: 5374 RVA: 0x0007F880 File Offset: 0x0007DA80
		internal UserContextBase(UserContextKey key)
		{
			this.key = key;
			this.sessionBeginTime = Stopwatch.GetTimestamp();
			this.sessionLastAccessedTime = this.sessionBeginTime;
			this.writerLock = UserContextManager.GetUserContextKeyLock(key.ToString());
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0007F8B7 File Offset: 0x0007DAB7
		internal long SessionBeginTime
		{
			get
			{
				return this.sessionBeginTime;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0007F8BF File Offset: 0x0007DABF
		internal long LastAccessedTime
		{
			get
			{
				return this.sessionLastAccessedTime;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0007F8C7 File Offset: 0x0007DAC7
		internal long RequestCount
		{
			get
			{
				return this.requestCount;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0007F8CF File Offset: 0x0007DACF
		internal bool IsUserRequestLockHeld
		{
			get
			{
				return this.userRequestLockHeld;
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0007F8D9 File Offset: 0x0007DAD9
		internal void UpdateLastAccessedTime()
		{
			this.sessionLastAccessedTime = Stopwatch.GetTimestamp();
			this.requestCount += 1L;
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0007F8F5 File Offset: 0x0007DAF5
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0007F8FD File Offset: 0x0007DAFD
		internal CacheItemRemovedReason AbandonedReason
		{
			get
			{
				return this.abandonedReason;
			}
			set
			{
				this.abandonedReason = value;
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0007F906 File Offset: 0x0007DB06
		public void Touch()
		{
			HttpRuntime.Cache.Get(this.key.ToString());
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0007F91E File Offset: 0x0007DB1E
		internal UserContextKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0007F926 File Offset: 0x0007DB26
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x0007F92E File Offset: 0x0007DB2E
		internal UserContextState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0007F938 File Offset: 0x0007DB38
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0007F9A3 File Offset: 0x0007DBA3
		internal bool LastLockRequestFailed
		{
			get
			{
				if (HttpContext.Current == null || !HttpContext.Current.Items.Contains("LastLockRequestFailed"))
				{
					return false;
				}
				if (HttpContext.Current.Items["LastLockRequestFailed"] is bool)
				{
					return (bool)HttpContext.Current.Items["LastLockRequestFailed"];
				}
				throw new InvalidOperationException("HttpContext.Current.Items[LastLockRequestFailedKey] value is not a bool.  Other code is re-using this key.");
			}
			set
			{
				if (HttpContext.Current != null)
				{
					HttpContext.Current.Items["LastLockRequestFailed"] = value;
				}
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0007F9C6 File Offset: 0x0007DBC6
		internal void Lock()
		{
			this.Lock(false);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0007F9D0 File Offset: 0x0007DBD0
		internal void Lock(bool requestLock)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextBase>(0L, "UserContext.Lock, User context instance={0}", this);
			try
			{
				this.writerLock.LockWriterElastic(Globals.UserContextLockTimeout);
			}
			catch (OwaLockTimeoutException)
			{
				this.LastLockRequestFailed = true;
				throw;
			}
			this.userRequestLockHeld = requestLock;
			ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "Acquired lock succesfully");
			this.LastLockRequestFailed = false;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0007FA3C File Offset: 0x0007DC3C
		public void Unlock()
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextBase>(0L, "UserContext.Unlock, User context instance={0}", this);
			if (!this.writerLock.IsWriterLockHeld)
			{
				throw new ApplicationException("Current thread does not have the writerLock.");
			}
			try
			{
				this.OnBeforeUnlock();
			}
			finally
			{
				if (this.NumberOfLocksHeld <= 1)
				{
					this.userRequestLockHeld = false;
				}
				this.writerLock.ReleaseWriterLock();
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0007FAAC File Offset: 0x0007DCAC
		internal void TraceObject()
		{
			ExTraceGlobals.UserContextDataTracer.TraceDebug(0L, "UserContext instance: Key.UserContextId={0}, Key={1}, State={2}, User context instance={3}", new object[]
			{
				this.Key.UserContextId,
				this.Key,
				(uint)this.State,
				this
			});
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0007FAFB File Offset: 0x0007DCFB
		internal bool LockedByCurrentThread()
		{
			return this.writerLock.IsWriterLockHeld;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0007FB08 File Offset: 0x0007DD08
		internal void UnlockForcefully()
		{
			this.Unlock();
			if (this.LockedByCurrentThread())
			{
				this.ForceReleaseLock();
				throw new InvalidOperationException("Had to forcefully unlock user context!");
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0007FB29 File Offset: 0x0007DD29
		protected void ForceReleaseLock()
		{
			this.writerLock.ReleaseLock();
			this.userRequestLockHeld = false;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0007FB3F File Offset: 0x0007DD3F
		protected int NumberOfLocksHeld
		{
			get
			{
				return this.writerLock.NumberOfWriterLocksHeld;
			}
		}

		// Token: 0x06001514 RID: 5396
		protected abstract void OnBeforeUnlock();

		// Token: 0x06001515 RID: 5397 RVA: 0x0007FB4C File Offset: 0x0007DD4C
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0007FB4E File Offset: 0x0007DD4E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UserContextBase>(this);
		}

		// Token: 0x040010D3 RID: 4307
		protected const int LockTimeout = 60000;

		// Token: 0x040010D4 RID: 4308
		private const string LastLockRequestFailedKey = "LastLockRequestFailed";

		// Token: 0x040010D5 RID: 4309
		private long requestCount;

		// Token: 0x040010D6 RID: 4310
		private long sessionBeginTime;

		// Token: 0x040010D7 RID: 4311
		private long sessionLastAccessedTime;

		// Token: 0x040010D8 RID: 4312
		private UserContextState state;

		// Token: 0x040010D9 RID: 4313
		private UserContextKey key;

		// Token: 0x040010DA RID: 4314
		private OwaRWLockWrapper writerLock;

		// Token: 0x040010DB RID: 4315
		private CacheItemRemovedReason abandonedReason;

		// Token: 0x040010DC RID: 4316
		private volatile bool userRequestLockHeld;
	}
}
