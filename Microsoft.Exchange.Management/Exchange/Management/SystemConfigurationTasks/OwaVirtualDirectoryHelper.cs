using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Metabase;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C31 RID: 3121
	internal class OwaVirtualDirectoryHelper : WebAppVirtualDirectoryHelper
	{
		// Token: 0x06007643 RID: 30275 RVA: 0x001E287C File Offset: 0x001E0A7C
		private OwaVirtualDirectoryHelper()
		{
		}

		// Token: 0x17002471 RID: 9329
		// (get) Token: 0x06007644 RID: 30276 RVA: 0x001E2884 File Offset: 0x001E0A84
		internal static string OwaPath
		{
			get
			{
				if (string.IsNullOrEmpty(OwaVirtualDirectoryHelper.owaPath))
				{
					OwaVirtualDirectoryHelper.owaPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\owa");
				}
				return OwaVirtualDirectoryHelper.owaPath;
			}
		}

		// Token: 0x17002472 RID: 9330
		// (get) Token: 0x06007645 RID: 30277 RVA: 0x001E28AB File Offset: 0x001E0AAB
		internal static string OwaCafePath
		{
			get
			{
				if (string.IsNullOrEmpty(OwaVirtualDirectoryHelper.cafePath))
				{
					OwaVirtualDirectoryHelper.cafePath = Path.Combine(ConfigurationContext.Setup.InstallPath, "FrontEnd\\HttpProxy\\owa");
				}
				return OwaVirtualDirectoryHelper.cafePath;
			}
		}

		// Token: 0x17002473 RID: 9331
		// (get) Token: 0x06007646 RID: 30278 RVA: 0x001E28D2 File Offset: 0x001E0AD2
		internal static string OwaVersionDllPath
		{
			get
			{
				if (string.IsNullOrEmpty(OwaVirtualDirectoryHelper.owaVersionDllPath))
				{
					OwaVirtualDirectoryHelper.owaVersionDllPath = Path.Combine(OwaVirtualDirectoryHelper.OwaPath, "Bin\\Microsoft.Exchange.Clients.Owa.dll");
				}
				return OwaVirtualDirectoryHelper.owaVersionDllPath;
			}
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x001E28FC File Offset: 0x001E0AFC
		internal static void EnableIsapiFilter(ADOwaVirtualDirectory adOwaVirtualDirectory, bool forCafe)
		{
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adOwaVirtualDirectory.MetabasePath))
			{
				if (forCafe)
				{
					OwaIsapiFilter.InstallForCafe(directoryEntry);
				}
				else
				{
					OwaIsapiFilter.Install(directoryEntry);
				}
			}
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x001E2944 File Offset: 0x001E0B44
		internal static void DisableIsapiFilter(ADOwaVirtualDirectory adOwaVirtualDirectory)
		{
			DirectoryEntry directoryEntry;
			DirectoryEntry virtualDirectory = directoryEntry = IisUtility.CreateIISDirectoryEntry(adOwaVirtualDirectory.MetabasePath);
			try
			{
				OwaIsapiFilter.UninstallIfLastVdir(virtualDirectory);
			}
			finally
			{
				if (directoryEntry != null)
				{
					((IDisposable)directoryEntry).Dispose();
				}
			}
		}

		// Token: 0x06007649 RID: 30281 RVA: 0x001E2984 File Offset: 0x001E0B84
		public static void CopyDavVdirsToMetabase(string domainController, string exchangeServerName, string metabaseServerName)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 154, "CopyDavVdirsToMetabase", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\VirtualDirectoryTasks\\OWAVirtualDirectoryHelper.cs");
			Server server = topologyConfigurationSession.FindServerByName(exchangeServerName);
			IConfigDataProvider configDataProvider = topologyConfigurationSession;
			IEnumerable<ADOwaVirtualDirectory> enumerable = configDataProvider.FindPaged<ADOwaVirtualDirectory>(null, server.Id, true, null, 0);
			OwaVirtualDirectoryHelper.SetWebSvcExtRestrictionList(metabaseServerName);
			foreach (ADOwaVirtualDirectory adowaVirtualDirectory in enumerable)
			{
				if (!adowaVirtualDirectory.IsExchange2007OrLater)
				{
					string[] array = adowaVirtualDirectory.MetabasePath.Split(new char[]
					{
						'/'
					});
					if (array.Length == 7)
					{
						array[2] = metabaseServerName;
						MultiValuedProperty<AuthenticationMethod> internalAuthenticationMethods = adowaVirtualDirectory.InternalAuthenticationMethods;
						adowaVirtualDirectory.WindowsAuthentication = true;
						string appPoolRootPath = IisUtility.GetAppPoolRootPath(metabaseServerName);
						string text = "MSExchangeOWAAppPool";
						if (!IisUtility.Exists(appPoolRootPath, text, "IIsApplicationPool"))
						{
							using (DirectoryEntry directoryEntry = IisUtility.CreateApplicationPool(metabaseServerName, text))
							{
								IisUtility.SetProperty(directoryEntry, "AppPoolIdentityType", 0, true);
								directoryEntry.CommitChanges();
							}
						}
						if (!IisUtility.Exists(string.Join("/", array)))
						{
							DirectoryEntry directoryEntry2 = IisUtility.CreateWebDirObject(string.Join("/", array, 0, 6), adowaVirtualDirectory.FolderPathname, array[6]);
							ArrayList arrayList = new ArrayList();
							arrayList.Add(new MetabaseProperty("LogonMethod", MetabasePropertyTypes.LogonMethod.ClearTextLogon));
							arrayList.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Write | MetabasePropertyTypes.AccessFlags.Source | MetabasePropertyTypes.AccessFlags.Script));
							arrayList.Add(new MetabaseProperty("DirBrowseFlags", (MetabasePropertyTypes.DirBrowseFlags)3221225534U));
							arrayList.Add(new MetabaseProperty("ScriptMaps", OwaVirtualDirectoryHelper.GetDavScriptMaps(), true));
							if (adowaVirtualDirectory.VirtualDirectoryType == VirtualDirectoryTypes.Exchweb)
							{
								arrayList.Add(new MetabaseProperty("HttpExpires", "D, 0x278d00"));
							}
							if (adowaVirtualDirectory.DefaultDomain.Length > 0)
							{
								arrayList.Add(new MetabaseProperty("DefaultLogonDomain", adowaVirtualDirectory.DefaultDomain, true));
							}
							OwaIsapiFilter.DisableFba(directoryEntry2);
							uint num = 0U;
							using (MultiValuedProperty<AuthenticationMethod>.Enumerator enumerator2 = adowaVirtualDirectory.InternalAuthenticationMethods.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									switch (enumerator2.Current)
									{
									case AuthenticationMethod.Basic:
										num |= 2U;
										break;
									case AuthenticationMethod.Digest:
										num |= 16U;
										break;
									case AuthenticationMethod.Ntlm:
										num |= 4U;
										break;
									case AuthenticationMethod.Fba:
										OwaIsapiFilter.EnableFba(directoryEntry2);
										break;
									}
								}
							}
							arrayList.Add(new MetabaseProperty("AuthFlags", num, true));
							IisUtility.SetProperties(directoryEntry2, arrayList);
							IisUtility.AssignApplicationPool(directoryEntry2, text);
						}
					}
				}
			}
		}

		// Token: 0x0600764A RID: 30282 RVA: 0x001E2C98 File Offset: 0x001E0E98
		internal static bool IsDataCenterCafe(VirtualDirectoryRole role)
		{
			return role == VirtualDirectoryRole.ClientAccess && Datacenter.IsMicrosoftHostedOnly(true);
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x001E2CA8 File Offset: 0x001E0EA8
		internal static void SetWebSvcExtRestrictionList(string metabaseServerName)
		{
			using (IsapiExtensionList isapiExtensionList = new IsapiExtensionList(metabaseServerName))
			{
				string groupID = "MSEXCHANGE";
				string description = "Microsoft Exchange Server";
				string physicalPath;
				if (RoleManager.GetRoleByName("MailboxRole").IsInstalled || RoleManager.GetRoleByName("MailboxRole").IsPartiallyInstalled)
				{
					physicalPath = Path.Combine(ConfigurationContext.Setup.BinPath, "davex.dll");
				}
				else
				{
					physicalPath = Path.Combine(ConfigurationContext.Setup.BinPath, "exprox.dll");
				}
				isapiExtensionList.Add(true, physicalPath, false, groupID, description);
				isapiExtensionList.CommitChanges();
			}
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x001E2D3C File Offset: 0x001E0F3C
		internal static string GetDavScriptMaps()
		{
			Role roleByName = RoleManager.GetRoleByName("MailboxRole");
			ConfigurationStatus configurationStatus = new ConfigurationStatus("MailboxRole");
			RolesUtility.GetConfiguringStatus(ref configurationStatus);
			string path;
			if (roleByName.IsInstalled || (roleByName.IsPartiallyInstalled && configurationStatus.Action != InstallationModes.Uninstall))
			{
				path = "davex.dll";
			}
			else
			{
				path = "exprox.dll";
			}
			string str = Path.Combine(ConfigurationContext.Setup.BinPath, path);
			return "*," + str + ",1";
		}

		// Token: 0x0600764D RID: 30285 RVA: 0x001E2DB8 File Offset: 0x001E0FB8
		internal static void AddVersionVDir(ListDictionary childVDirs)
		{
			ArrayList versionVDirProperties = OwaVirtualDirectoryHelper.GetVersionVDirProperties();
			string owaAssemblyVersion = OwaVirtualDirectoryHelper.GetOwaAssemblyVersion();
			childVDirs.Add(owaAssemblyVersion, versionVDirProperties);
		}

		// Token: 0x0600764E RID: 30286 RVA: 0x001E2DDC File Offset: 0x001E0FDC
		internal static void CreateLegacyVDirs(string metabasePath, bool deleteObjectIfExists)
		{
			string webSiteRoot = IisUtility.GetWebSiteRoot(metabasePath);
			IList legacyVirtualDirectories = OwaVirtualDirectoryHelper.GetLegacyVirtualDirectories();
			if (legacyVirtualDirectories != null)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new MetabaseProperty("HttpRedirect", "/owa"));
				string localPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\owa");
				OwaVirtualDirectoryHelper.CreatedLegacyVDirs.Clear();
				foreach (object obj in legacyVirtualDirectories)
				{
					string text = (string)obj;
					if (deleteObjectIfExists && IisUtility.WebDirObjectExists(webSiteRoot, text))
					{
						IisUtility.DeleteWebDirObject(webSiteRoot, text);
					}
					CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
					createVirtualDirectory.Name = text;
					createVirtualDirectory.Parent = webSiteRoot;
					createVirtualDirectory.LocalPath = localPath;
					createVirtualDirectory.CustomizedVDirProperties = arrayList;
					createVirtualDirectory.Initialize();
					createVirtualDirectory.Execute();
					OwaVirtualDirectoryHelper.CreatedLegacyVDirs.Add(text);
				}
			}
		}

		// Token: 0x0600764F RID: 30287 RVA: 0x001E2ED4 File Offset: 0x001E10D4
		internal static string GetOwaAssemblyVersion()
		{
			string text = OwaVirtualDirectoryHelper.OwaVersionDllPath;
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
				throw new GetOwaVersionException(text, innerException);
			}
			return result;
		}

		// Token: 0x06007650 RID: 30288 RVA: 0x001E2F60 File Offset: 0x001E1160
		internal static ArrayList GetVersionVDirProperties()
		{
			return new ArrayList
			{
				new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous),
				new MetabaseProperty("LogonMethod", MetabasePropertyTypes.LogonMethod.ClearTextLogon),
				new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read),
				new MetabaseProperty("HttpExpires", "D, 0x278d00")
			};
		}

		// Token: 0x06007651 RID: 30289 RVA: 0x001E2FD0 File Offset: 0x001E11D0
		internal static IList GetLegacyVirtualDirectories()
		{
			return new List<string>
			{
				"Exchange",
				"Exchweb",
				"Public"
			};
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x001E3008 File Offset: 0x001E1208
		private static List<MetabaseProperty> GetOwaCalendarVDirProperties(VirtualDirectoryRole role)
		{
			List<MetabaseProperty> list = new List<MetabaseProperty>();
			list.Add(new MetabaseProperty("Path", (role == VirtualDirectoryRole.Mailbox) ? OwaVirtualDirectoryHelper.OwaPath : OwaVirtualDirectoryHelper.OwaCafePath));
			list.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous));
			list.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script));
			list.Add(new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled));
			if (!OwaVirtualDirectoryHelper.IsDataCenterCafe(role))
			{
				list.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.None));
			}
			return list;
		}

		// Token: 0x06007653 RID: 30291 RVA: 0x001E30A0 File Offset: 0x001E12A0
		internal static void CreateOwaCalendarVDir(string metabasePath, VirtualDirectoryRole role)
		{
			MetabasePropertyTypes.AppPoolIdentityType appPoolIdentityType = MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			if (IisUtility.WebDirObjectExists(metabasePath, "Calendar"))
			{
				string hostName = IisUtility.GetHostName(metabasePath);
				string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
				using (DirectoryEntry directoryEntry = IisUtility.FindWebObject(appPoolRootPath, "MSExchangeOWACalendarAppPool", "IIsApplicationPool"))
				{
					IisUtility.SetProperty(directoryEntry, "AppPoolIdentityType", appPoolIdentityType, true);
					directoryEntry.CommitChanges();
				}
				return;
			}
			CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
			createVirtualDirectory.Name = "Calendar";
			createVirtualDirectory.Parent = metabasePath;
			createVirtualDirectory.LocalPath = ((role == VirtualDirectoryRole.Mailbox) ? OwaVirtualDirectoryHelper.OwaPath : OwaVirtualDirectoryHelper.OwaCafePath);
			createVirtualDirectory.CustomizedVDirProperties = OwaVirtualDirectoryHelper.GetOwaCalendarVDirProperties(role);
			createVirtualDirectory.ApplicationPool = "MSExchangeOWACalendarAppPool";
			createVirtualDirectory.AppPoolIdentityType = appPoolIdentityType;
			createVirtualDirectory.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			createVirtualDirectory.AppPoolQueueLength = 10;
			createVirtualDirectory.Initialize();
			createVirtualDirectory.Execute();
		}

		// Token: 0x06007654 RID: 30292 RVA: 0x001E3184 File Offset: 0x001E1384
		private static List<MetabaseProperty> GetOwaIntegratedVDirProperties(VirtualDirectoryRole role)
		{
			List<MetabaseProperty> list = new List<MetabaseProperty>();
			list.Add(new MetabaseProperty("Path", OwaVirtualDirectoryHelper.OwaCafePath));
			list.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Ntlm));
			list.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script));
			list.Add(new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled));
			if (!OwaVirtualDirectoryHelper.IsDataCenterCafe(role))
			{
				list.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128));
			}
			return list;
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x001E3218 File Offset: 0x001E1418
		internal static void CreateOwaIntegratedVDir(string metabasePath, VirtualDirectoryRole role)
		{
			MetabasePropertyTypes.AppPoolIdentityType appPoolIdentityType = MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			if (IisUtility.WebDirObjectExists(metabasePath, "Integrated"))
			{
				string hostName = IisUtility.GetHostName(metabasePath);
				string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
				using (DirectoryEntry directoryEntry = IisUtility.FindWebObject(appPoolRootPath, "MSExchangeOWAAppPool", "IIsApplicationPool"))
				{
					IisUtility.SetProperty(directoryEntry, "AppPoolIdentityType", appPoolIdentityType, true);
					directoryEntry.CommitChanges();
				}
				return;
			}
			CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
			createVirtualDirectory.Name = "Integrated";
			createVirtualDirectory.Parent = metabasePath;
			createVirtualDirectory.LocalPath = OwaVirtualDirectoryHelper.OwaCafePath;
			createVirtualDirectory.CustomizedVDirProperties = OwaVirtualDirectoryHelper.GetOwaIntegratedVDirProperties(role);
			createVirtualDirectory.ApplicationPool = "MSExchangeOWAAppPool";
			createVirtualDirectory.AppPoolIdentityType = appPoolIdentityType;
			createVirtualDirectory.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			createVirtualDirectory.AppPoolQueueLength = 10;
			createVirtualDirectory.Initialize();
			createVirtualDirectory.Execute();
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x001E32F0 File Offset: 0x001E14F0
		private static List<MetabaseProperty> GetOmaVDirProperties(VirtualDirectoryRole role)
		{
			List<MetabaseProperty> list = new List<MetabaseProperty>();
			list.Add(new MetabaseProperty("Path", OwaVirtualDirectoryHelper.OwaCafePath));
			list.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Basic));
			list.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script));
			list.Add(new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled));
			if (!OwaVirtualDirectoryHelper.IsDataCenterCafe(role))
			{
				list.Add(new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128));
			}
			return list;
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x001E3384 File Offset: 0x001E1584
		internal static void CreateOmaVDir(string metabasePath, VirtualDirectoryRole role)
		{
			MetabasePropertyTypes.AppPoolIdentityType appPoolIdentityType = MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			if (IisUtility.WebDirObjectExists(metabasePath, "oma"))
			{
				string hostName = IisUtility.GetHostName(metabasePath);
				string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
				using (DirectoryEntry directoryEntry = IisUtility.FindWebObject(appPoolRootPath, "MSExchangeOWAAppPool", "IIsApplicationPool"))
				{
					IisUtility.SetProperty(directoryEntry, "AppPoolIdentityType", appPoolIdentityType, true);
					directoryEntry.CommitChanges();
				}
				return;
			}
			CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
			createVirtualDirectory.Name = "oma";
			createVirtualDirectory.Parent = metabasePath;
			createVirtualDirectory.LocalPath = OwaVirtualDirectoryHelper.OwaCafePath;
			createVirtualDirectory.CustomizedVDirProperties = OwaVirtualDirectoryHelper.GetOmaVDirProperties(role);
			createVirtualDirectory.ApplicationPool = "MSExchangeOWAAppPool";
			createVirtualDirectory.AppPoolIdentityType = appPoolIdentityType;
			createVirtualDirectory.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			createVirtualDirectory.AppPoolQueueLength = 10;
			createVirtualDirectory.Initialize();
			createVirtualDirectory.Execute();
		}

		// Token: 0x04003BB0 RID: 15280
		internal const string LocalPath = "ClientAccess\\owa";

		// Token: 0x04003BB1 RID: 15281
		internal const string CafePath = "FrontEnd\\HttpProxy\\owa";

		// Token: 0x04003BB2 RID: 15282
		internal const string CafeBinPath = "FrontEnd\\HttpProxy\\bin";

		// Token: 0x04003BB3 RID: 15283
		internal const string DefaultApplicationPool = "MSExchangeOWAAppPool";

		// Token: 0x04003BB4 RID: 15284
		internal const string OwaCalendarApplicationPool = "MSExchangeOWACalendarAppPool";

		// Token: 0x04003BB5 RID: 15285
		private const string defaultExchangeName = "Exchange";

		// Token: 0x04003BB6 RID: 15286
		private const string defaultPublicName = "Public";

		// Token: 0x04003BB7 RID: 15287
		private const string defaultExchwebName = "Exchweb";

		// Token: 0x04003BB8 RID: 15288
		private const string defaultExadminName = "Exadmin";

		// Token: 0x04003BB9 RID: 15289
		internal const string defaultE12Name = "owa";

		// Token: 0x04003BBA RID: 15290
		internal const string CalendarVdirName = "Calendar";

		// Token: 0x04003BBB RID: 15291
		internal const string IntegratedVdirName = "Integrated";

		// Token: 0x04003BBC RID: 15292
		internal const string OmaVdirName = "oma";

		// Token: 0x04003BBD RID: 15293
		private static string owaPath;

		// Token: 0x04003BBE RID: 15294
		private static string cafePath;

		// Token: 0x04003BBF RID: 15295
		private static string owaVersionDllPath;

		// Token: 0x04003BC0 RID: 15296
		internal static List<string> CreatedLegacyVDirs = new List<string>();
	}
}
