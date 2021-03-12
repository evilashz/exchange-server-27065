using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermissionAttribute : IsolatedStoragePermissionAttribute
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x00088569 File Offset: 0x00086769
		public IsolatedStorageFilePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00088574 File Offset: 0x00086774
		public override IPermission CreatePermission()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission;
			if (this.m_unrestricted)
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			else
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.None);
				isolatedStorageFilePermission.UserQuota = this.m_userQuota;
				isolatedStorageFilePermission.UsageAllowed = this.m_allowed;
			}
			return isolatedStorageFilePermission;
		}
	}
}
