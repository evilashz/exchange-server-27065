using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F9 RID: 761
	internal class AcceptedDomainEntry : AcceptedDomain, DomainMatchMap<AcceptedDomainEntry>.IDomainEntry
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x00099E3C File Offset: 0x0009803C
		public AcceptedDomainEntry(AcceptedDomain domain, OrganizationId organizationId)
		{
			this.name = domain.Name;
			this.domain = domain.DomainName;
			this.DomainType = domain.DomainType;
			this.MatchSubDomains = domain.MatchSubDomains;
			this.IsDefault = domain.Default;
			this.AddressBookEnabled = domain.AddressBookEnabled;
			this.Guid = domain.Guid;
			this.syncedToPerimeterAsGuidDomain = (domain.PerimeterDuplicateDetected || domain.OutboundOnly);
			if (domain.CatchAllRecipientID != null)
			{
				this.catchAllRecipientID = domain.CatchAllRecipientID.ObjectGuid;
			}
			if (organizationId == null || organizationId == OrganizationId.ForestWideOrgId || organizationId.ConfigurationUnit == null)
			{
				this.tenantId = Guid.Empty;
				return;
			}
			this.tenantId = organizationId.ConfigurationUnit.ObjectGuid;
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00099F0E File Offset: 0x0009810E
		public bool IsAuthoritative
		{
			get
			{
				return this.DomainType == AcceptedDomainType.Authoritative;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x00099F19 File Offset: 0x00098119
		public bool IsInternal
		{
			get
			{
				return this.DomainType != AcceptedDomainType.ExternalRelay;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00099F27 File Offset: 0x00098127
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				if (this.domain.IncludeSubDomains || !this.MatchSubDomains)
				{
					return this.domain;
				}
				return new SmtpDomainWithSubdomains(this.domain.Domain, true);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x00099F56 File Offset: 0x00098156
		public int EstimatedSize
		{
			get
			{
				return RemoteDomainEntry.GetLenghAfterNullCheck(this.DomainName.Domain) * 2 + 1 + RemoteDomainEntry.GetLenghAfterNullCheck(this.Name) * 2 + RemoteDomainEntry.GetLenghAfterNullCheck(this.NameSpecification) * 2 + 3 + 4 + 64;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x00099F8F File Offset: 0x0009818F
		public override bool IsInCorporation
		{
			get
			{
				return this.IsInternal;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x00099F97 File Offset: 0x00098197
		public override bool UseAddressBook
		{
			get
			{
				return this.AddressBookEnabled;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x00099F9F File Offset: 0x0009819F
		public override string NameSpecification
		{
			get
			{
				return this.domain.ToString();
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00099FAC File Offset: 0x000981AC
		public override Guid TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x00099FB4 File Offset: 0x000981B4
		public Guid CatchAllRecipientID
		{
			get
			{
				return this.catchAllRecipientID;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x00099FBC File Offset: 0x000981BC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00099FC4 File Offset: 0x000981C4
		public bool SyncedToPerimeterAsGuidDomain
		{
			get
			{
				return this.syncedToPerimeterAsGuidDomain;
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x00099FCC File Offset: 0x000981CC
		public override string ToString()
		{
			return this.NameSpecification;
		}

		// Token: 0x0400160E RID: 5646
		private const int GuidLength = 16;

		// Token: 0x0400160F RID: 5647
		public readonly AcceptedDomainType DomainType;

		// Token: 0x04001610 RID: 5648
		public readonly bool MatchSubDomains;

		// Token: 0x04001611 RID: 5649
		public readonly bool IsDefault;

		// Token: 0x04001612 RID: 5650
		public readonly bool AddressBookEnabled;

		// Token: 0x04001613 RID: 5651
		public readonly Guid Guid;

		// Token: 0x04001614 RID: 5652
		private readonly string name;

		// Token: 0x04001615 RID: 5653
		private readonly SmtpDomainWithSubdomains domain;

		// Token: 0x04001616 RID: 5654
		private readonly Guid tenantId;

		// Token: 0x04001617 RID: 5655
		private readonly Guid catchAllRecipientID;

		// Token: 0x04001618 RID: 5656
		private readonly bool syncedToPerimeterAsGuidDomain;
	}
}
