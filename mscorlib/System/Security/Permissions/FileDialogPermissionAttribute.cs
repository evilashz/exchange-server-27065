using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002C5 RID: 709
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002545 RID: 9541 RVA: 0x000879D6 File Offset: 0x00085BD6
		public FileDialogPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000879DF File Offset: 0x00085BDF
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x000879EC File Offset: 0x00085BEC
		public bool Open
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Open) > FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Open) : (this.m_access & ~FileDialogPermissionAccess.Open));
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x00087A0A File Offset: 0x00085C0A
		// (set) Token: 0x06002549 RID: 9545 RVA: 0x00087A17 File Offset: 0x00085C17
		public bool Save
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Save) > FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Save) : (this.m_access & ~FileDialogPermissionAccess.Save));
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00087A35 File Offset: 0x00085C35
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this.m_access);
		}

		// Token: 0x04000E2F RID: 3631
		private FileDialogPermissionAccess m_access;
	}
}
