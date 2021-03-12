using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000614 RID: 1556
	[Cmdlet("Set", "PartnerApplication", DefaultParameterSetName = "AuthMetadataUrlParameterSet", SupportsShouldProcess = true)]
	public sealed class SetPartnerApplication : SetSystemConfigurationObjectTask<PartnerApplicationIdParameter, PartnerApplication>
	{
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000E46F8 File Offset: 0x000E28F8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPartnerApplication(this.Identity.RawIdentity);
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000E470A File Offset: 0x000E290A
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x000E4721 File Offset: 0x000E2921
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override PartnerApplicationIdParameter Identity
		{
			get
			{
				return (PartnerApplicationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000E4734 File Offset: 0x000E2934
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x000E474B File Offset: 0x000E294B
		[Parameter(ParameterSetName = "ACSTrustApplicationParameterSet")]
		public string ApplicationIdentifier
		{
			get
			{
				return (string)base.Fields[PartnerApplicationSchema.ApplicationIdentifier];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.ApplicationIdentifier] = value;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000E475E File Offset: 0x000E295E
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x000E4775 File Offset: 0x000E2975
		[Parameter(ParameterSetName = "ACSTrustApplicationParameterSet")]
		public string Realm
		{
			get
			{
				return (string)base.Fields[PartnerApplicationSchema.Realm];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.Realm] = value;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000E4788 File Offset: 0x000E2988
		// (set) Token: 0x06003736 RID: 14134 RVA: 0x000E479F File Offset: 0x000E299F
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet")]
		public string AuthMetadataUrl
		{
			get
			{
				return (string)base.Fields[PartnerApplicationSchema.AuthMetadataUrl];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.AuthMetadataUrl] = value;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x000E47B2 File Offset: 0x000E29B2
		// (set) Token: 0x06003738 RID: 14136 RVA: 0x000E47BA File Offset: 0x000E29BA
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet")]
		public SwitchParameter TrustAnySSLCertificate { get; set; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000E47C3 File Offset: 0x000E29C3
		// (set) Token: 0x0600373A RID: 14138 RVA: 0x000E47CB File Offset: 0x000E29CB
		[Parameter(ParameterSetName = "RefreshAuthMetadataParameterSet")]
		public SwitchParameter RefreshAuthMetadata { get; set; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000E47D4 File Offset: 0x000E29D4
		// (set) Token: 0x0600373C RID: 14140 RVA: 0x000E47EB File Offset: 0x000E29EB
		[Parameter]
		public UserIdParameter LinkedAccount
		{
			get
			{
				return (UserIdParameter)base.Fields[PartnerApplicationSchema.LinkedAccount];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.LinkedAccount] = value;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000E47FE File Offset: 0x000E29FE
		// (set) Token: 0x0600373E RID: 14142 RVA: 0x000E4815 File Offset: 0x000E2A15
		[Parameter]
		public string IssuerIdentifier
		{
			get
			{
				return (string)base.Fields[PartnerApplicationSchema.IssuerIdentifier];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.IssuerIdentifier] = value;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000E4828 File Offset: 0x000E2A28
		// (set) Token: 0x06003740 RID: 14144 RVA: 0x000E483F File Offset: 0x000E2A3F
		[Parameter]
		public string[] AppOnlyPermissions
		{
			get
			{
				return (string[])base.Fields[PartnerApplicationSchema.AppOnlyPermissions];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.AppOnlyPermissions] = value;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000E4852 File Offset: 0x000E2A52
		// (set) Token: 0x06003742 RID: 14146 RVA: 0x000E4869 File Offset: 0x000E2A69
		[Parameter]
		public string[] ActAsPermissions
		{
			get
			{
				return (string[])base.Fields[PartnerApplicationSchema.ActAsPermissions];
			}
			set
			{
				base.Fields[PartnerApplicationSchema.ActAsPermissions] = value;
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000E487C File Offset: 0x000E2A7C
		protected override IConfigurable PrepareDataObject()
		{
			PartnerApplication partnerApplication = (PartnerApplication)base.PrepareDataObject();
			if (base.Fields.IsModified(PartnerApplicationSchema.AuthMetadataUrl))
			{
				if (partnerApplication.UseAuthServer)
				{
					base.WriteError(new TaskException(Strings.ErrorPartnerApplicationUseAuthServerCannotSetUrl), ErrorCategory.InvalidArgument, null);
				}
				partnerApplication.AuthMetadataUrl = this.AuthMetadataUrl;
				OAuthTaskHelper.FetchAuthMetadata(partnerApplication, this.TrustAnySSLCertificate, false, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else if (base.Fields.IsModified(PartnerApplicationSchema.Realm) || base.Fields.IsModified(PartnerApplicationSchema.ApplicationIdentifier) || base.Fields.IsModified(PartnerApplicationSchema.IssuerIdentifier))
			{
				base.WriteError(new TaskException(Strings.ErrorChangePartnerApplicationDirectTrust), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified(PartnerApplicationSchema.LinkedAccount))
			{
				if (this.LinkedAccount == null)
				{
					partnerApplication.LinkedAccount = null;
				}
				else
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.LinkedAccount, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.LinkedAccount.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.LinkedAccount.ToString())));
					partnerApplication.LinkedAccount = adrecipient.Id;
				}
			}
			if (base.Fields.IsModified(PartnerApplicationSchema.AppOnlyPermissions))
			{
				partnerApplication.AppOnlyPermissions = this.AppOnlyPermissions;
			}
			if (base.Fields.IsModified(PartnerApplicationSchema.ActAsPermissions))
			{
				partnerApplication.ActAsPermissions = this.ActAsPermissions;
			}
			OAuthTaskHelper.ValidateApplicationRealmAndUniqueness(partnerApplication, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			return partnerApplication;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000E4A0C File Offset: 0x000E2C0C
		protected override void InternalProcessRecord()
		{
			if (this.RefreshAuthMetadata)
			{
				OAuthTaskHelper.FetchAuthMetadata(this.DataObject, this.TrustAnySSLCertificate, false, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			base.InternalProcessRecord();
		}
	}
}
