using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200023C RID: 572
	public class RawSmtpClientWrapper : IMinimalSmtpClient, IDisposable
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x00038734 File Offset: 0x00036934
		public RawSmtpClientWrapper(string host, SmtpProbeWorkDefinition workDefinition, DelTraceDebug traceDebug)
		{
			if (string.IsNullOrWhiteSpace(host))
			{
				throw new ArgumentException("host must not be null or whitespace.");
			}
			if (workDefinition == null)
			{
				throw new ArgumentException("workDefinition must not be null.");
			}
			this.host = host;
			this.workDefinition = workDefinition;
			this.traceDebug = traceDebug;
			ChainEnginePool pool = new ChainEnginePool();
			this.cache = new CertificateCache(pool);
			this.cache.Open(OpenFlags.ReadOnly);
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0003879B File Offset: 0x0003699B
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000387A3 File Offset: 0x000369A3
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x000387AB File Offset: 0x000369AB
		public CancellationToken CancellationToken { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000387B4 File Offset: 0x000369B4
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x000387BC File Offset: 0x000369BC
		public ICredentialsByHost Credentials { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x000387C5 File Offset: 0x000369C5
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x000387CD File Offset: 0x000369CD
		public int Port { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000387D6 File Offset: 0x000369D6
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x000387DE File Offset: 0x000369DE
		public bool UseDefaultCredentials { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x000387E7 File Offset: 0x000369E7
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x000387EF File Offset: 0x000369EF
		public int Timeout { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000387F8 File Offset: 0x000369F8
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x00038800 File Offset: 0x00036A00
		public bool EnableSsl { get; set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00038809 File Offset: 0x00036A09
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x00038811 File Offset: 0x00036A11
		public string LastResponse { get; private set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0003881A File Offset: 0x00036A1A
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x00038822 File Offset: 0x00036A22
		public string EhloSent { get; private set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0003882B File Offset: 0x00036A2B
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x00038833 File Offset: 0x00036A33
		public string FDContacted { get; private set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0003883C File Offset: 0x00036A3C
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x00038844 File Offset: 0x00036A44
		public string ExchangeMessageId { get; private set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0003884D File Offset: 0x00036A4D
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x00038855 File Offset: 0x00036A55
		public string HubServer { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0003885E File Offset: 0x00036A5E
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x00038866 File Offset: 0x00036A66
		public SimpleSmtpClient.SmtpResponseCode LastResponseCode { get; private set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0003886F File Offset: 0x00036A6F
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x00038877 File Offset: 0x00036A77
		public Exception LastEncounteredException { get; private set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00038880 File Offset: 0x00036A80
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x00038888 File Offset: 0x00036A88
		public string LastCommand { get; private set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00038891 File Offset: 0x00036A91
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x00038899 File Offset: 0x00036A99
		public bool SuccessfullySentLastMail { get; private set; }

		// Token: 0x0600134B RID: 4939 RVA: 0x000388AC File Offset: 0x00036AAC
		public void Send(MailMessage message)
		{
			this.SuccessfullySentLastMail = false;
			Guid guid = CombGuidGenerator.NewGuid();
			bool flag = false;
			using (SimpleSmtpClient simpleSmtpClient = new SimpleSmtpClient(this.CancellationToken))
			{
				try
				{
					this.ConnectWithRetry(simpleSmtpClient, this.workDefinition.SendMail.ConnectRetryCount);
					flag = true;
					this.CheckCancellation();
					this.CheckLastResponse(simpleSmtpClient);
					this.ExctractFDServer(simpleSmtpClient);
					this.EhloSent = guid.ToString() + "." + ComputerInformation.DnsPhysicalFullyQualifiedDomainName + ".OutsideInProbe";
					simpleSmtpClient.Ehlo(this.EhloSent);
					this.CheckCancellation();
					this.CheckLastResponse(simpleSmtpClient);
					this.ExtractClientIp(simpleSmtpClient);
					if (this.EnableSsl)
					{
						if (!simpleSmtpClient.IsXStartTlsAdvertised && this.workDefinition.SendMail.RequireTLS)
						{
							throw new RawSmtpClientWrapper.StartTlsNotAdvertisedException("STARTTLS was not advertised");
						}
						if (!simpleSmtpClient.IsXStartTlsAdvertised)
						{
							this.traceDebug("Starttls not advertised", new object[0]);
						}
						else
						{
							simpleSmtpClient.IgnoreCertificateNameMismatchPolicyError = this.workDefinition.SendMail.IgnoreCertificateNameMismatchPolicyError;
							if (this.workDefinition.ClientCertificate != null)
							{
								this.AddClientCertificatesToSmtp(simpleSmtpClient);
							}
							this.TraceDebug("STARTTLS", new object[0]);
							simpleSmtpClient.StartTls(false);
							this.CheckCancellation();
							this.CheckLastResponse(simpleSmtpClient);
							simpleSmtpClient.Ehlo(this.EhloSent);
							this.CheckCancellation();
							this.CheckLastResponse(simpleSmtpClient);
						}
					}
					if (!this.workDefinition.SendMail.Anonymous)
					{
						this.TraceDebug("AUTH", new object[0]);
						simpleSmtpClient.AuthLogin(this.workDefinition.SendMail.SenderUsername, this.workDefinition.SendMail.SenderPassword);
						this.CheckCancellation();
						this.CheckLastResponse(simpleSmtpClient);
					}
					if (this.workDefinition.SendMail.AuthOnly)
					{
						this.TraceDebug("Quit After Auth", new object[0]);
					}
					else
					{
						string text = simpleSmtpClient.IsXSysProbeAdvertised ? string.Format("{0} XSYSPROBEID={1}", this.workDefinition.SendMail.Message.Mail.From.ToString(), guid) : this.workDefinition.SendMail.Message.Mail.From.ToString();
						this.TraceDebug("MAILFROM:{0}", new object[]
						{
							text
						});
						simpleSmtpClient.MailFrom(text);
						this.CheckCancellation();
						this.CheckLastResponse(simpleSmtpClient);
						foreach (MailAddress mailAddress in this.workDefinition.SendMail.Message.Mail.To)
						{
							this.TraceDebug("RCPTTO:{0}", new object[]
							{
								mailAddress.ToString()
							});
							simpleSmtpClient.RcptTo(mailAddress.ToString());
							this.CheckCancellation();
							this.CheckLastResponse(simpleSmtpClient);
						}
						if (this.workDefinition.SendMail.RcptOnly)
						{
							this.TraceDebug("Quit After RCPT", new object[0]);
						}
						else
						{
							string text2 = Path.Combine(Path.GetTempPath(), guid.ToString());
							try
							{
								Directory.CreateDirectory(text2);
								Directory.GetFiles(text2, "*.eml").ToList<string>().ForEach(delegate(string f)
								{
									File.Delete(f);
								});
								using (SmtpClient smtpClient = new SmtpClient(this.host))
								{
									smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
									smtpClient.PickupDirectoryLocation = text2;
									smtpClient.Send(message);
								}
								List<string> list = Directory.GetFiles(text2, "*.eml").ToList<string>();
								using (FileStream fileStream = File.OpenRead(list[0]))
								{
									this.TraceDebug("BDAT", new object[0]);
									simpleSmtpClient.BDat(fileStream, true);
									this.CheckCancellation();
									this.CheckLastResponse(simpleSmtpClient);
									this.SuccessfullySentLastMail = true;
								}
								this.TraceDebug("Response to BDAT: {0}", new object[]
								{
									simpleSmtpClient.LastResponse.Trim()
								});
								this.ExtractMessageId(simpleSmtpClient);
								this.ExtractHubServer(simpleSmtpClient);
							}
							finally
							{
								try
								{
									Directory.Delete(text2, true);
								}
								catch (IOException)
								{
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.LastEncounteredException = ex;
					if (!flag && !(ex is OperationCanceledException))
					{
						this.LastEncounteredException = new RawSmtpClientWrapper.MiscellaneousConnectionException(string.Format("Connection failure while trying to connect to {0} on port {1}{2}", this.host, this.Port, Environment.NewLine), ex);
					}
					this.LastCommand = ((simpleSmtpClient.LastCommand == null) ? null : simpleSmtpClient.LastCommand.Trim());
					this.LastResponse = ((simpleSmtpClient.LastResponse == null) ? null : simpleSmtpClient.LastResponse.Trim());
					this.LastResponseCode = simpleSmtpClient.LastResponseCode;
					throw;
				}
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00038E18 File Offset: 0x00037018
		public void Dispose()
		{
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00038E1C File Offset: 0x0003701C
		private static void ValidateCertificateExpiration(X509Certificate2 tlsCertificate)
		{
			string expirationDateString = tlsCertificate.GetExpirationDateString();
			DateTime t;
			if (!DateTime.TryParse(expirationDateString, out t))
			{
				throw new RawSmtpClientWrapper.InvalidCertificateException(string.Format("Unable to parse expiration time: {0}", expirationDateString));
			}
			if (t < DateTime.UtcNow)
			{
				throw new RawSmtpClientWrapper.InvalidCertificateException(string.Format("The certificate expired on  {0}{1} The Current Time is: {2}", expirationDateString, Environment.NewLine, DateTime.UtcNow));
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00038E78 File Offset: 0x00037078
		private static bool TryFindIp(string input, out string output)
		{
			Regex regex = new Regex("\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}");
			MatchCollection matchCollection = regex.Matches(input);
			if (matchCollection != null && matchCollection.Count > 0)
			{
				output = matchCollection[0].Value;
				return true;
			}
			regex = new Regex("([0-9a-fA-F]{1,4}:+){1,7}[0-9a-fA-F]{1,4}");
			matchCollection = regex.Matches(input);
			if (matchCollection != null && matchCollection.Count > 0)
			{
				output = matchCollection[0].Value;
				return true;
			}
			regex = new Regex("\\[.*\\]");
			matchCollection = regex.Matches(input);
			if (matchCollection == null || matchCollection.Count == 0)
			{
				output = null;
				return false;
			}
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string ipString = match.Value.Trim(new char[]
				{
					'[',
					']'
				});
				IPAddress ipaddress;
				if (IPAddress.TryParse(ipString, out ipaddress))
				{
					output = ipaddress.ToString();
					return true;
				}
			}
			output = null;
			return false;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00038F90 File Offset: 0x00037190
		private void ConnectWithRetry(SimpleSmtpClient client, int retryCount)
		{
			int num = 0;
			this.TraceDebug("Connecting to {0} on {1}", new object[]
			{
				this.host,
				this.Port
			});
			while (!client.IsConnected && num <= retryCount)
			{
				num++;
				this.CheckCancellation();
				try
				{
					client.Connect(this.host, this.Port, false);
				}
				catch
				{
					if (num > retryCount)
					{
						throw;
					}
				}
				if (!client.IsConnected && num <= retryCount)
				{
					this.TraceDebug("Failed to connect, retrying", new object[0]);
				}
			}
			if (!client.IsConnected)
			{
				throw new Exception(string.Format("[Connection failure while trying to connect to {0} on port {1}]", this.host, this.Port));
			}
			this.TraceDebug("connected", new object[0]);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00039068 File Offset: 0x00037268
		private void ExctractFDServer(SimpleSmtpClient client)
		{
			this.TraceDebug("Found Banner: {0}", new object[]
			{
				client.LastResponse.Trim()
			});
			string[] array = client.LastResponse.Split(new char[]
			{
				' '
			});
			if (array.Count<string>() < 2)
			{
				this.FDContacted = "Unparsable Banner. See Execution Context";
				return;
			}
			this.FDContacted = array[1];
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000390D0 File Offset: 0x000372D0
		private void ExtractClientIp(SimpleSmtpClient client)
		{
			string text = null;
			if (RawSmtpClientWrapper.TryFindIp(client.LastResponse, out text))
			{
				this.traceDebug("Client IP: {0}", new object[]
				{
					text
				});
				return;
			}
			this.traceDebug("Couldn't Find IP.  Last Response: {0}", new object[]
			{
				client.LastResponse
			});
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003912C File Offset: 0x0003732C
		private void ExtractMessageId(SimpleSmtpClient client)
		{
			string[] array = client.LastResponse.Split(new char[]
			{
				' '
			});
			if (array.Count<string>() < 3)
			{
				this.ExchangeMessageId = "Unparsable MessageId. See Execution Context";
				return;
			}
			this.ExchangeMessageId = array[2];
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00039170 File Offset: 0x00037370
		private void ExtractHubServer(SimpleSmtpClient client)
		{
			Regex regex = new Regex("Hostname=(.*)] Queued mail for delivery");
			Match match = regex.Match(client.LastResponse);
			if (match != null && match.Success && match.Groups.Count > 0)
			{
				this.HubServer = match.Groups[1].Value;
				return;
			}
			this.HubServer = "Unparsable Hub Server Name.  See Execution Context";
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000391D4 File Offset: 0x000373D4
		private void CheckCancellation()
		{
			if (this.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000391F8 File Offset: 0x000373F8
		private void CheckLastResponse(SimpleSmtpClient client)
		{
			this.LastCommand = client.LastCommand.Trim();
			this.LastResponse = client.LastResponse.Trim();
			this.LastResponseCode = client.LastResponseCode;
			if (this.LastResponseCode == SimpleSmtpClient.SmtpResponseCode.OK || this.LastResponseCode == SimpleSmtpClient.SmtpResponseCode.AuthAccepted || this.LastResponseCode == SimpleSmtpClient.SmtpResponseCode.AuthPrompt || this.LastResponseCode == SimpleSmtpClient.SmtpResponseCode.ServiceReady || this.LastResponseCode == SimpleSmtpClient.SmtpResponseCode.DataAccepted)
			{
				return;
			}
			throw new Exception(string.Format("Unexpected SMTP Response: {0}", this.LastResponse));
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0003928C File Offset: 0x0003748C
		private void AddClientCertificatesToSmtp(SimpleSmtpClient client)
		{
			client.ClientCertificates = new X509CertificateCollection();
			if (!string.IsNullOrWhiteSpace(this.workDefinition.ClientCertificate.FindValue))
			{
				X509Store x509Store = new X509Store(this.workDefinition.ClientCertificate.StoreName, this.workDefinition.ClientCertificate.StoreLocation);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				try
				{
					X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(this.workDefinition.ClientCertificate.FindType, this.workDefinition.ClientCertificate.FindValue, true);
					if (x509Certificate2Collection != null && x509Certificate2Collection.Count != 0)
					{
						this.TraceDebug("Certs Loaded", new object[0]);
						client.ClientCertificates.AddRange(x509Certificate2Collection);
					}
					else
					{
						this.TraceDebug("No Certs Found", new object[0]);
					}
					return;
				}
				finally
				{
					x509Store.Close();
				}
			}
			if (!string.IsNullOrWhiteSpace(this.workDefinition.ClientCertificate.TransportCertificateName))
			{
				X509Certificate2 x509Certificate = this.cache.Find(this.workDefinition.ClientCertificate.TransportCertificateName);
				if (x509Certificate != null)
				{
					RawSmtpClientWrapper.ValidateCertificateExpiration(x509Certificate);
					this.TraceDebug("Cert Loaded", new object[0]);
					client.ClientCertificates.Add(x509Certificate);
					return;
				}
				this.TraceDebug("No Certs Found", new object[0]);
				return;
			}
			else if (!string.IsNullOrWhiteSpace(this.workDefinition.ClientCertificate.TransportCertificateFqdn))
			{
				string text = string.IsNullOrEmpty(this.workDefinition.ClientCertificate.TransportCertificateFqdn) ? ComputerInformation.DnsPhysicalFullyQualifiedDomainName : this.workDefinition.ClientCertificate.TransportCertificateFqdn;
				X509Certificate2 x509Certificate2 = this.cache.Find(new string[]
				{
					text
				}, true, this.workDefinition.ClientCertificate.TransportWildcardMatchType);
				if (x509Certificate2 != null)
				{
					RawSmtpClientWrapper.ValidateCertificateExpiration(x509Certificate2);
					this.TraceDebug("Cert Loaded", new object[0]);
					client.ClientCertificates.Add(x509Certificate2);
					return;
				}
				this.TraceDebug("No Cert Found", new object[0]);
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00039490 File Offset: 0x00037690
		private void TraceDebug(string format, params object[] args)
		{
			if (this.traceDebug != null)
			{
				this.traceDebug(format, args);
			}
		}

		// Token: 0x040008F7 RID: 2295
		private const string IpV4AddressRegEx = "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}";

		// Token: 0x040008F8 RID: 2296
		private const string IpV6AddressRegEx = "([0-9a-fA-F]{1,4}:+){1,7}[0-9a-fA-F]{1,4}";

		// Token: 0x040008F9 RID: 2297
		private const string IpFallbackRegEx = "\\[.*\\]";

		// Token: 0x040008FA RID: 2298
		private const string HostNameRegEx = "Hostname=(.*)] Queued mail for delivery";

		// Token: 0x040008FB RID: 2299
		private readonly string host;

		// Token: 0x040008FC RID: 2300
		private readonly CertificateCache cache;

		// Token: 0x040008FD RID: 2301
		private readonly SmtpProbeWorkDefinition workDefinition;

		// Token: 0x040008FE RID: 2302
		private readonly DelTraceDebug traceDebug;

		// Token: 0x0200023D RID: 573
		public class InvalidCertificateException : Exception
		{
			// Token: 0x06001359 RID: 4953 RVA: 0x000394A7 File Offset: 0x000376A7
			public InvalidCertificateException()
			{
			}

			// Token: 0x0600135A RID: 4954 RVA: 0x000394AF File Offset: 0x000376AF
			public InvalidCertificateException(string message) : base(message)
			{
			}
		}

		// Token: 0x0200023E RID: 574
		public class StartTlsNotAdvertisedException : Exception
		{
			// Token: 0x0600135B RID: 4955 RVA: 0x000394B8 File Offset: 0x000376B8
			public StartTlsNotAdvertisedException()
			{
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x000394C0 File Offset: 0x000376C0
			public StartTlsNotAdvertisedException(string message) : base(message)
			{
			}
		}

		// Token: 0x0200023F RID: 575
		public class MiscellaneousConnectionException : Exception
		{
			// Token: 0x0600135D RID: 4957 RVA: 0x000394C9 File Offset: 0x000376C9
			public MiscellaneousConnectionException()
			{
			}

			// Token: 0x0600135E RID: 4958 RVA: 0x000394D1 File Offset: 0x000376D1
			public MiscellaneousConnectionException(string message) : base(message)
			{
			}

			// Token: 0x0600135F RID: 4959 RVA: 0x000394DA File Offset: 0x000376DA
			public MiscellaneousConnectionException(string message, Exception innerException) : base(message, innerException)
			{
			}
		}
	}
}
