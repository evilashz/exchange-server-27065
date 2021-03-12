using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Utility;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x0200075B RID: 1883
	[Cmdlet("Start", "ComplianceSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class StartComplianceSearch : StartComplianceJob<ComplianceSearch>
	{
		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x00113AD2 File Offset: 0x00111CD2
		// (set) Token: 0x0600431E RID: 17182 RVA: 0x00113AF3 File Offset: 0x00111CF3
		[Parameter(Mandatory = false)]
		public ComplianceSearch.ComplianceSearchType Action
		{
			get
			{
				return (ComplianceSearch.ComplianceSearchType)(base.Fields["Action"] ?? ComplianceSearch.ComplianceSearchType.UnknownType);
			}
			set
			{
				base.Fields["Action"] = value;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x00113B0B File Offset: 0x00111D0B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartComplianceSearch(this.Identity.ToString());
			}
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x00113B20 File Offset: 0x00111D20
		protected override IConfigurable PrepareDataObject()
		{
			ComplianceSearch complianceSearch = (ComplianceSearch)base.PrepareDataObject();
			if (this.Action != ComplianceSearch.ComplianceSearchType.UnknownType)
			{
				complianceSearch.SearchType = this.Action;
			}
			return complianceSearch;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x00113B50 File Offset: 0x00111D50
		protected override ComplianceMessage CreateStartJobMessage()
		{
			ComplianceMessage complianceMessage = base.CreateStartJobMessage();
			complianceMessage.WorkDefinitionType = WorkDefinitionType.EDiscovery;
			JobPayload jobPayload = new JobPayload();
			jobPayload.JobId = this.DataObject.JobRunId.ToString();
			jobPayload.Target = complianceMessage.MessageTarget;
			jobPayload.PayloadId = string.Empty;
			jobPayload.Children.Add(PayloadHelper.GetPayloadReference(this.DataObject.JobRunId, -1));
			jobPayload.Payload = this.DataObject.GetExchangeWorkDefinition();
			complianceMessage.Payload = ComplianceSerializer.Serialize<JobPayload>(JobPayload.Description, jobPayload);
			return complianceMessage;
		}

		// Token: 0x040029EB RID: 10731
		private const string ParameterAction = "Action";
	}
}
