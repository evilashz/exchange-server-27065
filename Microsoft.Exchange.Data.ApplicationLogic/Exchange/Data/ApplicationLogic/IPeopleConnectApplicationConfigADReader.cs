using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPeopleConnectApplicationConfigADReader
	{
		// Token: 0x06000F02 RID: 3842
		AuthServer ReadFacebookAuthServer();

		// Token: 0x06000F03 RID: 3843
		AuthServer ReadLinkedInAuthServer();

		// Token: 0x06000F04 RID: 3844
		string ReadWebProxyUri();

		// Token: 0x06000F05 RID: 3845
		string ReadFacebookGraphApiEndpoint();

		// Token: 0x06000F06 RID: 3846
		string ReadLinkedInProfileEndpoint();

		// Token: 0x06000F07 RID: 3847
		string ReadLinkedInConnectionsEndpoint();

		// Token: 0x06000F08 RID: 3848
		string ReadLinkedInInvalidateTokenEndpoint();
	}
}
