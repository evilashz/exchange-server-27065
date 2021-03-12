using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000081 RID: 129
	[Cmdlet("Set", "MailContact", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailContact : SetMailContactBase<MailContact>
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00026E95 File Offset: 0x00025095
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00026EBB File Offset: 0x000250BB
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00026ED3 File Offset: 0x000250D3
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00026EF9 File Offset: 0x000250F9
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

		// Token: 0x06000923 RID: 2339 RVA: 0x00026F14 File Offset: 0x00025114
		protected override void InternalValidate()
		{
			base.InternalValidate();
			ADContact dataObject = this.DataObject;
			if (dataObject.RecipientTypeDetails == RecipientTypeDetails.MailForestContact && this.IsObjectStateChanged())
			{
				base.WriteError(new TaskInvalidOperationException(Strings.SetMailForestContactNotAllowed(dataObject.Name)), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (this.GenerateExternalDirectoryObjectId && (RecipientTaskHelper.GetAcceptedRecipientTypes() & this.DataObject.RecipientTypeDetails) == RecipientTypeDetails.None)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotGenerateExternalDirectoryObjectIdOnInternalRecipientType(this.Identity.ToString(), this.DataObject.RecipientTypeDetails.ToString())), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00026FC8 File Offset: 0x000251C8
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
	}
}
