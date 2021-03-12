using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200001C RID: 28
	internal class EhfConfigTargetConnection : EhfTargetConnection
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00009758 File Offset: 0x00007958
		public EhfConfigTargetConnection(int localServerVersion, EhfTargetServerConfig config, EnhancedTimeSpan syncInterval, EdgeSyncLogSession logSession) : base(localServerVersion, config, syncInterval, logSession)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009768 File Offset: 0x00007968
		public EhfConfigTargetConnection(int localServerVersion, EhfTargetServerConfig config, EdgeSyncLogSession logSession, EhfPerfCounterHandler perfCounterHandler, IProvisioningService provisioningService, IManagementService managementService, EhfADAdapter adapter, EnhancedTimeSpan syncInterval) : base(localServerVersion, config, logSession, perfCounterHandler, provisioningService, managementService, null, adapter, syncInterval)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009789 File Offset: 0x00007989
		public override void AbortSyncCycle(Exception cause)
		{
			if (this.companySync != null)
			{
				this.companySync.ClearBatches();
			}
			if (this.domainSync != null)
			{
				this.domainSync.ClearBatches();
			}
			base.AbortSyncCycle(cause);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000097B8 File Offset: 0x000079B8
		public override bool OnSynchronizing()
		{
			bool result = base.OnSynchronizing();
			if (this.companySync != null)
			{
				throw new InvalidOperationException("Company sync already exists");
			}
			this.companySync = new EhfCompanySynchronizer(this);
			if (this.domainSync != null)
			{
				throw new InvalidOperationException("Domain sync already exists");
			}
			if (base.Config.EhfWebServiceVersion == EhfWebServiceVersion.Version1)
			{
				this.domainSync = new EhfDomainSynchronizer(this);
			}
			else
			{
				if (base.Config.EhfWebServiceVersion != EhfWebServiceVersion.Version2)
				{
					throw new InvalidOperationException("This version of EHF provider does not support EHF Webservice version " + base.Config.EhfWebServiceVersion);
				}
				this.domainSync = new EhfDomainSynchronizerVersion2(this);
			}
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009857 File Offset: 0x00007A57
		public override void OnConnectedToSource(Connection sourceConnection)
		{
			base.ADAdapter.SetConnection(sourceConnection);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009865 File Offset: 0x00007A65
		public override bool OnSynchronized()
		{
			return (this.companySync == null || this.companySync.FlushBatches()) && (this.domainSync == null || this.domainSync.FlushBatches());
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009898 File Offset: 0x00007A98
		public override SyncResult OnAddEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			SyncResult result = SyncResult.Added;
			EhfConfigTargetConnection.ConfigObjectType syncObjectType = this.GetSyncObjectType(entry, "Add");
			switch (syncObjectType)
			{
			case EhfConfigTargetConnection.ConfigObjectType.AcceptedDomain:
				this.domainSync.HandleAddedDomain(entry);
				break;
			case EhfConfigTargetConnection.ConfigObjectType.PerimeterSettings:
				this.companySync.CreateOrModifyEhfCompany(entry);
				break;
			default:
				throw new InvalidOperationException("EhfConfigTargetConnection.GetSyncObjectType() returned unexpected value " + syncObjectType);
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000098FC File Offset: 0x00007AFC
		public override SyncResult OnModifyEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			SyncResult result = SyncResult.Modified;
			EhfConfigTargetConnection.ConfigObjectType syncObjectType = this.GetSyncObjectType(entry, "Modify");
			switch (syncObjectType)
			{
			case EhfConfigTargetConnection.ConfigObjectType.AcceptedDomain:
				this.domainSync.HandleModifiedDomain(entry);
				break;
			case EhfConfigTargetConnection.ConfigObjectType.PerimeterSettings:
				this.companySync.CreateOrModifyEhfCompany(entry);
				break;
			default:
				throw new InvalidOperationException("EhfConfigTargetConnection.GetSyncObjectType() returned unexpected value " + syncObjectType);
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009960 File Offset: 0x00007B60
		public override SyncResult OnDeleteEntry(ExSearchResultEntry entry)
		{
			EhfConfigTargetConnection.ConfigObjectType syncObjectType = this.GetSyncObjectType(entry, "Delete");
			SyncResult result;
			switch (syncObjectType)
			{
			case EhfConfigTargetConnection.ConfigObjectType.AcceptedDomain:
				this.domainSync.HandleDeletedDomain(entry);
				result = SyncResult.Deleted;
				break;
			case EhfConfigTargetConnection.ConfigObjectType.PerimeterSettings:
				this.companySync.DeleteEhfCompany(entry);
				result = SyncResult.Deleted;
				break;
			default:
				throw new InvalidOperationException("EhfConfigTargetConnection.GetSyncObjectType() returned unexpected value " + syncObjectType);
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000099C5 File Offset: 0x00007BC5
		public bool TryGetEhfCompanyIdentity(string configUnitDN, string missingIdAction, out EhfCompanyIdentity companyIdentity)
		{
			return this.companySync.TryGetEhfCompanyIdentity(configUnitDN, missingIdAction, out companyIdentity);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000099D8 File Offset: 0x00007BD8
		public void AddDomainsForNewCompany(EhfCompanyItem company)
		{
			if (company.CompanyId == 0)
			{
				throw new ArgumentException("Cannot push domains for a company without CompanyId");
			}
			if (company.IsDeleted)
			{
				throw new ArgumentException("Cannot push domains for a deleted company");
			}
			ADObjectId parent = new ADObjectId(company.DistinguishedName).Parent;
			this.domainSync.CreateEhfDomainsForNewCompany(parent.DistinguishedName, company.CompanyId, company.GetCompanyGuid());
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009A3C File Offset: 0x00007C3C
		private EhfConfigTargetConnection.ConfigObjectType GetSyncObjectType(ExSearchResultEntry entry, string operation)
		{
			string objectClass = entry.ObjectClass;
			if (string.IsNullOrEmpty(objectClass))
			{
				base.DiagSession.LogAndTraceError("Entry <{0}> contains no objectClass attribute in operation {1}; cannot proceed", new object[]
				{
					entry.DistinguishedName,
					operation
				});
				throw new ArgumentException("Entry does not contain objectClass attribute", "entry");
			}
			string a;
			if ((a = objectClass) != null)
			{
				if (a == "msExchAcceptedDomain")
				{
					return EhfConfigTargetConnection.ConfigObjectType.AcceptedDomain;
				}
				if (a == "msExchTenantPerimeterSettings")
				{
					return EhfConfigTargetConnection.ConfigObjectType.PerimeterSettings;
				}
			}
			base.DiagSession.LogAndTraceError("Entry <{0}> contains unexpected objectClass value <{1}> in operation {2}; cannot proceed", new object[]
			{
				entry.DistinguishedName,
				objectClass,
				operation
			});
			throw new ArgumentException("Entry's objectClass is invalid: " + objectClass, "entry");
		}

		// Token: 0x0400007C RID: 124
		private EhfCompanySynchronizer companySync;

		// Token: 0x0400007D RID: 125
		private EhfDomainSynchronizer domainSync;

		// Token: 0x0200001D RID: 29
		private enum ConfigObjectType
		{
			// Token: 0x0400007F RID: 127
			AcceptedDomain,
			// Token: 0x04000080 RID: 128
			PerimeterSettings,
			// Token: 0x04000081 RID: 129
			Unknown
		}
	}
}
