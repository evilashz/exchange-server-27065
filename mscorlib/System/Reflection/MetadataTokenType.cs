using System;

namespace System.Reflection
{
	// Token: 0x020005CD RID: 1485
	[Serializable]
	internal enum MetadataTokenType
	{
		// Token: 0x04001C78 RID: 7288
		Module,
		// Token: 0x04001C79 RID: 7289
		TypeRef = 16777216,
		// Token: 0x04001C7A RID: 7290
		TypeDef = 33554432,
		// Token: 0x04001C7B RID: 7291
		FieldDef = 67108864,
		// Token: 0x04001C7C RID: 7292
		MethodDef = 100663296,
		// Token: 0x04001C7D RID: 7293
		ParamDef = 134217728,
		// Token: 0x04001C7E RID: 7294
		InterfaceImpl = 150994944,
		// Token: 0x04001C7F RID: 7295
		MemberRef = 167772160,
		// Token: 0x04001C80 RID: 7296
		CustomAttribute = 201326592,
		// Token: 0x04001C81 RID: 7297
		Permission = 234881024,
		// Token: 0x04001C82 RID: 7298
		Signature = 285212672,
		// Token: 0x04001C83 RID: 7299
		Event = 335544320,
		// Token: 0x04001C84 RID: 7300
		Property = 385875968,
		// Token: 0x04001C85 RID: 7301
		ModuleRef = 436207616,
		// Token: 0x04001C86 RID: 7302
		TypeSpec = 452984832,
		// Token: 0x04001C87 RID: 7303
		Assembly = 536870912,
		// Token: 0x04001C88 RID: 7304
		AssemblyRef = 587202560,
		// Token: 0x04001C89 RID: 7305
		File = 637534208,
		// Token: 0x04001C8A RID: 7306
		ExportedType = 654311424,
		// Token: 0x04001C8B RID: 7307
		ManifestResource = 671088640,
		// Token: 0x04001C8C RID: 7308
		GenericPar = 704643072,
		// Token: 0x04001C8D RID: 7309
		MethodSpec = 721420288,
		// Token: 0x04001C8E RID: 7310
		String = 1879048192,
		// Token: 0x04001C8F RID: 7311
		Name = 1895825408,
		// Token: 0x04001C90 RID: 7312
		BaseType = 1912602624,
		// Token: 0x04001C91 RID: 7313
		Invalid = 2147483647
	}
}
