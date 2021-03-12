using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
	// Token: 0x0200019E RID: 414
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x06001963 RID: 6499 RVA: 0x00054838 File Offset: 0x00052A38
		[SecurityCritical]
		private PinnedBufferMemoryStream()
		{
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00054840 File Offset: 0x00052A40
		[SecurityCritical]
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			int num = array.Length;
			if (num == 0)
			{
				array = new byte[1];
				num = 0;
			}
			this._array = array;
			this._pinningHandle = new GCHandle(array, GCHandleType.Pinned);
			fixed (byte* array2 = this._array)
			{
				base.Initialize(array2, (long)num, (long)num, FileAccess.Read, true);
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000548A8 File Offset: 0x00052AA8
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000548D8 File Offset: 0x00052AD8
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				this._pinningHandle.Free();
				this._isOpen = false;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040008EA RID: 2282
		private byte[] _array;

		// Token: 0x040008EB RID: 2283
		private GCHandle _pinningHandle;
	}
}
