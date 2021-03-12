using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000220 RID: 544
	public interface IMinimalSmtpClient : IDisposable
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060011A4 RID: 4516
		// (set) Token: 0x060011A5 RID: 4517
		CancellationToken CancellationToken { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060011A6 RID: 4518
		// (set) Token: 0x060011A7 RID: 4519
		ICredentialsByHost Credentials { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060011A8 RID: 4520
		// (set) Token: 0x060011A9 RID: 4521
		int Port { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060011AA RID: 4522
		// (set) Token: 0x060011AB RID: 4523
		bool UseDefaultCredentials { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060011AC RID: 4524
		// (set) Token: 0x060011AD RID: 4525
		int Timeout { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060011AE RID: 4526
		// (set) Token: 0x060011AF RID: 4527
		bool EnableSsl { get; set; }

		// Token: 0x060011B0 RID: 4528
		void Send(MailMessage message);
	}
}
