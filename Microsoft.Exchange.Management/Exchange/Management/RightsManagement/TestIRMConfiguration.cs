using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200072E RID: 1838
	[Cmdlet("Test", "IRMConfiguration", SupportsShouldProcess = true)]
	public sealed class TestIRMConfiguration : GetMultitenancySingletonSystemConfigurationObjectTask<IRMConfiguration>
	{
		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x0010C198 File Offset: 0x0010A398
		// (set) Token: 0x0600414E RID: 16718 RVA: 0x0010C1AF File Offset: 0x0010A3AF
		[ValidateNotNullOrEmpty]
		[ValidateCount(1, 100)]
		[Parameter]
		public SmtpAddress[] Recipient
		{
			get
			{
				return (SmtpAddress[])base.Fields["Recipient"];
			}
			set
			{
				base.Fields["Recipient"] = value;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x0010C1C2 File Offset: 0x0010A3C2
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x0010C1D9 File Offset: 0x0010A3D9
		[Parameter]
		public SmtpAddress? Sender
		{
			get
			{
				return (SmtpAddress?)base.Fields["Sender"];
			}
			set
			{
				base.Fields["Sender"] = value;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x0010C1F1 File Offset: 0x0010A3F1
		// (set) Token: 0x06004152 RID: 16722 RVA: 0x0010C217 File Offset: 0x0010A417
		[Parameter]
		public SwitchParameter RMSOnline
		{
			get
			{
				return (SwitchParameter)(base.Fields["RMSOnline"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RMSOnline"] = value;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x0010C22F File Offset: 0x0010A42F
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x0010C254 File Offset: 0x0010A454
		[Parameter]
		public Guid RMSOnlineOrgOverride
		{
			get
			{
				return (Guid)(base.Fields["RMSOnlineOrgOverride"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["RMSOnlineOrgOverride"] = value;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x0010C26C File Offset: 0x0010A46C
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x0010C283 File Offset: 0x0010A483
		[Parameter]
		public string RMSOnlineAuthCertSubjectNameOverride
		{
			get
			{
				return (string)base.Fields["RMSOnlineAuthCertSubjectNameOverride"];
			}
			set
			{
				base.Fields["RMSOnlineAuthCertSubjectNameOverride"] = value;
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x0010C296 File Offset: 0x0010A496
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x0010C299 File Offset: 0x0010A499
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestIRMConfiguration;
			}
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x0010C2A0 File Offset: 0x0010A4A0
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			this.ThrowIfBothSenderAndRmsOnlineParametersSpecified();
			this.ThrowIfNeitherSenderAndRmsOnlineParametersSpecified();
			this.ThrowIfCurrentOrganizationIdIsNull();
			if (this.RMSOnline)
			{
				RMSOnlineValidator rmsonlineValidator = new RMSOnlineValidator(this.ConfigurationSession, (IConfigurationSession)base.DataSession, base.CurrentOrganizationId, this.RMSOnlineOrgOverride, this.RMSOnlineAuthCertSubjectNameOverride);
				this.WriteResult(rmsonlineValidator.Validate());
				return;
			}
			RmsClientManager.ADSession = this.ConfigurationSession;
			IRMConfigurationValidator irmconfigurationValidator = new IRMConfigurationValidator(new RmsClientManagerContext(base.CurrentOrganizationId, null), this.Sender.Value, this.Recipient);
			IRMConfigurationValidationResult dataObject = irmconfigurationValidator.Validate();
			RmsClientManager.ADSession = null;
			this.WriteResult(dataObject);
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x0010C350 File Offset: 0x0010A550
		protected override void WriteResult(IConfigurable dataObject)
		{
			IRMConfigurationValidationResult irmconfigurationValidationResult = dataObject as IRMConfigurationValidationResult;
			if (irmconfigurationValidationResult != null)
			{
				base.WriteResult(irmconfigurationValidationResult);
			}
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0010C36E File Offset: 0x0010A56E
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception);
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x0010C384 File Offset: 0x0010A584
		private void ThrowIfBothSenderAndRmsOnlineParametersSpecified()
		{
			if (this.Sender != null && this.RMSOnline)
			{
				base.WriteError(new SenderAndRmsOnlineParametersCannotBeCombinedException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0010C3BC File Offset: 0x0010A5BC
		private void ThrowIfNeitherSenderAndRmsOnlineParametersSpecified()
		{
			if (this.Sender == null && !this.RMSOnline)
			{
				base.WriteError(new EitherSenderOrRmsOnlineParametersMustBeSpecifiedException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x0010C3F3 File Offset: 0x0010A5F3
		private void ThrowIfCurrentOrganizationIdIsNull()
		{
			if (base.CurrentOrganizationId == null)
			{
				base.WriteError(new NullOrganizationIdException(), ErrorCategory.InvalidOperation, null);
			}
		}
	}
}
