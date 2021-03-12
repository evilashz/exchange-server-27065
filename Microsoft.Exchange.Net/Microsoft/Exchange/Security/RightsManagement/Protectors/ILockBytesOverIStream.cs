using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020009A9 RID: 2473
	internal sealed class ILockBytesOverIStream : ILockBytes
	{
		// Token: 0x060035A1 RID: 13729 RVA: 0x000878B4 File Offset: 0x00085AB4
		public ILockBytesOverIStream(Microsoft.Exchange.Security.RightsManagement.StructuredStorage.IStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000878D4 File Offset: 0x00085AD4
		public unsafe void ReadAt(ulong offset, byte[] buffer, int count, out int read)
		{
			this.stream.Seek((long)offset, 0);
			fixed (byte* ptr = buffer)
			{
				read = this.stream.Read(new IntPtr((void*)ptr), count);
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x00087920 File Offset: 0x00085B20
		public unsafe void WriteAt(ulong offset, byte[] buffer, int count, out int written)
		{
			this.stream.Seek((long)offset, 0);
			fixed (byte* ptr = buffer)
			{
				written = this.stream.Write(new IntPtr((void*)ptr), count);
			}
			if (written != count)
			{
				throw new InvalidOperationException("failed to write to IStream.");
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0008797C File Offset: 0x00085B7C
		public void Flush()
		{
			this.stream.Commit(STGC.STGC_DEFAULT);
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x0008798A File Offset: 0x00085B8A
		public void SetSize(ulong length)
		{
			this.stream.SetSize((long)length);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x00087998 File Offset: 0x00085B98
		public void LockRegion(ulong libOffset, ulong cb, int dwLockType)
		{
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x0008799A File Offset: 0x00085B9A
		public void UnlockRegion(ulong libOffset, ulong cb, int dwLockType)
		{
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x0008799C File Offset: 0x00085B9C
		public void Stat(out STATSTG pstatstg, STATFLAG grfStatFlag)
		{
			this.stream.Stat(out pstatstg, grfStatFlag);
		}

		// Token: 0x04002DC9 RID: 11721
		private Microsoft.Exchange.Security.RightsManagement.StructuredStorage.IStream stream;
	}
}
