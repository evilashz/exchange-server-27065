using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004D1 RID: 1233
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x06003B37 RID: 15159 RVA: 0x000DF822 File Offset: 0x000DDA22
		public override int GetHashCode()
		{
			return this._dwFlags + this._dwWriterSeqNum + this._wReaderAndWriterLevel + this._dwThreadID;
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000DF83F File Offset: 0x000DDA3F
		public override bool Equals(object obj)
		{
			return obj is LockCookie && this.Equals((LockCookie)obj);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000DF857 File Offset: 0x000DDA57
		public bool Equals(LockCookie obj)
		{
			return obj._dwFlags == this._dwFlags && obj._dwWriterSeqNum == this._dwWriterSeqNum && obj._wReaderAndWriterLevel == this._wReaderAndWriterLevel && obj._dwThreadID == this._dwThreadID;
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000DF893 File Offset: 0x000DDA93
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000DF89D File Offset: 0x000DDA9D
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !(a == b);
		}

		// Token: 0x040018E4 RID: 6372
		private int _dwFlags;

		// Token: 0x040018E5 RID: 6373
		private int _dwWriterSeqNum;

		// Token: 0x040018E6 RID: 6374
		private int _wReaderAndWriterLevel;

		// Token: 0x040018E7 RID: 6375
		private int _dwThreadID;
	}
}
