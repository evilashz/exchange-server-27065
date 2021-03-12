using System;
using System.Collections.Generic;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x0200002E RID: 46
	internal class EhfCompanyRecord
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00005C70 File Offset: 0x00003E70
		internal EhfCompanyRecord(Company company, IEnumerable<Domain> domains)
		{
			this.company = company;
			this.domains = new List<DomainSyncRecord>(DomainSyncRecord.CreateDomainSyncRecordList(domains));
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005C90 File Offset: 0x00003E90
		public IList<DomainSyncRecord> Domains
		{
			get
			{
				return this.domains;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005C98 File Offset: 0x00003E98
		public IList<string> GatewayIPAddresses
		{
			get
			{
				if (this.company != null && this.company.Settings != null && this.company.Settings.OnPremiseGatewayIPList != null && this.company.Settings.OnPremiseGatewayIPList.IPList != null)
				{
					return this.company.Settings.OnPremiseGatewayIPList.IPList;
				}
				return EhfCompanyRecord.emptyIpAddressList;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005D00 File Offset: 0x00003F00
		public IList<string> InternalIPAddresses
		{
			get
			{
				if (this.company != null && this.company.Settings != null && this.company.Settings.InternalServerIPList != null && this.company.Settings.InternalServerIPList.IPList != null)
				{
					return this.company.Settings.InternalServerIPList.IPList;
				}
				return EhfCompanyRecord.emptyIpAddressList;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005D68 File Offset: 0x00003F68
		public bool IPSkiplistingEnabled
		{
			get
			{
				return this.company != null && this.company.Settings != null && this.company.Settings.SkipList != null && this.company.Settings.SkipList.Value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005DBE File Offset: 0x00003FBE
		public bool IsEnabled
		{
			get
			{
				return this.company != null && this.company.IsEnabled;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public string CompanyId
		{
			get
			{
				if (this.company != null)
				{
					return this.company.CompanyId.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005E06 File Offset: 0x00004006
		public string CompanyName
		{
			get
			{
				if (this.company != null)
				{
					return this.company.Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005E24 File Offset: 0x00004024
		public Guid Guid
		{
			get
			{
				if (this.company != null && this.company.CompanyGuid != null)
				{
					return this.company.CompanyGuid.Value;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x04000074 RID: 116
		private static readonly List<string> emptyIpAddressList = new List<string>();

		// Token: 0x04000075 RID: 117
		private Company company;

		// Token: 0x04000076 RID: 118
		private List<DomainSyncRecord> domains;
	}
}
