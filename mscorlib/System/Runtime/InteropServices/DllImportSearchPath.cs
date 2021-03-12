using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000905 RID: 2309
	[Flags]
	[__DynamicallyInvokable]
	public enum DllImportSearchPath
	{
		// Token: 0x04002A4F RID: 10831
		[__DynamicallyInvokable]
		UseDllDirectoryForDependencies = 256,
		// Token: 0x04002A50 RID: 10832
		[__DynamicallyInvokable]
		ApplicationDirectory = 512,
		// Token: 0x04002A51 RID: 10833
		[__DynamicallyInvokable]
		UserDirectories = 1024,
		// Token: 0x04002A52 RID: 10834
		[__DynamicallyInvokable]
		System32 = 2048,
		// Token: 0x04002A53 RID: 10835
		[__DynamicallyInvokable]
		SafeDirectories = 4096,
		// Token: 0x04002A54 RID: 10836
		[__DynamicallyInvokable]
		AssemblyDirectory = 2,
		// Token: 0x04002A55 RID: 10837
		[__DynamicallyInvokable]
		LegacyBehavior = 0
	}
}
