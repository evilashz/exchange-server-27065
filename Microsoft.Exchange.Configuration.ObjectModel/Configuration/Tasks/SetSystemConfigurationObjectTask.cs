using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000091 RID: 145
	public abstract class SetSystemConfigurationObjectTask<TIdentity, TPublicObject, TDataObject> : SetADTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : ADObject, new()
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001609B File Offset: 0x0001429B
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000160A3 File Offset: 0x000142A3
		internal LazilyInitialized<SharedTenantConfigurationState> CurrentOrgState { get; set; }

		// Token: 0x060005CA RID: 1482 RVA: 0x000160B9 File Offset: 0x000142B9
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(() => SharedConfiguration.GetSharedConfigurationState(base.CurrentOrganizationId));
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000160D8 File Offset: 0x000142D8
		protected override void InternalValidate()
		{
			SharedTenantConfigurationMode sharedTenantConfigurationMode = this.SharedTenantConfigurationMode;
			LazilyInitialized<SharedTenantConfigurationState> currentOrgState = this.CurrentOrgState;
			string targetObject;
			if (this.Identity == null)
			{
				targetObject = null;
			}
			else
			{
				TIdentity identity = this.Identity;
				targetObject = identity.ToString();
			}
			SharedConfigurationTaskHelper.Validate(this, sharedTenantConfigurationMode, currentOrgState, targetObject);
			base.InternalValidate();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001614C File Offset: 0x0001434C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			OrganizationId currentOrganizationId = base.CurrentOrganizationId;
			TDataObject dataObject = this.DataObject;
			if (!currentOrganizationId.Equals(dataObject.OrganizationId))
			{
				this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(delegate()
				{
					TDataObject dataObject8 = this.DataObject;
					return SharedConfiguration.GetSharedConfigurationState(dataObject8.OrganizationId);
				});
			}
			if (SharedConfigurationTaskHelper.ShouldPrompt(this, this.SharedTenantConfigurationMode, this.CurrentOrgState) && !base.InternalForce)
			{
				TDataObject dataObject2 = this.DataObject;
				if (!base.ShouldContinue(Strings.ConfirmSharedConfiguration(dataObject2.OrganizationId.OrganizationalUnit.Name)))
				{
					TaskLogger.LogExit();
					return;
				}
			}
			TDataObject dataObject3 = this.DataObject;
			if (dataObject3.IsChanged(ADObjectSchema.Id))
			{
				IDirectorySession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.OrgWideSessionSettings, ConfigScopes.TenantSubTree, 702, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = ((IDirectorySession)base.DataSession).UseConfigNC;
				TDataObject dataObject4 = this.DataObject;
				ADObjectId parent = dataObject4.Id.Parent;
				ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(parent, new PropertyDefinition[]
				{
					ADObjectSchema.ExchangeVersion,
					ADObjectSchema.ObjectClass
				});
				ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)adrawEntry[ADObjectSchema.ExchangeVersion];
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)adrawEntry[ADObjectSchema.ObjectClass];
				TDataObject dataObject5 = this.DataObject;
				if (dataObject5.ExchangeVersion.IsOlderThan(exchangeObjectVersion) && !multiValuedProperty.Contains(Organization.MostDerivedClass))
				{
					TDataObject dataObject6 = this.DataObject;
					string name = dataObject6.Name;
					TDataObject dataObject7 = this.DataObject;
					base.WriteError(new TaskException(Strings.ErrorParentHasNewerVersion(name, dataObject7.ExchangeVersion.ToString(), exchangeObjectVersion.ToString())), (ErrorCategory)1004, null);
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001633C File Offset: 0x0001453C
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 750, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001636C File Offset: 0x0001456C
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.Validate(this, this.SharedTenantConfigurationMode, this.CurrentOrgState, null);
			ADObject adobject = (ADObject)base.ResolveDataObject();
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, adobject))
			{
				base.UnderscopeDataSession(adobject.OrganizationId);
			}
			return adobject;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000163B8 File Offset: 0x000145B8
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}
	}
}
