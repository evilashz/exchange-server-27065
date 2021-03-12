using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientPermission
{
	// Token: 0x0200068D RID: 1677
	[Cmdlet("Add", "RecipientPermission", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class AddRecipientPermission : SetRecipientPermissionTaskBase
	{
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000FCBDF File Offset: 0x000FADDF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddRecipientPermission(this.Identity.ToString(), base.Trustee.ToString(), base.FormatMultiValuedProperty(base.AccessRights));
			}
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000FCC08 File Offset: 0x000FAE08
		protected override ActiveDirectorySecurityInheritance GetInheritanceType()
		{
			return ActiveDirectorySecurityInheritance.None;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000FCC0B File Offset: 0x000FAE0B
		protected override void ApplyModification(ActiveDirectoryAccessRule[] modifiedAces)
		{
			TaskLogger.LogEnter();
			DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.DataObject, modifiedAces);
			base.WriteResults(modifiedAces);
			TaskLogger.LogExit();
		}
	}
}
