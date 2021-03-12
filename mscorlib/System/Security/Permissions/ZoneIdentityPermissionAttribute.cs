using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002CD RID: 717
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x000882D5 File Offset: 0x000864D5
		public ZoneIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000882E5 File Offset: 0x000864E5
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x000882ED File Offset: 0x000864ED
		public SecurityZone Zone
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000882F6 File Offset: 0x000864F6
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.m_flag);
		}

		// Token: 0x04000E4A RID: 3658
		private SecurityZone m_flag = SecurityZone.NoZone;
	}
}
