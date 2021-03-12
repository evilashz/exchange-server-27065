using System;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D8 RID: 216
	internal interface IMigrationMrsClient
	{
		// Token: 0x06000B62 RID: 2914
		bool CanConnectToMrsProxy(Fqdn serverName, Guid mbxGuid, NetworkCredential credentials, out LocalizedException error);
	}
}
