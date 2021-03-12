using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D7 RID: 1495
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodAttributes
	{
		// Token: 0x04001CAD RID: 7341
		[__DynamicallyInvokable]
		MemberAccessMask = 7,
		// Token: 0x04001CAE RID: 7342
		[__DynamicallyInvokable]
		PrivateScope = 0,
		// Token: 0x04001CAF RID: 7343
		[__DynamicallyInvokable]
		Private = 1,
		// Token: 0x04001CB0 RID: 7344
		[__DynamicallyInvokable]
		FamANDAssem = 2,
		// Token: 0x04001CB1 RID: 7345
		[__DynamicallyInvokable]
		Assembly = 3,
		// Token: 0x04001CB2 RID: 7346
		[__DynamicallyInvokable]
		Family = 4,
		// Token: 0x04001CB3 RID: 7347
		[__DynamicallyInvokable]
		FamORAssem = 5,
		// Token: 0x04001CB4 RID: 7348
		[__DynamicallyInvokable]
		Public = 6,
		// Token: 0x04001CB5 RID: 7349
		[__DynamicallyInvokable]
		Static = 16,
		// Token: 0x04001CB6 RID: 7350
		[__DynamicallyInvokable]
		Final = 32,
		// Token: 0x04001CB7 RID: 7351
		[__DynamicallyInvokable]
		Virtual = 64,
		// Token: 0x04001CB8 RID: 7352
		[__DynamicallyInvokable]
		HideBySig = 128,
		// Token: 0x04001CB9 RID: 7353
		[__DynamicallyInvokable]
		CheckAccessOnOverride = 512,
		// Token: 0x04001CBA RID: 7354
		[__DynamicallyInvokable]
		VtableLayoutMask = 256,
		// Token: 0x04001CBB RID: 7355
		[__DynamicallyInvokable]
		ReuseSlot = 0,
		// Token: 0x04001CBC RID: 7356
		[__DynamicallyInvokable]
		NewSlot = 256,
		// Token: 0x04001CBD RID: 7357
		[__DynamicallyInvokable]
		Abstract = 1024,
		// Token: 0x04001CBE RID: 7358
		[__DynamicallyInvokable]
		SpecialName = 2048,
		// Token: 0x04001CBF RID: 7359
		[__DynamicallyInvokable]
		PinvokeImpl = 8192,
		// Token: 0x04001CC0 RID: 7360
		[__DynamicallyInvokable]
		UnmanagedExport = 8,
		// Token: 0x04001CC1 RID: 7361
		[__DynamicallyInvokable]
		RTSpecialName = 4096,
		// Token: 0x04001CC2 RID: 7362
		ReservedMask = 53248,
		// Token: 0x04001CC3 RID: 7363
		[__DynamicallyInvokable]
		HasSecurity = 16384,
		// Token: 0x04001CC4 RID: 7364
		[__DynamicallyInvokable]
		RequireSecObject = 32768
	}
}
