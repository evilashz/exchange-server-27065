using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F9 RID: 1017
	internal class MatchableDomain
	{
		// Token: 0x06002E88 RID: 11912 RVA: 0x000BBA50 File Offset: 0x000B9C50
		public MatchableDomain(SmtpDomainWithSubdomains domain)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			if (domain.IsStar)
			{
				throw new ArgumentException("Domain cannot be just \"*\"");
			}
			this.domain = domain;
			this.dotCount = 0;
			this.firstDotIndex = -1;
			string domainName = this.DomainName;
			for (int i = 0; i < domainName.Length; i++)
			{
				if (domainName[i] == '.')
				{
					this.dotCount++;
					if (this.dotCount == 1)
					{
						this.firstDotIndex = i;
					}
				}
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000BBAD7 File Offset: 0x000B9CD7
		public SmtpDomainWithSubdomains Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000BBADF File Offset: 0x000B9CDF
		public string DomainName
		{
			get
			{
				return this.domain.SmtpDomain.Domain;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000BBAF1 File Offset: 0x000B9CF1
		public bool IncludeSubdomains
		{
			get
			{
				return this.domain.IncludeSubDomains;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000BBAFE File Offset: 0x000B9CFE
		public int DotCount
		{
			get
			{
				return this.dotCount;
			}
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000BBB08 File Offset: 0x000B9D08
		public int MatchCertName(string certName, MatchOptions options, int matchDotCountThreshold)
		{
			if (string.IsNullOrEmpty(certName))
			{
				return -1;
			}
			int num = 0;
			bool flag = certName[0] == '*';
			if (flag)
			{
				if (certName.Length < 3 || certName[1] != '.')
				{
					return -1;
				}
				num = 2;
			}
			if (this.EqualsToSubstring(certName, num))
			{
				return int.MaxValue;
			}
			bool flag2 = (options & MatchOptions.MultiLevelCertWildcards) != MatchOptions.None;
			if (matchDotCountThreshold < this.dotCount && (flag != this.IncludeSubdomains || (flag && flag2)))
			{
				int num2 = this.dotCount;
				int num3 = 0;
				bool flag3 = false;
				if (flag)
				{
					if (this.firstDotIndex == -1 || this.DomainName.Length <= certName.Length - num)
					{
						return -1;
					}
					if (flag2)
					{
						num--;
						num3 = this.DomainName.Length - certName.Length + 1;
						flag3 = true;
					}
					else
					{
						num3 = this.firstDotIndex + 1;
						num2--;
						if (matchDotCountThreshold >= num2)
						{
							return -1;
						}
					}
				}
				else
				{
					num = certName.IndexOf('.');
					if (num == -1)
					{
						return -1;
					}
					num++;
				}
				if (this.EndsWithSubstring(num3, certName, num))
				{
					if (flag3)
					{
						num2 = this.CountDotsFrom(num3 + 1);
						if (matchDotCountThreshold >= num2)
						{
							return -1;
						}
					}
					return num2;
				}
			}
			return -1;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000BBC20 File Offset: 0x000B9E20
		private bool EqualsToSubstring(string s, int substrStartIndex)
		{
			return this.EndsWithSubstring(0, s, substrStartIndex);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000BBC2C File Offset: 0x000B9E2C
		private bool EndsWithSubstring(int startIndex, string s, int substrStartIndex)
		{
			string domainName = this.DomainName;
			return domainName.Length - startIndex == s.Length - substrStartIndex && string.Compare(domainName, startIndex, s, substrStartIndex, domainName.Length - startIndex, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000BBC6C File Offset: 0x000B9E6C
		private int CountDotsFrom(int startIndex)
		{
			string domainName = this.DomainName;
			int num = 0;
			for (int i = startIndex; i < domainName.Length; i++)
			{
				if (domainName[i] == '.')
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x040016F3 RID: 5875
		private const char Dot = '.';

		// Token: 0x040016F4 RID: 5876
		private const char Star = '*';

		// Token: 0x040016F5 RID: 5877
		private readonly SmtpDomainWithSubdomains domain;

		// Token: 0x040016F6 RID: 5878
		private readonly int dotCount;

		// Token: 0x040016F7 RID: 5879
		private readonly int firstDotIndex;
	}
}
