using System;
using System.Web;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003E4 RID: 996
	internal interface IE4eLogger
	{
		// Token: 0x06002D7B RID: 11643
		void LogInfo(HttpContext context, string methodName, string messageID, string messageFormat, params object[] args);

		// Token: 0x06002D7C RID: 11644
		void LogError(HttpContext context, string methodName, string messageID, string messageFormat, params object[] args);
	}
}
