using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C21 RID: 3105
	[Cmdlet("New", "EcpVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewEcpVirtualDirectory : NewWebAppVirtualDirectory<ADEcpVirtualDirectory>
	{
		// Token: 0x06007506 RID: 29958 RVA: 0x001DE0F7 File Offset: 0x001DC2F7
		public NewEcpVirtualDirectory()
		{
			this.Name = "ecp";
			base.AppPoolId = "MSExchangeECPAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
		}

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x06007507 RID: 29959 RVA: 0x001DE11C File Offset: 0x001DC31C
		// (set) Token: 0x06007508 RID: 29960 RVA: 0x001DE124 File Offset: 0x001DC324
		public new string Name
		{
			get
			{
				return base.Name;
			}
			private set
			{
				base.Name = value;
			}
		}

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x06007509 RID: 29961 RVA: 0x001DE12D File Offset: 0x001DC32D
		// (set) Token: 0x0600750A RID: 29962 RVA: 0x001DE135 File Offset: 0x001DC335
		public new string ApplicationRoot
		{
			get
			{
				return base.ApplicationRoot;
			}
			private set
			{
				base.ApplicationRoot = value;
			}
		}

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x0600750B RID: 29963 RVA: 0x001DE13E File Offset: 0x001DC33E
		// (set) Token: 0x0600750C RID: 29964 RVA: 0x001DE146 File Offset: 0x001DC346
		internal new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return base.ExternalAuthenticationMethods;
			}
			private set
			{
				base.ExternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x0600750D RID: 29965 RVA: 0x001DE14F File Offset: 0x001DC34F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewEcpVirtualDirectory(base.WebSiteName, base.Server.ToString());
			}
		}

		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x0600750E RID: 29966 RVA: 0x001DE168 File Offset: 0x001DC368
		protected override ArrayList CustomizedVDirProperties
		{
			get
			{
				return new ArrayList
				{
					new MetabaseProperty("DefaultDoc", "default.aspx"),
					new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script),
					new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Basic),
					new MetabaseProperty("AppFriendlyName", this.Name),
					new MetabaseProperty("AppRoot", base.AppRootValue),
					new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled)
				};
			}
		}

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x0600750F RID: 29967 RVA: 0x001DE20C File Offset: 0x001DC40C
		protected override ListDictionary ChildVirtualDirectories
		{
			get
			{
				ListDictionary listDictionary = new ListDictionary();
				if (base.Role == VirtualDirectoryRole.Mailbox)
				{
					this.AddThemesDirectory(listDictionary, "App_Themes");
					this.AddThemesDirectory(listDictionary, NewEcpVirtualDirectory.GetEcpAssemblyVersion());
					this.AddCertAuthDirectory(listDictionary, "ReportingWebService");
				}
				return listDictionary;
			}
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x001DE250 File Offset: 0x001DC450
		protected override bool VerifyRoleConsistency()
		{
			if (base.Role == VirtualDirectoryRole.ClientAccess && !base.OwningServer.IsCafeServer)
			{
				base.WriteError(new ArgumentException("Argument: -Role ClientAccess"), ErrorCategory.InvalidArgument, null);
				return false;
			}
			if (base.Role == VirtualDirectoryRole.Mailbox && !base.OwningServer.IsCafeServer && !base.OwningServer.IsClientAccessServer && !base.OwningServer.IsFfoWebServiceRole && !base.OwningServer.IsOSPRole)
			{
				base.WriteError(new ArgumentException("Argument: -Role Mailbox"), ErrorCategory.InvalidArgument, null);
				return false;
			}
			return true;
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x001DE2D8 File Offset: 0x001DC4D8
		protected override bool ShouldCreateVirtualDirectory()
		{
			base.ShouldCreateVirtualDirectory();
			return this.VerifyRoleConsistency();
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x001DE2E8 File Offset: 0x001DC4E8
		private void AddThemesDirectory(ListDictionary dirs, string path)
		{
			if (Directory.Exists(System.IO.Path.Combine(base.Path, path)))
			{
				dirs.Add(path, new ArrayList
				{
					new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous),
					new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read)
				});
			}
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x001DE344 File Offset: 0x001DC544
		private void AddCertAuthDirectory(ListDictionary dirs, string path)
		{
			if (Directory.Exists(System.IO.Path.Combine(base.Path, path)))
			{
				dirs.Add(path, new ArrayList
				{
					new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSLNegotiateCert | MetabasePropertyTypes.AccessSSLFlags.AccessSSLRequireCert | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128),
					new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script)
				});
			}
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x001DE3A8 File Offset: 0x001DC5A8
		private static string GetEcpAssemblyVersion()
		{
			string text = NewEcpVirtualDirectory.EcpVersionDllPath;
			string result;
			try
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
				string text2 = string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					versionInfo.FileMajorPart,
					versionInfo.FileMinorPart,
					versionInfo.FileBuildPart,
					versionInfo.FilePrivatePart
				});
				result = text2;
			}
			catch (FileNotFoundException innerException)
			{
				throw new GetEcpVersionException(text, innerException);
			}
			return result;
		}

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06007515 RID: 29973 RVA: 0x001DE434 File Offset: 0x001DC634
		private static string EcpVersionDllPath
		{
			get
			{
				if (string.IsNullOrEmpty(NewEcpVirtualDirectory.ecpVersionDllPath))
				{
					NewEcpVirtualDirectory.ecpVersionDllPath = System.IO.Path.Combine(NewEcpVirtualDirectory.EcpPath, "Bin\\Microsoft.Exchange.Management.ControlPanel.dll");
				}
				return NewEcpVirtualDirectory.ecpVersionDllPath;
			}
		}

		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x06007516 RID: 29974 RVA: 0x001DE45B File Offset: 0x001DC65B
		private static string EcpPath
		{
			get
			{
				if (string.IsNullOrEmpty(NewEcpVirtualDirectory.ecpPath))
				{
					NewEcpVirtualDirectory.ecpPath = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\ecp");
				}
				return NewEcpVirtualDirectory.ecpPath;
			}
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x001DE484 File Offset: 0x001DC684
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			if (!base.Fields.IsModified("Path"))
			{
				base.Path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, (base.Role == VirtualDirectoryRole.ClientAccess) ? "FrontEnd\\HttpProxy\\ecp" : "ClientAccess\\ecp");
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!new VirtualDirectoryPathExistsCondition(base.OwningServer.Fqdn, base.Path).Verify())
			{
				base.WriteError(new ArgumentException(Strings.ErrorPathNotExistsOnServer(base.Path, base.OwningServer.Name), "Path"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x001DE54A File Offset: 0x001DC74A
		protected override bool InternalProcessStartWork()
		{
			this.SetDefaults();
			return true;
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x001DE554 File Offset: 0x001DC754
		protected override void InternalProcessMetabase()
		{
			ADOwaVirtualDirectory adowaVirtualDirectory = WebAppVirtualDirectoryHelper.FindWebAppVirtualDirectoryInSameWebSite<ADOwaVirtualDirectory>(this.DataObject, base.DataSession);
			if (adowaVirtualDirectory != null && !string.IsNullOrEmpty(adowaVirtualDirectory.DefaultDomain))
			{
				this.DataObject.DefaultDomain = adowaVirtualDirectory.DefaultDomain;
			}
			WebAppVirtualDirectoryHelper.UpdateMetabase(this.DataObject, this.DataObject.MetabasePath, true);
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.ReportingWebService.Enabled)
			{
				string physicalPath;
				List<MetabaseProperty> metabaseProperties;
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					physicalPath = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, "FrontEnd\\HttpProxy\\ReportingWebService");
					metabaseProperties = this.CreateFrontEndVdirProperies();
				}
				else
				{
					physicalPath = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\ReportingWebService");
					metabaseProperties = this.CreateBackEndVdirProperies();
				}
				this.CreateReportingWebServiceVDir(this.DataObject.MetabasePath, physicalPath, metabaseProperties);
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					string parent = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						this.DataObject.MetabasePath,
						"ReportingWebService"
					});
					this.CreatePartnerVDir(parent);
				}
			}
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x001DE65B File Offset: 0x001DC85B
		protected override void WriteResultMetabaseFixup(ExchangeVirtualDirectory targetDataObject)
		{
			base.WriteResultMetabaseFixup(targetDataObject);
			if (WebAppVirtualDirectoryHelper.FindWebAppVirtualDirectoryInSameWebSite<ADOwaVirtualDirectory>((ExchangeWebAppVirtualDirectory)targetDataObject, base.DataSession) == null)
			{
				this.WriteWarning(Strings.CreateOwaForEcpWarning);
			}
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x001DE684 File Offset: 0x001DC884
		private void SetDefaults()
		{
			this.DataObject.GzipLevel = GzipLevel.High;
			this.DataObject.FormsAuthentication = (base.Role == VirtualDirectoryRole.ClientAccess);
			this.DataObject.BasicAuthentication = (base.Role == VirtualDirectoryRole.ClientAccess);
			this.DataObject.WindowsAuthentication = (base.Role == VirtualDirectoryRole.Mailbox);
			this.DataObject.LiveIdAuthentication = false;
			this.DataObject.DigestAuthentication = false;
			this.DataObject.ExternalAuthenticationMethods = new MultiValuedProperty<AuthenticationMethod>(AuthenticationMethod.Fba);
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x001DE708 File Offset: 0x001DC908
		private void CreateReportingWebServiceVDir(string metabasePath, string physicalPath, List<MetabaseProperty> metabaseProperties)
		{
			TaskLogger.LogEnter();
			CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
			createVirtualDirectory.Name = "ReportingWebService";
			createVirtualDirectory.Parent = metabasePath;
			createVirtualDirectory.LocalPath = physicalPath;
			createVirtualDirectory.CustomizedVDirProperties = metabaseProperties;
			createVirtualDirectory.ApplicationPool = "MSExchangeReportingWebServiceAppPool";
			createVirtualDirectory.AppPoolIdentityType = MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			createVirtualDirectory.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			createVirtualDirectory.Initialize();
			createVirtualDirectory.Execute();
			TaskLogger.LogExit();
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x001DE76C File Offset: 0x001DC96C
		private void CreatePartnerVDir(string parent)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.NoneSet));
			arrayList.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSLNegotiateCert | MetabasePropertyTypes.AccessSSLFlags.AccessSSLRequireCert | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128));
			arrayList.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script));
			CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
			createVirtualDirectory.Name = "partner";
			createVirtualDirectory.Parent = parent;
			createVirtualDirectory.CustomizedVDirProperties = arrayList;
			createVirtualDirectory.LocalPath = null;
			createVirtualDirectory.Initialize();
			createVirtualDirectory.Execute();
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x001DE800 File Offset: 0x001DCA00
		private List<MetabaseProperty> CreateFrontEndVdirProperies()
		{
			return new List<MetabaseProperty>
			{
				new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.NoneSet),
				new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script),
				new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled)
			};
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x001DE85C File Offset: 0x001DCA5C
		private List<MetabaseProperty> CreateBackEndVdirProperies()
		{
			List<MetabaseProperty> list = new List<MetabaseProperty>();
			list.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous | MetabasePropertyTypes.AuthFlags.Ntlm));
			string value = string.Format("{0},{1}", "Negotiate", "NTLM");
			list.Add(new MetabaseProperty("NTAuthenticationProviders", value));
			list.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script));
			list.Add(new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled));
			return list;
		}

		// Token: 0x04003B66 RID: 15206
		private const string DefaultVDirName = "ecp";

		// Token: 0x04003B67 RID: 15207
		private const string LocalPath = "ClientAccess\\ecp";

		// Token: 0x04003B68 RID: 15208
		private const string CafePath = "FrontEnd\\HttpProxy\\ecp";

		// Token: 0x04003B69 RID: 15209
		private const string DefaultApplicationPool = "MSExchangeECPAppPool";

		// Token: 0x04003B6A RID: 15210
		private const string AppThemesPath = "App_Themes";

		// Token: 0x04003B6B RID: 15211
		private const string ReportingWebServicePath = "ReportingWebService";

		// Token: 0x04003B6C RID: 15212
		private const string ReportingWebServiceVDirName = "ReportingWebService";

		// Token: 0x04003B6D RID: 15213
		private const string ReportingWebServiceVDirPath = "ClientAccess\\ReportingWebService";

		// Token: 0x04003B6E RID: 15214
		private const string ReportingWebServiceVDirFrontEndPath = "FrontEnd\\HttpProxy\\ReportingWebService";

		// Token: 0x04003B6F RID: 15215
		private const string ReportingWebServiceApplicationPool = "MSExchangeReportingWebServiceAppPool";

		// Token: 0x04003B70 RID: 15216
		private static string ecpPath;

		// Token: 0x04003B71 RID: 15217
		private static string ecpVersionDllPath;
	}
}
