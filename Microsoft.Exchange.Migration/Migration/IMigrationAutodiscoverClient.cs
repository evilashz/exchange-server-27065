using System;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D7 RID: 215
	internal interface IMigrationAutodiscoverClient
	{
		// Token: 0x06000B60 RID: 2912
		AutodiscoverClientResponse GetUserSettings(ExchangeOutlookAnywhereEndpoint endpoint, string emailAddress);

		// Token: 0x06000B61 RID: 2913
		AutodiscoverClientResponse GetUserSettings(string userName, string encryptedPassword, string userDomain, string emailAddress);
	}
}
