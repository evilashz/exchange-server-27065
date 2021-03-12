using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000925 RID: 2341
	[CLSCompliant(false)]
	public enum HttpAuthenticationScheme : uint
	{
		// Token: 0x04002BA5 RID: 11173
		Undefined,
		// Token: 0x04002BA6 RID: 11174
		Basic,
		// Token: 0x04002BA7 RID: 11175
		Ntlm,
		// Token: 0x04002BA8 RID: 11176
		Passport = 4U,
		// Token: 0x04002BA9 RID: 11177
		Digest = 8U,
		// Token: 0x04002BAA RID: 11178
		Negotiate = 16U,
		// Token: 0x04002BAB RID: 11179
		Certificate = 65536U
	}
}
