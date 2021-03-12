using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000276 RID: 630
	[XmlType(TypeName = "ItemChoiceType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	public enum QueryType
	{
		// Token: 0x04000C1A RID: 3098
		Items,
		// Token: 0x04000C1B RID: 3099
		Groups
	}
}
