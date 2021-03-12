using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000015 RID: 21
	internal abstract class DisposableBase : IDisposeTrackable, IDisposable
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00006B21 File Offset: 0x00004D21
		internal DisposableBase()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006B43 File Offset: 0x00004D43
		internal bool IsDisposed
		{
			get
			{
				return 1 == Interlocked.CompareExchange(ref this.disposed, 1, 1);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006B55 File Offset: 0x00004D55
		public void AddReference()
		{
			this.CheckDisposed();
			Interlocked.Increment(ref this.references);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006B69 File Offset: 0x00004D69
		public void ReleaseReference()
		{
			this.CheckDisposed();
			this.Dispose(true, 1);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006B79 File Offset: 0x00004D79
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006B8E File Offset: 0x00004D8E
		public virtual void Dispose()
		{
			this.Dispose(true, Interlocked.CompareExchange(ref this.impliedReference, 0, 1));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x060001BA RID: 442
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x060001BB RID: 443
		protected abstract void InternalDispose(bool disposing);

		// Token: 0x060001BC RID: 444 RVA: 0x00006BAC File Offset: 0x00004DAC
		protected void CheckDisposed()
		{
			if (1 == Interlocked.CompareExchange(ref this.disposed, 1, 1))
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006BD0 File Offset: 0x00004DD0
		private void Dispose(bool disposing, int releaseCount)
		{
			if (releaseCount > 0)
			{
				int num = Interlocked.Add(ref this.references, -releaseCount);
				if (num < 0)
				{
					throw new InvalidOperationException();
				}
				if (num == 0)
				{
					if (disposing && this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					this.InternalDispose(disposing);
					Interlocked.CompareExchange(ref this.disposed, 1, 0);
				}
			}
		}

		// Token: 0x04000082 RID: 130
		private int references = 1;

		// Token: 0x04000083 RID: 131
		private int impliedReference = 1;

		// Token: 0x04000084 RID: 132
		private int disposed;

		// Token: 0x04000085 RID: 133
		private DisposeTracker disposeTracker;

		// Token: 0x02000016 RID: 22
		internal abstract class Finalizable : DisposableBase
		{
			// Token: 0x060001BE RID: 446 RVA: 0x00006C30 File Offset: 0x00004E30
			~Finalizable()
			{
				base.Dispose(false, this.references);
			}

			// Token: 0x060001BF RID: 447 RVA: 0x00006C64 File Offset: 0x00004E64
			public override void Dispose()
			{
				base.Dispose(true, Interlocked.CompareExchange(ref this.impliedReference, 0, 1));
				GC.SuppressFinalize(this);
			}
		}
	}
}
