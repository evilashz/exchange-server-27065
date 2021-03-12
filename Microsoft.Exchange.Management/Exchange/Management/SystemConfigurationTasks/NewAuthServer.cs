using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000616 RID: 1558
	[Cmdlet("New", "AuthServer", DefaultParameterSetName = "AuthMetadataUrlParameterSet", SupportsShouldProcess = true)]
	public sealed class NewAuthServer : NewSystemConfigurationObjectTask<AuthServer>
	{
		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000E4A7E File Offset: 0x000E2C7E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAuthServer(base.Name);
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000E4A8B File Offset: 0x000E2C8B
		// (set) Token: 0x0600374A RID: 14154 RVA: 0x000E4AA2 File Offset: 0x000E2CA2
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

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000E4AB5 File Offset: 0x000E2CB5
		// (set) Token: 0x0600374C RID: 14156 RVA: 0x000E4AC2 File Offset: 0x000E2CC2
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet", Mandatory = true)]
		[Parameter(ParameterSetName = "NativeClientAuthServerParameterSet", Mandatory = true)]
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

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000E4AD0 File Offset: 0x000E2CD0
		// (set) Token: 0x0600374E RID: 14158 RVA: 0x000E4AD8 File Offset: 0x000E2CD8
		[Parameter(ParameterSetName = "NativeClientAuthServerParameterSet")]
		[Parameter(ParameterSetName = "AuthMetadataUrlParameterSet")]
		public SwitchParameter TrustAnySSLCertificate { get; set; }

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000E4AE1 File Offset: 0x000E2CE1
		// (set) Token: 0x06003750 RID: 14160 RVA: 0x000E4AEE File Offset: 0x000E2CEE
		[Parameter(ParameterSetName = "AppSecretParameterSet", Mandatory = true)]
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

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000E4AFC File Offset: 0x000E2CFC
		// (set) Token: 0x06003752 RID: 14162 RVA: 0x000E4B09 File Offset: 0x000E2D09
		[Parameter(ParameterSetName = "AppSecretParameterSet", Mandatory = true)]
		public string TokenIssuingEndpoint
		{
			get
			{
				return this.DataObject.TokenIssuingEndpoint;
			}
			set
			{
				this.DataObject.TokenIssuingEndpoint = value;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000E4B17 File Offset: 0x000E2D17
		// (set) Token: 0x06003754 RID: 14164 RVA: 0x000E4B24 File Offset: 0x000E2D24
		[Parameter(ParameterSetName = "AppSecretParameterSet")]
		public string AuthorizationEndpoint
		{
			get
			{
				return this.DataObject.AuthorizationEndpoint;
			}
			set
			{
				this.DataObject.AuthorizationEndpoint = value;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000E4B32 File Offset: 0x000E2D32
		// (set) Token: 0x06003756 RID: 14166 RVA: 0x000E4B3F File Offset: 0x000E2D3F
		[Parameter(ParameterSetName = "AppSecretParameterSet")]
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

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000E4B4D File Offset: 0x000E2D4D
		// (set) Token: 0x06003758 RID: 14168 RVA: 0x000E4B5A File Offset: 0x000E2D5A
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

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000E4B68 File Offset: 0x000E2D68
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000E4B75 File Offset: 0x000E2D75
		[Parameter(ParameterSetName = "NativeClientAuthServerParameterSet", Mandatory = true)]
		[Parameter(ParameterSetName = "AppSecretParameterSet", Mandatory = true)]
		public AuthServerType Type
		{
			get
			{
				return this.DataObject.Type;
			}
			set
			{
				this.DataObject.Type = value;
			}
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000E4B84 File Offset: 0x000E2D84
		protected override IConfigurable PrepareDataObject()
		{
			this.CreateAuthServersContainer();
			AuthServer authServer = (AuthServer)base.PrepareDataObject();
			ADObjectId containerId = AuthServer.GetContainerId(this.ConfigurationSession);
			authServer.SetId(containerId.GetChildId(authServer.Name));
			if (base.Fields.IsModified("AppSecretParameter"))
			{
				if (authServer.Type != AuthServerType.Facebook && authServer.Type != AuthServerType.LinkedIn)
				{
					base.WriteError(new TaskException(Strings.ErrorInvalidAuthServerTypeValue), ErrorCategory.InvalidArgument, null);
				}
				authServer.CurrentEncryptedAppSecret = OAuthTaskHelper.EncryptSecretWithDKM(this.AppSecret, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else if (authServer.IsModified(AuthServerSchema.AuthMetadataUrl))
			{
				if (!authServer.IsModified(AuthServerSchema.Type))
				{
					authServer.Type = AuthServerType.MicrosoftACS;
				}
				else if (authServer.Type != AuthServerType.ADFS && authServer.Type != AuthServerType.AzureAD)
				{
					base.WriteError(new TaskException(Strings.ErrorInvalidAuthServerTypeValue), ErrorCategory.InvalidArgument, null);
				}
				OAuthTaskHelper.FixAuthMetadataUrl(authServer, new Task.TaskErrorLoggingDelegate(base.WriteError));
				OAuthTaskHelper.FetchAuthMetadata(authServer, this.TrustAnySSLCertificate, true, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			OAuthTaskHelper.ValidateAuthServerRealmAndUniqueness(authServer, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			return this.DataObject;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		private void CreateAuthServersContainer()
		{
			ADObjectId containerId = AuthServer.GetContainerId(this.ConfigurationSession);
			if (this.ConfigurationSession.Read<Container>(containerId) == null)
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				Container container = new Container();
				container.SetId(containerId);
				configurationSession.Save(container);
			}
		}
	}
}
