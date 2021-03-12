using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Office.Story.V1.GraphicsInterop
{
	// Token: 0x0200001A RID: 26
	internal class StreamWrapper : IStream
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003BCB File Offset: 0x00001DCB
		public StreamWrapper(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003BDA File Offset: 0x00001DDA
		public Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003BE2 File Offset: 0x00001DE2
		public void Clone(out IStream ppstm)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003BE9 File Offset: 0x00001DE9
		public void Commit(int grfCommitFlags)
		{
			this._stream.Flush();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			if (pstm == null)
			{
				throw new ArgumentNullException("pstm");
			}
			byte[] array = new byte[cb];
			int num = this._stream.Read(array, 0, array.Length);
			if (IntPtr.Zero != pcbRead)
			{
				Marshal.WriteInt32(pcbRead, 0, num);
			}
			pstm.Write(array, num, pcbWritten);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003C4B File Offset: 0x00001E4B
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C54 File Offset: 0x00001E54
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			int val = this._stream.Read(pv, 0, cb);
			if (IntPtr.Zero != pcbRead)
			{
				Marshal.WriteInt32(pcbRead, 0, val);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C85 File Offset: 0x00001E85
		public void Revert()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003C8C File Offset: 0x00001E8C
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			long val = this._stream.Seek(dlibMove, (SeekOrigin)dwOrigin);
			if (IntPtr.Zero != plibNewPosition)
			{
				Marshal.WriteInt64(plibNewPosition, 0, val);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003CBC File Offset: 0x00001EBC
		public void SetSize(long libNewSize)
		{
			this._stream.SetLength(libNewSize);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003CCC File Offset: 0x00001ECC
		public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG
			{
				cbSize = this._stream.Length,
				grfLocksSupported = 0
			};
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003D02 File Offset: 0x00001F02
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003D09 File Offset: 0x00001F09
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			this._stream.Write(pv, 0, cb);
			if (IntPtr.Zero != pcbWritten)
			{
				Marshal.WriteInt32(pcbWritten, 0, cb);
			}
		}

		// Token: 0x04000079 RID: 121
		private readonly Stream _stream;
	}
}
