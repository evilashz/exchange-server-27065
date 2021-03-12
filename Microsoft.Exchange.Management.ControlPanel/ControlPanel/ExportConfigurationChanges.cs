using System;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C3 RID: 963
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ExportConfigurationChanges : DataSourceService, IExportConfigurationChanges, INewObjectService<ExportConfigurationChangesRow, ExportConfigurationChangesParameters>
	{
		// Token: 0x06003212 RID: 12818 RVA: 0x0009BAD0 File Offset: 0x00099CD0
		[PrincipalPermission(SecurityAction.Demand, Role = "New-AdminAuditLogSearch?StartDate&EndDate&StatusMailRecipients@R:Organization")]
		public PowerShellResults<ExportConfigurationChangesRow> NewObject(ExportConfigurationChangesParameters parameters)
		{
			if (parameters.EndDate.IsNullOrBlank() || parameters.StartDate.IsNullOrBlank())
			{
				throw new FaultException(ClientStrings.DatesNotDefined);
			}
			if (parameters.StatusMailRecipients.IsNullOrBlank())
			{
				throw new FaultException(Strings.MailRecipientNotDefined);
			}
			return base.NewObject<ExportConfigurationChangesRow, ExportConfigurationChangesParameters>("New-AdminAuditLogSearch", parameters);
		}

		// Token: 0x0400246E RID: 9326
		internal const string WriteScope = "@R:Organization";

		// Token: 0x0400246F RID: 9327
		internal const string NewCmdlet = "New-AdminAuditLogSearch";

		// Token: 0x04002470 RID: 9328
		private const string NewObjectRole = "New-AdminAuditLogSearch?StartDate&EndDate&StatusMailRecipients@R:Organization";
	}
}
