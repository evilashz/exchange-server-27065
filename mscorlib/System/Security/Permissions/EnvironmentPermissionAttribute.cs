using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002C4 RID: 708
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x0008793C File Offset: 0x00085B3C
		public EnvironmentPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x00087945 File Offset: 0x00085B45
		// (set) Token: 0x0600253F RID: 9535 RVA: 0x0008794D File Offset: 0x00085B4D
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

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00087956 File Offset: 0x00085B56
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x0008795E File Offset: 0x00085B5E
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00087967 File Offset: 0x00085B67
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x00087978 File Offset: 0x00085B78
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_write = value;
				this.m_read = value;
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00087988 File Offset: 0x00085B88
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_read != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, this.m_write);
			}
			return environmentPermission;
		}

		// Token: 0x04000E2D RID: 3629
		private string m_read;

		// Token: 0x04000E2E RID: 3630
		private string m_write;
	}
}
