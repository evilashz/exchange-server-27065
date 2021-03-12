using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Servicelets.Provisioning.Messages;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200000C RID: 12
	internal class ProvisioningAgentContext : DisposeTrackableBase
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000476C File Offset: 0x0000296C
		public ProvisioningAgentContext(Guid jobId, CultureInfo cultureInfo, Guid ownerExchangeObjectId, ADObjectId ownerId, DelegatedPrincipal delegatedAdminOwner, SubmittedByUserAdminType submittedByUserAdminType, string tenantOrganization, OrganizationId organizationId, ExEventLog eventLog)
		{
			this.JobId = jobId;
			this.CultureInfo = cultureInfo;
			this.OwnerExchangeObjectId = ownerExchangeObjectId;
			this.OwnerId = ownerId;
			this.DelegatedAdminOwner = delegatedAdminOwner;
			this.SubmittedByUserAdminType = submittedByUserAdminType;
			this.TenantOrganization = tenantOrganization;
			this.OrganizationId = organizationId;
			this.MigrationRunspace = null;
			this.DatacenterMigrationRunspace = null;
			this.EventLog = eventLog;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000047D2 File Offset: 0x000029D2
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000047DA File Offset: 0x000029DA
		public ExDateTime LastModified
		{
			get
			{
				return this.lastModified;
			}
			internal set
			{
				this.lastModified = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000047E3 File Offset: 0x000029E3
		public RunspaceProxy Runspace
		{
			get
			{
				return this.MigrationRunspace.RunspaceProxy;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000047F0 File Offset: 0x000029F0
		public RunspaceProxy DatacenterRunspace
		{
			get
			{
				return this.DatacenterMigrationRunspace.RunspaceProxy;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000047FD File Offset: 0x000029FD
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00004808 File Offset: 0x00002A08
		private MigrationRunspaceProxy MigrationRunspace
		{
			get
			{
				return this.runspaceProxy;
			}
			set
			{
				this.runspaceProxy = value;
				ExDateTime now = ExDateTime.Now;
				this.lastModified = now;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000482C File Offset: 0x00002A2C
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000048B0 File Offset: 0x00002AB0
		private MigrationRunspaceProxy DatacenterMigrationRunspace
		{
			get
			{
				if (this.datacenterRunspaceProxy == null)
				{
					if (this.Owner == null && this.OrganizationId == null)
					{
						throw new InvalidOperationException("need to have an owner or org id before trying to create the proxy.");
					}
					ExTraceGlobals.WorkerTracer.TraceInformation(0, (long)this.GetHashCode(), "creating datacenter runspace proxy");
					this.datacenterRunspaceProxy = MigrationRunspaceProxy.CreateRunspaceForDatacenterAdmin((this.Owner != null) ? this.Owner.OrganizationId : this.OrganizationId);
					this.lastModified = ExDateTime.Now;
				}
				return this.datacenterRunspaceProxy;
			}
			set
			{
				this.datacenterRunspaceProxy = value;
				this.lastModified = ExDateTime.Now;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000048CC File Offset: 0x00002ACC
		private ADUser Owner { get; set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00004938 File Offset: 0x00002B38
		public bool Initialize()
		{
			if (this.Owner == null || (this.Owner.ExchangeObjectId == Guid.Empty && this.Owner.Id != this.OwnerId) || this.Owner.ExchangeObjectId != this.OwnerExchangeObjectId)
			{
				this.Owner = null;
				if (this.OwnerExchangeObjectId != Guid.Empty)
				{
					IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId), 243, "Initialize", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\Provisioning\\Program\\ProvisioningAgentContext.cs");
					ADNotificationAdapter.RunADOperation(delegate()
					{
						this.Owner = (ADUser)recipientSession.FindByExchangeObjectId(this.OwnerExchangeObjectId);
					});
				}
				else if (this.OwnerId != null)
				{
					IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(this.OwnerId), 258, "Initialize", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\Provisioning\\Program\\ProvisioningAgentContext.cs");
					ADNotificationAdapter.RunADOperation(delegate()
					{
						this.Owner = (ADUser)recipientSession.Read(this.OwnerId);
					});
				}
			}
			if (this.Owner == null && this.DelegatedAdminOwner == null)
			{
				this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_TenantAdminNotFound, string.Empty, new object[]
				{
					this.TenantOrganization
				});
				ExTraceGlobals.WorkerTracer.TraceInformation<ADObjectId>(0, (long)this.GetHashCode(), "couldn't find owner {0}", this.OwnerId);
				return false;
			}
			return this.InitializeRunspaceProxy();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004AA5 File Offset: 0x00002CA5
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.runspaceProxy != null)
				{
					this.runspaceProxy.Dispose();
				}
				this.runspaceProxy = null;
				if (this.datacenterRunspaceProxy != null)
				{
					this.datacenterRunspaceProxy.Dispose();
				}
				this.datacenterRunspaceProxy = null;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004ADE File Offset: 0x00002CDE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProvisioningAgentContext>(this);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004AE8 File Offset: 0x00002CE8
		private bool InitializeRunspaceProxy()
		{
			if (this.runspaceProxy == null)
			{
				if (this.Owner == null && this.DelegatedAdminOwner == null)
				{
					throw new InvalidOperationException("need to have an owner before trying to create the proxy.");
				}
				ExTraceGlobals.WorkerTracer.TraceInformation<SubmittedByUserAdminType>(0, (long)this.GetHashCode(), "creating runspace proxy for {0}", this.SubmittedByUserAdminType);
				try
				{
					if (this.SubmittedByUserAdminType == SubmittedByUserAdminType.Partner)
					{
						if (this.Owner != null)
						{
							this.runspaceProxy = MigrationRunspaceProxy.CreateRunspaceForPartner(this.Owner.Id, this.Owner, this.TenantOrganization);
						}
						else
						{
							this.runspaceProxy = MigrationRunspaceProxy.CreateRunspaceForDelegatedPartner(this.DelegatedAdminOwner, this.TenantOrganization);
						}
					}
					else if (this.Owner != null)
					{
						this.runspaceProxy = MigrationRunspaceProxy.CreateRunspaceForTenantAdmin(this.Owner.Id, this.Owner);
					}
					else
					{
						this.runspaceProxy = MigrationRunspaceProxy.CreateRunspaceForDelegatedTenantAdmin(this.DelegatedAdminOwner);
					}
				}
				catch (MigrationPermanentException arg)
				{
					this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_TenantAdminNotFound, string.Empty, new object[]
					{
						this.TenantOrganization
					});
					ExTraceGlobals.WorkerTracer.TraceInformation<ADObjectId, MigrationPermanentException>(0, (long)this.GetHashCode(), "owner couldn't create runspace{0}", this.OwnerId, arg);
					this.runspaceProxy = null;
				}
			}
			return this.runspaceProxy != null;
		}

		// Token: 0x04000045 RID: 69
		public readonly Guid JobId;

		// Token: 0x04000046 RID: 70
		public readonly CultureInfo CultureInfo;

		// Token: 0x04000047 RID: 71
		public readonly Guid OwnerExchangeObjectId;

		// Token: 0x04000048 RID: 72
		public readonly ADObjectId OwnerId;

		// Token: 0x04000049 RID: 73
		public readonly DelegatedPrincipal DelegatedAdminOwner;

		// Token: 0x0400004A RID: 74
		public readonly SubmittedByUserAdminType SubmittedByUserAdminType;

		// Token: 0x0400004B RID: 75
		public readonly string TenantOrganization;

		// Token: 0x0400004C RID: 76
		public readonly OrganizationId OrganizationId;

		// Token: 0x0400004D RID: 77
		public readonly ExEventLog EventLog;

		// Token: 0x0400004E RID: 78
		private ExDateTime lastModified;

		// Token: 0x0400004F RID: 79
		private MigrationRunspaceProxy runspaceProxy;

		// Token: 0x04000050 RID: 80
		private MigrationRunspaceProxy datacenterRunspaceProxy;
	}
}
