using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200064D RID: 1613
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ContainmentComparisonType
	{
		// Token: 0x04001C85 RID: 7301
		Exact,
		// Token: 0x04001C86 RID: 7302
		IgnoreCase,
		// Token: 0x04001C87 RID: 7303
		IgnoreNonSpacingCharacters,
		// Token: 0x04001C88 RID: 7304
		Loose,
		// Token: 0x04001C89 RID: 7305
		IgnoreCaseAndNonSpacingCharacters,
		// Token: 0x04001C8A RID: 7306
		LooseAndIgnoreCase,
		// Token: 0x04001C8B RID: 7307
		LooseAndIgnoreNonSpace,
		// Token: 0x04001C8C RID: 7308
		LooseAndIgnoreCaseAndIgnoreNonSpace
	}
}
