using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	internal class RetryException : LocalizedException
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		public RetryException(MessageStatus status) : base(Strings.RetryException)
		{
			this.status = status;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000CDD3 File Offset: 0x0000AFD3
		public RetryException(MessageStatus status, string storeDriverContext) : this(status)
		{
			this.storeDriverContext = storeDriverContext;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000CDE3 File Offset: 0x0000AFE3
		public MessageStatus MessageStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000CDEB File Offset: 0x0000AFEB
		public string StoreDriverContext
		{
			get
			{
				return this.storeDriverContext;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Delivery queue needs to be retried later due to {0}", new object[]
			{
				this.status.Exception
			});
		}

		// Token: 0x0400011D RID: 285
		private readonly string storeDriverContext;

		// Token: 0x0400011E RID: 286
		private MessageStatus status;
	}
}
