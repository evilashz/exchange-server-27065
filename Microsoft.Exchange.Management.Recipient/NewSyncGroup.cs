using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000DA RID: 218
	[Cmdlet("New", "SyncGroup", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewSyncGroup : NewGeneralRecipientObjectTask<ADGroup>
	{
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0003CCC5 File Offset: 0x0003AEC5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSyncGroup(base.Name.ToString());
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0003CCD7 File Offset: 0x0003AED7
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
		[Parameter]
		public GroupType Type
		{
			get
			{
				return (GroupType)(base.Fields[ADGroupSchema.GroupType] ?? GroupType.Distribution);
			}
			set
			{
				base.Fields[ADGroupSchema.GroupType] = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0003CD10 File Offset: 0x0003AF10
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x0003CD1D File Offset: 0x0003AF1D
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return this.DataObject.OnPremisesObjectId;
			}
			set
			{
				this.DataObject.OnPremisesObjectId = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0003CD2B File Offset: 0x0003AF2B
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x0003CD38 File Offset: 0x0003AF38
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return this.DataObject.IsDirSynced;
			}
			set
			{
				this.DataObject.IsDirSynced = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0003CD46 File Offset: 0x0003AF46
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x0003CD53 File Offset: 0x0003AF53
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return this.DataObject.DirSyncAuthorityMetadata;
			}
			set
			{
				this.DataObject.DirSyncAuthorityMetadata = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0003CD61 File Offset: 0x0003AF61
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0003CD78 File Offset: 0x0003AF78
		[Parameter(Mandatory = false)]
		public string ValidationOrganization
		{
			get
			{
				return (string)base.Fields["ValidationOrganization"];
			}
			set
			{
				base.Fields["ValidationOrganization"] = value;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003CD8C File Offset: 0x0003AF8C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003CDEC File Offset: 0x0003AFEC
		protected override void PrepareRecipientObject(ADGroup group)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(group);
			group.GroupType = (GroupTypeFlags)((GroupType)8 | this.Type);
			if (!group.IsModified(ADRecipientSchema.DisplayName))
			{
				group.DisplayName = group.Name;
			}
			if (!group.IsModified(ADMailboxRecipientSchema.SamAccountName))
			{
				group.SamAccountName = RecipientTaskHelper.GenerateUniqueSamAccountName(base.PartitionOrRootOrgGlobalCatalogSession, group.Id.DomainId, group.Name, false, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), true);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003CE6E File Offset: 0x0003B06E
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncGroup.FromDataObject((ADGroup)dataObject);
		}
	}
}
