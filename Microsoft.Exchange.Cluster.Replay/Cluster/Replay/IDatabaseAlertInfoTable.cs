using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001EC RID: 492
	internal interface IDatabaseAlertInfoTable
	{
		// Token: 0x0600137A RID: 4986
		void RaiseAppropriateAlertIfNecessary(IHealthValidationResultMinimal result);

		// Token: 0x0600137B RID: 4987
		void ResetState(Guid dbGuid);

		// Token: 0x0600137C RID: 4988
		void Cleanup(HashSet<Guid> currentlyKnownDatabaseGuids);
	}
}
