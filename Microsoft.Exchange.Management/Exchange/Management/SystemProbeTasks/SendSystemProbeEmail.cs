using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DA7 RID: 3495
	[Cmdlet("Send", "SystemProbeEmail")]
	public sealed class SendSystemProbeEmail : Task
	{
		// Token: 0x170029A9 RID: 10665
		// (get) Token: 0x060085E6 RID: 34278 RVA: 0x00223DAD File Offset: 0x00221FAD
		// (set) Token: 0x060085E7 RID: 34279 RVA: 0x00223DB5 File Offset: 0x00221FB5
		[Parameter(Mandatory = false)]
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170029AA RID: 10666
		// (get) Token: 0x060085E8 RID: 34280 RVA: 0x00223DBE File Offset: 0x00221FBE
		// (set) Token: 0x060085E9 RID: 34281 RVA: 0x00223DC6 File Offset: 0x00221FC6
		[Parameter(Mandatory = false)]
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x170029AB RID: 10667
		// (get) Token: 0x060085EA RID: 34282 RVA: 0x00223DCF File Offset: 0x00221FCF
		// (set) Token: 0x060085EB RID: 34283 RVA: 0x00223DDC File Offset: 0x00221FDC
		[Parameter(Mandatory = false)]
		public string[] Attachments
		{
			get
			{
				return this.attachments.ToArray();
			}
			set
			{
				this.attachments = new List<string>(value);
			}
		}

		// Token: 0x170029AC RID: 10668
		// (get) Token: 0x060085EC RID: 34284 RVA: 0x00223DEA File Offset: 0x00221FEA
		// (set) Token: 0x060085ED RID: 34285 RVA: 0x00223DF2 File Offset: 0x00221FF2
		[Parameter(Mandatory = true)]
		public string SmtpServer
		{
			get
			{
				return this.smtpServer;
			}
			set
			{
				this.smtpServer = value;
			}
		}

		// Token: 0x170029AD RID: 10669
		// (get) Token: 0x060085EE RID: 34286 RVA: 0x00223DFB File Offset: 0x00221FFB
		// (set) Token: 0x060085EF RID: 34287 RVA: 0x00223E03 File Offset: 0x00222003
		[Parameter(Mandatory = false)]
		public string SmtpUser
		{
			get
			{
				return this.smtpUser;
			}
			set
			{
				this.smtpUser = value;
			}
		}

		// Token: 0x170029AE RID: 10670
		// (get) Token: 0x060085F0 RID: 34288 RVA: 0x00223E0C File Offset: 0x0022200C
		// (set) Token: 0x060085F1 RID: 34289 RVA: 0x00223E14 File Offset: 0x00222014
		[Parameter(Mandatory = false)]
		public string SmtpPassword
		{
			get
			{
				return this.smtpPassword;
			}
			set
			{
				this.smtpPassword = value;
			}
		}

		// Token: 0x170029AF RID: 10671
		// (get) Token: 0x060085F2 RID: 34290 RVA: 0x00223E1D File Offset: 0x0022201D
		// (set) Token: 0x060085F3 RID: 34291 RVA: 0x00223E25 File Offset: 0x00222025
		[Parameter(Mandatory = true)]
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x170029B0 RID: 10672
		// (get) Token: 0x060085F4 RID: 34292 RVA: 0x00223E2E File Offset: 0x0022202E
		// (set) Token: 0x060085F5 RID: 34293 RVA: 0x00223E3B File Offset: 0x0022203B
		[Parameter(Mandatory = true)]
		public string[] To
		{
			get
			{
				return this.to.ToArray();
			}
			set
			{
				this.to = new List<string>(value);
			}
		}

		// Token: 0x170029B1 RID: 10673
		// (get) Token: 0x060085F6 RID: 34294 RVA: 0x00223E49 File Offset: 0x00222049
		// (set) Token: 0x060085F7 RID: 34295 RVA: 0x00223E56 File Offset: 0x00222056
		[Parameter(Mandatory = false)]
		public string[] CC
		{
			get
			{
				return this.cc.ToArray();
			}
			set
			{
				this.cc = new List<string>(value);
			}
		}

		// Token: 0x170029B2 RID: 10674
		// (get) Token: 0x060085F8 RID: 34296 RVA: 0x00223E64 File Offset: 0x00222064
		// (set) Token: 0x060085F9 RID: 34297 RVA: 0x00223E6C File Offset: 0x0022206C
		[Parameter(Mandatory = false)]
		public bool Html
		{
			get
			{
				return this.html;
			}
			set
			{
				this.html = value;
			}
		}

		// Token: 0x170029B3 RID: 10675
		// (get) Token: 0x060085FA RID: 34298 RVA: 0x00223E75 File Offset: 0x00222075
		// (set) Token: 0x060085FB RID: 34299 RVA: 0x00223E7D File Offset: 0x0022207D
		[Parameter(Mandatory = false)]
		public Guid ProbeGuid
		{
			get
			{
				return this.probeGuid;
			}
			set
			{
				this.probeGuid = value;
			}
		}

		// Token: 0x170029B4 RID: 10676
		// (get) Token: 0x060085FC RID: 34300 RVA: 0x00223E86 File Offset: 0x00222086
		// (set) Token: 0x060085FD RID: 34301 RVA: 0x00223E8E File Offset: 0x0022208E
		[Parameter(Mandatory = false)]
		public bool UseSsl
		{
			get
			{
				return this.useSsl;
			}
			set
			{
				this.useSsl = value;
			}
		}

		// Token: 0x170029B5 RID: 10677
		// (get) Token: 0x060085FE RID: 34302 RVA: 0x00223E98 File Offset: 0x00222098
		// (set) Token: 0x060085FF RID: 34303 RVA: 0x00223EBF File Offset: 0x002220BF
		[Parameter(Mandatory = false)]
		public int Port
		{
			get
			{
				int? num = this.port;
				if (num == null)
				{
					return 25;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.port = new int?(value);
			}
		}

		// Token: 0x170029B6 RID: 10678
		// (get) Token: 0x06008600 RID: 34304 RVA: 0x00223ECD File Offset: 0x002220CD
		// (set) Token: 0x06008601 RID: 34305 RVA: 0x00223EDA File Offset: 0x002220DA
		[Parameter(Mandatory = false)]
		public SwitchParameter UseXheader
		{
			get
			{
				return this.useXheader;
			}
			set
			{
				this.useXheader = value;
			}
		}

		// Token: 0x170029B7 RID: 10679
		// (get) Token: 0x06008602 RID: 34306 RVA: 0x00223EE8 File Offset: 0x002220E8
		// (set) Token: 0x06008603 RID: 34307 RVA: 0x00223EF0 File Offset: 0x002220F0
		[Parameter(Mandatory = false)]
		public bool TestContext
		{
			get
			{
				return this.testContext;
			}
			set
			{
				this.testContext = value;
			}
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x00223F20 File Offset: 0x00222120
		protected override void InternalProcessRecord()
		{
			if (this.probeGuid == Guid.Empty)
			{
				this.probeGuid = Guid.NewGuid();
			}
			SystemProbeMailProperties systemProbeMailProperties = new SystemProbeMailProperties();
			systemProbeMailProperties.Guid = this.ProbeGuid;
			try
			{
				SmtpTalk smtpTalk = new SmtpTalk(new SendSystemProbeEmail.SmtpClientDebugOutput(this));
				smtpTalk.Connect(this.smtpServer, this.port ?? 25);
				smtpTalk.Ehlo();
				SmtpResponse ehloResponse;
				if (SmtpResponse.TryParse(smtpTalk.EhloResponseText, out ehloResponse))
				{
					this.ehloOptions.ParseResponse(ehloResponse, new IPAddress(0L));
				}
				if (this.UseSsl)
				{
					smtpTalk.StartTls(true);
					smtpTalk.Ehlo();
				}
				if (!string.IsNullOrEmpty(this.smtpUser))
				{
					smtpTalk.Authenticate(new NetworkCredential(this.smtpUser, this.smtpPassword), SmtpSspiMechanism.Kerberos);
				}
				else if (this.UseSsl)
				{
					smtpTalk.Authenticate(CredentialCache.DefaultNetworkCredentials, SmtpSspiMechanism.Kerberos);
				}
				if (this.ehloOptions.XSysProbe)
				{
					base.WriteVerbose(Strings.SendingGuidInMailFrom);
					smtpTalk.MailFrom(string.Format(CultureInfo.InvariantCulture, "{0} xsysprobeid={1}", new object[]
					{
						this.from,
						this.probeGuid.ToString()
					}), null);
				}
				else
				{
					smtpTalk.MailFrom(this.from, null);
				}
				this.to.ForEach(delegate(string to)
				{
					smtpTalk.RcptTo(to, new bool?(false));
				});
				string text = Path.Combine(Path.GetTempPath(), this.probeGuid.ToString());
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				List<string> list = new List<string>(Directory.GetFiles(text, "*.eml"));
				list.ForEach(delegate(string f)
				{
					File.Delete(f);
				});
				MailMessage message = this.CreateMailMessage();
				using (SmtpClient smtpClient = new SmtpClient(this.smtpServer))
				{
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					smtpClient.PickupDirectoryLocation = text;
					smtpClient.Send(message);
				}
				list = new List<string>(Directory.GetFiles(text, "*.eml"));
				if (list.Count != 1)
				{
					throw new Exception("Unexpected number of files in private pickup folder");
				}
				using (FileStream fileStream = File.OpenRead(list[0]))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						fileStream.CopyTo(memoryStream);
						memoryStream.Position = 0L;
						smtpTalk.Chunking(memoryStream);
					}
				}
				if (this.testContext)
				{
					systemProbeMailProperties.MailMessage = list[0];
				}
				else
				{
					try
					{
						Directory.Delete(text, true);
					}
					catch (IOException ex)
					{
						base.WriteWarning(ex.Message);
					}
				}
				try
				{
					smtpTalk.Quit();
				}
				catch (SocketException)
				{
				}
				catch (IOException)
				{
				}
			}
			catch (MustBeTlsForAuthException ex2)
			{
				base.WriteWarning(ex2.Message);
			}
			catch (UnexpectedSmtpServerResponseException ex3)
			{
				base.WriteWarning(ex3.Message);
			}
			base.WriteObject(systemProbeMailProperties);
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x0022435C File Offset: 0x0022255C
		private MailMessage CreateMailMessage()
		{
			MailMessage message = new MailMessage();
			message.From = new MailAddress(this.from);
			message.Subject = this.subject;
			message.Body = this.body;
			message.IsBodyHtml = this.html;
			if (!this.ehloOptions.XSysProbe || this.useXheader)
			{
				base.WriteVerbose(new LocalizedString(string.Format(CultureInfo.CurrentCulture, "Sending system probe guid in {0} header", new object[]
				{
					"X-FFOSystemProbe"
				})));
				string value = SystemProbeId.EncryptProbeGuid(this.probeGuid, DateTime.UtcNow);
				message.Headers.Add("X-FFOSystemProbe", value);
			}
			this.to.ForEach(delegate(string s)
			{
				message.To.Add(s);
			});
			this.cc.ForEach(delegate(string s)
			{
				message.CC.Add(s);
			});
			this.attachments.ForEach(delegate(string s)
			{
				message.Attachments.Add(this.CreateAttachment(s));
			});
			return message;
		}

		// Token: 0x06008606 RID: 34310 RVA: 0x0022447C File Offset: 0x0022267C
		private Attachment CreateAttachment(string file)
		{
			Attachment attachment = new Attachment(file, "application/octet-stream");
			ContentDisposition contentDisposition = attachment.ContentDisposition;
			contentDisposition.CreationDate = File.GetCreationTime(file);
			contentDisposition.ModificationDate = File.GetLastWriteTime(file);
			contentDisposition.ReadDate = File.GetLastAccessTime(file);
			return attachment;
		}

		// Token: 0x040040ED RID: 16621
		private string subject;

		// Token: 0x040040EE RID: 16622
		private string body;

		// Token: 0x040040EF RID: 16623
		private List<string> attachments = new List<string>();

		// Token: 0x040040F0 RID: 16624
		private string smtpServer;

		// Token: 0x040040F1 RID: 16625
		private string smtpUser;

		// Token: 0x040040F2 RID: 16626
		private string smtpPassword;

		// Token: 0x040040F3 RID: 16627
		private string from;

		// Token: 0x040040F4 RID: 16628
		private List<string> to = new List<string>();

		// Token: 0x040040F5 RID: 16629
		private List<string> cc = new List<string>();

		// Token: 0x040040F6 RID: 16630
		private bool html;

		// Token: 0x040040F7 RID: 16631
		private Guid probeGuid;

		// Token: 0x040040F8 RID: 16632
		private bool useSsl;

		// Token: 0x040040F9 RID: 16633
		private int? port;

		// Token: 0x040040FA RID: 16634
		private EhloOptions ehloOptions = new EhloOptions();

		// Token: 0x040040FB RID: 16635
		private bool useXheader;

		// Token: 0x040040FC RID: 16636
		private bool testContext;

		// Token: 0x02000DA8 RID: 3496
		private class SmtpClientDebugOutput : ISmtpClientDebugOutput
		{
			// Token: 0x06008609 RID: 34313 RVA: 0x002244F5 File Offset: 0x002226F5
			public SmtpClientDebugOutput(PSCmdlet host)
			{
				this.host = host;
			}

			// Token: 0x0600860A RID: 34314 RVA: 0x00224504 File Offset: 0x00222704
			public void Output(Trace tracer, object context, string message, params object[] args)
			{
				tracer.TraceInformation(0, 0L, message, args);
				this.host.WriteVerbose(string.Format(CultureInfo.CurrentCulture, message, args));
			}

			// Token: 0x040040FE RID: 16638
			private PSCmdlet host;
		}
	}
}
