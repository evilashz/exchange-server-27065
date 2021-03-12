using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000184 RID: 388
	[ComVisible(true)]
	[Serializable]
	public sealed class FileInfo : FileSystemInfo
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0004CF22 File Offset: 0x0004B122
		[SecuritySafeCritical]
		public FileInfo(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.Init(fileName, true);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0004CF40 File Offset: 0x0004B140
		[SecurityCritical]
		private void Init(string fileName, bool checkHost)
		{
			this.OriginalPath = fileName;
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			this._name = Path.GetFileName(fileName);
			this.FullPath = fullPathInternal;
			base.DisplayPath = this.GetDisplayPath(fileName);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0004CF84 File Offset: 0x0004B184
		private string GetDisplayPath(string originalPath)
		{
			return originalPath;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0004CF87 File Offset: 0x0004B187
		[SecurityCritical]
		private FileInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, this.FullPath, false, false);
			this._name = Path.GetFileName(this.OriginalPath);
			base.DisplayPath = this.GetDisplayPath(this.OriginalPath);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0004CFC2 File Offset: 0x0004B1C2
		internal FileInfo(string fullPath, bool ignoreThis)
		{
			this._name = Path.GetFileName(fullPath);
			this.OriginalPath = this._name;
			this.FullPath = fullPath;
			base.DisplayPath = this._name;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0004CFF5 File Offset: 0x0004B1F5
		internal FileInfo(string fullPath, string fileName)
		{
			this._name = fileName;
			this.OriginalPath = this._name;
			this.FullPath = fullPath;
			base.DisplayPath = this._name;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0004D023 File Offset: 0x0004B223
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0004D02C File Offset: 0x0004B22C
		public long Length
		{
			[SecuritySafeCritical]
			get
			{
				if (this._dataInitialised == -1)
				{
					base.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, base.DisplayPath);
				}
				if ((this._data.fileAttributes & 16) != 0)
				{
					__Error.WinIOError(2, base.DisplayPath);
				}
				return (long)this._data.fileSizeHigh << 32 | ((long)this._data.fileSizeLow & (long)((ulong)-1));
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0004D09C File Offset: 0x0004B29C
		public string DirectoryName
		{
			[SecuritySafeCritical]
			get
			{
				string directoryName = Path.GetDirectoryName(this.FullPath);
				if (directoryName != null)
				{
					FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, directoryName, false, false);
				}
				return directoryName;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0004D0C4 File Offset: 0x0004B2C4
		public DirectoryInfo Directory
		{
			get
			{
				string directoryName = this.DirectoryName;
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0004D0E3 File Offset: 0x0004B2E3
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0004D0F0 File Offset: 0x0004B2F0
		public bool IsReadOnly
		{
			get
			{
				return (base.Attributes & FileAttributes.ReadOnly) > (FileAttributes)0;
			}
			set
			{
				if (value)
				{
					base.Attributes |= FileAttributes.ReadOnly;
					return;
				}
				base.Attributes &= ~FileAttributes.ReadOnly;
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0004D113 File Offset: 0x0004B313
		public FileSecurity GetAccessControl()
		{
			return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0004D122 File Offset: 0x0004B322
		public FileSecurity GetAccessControl(AccessControlSections includeSections)
		{
			return File.GetAccessControl(this.FullPath, includeSections);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0004D130 File Offset: 0x0004B330
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			File.SetAccessControl(this.FullPath, fileSecurity);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0004D13E File Offset: 0x0004B33E
		[SecuritySafeCritical]
		public StreamReader OpenText()
		{
			return new StreamReader(this.FullPath, Encoding.UTF8, true, StreamReader.DefaultBufferSize, false);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0004D157 File Offset: 0x0004B357
		public StreamWriter CreateText()
		{
			return new StreamWriter(this.FullPath, false);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0004D165 File Offset: 0x0004B365
		public StreamWriter AppendText()
		{
			return new StreamWriter(this.FullPath, true);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0004D174 File Offset: 0x0004B374
		public FileInfo CopyTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			destFileName = File.InternalCopy(this.FullPath, destFileName, false, true);
			return new FileInfo(destFileName, false);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
		public FileInfo CopyTo(string destFileName, bool overwrite)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			destFileName = File.InternalCopy(this.FullPath, destFileName, overwrite, true);
			return new FileInfo(destFileName, false);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0004D229 File Offset: 0x0004B429
		public FileStream Create()
		{
			return File.Create(this.FullPath);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0004D238 File Offset: 0x0004B438
		[SecuritySafeCritical]
		public override void Delete()
		{
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, this.FullPath, false, false);
			if (!Win32Native.DeleteFile(this.FullPath))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					return;
				}
				__Error.WinIOError(lastWin32Error, base.DisplayPath);
			}
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0004D279 File Offset: 0x0004B479
		[ComVisible(false)]
		public void Decrypt()
		{
			File.Decrypt(this.FullPath);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0004D286 File Offset: 0x0004B486
		[ComVisible(false)]
		public void Encrypt()
		{
			File.Encrypt(this.FullPath);
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0004D294 File Offset: 0x0004B494
		public override bool Exists
		{
			[SecuritySafeCritical]
			get
			{
				bool result;
				try
				{
					if (this._dataInitialised == -1)
					{
						base.Refresh();
					}
					if (this._dataInitialised != 0)
					{
						result = false;
					}
					else
					{
						result = ((this._data.fileAttributes & 16) == 0);
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0004D2E8 File Offset: 0x0004B4E8
		public FileStream Open(FileMode mode)
		{
			return this.Open(mode, FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0004D2F3 File Offset: 0x0004B4F3
		public FileStream Open(FileMode mode, FileAccess access)
		{
			return this.Open(mode, access, FileShare.None);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0004D2FE File Offset: 0x0004B4FE
		public FileStream Open(FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(this.FullPath, mode, access, share);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0004D30E File Offset: 0x0004B50E
		public FileStream OpenRead()
		{
			return new FileStream(this.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0004D324 File Offset: 0x0004B524
		public FileStream OpenWrite()
		{
			return new FileStream(this.FullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0004D334 File Offset: 0x0004B534
		[SecuritySafeCritical]
		public void MoveTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			string fullPathInternal = Path.GetFullPathInternal(destFileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, this.FullPath, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.MoveFile(this.FullPath, fullPathInternal))
			{
				__Error.WinIOError();
			}
			this.FullPath = fullPathInternal;
			this.OriginalPath = destFileName;
			this._name = Path.GetFileName(fullPathInternal);
			base.DisplayPath = this.GetDisplayPath(destFileName);
			this._dataInitialised = -1;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0004D3CB File Offset: 0x0004B5CB
		[ComVisible(false)]
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
		{
			return this.Replace(destinationFileName, destinationBackupFileName, false);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0004D3D6 File Offset: 0x0004B5D6
		[ComVisible(false)]
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			File.Replace(this.FullPath, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
			return new FileInfo(destinationFileName);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0004D3EC File Offset: 0x0004B5EC
		public override string ToString()
		{
			return base.DisplayPath;
		}

		// Token: 0x04000834 RID: 2100
		private string _name;
	}
}
