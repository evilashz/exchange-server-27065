using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000447 RID: 1095
	[Cmdlet("Remove", "OwaMailboxPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOwaMailboxPolicy : RemoveMailboxPolicyBase<OwaMailboxPolicy>
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x00099518 File Offset: 0x00097718
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOwaMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0009952A File Offset: 0x0009772A
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00099532 File Offset: 0x00097732
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x060026B8 RID: 9912 RVA: 0x0009953B File Offset: 0x0009773B
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			base.WriteError(new CannotDeleteAssociatedMailboxPolicyException(this.Identity.ToString()), ErrorCategory.WriteError, base.DataObject);
			return false;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0009955C File Offset: 0x0009775C
		protected override void InternalValidate()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			base.InternalValidate();
			if (base.DataObject.IsDefault)
			{
				base.WriteError(new InvalidOperationException(Strings.RemovingDefaultPolicyIsNotSupported(this.Identity.ToString())), ErrorCategory.WriteError, base.DataObject);
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000995D4 File Offset: 0x000977D4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
