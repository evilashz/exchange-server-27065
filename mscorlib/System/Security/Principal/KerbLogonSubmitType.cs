using System;

namespace System.Security.Principal
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	internal enum KerbLogonSubmitType
	{
		// Token: 0x04000F8A RID: 3978
		KerbInteractiveLogon = 2,
		// Token: 0x04000F8B RID: 3979
		KerbSmartCardLogon = 6,
		// Token: 0x04000F8C RID: 3980
		KerbWorkstationUnlockLogon,
		// Token: 0x04000F8D RID: 3981
		KerbSmartCardUnlockLogon,
		// Token: 0x04000F8E RID: 3982
		KerbProxyLogon,
		// Token: 0x04000F8F RID: 3983
		KerbTicketLogon,
		// Token: 0x04000F90 RID: 3984
		KerbTicketUnlockLogon,
		// Token: 0x04000F91 RID: 3985
		KerbS4ULogon
	}
}
