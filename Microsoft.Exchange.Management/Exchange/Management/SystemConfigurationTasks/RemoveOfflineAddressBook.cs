using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ADF RID: 2783
	[Cmdlet("Remove", "OfflineAddressBook", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOfflineAddressBook : RemoveSystemConfigurationObjectTask<OfflineAddressBookIdParameter, OfflineAddressBook>
	{
		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x060062E8 RID: 25320 RVA: 0x0019D680 File Offset: 0x0019B880
		// (set) Token: 0x060062E9 RID: 25321 RVA: 0x0019D6A6 File Offset: 0x0019B8A6
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x060062EA RID: 25322 RVA: 0x0019D6BE File Offset: 0x0019B8BE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOfflineAddressBook(this.Identity.ToString());
			}
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x0019D6D0 File Offset: 0x0019B8D0
		private bool HandleRemoveWithAssociatedAddressBookPolicies()
		{
			base.WriteError(new InvalidOperationException(Strings.ErrorRemoveOfflineAddressBookWithAssociatedAddressBookPolicies(base.DataObject.Name)), ErrorCategory.InvalidOperation, base.DataObject.Identity);
			return false;
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x0019D700 File Offset: 0x0019B900
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			OfflineAddressBook dataObject = base.DataObject;
			if (dataObject.CheckForAssociatedAddressBookPolicies() && !this.HandleRemoveWithAssociatedAddressBookPolicies())
			{
				TaskLogger.LogExit();
				return;
			}
			if (base.DataObject.IsDefault && !this.Force && !base.ShouldContinue(Strings.RemoveDefaultOAB(this.Identity.ToString())))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
