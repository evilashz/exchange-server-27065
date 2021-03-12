using System;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002BC RID: 700
	internal interface IMigrationServiceRpc : IDisposable
	{
		// Token: 0x06000CC7 RID: 3271
		byte[] InvokeMigrationServiceEndPoint(int version, byte[] pInBytes);
	}
}
