using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000055 RID: 85
	internal sealed class WebServiceUserInformation
	{
		// Token: 0x0600030A RID: 778 RVA: 0x000102A0 File Offset: 0x0000E4A0
		internal WebServiceUserInformation(SmtpAddress userSmtpAddress, string organization)
		{
			this.userSmtpAddress = userSmtpAddress;
			this.organization = organization;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public string EmailAddress
		{
			get
			{
				return this.userSmtpAddress.ToString();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000102DC File Offset: 0x0000E4DC
		public string Domain
		{
			get
			{
				return this.userSmtpAddress.Domain;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000102F7 File Offset: 0x0000E4F7
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x04000187 RID: 391
		private readonly SmtpAddress userSmtpAddress;

		// Token: 0x04000188 RID: 392
		private readonly string organization;
	}
}
