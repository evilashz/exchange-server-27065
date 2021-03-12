using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy.Routing.Providers
{
	// Token: 0x0200000A RID: 10
	internal interface IDatabaseLocationProvider
	{
		// Token: 0x0600001C RID: 28
		BackEndServer GetBackEndServerForDatabase(Guid databaseGuid, string domainName, string resourceForest, IRoutingDiagnostics diagnostics);
	}
}
