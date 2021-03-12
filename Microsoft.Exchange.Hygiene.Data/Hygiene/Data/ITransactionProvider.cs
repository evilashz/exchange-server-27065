using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000092 RID: 146
	internal interface ITransactionProvider
	{
		// Token: 0x06000518 RID: 1304
		void InvokeWithTransaction(Action action, string transactionIdentifier);
	}
}
