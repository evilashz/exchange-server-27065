using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000245 RID: 581
	public class SimpleSmtpClient : ISimpleSmtpClient, IDisposable
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00039BA4 File Offset: 0x00037DA4
		public SimpleSmtpClient(CancellationToken cancellationToken)
		{
			this.cancellationToken = cancellationToken;
			this.lastServerResponse = string.Empty;
			this.lastServerResponseCode = SimpleSmtpClient.SmtpResponseCode.Other;
			this.sessionText = new StringBuilder();
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x00039BD0 File Offset: 0x00037DD0
		public bool IsConnected
		{
			get
			{
				return this.client != null && this.client.Connected;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00039BE7 File Offset: 0x00037DE7
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00039BEF File Offset: 0x00037DEF
		public string LastCommand { get; private set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00039BF8 File Offset: 0x00037DF8
		public string LastResponse
		{
			get
			{
				return this.lastServerResponse;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x00039C00 File Offset: 0x00037E00
		public SimpleSmtpClient.SmtpResponseCode LastResponseCode
		{
			get
			{
				return this.lastServerResponseCode;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00039C08 File Offset: 0x00037E08
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x00039C10 File Offset: 0x00037E10
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00039C18 File Offset: 0x00037E18
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x00039C20 File Offset: 0x00037E20
		public bool IsXSysProbeAdvertised
		{
			get
			{
				return this.isXSysProbeAdvertised;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00039C28 File Offset: 0x00037E28
		public bool IsXStartTlsAdvertised
		{
			get
			{
				return this.isStartTlsAdvertised;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00039C30 File Offset: 0x00037E30
		public string AdvertisedServerName
		{
			get
			{
				return this.advertisedServerName;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00039C38 File Offset: 0x00037E38
		public string SessionText
		{
			get
			{
				return this.sessionText.ToString();
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x00039C45 File Offset: 0x00037E45
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x00039C4D File Offset: 0x00037E4D
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
			set
			{
				this.clientCertificates = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00039C56 File Offset: 0x00037E56
		public SmtpConnectionProbeWorkDefinition.CertificateProperties RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00039C5E File Offset: 0x00037E5E
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x00039C66 File Offset: 0x00037E66
		public bool IgnoreCertificateNameMismatchPolicyError
		{
			get
			{
				return this.ignoreCertificateNameMismatchPolicyError;
			}
			set
			{
				this.ignoreCertificateNameMismatchPolicyError = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00039C6F File Offset: 0x00037E6F
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x00039C77 File Offset: 0x00037E77
		public bool IgnoreCertificateChainPolicyErrorForSelfSigned
		{
			get
			{
				return this.ignoreCertificateChainPolicyErrorForSelfSigned;
			}
			set
			{
				this.ignoreCertificateChainPolicyErrorForSelfSigned = value;
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00039C80 File Offset: 0x00037E80
		public bool Connect(string server, int port, bool disconnectIfConnected)
		{
			if (this.client != null && !disconnectIfConnected)
			{
				return false;
			}
			if (this.client != null && this.client.Connected)
			{
				this.Disconnect();
			}
			this.client = new TcpClient(server, port);
			this.stream = this.client.GetStream();
			this.server = server;
			this.port = port;
			this.receiveBufferSize = this.client.ReceiveBufferSize;
			this.GetServerResponse(true);
			this.LastCommand = "connect";
			return true;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00039D05 File Offset: 0x00037F05
		public void Disconnect()
		{
			if (this.client != null && this.stream != null)
			{
				this.Quit();
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00039D1D File Offset: 0x00037F1D
		public void Helo(string domain = null)
		{
			this.SendCommand(string.Format("HELO{0}", string.IsNullOrEmpty(domain) ? string.Empty : (" " + domain)), true, true, true);
			this.GetAdvertisedServerName();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00039D54 File Offset: 0x00037F54
		public void Ehlo(string domain = null)
		{
			this.SendCommand(string.Format("EHLO{0}", string.IsNullOrEmpty(domain) ? string.Empty : (" " + domain)), true, true, true);
			this.GetAdvertisedServerName();
			this.GetIsXSysProbeAdvertised();
			this.GetIsStartTlsAdvertised();
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00039DA0 File Offset: 0x00037FA0
		public void AuthLogin(string username, string password)
		{
			this.SendCommand("AUTH LOGIN", true, true, true);
			if (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.AuthPrompt)
			{
				this.SendCommand(this.ConvertToBase64(username), true, false, true);
			}
			if (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.AuthPrompt)
			{
				this.SendCommand(this.ConvertToBase64(password), true, false, true);
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00039DF5 File Offset: 0x00037FF5
		public void AuthPlain(string username, string password)
		{
			this.SendCommand("AUTH PLAIN " + this.ConvertToBase64(string.Format("{0}\\0{0}\\0{1}", username, password)), true, true, true);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00039E1C File Offset: 0x0003801C
		public void StartTls(bool useAnonymousTls)
		{
			if (useAnonymousTls)
			{
				this.SendCommand("X-ANONYMOUSTLS", true, true, true);
				if (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.ServiceReady)
				{
					SmtpSslStream smtpSslStream = new SmtpSslStream(this.client.GetStream(), new SimpleSmtpClient.SmtpClientDebugOutput());
					smtpSslStream.Handshake();
					this.stream = smtpSslStream;
					return;
				}
			}
			else
			{
				this.SendCommand("STARTTLS", true, true, true);
				if (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.ServiceReady)
				{
					SslStream sslStream = new SslStream(this.client.GetStream(), false, new RemoteCertificateValidationCallback(this.ValidateCertificate));
					Task task = sslStream.AuthenticateAsClientAsync(this.server, this.clientCertificates, SslProtocols.Default, false);
					task.Wait(this.cancellationToken);
					this.stream = sslStream;
				}
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00039ED4 File Offset: 0x000380D4
		public void ExchangeAuth()
		{
			SmtpSslStream smtpSslStream = this.stream as SmtpSslStream;
			if (smtpSslStream == null)
			{
				throw new AuthenticationException("X-ANONYMOUSTLS must be called before Exchange Authentication or the call to X-ANONYMOUSTLS was unsuccessful.");
			}
			using (SmtpAuth smtpAuth = new SmtpAuth(new SimpleSmtpClient.SmtpClientDebugOutput()))
			{
				string targetSPN = string.Format("smtpsvc/{0}", this.advertisedServerName);
				string str = smtpAuth.HandleOutboundAuth(null, targetSPN, smtpSslStream.CertificatePublicKey, smtpSslStream.SessionKey, true);
				string text = "X-EXPS EXCHANGEAUTH SHA256 ";
				this.sessionText.Append(text);
				this.SendCommand(text + str, true, true, false);
				if (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.AuthAccepted)
				{
					string mutualBlob = ((string)this.lastServerResponse.Split(new char[]
					{
						' '
					}).GetValue(1)).Trim();
					smtpAuth.HandleOutboundAuth(mutualBlob, targetSPN, smtpSslStream.CertificatePublicKey, smtpSslStream.SessionKey, false);
					this.sessionText.Append("235 Exchange Auth Success ");
				}
				else
				{
					this.sessionText.Append(this.lastServerResponse);
				}
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00039FE8 File Offset: 0x000381E8
		public void MailFrom(string from)
		{
			this.SendCommand("MAIL FROM: " + from, true, true, true);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00039FFE File Offset: 0x000381FE
		public void RcptTo(string to)
		{
			this.SendCommand("RCPT TO: " + to, true, true, true);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0003A014 File Offset: 0x00038214
		public void Data(string data)
		{
			this.SendCommand("DATA", true, true, true);
			this.SendCommand(string.Format("{0}\r\n.", data), true, true, true);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0003A038 File Offset: 0x00038238
		public void BDat(Stream stream, bool last)
		{
			if (stream.Length > 2147483647L)
			{
				throw new InvalidOperationException("Stream length should not be longer than max integer value");
			}
			this.SendCommand(string.Format("BDAT {0}{1}", stream.Length, last ? " LAST" : string.Empty), false, true, true);
			MemoryStream memoryStream = stream as MemoryStream;
			byte[] buffer;
			if (memoryStream == null)
			{
				buffer = new byte[stream.Length];
				Task<int> task = stream.ReadAsync(buffer, 0, (int)stream.Length, this.cancellationToken);
				task.Wait(this.cancellationToken);
			}
			else
			{
				buffer = memoryStream.GetBuffer();
			}
			this.SendCommand(buffer, (int)stream.Length, true, true);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0003A0DD File Offset: 0x000382DD
		public void Reset()
		{
			this.SendCommand("RSET", true, true, true);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0003A0ED File Offset: 0x000382ED
		public void Verify()
		{
			this.SendCommand("VRFY", true, true, true);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003A0FD File Offset: 0x000382FD
		public void NoOp()
		{
			this.SendCommand("NOOP", true, true, true);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003A10D File Offset: 0x0003830D
		public void Quit()
		{
			this.SendCommand("QUIT", false, true, true);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003A11D File Offset: 0x0003831D
		public void Send(string text)
		{
			this.SendCommand(text, true, true, true);
			if (text.StartsWith("XPROXYTO", StringComparison.InvariantCultureIgnoreCase))
			{
				this.GetIsXSysProbeAdvertised();
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003A13D File Offset: 0x0003833D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0003A14C File Offset: 0x0003834C
		private void SendCommand(string command, bool getServerResponse = true, bool recordLastCommand = true, bool recordCommandInHistory = true)
		{
			if (recordLastCommand)
			{
				this.LastCommand = command;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(command + Environment.NewLine);
			if (recordCommandInHistory)
			{
				this.sessionText.AppendLine(command);
			}
			this.SendCommand(bytes, getServerResponse, recordCommandInHistory);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0003A194 File Offset: 0x00038394
		private void SendCommand(byte[] buffer, bool getServerResponse = true, bool recordCommandInHistory = true)
		{
			this.SendCommand(buffer, buffer.Length, getServerResponse, recordCommandInHistory);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0003A1A4 File Offset: 0x000383A4
		private void SendCommand(byte[] buffer, int length, bool getServerResponse = true, bool recordCommandInHistory = true)
		{
			Task task = this.stream.WriteAsync(buffer, 0, length, this.cancellationToken);
			task.Wait(this.cancellationToken);
			if (getServerResponse)
			{
				this.GetServerResponse(recordCommandInHistory);
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003A1E0 File Offset: 0x000383E0
		private void GetServerResponse(bool recordResponseInHistory = true)
		{
			byte[] array = new byte[this.receiveBufferSize];
			Task<int> task = this.stream.ReadAsync(array, 0, array.Length, this.cancellationToken);
			task.Wait(this.cancellationToken);
			int result = task.Result;
			this.lastServerResponse = Encoding.UTF8.GetString(array, 0, result);
			this.lastServerResponseCode = this.GetServerResponseCode(this.lastServerResponse);
			if (recordResponseInHistory)
			{
				if (SimpleSmtpClient.responseCodeToTruncate.Contains(this.lastServerResponseCode) && !string.IsNullOrWhiteSpace(this.lastServerResponse))
				{
					string text = this.lastServerResponse;
					int num = text.IndexOf(Environment.NewLine);
					if (num >= 0)
					{
						text = text.Substring(0, num);
					}
					this.sessionText.Append(text);
					return;
				}
				this.sessionText.Append(this.lastServerResponse);
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0003A2B0 File Offset: 0x000384B0
		private SimpleSmtpClient.SmtpResponseCode GetServerResponseCode(string responseText)
		{
			SimpleSmtpClient.SmtpResponseCode result = SimpleSmtpClient.SmtpResponseCode.Other;
			if (responseText.Length > 2)
			{
				int num = 0;
				if (int.TryParse(responseText.Substring(0, 3), out num) && Enum.IsDefined(typeof(SimpleSmtpClient.SmtpResponseCode), num))
				{
					result = (SimpleSmtpClient.SmtpResponseCode)num;
				}
			}
			return result;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0003A2F5 File Offset: 0x000384F5
		private string ConvertToBase64(string text)
		{
			return Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0003A308 File Offset: 0x00038508
		private bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			string text = "N/A";
			bool flag = false;
			if (policyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNotAvailable))
			{
				throw new AuthenticationException("Remote certificate not found.");
			}
			if (certificate != null)
			{
				text = string.Format("Subject: {0}, Issuer: {1}, Effective: {2} to {3}.", new object[]
				{
					certificate.Subject,
					certificate.Issuer,
					certificate.GetEffectiveDateString(),
					certificate.GetExpirationDateString()
				});
				flag = certificate.Subject.Equals(certificate.Issuer, StringComparison.InvariantCultureIgnoreCase);
			}
			if (!this.ignoreCertificateNameMismatchPolicyError && policyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch))
			{
				throw new AuthenticationException("Remote certificate name mismatch. Certificate information - " + text);
			}
			if ((!this.IgnoreCertificateChainPolicyErrorForSelfSigned || !flag) && policyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					stringBuilder.AppendLine(x509ChainStatus.StatusInformation);
				}
				text = text + " Chain status information: " + stringBuilder.ToString();
				throw new AuthenticationException("Remote certificate chain errors. Certificate information - " + text);
			}
			this.remoteCertificate = new SmtpConnectionProbeWorkDefinition.CertificateProperties();
			this.remoteCertificate.Subject = certificate.Subject;
			this.remoteCertificate.Issuer = certificate.Issuer;
			DateTime value;
			if (!DateTime.TryParse(certificate.GetEffectiveDateString(), null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal, out value))
			{
				throw new FormatException("Unable to recognize the effective date format of the server certificate.");
			}
			DateTime value2;
			if (!DateTime.TryParse(certificate.GetExpirationDateString(), null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal, out value2))
			{
				throw new FormatException("Unable to recognize the expiration date format of the server certificate.");
			}
			this.remoteCertificate.ValidFrom = new DateTime?(value);
			this.remoteCertificate.ValidTo = new DateTime?(value2);
			if (this.ignoreCertificateNameMismatchPolicyError)
			{
				policyErrors &= ~SslPolicyErrors.RemoteCertificateNameMismatch;
			}
			if (this.ignoreCertificateChainPolicyErrorForSelfSigned && flag)
			{
				policyErrors &= ~SslPolicyErrors.RemoteCertificateChainErrors;
			}
			return policyErrors == SslPolicyErrors.None;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0003A4F0 File Offset: 0x000386F0
		private void GetAdvertisedServerName()
		{
			if (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.OK)
			{
				int num = this.lastServerResponse.IndexOf(' ', 4);
				if (num > 4)
				{
					this.advertisedServerName = this.lastServerResponse.Substring(4, num - 4);
				}
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0003A532 File Offset: 0x00038732
		private void GetIsXSysProbeAdvertised()
		{
			this.isXSysProbeAdvertised = (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.OK && this.lastServerResponse.IndexOf("XSYSPROBE", StringComparison.InvariantCultureIgnoreCase) > 0);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003A55E File Offset: 0x0003875E
		private void GetIsStartTlsAdvertised()
		{
			this.isStartTlsAdvertised = (this.lastServerResponseCode == SimpleSmtpClient.SmtpResponseCode.OK && this.lastServerResponse.IndexOf("STARTTLS", StringComparison.InvariantCultureIgnoreCase) > 0);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003A58A File Offset: 0x0003878A
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed && disposing)
			{
				if (this.IsConnected)
				{
					this.Disconnect();
				}
				if (this.stream != null)
				{
					this.stream.Dispose();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000914 RID: 2324
		private static HashSet<SimpleSmtpClient.SmtpResponseCode> responseCodeToTruncate = new HashSet<SimpleSmtpClient.SmtpResponseCode>
		{
			SimpleSmtpClient.SmtpResponseCode.ServiceReady,
			SimpleSmtpClient.SmtpResponseCode.Disconnect,
			SimpleSmtpClient.SmtpResponseCode.AuthAccepted,
			SimpleSmtpClient.SmtpResponseCode.OK
		};

		// Token: 0x04000915 RID: 2325
		private readonly CancellationToken cancellationToken;

		// Token: 0x04000916 RID: 2326
		private string server;

		// Token: 0x04000917 RID: 2327
		private int port;

		// Token: 0x04000918 RID: 2328
		private TcpClient client;

		// Token: 0x04000919 RID: 2329
		private Stream stream;

		// Token: 0x0400091A RID: 2330
		private int receiveBufferSize;

		// Token: 0x0400091B RID: 2331
		private SimpleSmtpClient.SmtpResponseCode lastServerResponseCode;

		// Token: 0x0400091C RID: 2332
		private string lastServerResponse;

		// Token: 0x0400091D RID: 2333
		private StringBuilder sessionText;

		// Token: 0x0400091E RID: 2334
		private string advertisedServerName;

		// Token: 0x0400091F RID: 2335
		private X509CertificateCollection clientCertificates;

		// Token: 0x04000920 RID: 2336
		private SmtpConnectionProbeWorkDefinition.CertificateProperties remoteCertificate;

		// Token: 0x04000921 RID: 2337
		private bool ignoreCertificateNameMismatchPolicyError;

		// Token: 0x04000922 RID: 2338
		private bool ignoreCertificateChainPolicyErrorForSelfSigned;

		// Token: 0x04000923 RID: 2339
		private bool isDisposed;

		// Token: 0x04000924 RID: 2340
		private bool isXSysProbeAdvertised;

		// Token: 0x04000925 RID: 2341
		private bool isStartTlsAdvertised;

		// Token: 0x02000246 RID: 582
		public enum SmtpResponseCode
		{
			// Token: 0x04000928 RID: 2344
			Other,
			// Token: 0x04000929 RID: 2345
			ServiceReady = 220,
			// Token: 0x0400092A RID: 2346
			Disconnect,
			// Token: 0x0400092B RID: 2347
			AuthAccepted = 235,
			// Token: 0x0400092C RID: 2348
			OK = 250,
			// Token: 0x0400092D RID: 2349
			AuthPrompt = 334,
			// Token: 0x0400092E RID: 2350
			DataAccepted = 354,
			// Token: 0x0400092F RID: 2351
			BadCommand = 500,
			// Token: 0x04000930 RID: 2352
			AuthRejected = 535,
			// Token: 0x04000931 RID: 2353
			Rejected = 550,
			// Token: 0x04000932 RID: 2354
			Failed = 554
		}

		// Token: 0x02000247 RID: 583
		internal class SmtpClientDebugOutput : ISmtpClientDebugOutput
		{
			// Token: 0x060013A6 RID: 5030 RVA: 0x0003A609 File Offset: 0x00038809
			public void Output(Trace tracer, object context, string message, params object[] args)
			{
				if (!string.IsNullOrEmpty(message))
				{
					WTFDiagnostics.TraceDebug(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), message, args, null, "Output", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SimpleSmtpClient.cs", 876);
				}
			}
		}
	}
}
