using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000033 RID: 51
	internal class ThreadLimitExceededException : LocalizedException
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00009C76 File Offset: 0x00007E76
		public ThreadLimitExceededException(SmtpResponse response) : base(new LocalizedString(response.ToString()))
		{
			this.SmtpResponse = response;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009C97 File Offset: 0x00007E97
		public ThreadLimitExceededException(string message) : base(new LocalizedString(message))
		{
			this.SmtpResponse = SmtpResponse.Empty;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009CB0 File Offset: 0x00007EB0
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009CB8 File Offset: 0x00007EB8
		internal SmtpResponse SmtpResponse { get; private set; }
	}
}
