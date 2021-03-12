using System;
using System.Collections.Specialized;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B0 RID: 176
	internal static class RemoteRequestProcessor
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00014FD9 File Offset: 0x000131D9
		internal static bool IsRemoteRequest(NameValueCollection headers)
		{
			return headers["X-OWA-RemoteUserId"] != null;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00014FEC File Offset: 0x000131EC
		internal static string GetRemoteUserId(NameValueCollection headers)
		{
			return headers["X-OWA-RemoteUserId"];
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00014FF9 File Offset: 0x000131F9
		internal static string GetRequesterUserId(NameValueCollection headers)
		{
			return headers["X-OWA-SelfId"];
		}

		// Token: 0x040003CB RID: 971
		internal const string RemoteUserIdHeaderName = "X-OWA-RemoteUserId";

		// Token: 0x040003CC RID: 972
		internal const string UserIdHeaderName = "X-OWA-SelfId";
	}
}
