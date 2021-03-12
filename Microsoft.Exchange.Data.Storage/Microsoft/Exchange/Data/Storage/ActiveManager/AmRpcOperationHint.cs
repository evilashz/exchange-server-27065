using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000312 RID: 786
	internal enum AmRpcOperationHint
	{
		// Token: 0x040014C4 RID: 5316
		Gsfd,
		// Token: 0x040014C5 RID: 5317
		Mount,
		// Token: 0x040014C6 RID: 5318
		Remount,
		// Token: 0x040014C7 RID: 5319
		Dismount,
		// Token: 0x040014C8 RID: 5320
		MoveEx,
		// Token: 0x040014C9 RID: 5321
		GetPAM,
		// Token: 0x040014CA RID: 5322
		AcllDirect,
		// Token: 0x040014CB RID: 5323
		AcllDirect2,
		// Token: 0x040014CC RID: 5324
		AcllDirect3,
		// Token: 0x040014CD RID: 5325
		MountDirectEx,
		// Token: 0x040014CE RID: 5326
		DismountDirect,
		// Token: 0x040014CF RID: 5327
		SwitchOver,
		// Token: 0x040014D0 RID: 5328
		IsRunning,
		// Token: 0x040014D1 RID: 5329
		GetAmRole,
		// Token: 0x040014D2 RID: 5330
		ReportSystemEvent,
		// Token: 0x040014D3 RID: 5331
		CheckThirdPartyListener,
		// Token: 0x040014D4 RID: 5332
		GetAutomountConsensusState,
		// Token: 0x040014D5 RID: 5333
		SetAutomountConsensusState,
		// Token: 0x040014D6 RID: 5334
		MoveAllDatabases,
		// Token: 0x040014D7 RID: 5335
		MoveEx2,
		// Token: 0x040014D8 RID: 5336
		AmRefreshConfiguration,
		// Token: 0x040014D9 RID: 5337
		MoveEx3,
		// Token: 0x040014DA RID: 5338
		MoveAllDatabases2,
		// Token: 0x040014DB RID: 5339
		MountWithAmFlags,
		// Token: 0x040014DC RID: 5340
		ReportServiceKill,
		// Token: 0x040014DD RID: 5341
		GetDeferredRecoveryEntries,
		// Token: 0x040014DE RID: 5342
		MoveAllDatabases3,
		// Token: 0x040014DF RID: 5343
		GenericRpc
	}
}
