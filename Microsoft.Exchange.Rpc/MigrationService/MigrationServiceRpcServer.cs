using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002BE RID: 702
	internal abstract class MigrationServiceRpcServer : RpcServerBase
	{
		// Token: 0x06000CCC RID: 3276
		public abstract byte[] InvokeMigrationServiceEndPoint(int version, byte[] pInBytes);

		// Token: 0x06000CCD RID: 3277 RVA: 0x000308CC File Offset: 0x0002FCCC
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsRpcConnectionError(int errorCode)
		{
			return errorCode == 1753 || errorCode == 1722 || errorCode == 1727;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00030960 File Offset: 0x0002FD60
		public MigrationServiceRpcServer()
		{
		}

		// Token: 0x04000D71 RID: 3441
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.MigrationService_v1_0_s_ifspec;
	}
}
