using System;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092C RID: 2348
	internal sealed class SafeHeapHandleCache : IDisposable
	{
		// Token: 0x060060CD RID: 24781 RVA: 0x0014A333 File Offset: 0x00148533
		[SecuritySafeCritical]
		public SafeHeapHandleCache(ulong minSize = 64UL, ulong maxSize = 2048UL, int maxHandles = 0)
		{
			this._minSize = minSize;
			this._maxSize = maxSize;
			this._handleCache = new SafeHeapHandle[(maxHandles > 0) ? maxHandles : (Environment.ProcessorCount * 4)];
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0014A364 File Offset: 0x00148564
		[SecurityCritical]
		public SafeHeapHandle Acquire(ulong minSize = 0UL)
		{
			if (minSize < this._minSize)
			{
				minSize = this._minSize;
			}
			SafeHeapHandle safeHeapHandle = null;
			for (int i = 0; i < this._handleCache.Length; i++)
			{
				safeHeapHandle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[i], null);
				if (safeHeapHandle != null)
				{
					break;
				}
			}
			if (safeHeapHandle != null)
			{
				if (safeHeapHandle.ByteLength < minSize)
				{
					safeHeapHandle.Resize(minSize);
				}
			}
			else
			{
				safeHeapHandle = new SafeHeapHandle(minSize);
			}
			return safeHeapHandle;
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0014A3CC File Offset: 0x001485CC
		[SecurityCritical]
		public void Release(SafeHeapHandle handle)
		{
			if (handle.ByteLength <= this._maxSize)
			{
				for (int i = 0; i < this._handleCache.Length; i++)
				{
					handle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[i], handle);
					if (handle == null)
					{
						return;
					}
				}
			}
			handle.Dispose();
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0014A418 File Offset: 0x00148618
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x0014A428 File Offset: 0x00148628
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._handleCache != null)
			{
				for (int i = 0; i < this._handleCache.Length; i++)
				{
					SafeHeapHandle safeHeapHandle = this._handleCache[i];
					this._handleCache[i] = null;
					if (safeHeapHandle != null && disposing)
					{
						safeHeapHandle.Dispose();
					}
				}
			}
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x0014A470 File Offset: 0x00148670
		~SafeHeapHandleCache()
		{
			this.Dispose(false);
		}

		// Token: 0x04002AC8 RID: 10952
		private readonly ulong _minSize;

		// Token: 0x04002AC9 RID: 10953
		private readonly ulong _maxSize;

		// Token: 0x04002ACA RID: 10954
		[SecurityCritical]
		internal readonly SafeHeapHandle[] _handleCache;
	}
}
