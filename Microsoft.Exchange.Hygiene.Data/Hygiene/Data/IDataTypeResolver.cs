using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200008C RID: 140
	internal interface IDataTypeResolver
	{
		// Token: 0x06000506 RID: 1286
		Type Resolve(string typeName, Type targetType);
	}
}
