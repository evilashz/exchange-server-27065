using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000030 RID: 48
	public static class SmtpDomainWithSubdomainsMvpExtension
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000853C File Offset: 0x0000673C
		public static void InsertDomain(this MultiValuedProperty<SmtpDomainWithSubdomains> domainsList, string domainName)
		{
			if (domainsList != null && !string.IsNullOrEmpty(domainName))
			{
				SmtpDomainWithSubdomains domain = new SmtpDomainWithSubdomains(domainName);
				domainsList.InsertDomain(domain);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008562 File Offset: 0x00006762
		public static void InsertDomain(this MultiValuedProperty<SmtpDomainWithSubdomains> domainsList, SmtpDomainWithSubdomains domain)
		{
			if (domainsList != null && domain != null && !domainsList.Contains(domain))
			{
				domainsList.Add(domain);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000857C File Offset: 0x0000677C
		public static void InsertDomainRange(this MultiValuedProperty<SmtpDomainWithSubdomains> domainsList, MultiValuedProperty<SmtpDomainWithSubdomains> domains)
		{
			if (domainsList != null && domains != null)
			{
				foreach (SmtpDomainWithSubdomains domain in domains)
				{
					domainsList.InsertDomain(domain);
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000085D0 File Offset: 0x000067D0
		public static MultiValuedProperty<SmtpDomainWithSubdomains> AddPrefixForEachDomain(this MultiValuedProperty<SmtpDomainWithSubdomains> domainsList, string prefix)
		{
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty = new MultiValuedProperty<SmtpDomainWithSubdomains>();
			if (domainsList != null)
			{
				foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in domainsList)
				{
					string s = prefix + smtpDomainWithSubdomains.SmtpDomain.Domain;
					multiValuedProperty.InsertDomain(new SmtpDomainWithSubdomains(s, smtpDomainWithSubdomains.IncludeSubDomains));
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008648 File Offset: 0x00006848
		public static bool IsSameWith(this MultiValuedProperty<SmtpDomainWithSubdomains> domainsList, MultiValuedProperty<SmtpDomainWithSubdomains> domains)
		{
			if (domainsList == domains)
			{
				return true;
			}
			if (domains.Count != domainsList.Count)
			{
				return false;
			}
			foreach (SmtpDomainWithSubdomains item in domains)
			{
				if (!domainsList.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000086B8 File Offset: 0x000068B8
		public static SmtpDomainWithSubdomains GetShortestDomain(this MultiValuedProperty<SmtpDomainWithSubdomains> domainsList)
		{
			SmtpDomainWithSubdomains smtpDomainWithSubdomains = null;
			if (domainsList != null)
			{
				foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains2 in domainsList)
				{
					int num = (smtpDomainWithSubdomains == null) ? int.MaxValue : smtpDomainWithSubdomains.ToString().Length;
					if (smtpDomainWithSubdomains2.ToString().Length < num)
					{
						smtpDomainWithSubdomains = smtpDomainWithSubdomains2;
					}
				}
			}
			return smtpDomainWithSubdomains;
		}
	}
}
