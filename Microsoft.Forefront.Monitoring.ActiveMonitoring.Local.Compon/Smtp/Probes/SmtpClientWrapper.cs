using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000249 RID: 585
	public class SmtpClientWrapper : IMinimalSmtpClient, IDisposable
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0003A672 File Offset: 0x00038872
		public SmtpClientWrapper(string host)
		{
			this.smtpClient = new SmtpClient(host);
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0003A686 File Offset: 0x00038886
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x0003A68E File Offset: 0x0003888E
		public CancellationToken CancellationToken { get; set; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0003A697 File Offset: 0x00038897
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0003A6A4 File Offset: 0x000388A4
		public ICredentialsByHost Credentials
		{
			get
			{
				return this.smtpClient.Credentials;
			}
			set
			{
				this.smtpClient.Credentials = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0003A6B2 File Offset: 0x000388B2
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0003A6BF File Offset: 0x000388BF
		public int Port
		{
			get
			{
				return this.smtpClient.Port;
			}
			set
			{
				this.smtpClient.Port = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0003A6CD File Offset: 0x000388CD
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x0003A6DA File Offset: 0x000388DA
		public bool UseDefaultCredentials
		{
			get
			{
				return this.smtpClient.UseDefaultCredentials;
			}
			set
			{
				this.smtpClient.UseDefaultCredentials = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0003A6E8 File Offset: 0x000388E8
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0003A6F5 File Offset: 0x000388F5
		public int Timeout
		{
			get
			{
				return this.smtpClient.Timeout;
			}
			set
			{
				this.smtpClient.Timeout = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0003A703 File Offset: 0x00038903
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0003A710 File Offset: 0x00038910
		public bool EnableSsl
		{
			get
			{
				return this.smtpClient.EnableSsl;
			}
			set
			{
				this.smtpClient.EnableSsl = value;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0003A71E File Offset: 0x0003891E
		public void Send(MailMessage message)
		{
			this.smtpClient.Send(message);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0003A72C File Offset: 0x0003892C
		public void Dispose()
		{
			this.smtpClient.Dispose();
		}

		// Token: 0x04000934 RID: 2356
		private SmtpClient smtpClient;
	}
}
