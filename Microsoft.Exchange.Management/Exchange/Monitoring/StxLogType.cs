using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200002A RID: 42
	internal enum StxLogType
	{
		// Token: 0x040000D6 RID: 214
		TestLiveIdAuthentication,
		// Token: 0x040000D7 RID: 215
		TestNtlmConnectivity,
		// Token: 0x040000D8 RID: 216
		TestActiveDirectoryConnectivity,
		// Token: 0x040000D9 RID: 217
		TestTopologyService,
		// Token: 0x040000DA RID: 218
		TestGlobalLocatorService,
		// Token: 0x040000DB RID: 219
		TestForwardFullSync,
		// Token: 0x040000DC RID: 220
		TestForwardSyncCookie,
		// Token: 0x040000DD RID: 221
		TestForwardSyncCookieResponder,
		// Token: 0x040000DE RID: 222
		TestForwardSyncCompanyProbe,
		// Token: 0x040000DF RID: 223
		TestForwardSyncCompanyResponder,
		// Token: 0x040000E0 RID: 224
		DatabaseAvailability,
		// Token: 0x040000E1 RID: 225
		TestRidMonitor,
		// Token: 0x040000E2 RID: 226
		TestRidSetMonitor,
		// Token: 0x040000E3 RID: 227
		TestActiveDirectorySelfCheck,
		// Token: 0x040000E4 RID: 228
		TenantRelocationErrorMonitor,
		// Token: 0x040000E5 RID: 229
		SharedConfigurationTenantMonitor,
		// Token: 0x040000E6 RID: 230
		TestActivedirectoryConnectivityForConfigDC,
		// Token: 0x040000E7 RID: 231
		SyntheticReplicationTransaction,
		// Token: 0x040000E8 RID: 232
		SyntheticReplicationMonitor,
		// Token: 0x040000E9 RID: 233
		PassiveReplicationMonitor,
		// Token: 0x040000EA RID: 234
		PassiveADReplicationMonitor,
		// Token: 0x040000EB RID: 235
		PassiveReplicationPerfCounterProbe,
		// Token: 0x040000EC RID: 236
		RemoteDomainControllerStateProbe,
		// Token: 0x040000ED RID: 237
		TrustMonitorProbe,
		// Token: 0x040000EE RID: 238
		TestKDCService,
		// Token: 0x040000EF RID: 239
		TestDoMTConnectivity,
		// Token: 0x040000F0 RID: 240
		TestOfflineGLS
	}
}
