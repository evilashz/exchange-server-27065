using System;
using System.Net;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000035 RID: 53
	public class HeloCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00005EAC File Offset: 0x000040AC
		internal HeloCommandEventArgs()
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005EB4 File Offset: 0x000040B4
		internal HeloCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005EBD File Offset: 0x000040BD
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005EC5 File Offset: 0x000040C5
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

		// Token: 0x0600013F RID: 319 RVA: 0x00005F08 File Offset: 0x00004108
		internal static bool IsValidIpv6WindowsAddress(string domain)
		{
			IPAddress ipaddress;
			return (domain != null && domain.StartsWith("[IPv6:", StringComparison.InvariantCultureIgnoreCase) && domain.EndsWith("]") && IPAddress.TryParse(domain.Substring(6, domain.Length - 7), out ipaddress)) || (domain != null && IPAddress.TryParse(domain, out ipaddress));
		}

		// Token: 0x04000149 RID: 329
		private string domain;
	}
}
