using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x0200001F RID: 31
	internal class ConnectionSkipException : LocalizedException
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00006C60 File Offset: 0x00004E60
		public ConnectionSkipException(SmtpResponse response) : base(new LocalizedString(response.ToString()))
		{
			this.SmtpResponse = response;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006C81 File Offset: 0x00004E81
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006C89 File Offset: 0x00004E89
		internal SmtpResponse SmtpResponse { get; private set; }
	}
}
