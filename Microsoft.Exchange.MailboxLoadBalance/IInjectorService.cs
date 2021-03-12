using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[ServiceContract(SessionMode = SessionMode.Required)]
	internal interface IInjectorService : IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600015E RID: 350
		[OperationContract]
		void InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes);

		// Token: 0x0600015F RID: 351
		[OperationContract]
		void InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox);
	}
}
