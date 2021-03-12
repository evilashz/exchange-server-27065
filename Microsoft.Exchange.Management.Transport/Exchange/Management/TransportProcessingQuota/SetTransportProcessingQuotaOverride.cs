using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000B2 RID: 178
	[Cmdlet("Set", "TransportProcessingQuotaOverride", SupportsShouldProcess = true)]
	public sealed class SetTransportProcessingQuotaOverride : TransportProcessingQuotaBaseTask
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001A7F8 File Offset: 0x000189F8
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0001A800 File Offset: 0x00018A00
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001A809 File Offset: 0x00018A09
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001A811 File Offset: 0x00018A11
		[Parameter(Mandatory = true)]
		public bool Throttle { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001A81A File Offset: 0x00018A1A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTransportProcessingQuotaOverride(this.ExternalDirectoryOrganizationId, this.Throttle);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001A830 File Offset: 0x00018A30
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			TenantThrottleState tenantThrottleState = this.Throttle ? TenantThrottleState.Throttled : TenantThrottleState.Unthrottled;
			TenantThrottleInfo throttleState = base.Session.GetThrottleState(this.ExternalDirectoryOrganizationId);
			if (throttleState == null || throttleState.ThrottleState != tenantThrottleState)
			{
				base.Session.SetThrottleState(new TenantThrottleInfo
				{
					TenantId = this.ExternalDirectoryOrganizationId,
					ThrottleState = tenantThrottleState
				});
			}
		}
	}
}
