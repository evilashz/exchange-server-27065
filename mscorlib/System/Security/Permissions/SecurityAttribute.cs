using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002C2 RID: 706
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		// Token: 0x06002535 RID: 9525 RVA: 0x000878C0 File Offset: 0x00085AC0
		protected SecurityAttribute(SecurityAction action)
		{
			this.m_action = action;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x000878CF File Offset: 0x00085ACF
		// (set) Token: 0x06002537 RID: 9527 RVA: 0x000878D7 File Offset: 0x00085AD7
		public SecurityAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000878E0 File Offset: 0x00085AE0
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x000878E8 File Offset: 0x00085AE8
		public bool Unrestricted
		{
			get
			{
				return this.m_unrestricted;
			}
			set
			{
				this.m_unrestricted = value;
			}
		}

		// Token: 0x0600253A RID: 9530
		public abstract IPermission CreatePermission();

		// Token: 0x0600253B RID: 9531 RVA: 0x000878F4 File Offset: 0x00085AF4
		[SecurityCritical]
		internal static IntPtr FindSecurityAttributeTypeHandle(string typeName)
		{
			PermissionSet.s_fullTrust.Assert();
			Type type = Type.GetType(typeName, false, false);
			if (type == null)
			{
				return IntPtr.Zero;
			}
			return type.TypeHandle.Value;
		}

		// Token: 0x04000E2B RID: 3627
		internal SecurityAction m_action;

		// Token: 0x04000E2C RID: 3628
		internal bool m_unrestricted;
	}
}
