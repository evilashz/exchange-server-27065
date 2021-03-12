using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000612 RID: 1554
	[Cmdlet("New", "PartnerApplication", DefaultParameterSetName = "AuthMetadataUrlParameterSet", SupportsShouldProcess = true)]
	public sealed class NewPartnerApplication : NewMultitenancySystemConfigurationObjectTask<PartnerApplication>
	{
		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x000E43C8 File Offset: 0x000E25C8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPartnerApplication(base.Name);
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000E43D5 File Offset: 0x000E25D5
		// (set) Token: 0x06003716 RID: 14102 RVA: 0x000E43E2 File Offset: 0x000E25E2
		[Parameter(ParameterSetName = "ACSTrustApplicationParameterSet", Mandatory = true)]
		public string ApplicationIdentifier
		{
			get
			{
				return this.DataObject.ApplicationIdentifier;
			}
			set
			{
				this.DataObject.ApplicationIdentifier = value;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000E43F0 File Offset: 0x000E25F0
		// (set) Token: 0x06003718 RID: 14104 RVA: 0x000E43FD File Offset: 0x000E25FD
		[Parameter]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000E440B File Offset: 0x000E260B
		// (set) Token: 0x0600371A RID: 14106 RVA: 0x000E4418 File Offset: 0x000E2618
		[Parameter]
		public bool AcceptSecurityIdentifierInformation
		{
			get
			{
				return this.DataObject.AcceptSecurityIdentifierInformation;
			}
			set
			{
				this.DataObject.AcceptSecurityIdentifierInformation = value;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000E4426 File Offset: 0x000E2626
		// (set) Token: 0x0600371C RID: 14108 RVA: 0x000E4433 File Offset: 0x000E2633
		[Parameter(ParameterSetName = "ACSTrustApplicationParameterSet")]
		public string Realm
		{
			get
			{
				return this.DataObject.Realm;
			}
			set
			{
				this.DataObject.Realm = value;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000E4441 File Offset: 0x000E2641
		// (set) Token: 0x0600371E RID: 14110 RVA: 0x000E444E File Offset: 0x000E264E
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet", Mandatory = true)]
		public string AuthMetadataUrl
		{
			get
			{
				return this.DataObject.AuthMetadataUrl;
			}
			set
			{
				this.DataObject.AuthMetadataUrl = value;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000E445C File Offset: 0x000E265C
		// (set) Token: 0x06003720 RID: 14112 RVA: 0x000E4464 File Offset: 0x000E2664
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet")]
		public SwitchParameter TrustAnySSLCertificate { get; set; }

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000E446D File Offset: 0x000E266D
		// (set) Token: 0x06003722 RID: 14114 RVA: 0x000E4484 File Offset: 0x000E2684
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

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000E4497 File Offset: 0x000E2697
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x000E44A4 File Offset: 0x000E26A4
		[Parameter]
		public string IssuerIdentifier
		{
			get
			{
				return this.DataObject.IssuerIdentifier;
			}
			set
			{
				this.DataObject.IssuerIdentifier = value;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000E44B2 File Offset: 0x000E26B2
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x000E44BF File Offset: 0x000E26BF
		[Parameter]
		public string[] AppOnlyPermissions
		{
			get
			{
				return this.DataObject.AppOnlyPermissions;
			}
			set
			{
				this.DataObject.AppOnlyPermissions = value;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000E44CD File Offset: 0x000E26CD
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x000E44DA File Offset: 0x000E26DA
		[Parameter]
		public string[] ActAsPermissions
		{
			get
			{
				return this.DataObject.ActAsPermissions;
			}
			set
			{
				this.DataObject.ActAsPermissions = value;
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000E44E8 File Offset: 0x000E26E8
		protected override IConfigurable PrepareDataObject()
		{
			this.CreatePartnerApplicationsContainer();
			PartnerApplication partnerApplication = (PartnerApplication)base.PrepareDataObject();
			ADObjectId containerId = PartnerApplication.GetContainerId(this.ConfigurationSession);
			partnerApplication.SetId(containerId.GetChildId(partnerApplication.Name));
			partnerApplication.UseAuthServer = true;
			if (partnerApplication.IsModified(PartnerApplicationSchema.AuthMetadataUrl))
			{
				partnerApplication.UseAuthServer = false;
				OAuthTaskHelper.FetchAuthMetadata(partnerApplication, this.TrustAnySSLCertificate, true, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
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

		// Token: 0x0600372A RID: 14122 RVA: 0x000E4634 File Offset: 0x000E2834
		private void CreatePartnerApplicationsContainer()
		{
			ADObjectId containerId = PartnerApplication.GetContainerId(this.ConfigurationSession);
			if (this.ConfigurationSession.Read<Container>(containerId) == null)
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				OrganizationId currentOrganizationId = this.ConfigurationSession.SessionSettings.CurrentOrganizationId;
				if (!currentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					ADObjectId containerId2 = AuthConfig.GetContainerId(this.ConfigurationSession);
					if (this.ConfigurationSession.Read<AuthConfig>(containerId2) == null)
					{
						AuthConfig authConfig = new AuthConfig();
						authConfig.OrganizationId = currentOrganizationId;
						authConfig.SetId(containerId2);
						configurationSession.Save(authConfig);
					}
				}
				Container container = new Container();
				container.OrganizationId = currentOrganizationId;
				container.SetId(containerId);
				configurationSession.Save(container);
			}
		}
	}
}
