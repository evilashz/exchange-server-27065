using System;
using System.Collections;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C2F RID: 3119
	[Cmdlet("New", "WebServicesVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewWebServicesVirtualDirectory : NewExchangeServiceVirtualDirectory<ADWebServicesVirtualDirectory>
	{
		// Token: 0x17002464 RID: 9316
		// (get) Token: 0x0600761C RID: 30236 RVA: 0x001E1A91 File Offset: 0x001DFC91
		// (set) Token: 0x0600761D RID: 30237 RVA: 0x001E1A9E File Offset: 0x001DFC9E
		[Parameter]
		public Uri InternalNLBBypassUrl
		{
			get
			{
				return this.DataObject.InternalNLBBypassUrl;
			}
			set
			{
				this.DataObject.InternalNLBBypassUrl = value;
			}
		}

		// Token: 0x17002465 RID: 9317
		// (get) Token: 0x0600761E RID: 30238 RVA: 0x001E1AAC File Offset: 0x001DFCAC
		// (set) Token: 0x0600761F RID: 30239 RVA: 0x001E1AB9 File Offset: 0x001DFCB9
		[Parameter]
		public GzipLevel GzipLevel
		{
			get
			{
				return this.DataObject.GzipLevel;
			}
			set
			{
				this.DataObject.GzipLevel = value;
			}
		}

		// Token: 0x17002466 RID: 9318
		// (get) Token: 0x06007620 RID: 30240 RVA: 0x001E1AC7 File Offset: 0x001DFCC7
		// (set) Token: 0x06007621 RID: 30241 RVA: 0x001E1ADE File Offset: 0x001DFCDE
		[Parameter(Mandatory = false)]
		public string AppPoolIdForManagement
		{
			get
			{
				return (string)base.Fields["AppPoolIdForManagement"];
			}
			set
			{
				base.Fields["AppPoolIdForManagement"] = value;
			}
		}

		// Token: 0x17002467 RID: 9319
		// (get) Token: 0x06007622 RID: 30242 RVA: 0x001E1AF1 File Offset: 0x001DFCF1
		// (set) Token: 0x06007623 RID: 30243 RVA: 0x001E1AF9 File Offset: 0x001DFCF9
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17002468 RID: 9320
		// (get) Token: 0x06007624 RID: 30244 RVA: 0x001E1B02 File Offset: 0x001DFD02
		// (set) Token: 0x06007625 RID: 30245 RVA: 0x001E1B2D File Offset: 0x001DFD2D
		[Parameter(Mandatory = false)]
		public bool WSSecurityAuthentication
		{
			get
			{
				return base.Fields["WSSecurityAuthentication"] != null && (bool)base.Fields["WSSecurityAuthentication"];
			}
			set
			{
				base.Fields["WSSecurityAuthentication"] = value;
			}
		}

		// Token: 0x17002469 RID: 9321
		// (get) Token: 0x06007626 RID: 30246 RVA: 0x001E1B45 File Offset: 0x001DFD45
		// (set) Token: 0x06007627 RID: 30247 RVA: 0x001E1B70 File Offset: 0x001DFD70
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

		// Token: 0x1700246A RID: 9322
		// (get) Token: 0x06007628 RID: 30248 RVA: 0x001E1B88 File Offset: 0x001DFD88
		// (set) Token: 0x06007629 RID: 30249 RVA: 0x001E1B95 File Offset: 0x001DFD95
		[Parameter(Mandatory = false)]
		public bool MRSProxyEnabled
		{
			get
			{
				return this.DataObject.MRSProxyEnabled;
			}
			set
			{
				this.DataObject.MRSProxyEnabled = value;
			}
		}

		// Token: 0x1700246B RID: 9323
		// (get) Token: 0x0600762A RID: 30250 RVA: 0x001E1BA3 File Offset: 0x001DFDA3
		protected override string VirtualDirectoryName
		{
			get
			{
				return "EWS";
			}
		}

		// Token: 0x1700246C RID: 9324
		// (get) Token: 0x0600762B RID: 30251 RVA: 0x001E1BAA File Offset: 0x001DFDAA
		protected override string VirtualDirectoryPath
		{
			get
			{
				if (base.Role != VirtualDirectoryRole.ClientAccess)
				{
					return "ClientAccess\\exchweb\\EWS";
				}
				return "FrontEnd\\HttpProxy\\EWS";
			}
		}

		// Token: 0x1700246D RID: 9325
		// (get) Token: 0x0600762C RID: 30252 RVA: 0x001E1BBF File Offset: 0x001DFDBF
		protected override string DefaultApplicationPoolId
		{
			get
			{
				return "MSExchangeServicesAppPool";
			}
		}

		// Token: 0x1700246E RID: 9326
		// (get) Token: 0x0600762D RID: 30253 RVA: 0x001E1BC6 File Offset: 0x001DFDC6
		protected override Uri DefaultInternalUrl
		{
			get
			{
				return new Uri(string.Format("https://{0}/EWS/Exchange.asmx", ComputerInformation.DnsFullyQualifiedDomainName));
			}
		}

		// Token: 0x0600762E RID: 30254 RVA: 0x001E1BDC File Offset: 0x001DFDDC
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.WindowsAuthentication = new bool?(true);
			virtualDirectory.WSSecurityAuthentication = new bool?(true);
			virtualDirectory.OAuthAuthentication = new bool?(base.Role == VirtualDirectoryRole.ClientAccess);
			virtualDirectory.BasicAuthentication = new bool?(false);
			virtualDirectory.DigestAuthentication = new bool?(false);
			virtualDirectory.LiveIdBasicAuthentication = new bool?(false);
			virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
		}

		// Token: 0x1700246F RID: 9327
		// (get) Token: 0x0600762F RID: 30255 RVA: 0x001E1C45 File Offset: 0x001DFE45
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewWebServicesVirtualDirectory(base.Server.ToString());
			}
		}

		// Token: 0x17002470 RID: 9328
		// (get) Token: 0x06007630 RID: 30256 RVA: 0x001E1C58 File Offset: 0x001DFE58
		protected override ListDictionary ChildVirtualDirectories
		{
			get
			{
				ListDictionary listDictionary = new ListDictionary();
				if (base.Role == VirtualDirectoryRole.Mailbox && Directory.Exists(System.IO.Path.Combine(base.Path, "bin")))
				{
					listDictionary.Add("bin", new ArrayList
					{
						new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.NoAccess),
						new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.NoneSet),
						new MetabaseProperty("AppPoolId", base.AppPoolId)
					});
				}
				return listDictionary;
			}
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x001E1CE4 File Offset: 0x001DFEE4
		protected override void InternalValidate()
		{
			if (!base.Fields.IsModified("Path"))
			{
				base.Path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, this.VirtualDirectoryPath);
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			base.InternalValidateBasicLiveIdBasic();
			if (string.Empty.Equals(this.AppPoolIdForManagement))
			{
				base.WriteError(new ArgumentException(Strings.ErrorAppPoolIdCannotBeEmpty, "AppPoolIdForManagement"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				string path = System.IO.Path.Combine(base.Path, "Nego2");
				if (!Directory.Exists(path))
				{
					base.WriteError(new ArgumentException(Strings.ErrorDirectoryManagementWebServiceNotFound(path), "Path"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
			}
		}

		// Token: 0x06007632 RID: 30258 RVA: 0x001E1DAE File Offset: 0x001DFFAE
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.Role == VirtualDirectoryRole.Mailbox && this.InternalNLBBypassUrl == null)
			{
				this.InternalNLBBypassUrl = new Uri(string.Format("https://{0}:444/EWS/Exchange.asmx", ComputerInformation.DnsFullyQualifiedDomainName));
			}
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
		}

		// Token: 0x06007633 RID: 30259 RVA: 0x001E1DEE File Offset: 0x001DFFEE
		protected override bool InternalProcessStartWork()
		{
			this.DataObject.GzipLevel = GzipLevel.High;
			return true;
		}

		// Token: 0x06007634 RID: 30260 RVA: 0x001E1E00 File Offset: 0x001E0000
		protected override void InternalProcessMetabase()
		{
			base.InternalProcessMetabase();
			if (Environment.OSVersion.Version.Major < 6)
			{
				return;
			}
			string path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "system32");
			string text = System.IO.Path.Combine(path, "inetsrv");
			string text2 = System.IO.Path.Combine(text, "appcmd.exe");
			if (!File.Exists(text2))
			{
				base.WriteError(new InvalidOperationException(Strings.StampExistingResponsePassThroughOnVirtualDirectoryFailure(this.DataObject.Server.Name, this.DataObject.MetabasePath, 0, "appcmd.exe doesn't exist in the given path " + text2)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			string text3 = "set config \"" + base.WebSiteName + "/EWS/\" -section:system.webServer/httpErrors -existingResponse:PassThrough -commit:apphost";
			TaskLogger.Trace("Stamping the virtual directory {0} with existingResponse = PassThrough", new object[]
			{
				this.DataObject.MetabasePath
			});
			TaskLogger.Trace("Invoking appcmd with command line: {0}{1}", new object[]
			{
				text2,
				text3
			});
			string text4;
			string text5;
			int num = ProcessRunner.Run(text2, text3, -1, text, out text4, out text5);
			TaskLogger.Trace("The return value from appcmd: {0}", new object[]
			{
				num
			});
			TaskLogger.Trace("appcmd output: {0}", new object[]
			{
				text4
			});
			TaskLogger.Trace("appcmd errors: {0}", new object[]
			{
				text5
			});
			TaskLogger.Trace("Finished running appcmd command", new object[0]);
			if (num != 0)
			{
				base.WriteError(new InvalidOperationException(Strings.StampExistingResponsePassThroughOnVirtualDirectoryFailure(this.DataObject.Server.Name, this.DataObject.MetabasePath, num, text5)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			this.UpdateCompressionSettings();
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x001E1FB8 File Offset: 0x001E01B8
		private void UpdateCompressionSettings()
		{
			if (this.GzipLevel == GzipLevel.Error)
			{
				base.WriteError(new TaskException(Strings.GzipCannotBeSetToError), ErrorCategory.NotSpecified, null);
				return;
			}
			if (this.GzipLevel == GzipLevel.Low)
			{
				this.WriteWarning(Strings.GzipLowDoesNotUseDynamicCompression);
			}
			string metabasePath = this.DataObject.MetabasePath;
			Gzip.SetIisGzipLevel(IisUtility.WebSiteFromMetabasePath(metabasePath), GzipLevel.High);
			Gzip.SetVirtualDirectoryGzipLevel(metabasePath, this.DataObject.GzipLevel);
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
			{
				try
				{
					Gzip.SetIisGzipMimeTypes();
				}
				catch (Exception ex)
				{
					TaskLogger.Trace("Exception occurred in SetIisGzipMimeTypes(): {0}", new object[]
					{
						ex.Message
					});
					this.WriteWarning(Strings.SetIISGzipMimeTypesFailure);
					throw;
				}
			}
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x001E2080 File Offset: 0x001E0280
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force)
			{
				foreach (ADPropertyDefinition adpropertyDefinition in NewWebServicesVirtualDirectory.HostProperties)
				{
					if (!NewWebServicesVirtualDirectory.IsValidHost(this.DataObject, adpropertyDefinition) && !base.ShouldContinue(Strings.ConfirmationMessageHostCannotBeResolved(adpropertyDefinition.Name)))
					{
						TaskLogger.LogExit();
						return;
					}
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06007637 RID: 30263 RVA: 0x001E20EC File Offset: 0x001E02EC
		protected override void InternalProcessComplete()
		{
			base.InternalProcessComplete();
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				DirectoryEntry directoryEntry = IisUtility.CreateWebDirObject(this.DataObject.MetabasePath, null, "Nego2");
				IisUtility.SetProperty(directoryEntry, "AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script, true);
				directoryEntry.CommitChanges();
				string nego2Path = string.Format("{0}/{1}", this.DataObject.MetabasePath, "Nego2");
				ExchangeServiceVDirHelper.SetSplitVirtualDirectoryAuthenticationMethods(this.DataObject, nego2Path, new Task.TaskErrorLoggingDelegate(base.WriteError), this.MetabaseSetPropertiesFailureMessage);
			}
			else if (base.Role == VirtualDirectoryRole.ClientAccess)
			{
				ExchangeServiceVDirHelper.CheckAndUpdateLocalhostWebBindingsIfNecessary(this.DataObject);
			}
			ExchangeServiceVDirHelper.EwsAutodiscMWA.OnNewManageWCFEndpoints(this, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.Ews, this.DataObject.BasicAuthentication, this.DataObject.WindowsAuthentication, this.DataObject.WSSecurityAuthentication ?? false, this.DataObject.OAuthAuthentication ?? false, this.DataObject, base.Role);
		}

		// Token: 0x06007638 RID: 30264 RVA: 0x001E21F0 File Offset: 0x001E03F0
		protected override IConfigurable PrepareDataObject()
		{
			if (!base.Fields.Contains("ExtendedProtectionTokenChecking"))
			{
				base.Fields["ExtendedProtectionTokenChecking"] = ExtendedProtectionTokenCheckingMode.None;
			}
			if (!base.Fields.Contains("ExtendedProtectionSPNList"))
			{
				base.Fields["ExtendedProtectionSPNList"] = null;
			}
			if (!base.Fields.Contains("ExtendedProtectionFlags"))
			{
				base.Fields["ExtendedProtectionFlags"] = ExtendedProtectionFlag.None;
			}
			ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = (ADWebServicesVirtualDirectory)base.PrepareDataObject();
			if (base.Fields["WSSecurityAuthentication"] != null)
			{
				adwebServicesVirtualDirectory.WSSecurityAuthentication = new bool?(this.WSSecurityAuthentication);
			}
			if (base.Fields["OAuthAuthentication"] != null)
			{
				adwebServicesVirtualDirectory.OAuthAuthentication = new bool?(this.OAuthAuthentication);
			}
			return adwebServicesVirtualDirectory;
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x001E22C4 File Offset: 0x001E04C4
		protected override void WriteResultMetabaseFixup(ExchangeVirtualDirectory dataObject)
		{
			ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = (ADWebServicesVirtualDirectory)dataObject;
			adwebServicesVirtualDirectory.CertificateAuthentication = new bool?(true);
			adwebServicesVirtualDirectory.ResetChangeTracking();
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x001E22EC File Offset: 0x001E04EC
		internal static bool IsValidHost(ADWebServicesVirtualDirectory dataObject, ADPropertyDefinition property)
		{
			if (dataObject.IsChanged(property))
			{
				Uri uri = dataObject.propertyBag[property] as Uri;
				try
				{
					if (uri != null)
					{
						Dns.GetHostEntry(uri.DnsSafeHost);
					}
				}
				catch (SocketException)
				{
					return false;
				}
				catch (ArgumentOutOfRangeException)
				{
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x04003BA9 RID: 15273
		private const string EWSVDirName = "EWS";

		// Token: 0x04003BAA RID: 15274
		private const string EWSVDirPath = "ClientAccess\\exchweb\\EWS";

		// Token: 0x04003BAB RID: 15275
		private const string EWSCafeVDirPath = "FrontEnd\\HttpProxy\\EWS";

		// Token: 0x04003BAC RID: 15276
		private const string EWSDefaultAppPoolId = "MSExchangeServicesAppPool";

		// Token: 0x04003BAD RID: 15277
		private const string BinFolderName = "bin";

		// Token: 0x04003BAE RID: 15278
		internal static ADPropertyDefinition[] HostProperties = new ADPropertyDefinition[]
		{
			ADWebServicesVirtualDirectorySchema.InternalNLBBypassUrl,
			ADVirtualDirectorySchema.InternalUrl,
			ADVirtualDirectorySchema.ExternalUrl
		};
	}
}
