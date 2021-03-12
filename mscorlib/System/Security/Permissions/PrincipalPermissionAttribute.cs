using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002C8 RID: 712
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x00087D1C File Offset: 0x00085F1C
		public PrincipalPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x00087D2C File Offset: 0x00085F2C
		// (set) Token: 0x06002571 RID: 9585 RVA: 0x00087D34 File Offset: 0x00085F34
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x00087D3D File Offset: 0x00085F3D
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x00087D45 File Offset: 0x00085F45
		public string Role
		{
			get
			{
				return this.m_role;
			}
			set
			{
				this.m_role = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x00087D4E File Offset: 0x00085F4E
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x00087D56 File Offset: 0x00085F56
		public bool Authenticated
		{
			get
			{
				return this.m_authenticated;
			}
			set
			{
				this.m_authenticated = value;
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00087D5F File Offset: 0x00085F5F
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			return new PrincipalPermission(this.m_name, this.m_role, this.m_authenticated);
		}

		// Token: 0x04000E3E RID: 3646
		private string m_name;

		// Token: 0x04000E3F RID: 3647
		private string m_role;

		// Token: 0x04000E40 RID: 3648
		private bool m_authenticated = true;
	}
}
