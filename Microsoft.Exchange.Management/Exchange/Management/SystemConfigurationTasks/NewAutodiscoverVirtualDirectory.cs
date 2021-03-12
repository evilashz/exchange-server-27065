using System;
using System.Collections;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C1F RID: 3103
	[Cmdlet("New", "AutodiscoverVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewAutodiscoverVirtualDirectory : NewExchangeServiceVirtualDirectory<ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x060074ED RID: 29933 RVA: 0x001DDA79 File Offset: 0x001DBC79
		public NewAutodiscoverVirtualDirectory()
		{
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
		}

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x060074EE RID: 29934 RVA: 0x001DDA88 File Offset: 0x001DBC88
		// (set) Token: 0x060074EF RID: 29935 RVA: 0x001DDAB3 File Offset: 0x001DBCB3
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

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x060074F0 RID: 29936 RVA: 0x001DDACB File Offset: 0x001DBCCB
		// (set) Token: 0x060074F1 RID: 29937 RVA: 0x001DDAF6 File Offset: 0x001DBCF6
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

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x060074F2 RID: 29938 RVA: 0x001DDB0E File Offset: 0x001DBD0E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAutodiscoverVirtualDirectory;
			}
		}

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x060074F3 RID: 29939 RVA: 0x001DDB15 File Offset: 0x001DBD15
		protected override string VirtualDirectoryName
		{
			get
			{
				return "Autodiscover";
			}
		}

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x060074F4 RID: 29940 RVA: 0x001DDB1C File Offset: 0x001DBD1C
		protected override string VirtualDirectoryPath
		{
			get
			{
				if (base.Role != VirtualDirectoryRole.ClientAccess)
				{
					return "ClientAccess\\Autodiscover";
				}
				return "FrontEnd\\HttpProxy\\Autodiscover";
			}
		}

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x060074F5 RID: 29941 RVA: 0x001DDB31 File Offset: 0x001DBD31
		protected override string DefaultApplicationPoolId
		{
			get
			{
				return "MSExchangeAutodiscoverAppPool";
			}
		}

		// Token: 0x060074F6 RID: 29942 RVA: 0x001DDB38 File Offset: 0x001DBD38
		protected override void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.WindowsAuthentication = new bool?(true);
			virtualDirectory.WSSecurityAuthentication = new bool?(true);
			virtualDirectory.BasicAuthentication = new bool?(base.Role == VirtualDirectoryRole.ClientAccess);
			virtualDirectory.OAuthAuthentication = new bool?(base.Role == VirtualDirectoryRole.ClientAccess);
			virtualDirectory.DigestAuthentication = new bool?(false);
			virtualDirectory.LiveIdBasicAuthentication = new bool?(false);
			virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
		}

		// Token: 0x060074F7 RID: 29943 RVA: 0x001DDBAC File Offset: 0x001DBDAC
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
			ADAutodiscoverVirtualDirectory adautodiscoverVirtualDirectory = (ADAutodiscoverVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields["WSSecurityAuthentication"] != null)
			{
				adautodiscoverVirtualDirectory.WSSecurityAuthentication = new bool?(this.WSSecurityAuthentication);
			}
			if (base.Fields["OAuthAuthentication"] != null)
			{
				adautodiscoverVirtualDirectory.OAuthAuthentication = new bool?(this.OAuthAuthentication);
			}
			return adautodiscoverVirtualDirectory;
		}

		// Token: 0x060074F8 RID: 29944 RVA: 0x001DDC8C File Offset: 0x001DBE8C
		protected override void InternalProcessMetabase()
		{
			TaskLogger.LogEnter();
			base.InternalProcessMetabase();
			if (base.Role == VirtualDirectoryRole.ClientAccess && Datacenter.IsMicrosoftHostedOnly(false) && ((Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 7))
			{
				if (DirectoryEntry.Exists(this.DataObject.MetabasePath))
				{
					TaskLogger.Trace("MultiTenancy mode: installing AuthModuleFilter isapi filter", new object[0]);
					try
					{
						NewAutodiscoverVirtualDirectory.InstallAuthModuleIsapiFilter(this.DataObject);
						goto IL_DE;
					}
					catch (Exception ex)
					{
						TaskLogger.Trace("Exception occurred in InstallIsapiFilter(): {0}", new object[]
						{
							ex.Message
						});
						this.WriteWarning(Strings.AuthModuleFilterMetabaseIsapiInstallFailure);
						throw;
					}
				}
				base.WriteError(new InvalidOperationException(Strings.ErrorMetabasePathNotFound(this.DataObject.MetabasePath)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			IL_DE:
			TaskLogger.LogExit();
		}

		// Token: 0x060074F9 RID: 29945 RVA: 0x001DDD8C File Offset: 0x001DBF8C
		protected override void AddCustomVDirProperties(ArrayList customProperties)
		{
			base.AddCustomVDirProperties(customProperties);
			customProperties.Add(new MetabaseProperty("DefaultDoc", "autodiscover.xml"));
			customProperties.Add(new MetabaseProperty("ScriptMaps", ".xml," + DotNetFrameworkInfo.AspNetIsapiDllPath + ",1,POST,GET"));
			string path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess");
			customProperties.Add(new MetabaseProperty("HttpErrors", "401,1,FILE," + System.IO.Path.Combine(path, "help\\401-1.xml")));
		}

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x060074FA RID: 29946 RVA: 0x001DDE14 File Offset: 0x001DC014
		protected override ListDictionary ChildVirtualDirectories
		{
			get
			{
				ListDictionary listDictionary = new ListDictionary();
				if (base.Role == VirtualDirectoryRole.Mailbox)
				{
					string[] array = new string[]
					{
						"bin",
						"help"
					};
					foreach (string text in array)
					{
						if (Directory.Exists(System.IO.Path.Combine(base.Path, text)))
						{
							listDictionary.Add(text, new ArrayList
							{
								new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.NoAccess),
								new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.NoneSet),
								new MetabaseProperty("AppPoolId", base.AppPoolId)
							});
						}
					}
				}
				return listDictionary;
			}
		}

		// Token: 0x060074FB RID: 29947 RVA: 0x001DDED3 File Offset: 0x001DC0D3
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
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x001DDF14 File Offset: 0x001DC114
		protected override void InternalProcessComplete()
		{
			base.InternalProcessComplete();
			ExchangeServiceVDirHelper.ForceAnonymous(this.DataObject.MetabasePath);
			ExchangeServiceVDirHelper.EwsAutodiscMWA.OnNewManageWCFEndpoints(this, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.Autodiscover, this.DataObject.BasicAuthentication, this.DataObject.WindowsAuthentication, this.DataObject.WSSecurityAuthentication ?? false, this.DataObject.OAuthAuthentication ?? false, this.DataObject, base.Role);
		}

		// Token: 0x060074FD RID: 29949 RVA: 0x001DDFA0 File Offset: 0x001DC1A0
		internal static void InstallAuthModuleIsapiFilter(ADAutodiscoverVirtualDirectory vdirObject)
		{
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(vdirObject.MetabasePath))
			{
				AuthModuleIsapiFilter.Install(directoryEntry);
			}
		}

		// Token: 0x04003B5F RID: 15199
		private const string AutodiscoverVDirName = "Autodiscover";

		// Token: 0x04003B60 RID: 15200
		private const string AutodiscoverVDirPath = "ClientAccess\\Autodiscover";

		// Token: 0x04003B61 RID: 15201
		private const string AutodiscoverCafeVDirPath = "FrontEnd\\HttpProxy\\Autodiscover";

		// Token: 0x04003B62 RID: 15202
		private const string AutodiscoverDefaultAppPoolId = "MSExchangeAutodiscoverAppPool";

		// Token: 0x04003B63 RID: 15203
		private const string BinFolderName = "bin";

		// Token: 0x04003B64 RID: 15204
		private const string HelpFolderName = "help";

		// Token: 0x04003B65 RID: 15205
		private const string DefaultDocName = "autodiscover.xml";
	}
}
