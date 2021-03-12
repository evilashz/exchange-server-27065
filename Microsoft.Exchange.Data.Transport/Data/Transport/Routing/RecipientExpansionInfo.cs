using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x02000087 RID: 135
	public struct RecipientExpansionInfo
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00008030 File Offset: 0x00006230
		public RecipientExpansionInfo(EnvelopeRecipient removeRecipient, RoutingAddress[] addresses)
		{
			this = new RecipientExpansionInfo(removeRecipient, addresses, SmtpResponse.Empty);
			this.generateDSN = false;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008048 File Offset: 0x00006248
		public RecipientExpansionInfo(EnvelopeRecipient removeRecipient, RoutingAddress[] addresses, SmtpResponse smtpResponse)
		{
			if (removeRecipient == null)
			{
				throw new ArgumentNullException("removeRecipient");
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			for (int i = 0; i < addresses.Length; i++)
			{
				if (!addresses[i].IsValid)
				{
					throw new ArgumentException(string.Format("The specified address is an invalid SMTP address - {0}", addresses[i]));
				}
			}
			this.removeRecipient = removeRecipient;
			this.addresses = addresses;
			this.response = smtpResponse;
			this.generateDSN = true;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000080CA File Offset: 0x000062CA
		public EnvelopeRecipient RemoveRecipient
		{
			get
			{
				return this.removeRecipient;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000080D2 File Offset: 0x000062D2
		public RoutingAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000080DA File Offset: 0x000062DA
		public SmtpResponse SmtpResponse
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000080E2 File Offset: 0x000062E2
		internal bool GenerateDSN
		{
			get
			{
				return this.generateDSN;
			}
		}

		// Token: 0x040001F8 RID: 504
		private EnvelopeRecipient removeRecipient;

		// Token: 0x040001F9 RID: 505
		private RoutingAddress[] addresses;

		// Token: 0x040001FA RID: 506
		private SmtpResponse response;

		// Token: 0x040001FB RID: 507
		private bool generateDSN;
	}
}
