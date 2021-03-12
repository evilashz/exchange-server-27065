using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200073C RID: 1852
	[Serializable]
	internal enum BinaryTypeEnum
	{
		// Token: 0x04002442 RID: 9282
		Primitive,
		// Token: 0x04002443 RID: 9283
		String,
		// Token: 0x04002444 RID: 9284
		Object,
		// Token: 0x04002445 RID: 9285
		ObjectUrt,
		// Token: 0x04002446 RID: 9286
		ObjectUser,
		// Token: 0x04002447 RID: 9287
		ObjectArray,
		// Token: 0x04002448 RID: 9288
		StringArray,
		// Token: 0x04002449 RID: 9289
		PrimitiveArray
	}
}
