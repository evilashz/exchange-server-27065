using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200028F RID: 655
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExRpcConnectionInterface : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000BC5 RID: 3013
		int OpenMsgStore(int ulFlags, long ullOpenFlags, string lpszMailboxDN, byte[] pbMailboxGuid, byte[] pbMdbGuid, out string lppszWrongServerDN, IntPtr hToken, byte[] pSidUser, byte[] pSidPrimaryGroup, string lpszUserDN, int ulLcidString, int ulLcidSort, int ulCpid, bool unifiedLogon, string lpszApplicationId, byte[] pbTenantHint, int cbTenantHint, out IExMapiStore iMsgStore);

		// Token: 0x06000BC6 RID: 3014
		int SendAuxBuffer(int ulFlags, int cbAuxBuffer, byte[] pbAuxBuffer, int fForceSend);

		// Token: 0x06000BC7 RID: 3015
		int FlushRPCBuffer(bool fForceSend);

		// Token: 0x06000BC8 RID: 3016
		int GetServerVersion(out int pulVersionMajor, out int pulVersionMinor, out int pulBuildMajor, out int pulBuildMinor);

		// Token: 0x06000BC9 RID: 3017
		int IsDead(out bool pfDead);

		// Token: 0x06000BCA RID: 3018
		int RpcSentToServer(out bool pfRpcSent);

		// Token: 0x06000BCB RID: 3019
		int IsMapiMT(out bool pfMapiMT);

		// Token: 0x06000BCC RID: 3020
		int IsConnectedToMapiServer(out bool pfConnectedToMapiServer);

		// Token: 0x06000BCD RID: 3021
		void ClearStorePerRPCStats();

		// Token: 0x06000BCE RID: 3022
		uint GetStorePerRPCStats(out PerRpcStats pPerRpcPerformanceStatistics);

		// Token: 0x06000BCF RID: 3023
		void ClearRPCStats();

		// Token: 0x06000BD0 RID: 3024
		int GetRPCStats(out RpcStats pRpcStats);

		// Token: 0x06000BD1 RID: 3025
		int SetInternalAccess();

		// Token: 0x06000BD2 RID: 3026
		int ClearInternalAccess();

		// Token: 0x06000BD3 RID: 3027
		void CheckForNotifications();
	}
}
