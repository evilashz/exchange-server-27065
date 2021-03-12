using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x0200004D RID: 77
	[Cmdlet("Install", "CannedPushNotificationApp")]
	public sealed class InstallCannedPushNotificationApp : DataAccessTask<PushNotificationApp>
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000CA1A File Offset: 0x0000AC1A
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000CA31 File Offset: 0x0000AC31
		[Parameter(Mandatory = false)]
		public Hashtable AuthenticationKeys
		{
			get
			{
				return (Hashtable)base.Fields["AuthenticationKeys"];
			}
			set
			{
				base.Fields["AuthenticationKeys"] = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000CA44 File Offset: 0x0000AC44
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000CA65 File Offset: 0x0000AC65
		[Parameter(Mandatory = false)]
		public PushNotificationSetupEnvironment Environment
		{
			get
			{
				return (PushNotificationSetupEnvironment)(base.Fields["Environment"] ?? PushNotificationSetupEnvironment.None);
			}
			set
			{
				base.Fields["Environment"] = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000CA94 File Offset: 0x0000AC94
		[Parameter(Mandatory = false)]
		public string AcsUser
		{
			get
			{
				return (string)base.Fields["AcsUser"];
			}
			set
			{
				base.Fields["AcsUser"] = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		[Parameter(Mandatory = false)]
		public bool IsDedicated
		{
			get
			{
				return (bool)(base.Fields["IsDedicated"] ?? false);
			}
			set
			{
				base.Fields["IsDedicated"] = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		private PushNotificationSetupConfig PushNotificationSetupConfiguration { get; set; }

		// Token: 0x060002E5 RID: 741 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			this.PushNotificationSetupConfiguration = PushNotificationCannedSet.PushNotificationSetupEnvironmentConfig[this.Environment];
			foreach (string name in this.ResolveRetiredApps())
			{
				PushNotificationApp pushNotificationApp = this.FindPushNotificationApp(name);
				if (pushNotificationApp != null)
				{
					this.RemovePushNotificationApp(pushNotificationApp);
				}
			}
			foreach (PushNotificationApp pushNotificationApp2 in this.ResolveInstallableBySetupApps())
			{
				PushNotificationApp pushNotificationApp3 = this.FindPushNotificationApp(pushNotificationApp2.Name);
				if (pushNotificationApp3 != null && pushNotificationApp3.Platform != pushNotificationApp2.Platform)
				{
					base.WriteVerbose(Strings.PushNotificationAppPlatformMismatch(pushNotificationApp3.Name, pushNotificationApp3.ToFullString()));
					this.RemovePushNotificationApp(pushNotificationApp3);
					pushNotificationApp3 = null;
				}
				if (pushNotificationApp3 == null)
				{
					base.WriteVerbose(Strings.PushNotificationAppNotFound(pushNotificationApp2.Name));
					pushNotificationApp3 = new PushNotificationApp();
					pushNotificationApp3.CopyChangesFrom(pushNotificationApp2);
					this.SavePushNotificationApp(pushNotificationApp3);
				}
				else
				{
					base.WriteVerbose(Strings.PushNotificationAppFound(pushNotificationApp2.Name, pushNotificationApp3.ToFullString()));
					SecureString authKey;
					if (this.TryGetAuthenticationKeyFromParameters(pushNotificationApp3, out authKey))
					{
						this.SetAuthenticationKey(pushNotificationApp3, authKey);
						this.UpdatePushNotificationApp(pushNotificationApp3);
					}
					else if (PushNotificationPlatform.APNS == pushNotificationApp3.Platform && (pushNotificationApp3.AuthenticationKey == null || !pushNotificationApp3.AuthenticationKey.Equals(pushNotificationApp2.AuthenticationKey)))
					{
						this.CopyAuthenticationKeys(pushNotificationApp2, pushNotificationApp3);
						this.UpdatePushNotificationApp(pushNotificationApp3);
					}
				}
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CC46 File Offset: 0x0000AE46
		protected override IConfigDataProvider CreateSession()
		{
			return base.ReadWriteRootOrgGlobalConfigSession;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000CC4E File Offset: 0x0000AE4E
		private PushNotificationApp[] ResolveInstallableBySetupApps()
		{
			if (!this.IsDedicated)
			{
				return this.PushNotificationSetupConfiguration.InstallableBySetup;
			}
			return this.PushNotificationSetupConfiguration.InstallableBySetupDedicated;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000CC6F File Offset: 0x0000AE6F
		private string[] ResolveRetiredApps()
		{
			if (!this.IsDedicated)
			{
				return this.PushNotificationSetupConfiguration.RetiredBySetup;
			}
			return this.PushNotificationSetupConfiguration.RetiredBySetupDedicated;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000CC90 File Offset: 0x0000AE90
		private void SavePushNotificationApp(PushNotificationApp app)
		{
			app.SetId(base.ReadWriteRootOrgGlobalConfigSession, app.Name);
			SecureString authKey;
			if (this.TryGetAuthenticationKeyFromParameters(app, out authKey))
			{
				this.SetAuthenticationKey(app, authKey);
			}
			switch (app.Platform)
			{
			case PushNotificationPlatform.Azure:
				this.PrepareAzureApp(app);
				break;
			case PushNotificationPlatform.AzureHubCreation:
				this.PrepareHubCreationApp(app);
				break;
			}
			this.UpdatePushNotificationApp(app);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
		private void UpdatePushNotificationApp(PushNotificationApp app)
		{
			base.ReadWriteRootOrgGlobalConfigSession.Save(app);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000CD00 File Offset: 0x0000AF00
		private void RemovePushNotificationApp(PushNotificationApp app)
		{
			base.ReadWriteRootOrgGlobalConfigSession.Delete(app);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		private PushNotificationApp FindPushNotificationApp(string name)
		{
			return new PushNotificationAppIdParameter(name).GetObjects<PushNotificationApp>(null, base.ReadWriteRootOrgGlobalConfigSession).FirstOrDefault<PushNotificationApp>();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000CD28 File Offset: 0x0000AF28
		private bool TryGetAuthenticationKeyFromParameters(PushNotificationApp app, out SecureString authenticationKey)
		{
			authenticationKey = null;
			if (app.Platform == PushNotificationPlatform.Azure)
			{
				return false;
			}
			if (this.AuthenticationKeys != null && this.AuthenticationKeys.ContainsKey(app.Name))
			{
				authenticationKey = (this.AuthenticationKeys[app.Name] as SecureString);
			}
			return authenticationKey != null;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000CD80 File Offset: 0x0000AF80
		private void SetAuthenticationKey(PushNotificationApp app, SecureString authKey)
		{
			string authenticationKey = string.Empty;
			try
			{
				authenticationKey = this.dkm.Encrypt(authKey.AsUnsecureString());
			}
			catch (PushNotificationConfigurationException ex)
			{
				base.WriteWarning(Strings.PushNotificationAppSecretEncryptionWarning(app.Name), (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
			}
			app.AuthenticationKey = authenticationKey;
			app.IsAuthenticationKeyEncrypted = new bool?(true);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		private void CopyAuthenticationKeys(PushNotificationApp srcApp, PushNotificationApp destApp)
		{
			destApp.AuthenticationKey = srcApp.AuthenticationKey;
			destApp.AuthenticationKeyFallback = srcApp.AuthenticationKeyFallback;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000CE18 File Offset: 0x0000B018
		private void PrepareAzureApp(PushNotificationApp app)
		{
			string partitionName;
			app.IsDefaultPartitionName = new bool?(!this.ResolvePartitionName(app, out partitionName));
			app.PartitionName = partitionName;
			app.UriTemplate = string.Format("https://{{0}}-{{1}}.servicebus.windows.net/{0}/{{2}}/{{3}}", this.PushNotificationSetupConfiguration.AcsHierarchyNode);
			this.SetAuthenticationKey(app, AzureSasKey.GenerateRandomKey(AzureSasKey.ClaimType.Send | AzureSasKey.ClaimType.Listen | AzureSasKey.ClaimType.Manage, null).KeyValue);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CE74 File Offset: 0x0000B074
		private void PrepareHubCreationApp(PushNotificationApp app)
		{
			app.AuthenticationId = this.AcsUser;
			app.UriTemplate = string.Format("https://{{0}}-{{1}}.servicebus.windows.net/{0}/{{2}}{{3}}", this.PushNotificationSetupConfiguration.AcsHierarchyNode);
			app.Url = "https://{0}-{1}-sb.accesscontrol.windows.net/";
			app.SecondaryUrl = string.Format("https://{{0}}-{{1}}.servicebus.windows.net/{0}", this.PushNotificationSetupConfiguration.AcsHierarchyNode);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		private bool ResolvePartitionName(PushNotificationApp app, out string partitionName)
		{
			bool flag = false;
			partitionName = null;
			string text = this.ResolveCurrentForestName();
			Uri url = new Uri(string.Format("https://{0}-{1}.servicebus.windows.net", AzureUriTemplate.ConvertAppIdToValidNamespace(app.Name), text));
			using (HttpClient httpClient = new HttpClient())
			{
				DownloadResult downloadResult = httpClient.EndDownload(httpClient.BeginDownload(url, new HttpSessionConfig(2000)
				{
					Method = "GET"
				}, null, null));
				flag = downloadResult.IsSucceeded;
				if (downloadResult.ResponseStream != null)
				{
					downloadResult.ResponseStream.Dispose();
				}
			}
			if (flag)
			{
				partitionName = text;
				base.WriteVerbose(Strings.PushNotificationSucceededToValidatePartition(app.Name, partitionName));
			}
			else
			{
				if (this.PushNotificationSetupConfiguration.FallbackPartitionMapping.ContainsKey(app.Name))
				{
					partitionName = this.PushNotificationSetupConfiguration.FallbackPartitionMapping[app.Name];
				}
				this.WriteWarning(Strings.PushNotificationFailedToValidatePartitionWarning(app.Name, text, partitionName));
			}
			if (string.IsNullOrWhiteSpace(partitionName))
			{
				base.WriteError(new CannotResolveFallbackPartition(app.Name, text), ExchangeErrorCategory.ServerOperation, null);
			}
			return flag;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		private string ResolveCurrentForestName()
		{
			return base.ReadWriteRootOrgGlobalConfigSession.GetRootDomainNamingContext().Name;
		}

		// Token: 0x040000C1 RID: 193
		private const string NamespaceCheckTemplate = "https://{0}-{1}.servicebus.windows.net";

		// Token: 0x040000C2 RID: 194
		private const string UriTemplateModel = "https://{{0}}-{{1}}.servicebus.windows.net/{0}/{{2}}/{{3}}";

		// Token: 0x040000C3 RID: 195
		private const string AcsScopeUriTemplateModel = "https://{{0}}-{{1}}.servicebus.windows.net/{0}";

		// Token: 0x040000C4 RID: 196
		private const string HubCreationUriTemplateModel = "https://{{0}}-{{1}}.servicebus.windows.net/{0}/{{2}}{{3}}";

		// Token: 0x040000C5 RID: 197
		private PushNotificationDataProtector dkm = new PushNotificationDataProtector(null);
	}
}
