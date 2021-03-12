using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000031 RID: 49
	public class ExchangeTenantRecord
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000067AC File Offset: 0x000049AC
		public ExchangeTenantRecord(OrganizationIdParameter identity, SyncedPerimeterConfig config, IEnumerable<SyncedAcceptedDomain> acceptedDomains)
		{
			this.organization = identity;
			this.perimeterConfig = config;
			this.acceptedDomains = new Dictionary<string, SyncedAcceptedDomain>();
			this.domains = new List<DomainSyncRecord>();
			foreach (SyncedAcceptedDomain syncedAcceptedDomain in acceptedDomains)
			{
				DomainSyncRecord domainSyncRecord = new DomainSyncRecord(syncedAcceptedDomain);
				this.domains.Add(domainSyncRecord);
				this.acceptedDomains.Add(domainSyncRecord.Name, syncedAcceptedDomain);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000683C File Offset: 0x00004A3C
		public SyncedPerimeterConfig PerimeterConfig
		{
			get
			{
				return this.perimeterConfig;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00006844 File Offset: 0x00004A44
		public Dictionary<string, SyncedAcceptedDomain> AcceptedDomains
		{
			get
			{
				return this.acceptedDomains;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000684C File Offset: 0x00004A4C
		public OrganizationIdParameter Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006854 File Offset: 0x00004A54
		public IList<string> GatewayIPAddresses
		{
			get
			{
				if (this.gatewayServerIpAddresses == null)
				{
					this.gatewayServerIpAddresses = Utils.ConvertIPAddresssesToStrings(this.PerimeterConfig.GatewayIPAddresses);
				}
				return this.gatewayServerIpAddresses;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000687A File Offset: 0x00004A7A
		public IList<string> InternalIPAddresses
		{
			get
			{
				if (this.internalServerIpAddresses == null)
				{
					this.internalServerIpAddresses = Utils.ConvertIPAddresssesToStrings(this.PerimeterConfig.InternalServerIPAddresses);
				}
				return this.internalServerIpAddresses;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000068A0 File Offset: 0x00004AA0
		public bool IPSkiplistingEnabled
		{
			get
			{
				return this.PerimeterConfig.IPSkiplistingEnabled;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000068AD File Offset: 0x00004AAD
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000068B0 File Offset: 0x00004AB0
		public string CompanyId
		{
			get
			{
				return this.PerimeterConfig.PerimeterOrgId;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000068C0 File Offset: 0x00004AC0
		public string CompanyName
		{
			get
			{
				string text = this.PerimeterConfig.Identity.ToString();
				return text.Substring(0, text.IndexOf('\\'));
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000068EF File Offset: 0x00004AEF
		public Guid Guid
		{
			get
			{
				return this.PerimeterConfig.Guid;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000068FC File Offset: 0x00004AFC
		internal IList<DomainSyncRecord> Domains
		{
			get
			{
				if (this.domains == null)
				{
					this.domains = DomainSyncRecord.CreateDomainSyncRecordList(this.AcceptedDomains.Values);
				}
				return this.domains;
			}
		}

		// Token: 0x04000082 RID: 130
		private SyncedPerimeterConfig perimeterConfig;

		// Token: 0x04000083 RID: 131
		private OrganizationIdParameter organization;

		// Token: 0x04000084 RID: 132
		private Dictionary<string, SyncedAcceptedDomain> acceptedDomains;

		// Token: 0x04000085 RID: 133
		private IList<string> gatewayServerIpAddresses;

		// Token: 0x04000086 RID: 134
		private IList<string> internalServerIpAddresses;

		// Token: 0x04000087 RID: 135
		private IList<DomainSyncRecord> domains;
	}
}
