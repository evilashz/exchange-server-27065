using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[ServiceContract(SessionMode = SessionMode.Required)]
	internal interface IVersionedService : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600015D RID: 349
		[OperationContract]
		void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion);
	}
}
