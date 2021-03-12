using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MapiHttpBindingInfo
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00005896 File Offset: 0x00003A96
		public MapiHttpBindingInfo(string serverFqdn, ICredentials credentials) : this(serverFqdn, Constants.DefaultHttpsPort, true, credentials, true, null)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000058A8 File Offset: 0x00003AA8
		public MapiHttpBindingInfo(string serverFqdn, ICredentials credentials, string mailboxId) : this(serverFqdn, Constants.DefaultHttpsPort, true, credentials, true, mailboxId)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000058BC File Offset: 0x00003ABC
		public MapiHttpBindingInfo(string serverFqdn, int port, bool useSsl, ICredentials credentials, bool ignoreCertificateErrors, string mailboxId)
		{
			this.ServerFqdn = serverFqdn;
			this.Port = port;
			this.UseSsl = useSsl;
			this.Credentials = credentials;
			this.IgnoreCertificateErrors = ignoreCertificateErrors;
			this.MailboxId = mailboxId;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000591A File Offset: 0x00003B1A
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00005922 File Offset: 0x00003B22
		public TimeSpan? Expiration
		{
			get
			{
				return this.expiration;
			}
			set
			{
				this.expiration = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000592B File Offset: 0x00003B2B
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00005933 File Offset: 0x00003B33
		public bool KeepContextsAlive
		{
			get
			{
				return this.keepContextsAlive;
			}
			set
			{
				this.keepContextsAlive = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000593C File Offset: 0x00003B3C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00005944 File Offset: 0x00003B44
		public WebHeaderCollection AdditionalHttpHeaders
		{
			get
			{
				return this.additionalHttpHeaders;
			}
			set
			{
				this.additionalHttpHeaders = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000594D File Offset: 0x00003B4D
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00005955 File Offset: 0x00003B55
		internal bool ShouldTrimVdirPath { get; set; }

		// Token: 0x060000A6 RID: 166 RVA: 0x00005960 File Offset: 0x00003B60
		internal string BuildRequestPath(ref string originalVdirPath)
		{
			string text = originalVdirPath;
			if (this.ShouldTrimVdirPath)
			{
				text = text.TrimEnd(new char[]
				{
					'/'
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "/";
			}
			else if (text[0] != '/')
			{
				text = "/" + text;
			}
			string text2 = string.IsNullOrEmpty(this.MailboxId) ? "useMailboxOfAuthenticatedUser=true" : string.Format("mailboxId={0}", this.MailboxId);
			string result;
			if ((this.UseSsl && this.Port == Constants.DefaultHttpsPort) || (!this.UseSsl && this.Port == Constants.DefaultHttpPort))
			{
				result = string.Format("{0}{1}{2}{3}?{4}", new object[]
				{
					this.UseSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
					Uri.SchemeDelimiter,
					this.ServerFqdn,
					text,
					text2
				});
			}
			else
			{
				result = string.Format("{0}{1}{2}:{3}{4}?{5}", new object[]
				{
					this.UseSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
					Uri.SchemeDelimiter,
					this.ServerFqdn,
					this.Port,
					text,
					text2
				});
			}
			originalVdirPath = text;
			return result;
		}

		// Token: 0x0400003D RID: 61
		public readonly string ServerFqdn;

		// Token: 0x0400003E RID: 62
		public readonly int Port;

		// Token: 0x0400003F RID: 63
		public readonly bool UseSsl;

		// Token: 0x04000040 RID: 64
		public readonly ICredentials Credentials;

		// Token: 0x04000041 RID: 65
		public readonly bool IgnoreCertificateErrors;

		// Token: 0x04000042 RID: 66
		public readonly string MailboxId;

		// Token: 0x04000043 RID: 67
		private TimeSpan? expiration = null;

		// Token: 0x04000044 RID: 68
		private bool keepContextsAlive = true;

		// Token: 0x04000045 RID: 69
		private WebHeaderCollection additionalHttpHeaders = new WebHeaderCollection();
	}
}
