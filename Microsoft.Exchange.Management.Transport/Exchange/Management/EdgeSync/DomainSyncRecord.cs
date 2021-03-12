using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x0200002D RID: 45
	internal class DomainSyncRecord
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005A70 File Offset: 0x00003C70
		public DomainSyncRecord(AcceptedDomain acceptedDomain)
		{
			if (acceptedDomain.OutboundOnly || acceptedDomain.PerimeterDuplicateDetected)
			{
				this.name = AcceptedDomain.FormatEhfOutboundOnlyDomainName(acceptedDomain.DomainName.Domain.Trim(), acceptedDomain.Guid).ToLower().Trim();
			}
			else
			{
				this.name = acceptedDomain.DomainName.Domain.ToLower().Trim();
			}
			this.guid = acceptedDomain.Guid;
			this.enabled = true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public DomainSyncRecord(Domain domain)
		{
			if (domain.DomainGuid != null)
			{
				this.guid = domain.DomainGuid.Value;
			}
			else
			{
				this.guid = Guid.Empty;
			}
			this.enabled = domain.IsEnabled;
			this.name = domain.Name.ToLower().Trim();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005B56 File Offset: 0x00003D56
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005B5E File Offset: 0x00003D5E
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005B66 File Offset: 0x00003D66
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005B6E File Offset: 0x00003D6E
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005B76 File Offset: 0x00003D76
		public override bool Equals(object obj)
		{
			return obj != null && (object.ReferenceEquals(this, obj) || this.Equals(obj as DomainSyncRecord));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005B94 File Offset: 0x00003D94
		public bool Equals(DomainSyncRecord record)
		{
			return record != null && (object.ReferenceEquals(this, record) || this.name.Equals(record.Name, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public static IList<DomainSyncRecord> CreateDomainSyncRecordList(IEnumerable<SyncedAcceptedDomain> acceptedDomains)
		{
			List<DomainSyncRecord> list = new List<DomainSyncRecord>();
			foreach (AcceptedDomain acceptedDomain in acceptedDomains)
			{
				list.Add(new DomainSyncRecord(acceptedDomain));
			}
			return list;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005C1C File Offset: 0x00003E1C
		public static IList<DomainSyncRecord> CreateDomainSyncRecordList(IEnumerable<Domain> ehfDomains)
		{
			List<DomainSyncRecord> list = new List<DomainSyncRecord>();
			foreach (Domain domain in ehfDomains)
			{
				list.Add(new DomainSyncRecord(domain));
			}
			return list;
		}

		// Token: 0x04000071 RID: 113
		private readonly string name;

		// Token: 0x04000072 RID: 114
		private readonly bool enabled;

		// Token: 0x04000073 RID: 115
		private readonly Guid guid;
	}
}
