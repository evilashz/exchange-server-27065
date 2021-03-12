using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005A3 RID: 1443
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum BindingFlags
	{
		// Token: 0x04001B75 RID: 7029
		Default = 0,
		// Token: 0x04001B76 RID: 7030
		[__DynamicallyInvokable]
		IgnoreCase = 1,
		// Token: 0x04001B77 RID: 7031
		[__DynamicallyInvokable]
		DeclaredOnly = 2,
		// Token: 0x04001B78 RID: 7032
		[__DynamicallyInvokable]
		Instance = 4,
		// Token: 0x04001B79 RID: 7033
		[__DynamicallyInvokable]
		Static = 8,
		// Token: 0x04001B7A RID: 7034
		[__DynamicallyInvokable]
		Public = 16,
		// Token: 0x04001B7B RID: 7035
		[__DynamicallyInvokable]
		NonPublic = 32,
		// Token: 0x04001B7C RID: 7036
		[__DynamicallyInvokable]
		FlattenHierarchy = 64,
		// Token: 0x04001B7D RID: 7037
		InvokeMethod = 256,
		// Token: 0x04001B7E RID: 7038
		CreateInstance = 512,
		// Token: 0x04001B7F RID: 7039
		GetField = 1024,
		// Token: 0x04001B80 RID: 7040
		SetField = 2048,
		// Token: 0x04001B81 RID: 7041
		GetProperty = 4096,
		// Token: 0x04001B82 RID: 7042
		SetProperty = 8192,
		// Token: 0x04001B83 RID: 7043
		PutDispProperty = 16384,
		// Token: 0x04001B84 RID: 7044
		PutRefDispProperty = 32768,
		// Token: 0x04001B85 RID: 7045
		[__DynamicallyInvokable]
		ExactBinding = 65536,
		// Token: 0x04001B86 RID: 7046
		SuppressChangeType = 131072,
		// Token: 0x04001B87 RID: 7047
		[__DynamicallyInvokable]
		OptionalParamBinding = 262144,
		// Token: 0x04001B88 RID: 7048
		IgnoreReturn = 16777216
	}
}
