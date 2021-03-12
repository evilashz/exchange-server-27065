using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.SecureMail;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000474 RID: 1140
	internal sealed class AuthParseOutput
	{
		// Token: 0x06003482 RID: 13442 RVA: 0x000D6332 File Offset: 0x000D4532
		public AuthParseOutput(SmtpAuthenticationMechanism authenticationMechanism, MultilevelAuthMechanism multilevelAuthMechanism, CommandContext initialBlob, string exchangeAuthHashAlgorithm = null)
		{
			this.AuthenticationMechanism = authenticationMechanism;
			this.MultilevelAuthMechanism = multilevelAuthMechanism;
			this.InitialBlob = initialBlob;
			this.ExchangeAuthHashAlgorithm = (string.IsNullOrEmpty(exchangeAuthHashAlgorithm) ? string.Empty : exchangeAuthHashAlgorithm);
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000D6367 File Offset: 0x000D4567
		// (set) Token: 0x06003484 RID: 13444 RVA: 0x000D636F File Offset: 0x000D456F
		public SmtpAuthenticationMechanism AuthenticationMechanism { get; private set; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000D6378 File Offset: 0x000D4578
		// (set) Token: 0x06003486 RID: 13446 RVA: 0x000D6380 File Offset: 0x000D4580
		public MultilevelAuthMechanism MultilevelAuthMechanism { get; private set; }

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000D6389 File Offset: 0x000D4589
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x000D6391 File Offset: 0x000D4591
		public CommandContext InitialBlob { get; private set; }

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000D639A File Offset: 0x000D459A
		// (set) Token: 0x0600348A RID: 13450 RVA: 0x000D63A2 File Offset: 0x000D45A2
		public string ExchangeAuthHashAlgorithm { get; private set; }
	}
}
