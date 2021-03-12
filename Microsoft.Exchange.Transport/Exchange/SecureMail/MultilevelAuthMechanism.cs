using System;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x0200051A RID: 1306
	internal enum MultilevelAuthMechanism
	{
		// Token: 0x04001F07 RID: 7943
		None,
		// Token: 0x04001F08 RID: 7944
		SenderId,
		// Token: 0x04001F09 RID: 7945
		MutualTLS,
		// Token: 0x04001F0A RID: 7946
		MapiSubmit,
		// Token: 0x04001F0B RID: 7947
		SecureMapiSubmit,
		// Token: 0x04001F0C RID: 7948
		SecureInternalSubmit,
		// Token: 0x04001F0D RID: 7949
		TLSAuthLogin,
		// Token: 0x04001F0E RID: 7950
		Login,
		// Token: 0x04001F0F RID: 7951
		Pickup,
		// Token: 0x04001F10 RID: 7952
		NTLM = 10,
		// Token: 0x04001F11 RID: 7953
		GSSAPI,
		// Token: 0x04001F12 RID: 7954
		MUTUALGSSAPI,
		// Token: 0x04001F13 RID: 7955
		Custom,
		// Token: 0x04001F14 RID: 7956
		SecureExternalSubmit = 16,
		// Token: 0x04001F15 RID: 7957
		DirectTrustTLS,
		// Token: 0x04001F16 RID: 7958
		Replay
	}
}
