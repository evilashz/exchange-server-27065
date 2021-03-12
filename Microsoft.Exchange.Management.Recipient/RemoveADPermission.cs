using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A7 RID: 167
	[Cmdlet("Remove", "ADPermission", DefaultParameterSetName = "AccessRights", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveADPermission : SetADPermissionTaskBase
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002D7E0 File Offset: 0x0002B9E0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveADPermissionAccessRights(this.Identity.ToString(), (base.Instance.AccessRights != null) ? base.FormatMultiValuedProperty(base.Instance.AccessRights) : base.FormatMultiValuedProperty(base.Instance.ExtendedRights), base.Instance.User.ToString());
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002D83E File Offset: 0x0002BA3E
		protected override void ApplyModification(ADRawEntry modifiedObject, ActiveDirectoryAccessRule[] modifiedAces)
		{
			DirectoryCommon.RemoveAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(this.WriteErrorPerObject), base.GetWritableSession(modifiedObject.Id), modifiedObject.Id, modifiedAces);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002D87E File Offset: 0x0002BA7E
		protected override void WriteAces(ADObjectId id, IEnumerable<ActiveDirectoryAccessRule> aces)
		{
		}
	}
}
