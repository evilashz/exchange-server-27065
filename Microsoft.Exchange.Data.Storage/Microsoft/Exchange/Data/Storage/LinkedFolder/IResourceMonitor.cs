using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000972 RID: 2418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IResourceMonitor
	{
		// Token: 0x0600599C RID: 22940
		void CheckResourceHealth();

		// Token: 0x0600599D RID: 22941
		DelayInfo GetDelay();

		// Token: 0x0600599E RID: 22942
		void StartChargingBudget();

		// Token: 0x0600599F RID: 22943
		void ResetBudget();

		// Token: 0x060059A0 RID: 22944
		IBudget GetBudget();
	}
}
