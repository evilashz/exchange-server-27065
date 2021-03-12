using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C7 RID: 711
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExMapiStreamHandle : SafeExInterfaceHandle, IExMapiStream, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x00036BAC File Offset: 0x00034DAC
		protected SafeExMapiStreamHandle()
		{
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00036BB4 File Offset: 0x00034DB4
		internal SafeExMapiStreamHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00036BBD File Offset: 0x00034DBD
		internal SafeExMapiStreamHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00036BC6 File Offset: 0x00034DC6
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiStreamHandle>(this);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00036BD0 File Offset: 0x00034DD0
		public int Read(byte[] pv, uint cb, out uint cbRead)
		{
			GCHandle gchandle = default(GCHandle);
			int result;
			try
			{
				gchandle = GCHandle.Alloc(pv, GCHandleType.Pinned);
				IntPtr pv2 = Marshal.UnsafeAddrOfPinnedArrayElement(pv, 0);
				result = SafeExMapiStreamHandle.IStream_Read(this.handle, pv2, cb, out cbRead);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00036C28 File Offset: 0x00034E28
		public int Write(byte[] pv, int cb, out int pcbWritten)
		{
			return SafeExMapiStreamHandle.IStream_Write(this.handle, pv, cb, out pcbWritten);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00036C38 File Offset: 0x00034E38
		public int Seek(long dlibMove, int dwOrigin, out long plibNewPosition)
		{
			return SafeExMapiStreamHandle.IStream_Seek(this.handle, dlibMove, dwOrigin, out plibNewPosition);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00036C48 File Offset: 0x00034E48
		public int SetSize(long libNewSize)
		{
			return SafeExMapiStreamHandle.IStream_SetSize(this.handle, libNewSize);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00036C56 File Offset: 0x00034E56
		public int CopyTo(IFastStream pstm, long cb, IntPtr pcbRead, out long pcbWritten)
		{
			return SafeExMapiStreamHandle.IStream_CopyTo(this.handle, pstm, cb, pcbRead, out pcbWritten);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00036C68 File Offset: 0x00034E68
		public int Commit(int grfCommitFlags)
		{
			return SafeExMapiStreamHandle.IStream_Commit(this.handle, grfCommitFlags);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00036C76 File Offset: 0x00034E76
		public int Revert()
		{
			return SafeExMapiStreamHandle.IStream_Revert(this.handle);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00036C83 File Offset: 0x00034E83
		public int LockRegion(long libOffset, long cb, int dwLockType)
		{
			return SafeExMapiStreamHandle.IStream_LockRegion(this.handle, libOffset, cb, dwLockType);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00036C93 File Offset: 0x00034E93
		public int UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			return SafeExMapiStreamHandle.IStream_UnlockRegion(this.handle, libOffset, cb, dwLockType);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00036CA3 File Offset: 0x00034EA3
		public int Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			return SafeExMapiStreamHandle.IStream_Stat(this.handle, out pstatstg, grfStatFlag);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00036CB4 File Offset: 0x00034EB4
		public int Clone(out IExInterface iStreamNew)
		{
			SafeExInterfaceHandle safeExInterfaceHandle = null;
			int result = SafeExMapiStreamHandle.IStream_Clone(this.handle, out safeExInterfaceHandle);
			iStreamNew = safeExInterfaceHandle;
			return result;
		}

		// Token: 0x06000E41 RID: 3649
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Read(IntPtr iStream, IntPtr pv, uint cb, out uint cbRead);

		// Token: 0x06000E42 RID: 3650
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Write(IntPtr iStream, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, out int pcbWritten);

		// Token: 0x06000E43 RID: 3651
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Seek(IntPtr iStream, long dlibMove, int dwOrigin, out long plibNewPosition);

		// Token: 0x06000E44 RID: 3652
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_SetSize(IntPtr iStream, long libNewSize);

		// Token: 0x06000E45 RID: 3653
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_CopyTo(IntPtr iStream, IFastStream pstm, long cb, IntPtr pcbRead, out long pcbWritten);

		// Token: 0x06000E46 RID: 3654
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Commit(IntPtr iStream, int grfCommitFlags);

		// Token: 0x06000E47 RID: 3655
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Revert(IntPtr iStream);

		// Token: 0x06000E48 RID: 3656
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_LockRegion(IntPtr iStream, long libOffset, long cb, int dwLockType);

		// Token: 0x06000E49 RID: 3657
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_UnlockRegion(IntPtr iStream, long libOffset, long cb, int dwLockType);

		// Token: 0x06000E4A RID: 3658
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Stat(IntPtr iStream, out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);

		// Token: 0x06000E4B RID: 3659
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IStream_Clone(IntPtr iStream, out SafeExInterfaceHandle iStreamNew);
	}
}
