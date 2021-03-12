using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000046 RID: 70
	[Cmdlet("Set", "UpgradeWorkItem", SupportsShouldProcess = true)]
	public class SetUpgradeWorkItem : SymphonyTaskBase
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000F16C File Offset: 0x0000D36C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetUpgradeWorkItemConfirmationMessage(this.Identity, this.modifiedProperties);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000F192 File Offset: 0x0000D392
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000F19A File Offset: 0x0000D39A
		[Parameter(Mandatory = false)]
		public WorkItemStatus Status { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000F1A3 File Offset: 0x0000D3A3
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000F1AB File Offset: 0x0000D3AB
		[Parameter(Mandatory = false)]
		public string Comment { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		[Parameter(Mandatory = false)]
		public int CompletedCount { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000F1C5 File Offset: 0x0000D3C5
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000F1CD File Offset: 0x0000D3CD
		[Parameter(Mandatory = false)]
		public string HandlerState { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000F1D6 File Offset: 0x0000D3D6
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000F1DE File Offset: 0x0000D3DE
		[Parameter(Mandatory = false)]
		public string StatusDetailsUri { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000F1E7 File Offset: 0x0000D3E7
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000F1EF File Offset: 0x0000D3EF
		[Parameter(Mandatory = false)]
		public int TotalCount { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000F200 File Offset: 0x0000D400
		[Parameter(Mandatory = false)]
		public Guid Tenant { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000F209 File Offset: 0x0000D409
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000F211 File Offset: 0x0000D411
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Identity { get; set; }

		// Token: 0x0600036C RID: 876 RVA: 0x0000F21C File Offset: 0x0000D41C
		protected override void InternalValidate()
		{
			if (base.UserSpecifiedParameters.Contains("Tenant"))
			{
				if (this.Tenant == Guid.Empty)
				{
					throw new InvalidTenantGuidException(this.Tenant.ToString());
				}
				this.retrievedWorkItem = base.GetWorkitemByIdAndTenantId(this.Identity, this.Tenant);
			}
			else
			{
				this.retrievedWorkItem = base.GetWorkItemById(this.Identity);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (base.UserSpecifiedParameters.Contains("Status"))
			{
				this.retrievedWorkItem.WorkItemStatus.Status = this.Status;
				stringBuilder.AppendFormat("Status: {0} ", this.Status.ToString());
			}
			if (base.UserSpecifiedParameters.Contains("HandlerState"))
			{
				this.retrievedWorkItem.WorkItemStatus.HandlerState = this.HandlerState;
				stringBuilder.AppendFormat("HandlerState: {0} ", this.HandlerState);
			}
			if (base.UserSpecifiedParameters.Contains("Comment"))
			{
				this.retrievedWorkItem.WorkItemStatus.Comment = this.Comment;
				stringBuilder.AppendFormat("Comment: {0} ", this.Comment);
			}
			if (base.UserSpecifiedParameters.Contains("CompletedCount"))
			{
				this.retrievedWorkItem.WorkItemStatus.CompletedCount = this.CompletedCount;
				stringBuilder.AppendFormat("CompletedCount: {0} ", this.CompletedCount);
			}
			if (base.UserSpecifiedParameters.Contains("TotalCount"))
			{
				this.retrievedWorkItem.WorkItemStatus.TotalCount = this.TotalCount;
				stringBuilder.AppendFormat("TotalCount: {0} ", this.TotalCount);
			}
			if (base.UserSpecifiedParameters.Contains("StatusDetailsUri"))
			{
				Uri statusDetails;
				if (!Uri.TryCreate(this.StatusDetailsUri, UriKind.Absolute, out statusDetails))
				{
					throw new InvalidStatusDetailException(this.StatusDetailsUri);
				}
				this.retrievedWorkItem.WorkItemStatus.StatusDetails = statusDetails;
				stringBuilder.AppendFormat("StatusDetails: {0} ", this.StatusDetailsUri);
			}
			this.modifiedProperties = stringBuilder.ToString();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000F468 File Offset: 0x0000D668
		protected override void InternalProcessRecord()
		{
			using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(base.WorkloadUri, base.Certificate))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateWorkItem(this.retrievedWorkItem.WorkItemId, this.retrievedWorkItem.WorkItemStatus);
				}, base.WorkloadUri.ToString());
			}
		}

		// Token: 0x04000141 RID: 321
		private WorkItemInfo retrievedWorkItem;

		// Token: 0x04000142 RID: 322
		private string modifiedProperties = string.Empty;
	}
}
