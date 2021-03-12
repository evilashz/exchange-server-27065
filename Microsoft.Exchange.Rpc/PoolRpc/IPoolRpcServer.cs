using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x02000382 RID: 898
	public interface IPoolRpcServer
	{
		// Token: 0x06000FF9 RID: 4089
		int EcPoolConnect(uint flags, Guid poolGuid, ArraySegment<byte> auxiliaryIn, IPoolConnectCompletion completion);

		// Token: 0x06000FFA RID: 4090
		int EcPoolDisconnect(IntPtr contextHandle);

		// Token: 0x06000FFB RID: 4091
		int EcPoolCreateSession(IntPtr contextHandle, ClientSecurityContext callerSecurityContext, byte[] sessionSecurityContext, uint flags, string userDn, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, short[] clientVersion, ArraySegment<byte> auxiliaryIn, IPoolCreateSessionCompletion completion);

		// Token: 0x06000FFC RID: 4092
		int EcPoolCloseSession(IntPtr contextHandle, uint sessionHandle, IPoolCloseSessionCompletion completion);

		// Token: 0x06000FFD RID: 4093
		int EcPoolSessionDoRpc(IntPtr contextHandle, uint sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, IPoolSessionDoRpcCompletion completion);

		// Token: 0x06000FFE RID: 4094
		int EcPoolWaitForNotificationsAsync(IntPtr contextHandle, IPoolWaitForNotificationsCompletion completion);

		// Token: 0x06000FFF RID: 4095
		ushort GetVersionDelta();
	}
}
