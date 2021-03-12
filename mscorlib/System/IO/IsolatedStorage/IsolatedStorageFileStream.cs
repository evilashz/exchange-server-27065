using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020001B0 RID: 432
	[ComVisible(true)]
	public class IsolatedStorageFileStream : FileStream
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x0005BFBC File Offset: 0x0005A1BC
		private IsolatedStorageFileStream()
		{
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0005BFC4 File Offset: 0x0005A1C4
		public IsolatedStorageFileStream(string path, FileMode mode) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, null)
		{
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0005BFD8 File Offset: 0x0005A1D8
		public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, isf)
		{
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0005BFEC File Offset: 0x0005A1EC
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access) : this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, null)
		{
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0005C005 File Offset: 0x0005A205
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf) : this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, isf)
		{
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0005C01F File Offset: 0x0005A21F
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share) : this(path, mode, access, share, 4096, null)
		{
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0005C032 File Offset: 0x0005A232
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf) : this(path, mode, access, share, 4096, isf)
		{
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0005C046 File Offset: 0x0005A246
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : this(path, mode, access, share, bufferSize, null)
		{
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0005C058 File Offset: 0x0005A258
		[SecuritySafeCritical]
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0 || path.Equals("\\"))
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Path"));
			}
			if (isf == null)
			{
				this.m_OwnedStore = true;
				isf = IsolatedStorageFile.GetUserStoreForDomain();
			}
			if (isf.Disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (mode - FileMode.CreateNew > 5)
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_FileOpenMode"));
			}
			this.m_isf = isf;
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_isf.RootDirectory);
			fileIOPermission.Assert();
			fileIOPermission.PermitOnly();
			this.m_GivenPath = path;
			this.m_FullPath = this.m_isf.GetFullPath(this.m_GivenPath);
			ulong num = 0UL;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				switch (mode)
				{
				case FileMode.CreateNew:
					flag = true;
					break;
				case FileMode.Create:
				case FileMode.OpenOrCreate:
				case FileMode.Truncate:
				case FileMode.Append:
					this.m_isf.Lock(ref flag2);
					try
					{
						num = IsolatedStorageFile.RoundToBlockSize((ulong)LongPathFile.GetLength(this.m_FullPath));
					}
					catch (FileNotFoundException)
					{
						flag = true;
					}
					catch
					{
					}
					break;
				}
				if (flag)
				{
					this.m_isf.ReserveOneBlock();
				}
				try
				{
					this.m_fs = new FileStream(this.m_FullPath, mode, access, share, bufferSize, FileOptions.None, this.m_GivenPath, true, true);
				}
				catch
				{
					if (flag)
					{
						this.m_isf.UnreserveOneBlock();
					}
					throw;
				}
				if (!flag && (mode == FileMode.Truncate || mode == FileMode.Create))
				{
					ulong num2 = IsolatedStorageFile.RoundToBlockSize((ulong)this.m_fs.Length);
					if (num > num2)
					{
						this.m_isf.Unreserve(num - num2);
					}
					else if (num2 > num)
					{
						this.m_isf.Reserve(num2 - num);
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_isf.Unlock();
				}
			}
			CodeAccessPermission.RevertAll();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0005C250 File Offset: 0x0005A450
		public override bool CanRead
		{
			get
			{
				return this.m_fs.CanRead;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0005C25D File Offset: 0x0005A45D
		public override bool CanWrite
		{
			get
			{
				return this.m_fs.CanWrite;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005C26A File Offset: 0x0005A46A
		public override bool CanSeek
		{
			get
			{
				return this.m_fs.CanSeek;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0005C277 File Offset: 0x0005A477
		public override bool IsAsync
		{
			get
			{
				return this.m_fs.IsAsync;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0005C284 File Offset: 0x0005A484
		public override long Length
		{
			get
			{
				return this.m_fs.Length;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0005C291 File Offset: 0x0005A491
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x0005C29E File Offset: 0x0005A49E
		public override long Position
		{
			get
			{
				return this.m_fs.Position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0005C2C4 File Offset: 0x0005A4C4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					try
					{
						if (this.m_fs != null)
						{
							this.m_fs.Close();
						}
					}
					finally
					{
						if (this.m_OwnedStore && this.m_isf != null)
						{
							this.m_isf.Close();
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0005C32C File Offset: 0x0005A52C
		public override void Flush()
		{
			this.m_fs.Flush();
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0005C339 File Offset: 0x0005A539
		public override void Flush(bool flushToDisk)
		{
			this.m_fs.Flush(flushToDisk);
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0005C347 File Offset: 0x0005A547
		[Obsolete("This property has been deprecated.  Please use IsolatedStorageFileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override IntPtr Handle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0005C354 File Offset: 0x0005A554
		public override SafeFileHandle SafeFileHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return null;
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0005C360 File Offset: 0x0005A560
		[SecuritySafeCritical]
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				this.m_isf.Reserve(length, (ulong)value);
				try
				{
					this.ZeroInit(length, (ulong)value);
					this.m_fs.SetLength(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, (ulong)value);
					throw;
				}
				if (length > (ulong)value)
				{
					this.m_isf.UndoReserveOperation((ulong)value, length);
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0005C41C File Offset: 0x0005A61C
		public override void Lock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_fs.Lock(position, length);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0005C456 File Offset: 0x0005A656
		public override void Unlock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_fs.Unlock(position, length);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0005C490 File Offset: 0x0005A690
		private void ZeroInit(ulong oldLen, ulong newLen)
		{
			if (oldLen >= newLen)
			{
				return;
			}
			ulong num = newLen - oldLen;
			byte[] buffer = new byte[1024];
			long position = this.m_fs.Position;
			this.m_fs.Seek((long)oldLen, SeekOrigin.Begin);
			if (num <= 1024UL)
			{
				this.m_fs.Write(buffer, 0, (int)num);
				this.m_fs.Position = position;
				return;
			}
			int num2 = 1024 - (int)(oldLen & 1023UL);
			this.m_fs.Write(buffer, 0, num2);
			num -= (ulong)((long)num2);
			int num3 = (int)(num / 1024UL);
			for (int i = 0; i < num3; i++)
			{
				this.m_fs.Write(buffer, 0, 1024);
			}
			this.m_fs.Write(buffer, 0, (int)(num & 1023UL));
			this.m_fs.Position = position;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0005C563 File Offset: 0x0005A763
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_fs.Read(buffer, offset, count);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0005C573 File Offset: 0x0005A773
		public override int ReadByte()
		{
			return this.m_fs.ReadByte();
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0005C580 File Offset: 0x0005A780
		[SecuritySafeCritical]
		public override long Seek(long offset, SeekOrigin origin)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long result;
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen;
				switch (origin)
				{
				case SeekOrigin.Begin:
					newLen = (ulong)((offset < 0L) ? 0L : offset);
					break;
				case SeekOrigin.Current:
					newLen = (ulong)((this.m_fs.Position + offset < 0L) ? 0L : (this.m_fs.Position + offset));
					break;
				case SeekOrigin.End:
					newLen = (ulong)((this.m_fs.Length + offset < 0L) ? 0L : (this.m_fs.Length + offset));
					break;
				default:
					throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_SeekOrigin"));
				}
				this.m_isf.Reserve(length, newLen);
				try
				{
					this.ZeroInit(length, newLen);
					result = this.m_fs.Seek(offset, origin);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
			return result;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0005C68C File Offset: 0x0005A88C
		[SecuritySafeCritical]
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen = (ulong)(this.m_fs.Position + (long)count);
				this.m_isf.Reserve(length, newLen);
				try
				{
					this.m_fs.Write(buffer, offset, count);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0005C720 File Offset: 0x0005A920
		[SecuritySafeCritical]
		public override void WriteByte(byte value)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen = (ulong)(this.m_fs.Position + 1L);
				this.m_isf.Reserve(length, newLen);
				try
				{
					this.m_fs.WriteByte(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0005C7B4 File Offset: 0x0005A9B4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return this.m_fs.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0005C7C8 File Offset: 0x0005A9C8
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return this.m_fs.EndRead(asyncResult);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0005C7E4 File Offset: 0x0005A9E4
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IAsyncResult result;
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen = (ulong)(this.m_fs.Position + (long)numBytes);
				this.m_isf.Reserve(length, newLen);
				try
				{
					result = this.m_fs.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
			return result;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0005C880 File Offset: 0x0005AA80
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0005C89C File Offset: 0x0005AA9C
		internal void NotPermittedError(string str)
		{
			throw new IsolatedStorageException(str);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0005C8A4 File Offset: 0x0005AAA4
		internal void NotPermittedError()
		{
			this.NotPermittedError(Environment.GetResourceString("IsolatedStorage_Operation_ISFS"));
		}

		// Token: 0x0400096D RID: 2413
		private const int s_BlockSize = 1024;

		// Token: 0x0400096E RID: 2414
		private const string s_BackSlash = "\\";

		// Token: 0x0400096F RID: 2415
		private FileStream m_fs;

		// Token: 0x04000970 RID: 2416
		private IsolatedStorageFile m_isf;

		// Token: 0x04000971 RID: 2417
		private string m_GivenPath;

		// Token: 0x04000972 RID: 2418
		private string m_FullPath;

		// Token: 0x04000973 RID: 2419
		private bool m_OwnedStore;
	}
}
