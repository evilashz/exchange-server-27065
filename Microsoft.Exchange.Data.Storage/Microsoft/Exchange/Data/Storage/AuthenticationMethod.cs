using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D60 RID: 3424
	[Flags]
	internal enum AuthenticationMethod
	{
		// Token: 0x04005205 RID: 20997
		None = 0,
		// Token: 0x04005206 RID: 20998
		Basic = 1,
		// Token: 0x04005207 RID: 20999
		Ntlm = 2,
		// Token: 0x04005208 RID: 21000
		Fba = 4,
		// Token: 0x04005209 RID: 21001
		Digest = 8,
		// Token: 0x0400520A RID: 21002
		WindowsIntegrated = 16,
		// Token: 0x0400520B RID: 21003
		LiveIdFba = 32,
		// Token: 0x0400520C RID: 21004
		LiveIdBasic = 64,
		// Token: 0x0400520D RID: 21005
		WSSecurity = 128,
		// Token: 0x0400520E RID: 21006
		Certificate = 256,
		// Token: 0x0400520F RID: 21007
		NegoEx = 512,
		// Token: 0x04005210 RID: 21008
		OAuth = 1024,
		// Token: 0x04005211 RID: 21009
		Adfs = 2048,
		// Token: 0x04005212 RID: 21010
		Kerberos = 4096,
		// Token: 0x04005213 RID: 21011
		Negotiate = 8192,
		// Token: 0x04005214 RID: 21012
		LiveIdNegotiate = 16384
	}
}
