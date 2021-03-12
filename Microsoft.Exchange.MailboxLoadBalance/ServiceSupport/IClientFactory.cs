using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IClientFactory
	{
		// Token: 0x06000006 RID: 6
		ILoadBalanceService GetLoadBalanceClientForServer(DirectoryServer server, bool allowFallbackToLocal);

		// Token: 0x06000007 RID: 7
		ILoadBalanceService GetLoadBalanceClientForDatabase(DirectoryDatabase database);

		// Token: 0x06000008 RID: 8
		IInjectorService GetInjectorClientForDatabase(DirectoryDatabase database);

		// Token: 0x06000009 RID: 9
		IPhysicalDatabase GetPhysicalDatabaseConnection(DirectoryDatabase database);

		// Token: 0x0600000A RID: 10
		ILoadBalanceService GetLoadBalanceClientForCentralServer();
	}
}
