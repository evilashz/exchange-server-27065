using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A8 RID: 168
	internal sealed class OwaPerTenantTransportSettings : TenantConfigurationCacheableItem<TransportConfigContainer>
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001489C File Offset: 0x00012A9C
		public override long ItemSize
		{
			get
			{
				if (this.tlsSendDomainSecureList == null)
				{
					return 18L;
				}
				int num = 18;
				foreach (string text in this.tlsSendDomainSecureList)
				{
					num += text.Length;
				}
				return (long)num;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00014904 File Offset: 0x00012B04
		public override void ReadData(IConfigurationSession session)
		{
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length != 1 || array[0] == null)
			{
				throw new InvalidDataResultException(string.Format("One and only one TransportConfigContainer was expected. But found these many: {0}", (array == null) ? 0 : array.Length));
			}
			MultiValuedProperty<SmtpDomain> tlssendDomainSecureList = array[0].TLSSendDomainSecureList;
			if (tlssendDomainSecureList != null)
			{
				this.tlsSendDomainSecureList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (SmtpDomain smtpDomain in tlssendDomainSecureList)
				{
					if (!string.IsNullOrEmpty(smtpDomain.Domain))
					{
						this.tlsSendDomainSecureList.Add(smtpDomain.Domain);
					}
				}
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000149C0 File Offset: 0x00012BC0
		internal bool IsTLSSendSecureDomain(string domainName)
		{
			return this.tlsSendDomainSecureList != null && this.tlsSendDomainSecureList.Count > 0 && this.tlsSendDomainSecureList.Contains(domainName);
		}

		// Token: 0x040003AD RID: 941
		private const int FixedClrObjectOverhead = 18;

		// Token: 0x040003AE RID: 942
		private HashSet<string> tlsSendDomainSecureList;
	}
}
