using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000618 RID: 1560
	[Cmdlet("Set", "AuthServer", DefaultParameterSetName = "AuthMetadataUrlParameterSet", SupportsShouldProcess = true)]
	public sealed class SetAuthServer : SetSystemConfigurationObjectTask<AuthServerIdParameter, AuthServer>
	{
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000E4D20 File Offset: 0x000E2F20
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAuthServer(this.Identity.RawIdentity);
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000E4D32 File Offset: 0x000E2F32
		// (set) Token: 0x06003763 RID: 14179 RVA: 0x000E4D49 File Offset: 0x000E2F49
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override AuthServerIdParameter Identity
		{
			get
			{
				return (AuthServerIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000E4D5C File Offset: 0x000E2F5C
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x000E4D73 File Offset: 0x000E2F73
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet")]
		[Parameter(ParameterSetName = "NativeClientAuthServerParameterSet")]
		public string AuthMetadataUrl
		{
			get
			{
				return (string)base.Fields[AuthServerSchema.AuthMetadataUrl];
			}
			set
			{
				base.Fields[AuthServerSchema.AuthMetadataUrl] = value;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000E4D86 File Offset: 0x000E2F86
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x000E4D8E File Offset: 0x000E2F8E
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet")]
		[Parameter(ParameterSetName = "NativeClientAuthServerParameterSet")]
		public SwitchParameter TrustAnySSLCertificate { get; set; }

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000E4D97 File Offset: 0x000E2F97
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000E4D9F File Offset: 0x000E2F9F
		[Parameter(ParameterSetName = "RefreshAuthMetadataParameterSet")]
		public SwitchParameter RefreshAuthMetadata { get; set; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000E4DA8 File Offset: 0x000E2FA8
		// (set) Token: 0x0600376B RID: 14187 RVA: 0x000E4DBF File Offset: 0x000E2FBF
		[Parameter(ParameterSetName = "AppSecretParameterSet")]
		public string AppSecret
		{
			get
			{
				return (string)base.Fields["AppSecretParameter"];
			}
			set
			{
				base.Fields["AppSecretParameter"] = value;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000E4DD2 File Offset: 0x000E2FD2
		// (set) Token: 0x0600376D RID: 14189 RVA: 0x000E4DE9 File Offset: 0x000E2FE9
		[Parameter(ParameterSetName = "AppSecretParameterSet")]
		public string IssuerIdentifier
		{
			get
			{
				return (string)base.Fields[AuthServerSchema.IssuerIdentifier];
			}
			set
			{
				base.Fields[AuthServerSchema.IssuerIdentifier] = value;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000E4DFC File Offset: 0x000E2FFC
		// (set) Token: 0x0600376F RID: 14191 RVA: 0x000E4E13 File Offset: 0x000E3013
		[Parameter(ParameterSetName = "AppSecretParameterSet")]
		public string TokenIssuingEndpoint
		{
			get
			{
				return (string)base.Fields[AuthServerSchema.TokenIssuingEndpoint];
			}
			set
			{
				base.Fields[AuthServerSchema.TokenIssuingEndpoint] = value;
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000E4E26 File Offset: 0x000E3026
		// (set) Token: 0x06003771 RID: 14193 RVA: 0x000E4E3D File Offset: 0x000E303D
		[Parameter(ParameterSetName = "AppSecretParameterSet")]
		public string ApplicationIdentifier
		{
			get
			{
				return (string)base.Fields[AuthServerSchema.ApplicationIdentifier];
			}
			set
			{
				base.Fields[AuthServerSchema.ApplicationIdentifier] = value;
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000E4E50 File Offset: 0x000E3050
		// (set) Token: 0x06003773 RID: 14195 RVA: 0x000E4E67 File Offset: 0x000E3067
		[Parameter(ParameterSetName = "NativeClientAuthServerParameterSet")]
		public bool IsDefaultAuthorizationEndpoint
		{
			get
			{
				return (bool)base.Fields[AuthServerSchema.IsDefaultAuthorizationEndpoint];
			}
			set
			{
				base.Fields[AuthServerSchema.IsDefaultAuthorizationEndpoint] = value;
			}
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000E4E80 File Offset: 0x000E3080
		protected override IConfigurable PrepareDataObject()
		{
			AuthServer authServer = (AuthServer)base.PrepareDataObject();
			if ((base.ParameterSetName == "AppSecretParameterSet" && !SetAuthServer.IsOneOfAuthServerTypes(authServer.Type, new AuthServerType[]
			{
				AuthServerType.Facebook,
				AuthServerType.LinkedIn
			})) || (base.ParameterSetName == "AuthMetadataUrlParameterSet" && !SetAuthServer.IsOneOfAuthServerTypes(authServer.Type, new AuthServerType[]
			{
				AuthServerType.MicrosoftACS,
				AuthServerType.AzureAD,
				AuthServerType.ADFS
			})) || (base.ParameterSetName == "NativeClientAuthServerParameterSet" && !SetAuthServer.IsOneOfAuthServerTypes(authServer.Type, new AuthServerType[]
			{
				AuthServerType.AzureAD,
				AuthServerType.ADFS
			})))
			{
				base.WriteError(new TaskException(Strings.ErrorAuthServerCannotSwitchType), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("AppSecretParameter"))
			{
				authServer.CurrentEncryptedAppSecret = OAuthTaskHelper.EncryptSecretWithDKM(this.AppSecret, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(AuthServerSchema.IssuerIdentifier))
			{
				authServer.IssuerIdentifier = this.IssuerIdentifier;
			}
			if (base.Fields.IsModified(AuthServerSchema.TokenIssuingEndpoint))
			{
				authServer.TokenIssuingEndpoint = this.TokenIssuingEndpoint;
			}
			if (base.Fields.IsModified(AuthServerSchema.ApplicationIdentifier))
			{
				authServer.ApplicationIdentifier = this.ApplicationIdentifier;
			}
			if (base.Fields.IsModified(AuthServerSchema.AuthMetadataUrl))
			{
				authServer.AuthMetadataUrl = this.AuthMetadataUrl;
				OAuthTaskHelper.FixAuthMetadataUrl(authServer, new Task.TaskErrorLoggingDelegate(base.WriteError));
				OAuthTaskHelper.FetchAuthMetadata(authServer, this.TrustAnySSLCertificate, false, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
				OAuthTaskHelper.ValidateAuthServerRealmAndUniqueness(authServer, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(AuthServerSchema.IsDefaultAuthorizationEndpoint))
			{
				authServer.IsDefaultAuthorizationEndpoint = this.IsDefaultAuthorizationEndpoint;
				OAuthTaskHelper.ValidateAuthServerAuthorizationEndpoint(authServer, this.ConfigurationSession, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			return authServer;
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000E507C File Offset: 0x000E327C
		protected override void InternalProcessRecord()
		{
			if (this.RefreshAuthMetadata)
			{
				OAuthTaskHelper.FetchAuthMetadata(this.DataObject, this.TrustAnySSLCertificate, false, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000E50CC File Offset: 0x000E32CC
		private static bool IsOneOfAuthServerTypes(AuthServerType thisType, params AuthServerType[] authServerTypes)
		{
			foreach (AuthServerType authServerType in authServerTypes)
			{
				if (authServerType == thisType)
				{
					return true;
				}
			}
			return false;
		}
	}
}
