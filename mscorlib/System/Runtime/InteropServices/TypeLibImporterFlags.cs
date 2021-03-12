using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093E RID: 2366
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		// Token: 0x04002AE2 RID: 10978
		None = 0,
		// Token: 0x04002AE3 RID: 10979
		PrimaryInteropAssembly = 1,
		// Token: 0x04002AE4 RID: 10980
		UnsafeInterfaces = 2,
		// Token: 0x04002AE5 RID: 10981
		SafeArrayAsSystemArray = 4,
		// Token: 0x04002AE6 RID: 10982
		TransformDispRetVals = 8,
		// Token: 0x04002AE7 RID: 10983
		PreventClassMembers = 16,
		// Token: 0x04002AE8 RID: 10984
		SerializableValueClasses = 32,
		// Token: 0x04002AE9 RID: 10985
		ImportAsX86 = 256,
		// Token: 0x04002AEA RID: 10986
		ImportAsX64 = 512,
		// Token: 0x04002AEB RID: 10987
		ImportAsItanium = 1024,
		// Token: 0x04002AEC RID: 10988
		ImportAsAgnostic = 2048,
		// Token: 0x04002AED RID: 10989
		ReflectionOnlyLoading = 4096,
		// Token: 0x04002AEE RID: 10990
		NoDefineVersionResource = 8192,
		// Token: 0x04002AEF RID: 10991
		ImportAsArm = 16384
	}
}
