using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000748 RID: 1864
	[Flags]
	[Serializable]
	internal enum MessageEnum
	{
		// Token: 0x0400249A RID: 9370
		NoArgs = 1,
		// Token: 0x0400249B RID: 9371
		ArgsInline = 2,
		// Token: 0x0400249C RID: 9372
		ArgsIsArray = 4,
		// Token: 0x0400249D RID: 9373
		ArgsInArray = 8,
		// Token: 0x0400249E RID: 9374
		NoContext = 16,
		// Token: 0x0400249F RID: 9375
		ContextInline = 32,
		// Token: 0x040024A0 RID: 9376
		ContextInArray = 64,
		// Token: 0x040024A1 RID: 9377
		MethodSignatureInArray = 128,
		// Token: 0x040024A2 RID: 9378
		PropertyInArray = 256,
		// Token: 0x040024A3 RID: 9379
		NoReturnValue = 512,
		// Token: 0x040024A4 RID: 9380
		ReturnValueVoid = 1024,
		// Token: 0x040024A5 RID: 9381
		ReturnValueInline = 2048,
		// Token: 0x040024A6 RID: 9382
		ReturnValueInArray = 4096,
		// Token: 0x040024A7 RID: 9383
		ExceptionInArray = 8192,
		// Token: 0x040024A8 RID: 9384
		GenericMethod = 32768
	}
}
