using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200033B RID: 827
	public enum DataWipeReason
	{
		// Token: 0x04000F56 RID: 3926
		None,
		// Token: 0x04000F57 RID: 3927
		AccountDeletedInMowa,
		// Token: 0x04000F58 RID: 3928
		PasswordAttemptsExceeded,
		// Token: 0x04000F59 RID: 3929
		MowaDisabled,
		// Token: 0x04000F5A RID: 3930
		RemoteWipe,
		// Token: 0x04000F5B RID: 3931
		MaxDevicePartnerships,
		// Token: 0x04000F5C RID: 3932
		AccountDeletedInAccountManager,
		// Token: 0x04000F5D RID: 3933
		PartnerAppNotifiesOfWipe
	}
}
