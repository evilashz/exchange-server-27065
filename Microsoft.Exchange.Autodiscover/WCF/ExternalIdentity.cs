using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000064 RID: 100
	internal class ExternalIdentity : IIdentity
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001287C File Offset: 0x00010A7C
		public string AuthenticationType
		{
			get
			{
				return "External";
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00012883 File Offset: 0x00010A83
		public bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00012886 File Offset: 0x00010A86
		public string Name
		{
			get
			{
				return this.emailAddress.ToString();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00012899 File Offset: 0x00010A99
		public SmtpAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000128A1 File Offset: 0x00010AA1
		public SmtpAddress ExternalId
		{
			get
			{
				return this.externalId;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000128A9 File Offset: 0x00010AA9
		internal ExternalIdentity(SmtpAddress emailAddress, SmtpAddress externalId)
		{
			this.emailAddress = emailAddress;
			this.externalId = externalId;
		}

		// Token: 0x040002C7 RID: 711
		private const string ExternalAuthenticationType = "External";

		// Token: 0x040002C8 RID: 712
		private SmtpAddress emailAddress;

		// Token: 0x040002C9 RID: 713
		private SmtpAddress externalId;
	}
}
