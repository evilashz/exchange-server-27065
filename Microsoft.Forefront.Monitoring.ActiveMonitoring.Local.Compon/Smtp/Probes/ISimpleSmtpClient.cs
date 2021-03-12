using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000222 RID: 546
	public interface ISimpleSmtpClient : IDisposable
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060011B8 RID: 4536
		string AdvertisedServerName { get; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060011B9 RID: 4537
		bool IsConnected { get; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060011BA RID: 4538
		string LastResponse { get; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060011BB RID: 4539
		SimpleSmtpClient.SmtpResponseCode LastResponseCode { get; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060011BC RID: 4540
		string Server { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060011BD RID: 4541
		int Port { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060011BE RID: 4542
		string SessionText { get; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060011BF RID: 4543
		Stream Stream { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060011C0 RID: 4544
		bool IsXSysProbeAdvertised { get; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060011C1 RID: 4545
		// (set) Token: 0x060011C2 RID: 4546
		X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060011C3 RID: 4547
		SmtpConnectionProbeWorkDefinition.CertificateProperties RemoteCertificate { get; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060011C4 RID: 4548
		// (set) Token: 0x060011C5 RID: 4549
		bool IgnoreCertificateNameMismatchPolicyError { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060011C6 RID: 4550
		// (set) Token: 0x060011C7 RID: 4551
		bool IgnoreCertificateChainPolicyErrorForSelfSigned { get; set; }

		// Token: 0x060011C8 RID: 4552
		bool Connect(string server, int port, bool disconnectIfConnected);

		// Token: 0x060011C9 RID: 4553
		void Disconnect();

		// Token: 0x060011CA RID: 4554
		void Helo(string domain = null);

		// Token: 0x060011CB RID: 4555
		void Ehlo(string domain = null);

		// Token: 0x060011CC RID: 4556
		void AuthLogin(string username, string password);

		// Token: 0x060011CD RID: 4557
		void AuthPlain(string username, string password);

		// Token: 0x060011CE RID: 4558
		void ExchangeAuth();

		// Token: 0x060011CF RID: 4559
		void StartTls(bool useAnonymousTls);

		// Token: 0x060011D0 RID: 4560
		void MailFrom(string from);

		// Token: 0x060011D1 RID: 4561
		void RcptTo(string to);

		// Token: 0x060011D2 RID: 4562
		void Data(string data);

		// Token: 0x060011D3 RID: 4563
		void BDat(Stream stream, bool last);

		// Token: 0x060011D4 RID: 4564
		void Reset();

		// Token: 0x060011D5 RID: 4565
		void Verify();

		// Token: 0x060011D6 RID: 4566
		void NoOp();

		// Token: 0x060011D7 RID: 4567
		void Send(string text);
	}
}
