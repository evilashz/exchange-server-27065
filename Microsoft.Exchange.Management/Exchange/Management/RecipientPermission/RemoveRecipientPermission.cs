using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Permission;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientPermission
{
	// Token: 0x02000690 RID: 1680
	[Cmdlet("Remove", "RecipientPermission", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveRecipientPermission : SetRecipientPermissionTaskBase
	{
		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06003B74 RID: 15220 RVA: 0x000FD092 File Offset: 0x000FB292
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRecipientPermission(this.Identity.ToString(), base.Trustee.ToString(), base.FormatMultiValuedProperty(base.AccessRights));
			}
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000FD0BB File Offset: 0x000FB2BB
		protected override ActiveDirectorySecurityInheritance GetInheritanceType()
		{
			return ActiveDirectorySecurityInheritance.All;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000FD0C0 File Offset: 0x000FB2C0
		protected override void ApplyModification(ActiveDirectoryAccessRule[] modifiedAces)
		{
			TaskLogger.LogEnter();
			if (this.trustee != null)
			{
				List<ActiveDirectoryAccessRule> list = new List<ActiveDirectoryAccessRule>();
				foreach (SecurityIdentifier identity in ((IADSecurityPrincipal)this.trustee).SidHistory)
				{
					foreach (RecipientAccessRight right in base.AccessRights)
					{
						list.Add(new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, RecipientPermissionHelper.GetRecipientAccessRightGuid(right), this.GetInheritanceType(), Guid.Empty));
					}
				}
				if (list.Count > 0)
				{
					list.AddRange(modifiedAces);
					modifiedAces = list.ToArray();
				}
			}
			DirectoryCommon.RemoveAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(base.WriteError), this.DataObject, modifiedAces);
			TaskLogger.LogExit();
		}
	}
}
