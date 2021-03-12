using System;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000075 RID: 117
	internal interface IADServerInfoMapper
	{
		// Token: 0x06000551 RID: 1361
		ADRole GetADRole(ADServerInfo adServerInfo);

		// Token: 0x06000552 RID: 1362
		string GetMappedFqdn(string serverFqdn);

		// Token: 0x06000553 RID: 1363
		int GetMappedPortNumber(string serverFqdn, string forestFqdn, int portNumber);

		// Token: 0x06000554 RID: 1364
		AuthType GetMappedAuthType(string serverFqdn, AuthType authType);
	}
}
