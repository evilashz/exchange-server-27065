using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200066F RID: 1647
	public abstract class RoleGroupMemberTaskBase : RecipientObjectActionTask<RoleGroupIdParameter, ADGroup>
	{
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000F6404 File Offset: 0x000F4604
		private ADObjectId RootOrgUSGContainerId
		{
			get
			{
				if (this.rootOrgUSGContainerId == null)
				{
					this.rootOrgUSGContainerId = RoleGroupCommon.GetRootOrgUsgContainerId(this.ConfigurationSession, base.ServerSettings, base.PartitionOrRootOrgGlobalCatalogSession, base.CurrentOrganizationId);
				}
				return this.rootOrgUSGContainerId;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x000F6437 File Offset: 0x000F4637
		// (set) Token: 0x06003A46 RID: 14918 RVA: 0x000F644E File Offset: 0x000F464E
		[ValidateNotNull]
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public SecurityPrincipalIdParameter Member
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["Member"];
			}
			set
			{
				base.Fields["Member"] = value;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000F6461 File Offset: 0x000F4661
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x000F6487 File Offset: 0x000F4687
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedObject"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SoftDeletedObject"] = value;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x000F649F File Offset: 0x000F469F
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x000F64D3 File Offset: 0x000F46D3
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				if (DatacenterRegistry.IsForefrontForOffice())
				{
					return true;
				}
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x000F64EB File Offset: 0x000F46EB
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x06003A4C RID: 14924
		protected abstract void PerformGroupMemberAction();

		// Token: 0x06003A4D RID: 14925 RVA: 0x000F64F4 File Offset: 0x000F46F4
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = true;
			}
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000F6528 File Offset: 0x000F4728
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.OptionalIdentityData.RootOrgDomainContainerId = this.RootOrgUSGContainerId;
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!this.BypassSecurityGroupManagerCheck)
			{
				ADObjectId user;
				base.TryGetExecutingUserId(out user);
				RoleGroupCommon.ValidateExecutingUserHasGroupManagementRights(user, this.DataObject, base.ExchangeRunspaceConfig, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000F6593 File Offset: 0x000F4793
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.PerformGroupMemberAction();
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0400264D RID: 9805
		private ADObjectId rootOrgUSGContainerId;

		// Token: 0x0400264E RID: 9806
		private ADObjectId rootId;
	}
}
