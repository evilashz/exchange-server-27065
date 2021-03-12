using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C2C RID: 3116
	[Cmdlet("New", "PushNotificationsVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewPushNotificationsVirtualDirectory : NewExchangeServiceVirtualDirectory<ADPushNotificationsVirtualDirectory>
	{
		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x060075D5 RID: 30165 RVA: 0x001E144F File Offset: 0x001DF64F
		// (set) Token: 0x060075D6 RID: 30166 RVA: 0x001E147A File Offset: 0x001DF67A
		[Parameter(Mandatory = false)]
		public bool LiveIdAuthentication
		{
			get
			{
				return base.Fields["LiveIdFbaAuthentication"] != null && (bool)base.Fields["LiveIdFbaAuthentication"];
			}
			set
			{
				base.Fields["LiveIdFbaAuthentication"] = value;
			}
		}

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x060075D7 RID: 30167 RVA: 0x001E1492 File Offset: 0x001DF692
		// (set) Token: 0x060075D8 RID: 30168 RVA: 0x001E14BD File Offset: 0x001DF6BD
		[Parameter(Mandatory = false)]
		public bool OAuthAuthentication
		{
			get
			{
				return base.Fields["OAuthAuthentication"] != null && (bool)base.Fields["OAuthAuthentication"];
			}
			set
			{
				base.Fields["OAuthAuthentication"] = value;
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x060075D9 RID: 30169 RVA: 0x001E14D5 File Offset: 0x001DF6D5
		protected override string VirtualDirectoryName
		{
			get
			{
				return "PushNotifications";
			}
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x060075DA RID: 30170 RVA: 0x001E14DC File Offset: 0x001DF6DC
		protected override string VirtualDirectoryPath
		{
			get
			{
				return this.vdirPath;
			}
		}

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x060075DB RID: 30171 RVA: 0x001E14E4 File Offset: 0x001DF6E4
		protected override string DefaultApplicationPoolId
		{
			get
			{
				return "MSExchangePushNotificationsAppPool";
			}
		}

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x060075DC RID: 30172 RVA: 0x001E14EB File Offset: 0x001DF6EB
		protected override Uri DefaultInternalUrl
		{
			get
			{
				return NewPushNotificationsVirtualDirectory.PushNotificationsInternalUri;
			}
		}

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x060075DD RID: 30173 RVA: 0x001E14F2 File Offset: 0x001DF6F2
		// (set) Token: 0x060075DE RID: 30174 RVA: 0x001E14FA File Offset: 0x001DF6FA
		internal new string WebSiteName
		{
			get
			{
				return base.WebSiteName;
			}
			private set
			{
				base.WebSiteName = value;
			}
		}

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x060075DF RID: 30175 RVA: 0x001E1503 File Offset: 0x001DF703
		// (set) Token: 0x060075E0 RID: 30176 RVA: 0x001E150B File Offset: 0x001DF70B
		internal new string AppPoolId
		{
			get
			{
				return base.AppPoolId;
			}
			private set
			{
				base.AppPoolId = value;
			}
		}

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x060075E1 RID: 30177 RVA: 0x001E1514 File Offset: 0x001DF714
		// (set) Token: 0x060075E2 RID: 30178 RVA: 0x001E151C File Offset: 0x001DF71C
		internal new string ApplicationRoot
		{
			get
			{
				return base.ApplicationRoot;
			}
			set
			{
				base.ApplicationRoot = value;
			}
		}

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x060075E3 RID: 30179 RVA: 0x001E1525 File Offset: 0x001DF725
		// (set) Token: 0x060075E4 RID: 30180 RVA: 0x001E152D File Offset: 0x001DF72D
		internal new string Path
		{
			get
			{
				return base.Path;
			}
			set
			{
				base.Path = value;
			}
		}

		// Token: 0x1700244C RID: 9292
		// (get) Token: 0x060075E5 RID: 30181 RVA: 0x001E1536 File Offset: 0x001DF736
		// (set) Token: 0x060075E6 RID: 30182 RVA: 0x001E153E File Offset: 0x001DF73E
		internal new ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
		{
			get
			{
				return base.ExtendedProtectionTokenChecking;
			}
			set
			{
				base.ExtendedProtectionTokenChecking = value;
			}
		}

		// Token: 0x1700244D RID: 9293
		// (get) Token: 0x060075E7 RID: 30183 RVA: 0x001E1547 File Offset: 0x001DF747
		// (set) Token: 0x060075E8 RID: 30184 RVA: 0x001E154F File Offset: 0x001DF74F
		internal new MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
		{
			get
			{
				return base.ExtendedProtectionFlags;
			}
			set
			{
				base.ExtendedProtectionFlags = value;
			}
		}

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x060075E9 RID: 30185 RVA: 0x001E1558 File Offset: 0x001DF758
		// (set) Token: 0x060075EA RID: 30186 RVA: 0x001E1560 File Offset: 0x001DF760
		internal new MultiValuedProperty<string> ExtendedProtectionSPNList
		{
			get
			{
				return base.ExtendedProtectionSPNList;
			}
			set
			{
				base.ExtendedProtectionSPNList = value;
			}
		}

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x060075EB RID: 30187 RVA: 0x001E1569 File Offset: 0x001DF769
		// (set) Token: 0x060075EC RID: 30188 RVA: 0x001E1571 File Offset: 0x001DF771
		internal new bool BasicAuthentication
		{
			get
			{
				return base.BasicAuthentication;
			}
			set
			{
				base.BasicAuthentication = value;
			}
		}

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x060075ED RID: 30189 RVA: 0x001E157A File Offset: 0x001DF77A
		// (set) Token: 0x060075EE RID: 30190 RVA: 0x001E1582 File Offset: 0x001DF782
		internal new bool DigestAuthentication
		{
			get
			{
				return base.DigestAuthentication;
			}
			set
			{
				base.DigestAuthentication = value;
			}
		}

		// Token: 0x17002451 RID: 9297
		// (get) Token: 0x060075EF RID: 30191 RVA: 0x001E158B File Offset: 0x001DF78B
		// (set) Token: 0x060075F0 RID: 30192 RVA: 0x001E1593 File Offset: 0x001DF793
		internal new bool WindowsAuthentication
		{
			get
			{
				return base.WindowsAuthentication;
			}
			set
			{
				base.WindowsAuthentication = value;
			}
		}

		// Token: 0x17002452 RID: 9298
		// (get) Token: 0x060075F1 RID: 30193 RVA: 0x001E159C File Offset: 0x001DF79C
		private bool IsBackEnd
		{
			get
			{
				return base.Role == VirtualDirectoryRole.Mailbox;
			}
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x001E15A8 File Offset: 0x001DF7A8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			this.AppPoolId = "MSExchangePushNotificationsAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			if (this.IsBackEnd)
			{
				this.WebSiteName = "Exchange Back End";
			}
			else
			{
				this.vdirPath = "FrontEnd\\HttpProxy\\PushNotifications";
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x060075F3 RID: 30195 RVA: 0x001E15F7 File Offset: 0x001DF7F7
		protected override void AddCustomVDirProperties(ArrayList customProperties)
		{
			base.AddCustomVDirProperties(customProperties);
			customProperties.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128));
		}

		// Token: 0x060075F4 RID: 30196 RVA: 0x001E161C File Offset: 0x001DF81C
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.WindowsAuthentication = new bool?(false);
			virtualDirectory.BasicAuthentication = new bool?(false);
			virtualDirectory.DigestAuthentication = new bool?(false);
			virtualDirectory.LiveIdBasicAuthentication = new bool?(false);
			virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
			virtualDirectory.WSSecurityAuthentication = new bool?(false);
			virtualDirectory.OAuthAuthentication = new bool?(this.OAuthAuthentication);
			ADPushNotificationsVirtualDirectory adpushNotificationsVirtualDirectory = (ADPushNotificationsVirtualDirectory)virtualDirectory;
			adpushNotificationsVirtualDirectory.LiveIdAuthentication = this.LiveIdAuthentication;
			if (this.IsBackEnd)
			{
				virtualDirectory.WindowsAuthentication = new bool?(true);
			}
		}

		// Token: 0x060075F5 RID: 30197 RVA: 0x001E16AC File Offset: 0x001DF8AC
		protected override void InternalProcessMetabase()
		{
			base.InternalProcessMetabase();
			if (this.IsBackEnd)
			{
				ExchangeServiceVDirHelper.CheckAndUpdateLocalhostNetPipeBindingsIfNecessary(this.DataObject);
				try
				{
					ExchangeServiceVDirHelper.RunAppcmd(string.Format("set app \"{0}/{1}\" /enabledProtocols:http,net.pipe", this.WebSiteName, this.VirtualDirectoryName));
				}
				catch (AppcmdException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.ServerOperation, this.DataObject.Identity);
				}
			}
		}

		// Token: 0x060075F6 RID: 30198 RVA: 0x001E171C File Offset: 0x001DF91C
		protected override void InternalProcessComplete()
		{
			base.InternalProcessComplete();
			if (this.IsBackEnd)
			{
				ExchangeServiceVDirHelper.ForceAnonymous(this.DataObject.MetabasePath);
			}
		}

		// Token: 0x04003B9D RID: 15261
		private const string PushNotificationsBackendVDirPath = "ClientAccess\\PushNotifications";

		// Token: 0x04003B9E RID: 15262
		private const string PushNotificationsCafeVDirPath = "FrontEnd\\HttpProxy\\PushNotifications";

		// Token: 0x04003B9F RID: 15263
		private const string LiveIdAuthenticationFieldName = "LiveIdFbaAuthentication";

		// Token: 0x04003BA0 RID: 15264
		private const string OAuthAuthenticationFieldName = "OAuthAuthentication";

		// Token: 0x04003BA1 RID: 15265
		private static readonly Uri PushNotificationsInternalUri = new Uri(string.Format("https://{0}/{1}/{2}", ComputerInformation.DnsFullyQualifiedDomainName, "PushNotifications", ""));

		// Token: 0x04003BA2 RID: 15266
		private string vdirPath = "ClientAccess\\PushNotifications";
	}
}
