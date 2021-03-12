using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067F RID: 1663
	internal struct StoreTransactionOperation
	{
		// Token: 0x040021A9 RID: 8617
		[MarshalAs(UnmanagedType.U4)]
		public StoreTransactionOperationType Operation;

		// Token: 0x040021AA RID: 8618
		public StoreTransactionData Data;
	}
}
