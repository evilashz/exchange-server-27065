using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000683 RID: 1667
	[Cmdlet("Remove", "ManagementRoleEntry", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveManagementRoleEntry : AddRemoveManagementRoleEntryActionBase
	{
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000FAE3E File Offset: 0x000F903E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveManagementRoleEntry(this.removedEntry.ToString(), this.DataObject.Id.ToString());
			}
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x000FAE60 File Offset: 0x000F9060
		protected override bool IsTopLevelUnscopedRoleModificationAllowed()
		{
			return true;
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000FAE64 File Offset: 0x000F9064
		protected override void InternalApplyChangeAndValidate()
		{
			TaskLogger.LogEnter();
			this.removedEntry = RoleHelper.GetMandatoryRoleEntry(this.DataObject, this.Identity.CmdletOrScriptName, this.Identity.PSSnapinName, new Task.TaskErrorLoggingDelegate(base.WriteError));
			this.InternalAddRemoveRoleEntry(this.DataObject.RoleEntries);
			TaskLogger.LogExit();
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x000FAEBF File Offset: 0x000F90BF
		protected override void InternalAddRemoveRoleEntry(MultiValuedProperty<RoleEntry> roleEntries)
		{
			roleEntries.Remove(this.removedEntry);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000FAECE File Offset: 0x000F90CE
		protected override string GetRoleEntryString()
		{
			return this.removedEntry.ToString();
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000FAEDB File Offset: 0x000F90DB
		protected override LocalizedException GetRoleSaveException(string roleEntry, string role, string exception)
		{
			return new ExRBACSaveRemoveRoleEntry(roleEntry, role, exception);
		}

		// Token: 0x040026B4 RID: 9908
		private RoleEntry removedEntry;
	}
}
