using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002DE RID: 734
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RecipientTrackingDetails : DataSourceService, IRecipientTrackingDetails, IGetObjectService<RecipientTrackingEventRow>
	{
		// Token: 0x06002CD0 RID: 11472 RVA: 0x00089C74 File Offset: 0x00087E74
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MessageTrackingReport?Identity&ReportTemplate&RecipientPathFilter@R:Self")]
		public PowerShellResults<RecipientTrackingEventRow> GetObject(Identity identity)
		{
			RecipientMessageTrackingReportId recipientMessageTrackingReportId = RecipientMessageTrackingReportId.Parse(identity);
			if (string.IsNullOrEmpty(recipientMessageTrackingReportId.Recipient))
			{
				throw new FaultException(new ArgumentException("Identity").Message);
			}
			GetMessageTrackingReportDetailParameters getMessageTrackingReportDetailParameters = new GetMessageTrackingReportDetailParameters();
			getMessageTrackingReportDetailParameters.Identity = recipientMessageTrackingReportId.MessageTrackingReportId;
			getMessageTrackingReportDetailParameters.RecipientPathFilter = recipientMessageTrackingReportId.Recipient;
			getMessageTrackingReportDetailParameters.ReportTemplate = ReportTemplate.RecipientPath;
			getMessageTrackingReportDetailParameters.ByPassDelegateChecking = true;
			getMessageTrackingReportDetailParameters.DetailLevel = MessageTrackingDetailLevel.Verbose;
			PSCommand psCommand = new PSCommand().AddCommand("Get-MessageTrackingReport").AddParameters(getMessageTrackingReportDetailParameters);
			return base.Invoke<RecipientTrackingEventRow>(psCommand);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x00089CFC File Offset: 0x00087EFC
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity&Filter@R:MyDistributionGroups")]
		internal RecipientRow GetOwnedDistributionGroup(string identity)
		{
			RecipientRow result = null;
			if (RbacPrincipal.Current.ExecutingUserId != null)
			{
				Identity identity2 = new Identity(identity, identity);
				PSCommand pscommand = new PSCommand().AddCommand("Get-Recipient");
				string distinguishedName = RbacPrincipal.Current.ExecutingUserId.DistinguishedName;
				string value = string.Format("ManagedBy -eq '{0}'", distinguishedName.Replace("'", "''"));
				pscommand.AddParameter("Filter", value);
				PowerShellResults<RecipientRow> @object = base.GetObject<RecipientRow>(pscommand, identity2);
				if (@object.SucceededWithValue)
				{
					result = @object.Value;
				}
			}
			return result;
		}

		// Token: 0x04002222 RID: 8738
		internal const string GetOwnedDistributionGroupRole = "Get-Recipient?Identity&Filter@R:MyDistributionGroups";

		// Token: 0x04002223 RID: 8739
		private const string GetObjectRole = "Get-MessageTrackingReport?Identity&ReportTemplate&RecipientPathFilter@R:Self";
	}
}
