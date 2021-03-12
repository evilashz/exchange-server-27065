using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000671 RID: 1649
	[Cmdlet("Get", "RoleGroupMember")]
	public sealed class GetRoleGroupMember : GetRecipientObjectTask<RoleGroupMemberIdParameter, ReducedRecipient>
	{
		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000F666C File Offset: 0x000F486C
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

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000F669F File Offset: 0x000F489F
		// (set) Token: 0x06003A56 RID: 14934 RVA: 0x000F66B6 File Offset: 0x000F48B6
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true)]
		public override RoleGroupMemberIdParameter Identity
		{
			get
			{
				return (RoleGroupMemberIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000F66C9 File Offset: 0x000F48C9
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000F66D0 File Offset: 0x000F48D0
		internal new PSCredential Credential
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000F66D3 File Offset: 0x000F48D3
		// (set) Token: 0x06003A5A RID: 14938 RVA: 0x000F66F9 File Offset: 0x000F48F9
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

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F6711 File Offset: 0x000F4911
		protected override void InternalValidate()
		{
			base.OptionalIdentityData.RootOrgDomainContainerId = this.RootOrgUSGContainerId;
			base.InternalValidate();
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000F672C File Offset: 0x000F492C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = true;
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000F6760 File Offset: 0x000F4960
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			LocalizedString? localizedString;
			IEnumerable<ReducedRecipient> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
			this.WriteResult<ReducedRecipient>(dataObjects);
			if (!base.HasErrors && localizedString != null)
			{
				base.WriteError(new ManagementObjectNotFoundException(localizedString.Value), ErrorCategory.InvalidData, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400264F RID: 9807
		private ADObjectId rootOrgUSGContainerId;
	}
}
