using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Net.WSSecurity;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000778 RID: 1912
	internal class ExternalIdentity : WindowsIdentity
	{
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000CA2C7 File Offset: 0x000C84C7
		public new string AuthenticationType
		{
			get
			{
				return "External";
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000CA2CE File Offset: 0x000C84CE
		public override bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000CA2D1 File Offset: 0x000C84D1
		public override string Name
		{
			get
			{
				return this.emailAddress.ToString();
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000CA2E4 File Offset: 0x000C84E4
		public SmtpAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000CA2EC File Offset: 0x000C84EC
		public SmtpAddress ExternalId
		{
			get
			{
				return this.externalId;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x000CA2F4 File Offset: 0x000C84F4
		public Offer Offer
		{
			get
			{
				return this.offer;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000CA2FC File Offset: 0x000C84FC
		public WSSecurityHeader WSSecurityHeader
		{
			get
			{
				return this.wsSecurityHeader;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000CA304 File Offset: 0x000C8504
		public SharingSecurityHeader SharingSecurityHeader
		{
			get
			{
				return this.sharingSecurityHeader;
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000CA30C File Offset: 0x000C850C
		internal ExternalIdentity(SmtpAddress emailAddress, SmtpAddress externalId, WSSecurityHeader wsSecurityHeader, SharingSecurityHeader sharingSecurityHeader, Offer offer, IntPtr token) : base(token)
		{
			this.emailAddress = emailAddress;
			this.externalId = externalId;
			this.wsSecurityHeader = wsSecurityHeader;
			this.sharingSecurityHeader = sharingSecurityHeader;
			this.offer = offer;
		}

		// Token: 0x04001FE1 RID: 8161
		private const string ExternalAuthenticationType = "External";

		// Token: 0x04001FE2 RID: 8162
		private SmtpAddress emailAddress;

		// Token: 0x04001FE3 RID: 8163
		private SmtpAddress externalId;

		// Token: 0x04001FE4 RID: 8164
		private Offer offer;

		// Token: 0x04001FE5 RID: 8165
		private WSSecurityHeader wsSecurityHeader;

		// Token: 0x04001FE6 RID: 8166
		private SharingSecurityHeader sharingSecurityHeader;
	}
}
