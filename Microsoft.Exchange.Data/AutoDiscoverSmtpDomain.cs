using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public class AutoDiscoverSmtpDomain : SmtpDomain
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x0001EE57 File Offset: 0x0001D057
		public AutoDiscoverSmtpDomain(string domain) : this(domain, true)
		{
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001EE61 File Offset: 0x0001D061
		private AutoDiscoverSmtpDomain(string domain, bool check) : base(domain, check)
		{
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0001EE6B File Offset: 0x0001D06B
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0001EE73 File Offset: 0x0001D073
		public bool AutoDiscover { get; set; }

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001EE7C File Offset: 0x0001D07C
		public new static AutoDiscoverSmtpDomain Parse(string text)
		{
			bool autoDiscover;
			string domain = AutoDiscoverSmtpDomain.Parse(text, out autoDiscover);
			return new AutoDiscoverSmtpDomain(domain)
			{
				AutoDiscover = autoDiscover
			};
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		public static bool TryParse(string text, out AutoDiscoverSmtpDomain obj)
		{
			if (!string.IsNullOrEmpty(text))
			{
				bool autoDiscover;
				string domain = AutoDiscoverSmtpDomain.Parse(text, out autoDiscover);
				if (SmtpAddress.IsValidDomain(domain))
				{
					obj = new AutoDiscoverSmtpDomain(domain, false)
					{
						AutoDiscover = autoDiscover
					};
					return true;
				}
			}
			obj = null;
			return false;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		public new static AutoDiscoverSmtpDomain GetDomainPart(RoutingAddress address)
		{
			string domainPart = address.DomainPart;
			if (domainPart != null)
			{
				return new AutoDiscoverSmtpDomain(domainPart, false);
			}
			return null;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001EF05 File Offset: 0x0001D105
		public bool Equals(AutoDiscoverSmtpDomain rhs)
		{
			return base.Equals(rhs);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001EF0E File Offset: 0x0001D10E
		public override string ToString()
		{
			if (!this.AutoDiscover)
			{
				return base.ToString();
			}
			return string.Format("{0}{1}", "autod:", base.ToString());
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001EF34 File Offset: 0x0001D134
		private static string Parse(string text, out bool autoDiscover)
		{
			autoDiscover = false;
			if (!string.IsNullOrEmpty(text) && text.StartsWith("autod:", StringComparison.OrdinalIgnoreCase))
			{
				autoDiscover = true;
				return text.Substring("autod:".Length);
			}
			return text;
		}

		// Token: 0x04000629 RID: 1577
		private const string AutoDiscoverPrefix = "autod:";
	}
}
