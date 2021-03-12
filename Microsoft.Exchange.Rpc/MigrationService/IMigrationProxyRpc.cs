using System;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002A3 RID: 675
	internal interface IMigrationProxyRpc : IDisposable
	{
		// Token: 0x06000CAC RID: 3244
		int NspiQueryRows(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle);

		// Token: 0x06000CAD RID: 3245
		int NspiGetRecipient(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle);

		// Token: 0x06000CAE RID: 3246
		int NspiSetRecipient(int version, byte[] inBlob, out byte[] outBlob);

		// Token: 0x06000CAF RID: 3247
		int NspiGetGroupMembers(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle);

		// Token: 0x06000CB0 RID: 3248
		int NspiRfrGetNewDSA(int version, byte[] inBlob, out byte[] outBlob);

		// Token: 0x06000CB1 RID: 3249
		void AutodiscoverGetUserSettings(int version, byte[] inBlob, out byte[] outBlob);
	}
}
