using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x020002A1 RID: 673
	internal sealed class ConnectionRetryDiscoverer
	{
		// Token: 0x06001C6D RID: 7277 RVA: 0x0007B07C File Offset: 0x0007927C
		static ConnectionRetryDiscoverer()
		{
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools";
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
				{
					if (registryKey != null)
					{
						bool.TryParse(registryKey.GetValue("EMC.SkipCertificateCheck") as string, out ConnectionRetryDiscoverer.skipCertificateCheckSetting);
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0007B0F8 File Offset: 0x000792F8
		public ConnectionRetryDiscoverer(UIInteractionHandler uiInteractionHandler, OrganizationType orgType, string displayName, Uri uri, bool logonWithDefaultCredential, string credentialKey, PSCredential proposedCredential)
		{
			this.uiInteractionHandler = uiInteractionHandler;
			this.orgType = orgType;
			this.displayName = displayName;
			this.uri = uri;
			this.logonWithDefaultCredential = logonWithDefaultCredential;
			this.credentialKey = credentialKey;
			this.proposedCredential = proposedCredential;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0007B14B File Offset: 0x0007934B
		public ConnectionRetryDiscoverer(IUIService uiService, OrganizationType orgType, string displayName, Uri uri, bool logonWithDefaultCredential) : this(new WindowUIInteractionHandler((Control)uiService.GetDialogOwnerWindow()), orgType, displayName, uri, logonWithDefaultCredential, null, null)
		{
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0007B16C File Offset: 0x0007936C
		public ConnectionRetryDiscoverer(PSConnectionInfoSingleton connInfoSingleton) : this((connInfoSingleton.UISyncContext == null) ? null : new SyncContextUIInteractionHandler(connInfoSingleton.UISyncContext), connInfoSingleton.Type, connInfoSingleton.DisplayName, connInfoSingleton.Uri, connInfoSingleton.LogonWithDefaultCredential, connInfoSingleton.CredentialKey, connInfoSingleton.ProposedCredential)
		{
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0007B1B9 File Offset: 0x000793B9
		public ConnectionRetryDiscoverer(OrganizationSetting orgSetting, IUIService uiService) : this(new WindowUIInteractionHandler((Control)uiService.GetDialogOwnerWindow()), orgSetting.Type, orgSetting.DisplayName, orgSetting.Uri, orgSetting.LogonWithDefaultCredential, orgSetting.CredentialKey, null)
		{
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0007B1F0 File Offset: 0x000793F0
		public MonadConnectionInfo DiscoverConnectionInfo(out SupportedVersionList supportedVersionList, string slotVersion)
		{
			if (string.IsNullOrEmpty(this.credentialKey) && !this.logonWithDefaultCredential)
			{
				throw new NotSupportedException();
			}
			bool flag = false;
			MonadConnectionInfo monadConnectionInfo = this.DiscoverConnectionInfo(ref flag, out supportedVersionList, slotVersion);
			if (flag && monadConnectionInfo.Credentials != null)
			{
				CredentialHelper.SaveCredential(this.credentialKey, monadConnectionInfo.Credentials);
			}
			return monadConnectionInfo;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0007B244 File Offset: 0x00079444
		public MonadConnectionInfo DiscoverConnectionInfo(ref bool rememberCredentialChecked, out SupportedVersionList supportedVersionList, string slotVersion)
		{
			this.rememberCredentialChecked = rememberCredentialChecked;
			this.slotVersion = slotVersion;
			MonadConnectionInfo result = this.DiscoverConnectionInfoInternal();
			rememberCredentialChecked = this.rememberCredentialChecked;
			supportedVersionList = this.supportedVersionList;
			return result;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0007B284 File Offset: 0x00079484
		private MonadConnectionInfo DiscoverConnectionInfoInternal()
		{
			if (!this.logonWithDefaultCredential)
			{
				return this.RetryCredentialsFromLoadOrInput((PSCredential cred) => this.TryConnectWithExplicitCredential(cred));
			}
			MonadConnectionInfo monadConnectionInfo = this.TryConnectWithDefaultCredential();
			if (monadConnectionInfo != null)
			{
				return monadConnectionInfo;
			}
			throw new OperationCanceledException(this.GetErrorMessage());
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0007B328 File Offset: 0x00079528
		private MonadConnectionInfo RetryCredentialsFromLoadOrInput(Func<PSCredential, MonadConnectionInfo> tryConnectWithCredential)
		{
			PSCredential cred = this.proposedCredential;
			if (cred == null && !string.IsNullOrEmpty(this.credentialKey))
			{
				cred = CredentialHelper.ReadCredential(this.credentialKey);
			}
			MonadConnectionInfo monadConnectionInfo = null;
			while (monadConnectionInfo == null)
			{
				if (cred == null && this.uiInteractionHandler != null)
				{
					if (WinformsHelper.IsWin7OrLater() && this.orgType == OrganizationType.Cloud)
					{
						CredentialHelper.ForceConnection(this.uri);
					}
					this.uiInteractionHandler.DoActionSynchronizely(delegate(IWin32Window window)
					{
						cred = CredentialHelper.PromptForCredentials((window == null) ? IntPtr.Zero : window.Handle, this.displayName, this.uri, this.GetErrorMessage(), ref this.rememberCredentialChecked);
					});
					if (cred != null && !string.IsNullOrEmpty(this.credentialKey))
					{
						CredentialHelper.RemoveCredential(this.credentialKey);
					}
				}
				if (cred == null)
				{
					throw new OperationCanceledException(Strings.UnableToConnectExchangeForest(this.displayName));
				}
				monadConnectionInfo = tryConnectWithCredential(cred);
				cred = null;
			}
			return monadConnectionInfo;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0007B41E File Offset: 0x0007961E
		private MonadConnectionInfo TryConnectWithDefaultCredential()
		{
			if (this.orgType == OrganizationType.Cloud)
			{
				return this.ConnectTenantWithDefaultCredential();
			}
			return this.ConnectOnPremiseWithDefaultCredential();
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0007B436 File Offset: 0x00079636
		private MonadConnectionInfo TryConnectWithExplicitCredential(PSCredential cred)
		{
			if (this.orgType == OrganizationType.Cloud)
			{
				return this.ConnectTenantWithExplicitCredential(cred);
			}
			if (this.uri.IsHttps())
			{
				return this.ConnectRemoteOnPremiseWithHttps(cred);
			}
			return this.ConnectRemoteOnPremiseWithHttp(cred);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0007B468 File Offset: 0x00079668
		private MonadConnectionInfo ConnectOnPremiseWithDefaultCredential()
		{
			AuthenticationMechanism[] authMechanisms = new AuthenticationMechanism[]
			{
				AuthenticationMechanism.Kerberos
			};
			return this.TryAuthenticationMechanisms(null, authMechanisms);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0007B48C File Offset: 0x0007968C
		private MonadConnectionInfo ConnectTenantWithDefaultCredential()
		{
			AuthenticationMechanism[] authMechanisms = new AuthenticationMechanism[]
			{
				AuthenticationMechanism.NegotiateWithImplicitCredential
			};
			return this.TryAuthenticationMechanisms(null, authMechanisms);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0007B4B0 File Offset: 0x000796B0
		private MonadConnectionInfo ConnectRemoteOnPremiseWithHttp(PSCredential cred)
		{
			AuthenticationMechanism[] authMechanisms = new AuthenticationMechanism[]
			{
				AuthenticationMechanism.Kerberos
			};
			return this.TryAuthenticationMechanisms(cred, authMechanisms);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0007B4D4 File Offset: 0x000796D4
		private MonadConnectionInfo ConnectRemoteOnPremiseWithHttps(PSCredential cred)
		{
			AuthenticationMechanism[] authMechanisms = new AuthenticationMechanism[]
			{
				AuthenticationMechanism.Basic
			};
			return this.TryAuthenticationMechanisms(cred, authMechanisms);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0007B4F8 File Offset: 0x000796F8
		private MonadConnectionInfo ConnectTenantWithExplicitCredential(PSCredential cred)
		{
			AuthenticationMechanism[] array = new AuthenticationMechanism[]
			{
				AuthenticationMechanism.Basic
			};
			AuthenticationMechanism[] array2 = new AuthenticationMechanism[]
			{
				AuthenticationMechanism.Negotiate
			};
			return this.TryAuthenticationMechanisms(cred, cred.IsLiveId() ? array : array2);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0007B532 File Offset: 0x00079732
		private bool CanSkipCertificateCheck()
		{
			return ConnectionRetryDiscoverer.skipCertificateCheckSetting || !this.uri.IsHttps();
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0007B54C File Offset: 0x0007974C
		private MonadConnectionInfo TryAuthenticationMechanisms(PSCredential cred, AuthenticationMechanism[] authMechanisms)
		{
			this.exceptionList.Clear();
			foreach (AuthenticationMechanism authenticationMechanism in authMechanisms)
			{
				try
				{
					bool skipCertificateCheck = this.CanSkipCertificateCheck();
					this.supportedVersionList = MonadRemoteRunspaceFactory.TestConnection(this.uri, "http://schemas.microsoft.com/powershell/Microsoft.Exchange", cred, authenticationMechanism, this.MaxRedirectionCount, skipCertificateCheck);
					return new MonadConnectionInfo(this.uri, cred, "http://schemas.microsoft.com/powershell/Microsoft.Exchange", null, authenticationMechanism, ExchangeRunspaceConfigurationSettings.SerializationLevel.Full, ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC, (this.orgType == OrganizationType.Cloud) ? this.slotVersion : string.Empty, this.MaxRedirectionCount, skipCertificateCheck);
				}
				catch (PSRemotingTransportException value)
				{
					this.exceptionList.Add(authenticationMechanism, value);
				}
				catch (PSRemotingDataStructureException value2)
				{
					this.exceptionList.Add(authenticationMechanism, value2);
				}
			}
			this.supportedVersionList = null;
			return null;
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0007B62C File Offset: 0x0007982C
		private int MaxRedirectionCount
		{
			get
			{
				if (this.orgType != OrganizationType.Cloud)
				{
					return 0;
				}
				return 3;
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0007B63C File Offset: 0x0007983C
		private string GetErrorMessage()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<AuthenticationMechanism, Exception> keyValuePair in this.exceptionList)
			{
				stringBuilder.AppendLine(Strings.TryAuthenticationMechanismFailedMessage(this.uri.ToString(), LocalizedDescriptionAttribute.FromEnum(typeof(AuthenticationMechanism), keyValuePair.Key), keyValuePair.Value.Message));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A9A RID: 2714
		private const int maxRedirectionCountForCloud = 3;

		// Token: 0x04000A9B RID: 2715
		private const int maxRedirectionCountForOnPremise = 0;

		// Token: 0x04000A9C RID: 2716
		private OrganizationType orgType;

		// Token: 0x04000A9D RID: 2717
		private string displayName;

		// Token: 0x04000A9E RID: 2718
		private Uri uri;

		// Token: 0x04000A9F RID: 2719
		private bool logonWithDefaultCredential;

		// Token: 0x04000AA0 RID: 2720
		private string credentialKey;

		// Token: 0x04000AA1 RID: 2721
		private PSCredential proposedCredential;

		// Token: 0x04000AA2 RID: 2722
		private UIInteractionHandler uiInteractionHandler;

		// Token: 0x04000AA3 RID: 2723
		private bool rememberCredentialChecked;

		// Token: 0x04000AA4 RID: 2724
		private SupportedVersionList supportedVersionList;

		// Token: 0x04000AA5 RID: 2725
		private string slotVersion;

		// Token: 0x04000AA6 RID: 2726
		private static bool skipCertificateCheckSetting;

		// Token: 0x04000AA7 RID: 2727
		private Dictionary<AuthenticationMechanism, Exception> exceptionList = new Dictionary<AuthenticationMechanism, Exception>();
	}
}
