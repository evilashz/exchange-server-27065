using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000227 RID: 551
	internal interface IOperationRetryManager
	{
		// Token: 0x0600133B RID: 4923
		OperationRetryManagerResult TryRun(Action operation);

		// Token: 0x0600133C RID: 4924
		void Run(Action operation);
	}
}
