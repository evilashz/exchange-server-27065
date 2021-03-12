using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200003A RID: 58
	public abstract class DataAccessTask<TDataObject> : Task where TDataObject : IConfigurable, new()
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000A588 File Offset: 0x00008788
		protected DataAccessTask()
		{
			this.optionalData = new OptionalIdentityData();
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000A59B File Offset: 0x0000879B
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000A5B2 File Offset: 0x000087B2
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000A5C5 File Offset: 0x000087C5
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000A5CD File Offset: 0x000087CD
		protected NetworkCredential NetCredential
		{
			get
			{
				return this.netCredential;
			}
			set
			{
				this.netCredential = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A5D6 File Offset: 0x000087D6
		protected ADObjectId CurrentOrgContainerId
		{
			get
			{
				if (base.CurrentOrganizationId != null && base.CurrentOrganizationId.ConfigurationUnit != null)
				{
					return base.CurrentOrganizationId.ConfigurationUnit;
				}
				return this.RootOrgContainerId;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A608 File Offset: 0x00008808
		internal ADObjectId RootOrgContainerId
		{
			get
			{
				if (this.rootOrgId == null)
				{
					this.rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, string.IsNullOrEmpty(this.DomainController) ? null : this.NetCredential);
				}
				return this.rootOrgId;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A654 File Offset: 0x00008854
		internal ADSessionSettings OrgWideSessionSettings
		{
			get
			{
				if (this.orgWideSessionSettings == null || !this.orgWideSessionSettings.CurrentOrganizationId.Equals(base.CurrentOrganizationId))
				{
					ADSessionSettings adsessionSettings = this.orgWideSessionSettings;
					this.orgWideSessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
					if (adsessionSettings != null)
					{
						this.orgWideSessionSettings.IsSharedConfigChecked = adsessionSettings.IsSharedConfigChecked;
						this.orgWideSessionSettings.IsRedirectedToSharedConfig = adsessionSettings.IsRedirectedToSharedConfig;
					}
				}
				return this.orgWideSessionSettings;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000A6D4 File Offset: 0x000088D4
		internal virtual IConfigurationSession ConfigurationSession
		{
			get
			{
				if (this.configurationSession == null || this.ShouldChangeScope(this.configurationSession))
				{
					IConfigurationSession oldSession = this.configurationSession;
					this.configurationSession = this.CreateConfigurationSession(this.OrgWideSessionSettings);
					ADSession.CopySettableSessionPropertiesAndSettings(oldSession, this.configurationSession);
				}
				return this.configurationSession;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A724 File Offset: 0x00008924
		internal IRecipientSession TenantGlobalCatalogSession
		{
			get
			{
				if (this.tenantGlobalCatalogSession == null || this.ShouldChangeScope(this.tenantGlobalCatalogSession))
				{
					IRecipientSession oldSession = this.tenantGlobalCatalogSession;
					this.tenantGlobalCatalogSession = this.CreateTenantGlobalCatalogSession(this.OrgWideSessionSettings);
					ADSession.CopySettableSessionPropertiesAndSettings(oldSession, this.tenantGlobalCatalogSession);
				}
				return this.tenantGlobalCatalogSession;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A772 File Offset: 0x00008972
		internal ITopologyConfigurationSession GlobalConfigSession
		{
			get
			{
				if (this.globalConfigSession == null)
				{
					this.CreateGlobalConfigSession();
				}
				return this.globalConfigSession;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000A788 File Offset: 0x00008988
		internal ITopologyConfigurationSession PartitionConfigSession
		{
			get
			{
				if (this.partitionOrRootOrgGlobalConfigSession == null || (base.CurrentOrganizationId.PartitionId != null && this.partitionOrRootOrgGlobalConfigSession.SessionSettings.PartitionId != null && !base.CurrentOrganizationId.PartitionId.Equals(this.partitionOrRootOrgGlobalConfigSession.SessionSettings.PartitionId)))
				{
					ITopologyConfigurationSession oldSession = this.partitionOrRootOrgGlobalConfigSession;
					this.CreateCurrentPartitionTopologyConfigSession();
					ADSession.CopySettableSessionPropertiesAndSettings(oldSession, this.partitionOrRootOrgGlobalConfigSession);
				}
				return this.partitionOrRootOrgGlobalConfigSession;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A809 File Offset: 0x00008A09
		internal ITopologyConfigurationSession RootOrgGlobalConfigSession
		{
			get
			{
				if (this.rootOrgGlobalConfigSession == null)
				{
					this.CreateRootOrgConfigSession();
				}
				return this.rootOrgGlobalConfigSession;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A81F File Offset: 0x00008A1F
		internal ITopologyConfigurationSession ReadWriteRootOrgGlobalConfigSession
		{
			get
			{
				if (this.readWriteRootOrgGlobalConfigSession == null)
				{
					this.CreateReadWriteRootOrgConfigSession();
				}
				return this.readWriteRootOrgGlobalConfigSession;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A838 File Offset: 0x00008A38
		internal IRecipientSession PartitionOrRootOrgGlobalCatalogSession
		{
			get
			{
				if (this.partitionOrRootOrgGlobalCatalogSession == null || (base.CurrentOrganizationId.PartitionId != null && this.partitionOrRootOrgGlobalCatalogSession.SessionSettings.PartitionId != null && !base.CurrentOrganizationId.PartitionId.Equals(this.partitionOrRootOrgGlobalCatalogSession.SessionSettings.PartitionId)))
				{
					IRecipientSession oldSession = this.partitionOrRootOrgGlobalCatalogSession;
					this.CreatePartitionAllTenantsOrRootOrgGlobalCatalogSession();
					ADSession.CopySettableSessionPropertiesAndSettings(oldSession, this.partitionOrRootOrgGlobalCatalogSession);
				}
				return this.partitionOrRootOrgGlobalCatalogSession;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A8B9 File Offset: 0x00008AB9
		internal IRootOrganizationRecipientSession RootOrgGlobalCatalogSession
		{
			get
			{
				if (this.rootOrgGlobalCatalogSession == null)
				{
					this.CreateRootOrgGlobalCatalogSession();
				}
				return this.rootOrgGlobalCatalogSession;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		internal virtual IRecipientSession CreateTenantGlobalCatalogSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(flag ? this.DomainController : null, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 375, "CreateTenantGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			if (!tenantOrRootOrgRecipientSession.IsReadConnectionAvailable())
			{
				tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 384, "CreateTenantGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			}
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A97C File Offset: 0x00008B7C
		internal virtual IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(flag ? this.DomainController : null, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 419, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		private void CreateGlobalConfigSession()
		{
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, ADSessionSettings.FromRootOrgScopeSet());
			this.globalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(flag ? this.DomainController : null, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), ADSessionSettings.FromRootOrgScopeSet(), 433, "CreateGlobalConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000AA74 File Offset: 0x00008C74
		private void CreatePartitionAllTenantsOrRootOrgGlobalCatalogSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CurrentOrganizationId);
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			this.partitionOrRootOrgGlobalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(flag ? this.DomainController : null, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 450, "CreatePartitionAllTenantsOrRootOrgGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			if (!this.partitionOrRootOrgGlobalCatalogSession.IsReadConnectionAvailable())
			{
				this.partitionOrRootOrgGlobalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 461, "CreatePartitionAllTenantsOrRootOrgGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			}
			this.partitionOrRootOrgGlobalCatalogSession.UseGlobalCatalog = true;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000AB38 File Offset: 0x00008D38
		private void CreateCurrentPartitionTopologyConfigSession()
		{
			ADSessionSettings sessionSettings = OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromAccountPartitionRootOrgScopeSet(base.CurrentOrganizationId.PartitionId);
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			this.partitionOrRootOrgGlobalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(flag ? this.DomainController : null, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 479, "CreateCurrentPartitionTopologyConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			if (!this.partitionOrRootOrgGlobalConfigSession.IsReadConnectionAvailable())
			{
				this.partitionOrRootOrgGlobalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 488, "CreateCurrentPartitionTopologyConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			}
			this.partitionOrRootOrgGlobalConfigSession.UseGlobalCatalog = true;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000AC10 File Offset: 0x00008E10
		private void CreateRootOrgGlobalCatalogSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			this.rootOrgGlobalCatalogSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(flag ? this.DomainController : null, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 504, "CreateRootOrgGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			if (!this.rootOrgGlobalCatalogSession.IsReadConnectionAvailable())
			{
				this.rootOrgGlobalCatalogSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 515, "CreateRootOrgGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			}
			this.rootOrgGlobalCatalogSession.UseGlobalCatalog = true;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		private void CreateReadWriteRootOrgConfigSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			this.readWriteRootOrgGlobalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(flag ? this.DomainController : null, false, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 532, "CreateReadWriteRootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			if (!this.readWriteRootOrgGlobalConfigSession.IsReadConnectionAvailable())
			{
				this.readWriteRootOrgGlobalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 541, "CreateReadWriteRootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			}
			this.readWriteRootOrgGlobalConfigSession.UseGlobalCatalog = true;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AD84 File Offset: 0x00008F84
		private void CreateRootOrgConfigSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.DomainController, sessionSettings);
			this.rootOrgGlobalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(flag ? this.DomainController : null, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(this.DomainController) ? null : (flag ? this.NetCredential : null), sessionSettings, 558, "CreateRootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			if (!this.rootOrgGlobalConfigSession.IsReadConnectionAvailable())
			{
				this.rootOrgGlobalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 567, "CreateRootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
			}
			this.rootOrgGlobalConfigSession.UseGlobalCatalog = true;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AE37 File Offset: 0x00009037
		protected virtual bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return false;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AE3C File Offset: 0x0000903C
		protected virtual void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			if (this.ShouldSupportPreResolveOrgIdBasedOnIdentity() && base.CurrentOrganizationId != null && base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				ADIdParameter adidParameter = identity as ADIdParameter;
				if (adidParameter != null)
				{
					OrganizationId organizationId = adidParameter.ResolveOrganizationIdBasedOnIdentity(base.ExecutingUserOrganizationId);
					if (organizationId != null && !organizationId.Equals(base.CurrentOrganizationId))
					{
						this.SetCurrentOrganizationWithScopeSet(organizationId);
					}
				}
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AEA6 File Offset: 0x000090A6
		protected void SetCurrentOrganizationWithScopeSet(OrganizationId orgId)
		{
			if (orgId != null && !orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				base.CurrentTaskContext.ScopeSet = ScopeSet.ResolveUnderScope(orgId, base.CurrentTaskContext.ScopeSet);
			}
			base.CurrentOrganizationId = orgId;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AEE1 File Offset: 0x000090E1
		private bool ShouldChangeScope(IDirectorySession session)
		{
			return base.CurrentOrganizationId != null && session.SessionSettings.CurrentOrganizationId != null && !base.CurrentOrganizationId.Equals(session.SessionSettings.CurrentOrganizationId);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000AF1F File Offset: 0x0000911F
		protected IConfigDataProvider DataSession
		{
			get
			{
				return this.dataSession;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000AF27 File Offset: 0x00009127
		protected virtual ObjectId RootId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AF2A File Offset: 0x0000912A
		protected OptionalIdentityData OptionalIdentityData
		{
			get
			{
				return this.optionalData;
			}
		}

		// Token: 0x060002BC RID: 700
		protected abstract IConfigDataProvider CreateSession();

		// Token: 0x060002BD RID: 701 RVA: 0x0000AF34 File Offset: 0x00009134
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.dataSession = this.CreateSession();
			if (this.dataSession != null)
			{
				IDirectorySession directorySession = this.dataSession as IDirectorySession;
				if (directorySession != null && directorySession.SessionSettings != null)
				{
					directorySession.SessionSettings.ExecutingUserIdentityName = base.ExecutingUserIdentityName;
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AF84 File Offset: 0x00009184
		protected void Validate(TDataObject dataObject)
		{
			ValidationError[] array = dataObject.Validate();
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.WriteError(new DataValidationException(array[i]), (ErrorCategory)1003, dataObject.Identity, array.Length - 1 == i);
				}
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AFDC File Offset: 0x000091DC
		protected IConfigurable GetDataObject(IIdentityParameter id)
		{
			return this.GetDataObject<TDataObject>(id, this.DataSession, this.RootId, null, null, null);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B00F File Offset: 0x0000920F
		protected IEnumerable<TDataObject> GetDataObjects(IIdentityParameter id, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetDataObjects<TDataObject>(id, this.DataSession, this.RootId, optionalData, out notFoundReason);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B028 File Offset: 0x00009228
		protected IEnumerable<TDataObject> GetDataObjects(IIdentityParameter id)
		{
			LocalizedString? localizedString;
			return this.GetDataObjects<TDataObject>(id, this.DataSession, this.RootId, null, out localizedString);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B04B File Offset: 0x0000924B
		protected IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new()
		{
			return this.GetDataObject<TObject>(id, session, rootID, null, notFoundError, multipleFoundError, (ExchangeErrorCategory)0);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B05C File Offset: 0x0000925C
		protected IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new()
		{
			return this.GetDataObject<TObject>(id, session, rootID, optionalData, notFoundError, multipleFoundError, (ExchangeErrorCategory)0);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B070 File Offset: 0x00009270
		protected IEnumerable<TObject> GetDataObjects<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID) where TObject : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetDataObjects<TObject>(id, session, rootID, null, out localizedString);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B08C File Offset: 0x0000928C
		protected IEnumerable<TObject> GetDataObjects<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where TObject : IConfigurable, new()
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			notFoundReason = null;
			base.WriteVerbose(TaskVerboseStringHelper.GetFindByIdParameterVerboseString(id, session ?? this.DataSession, typeof(TObject), rootID));
			IEnumerable<TObject> objects;
			try
			{
				objects = id.GetObjects<TObject>(rootID, session ?? this.DataSession, optionalData, out notFoundReason);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(session ?? this.DataSession));
			}
			return objects;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B114 File Offset: 0x00009314
		internal void VerifyIsWithinScopes(IDirectorySession session, ADObject adObject, bool isModification, DataAccessTask<TDataObject>.ADObjectOutOfScopeString adObjectOutOfScopeString)
		{
			ADScopeException ex;
			if (!session.TryVerifyIsWithinScopes(adObject, isModification, out ex))
			{
				base.WriteError(new InvalidOperationException(adObjectOutOfScopeString(adObject.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.InvalidOperation, adObject.Identity);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B168 File Offset: 0x00009368
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			base.TranslateException(ref e, out category);
			ErrorCategory exceptionErrorCategory = (ErrorCategory)RecipientTaskHelper.GetExceptionErrorCategory(e);
			if (exceptionErrorCategory != ErrorCategory.NotSpecified)
			{
				category = exceptionErrorCategory;
				return;
			}
			category = (ErrorCategory)DataAccessHelper.ResolveExceptionErrorCategory(e);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B195 File Offset: 0x00009395
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B1A8 File Offset: 0x000093A8
		protected LocalizedString GetErrorMessageObjectNotFound(string id, string type, string source)
		{
			if (source == null)
			{
				if (id != null)
				{
					return Strings.ErrorManagementObjectNotFound(id);
				}
				return Strings.ErrorManagementObjectNotFoundByType(type);
			}
			else
			{
				if (id != null)
				{
					return Strings.ErrorManagementObjectNotFoundWithSource(id, source);
				}
				return Strings.ErrorManagementObjectNotFoundWithSourceByType(type, source);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B1D0 File Offset: 0x000093D0
		protected virtual IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return dataObject;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B1D4 File Offset: 0x000093D4
		protected void VerifyValues<TType>(TType[] allowedTypes, TType value)
		{
			this.VerifyValues<TType>(allowedTypes, new TType[]
			{
				value
			});
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B1F8 File Offset: 0x000093F8
		protected void VerifyValues<TType>(TType[] allowedTypes, TType[] values)
		{
			if (allowedTypes == null)
			{
				throw new ArgumentNullException("allowedTypes");
			}
			if (values == null)
			{
				return;
			}
			List<TType> list = new List<TType>();
			foreach (TType ttype in values)
			{
				if (-1 == Array.IndexOf<TType>(allowedTypes, ttype))
				{
					list.Add(ttype);
				}
			}
			if (list.Count > 0)
			{
				throw new TaskException(Strings.ErrorUnsupportedValues(this.FormatMultiValuedProperty(list), this.FormatMultiValuedProperty(allowedTypes)));
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B268 File Offset: 0x00009468
		protected void UnderscopeDataSession(OrganizationId orgId)
		{
			IDirectorySession session = (IDirectorySession)this.dataSession;
			this.dataSession = (IConfigDataProvider)TaskHelper.UnderscopeSessionToOrganization(session, orgId, this.RehomeDataSession);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B29C File Offset: 0x0000949C
		internal void RebindDataSessionToDataObjectPartitionRidMasterIncludeRetiredTenants(PartitionId partitionId)
		{
			string ridMasterName = ForestTenantRelocationsCache.GetRidMasterName(partitionId);
			if (this.DomainController != null)
			{
				string value = this.DomainController.ToString().Split(new char[]
				{
					'.'
				})[0] + ".";
				if (!ridMasterName.StartsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					ForestTenantRelocationsCache.Reset();
					base.WriteError(new InvalidOperationException(Strings.ErrorMustWriteToRidMaster(ridMasterName)), ErrorCategory.InvalidOperation, ridMasterName);
				}
			}
			ADSessionSettings adsessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			adsessionSettings.RetiredTenantModificationAllowed = true;
			this.dataSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ridMasterName, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 1125, "RebindDataSessionToDataObjectPartitionRidMasterIncludeRetiredTenants", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\DataAccessTask.cs");
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B341 File Offset: 0x00009541
		protected virtual bool RehomeDataSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B344 File Offset: 0x00009544
		protected string FormatMultiValuedProperty(IList mvp)
		{
			return MultiValuedPropertyBase.FormatMultiValuedProperty(mvp);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B34C File Offset: 0x0000954C
		protected IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) where TObject : IConfigurable, new()
		{
			return this.GetDataObject<TObject>(id, session, rootID, null, notFoundError, multipleFoundError, errorCategory);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B360 File Offset: 0x00009560
		protected MultiValuedProperty<TResult> ResolveIdParameterCollection<TIdParameter, TObject, TResult>(IEnumerable<TIdParameter> idParameters, IConfigDataProvider session, ObjectId rootId, OptionalIdentityData optionalData) where TIdParameter : IIdentityParameter where TObject : IConfigurable, new()
		{
			return this.ResolveIdParameterCollection<TIdParameter, TObject, TResult>(idParameters, session, rootId, optionalData, (ExchangeErrorCategory)0, null, null, null, null);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B380 File Offset: 0x00009580
		protected MultiValuedProperty<TResult> ResolveIdParameterCollection<TIdParameter, TObject, TResult>(IEnumerable<TIdParameter> idParameters, IConfigDataProvider session, ObjectId rootId, OptionalIdentityData optionalData, ExchangeErrorCategory errorCategory, Func<IIdentityParameter, LocalizedString> parameterToNotFoundError, Func<IIdentityParameter, LocalizedString> parameterToMultipleFoundError, Func<IConfigurable, TResult> convertToResult, Func<IConfigurable, IConfigurable> validateObject) where TIdParameter : IIdentityParameter where TObject : IConfigurable, new()
		{
			MultiValuedProperty<TIdParameter> multiValuedProperty = idParameters as MultiValuedProperty<TIdParameter>;
			MultiValuedProperty<TResult> result;
			if (multiValuedProperty != null && multiValuedProperty.IsChangesOnlyCopy)
			{
				Hashtable hashtable = new Hashtable();
				if (multiValuedProperty.Added.Length > 0)
				{
					IEnumerable<TResult> value = this.ResolveIdList<TObject, TResult>(multiValuedProperty.Added, session, rootId, optionalData, errorCategory, parameterToNotFoundError, parameterToMultipleFoundError, convertToResult, validateObject);
					hashtable.Add("Add", value);
				}
				if (multiValuedProperty.Removed.Length > 0)
				{
					IEnumerable<TResult> value2 = this.ResolveIdList<TObject, TResult>(multiValuedProperty.Removed, session, rootId, optionalData, errorCategory, parameterToNotFoundError, parameterToMultipleFoundError, convertToResult, null);
					hashtable.Add("Remove", value2);
				}
				result = new MultiValuedProperty<TResult>(hashtable);
			}
			else
			{
				IEnumerable<TResult> value3 = this.ResolveIdList<TObject, TResult>(idParameters, session, rootId, optionalData, errorCategory, parameterToNotFoundError, parameterToMultipleFoundError, convertToResult, validateObject);
				result = new MultiValuedProperty<TResult>(value3);
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B448 File Offset: 0x00009648
		private IEnumerable<TResult> ResolveIdList<TObject, TResult>(IEnumerable idParameters, IConfigDataProvider session, ObjectId rootId, OptionalIdentityData optionalData, ExchangeErrorCategory errorCategory, Func<IIdentityParameter, LocalizedString> parameterToNotFoundError, Func<IIdentityParameter, LocalizedString> parameterToMultipleFoundError, Func<IConfigurable, TResult> convertToResult, Func<IConfigurable, IConfigurable> validateObject) where TObject : IConfigurable, new()
		{
			Func<IConfigurable, TResult> func = null;
			Func<IConfigurable, IConfigurable> func2 = null;
			Dictionary<TResult, IIdentityParameter> dictionary = new Dictionary<TResult, IIdentityParameter>();
			if (idParameters != null)
			{
				if (convertToResult == null)
				{
					if (func == null)
					{
						func = ((IConfigurable obj) => (TResult)((object)obj.Identity));
					}
					convertToResult = func;
				}
				if (validateObject == null)
				{
					if (func2 == null)
					{
						func2 = ((IConfigurable obj) => obj);
					}
					validateObject = func2;
				}
				foreach (object obj2 in idParameters)
				{
					IIdentityParameter identityParameter = (IIdentityParameter)obj2;
					LocalizedString? notFoundError = (parameterToNotFoundError == null) ? null : new LocalizedString?(parameterToNotFoundError(identityParameter));
					LocalizedString? multipleFoundError = (parameterToMultipleFoundError == null) ? null : new LocalizedString?(parameterToMultipleFoundError(identityParameter));
					IConfigurable arg = this.GetDataObject<TObject>(identityParameter, session, rootId, notFoundError, multipleFoundError);
					arg = validateObject(arg);
					TResult tresult = convertToResult(arg);
					if (dictionary.ContainsKey(tresult))
					{
						throw new ManagementObjectDuplicateException(Strings.ErrorDuplicateManagementObjectFound(dictionary[tresult], identityParameter, tresult));
					}
					dictionary.Add(tresult, identityParameter);
				}
			}
			return dictionary.Keys;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000B580 File Offset: 0x00009780
		protected IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) where TObject : IConfigurable, new()
		{
			IConfigurable result = null;
			LocalizedString? localizedString;
			IEnumerable<TObject> dataObjects = this.GetDataObjects<TObject>(id, session, rootID, optionalData, out localizedString);
			Exception ex = null;
			using (IEnumerator<TObject> enumerator = dataObjects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					result = enumerator.Current;
					if (enumerator.MoveNext())
					{
						ex = new ManagementObjectAmbiguousException(multipleFoundError ?? Strings.ErrorManagementObjectAmbiguous(id.ToString()));
					}
				}
				else if (notFoundError != null)
				{
					LocalizedString message;
					if (localizedString != null)
					{
						LocalizedString? localizedString2 = notFoundError;
						string notFound = (localizedString2 != null) ? localizedString2.GetValueOrDefault() : null;
						LocalizedString? localizedString3 = localizedString;
						message = Strings.ErrorNotFoundWithReason(notFound, (localizedString3 != null) ? localizedString3.GetValueOrDefault() : null);
					}
					else
					{
						message = notFoundError.Value;
					}
					ex = new ManagementObjectNotFoundException(message);
				}
				else
				{
					ex = new ManagementObjectNotFoundException(localizedString ?? this.GetErrorMessageObjectNotFound(id.ToString(), typeof(TObject).ToString(), (this.DataSession != null) ? this.DataSession.Source : null));
				}
			}
			if (ex != null)
			{
				if (errorCategory != (ExchangeErrorCategory)0)
				{
					RecipientTaskHelper.SetExceptionErrorCategory(ex, errorCategory);
				}
				throw ex;
			}
			return result;
		}

		// Token: 0x040000A9 RID: 169
		private IConfigDataProvider dataSession;

		// Token: 0x040000AA RID: 170
		private OptionalIdentityData optionalData;

		// Token: 0x040000AB RID: 171
		private NetworkCredential netCredential;

		// Token: 0x040000AC RID: 172
		private ADSessionSettings orgWideSessionSettings;

		// Token: 0x040000AD RID: 173
		private ADObjectId rootOrgId;

		// Token: 0x040000AE RID: 174
		private IConfigurationSession configurationSession;

		// Token: 0x040000AF RID: 175
		private IRecipientSession tenantGlobalCatalogSession;

		// Token: 0x040000B0 RID: 176
		private ITopologyConfigurationSession globalConfigSession;

		// Token: 0x040000B1 RID: 177
		private ITopologyConfigurationSession partitionOrRootOrgGlobalConfigSession;

		// Token: 0x040000B2 RID: 178
		private ITopologyConfigurationSession rootOrgGlobalConfigSession;

		// Token: 0x040000B3 RID: 179
		private ITopologyConfigurationSession readWriteRootOrgGlobalConfigSession;

		// Token: 0x040000B4 RID: 180
		private IRecipientSession partitionOrRootOrgGlobalCatalogSession;

		// Token: 0x040000B5 RID: 181
		private IRootOrganizationRecipientSession rootOrgGlobalCatalogSession;

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x060002D9 RID: 729
		internal delegate LocalizedString ADObjectOutOfScopeString(string objectName, string reason);
	}
}
