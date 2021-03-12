using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000055 RID: 85
	internal enum FxOpcodes
	{
		// Token: 0x04000450 RID: 1104
		None,
		// Token: 0x04000451 RID: 1105
		Config,
		// Token: 0x04000452 RID: 1106
		TransferBuffer,
		// Token: 0x04000453 RID: 1107
		IsInterfaceOk,
		// Token: 0x04000454 RID: 1108
		TellPartnerVersion,
		// Token: 0x04000455 RID: 1109
		StartMdbEventsImport = 11,
		// Token: 0x04000456 RID: 1110
		FinishMdbEventsImport,
		// Token: 0x04000457 RID: 1111
		AddMdbEvents,
		// Token: 0x04000458 RID: 1112
		SetWatermarks,
		// Token: 0x04000459 RID: 1113
		SetReceiveFolder,
		// Token: 0x0400045A RID: 1114
		SetPerUser,
		// Token: 0x0400045B RID: 1115
		SetProps
	}
}
