using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000031 RID: 49
	public class EhloCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00005DBA File Offset: 0x00003FBA
		internal EhloCommandEventArgs()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005DC2 File Offset: 0x00003FC2
		internal EhloCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005DCB File Offset: 0x00003FCB
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00005DD3 File Offset: 0x00003FD3
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!RoutingAddress.IsValidDomain(value) && !RoutingAddress.IsDomainIPLiteral(value) && !HeloCommandEventArgs.IsValidIpv6WindowsAddress(value))
				{
					throw new ArgumentException(string.Format("Invalid SMTP domain {0}.  SMTP domains should be of the form 'contoso.com'", value));
				}
				this.domain = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005E13 File Offset: 0x00004013
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00005E20 File Offset: 0x00004020
		public bool DisableStartTls
		{
			get
			{
				return base.SmtpSession.DisableStartTls;
			}
			set
			{
				base.SmtpSession.DisableStartTls = value;
			}
		}

		// Token: 0x04000143 RID: 323
		private string domain;
	}
}
