using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002E4 RID: 740
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002689 RID: 9865 RVA: 0x0008BBAF File Offset: 0x00089DAF
		public GacIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0008BBB8 File Offset: 0x00089DB8
		public override IPermission CreatePermission()
		{
			return new GacIdentityPermission();
		}
	}
}
