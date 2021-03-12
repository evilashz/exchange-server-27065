using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000C6 RID: 198
	[Cmdlet("Remove", "SyncMailbox", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncMailbox : RemoveMailboxBase<MailboxIdParameter>
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00035C2D File Offset: 0x00033E2D
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00035C35 File Offset: 0x00033E35
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter DisableWindowsLiveID { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00035C3E File Offset: 0x00033E3E
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00035C46 File Offset: 0x00033E46
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00035C4F File Offset: 0x00033E4F
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00035C5C File Offset: 0x00033E5C
		protected override void InternalValidate()
		{
			this.latencyContext = ProvisioningPerformanceHelper.StartLatencyDetection(this);
			base.InternalValidate();
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00035C70 File Offset: 0x00033E70
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			finally
			{
				ProvisioningPerformanceHelper.StopLatencyDetection(this.latencyContext);
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00035CA4 File Offset: 0x00033EA4
		protected override bool ShouldSoftDeleteObject()
		{
			ADRecipient dataObject = base.DataObject;
			return dataObject != null && !(dataObject.OrganizationId == null) && dataObject.OrganizationId.ConfigurationUnit != null && SoftDeletedTaskHelper.IsSoftDeleteSupportedRecipientTypeDetail(dataObject.RecipientTypeDetails) && !base.Disconnect && !base.Permanent && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SoftDeleteObject.Enabled;
		}

		// Token: 0x040002B8 RID: 696
		private LatencyDetectionContext latencyContext;
	}
}
