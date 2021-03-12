using System;
using System.Collections.Generic;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D9 RID: 217
	internal interface IMigrationNspiClient
	{
		// Token: 0x06000B63 RID: 2915
		IList<PropRow> QueryRows(ExchangeOutlookAnywhereEndpoint connectionSettings, int? batchSize, int? startIndex, long[] longPropTags);

		// Token: 0x06000B64 RID: 2916
		PropRow GetRecipient(ExchangeOutlookAnywhereEndpoint connectionSettings, string recipientSmtpAddress, long[] longPropTags);

		// Token: 0x06000B65 RID: 2917
		void SetRecipient(ExchangeOutlookAnywhereEndpoint connectionSettings, string recipientSmtpAddress, string recipientLegDN, string[] propTagValues, long[] longPropTags);

		// Token: 0x06000B66 RID: 2918
		IList<PropRow> GetGroupMembers(ExchangeOutlookAnywhereEndpoint connectionSettings, string groupSmtpAddress);

		// Token: 0x06000B67 RID: 2919
		string GetNewDSA(ExchangeOutlookAnywhereEndpoint connectionSettings);
	}
}
