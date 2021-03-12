using System;
using System.Net;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000046 RID: 70
	public class MissingIdentityException : Exception
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public MissingIdentityException(Guid mailboxId, string statusDescription)
		{
			this.StatusCode = HttpStatusCode.Unauthorized;
			this.StatusDescription = statusDescription;
			this.ChallengeString = ConfigProvider.Instance.Configuration.ChallengeResponseStringWithClientProfileEnabled;
			this.DiagnosticText = mailboxId.ToString();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000DAA9 File Offset: 0x0000BCA9
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000DAB1 File Offset: 0x0000BCB1
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000DABA File Offset: 0x0000BCBA
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000DAC2 File Offset: 0x0000BCC2
		public string StatusDescription { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000DACB File Offset: 0x0000BCCB
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000DAD3 File Offset: 0x0000BCD3
		public string ChallengeString { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000DADC File Offset: 0x0000BCDC
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public string DiagnosticText { get; private set; }
	}
}
