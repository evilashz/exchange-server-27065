using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IDelegateUserCollectionBridge
	{
		// Token: 0x060005A8 RID: 1448
		IList<Exception> CreateDelegateForwardingRule();

		// Token: 0x060005A9 RID: 1449
		IList<Exception> UpdateSendOnBehalfOfPermissions();

		// Token: 0x060005AA RID: 1450
		IList<Exception> SetFolderPermissions();

		// Token: 0x060005AB RID: 1451
		IList<Exception> SetOulookLocalFreeBusyData();

		// Token: 0x060005AC RID: 1452
		IList<Exception> RollbackDelegateState();
	}
}
