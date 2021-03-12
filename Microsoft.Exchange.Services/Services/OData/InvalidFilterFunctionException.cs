using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E21 RID: 3617
	internal class InvalidFilterFunctionException : InvalidFilterNodeException
	{
		// Token: 0x06005D60 RID: 23904 RVA: 0x0012308E File Offset: 0x0012128E
		public InvalidFilterFunctionException(QueryNode queryNode) : base(queryNode)
		{
		}
	}
}
