using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	// Token: 0x020002CA RID: 714
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002583 RID: 9603 RVA: 0x00087E69 File Offset: 0x00086069
		public RegistryPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x00087E72 File Offset: 0x00086072
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x00087E7A File Offset: 0x0008607A
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

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00087E83 File Offset: 0x00086083
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x00087E8B File Offset: 0x0008608B
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

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x00087E94 File Offset: 0x00086094
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x00087E9C File Offset: 0x0008609C
		public string Create
		{
			get
			{
				return this.m_create;
			}
			set
			{
				this.m_create = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x00087EA5 File Offset: 0x000860A5
		// (set) Token: 0x0600258B RID: 9611 RVA: 0x00087EAD File Offset: 0x000860AD
		public string ViewAccessControl
		{
			get
			{
				return this.m_viewAcl;
			}
			set
			{
				this.m_viewAcl = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x00087EB6 File Offset: 0x000860B6
		// (set) Token: 0x0600258D RID: 9613 RVA: 0x00087EBE File Offset: 0x000860BE
		public string ChangeAccessControl
		{
			get
			{
				return this.m_changeAcl;
			}
			set
			{
				this.m_changeAcl = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x00087EC7 File Offset: 0x000860C7
		// (set) Token: 0x0600258F RID: 9615 RVA: 0x00087ED8 File Offset: 0x000860D8
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
				this.m_create = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00087EEF File Offset: 0x000860EF
		// (set) Token: 0x06002591 RID: 9617 RVA: 0x00087F00 File Offset: 0x00086100
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
				this.m_create = value;
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00087F18 File Offset: 0x00086118
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
			if (this.m_read != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, this.m_write);
			}
			if (this.m_create != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, this.m_create);
			}
			if (this.m_viewAcl != null)
			{
				registryPermission.SetPathList(AccessControlActions.View, this.m_viewAcl);
			}
			if (this.m_changeAcl != null)
			{
				registryPermission.SetPathList(AccessControlActions.Change, this.m_changeAcl);
			}
			return registryPermission;
		}

		// Token: 0x04000E42 RID: 3650
		private string m_read;

		// Token: 0x04000E43 RID: 3651
		private string m_write;

		// Token: 0x04000E44 RID: 3652
		private string m_create;

		// Token: 0x04000E45 RID: 3653
		private string m_viewAcl;

		// Token: 0x04000E46 RID: 3654
		private string m_changeAcl;
	}
}
