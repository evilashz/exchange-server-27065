using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200073B RID: 1851
	[Serializable]
	internal enum BinaryHeaderEnum
	{
		// Token: 0x0400242A RID: 9258
		SerializedStreamHeader,
		// Token: 0x0400242B RID: 9259
		Object,
		// Token: 0x0400242C RID: 9260
		ObjectWithMap,
		// Token: 0x0400242D RID: 9261
		ObjectWithMapAssemId,
		// Token: 0x0400242E RID: 9262
		ObjectWithMapTyped,
		// Token: 0x0400242F RID: 9263
		ObjectWithMapTypedAssemId,
		// Token: 0x04002430 RID: 9264
		ObjectString,
		// Token: 0x04002431 RID: 9265
		Array,
		// Token: 0x04002432 RID: 9266
		MemberPrimitiveTyped,
		// Token: 0x04002433 RID: 9267
		MemberReference,
		// Token: 0x04002434 RID: 9268
		ObjectNull,
		// Token: 0x04002435 RID: 9269
		MessageEnd,
		// Token: 0x04002436 RID: 9270
		Assembly,
		// Token: 0x04002437 RID: 9271
		ObjectNullMultiple256,
		// Token: 0x04002438 RID: 9272
		ObjectNullMultiple,
		// Token: 0x04002439 RID: 9273
		ArraySinglePrimitive,
		// Token: 0x0400243A RID: 9274
		ArraySingleObject,
		// Token: 0x0400243B RID: 9275
		ArraySingleString,
		// Token: 0x0400243C RID: 9276
		CrossAppDomainMap,
		// Token: 0x0400243D RID: 9277
		CrossAppDomainString,
		// Token: 0x0400243E RID: 9278
		CrossAppDomainAssembly,
		// Token: 0x0400243F RID: 9279
		MethodCall,
		// Token: 0x04002440 RID: 9280
		MethodReturn
	}
}
