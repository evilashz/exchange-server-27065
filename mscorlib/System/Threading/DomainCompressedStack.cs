using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	internal sealed class DomainCompressedStack
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000DE3C0 File Offset: 0x000DC5C0
		internal PermissionListSet PLS
		{
			get
			{
				return this.m_pls;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000DE3C8 File Offset: 0x000DC5C8
		internal bool ConstructionHalted
		{
			get
			{
				return this.m_bHaltConstruction;
			}
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000DE3D0 File Offset: 0x000DC5D0
		[SecurityCritical]
		private static DomainCompressedStack CreateManagedObject(IntPtr unmanagedDCS)
		{
			DomainCompressedStack domainCompressedStack = new DomainCompressedStack();
			domainCompressedStack.m_pls = PermissionListSet.CreateCompressedState(unmanagedDCS, out domainCompressedStack.m_bHaltConstruction);
			return domainCompressedStack;
		}

		// Token: 0x06003AB4 RID: 15028
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDescCount(IntPtr dcs);

		// Token: 0x06003AB5 RID: 15029
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDomainPermissionSets(IntPtr dcs, out PermissionSet granted, out PermissionSet refused);

		// Token: 0x06003AB6 RID: 15030
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetDescriptorInfo(IntPtr dcs, int index, out PermissionSet granted, out PermissionSet refused, out Assembly assembly, out FrameSecurityDescriptor fsd);

		// Token: 0x06003AB7 RID: 15031
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IgnoreDomain(IntPtr dcs);

		// Token: 0x040018C2 RID: 6338
		private PermissionListSet m_pls;

		// Token: 0x040018C3 RID: 6339
		private bool m_bHaltConstruction;
	}
}
