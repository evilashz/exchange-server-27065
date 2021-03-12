using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200002F RID: 47
	[Cmdlet("Set", "DistributionGroup", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDistributionGroup : SetDistributionGroupBase<DistributionGroup>
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000BA28 File Offset: 0x00009C28
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000BA4E File Offset: 0x00009C4E
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000BA66 File Offset: 0x00009C66
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000BA8C File Offset: 0x00009C8C
		[Parameter(Mandatory = false)]
		public SwitchParameter GenerateExternalDirectoryObjectId
		{
			get
			{
				return (SwitchParameter)(base.Fields["GenerateExternalDirectoryObjectId"] ?? false);
			}
			set
			{
				base.Fields["GenerateExternalDirectoryObjectId"] = value;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		protected override IConfigurable PrepareDataObject()
		{
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			if (adgroup != null && (adgroup.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || adgroup.RecipientTypeDetails == RecipientTypeDetails.RemoteGroupMailbox))
			{
				base.WriteError(new RecipientTaskException(Strings.NotAValidDistributionGroup), ExchangeErrorCategory.Client, this.Identity.ToString());
			}
			return adgroup;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000BB04 File Offset: 0x00009D04
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.GenerateExternalDirectoryObjectId && (RecipientTaskHelper.GetAcceptedRecipientTypes() & this.DataObject.RecipientTypeDetails) == RecipientTypeDetails.None)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotGenerateExternalDirectoryObjectIdOnInternalRecipientType(this.Identity.ToString(), this.DataObject.RecipientTypeDetails.ToString())), ExchangeErrorCategory.Client, this.Identity);
			}
			if (base.Fields.IsModified(DistributionGroupSchema.ManagedBy) && (this.DataObject.ManagedBy == null || this.DataObject.ManagedBy.Count == 0))
			{
				base.WriteError(new RecipientTaskException(Strings.AutoGroupManagedByCannotBeEmpty), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		protected override void InternalProcessRecord()
		{
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ContinueUpgradeObjectVersion(this.DataObject.Name)))
			{
				if (this.GenerateExternalDirectoryObjectId && string.IsNullOrEmpty(this.DataObject.ExternalDirectoryObjectId))
				{
					this.DataObject.ExternalDirectoryObjectId = Guid.NewGuid().ToString();
				}
				base.InternalProcessRecord();
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000BC45 File Offset: 0x00009E45
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DistributionGroup.FromDataObject((ADGroup)dataObject);
		}
	}
}
