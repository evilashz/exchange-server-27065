using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000445 RID: 1093
	[Cmdlet("New", "OwaMailboxPolicy", SupportsShouldProcess = true)]
	public sealed class NewOwaMailboxPolicy : NewMailboxPolicyBase<OwaMailboxPolicy>
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x000992F8 File Offset: 0x000974F8
		// (set) Token: 0x060026AD RID: 9901 RVA: 0x0009931E File Offset: 0x0009751E
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? false);
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x00099336 File Offset: 0x00097536
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOWAMailboxPolicy(base.Name);
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00099344 File Offset: 0x00097544
		protected override void InternalProcessRecord()
		{
			if (this.updateExistingDefaultPolicies)
			{
				if (!base.ShouldContinue(Strings.ConfirmationMessageSwitchMailboxPolicy("OWAMailboxPolicy", this.DataObject.Name)))
				{
					return;
				}
				try
				{
					DefaultMailboxPolicyUtility<OwaMailboxPolicy>.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.existingDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			this.DataObject.ActionForUnknownFileAndMIMETypes = AttachmentBlockingActions.Allow;
			base.InternalProcessRecord();
			this.DataObject.AllowedFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultAllowedFileTypes);
			this.DataObject.AllowedMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultAllowedMimeTypes);
			this.DataObject.BlockedFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultBlockedFileTypes);
			this.DataObject.BlockedMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultBlockedMimeTypes);
			this.DataObject.ForceSaveFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultForceSaveFileTypes);
			this.DataObject.ForceSaveMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultForceSaveMimeTypes);
			this.DataObject.WebReadyFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyFileTypes);
			this.DataObject.WebReadyMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyMimeTypes);
			base.DataSession.Save(this.DataObject);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x00099478 File Offset: 0x00097678
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.IsDefault)
			{
				this.DataObject.IsDefault = true;
			}
			if (this.DataObject.IsDefault)
			{
				this.existingDefaultPolicies = DefaultOwaMailboxPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession);
				if (this.existingDefaultPolicies.Count > 0)
				{
					this.updateExistingDefaultPolicies = true;
				}
			}
		}
	}
}
