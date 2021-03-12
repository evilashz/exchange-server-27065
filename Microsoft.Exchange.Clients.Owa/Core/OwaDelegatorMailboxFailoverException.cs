using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public class OwaDelegatorMailboxFailoverException : OwaPermanentException
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0005E934 File Offset: 0x0005CB34
		public string MailboxOwnerLegacyDN
		{
			get
			{
				return this.mailboxOwnerLegacyDN;
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0005E93C File Offset: 0x0005CB3C
		public OwaDelegatorMailboxFailoverException(string mailboxOwnerLegacyDN, Exception innerException) : base(null, innerException)
		{
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
		}

		// Token: 0x04000A2A RID: 2602
		private string mailboxOwnerLegacyDN;
	}
}
