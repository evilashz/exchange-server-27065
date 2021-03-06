using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002CB RID: 715
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MessageTrackingReports : DataSourceService, IMessageTrackingReport, IGetListService<RecipientTrackingEventsFilter, RecipientStatusRow>, IGetObjectService<MessageTrackingReportRow>
	{
		// Token: 0x06002C65 RID: 11365 RVA: 0x0008925C File Offset: 0x0008745C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MessageTrackingReport?ResultSize&Identity&Status&Recipients@R:Self")]
		public PowerShellResults<RecipientStatusRow> GetList(RecipientTrackingEventsFilter filter, SortOptions sort)
		{
			PowerShellResults<MessageTrackingReportRow> list = base.GetList<MessageTrackingReportRow, RecipientTrackingEventsFilter>("Get-MessageTrackingReport", filter, null);
			PowerShellResults<RecipientStatusRow> powerShellResults = new PowerShellResults<RecipientStatusRow>();
			powerShellResults.MergeErrors<MessageTrackingReportRow>(list);
			if (list.SucceededWithValue)
			{
				powerShellResults.Output = list.Value.RecipientStatuses;
			}
			else
			{
				powerShellResults.Output = new RecipientStatusRow[0];
			}
			if (sort != null && powerShellResults.Output.Length > 1)
			{
				Func<RecipientStatusRow[], RecipientStatusRow[]> sortFunction = sort.GetSortFunction<RecipientStatusRow>();
				powerShellResults.Output = sortFunction(powerShellResults.Output);
			}
			return powerShellResults;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000892D4 File Offset: 0x000874D4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MessageTrackingReport?ResultSize&Identity&Recipients@R:Self")]
		public PowerShellResults<MessageTrackingReportRow> GetObject(Identity identity)
		{
			RecipientMessageTrackingReportId recipientMessageTrackingReportId = RecipientMessageTrackingReportId.Parse(identity);
			GetMessageTrackingReportParameters getMessageTrackingReportParameters = new GetMessageTrackingReportParameters();
			getMessageTrackingReportParameters.Identity = recipientMessageTrackingReportId.MessageTrackingReportId;
			getMessageTrackingReportParameters.ResultSize = 30;
			getMessageTrackingReportParameters.ByPassDelegateChecking = true;
			getMessageTrackingReportParameters.DetailLevel = MessageTrackingDetailLevel.Verbose;
			if (!string.IsNullOrEmpty(recipientMessageTrackingReportId.Recipient))
			{
				getMessageTrackingReportParameters.Recipients = recipientMessageTrackingReportId.Recipient;
			}
			PSCommand psCommand = new PSCommand().AddCommand("Get-MessageTrackingReport").AddParameters(getMessageTrackingReportParameters);
			return base.Invoke<MessageTrackingReportRow>(psCommand);
		}

		// Token: 0x040021F4 RID: 8692
		internal const string GetCmdlet = "Get-MessageTrackingReport";

		// Token: 0x040021F5 RID: 8693
		internal const string ReadScope = "@R:Self";

		// Token: 0x040021F6 RID: 8694
		internal const int MaxResultSize = 30;

		// Token: 0x040021F7 RID: 8695
		private const string GetListRole = "Get-MessageTrackingReport?ResultSize&Identity&Status&Recipients@R:Self";

		// Token: 0x040021F8 RID: 8696
		private const string GetObjectRole = "Get-MessageTrackingReport?ResultSize&Identity&Recipients@R:Self";
	}
}
