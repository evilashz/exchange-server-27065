using System;

namespace System.Security.AccessControl
{
	// Token: 0x020001FB RID: 507
	public enum ResourceType
	{
		// Token: 0x04000AAE RID: 2734
		Unknown,
		// Token: 0x04000AAF RID: 2735
		FileObject,
		// Token: 0x04000AB0 RID: 2736
		Service,
		// Token: 0x04000AB1 RID: 2737
		Printer,
		// Token: 0x04000AB2 RID: 2738
		RegistryKey,
		// Token: 0x04000AB3 RID: 2739
		LMShare,
		// Token: 0x04000AB4 RID: 2740
		KernelObject,
		// Token: 0x04000AB5 RID: 2741
		WindowObject,
		// Token: 0x04000AB6 RID: 2742
		DSObject,
		// Token: 0x04000AB7 RID: 2743
		DSObjectAll,
		// Token: 0x04000AB8 RID: 2744
		ProviderDefined,
		// Token: 0x04000AB9 RID: 2745
		WmiGuidObject,
		// Token: 0x04000ABA RID: 2746
		RegistryWow6432Key
	}
}
