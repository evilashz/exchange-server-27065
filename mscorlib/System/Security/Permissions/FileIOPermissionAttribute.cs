using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	// Token: 0x020002C6 RID: 710
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x00087A51 File Offset: 0x00085C51
		public FileIOPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x00087A5A File Offset: 0x00085C5A
		// (set) Token: 0x0600254D RID: 9549 RVA: 0x00087A62 File Offset: 0x00085C62
		public string Read
		{
			get
			{
				return this.m_read;
			}
			set
			{
				this.m_read = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x00087A6B File Offset: 0x00085C6B
		// (set) Token: 0x0600254F RID: 9551 RVA: 0x00087A73 File Offset: 0x00085C73
		public string Write
		{
			get
			{
				return this.m_write;
			}
			set
			{
				this.m_write = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x00087A7C File Offset: 0x00085C7C
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x00087A84 File Offset: 0x00085C84
		public string Append
		{
			get
			{
				return this.m_append;
			}
			set
			{
				this.m_append = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00087A8D File Offset: 0x00085C8D
		// (set) Token: 0x06002553 RID: 9555 RVA: 0x00087A95 File Offset: 0x00085C95
		public string PathDiscovery
		{
			get
			{
				return this.m_pathDiscovery;
			}
			set
			{
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x00087A9E File Offset: 0x00085C9E
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x00087AA6 File Offset: 0x00085CA6
		public string ViewAccessControl
		{
			get
			{
				return this.m_viewAccess;
			}
			set
			{
				this.m_viewAccess = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00087AAF File Offset: 0x00085CAF
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x00087AB7 File Offset: 0x00085CB7
		public string ChangeAccessControl
		{
			get
			{
				return this.m_changeAccess;
			}
			set
			{
				this.m_changeAccess = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x00087ADE File Offset: 0x00085CDE
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x00087AC0 File Offset: 0x00085CC0
		[Obsolete("Please use the ViewAndModify property instead.")]
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_read = value;
				this.m_write = value;
				this.m_append = value;
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x00087AEF File Offset: 0x00085CEF
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x00087B00 File Offset: 0x00085D00
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_read = value;
				this.m_write = value;
				this.m_append = value;
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x00087B1E File Offset: 0x00085D1E
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x00087B26 File Offset: 0x00085D26
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_allFiles;
			}
			set
			{
				this.m_allFiles = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x00087B2F File Offset: 0x00085D2F
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x00087B37 File Offset: 0x00085D37
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_allLocalFiles;
			}
			set
			{
				this.m_allLocalFiles = value;
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00087B40 File Offset: 0x00085D40
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			if (this.m_read != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Write, this.m_write);
			}
			if (this.m_append != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Append, this.m_append);
			}
			if (this.m_pathDiscovery != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.PathDiscovery, this.m_pathDiscovery);
			}
			if (this.m_viewAccess != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.View, new string[]
				{
					this.m_viewAccess
				}, false);
			}
			if (this.m_changeAccess != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, new string[]
				{
					this.m_changeAccess
				}, false);
			}
			fileIOPermission.AllFiles = this.m_allFiles;
			fileIOPermission.AllLocalFiles = this.m_allLocalFiles;
			return fileIOPermission;
		}

		// Token: 0x04000E30 RID: 3632
		private string m_read;

		// Token: 0x04000E31 RID: 3633
		private string m_write;

		// Token: 0x04000E32 RID: 3634
		private string m_append;

		// Token: 0x04000E33 RID: 3635
		private string m_pathDiscovery;

		// Token: 0x04000E34 RID: 3636
		private string m_viewAccess;

		// Token: 0x04000E35 RID: 3637
		private string m_changeAccess;

		// Token: 0x04000E36 RID: 3638
		[OptionalField(VersionAdded = 2)]
		private FileIOPermissionAccess m_allLocalFiles;

		// Token: 0x04000E37 RID: 3639
		[OptionalField(VersionAdded = 2)]
		private FileIOPermissionAccess m_allFiles;
	}
}
