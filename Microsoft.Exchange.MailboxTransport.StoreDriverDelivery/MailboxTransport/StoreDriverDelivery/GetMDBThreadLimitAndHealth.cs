using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000022 RID: 34
	// (Invoke) Token: 0x060001D2 RID: 466
	internal delegate int GetMDBThreadLimitAndHealth(Guid mbGuid, out int databaseHealthMeasure, out List<KeyValuePair<string, double>> monitorHealthValues);
}
