using System;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C7 RID: 967
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ExportMailboxChanges : DataSourceService, IExportMailboxChanges, INewObjectService<ExportMailboxChangesRow, ExportMailboxChangesParameters>
	{
		// Token: 0x0600321E RID: 12830 RVA: 0x0009BBC8 File Offset: 0x00099DC8
		[PrincipalPermission(SecurityAction.Demand, Role = "New-MailboxAuditLogSearch?StartDate&EndDate&Mailboxes&StatusMailRecipients&LogonTypes&ExternalAccess&ShowDetails@R:Organization")]
		public PowerShellResults<ExportMailboxChangesRow> NewObject(ExportMailboxChangesParameters parameters)
		{
			string logonTypes;
			if ((logonTypes = parameters.LogonTypes) != null)
			{
				if (!(logonTypes == "AllNonOwners"))
				{
					if (!(logonTypes == "OutsideUsers"))
					{
						if (!(logonTypes == "InternalUsers"))
						{
							if (logonTypes == "NonDelegateUsers")
							{
								parameters.LogonTypes = "Admin";
								parameters.ExternalAccess = new bool?(false);
							}
						}
						else
						{
							parameters.LogonTypes = "Admin,Delegate";
							parameters.ExternalAccess = new bool?(false);
						}
					}
					else
					{
						parameters.LogonTypes = null;
						parameters.ExternalAccess = new bool?(true);
					}
				}
				else
				{
					parameters.LogonTypes = "Admin,Delegate";
					parameters.ExternalAccess = null;
				}
			}
			if (parameters.EndDate.IsNullOrBlank() || parameters.StartDate.IsNullOrBlank())
			{
				throw new FaultException(ClientStrings.DatesNotDefined);
			}
			if (parameters.StatusMailRecipients.IsNullOrBlank())
			{
				throw new FaultException(Strings.MailRecipientNotDefined);
			}
			parameters.ShowDetails = true;
			return base.NewObject<ExportMailboxChangesRow, ExportMailboxChangesParameters>("New-MailboxAuditLogSearch", parameters);
		}

		// Token: 0x04002472 RID: 9330
		internal const string WriteScope = "@R:Organization";

		// Token: 0x04002473 RID: 9331
		internal const string NewCmdlet = "New-MailboxAuditLogSearch";

		// Token: 0x04002474 RID: 9332
		private const string NewObjectRole = "New-MailboxAuditLogSearch?StartDate&EndDate&Mailboxes&StatusMailRecipients&LogonTypes&ExternalAccess&ShowDetails@R:Organization";
	}
}
