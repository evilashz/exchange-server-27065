using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAsyncOperationFactory
	{
		// Token: 0x06000218 RID: 536
		AsyncOperation Create(string requestType, HttpContextBase context);
	}
}
