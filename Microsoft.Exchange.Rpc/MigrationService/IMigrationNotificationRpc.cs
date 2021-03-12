using System;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x0200029A RID: 666
	internal interface IMigrationNotificationRpc : IDisposable
	{
		// Token: 0x06000C54 RID: 3156
		byte[] UpdateMigrationRequest(int version, byte[] pInBytes);
	}
}
