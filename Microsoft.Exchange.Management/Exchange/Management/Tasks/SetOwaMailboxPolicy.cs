using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000454 RID: 1108
	[Cmdlet("Set", "OwaMailboxPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOwaMailboxPolicy : SetMailboxPolicyBase<OwaMailboxPolicy>
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0009B34E File Offset: 0x0009954E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOWAMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x0009B360 File Offset: 0x00099560
		// (set) Token: 0x06002739 RID: 10041 RVA: 0x0009B386 File Offset: 0x00099586
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

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0009B39E File Offset: 0x0009959E
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0009B3C4 File Offset: 0x000995C4
		[Parameter(Mandatory = false)]
		public SwitchParameter DisableFacebook
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableFacebook"] ?? false);
			}
			set
			{
				base.Fields["DisableFacebook"] = value;
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0009B3DC File Offset: 0x000995DC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.InstantMessagingType == InstantMessagingTypeOptions.Msn && !Datacenter.IsMultiTenancyEnabled())
			{
				base.WriteError(new TaskException(Strings.ErrorMsnIsNotSupportedInEnterprise), ErrorCategory.InvalidArgument, null);
			}
			if (this.IsDefault)
			{
				this.DataObject.IsDefault = true;
				this.otherDefaultPolicies = DefaultOwaMailboxPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession);
				if (this.otherDefaultPolicies.Count > 0)
				{
					this.updateOtherDefaultPolicies = true;
					return;
				}
			}
			else if (!this.IsDefault && base.Fields.IsChanged("IsDefault") && this.DataObject.IsDefault)
			{
				base.WriteError(new InvalidOperationException(Strings.ResettingIsDefaultIsNotSupported("IsDefault", "OwaMailboxPolicy")), ErrorCategory.WriteError, this.DataObject);
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0009B4D0 File Offset: 0x000996D0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.updateOtherDefaultPolicies)
			{
				if (!base.ShouldContinue(Strings.ConfirmationMessageSwitchMailboxPolicy("OWAMailboxPolicy", this.Identity.ToString())))
				{
					return;
				}
				try
				{
					DefaultMailboxPolicyUtility<OwaMailboxPolicy>.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.otherDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			if (base.Fields.IsChanged("DisableFacebook") && this.DisableFacebook)
			{
				this.DataObject.FacebookEnabled = false;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04001D9A RID: 7578
		private const string DisableFacebookParam = "DisableFacebook";
	}
}
