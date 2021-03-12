using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200008E RID: 142
	[Cmdlet("set", "MailUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailUser : SetMailUserBase<MailUserIdParameter, MailUser>
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000290AF File Offset: 0x000272AF
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x000290D5 File Offset: 0x000272D5
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000290ED File Offset: 0x000272ED
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00029104 File Offset: 0x00027304
		[Parameter(Mandatory = false)]
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)base.Fields[ADRecipientSchema.MailboxProvisioningConstraint];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningConstraint] = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00029117 File Offset: 0x00027317
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0002912E File Offset: 0x0002732E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)base.Fields[ADRecipientSchema.MailboxProvisioningPreferences];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningPreferences] = value;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00029160 File Offset: 0x00027360
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			if (base.Fields.IsModified(ADRecipientSchema.MailboxProvisioningConstraint))
			{
				aduser.MailboxProvisioningConstraint = this.MailboxProvisioningConstraint;
			}
			if (base.Fields.IsModified(ADRecipientSchema.MailboxProvisioningPreferences))
			{
				aduser.MailboxProvisioningPreferences = this.MailboxProvisioningPreferences;
			}
			if (aduser.MailboxProvisioningConstraint != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(new MailboxProvisioningConstraint[]
				{
					aduser.MailboxProvisioningConstraint
				}, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (aduser.MailboxProvisioningPreferences != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(aduser.MailboxProvisioningPreferences, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00029248 File Offset: 0x00027448
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (ComplianceConfigImpl.JournalArchivingHardeningEnabled && this.DataObject.IsModified(ADRecipientSchema.EmailAddresses) && this.originalJournalArchiveAddress != SmtpAddress.Empty && this.DataObject.JournalArchiveAddress.CompareTo(this.originalJournalArchiveAddress) != 0)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorModifyJournalArchiveAddress), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000292C1 File Offset: 0x000274C1
		protected override void InternalProcessRecord()
		{
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ContinueUpgradeObjectVersion(this.DataObject.Name)))
			{
				base.InternalProcessRecord();
			}
		}
	}
}
