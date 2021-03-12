using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200027F RID: 639
	[XmlType(TypeName = "StandardGroupByType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum StandardGroupBys
	{
		// Token: 0x04000C32 RID: 3122
		[XmlEnum(Name = "ConversationTopic")]
		ConversationTopic
	}
}
