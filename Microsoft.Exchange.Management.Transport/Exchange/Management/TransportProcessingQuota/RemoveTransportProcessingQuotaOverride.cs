using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000B0 RID: 176
	[Cmdlet("Remove", "TransportProcessingQuotaOverride", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveTransportProcessingQuotaOverride : TransportProcessingQuotaBaseTask
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001A50C File Offset: 0x0001870C
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0001A514 File Offset: 0x00018714
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001A51D File Offset: 0x0001871D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveTransportProcessingQuotaOverride(this.ExternalDirectoryOrganizationId);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001A52C File Offset: 0x0001872C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			TenantThrottleInfo throttleState = base.Session.GetThrottleState(this.ExternalDirectoryOrganizationId);
			if (throttleState != null && throttleState.ThrottleState != TenantThrottleState.Auto)
			{
				base.Session.SetThrottleState(new TenantThrottleInfo
				{
					TenantId = this.ExternalDirectoryOrganizationId,
					ThrottleState = TenantThrottleState.Auto
				});
				return;
			}
			this.WriteWarning(Strings.ErrorObjectNotFound("TransportProcessingQuotaOverride"));
		}
	}
}
