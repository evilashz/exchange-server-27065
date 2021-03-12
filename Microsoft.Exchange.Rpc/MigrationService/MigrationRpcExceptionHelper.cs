using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002BB RID: 699
	internal class MigrationRpcExceptionHelper
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x000307B0 File Offset: 0x0002FBB0
		public static void ThrowRpcException(int status, string routineName)
		{
			throw new SyncMigrationRpcTransientException(string.Format("Error 0x{0:x} ({2}) from {1}", status, routineName, new Win32Exception(status).Message), status);
		}
	}
}
