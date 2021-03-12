using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002CF RID: 719
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060025C5 RID: 9669 RVA: 0x000883EE File Offset: 0x000865EE
		public SiteIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000883F7 File Offset: 0x000865F7
		// (set) Token: 0x060025C7 RID: 9671 RVA: 0x000883FF File Offset: 0x000865FF
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				this.m_site = value;
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x00088408 File Offset: 0x00086608
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_site == null)
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this.m_site);
		}

		// Token: 0x04000E4E RID: 3662
		private string m_site;
	}
}
