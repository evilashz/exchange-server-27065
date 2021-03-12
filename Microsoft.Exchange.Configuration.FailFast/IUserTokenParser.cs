using System;
using System.Web;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x02000002 RID: 2
	internal interface IUserTokenParser
	{
		// Token: 0x06000001 RID: 1
		bool TryParseUserToken(HttpContext context, out string userToken);
	}
}
