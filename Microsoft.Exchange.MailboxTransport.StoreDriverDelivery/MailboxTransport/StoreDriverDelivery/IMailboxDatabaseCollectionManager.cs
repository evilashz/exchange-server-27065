using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200002C RID: 44
	internal interface IMailboxDatabaseCollectionManager : IDisposable
	{
		// Token: 0x0600022F RID: 559
		IMailboxDatabaseConnectionManager GetConnectionManager(Guid mdbGuid);

		// Token: 0x06000230 RID: 560
		XElement GetDiagnosticInfo(XElement parentElement);

		// Token: 0x06000231 RID: 561
		void UpdateMdbThreadCounters();
	}
}
