using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000087 RID: 135
	public abstract class RemoveSystemConfigurationObjectTask<TIdentity, TDataObject> : RemoveADTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000151A4 File Offset: 0x000133A4
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x000151AC File Offset: 0x000133AC
		internal LazilyInitialized<SharedTenantConfigurationState> CurrentOrgState { get; set; }

		// Token: 0x06000582 RID: 1410 RVA: 0x000151C2 File Offset: 0x000133C2
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(() => SharedConfiguration.GetSharedConfigurationState(base.CurrentOrganizationId));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000151E4 File Offset: 0x000133E4
		protected override void InternalValidate()
		{
			SharedTenantConfigurationMode sharedTenantConfigurationMode = this.SharedTenantConfigurationMode;
			LazilyInitialized<SharedTenantConfigurationState> currentOrgState = this.CurrentOrgState;
			TIdentity identity = this.Identity;
			SharedConfigurationTaskHelper.Validate(this, sharedTenantConfigurationMode, currentOrgState, identity.ToString());
			base.InternalValidate();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00015248 File Offset: 0x00013448
		protected override void InternalProcessRecord()
		{
			LazilyInitialized<SharedTenantConfigurationState> currentOrgState = this.CurrentOrgState;
			OrganizationId currentOrganizationId = base.CurrentOrganizationId;
			TDataObject dataObject = base.DataObject;
			if (!currentOrganizationId.Equals(dataObject.OrganizationId))
			{
				currentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(delegate()
				{
					TDataObject dataObject3 = base.DataObject;
					return SharedConfiguration.GetSharedConfigurationState(dataObject3.OrganizationId);
				});
			}
			if (SharedConfigurationTaskHelper.ShouldPrompt(this, this.SharedTenantConfigurationMode, currentOrgState) && !base.InternalForce)
			{
				TDataObject dataObject2 = base.DataObject;
				if (!base.ShouldContinue(Strings.ConfirmSharedConfiguration(dataObject2.OrganizationId.OrganizationalUnit.Name)))
				{
					TaskLogger.LogExit();
					return;
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000152E9 File Offset: 0x000134E9
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 503, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\RemoveAdObjectTask.cs");
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00015318 File Offset: 0x00013518
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)base.ResolveDataObject();
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, adobject))
			{
				base.UnderscopeDataSession(adobject.OrganizationId);
				base.CurrentOrganizationId = adobject.OrganizationId;
			}
			return adobject;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001535D File Offset: 0x0001355D
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}
	}
}
