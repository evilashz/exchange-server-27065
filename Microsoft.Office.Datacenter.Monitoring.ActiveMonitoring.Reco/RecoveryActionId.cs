using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200003C RID: 60
	public enum RecoveryActionId
	{
		// Token: 0x04000136 RID: 310
		None,
		// Token: 0x04000137 RID: 311
		RestartService,
		// Token: 0x04000138 RID: 312
		RecycleApplicationPool,
		// Token: 0x04000139 RID: 313
		ServerFailover,
		// Token: 0x0400013A RID: 314
		ForceReboot,
		// Token: 0x0400013B RID: 315
		WatsonDump,
		// Token: 0x0400013C RID: 316
		DatabaseFailover,
		// Token: 0x0400013D RID: 317
		MoveClusterGroup,
		// Token: 0x0400013E RID: 318
		ControlService,
		// Token: 0x0400013F RID: 319
		TakeComponentOffline,
		// Token: 0x04000140 RID: 320
		TakeComponentOnline,
		// Token: 0x04000141 RID: 321
		ResumeCatalog,
		// Token: 0x04000142 RID: 322
		Escalate,
		// Token: 0x04000143 RID: 323
		PutDCInMM,
		// Token: 0x04000144 RID: 324
		PutMultipleDCInMM,
		// Token: 0x04000145 RID: 325
		CheckDCInMMEscalate,
		// Token: 0x04000146 RID: 326
		KillProcess,
		// Token: 0x04000147 RID: 327
		RenameNTDSPowerOff,
		// Token: 0x04000148 RID: 328
		MomtRestart,
		// Token: 0x04000149 RID: 329
		RpcClientAccessRestart,
		// Token: 0x0400014A RID: 330
		RemoteForceReboot,
		// Token: 0x0400014B RID: 331
		RestartFastNode,
		// Token: 0x0400014C RID: 332
		DeleteFile,
		// Token: 0x0400014D RID: 333
		SetNetworkAdapter,
		// Token: 0x0400014E RID: 334
		AddRoute,
		// Token: 0x0400014F RID: 335
		ClusterNodeHammerDown,
		// Token: 0x04000150 RID: 336
		ClearLsassCache,
		// Token: 0x04000151 RID: 337
		Watson,
		// Token: 0x04000152 RID: 338
		CollectAndMerge,
		// Token: 0x04000153 RID: 339
		RaiseFailureItem,
		// Token: 0x04000154 RID: 340
		PotentialOneCopyRemoteServerRestartResponder,
		// Token: 0x04000155 RID: 341
		Any = 9999
	}
}
