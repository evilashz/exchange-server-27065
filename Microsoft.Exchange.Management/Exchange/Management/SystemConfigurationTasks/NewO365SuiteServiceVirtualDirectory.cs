using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C26 RID: 3110
	[Cmdlet("New", "O365SuiteServiceVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewO365SuiteServiceVirtualDirectory : NewExchangeServiceVirtualDirectory<ADO365SuiteServiceVirtualDirectory>
	{
		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x06007555 RID: 30037 RVA: 0x001DF737 File Offset: 0x001DD937
		// (set) Token: 0x06007556 RID: 30038 RVA: 0x001DF762 File Offset: 0x001DD962
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

		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x06007557 RID: 30039 RVA: 0x001DF77A File Offset: 0x001DD97A
		// (set) Token: 0x06007558 RID: 30040 RVA: 0x001DF7A5 File Offset: 0x001DD9A5
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

		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x06007559 RID: 30041 RVA: 0x001DF7BD File Offset: 0x001DD9BD
		protected override string VirtualDirectoryName
		{
			get
			{
				return "O365SuiteService";
			}
		}

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x0600755A RID: 30042 RVA: 0x001DF7C4 File Offset: 0x001DD9C4
		protected override string VirtualDirectoryPath
		{
			get
			{
				return this.vdirPath;
			}
		}

		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x0600755B RID: 30043 RVA: 0x001DF7CC File Offset: 0x001DD9CC
		protected override string DefaultApplicationPoolId
		{
			get
			{
				return "MSExchangeO365SuiteServiceAppPool";
			}
		}

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x0600755C RID: 30044 RVA: 0x001DF7D3 File Offset: 0x001DD9D3
		protected override Uri DefaultInternalUrl
		{
			get
			{
				return NewO365SuiteServiceVirtualDirectory.O365SuiteServiceInternalUri;
			}
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x0600755D RID: 30045 RVA: 0x001DF7DA File Offset: 0x001DD9DA
		// (set) Token: 0x0600755E RID: 30046 RVA: 0x001DF7E2 File Offset: 0x001DD9E2
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

		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x0600755F RID: 30047 RVA: 0x001DF7EB File Offset: 0x001DD9EB
		// (set) Token: 0x06007560 RID: 30048 RVA: 0x001DF7F3 File Offset: 0x001DD9F3
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

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x06007561 RID: 30049 RVA: 0x001DF7FC File Offset: 0x001DD9FC
		// (set) Token: 0x06007562 RID: 30050 RVA: 0x001DF804 File Offset: 0x001DDA04
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

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x06007563 RID: 30051 RVA: 0x001DF80D File Offset: 0x001DDA0D
		// (set) Token: 0x06007564 RID: 30052 RVA: 0x001DF815 File Offset: 0x001DDA15
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

		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x06007565 RID: 30053 RVA: 0x001DF81E File Offset: 0x001DDA1E
		// (set) Token: 0x06007566 RID: 30054 RVA: 0x001DF826 File Offset: 0x001DDA26
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

		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06007567 RID: 30055 RVA: 0x001DF82F File Offset: 0x001DDA2F
		// (set) Token: 0x06007568 RID: 30056 RVA: 0x001DF837 File Offset: 0x001DDA37
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

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06007569 RID: 30057 RVA: 0x001DF840 File Offset: 0x001DDA40
		// (set) Token: 0x0600756A RID: 30058 RVA: 0x001DF848 File Offset: 0x001DDA48
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

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x0600756B RID: 30059 RVA: 0x001DF851 File Offset: 0x001DDA51
		// (set) Token: 0x0600756C RID: 30060 RVA: 0x001DF859 File Offset: 0x001DDA59
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

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x0600756D RID: 30061 RVA: 0x001DF862 File Offset: 0x001DDA62
		// (set) Token: 0x0600756E RID: 30062 RVA: 0x001DF86A File Offset: 0x001DDA6A
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

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x0600756F RID: 30063 RVA: 0x001DF873 File Offset: 0x001DDA73
		// (set) Token: 0x06007570 RID: 30064 RVA: 0x001DF87B File Offset: 0x001DDA7B
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

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x06007571 RID: 30065 RVA: 0x001DF884 File Offset: 0x001DDA84
		private bool IsBackEnd
		{
			get
			{
				return base.Role == VirtualDirectoryRole.Mailbox;
			}
		}

		// Token: 0x06007572 RID: 30066 RVA: 0x001DF890 File Offset: 0x001DDA90
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.Name = "O365SuiteService";
			this.AppPoolId = "MSExchangeO365SuiteServiceAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			if (this.IsBackEnd)
			{
				this.WebSiteName = "Exchange Back End";
			}
			else
			{
				this.vdirPath = "FrontEnd\\HttpProxy\\O365SuiteService";
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06007573 RID: 30067 RVA: 0x001DF8EA File Offset: 0x001DDAEA
		protected override void AddCustomVDirProperties(ArrayList customProperties)
		{
			base.AddCustomVDirProperties(customProperties);
			customProperties.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128));
		}

		// Token: 0x06007574 RID: 30068 RVA: 0x001DF910 File Offset: 0x001DDB10
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.WindowsAuthentication = new bool?(false);
			virtualDirectory.BasicAuthentication = new bool?(false);
			virtualDirectory.DigestAuthentication = new bool?(false);
			virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
			virtualDirectory.WSSecurityAuthentication = new bool?(false);
			virtualDirectory.OAuthAuthentication = new bool?(this.OAuthAuthentication);
			ADO365SuiteServiceVirtualDirectory ado365SuiteServiceVirtualDirectory = (ADO365SuiteServiceVirtualDirectory)virtualDirectory;
			ado365SuiteServiceVirtualDirectory.LiveIdAuthentication = this.LiveIdAuthentication;
			if (this.IsBackEnd)
			{
				virtualDirectory.WindowsAuthentication = new bool?(true);
			}
		}

		// Token: 0x04003B7C RID: 15228
		private const string BackendVDirPath = "ClientAccess\\O365SuiteService";

		// Token: 0x04003B7D RID: 15229
		private const string CafeVDirPath = "FrontEnd\\HttpProxy\\O365SuiteService";

		// Token: 0x04003B7E RID: 15230
		private const string ApplicationPoolId = "MSExchangeO365SuiteServiceAppPool";

		// Token: 0x04003B7F RID: 15231
		private const string LiveIdAuthenticationFieldName = "LiveIdFbaAuthentication";

		// Token: 0x04003B80 RID: 15232
		private const string OAuthAuthenticationFieldName = "OAuthAuthentication";

		// Token: 0x04003B81 RID: 15233
		private static readonly Uri O365SuiteServiceInternalUri = new Uri(string.Format("https://{0}/{1}/{2}", ComputerInformation.DnsFullyQualifiedDomainName, "O365SuiteService", ""));

		// Token: 0x04003B82 RID: 15234
		private string vdirPath = "ClientAccess\\O365SuiteService";
	}
}
