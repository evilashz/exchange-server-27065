using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002D0 RID: 720
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x00088433 File Offset: 0x00086633
		public UrlIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x0008843C File Offset: 0x0008663C
		// (set) Token: 0x060025CB RID: 9675 RVA: 0x00088444 File Offset: 0x00086644
		public string Url
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0008844D File Offset: 0x0008664D
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.m_url);
		}

		// Token: 0x04000E4F RID: 3663
		private string m_url;
	}
}
