using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E20 RID: 3616
	internal class InvalidFilterNodeException : InvalidFilterException
	{
		// Token: 0x06005D5D RID: 23901 RVA: 0x00123069 File Offset: 0x00121269
		public InvalidFilterNodeException(QueryNode queryNode) : base(CoreResources.ErrorInvalidFilterNode)
		{
			this.QueryNode = queryNode;
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06005D5E RID: 23902 RVA: 0x0012307D File Offset: 0x0012127D
		// (set) Token: 0x06005D5F RID: 23903 RVA: 0x00123085 File Offset: 0x00121285
		public QueryNode QueryNode { get; private set; }
	}
}
