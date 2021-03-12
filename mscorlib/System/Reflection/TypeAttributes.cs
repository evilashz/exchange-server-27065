using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005F7 RID: 1527
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TypeAttributes
	{
		// Token: 0x04001D66 RID: 7526
		[__DynamicallyInvokable]
		VisibilityMask = 7,
		// Token: 0x04001D67 RID: 7527
		[__DynamicallyInvokable]
		NotPublic = 0,
		// Token: 0x04001D68 RID: 7528
		[__DynamicallyInvokable]
		Public = 1,
		// Token: 0x04001D69 RID: 7529
		[__DynamicallyInvokable]
		NestedPublic = 2,
		// Token: 0x04001D6A RID: 7530
		[__DynamicallyInvokable]
		NestedPrivate = 3,
		// Token: 0x04001D6B RID: 7531
		[__DynamicallyInvokable]
		NestedFamily = 4,
		// Token: 0x04001D6C RID: 7532
		[__DynamicallyInvokable]
		NestedAssembly = 5,
		// Token: 0x04001D6D RID: 7533
		[__DynamicallyInvokable]
		NestedFamANDAssem = 6,
		// Token: 0x04001D6E RID: 7534
		[__DynamicallyInvokable]
		NestedFamORAssem = 7,
		// Token: 0x04001D6F RID: 7535
		[__DynamicallyInvokable]
		LayoutMask = 24,
		// Token: 0x04001D70 RID: 7536
		[__DynamicallyInvokable]
		AutoLayout = 0,
		// Token: 0x04001D71 RID: 7537
		[__DynamicallyInvokable]
		SequentialLayout = 8,
		// Token: 0x04001D72 RID: 7538
		[__DynamicallyInvokable]
		ExplicitLayout = 16,
		// Token: 0x04001D73 RID: 7539
		[__DynamicallyInvokable]
		ClassSemanticsMask = 32,
		// Token: 0x04001D74 RID: 7540
		[__DynamicallyInvokable]
		Class = 0,
		// Token: 0x04001D75 RID: 7541
		[__DynamicallyInvokable]
		Interface = 32,
		// Token: 0x04001D76 RID: 7542
		[__DynamicallyInvokable]
		Abstract = 128,
		// Token: 0x04001D77 RID: 7543
		[__DynamicallyInvokable]
		Sealed = 256,
		// Token: 0x04001D78 RID: 7544
		[__DynamicallyInvokable]
		SpecialName = 1024,
		// Token: 0x04001D79 RID: 7545
		[__DynamicallyInvokable]
		Import = 4096,
		// Token: 0x04001D7A RID: 7546
		[__DynamicallyInvokable]
		Serializable = 8192,
		// Token: 0x04001D7B RID: 7547
		[ComVisible(false)]
		[__DynamicallyInvokable]
		WindowsRuntime = 16384,
		// Token: 0x04001D7C RID: 7548
		[__DynamicallyInvokable]
		StringFormatMask = 196608,
		// Token: 0x04001D7D RID: 7549
		[__DynamicallyInvokable]
		AnsiClass = 0,
		// Token: 0x04001D7E RID: 7550
		[__DynamicallyInvokable]
		UnicodeClass = 65536,
		// Token: 0x04001D7F RID: 7551
		[__DynamicallyInvokable]
		AutoClass = 131072,
		// Token: 0x04001D80 RID: 7552
		[__DynamicallyInvokable]
		CustomFormatClass = 196608,
		// Token: 0x04001D81 RID: 7553
		[__DynamicallyInvokable]
		CustomFormatMask = 12582912,
		// Token: 0x04001D82 RID: 7554
		[__DynamicallyInvokable]
		BeforeFieldInit = 1048576,
		// Token: 0x04001D83 RID: 7555
		ReservedMask = 264192,
		// Token: 0x04001D84 RID: 7556
		[__DynamicallyInvokable]
		RTSpecialName = 2048,
		// Token: 0x04001D85 RID: 7557
		[__DynamicallyInvokable]
		HasSecurity = 262144
	}
}
