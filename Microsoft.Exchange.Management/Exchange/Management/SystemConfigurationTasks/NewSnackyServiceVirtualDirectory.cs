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
	// Token: 0x02000C2E RID: 3118
	[Cmdlet("New", "SnackyServiceVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewSnackyServiceVirtualDirectory : NewExchangeServiceVirtualDirectory<ADSnackyServiceVirtualDirectory>
	{
		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x060075FC RID: 30204 RVA: 0x001E1847 File Offset: 0x001DFA47
		// (set) Token: 0x060075FD RID: 30205 RVA: 0x001E1872 File Offset: 0x001DFA72
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

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x060075FE RID: 30206 RVA: 0x001E188A File Offset: 0x001DFA8A
		protected override string VirtualDirectoryName
		{
			get
			{
				return "SnackyService";
			}
		}

		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x060075FF RID: 30207 RVA: 0x001E1891 File Offset: 0x001DFA91
		protected override string VirtualDirectoryPath
		{
			get
			{
				return this.vdirPath;
			}
		}

		// Token: 0x17002457 RID: 9303
		// (get) Token: 0x06007600 RID: 30208 RVA: 0x001E1899 File Offset: 0x001DFA99
		protected override string DefaultApplicationPoolId
		{
			get
			{
				return "MSExchangeSnackyServiceAppPool";
			}
		}

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x06007601 RID: 30209 RVA: 0x001E18A0 File Offset: 0x001DFAA0
		protected override Uri DefaultInternalUrl
		{
			get
			{
				return NewSnackyServiceVirtualDirectory.SnackyServiceInternalUri;
			}
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x06007602 RID: 30210 RVA: 0x001E18A7 File Offset: 0x001DFAA7
		// (set) Token: 0x06007603 RID: 30211 RVA: 0x001E18AF File Offset: 0x001DFAAF
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

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x06007604 RID: 30212 RVA: 0x001E18B8 File Offset: 0x001DFAB8
		// (set) Token: 0x06007605 RID: 30213 RVA: 0x001E18C0 File Offset: 0x001DFAC0
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

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x06007606 RID: 30214 RVA: 0x001E18C9 File Offset: 0x001DFAC9
		// (set) Token: 0x06007607 RID: 30215 RVA: 0x001E18D1 File Offset: 0x001DFAD1
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

		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x06007608 RID: 30216 RVA: 0x001E18DA File Offset: 0x001DFADA
		// (set) Token: 0x06007609 RID: 30217 RVA: 0x001E18E2 File Offset: 0x001DFAE2
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

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x0600760A RID: 30218 RVA: 0x001E18EB File Offset: 0x001DFAEB
		// (set) Token: 0x0600760B RID: 30219 RVA: 0x001E18F3 File Offset: 0x001DFAF3
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

		// Token: 0x1700245E RID: 9310
		// (get) Token: 0x0600760C RID: 30220 RVA: 0x001E18FC File Offset: 0x001DFAFC
		// (set) Token: 0x0600760D RID: 30221 RVA: 0x001E1904 File Offset: 0x001DFB04
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

		// Token: 0x1700245F RID: 9311
		// (get) Token: 0x0600760E RID: 30222 RVA: 0x001E190D File Offset: 0x001DFB0D
		// (set) Token: 0x0600760F RID: 30223 RVA: 0x001E1915 File Offset: 0x001DFB15
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

		// Token: 0x17002460 RID: 9312
		// (get) Token: 0x06007610 RID: 30224 RVA: 0x001E191E File Offset: 0x001DFB1E
		// (set) Token: 0x06007611 RID: 30225 RVA: 0x001E1926 File Offset: 0x001DFB26
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

		// Token: 0x17002461 RID: 9313
		// (get) Token: 0x06007612 RID: 30226 RVA: 0x001E192F File Offset: 0x001DFB2F
		// (set) Token: 0x06007613 RID: 30227 RVA: 0x001E1937 File Offset: 0x001DFB37
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

		// Token: 0x17002462 RID: 9314
		// (get) Token: 0x06007614 RID: 30228 RVA: 0x001E1940 File Offset: 0x001DFB40
		// (set) Token: 0x06007615 RID: 30229 RVA: 0x001E1948 File Offset: 0x001DFB48
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

		// Token: 0x17002463 RID: 9315
		// (get) Token: 0x06007616 RID: 30230 RVA: 0x001E1951 File Offset: 0x001DFB51
		private bool IsBackEnd
		{
			get
			{
				return base.Role == VirtualDirectoryRole.Mailbox;
			}
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x001E195C File Offset: 0x001DFB5C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.Name = "SnackyService";
			this.AppPoolId = "MSExchangeSnackyServiceAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			if (this.IsBackEnd)
			{
				this.WebSiteName = "Exchange Back End";
			}
			else
			{
				this.vdirPath = "FrontEnd\\HttpProxy\\SnackyService";
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x001E19B6 File Offset: 0x001DFBB6
		protected override void AddCustomVDirProperties(ArrayList customProperties)
		{
			base.AddCustomVDirProperties(customProperties);
			customProperties.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128));
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x001E19DC File Offset: 0x001DFBDC
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.WindowsAuthentication = new bool?(false);
			virtualDirectory.BasicAuthentication = new bool?(false);
			virtualDirectory.DigestAuthentication = new bool?(false);
			virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
			virtualDirectory.WSSecurityAuthentication = new bool?(false);
			ADSnackyServiceVirtualDirectory adsnackyServiceVirtualDirectory = (ADSnackyServiceVirtualDirectory)virtualDirectory;
			virtualDirectory.BasicAuthentication = new bool?(true);
			adsnackyServiceVirtualDirectory.LiveIdAuthentication = this.LiveIdAuthentication;
			if (this.IsBackEnd)
			{
				virtualDirectory.WindowsAuthentication = new bool?(true);
				virtualDirectory.BasicAuthentication = new bool?(true);
			}
		}

		// Token: 0x04003BA3 RID: 15267
		private const string BackendVDirPath = "ClientAccess\\SnackyService";

		// Token: 0x04003BA4 RID: 15268
		private const string CafeVDirPath = "FrontEnd\\HttpProxy\\SnackyService";

		// Token: 0x04003BA5 RID: 15269
		private const string ApplicationPoolId = "MSExchangeSnackyServiceAppPool";

		// Token: 0x04003BA6 RID: 15270
		private const string LiveIdAuthenticationFieldName = "LiveIdFbaAuthentication";

		// Token: 0x04003BA7 RID: 15271
		private static readonly Uri SnackyServiceInternalUri = new Uri(string.Format("https://{0}/{1}/{2}", ComputerInformation.DnsFullyQualifiedDomainName, "SnackyService", ""));

		// Token: 0x04003BA8 RID: 15272
		private string vdirPath = "ClientAccess\\SnackyService";
	}
}
