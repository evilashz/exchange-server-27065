using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Clients
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CachedClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00006E0B File Offset: 0x0000500B
		protected CachedClient(IWcfClient client)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.Client = client;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006E26 File Offset: 0x00005026
		public virtual bool IsValid
		{
			get
			{
				return this.Client == null || this.Client.IsValid;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006E3D File Offset: 0x0000503D
		internal bool CanRemoveFromCache
		{
			get
			{
				return this.refCount <= 0;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006E4B File Offset: 0x0000504B
		internal int ReferenceCount
		{
			get
			{
				return this.refCount;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006E53 File Offset: 0x00005053
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006E5B File Offset: 0x0000505B
		private protected IWcfClient Client { protected get; private set; }

		// Token: 0x06000157 RID: 343 RVA: 0x00006E64 File Offset: 0x00005064
		void IDisposable.Dispose()
		{
			Interlocked.Decrement(ref this.refCount);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006E72 File Offset: 0x00005072
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker == null)
			{
				return;
			}
			this.disposeTracker.Suppress();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006E88 File Offset: 0x00005088
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006E90 File Offset: 0x00005090
		internal virtual void Cleanup()
		{
			if (this.Client == null)
			{
				return;
			}
			this.Client.Disconnect();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006EA6 File Offset: 0x000050A6
		internal void IncrementReferenceCount()
		{
			Interlocked.Increment(ref this.refCount);
		}

		// Token: 0x0600015C RID: 348
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x0400008D RID: 141
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400008E RID: 142
		private int refCount;
	}
}
