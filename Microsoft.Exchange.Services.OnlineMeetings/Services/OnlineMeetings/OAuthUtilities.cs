using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000019 RID: 25
	internal class OAuthUtilities
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000303C File Offset: 0x0000123C
		internal static string ServerVersionString
		{
			get
			{
				if (OAuthUtilities.serverVersion == null)
				{
					OAuthUtilities.serverVersion = LocalServer.GetServer().AdminDisplayVersion;
				}
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					OAuthUtilities.serverVersion.Major,
					OAuthUtilities.serverVersion.Minor,
					OAuthUtilities.serverVersion.Build,
					OAuthUtilities.serverVersion.Revision
				});
			}
		}

		// Token: 0x040000CD RID: 205
		public const string ClientRequestIdHeaderString = "client-request-id";

		// Token: 0x040000CE RID: 206
		public const string ReturnClientRequestIdHeaderString = "return-client-request-id";

		// Token: 0x040000CF RID: 207
		private static ServerVersion serverVersion;
	}
}
