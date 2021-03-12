using System;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000017 RID: 23
	internal interface IRetrieverPropertyDefinition
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E7 RID: 231
		string Name { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E8 RID: 232
		PropertyDefinitionAttribute Attributes { get; }
	}
}
