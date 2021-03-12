using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A17 RID: 2583
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IStreamOverStream : IStream
	{
		// Token: 0x06003867 RID: 14439 RVA: 0x0008FD4E File Offset: 0x0008DF4E
		public IStreamOverStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x0008FD6B File Offset: 0x0008DF6B
		public int Read(IntPtr buf, int len)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x0008FD74 File Offset: 0x0008DF74
		public int Write(IntPtr buf, int len)
		{
			if (buf == IntPtr.Zero)
			{
				throw new ArgumentNullException("buf");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len", len, "len cannot be less than zero.");
			}
			byte[] array = new byte[len];
			Marshal.Copy(buf, array, 0, array.Length);
			this.stream.Write(array, 0, len);
			return len;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x0008FDD4 File Offset: 0x0008DFD4
		public long Seek(long dlibMove, int dwOrigin)
		{
			return this.stream.Seek(dlibMove, (SeekOrigin)dwOrigin);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x0008FDE3 File Offset: 0x0008DFE3
		public void SetSize(long libNewSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x0008FDEA File Offset: 0x0008DFEA
		public long CopyTo(IStream pstm, long cb, long[] pcbRead)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x0008FDF1 File Offset: 0x0008DFF1
		public void Commit(STGC grfCommitFlags)
		{
			this.stream.Flush();
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x0008FDFE File Offset: 0x0008DFFE
		public void Revert()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x0008FE05 File Offset: 0x0008E005
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x0008FE0C File Offset: 0x0008E00C
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x0008FE14 File Offset: 0x0008E014
		public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pStatstg, STATFLAG grfStatFlag)
		{
			pStatstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			if (grfStatFlag != STATFLAG.NoName)
			{
				throw new NotImplementedException();
			}
			pStatstg.pwcsName = null;
			pStatstg.type = 2;
			pStatstg.cbSize = this.stream.Length;
			pStatstg.atime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			pStatstg.mtime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			pStatstg.ctime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			pStatstg.clsid = Guid.Empty;
			pStatstg.grfMode = (this.stream.CanWrite ? 2 : 0);
			pStatstg.grfLocksSupported = 2;
			pStatstg.grfStateBits = 0;
			pStatstg.reserved = 0;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x0008FEAC File Offset: 0x0008E0AC
		public IStream Clone()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002F99 RID: 12185
		private Stream stream;
	}
}
