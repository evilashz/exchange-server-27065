using System;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002B9 RID: 697
	internal abstract class MigrationProxyRpcServer : RpcServerBase
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00030474 File Offset: 0x0002F874
		public static MigrationProxyRpcServer GetServerInstance()
		{
			return (MigrationProxyRpcServer)RpcServerBase.GetServerInstance(MigrationProxyRpcServer.RpcIntfHandle);
		}

		// Token: 0x06000CBA RID: 3258
		public abstract int NspiQueryRows(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle);

		// Token: 0x06000CBB RID: 3259
		public abstract int NspiGetRecipient(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle);

		// Token: 0x06000CBC RID: 3260
		public abstract int NspiSetRecipient(int version, byte[] inBlob, out byte[] outBlob);

		// Token: 0x06000CBD RID: 3261
		public abstract int NspiGetGroupMembers(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle);

		// Token: 0x06000CBE RID: 3262
		public abstract uint NspiRfrGetNewDSA(int version, byte[] inBlob, out byte[] outBlob);

		// Token: 0x06000CBF RID: 3263
		public abstract void AutodiscoverGetUserSettings(int version, byte[] inBlob, out byte[] outBlob);

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00030490 File Offset: 0x0002F890
		public MigrationProxyRpcServer()
		{
		}

		// Token: 0x04000D6F RID: 3439
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.MigrationProxyRpc_v1_0_s_ifspec;
	}
}
