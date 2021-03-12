using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004DA RID: 1242
	public sealed class PreAllocatedOverlapped : IDisposable, IDeferredDisposable
	{
		// Token: 0x06003B90 RID: 15248 RVA: 0x000E034F File Offset: 0x000DE54F
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._overlapped = new ThreadPoolBoundHandleOverlapped(callback, state, pinData, this);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000E0374 File Offset: 0x000DE574
		internal bool AddRef()
		{
			return this._lifetime.AddRef(this);
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000E0382 File Offset: 0x000DE582
		[SecurityCritical]
		internal void Release()
		{
			this._lifetime.Release(this);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000E0390 File Offset: 0x000DE590
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000E03A4 File Offset: 0x000DE5A4
		~PreAllocatedOverlapped()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose();
			}
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000E03D8 File Offset: 0x000DE5D8
		[SecurityCritical]
		unsafe void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (this._overlapped != null)
			{
				if (disposed)
				{
					Overlapped.Free(this._overlapped._nativeOverlapped);
					return;
				}
				this._overlapped._boundHandle = null;
				this._overlapped._completed = false;
				*this._overlapped._nativeOverlapped = default(NativeOverlapped);
			}
		}

		// Token: 0x04001901 RID: 6401
		[SecurityCritical]
		internal readonly ThreadPoolBoundHandleOverlapped _overlapped;

		// Token: 0x04001902 RID: 6402
		private DeferredDisposableLifetime<PreAllocatedOverlapped> _lifetime;
	}
}
