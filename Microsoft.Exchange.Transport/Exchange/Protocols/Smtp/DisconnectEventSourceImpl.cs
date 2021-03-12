using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A8 RID: 1192
	internal class DisconnectEventSourceImpl : DisconnectEventSource
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x000DDA12 File Offset: 0x000DBC12
		private DisconnectEventSourceImpl()
		{
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000DDA1A File Offset: 0x000DBC1A
		public static DisconnectEventSource Create(SmtpSession smtpSession)
		{
			return new DisconnectEventSourceImpl();
		}
	}
}
