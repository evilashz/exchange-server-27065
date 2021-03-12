using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005A4 RID: 1444
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CallingConventions
	{
		// Token: 0x04001B8A RID: 7050
		[__DynamicallyInvokable]
		Standard = 1,
		// Token: 0x04001B8B RID: 7051
		[__DynamicallyInvokable]
		VarArgs = 2,
		// Token: 0x04001B8C RID: 7052
		[__DynamicallyInvokable]
		Any = 3,
		// Token: 0x04001B8D RID: 7053
		[__DynamicallyInvokable]
		HasThis = 32,
		// Token: 0x04001B8E RID: 7054
		[__DynamicallyInvokable]
		ExplicitThis = 64
	}
}
