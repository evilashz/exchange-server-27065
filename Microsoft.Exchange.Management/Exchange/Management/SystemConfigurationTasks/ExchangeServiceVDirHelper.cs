using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C04 RID: 3076
	internal static class ExchangeServiceVDirHelper
	{
		// Token: 0x06007449 RID: 29769 RVA: 0x001D9A98 File Offset: 0x001D7C98
		public static void SetIisVirtualDirectoryAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory, Task.TaskErrorLoggingDelegate errorHandler, LocalizedString errorMessage)
		{
			ExchangeServiceVDirHelper.SetSplitVirtualDirectoryAuthenticationMethods(virtualDirectory, null, errorHandler, errorMessage);
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x001D9AA4 File Offset: 0x001D7CA4
		public static void SetSplitVirtualDirectoryAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory, string nego2Path, Task.TaskErrorLoggingDelegate errorHandler, LocalizedString errorMessage)
		{
			try
			{
				ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(virtualDirectory);
				if (!string.IsNullOrEmpty(nego2Path))
				{
					bool? windowsAuthentication = virtualDirectory.WindowsAuthentication;
					bool? liveIdNegotiateAuthentication = virtualDirectory.LiveIdNegotiateAuthentication;
					ADExchangeServiceVirtualDirectory adexchangeServiceVirtualDirectory = (ADExchangeServiceVirtualDirectory)virtualDirectory.Clone();
					if (windowsAuthentication != null)
					{
						adexchangeServiceVirtualDirectory.WindowsAuthentication = new bool?(false);
					}
					if (liveIdNegotiateAuthentication != null)
					{
						virtualDirectory.LiveIdNegotiateAuthentication = new bool?(false);
					}
					ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(virtualDirectory);
					ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(adexchangeServiceVirtualDirectory, nego2Path);
				}
			}
			catch (DataSourceTransientException innerException)
			{
				errorHandler(new LocalizedException(errorMessage, innerException), ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			catch (DataSourceOperationException innerException2)
			{
				errorHandler(new LocalizedException(errorMessage, innerException2), ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			catch (COMException innerException3)
			{
				errorHandler(new LocalizedException(errorMessage, innerException3), ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
		}

		// Token: 0x0600744B RID: 29771 RVA: 0x001D9B84 File Offset: 0x001D7D84
		public static void SetIisVirtualDirectoryAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(virtualDirectory, virtualDirectory.MetabasePath);
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x001D9B94 File Offset: 0x001D7D94
		public static void SetIisVirtualDirectoryAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory, string metabasePath)
		{
			DirectoryEntry directoryEntry2;
			DirectoryEntry directoryEntry = directoryEntry2 = IisUtility.CreateIISDirectoryEntry(metabasePath);
			try
			{
				if (virtualDirectory.BasicAuthentication != null)
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic, virtualDirectory.BasicAuthentication.Value);
				}
				if (virtualDirectory.DigestAuthentication != null)
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Digest, virtualDirectory.DigestAuthentication.Value);
				}
				if (virtualDirectory.WindowsAuthentication != null)
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Ntlm, virtualDirectory.WindowsAuthentication.Value);
				}
				if (virtualDirectory.WSSecurityAuthentication != null)
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.WSSecurity, virtualDirectory.WSSecurityAuthentication.Value);
				}
				if (virtualDirectory.LiveIdNegotiateAuthentication != null)
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.LiveIdNegotiate, virtualDirectory.LiveIdNegotiateAuthentication.Value);
				}
				bool? flag = null;
				if (virtualDirectory is ADWebServicesVirtualDirectory)
				{
					ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = (ADWebServicesVirtualDirectory)virtualDirectory;
					flag = adwebServicesVirtualDirectory.CertificateAuthentication;
				}
				else if (virtualDirectory is ADPowerShellCommonVirtualDirectory)
				{
					ADPowerShellCommonVirtualDirectory adpowerShellCommonVirtualDirectory = (ADPowerShellCommonVirtualDirectory)virtualDirectory;
					flag = adpowerShellCommonVirtualDirectory.CertificateAuthentication;
				}
				if (flag != null)
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Certificate, flag.Value);
				}
				directoryEntry.CommitChanges();
				IisUtility.CommitMetabaseChanges((virtualDirectory.Server == null) ? null : virtualDirectory.Server.ToString());
			}
			finally
			{
				if (directoryEntry2 != null)
				{
					((IDisposable)directoryEntry2).Dispose();
				}
			}
		}

		// Token: 0x0600744D RID: 29773 RVA: 0x001D9D14 File Offset: 0x001D7F14
		internal static void CheckAndUpdateWindowsAuthProvidersIfNecessary(ADExchangeServiceVirtualDirectory adVDir, bool? windowsAuthentication)
		{
			if (windowsAuthentication == null || !windowsAuthentication.Value)
			{
				return;
			}
			DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adVDir.MetabasePath);
			using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(adVDir.MetabasePath)))
			{
				Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
				ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/security/authentication/windowsAuthentication", "/" + IisUtility.GetWebSiteName(directoryEntry.Parent.Path) + "/" + directoryEntry.Name);
				ConfigurationElementCollection collection = section.GetCollection("providers");
				foreach (ConfigurationElement configurationElement in collection)
				{
					string a = configurationElement.GetAttributeValue("value") as string;
					if (string.Equals(a, "Negotiate", StringComparison.OrdinalIgnoreCase))
					{
						return;
					}
				}
				ConfigurationElement configurationElement2 = collection.CreateElement();
				configurationElement2.SetAttributeValue("value", "Negotiate");
				collection.AddAt(0, configurationElement2);
				serverManager.CommitChanges();
			}
		}

		// Token: 0x0600744E RID: 29774 RVA: 0x001D9E34 File Offset: 0x001D8034
		public static bool? IsSSLRequired(ADExchangeServiceVirtualDirectory virtualDirectory, Task.TaskErrorLoggingDelegate errorHandler)
		{
			bool? result = null;
			try
			{
				using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(virtualDirectory.MetabasePath))
				{
					int? num = (int?)directoryEntry.Properties["AccessSSLFlags"].Value;
					if (num != null)
					{
						result = new bool?((num.Value & 8) == 8);
					}
				}
			}
			catch (DataSourceTransientException exception)
			{
				errorHandler(exception, ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			catch (DataSourceOperationException exception2)
			{
				errorHandler(exception2, ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			catch (COMException exception3)
			{
				errorHandler(exception3, ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			return result;
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x001D9F08 File Offset: 0x001D8108
		public static void SetSSLRequired(ADExchangeServiceVirtualDirectory virtualDirectory, Task.TaskErrorLoggingDelegate errorHandler, LocalizedString errorMessage, bool enabled)
		{
			try
			{
				using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(virtualDirectory.MetabasePath))
				{
					if (enabled)
					{
						directoryEntry.Properties["AccessSSLFlags"].Value = (MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSLNegotiateCert | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128);
					}
					else
					{
						directoryEntry.Properties["AccessSSLFlags"].Value = MetabasePropertyTypes.AccessSSLFlags.AccessSSLNegotiateCert;
					}
					directoryEntry.CommitChanges();
					IisUtility.CommitMetabaseChanges((virtualDirectory.Server == null) ? null : virtualDirectory.Server.ToString());
				}
			}
			catch (DataSourceTransientException innerException)
			{
				errorHandler(new LocalizedException(errorMessage, innerException), ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			catch (DataSourceOperationException innerException2)
			{
				errorHandler(new LocalizedException(errorMessage, innerException2), ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
			catch (COMException innerException3)
			{
				errorHandler(new LocalizedException(errorMessage, innerException3), ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
		}

		// Token: 0x06007450 RID: 29776 RVA: 0x001DA00C File Offset: 0x001D820C
		internal static void ForceAnonymous(string metabasePath)
		{
			ExchangeServiceVDirHelper.ConfigureAnonymousAuthentication(metabasePath, true);
		}

		// Token: 0x06007451 RID: 29777 RVA: 0x001DA018 File Offset: 0x001D8218
		internal static void ConfigureAnonymousAuthentication(string metabasePath, bool enableAnonymous)
		{
			try
			{
				DirectoryEntry directoryEntry2;
				DirectoryEntry directoryEntry = directoryEntry2 = IisUtility.CreateIISDirectoryEntry(metabasePath);
				try
				{
					IisUtility.SetAuthenticationMethod(directoryEntry, MetabasePropertyTypes.AuthFlags.Anonymous, enableAnonymous);
					directoryEntry.CommitChanges();
				}
				finally
				{
					if (directoryEntry2 != null)
					{
						((IDisposable)directoryEntry2).Dispose();
					}
				}
			}
			catch (IISGeneralCOMException ex)
			{
				if (ex.Code == -2147023174)
				{
					throw new IISNotReachableException(IisUtility.GetHostName(metabasePath), ex.Message);
				}
				throw;
			}
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x001DA088 File Offset: 0x001D8288
		internal static void CheckAndUpdateLocalhostWebBindingsIfNecessary(ADExchangeServiceVirtualDirectory adVDir)
		{
			ExchangeServiceVDirHelper.CheckAndUpdateBindingsIfNecessary(adVDir, ExchangeServiceVDirHelper.LocalHostBindings);
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x001DA095 File Offset: 0x001D8295
		internal static void CheckAndUpdateLocalhostNetPipeBindingsIfNecessary(ADExchangeServiceVirtualDirectory adVDir)
		{
			ExchangeServiceVDirHelper.CheckAndUpdateBindingsIfNecessary(adVDir, ExchangeServiceVDirHelper.NetPipeBindings);
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x001DA0A4 File Offset: 0x001D82A4
		private static void CheckAndUpdateBindingsIfNecessary(ADExchangeServiceVirtualDirectory adVDir, List<ExchangeServiceVDirHelper.WebBinding> bindings)
		{
			DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adVDir.MetabasePath);
			string webSiteName = IisUtility.GetWebSiteName(directoryEntry.Parent.Path);
			using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(adVDir.MetabasePath)))
			{
				Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
				if (applicationHostConfiguration != null)
				{
					ConfigurationSection section = applicationHostConfiguration.GetSection("system.applicationHost/sites");
					if (section != null)
					{
						ConfigurationElementCollection collection = section.GetCollection();
						if (collection != null)
						{
							ConfigurationElement configurationElement = ExchangeServiceVDirHelper.FindSiteElement(collection, webSiteName);
							if (configurationElement != null)
							{
								ConfigurationElementCollection collection2 = configurationElement.GetCollection("bindings");
								bool flag = false;
								foreach (ExchangeServiceVDirHelper.WebBinding binding in bindings)
								{
									if (ExchangeServiceVDirHelper.FindBindingElement(collection2, binding) == null)
									{
										ExchangeServiceVDirHelper.AddBindingElement(collection2, binding);
										flag = true;
									}
								}
								if (flag)
								{
									serverManager.CommitChanges();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x001DA1A0 File Offset: 0x001D83A0
		public static void ExecuteUnderVDirSpecificAssemblyResolvers(Action configurationAction)
		{
			AssemblyResolver[] resolvers = AssemblyResolver.Install(new AssemblyResolver[]
			{
				new FileSearchAssemblyResolver
				{
					FileNameFilter = new Func<string, bool>(AssemblyResolver.ExchangePrefixedAssembliesOnly),
					SearchPaths = new string[]
					{
						ExchangeSetupContext.InstallPath
					},
					Recursive = true
				}
			});
			try
			{
				configurationAction();
			}
			finally
			{
				AssemblyResolver.Uninstall(resolvers);
			}
		}

		// Token: 0x06007456 RID: 29782 RVA: 0x001DA214 File Offset: 0x001D8414
		private static void AddBindingElement(ConfigurationElementCollection bindingsCollection, ExchangeServiceVDirHelper.WebBinding binding)
		{
			ConfigurationElement configurationElement = bindingsCollection.CreateElement("binding");
			configurationElement["protocol"] = binding.Protocol;
			configurationElement["bindingInformation"] = binding.Info;
			bindingsCollection.Add(configurationElement);
		}

		// Token: 0x06007457 RID: 29783 RVA: 0x001DA294 File Offset: 0x001D8494
		private static ConfigurationElement FindBindingElement(ConfigurationElementCollection bindingsCollection, ExchangeServiceVDirHelper.WebBinding binding)
		{
			return ExchangeServiceVDirHelper.FindConfigurationElement(bindingsCollection, "binding", (ConfigurationElement e) => ExchangeServiceVDirHelper.AttributeValueMatches(e, "protocol", binding.Protocol) && ExchangeServiceVDirHelper.AttributeValueMatches(e, "bindingInformation", binding.Info));
		}

		// Token: 0x06007458 RID: 29784 RVA: 0x001DA2E0 File Offset: 0x001D84E0
		private static ConfigurationElement FindSiteElement(ConfigurationElementCollection sitesCollection, string webSiteName)
		{
			return ExchangeServiceVDirHelper.FindConfigurationElement(sitesCollection, "site", (ConfigurationElement e) => ExchangeServiceVDirHelper.AttributeValueMatches(e, "name", webSiteName));
		}

		// Token: 0x06007459 RID: 29785 RVA: 0x001DA314 File Offset: 0x001D8514
		private static ConfigurationElement FindConfigurationElement(ConfigurationElementCollection collection, string elementTagName, Func<ConfigurationElement, bool> predicate)
		{
			foreach (ConfigurationElement configurationElement in collection)
			{
				if (string.Equals(configurationElement.ElementTagName, elementTagName, StringComparison.OrdinalIgnoreCase) && predicate(configurationElement))
				{
					return configurationElement;
				}
			}
			return null;
		}

		// Token: 0x0600745A RID: 29786 RVA: 0x001DA374 File Offset: 0x001D8574
		private static bool AttributeValueMatches(ConfigurationElement element, string attributeName, string attributeValue)
		{
			object attributeValue2 = element.GetAttributeValue(attributeName);
			return attributeValue2 != null && attributeValue2.ToString().Equals(attributeValue);
		}

		// Token: 0x0600745B RID: 29787 RVA: 0x001DA39C File Offset: 0x001D859C
		internal static void SetAuthModule(bool EnableModule, bool isChildVDirApplication, string moduleName, string moduleType, ExchangeVirtualDirectory advdir)
		{
			DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(advdir.MetabasePath);
			using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(advdir.MetabasePath)))
			{
				Configuration webConfiguration;
				if (isChildVDirApplication)
				{
					webConfiguration = serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Parent.Path)].Applications[string.Format("/{0}/{1}", directoryEntry.Parent.Name, directoryEntry.Name)].GetWebConfiguration();
				}
				else
				{
					webConfiguration = serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Path)].Applications["/" + directoryEntry.Name].GetWebConfiguration();
				}
				ConfigurationElementCollection collection = webConfiguration.GetSection("system.webServer/modules").GetCollection();
				int num = Array.IndexOf<string>(ExchangeServiceVDirHelper.OrderedModuleList, moduleName);
				int num2 = ExchangeServiceVDirHelper.OrderedModuleList.Length;
				int[] array = new int[num2];
				ConfigurationElement configurationElement = null;
				foreach (ConfigurationElement configurationElement2 in collection)
				{
					if (num != -1)
					{
						for (int i = 0; i < num2; i++)
						{
							if (string.Equals(configurationElement2.Attributes["name"].Value.ToString(), ExchangeServiceVDirHelper.OrderedModuleList[i], StringComparison.Ordinal))
							{
								array[i] = collection.IndexOf(configurationElement2);
								break;
							}
						}
					}
					if (string.Equals(configurationElement2.Attributes["name"].Value.ToString(), moduleName, StringComparison.Ordinal))
					{
						configurationElement = configurationElement2;
						break;
					}
				}
				if (configurationElement == null && EnableModule)
				{
					int j = collection.Count;
					if (num != -1)
					{
						for (int k = 0; k < num2; k++)
						{
							if (k < num && array[k] != 0)
							{
								j = array[k] + 1;
							}
							else if (k > num && array[k] != 0)
							{
								j = array[k];
								break;
							}
						}
					}
					configurationElement = collection.CreateElement();
					configurationElement.SetAttributeValue("name", moduleName);
					configurationElement.SetAttributeValue("type", moduleType);
					if (j == collection.Count || (j != 0 && collection[j - 1].IsLocallyStored))
					{
						collection.AddAt(j, configurationElement);
					}
					else
					{
						List<ConfigurationElement> list = new List<ConfigurationElement>();
						while (j < collection.Count)
						{
							ConfigurationElement configurationElement3 = collection[j];
							collection.Remove(configurationElement3);
							list.Add(configurationElement3);
						}
						collection.Add(configurationElement);
						foreach (ConfigurationElement configurationElement4 in list)
						{
							ConfigurationElement configurationElement5 = collection.CreateElement(configurationElement4.ElementTagName);
							foreach (ConfigurationAttribute configurationAttribute in configurationElement4.Attributes)
							{
								if (configurationAttribute.Value != null && !configurationAttribute.Value.ToString().Equals(string.Empty))
								{
									configurationElement5.SetAttributeValue(configurationAttribute.Name, configurationAttribute.Value);
								}
							}
							collection.Add(configurationElement5);
						}
					}
					serverManager.CommitChanges();
				}
				else if (configurationElement != null && !EnableModule)
				{
					collection.Remove(configurationElement);
					serverManager.CommitChanges();
				}
			}
		}

		// Token: 0x0600745C RID: 29788 RVA: 0x001DA750 File Offset: 0x001D8950
		internal static void SetLiveIdBasicAuthModule(bool EnableModule, bool isChildVDirApplication, ADExchangeServiceVirtualDirectory advdir)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "LiveIdBasicAuthenticationModule", typeof(LiveIdBasicAuthModule).FullName, advdir);
		}

		// Token: 0x0600745D RID: 29789 RVA: 0x001DA76E File Offset: 0x001D896E
		internal static void SetOAuthAuthenticationModule(bool EnableModule, bool isChildVDirApplication, ExchangeVirtualDirectory advdir)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "OAuthAuthModule", typeof(OAuthHttpModule).FullName, advdir);
		}

		// Token: 0x0600745E RID: 29790 RVA: 0x001DA78C File Offset: 0x001D898C
		internal static void SetAdfsAuthenticationModule(bool EnableModule, bool isChildVDirApplication, ExchangeVirtualDirectory advdir)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "ADFSFederationAuthModule", typeof(AdfsFederationAuthModule).FullName, advdir);
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "ADFSSessionAuthModule", typeof(AdfsSessionAuthModule).FullName, advdir);
		}

		// Token: 0x0600745F RID: 29791 RVA: 0x001DA7C6 File Offset: 0x001D89C6
		internal static bool IsBackEndVirtualDirectory(ExchangeVirtualDirectory adVirtualDirectory)
		{
			return adVirtualDirectory.Name.EndsWith("(Exchange Back End)", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x001DA7DC File Offset: 0x001D89DC
		internal static void UpdateFrontEndHttpModule(ExchangeVirtualDirectory advdir, bool enableFba)
		{
			DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(advdir.MetabasePath);
			using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(advdir.MetabasePath)))
			{
				Configuration webConfiguration = serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Path)].Applications["/" + directoryEntry.Name].GetWebConfiguration();
				ConfigurationElementCollection collection = webConfiguration.GetSection("system.webServer/modules").GetCollection();
				foreach (ConfigurationElement configurationElement in collection)
				{
					if (string.Equals(configurationElement.Attributes["name"].Value.ToString(), "HttpProxy", StringComparison.OrdinalIgnoreCase))
					{
						if (enableFba)
						{
							configurationElement.SetAttributeValue("type", "Microsoft.Exchange.HttpProxy.FbaModule,Microsoft.Exchange.FrontEndHttpProxy,Version=15.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35");
						}
						else
						{
							configurationElement.SetAttributeValue("type", "Microsoft.Exchange.HttpProxy.ProxyModule,Microsoft.Exchange.FrontEndHttpProxy,Version=15.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35");
						}
					}
				}
				serverManager.CommitChanges();
			}
		}

		// Token: 0x06007461 RID: 29793 RVA: 0x001DA8FC File Offset: 0x001D8AFC
		internal static void RunAppcmd(string args)
		{
			string text = Path.Combine(new string[]
			{
				Environment.GetEnvironmentVariable("windir"),
				"system32",
				"inetsrv"
			});
			string text2 = Path.Combine(text, "appcmd.exe");
			if (!File.Exists(text2))
			{
				throw new AppcmdException(Strings.AppcmdNotFoundInPath(text2));
			}
			string text3;
			string error;
			int num = ProcessRunner.Run(text2, args, -1, text, out text3, out error);
			if (num != 0)
			{
				throw new AppcmdException(Strings.AppcmdExecutionFailed(num, error));
			}
		}

		// Token: 0x04003AF5 RID: 15093
		public const string BasicAuthenticationField = "BasicAuthentication";

		// Token: 0x04003AF6 RID: 15094
		public const string DigestAuthenticationField = "DigestAuthentication";

		// Token: 0x04003AF7 RID: 15095
		public const string WindowsAuthenticationField = "WindowsAuthentication";

		// Token: 0x04003AF8 RID: 15096
		public const string LiveIdBasicAuthenticationField = "LiveIdBasicAuthentication";

		// Token: 0x04003AF9 RID: 15097
		public const string LiveIdBasicAuthenticationModule = "LiveIdBasicAuthenticationModule";

		// Token: 0x04003AFA RID: 15098
		public const string LiveIdNegotiateAuthenticationField = "LiveIdNegotiateAuthentication";

		// Token: 0x04003AFB RID: 15099
		public const string OAuthAuthenticationField = "OAuthAuthentication";

		// Token: 0x04003AFC RID: 15100
		public const string LiveIdNegotiateAuxiliaryModule = "LiveIdNegotiateAuxiliaryModule";

		// Token: 0x04003AFD RID: 15101
		public const string DelegatedAuthenticationModule = "DelegatedAuthModule";

		// Token: 0x04003AFE RID: 15102
		public const string OAuthAuthenticationModule = "OAuthAuthModule";

		// Token: 0x04003AFF RID: 15103
		public const string PowerShellRequestFilterModule = "PowerShellRequestFilterModule";

		// Token: 0x04003B00 RID: 15104
		public const string CertificateHeaderAuthenticationModule = "CertificateHeaderAuthModule";

		// Token: 0x04003B01 RID: 15105
		public const string SessionKeyRedirectionModule = "SessionKeyRedirectionModule";

		// Token: 0x04003B02 RID: 15106
		public const string CertificateAuthenticationModule = "CertificateAuthModule";

		// Token: 0x04003B03 RID: 15107
		public const string LiveIdAuthenticationModule = "LiveIdAuthenticationModule";

		// Token: 0x04003B04 RID: 15108
		public const string ADFSFederationAuthModule = "ADFSFederationAuthModule";

		// Token: 0x04003B05 RID: 15109
		public const string ADFSSessionAuthModule = "ADFSSessionAuthModule";

		// Token: 0x04003B06 RID: 15110
		public const string BackendRehydrationModule = "BackendRehydrationModule";

		// Token: 0x04003B07 RID: 15111
		public const string WSSecurityAuthenticationField = "WSSecurityAuthentication";

		// Token: 0x04003B08 RID: 15112
		public const string CertificateAuthenticationField = "CertificateAuthentication";

		// Token: 0x04003B09 RID: 15113
		public const string ExtendedProtectionTokenCheckingField = "ExtendedProtectionTokenChecking";

		// Token: 0x04003B0A RID: 15114
		public const string ExtendedProtectionFlagsField = "ExtendedProtectionFlags";

		// Token: 0x04003B0B RID: 15115
		public const string ExtendedProtectionSpnListField = "ExtendedProtectionSPNList";

		// Token: 0x04003B0C RID: 15116
		public const string WindowsAuthenticationSectionInAppHostConfig = "system.webServer/security/authentication/windowsAuthentication";

		// Token: 0x04003B0D RID: 15117
		public const string BackEndWebSiteName = "Exchange Back End";

		// Token: 0x04003B0E RID: 15118
		private const string BackEndWebSiteNameInParens = "(Exchange Back End)";

		// Token: 0x04003B0F RID: 15119
		private const string ProvidersCollectionName = "providers";

		// Token: 0x04003B10 RID: 15120
		private const string NegotiateProviderName = "Negotiate";

		// Token: 0x04003B11 RID: 15121
		private const string ValueAttributeName = "value";

		// Token: 0x04003B12 RID: 15122
		private const string BindingElementName = "binding";

		// Token: 0x04003B13 RID: 15123
		private const string BindingsElementName = "bindings";

		// Token: 0x04003B14 RID: 15124
		private const string SiteElementName = "site";

		// Token: 0x04003B15 RID: 15125
		private const string BindingInformationAttributeName = "bindingInformation";

		// Token: 0x04003B16 RID: 15126
		private const string ProtocolAttributeName = "protocol";

		// Token: 0x04003B17 RID: 15127
		private const string LocalHostIPv4 = "127.0.0.1";

		// Token: 0x04003B18 RID: 15128
		private const string LocalHostIPv6 = "[::1]";

		// Token: 0x04003B19 RID: 15129
		private const string HttpProtocol = "http";

		// Token: 0x04003B1A RID: 15130
		private const string HttpPortBinding = ":80:";

		// Token: 0x04003B1B RID: 15131
		private const string HttpsProtocol = "https";

		// Token: 0x04003B1C RID: 15132
		private const string HttpsPortBinding = ":443:";

		// Token: 0x04003B1D RID: 15133
		private const string NetPipeProtocol = "net.pipe";

		// Token: 0x04003B1E RID: 15134
		private const string NetPipeBindingInfo = "*";

		// Token: 0x04003B1F RID: 15135
		public static string[] OrderedModuleList = new string[]
		{
			"CertificateAuthModule",
			"CertificateHeaderAuthModule",
			"LiveIdBasicAuthenticationModule",
			"LiveIdNegotiateAuxiliaryModule",
			"DelegatedAuthModule",
			"OAuthAuthModule",
			"SessionKeyRedirectionModule",
			"LiveIdAuthenticationModule",
			"ADFSFederationAuthModule",
			"ADFSSessionAuthModule",
			"BackendRehydrationModule"
		};

		// Token: 0x04003B20 RID: 15136
		private static List<ExchangeServiceVDirHelper.WebBinding> LocalHostBindings = new List<ExchangeServiceVDirHelper.WebBinding>
		{
			new ExchangeServiceVDirHelper.WebBinding
			{
				Protocol = "http",
				Info = "127.0.0.1:80:"
			},
			new ExchangeServiceVDirHelper.WebBinding
			{
				Protocol = "https",
				Info = "127.0.0.1:443:"
			}
		};

		// Token: 0x04003B21 RID: 15137
		private static List<ExchangeServiceVDirHelper.WebBinding> NetPipeBindings = new List<ExchangeServiceVDirHelper.WebBinding>
		{
			new ExchangeServiceVDirHelper.WebBinding
			{
				Protocol = "net.pipe",
				Info = "*"
			}
		};

		// Token: 0x02000C05 RID: 3077
		internal static class EwsAutodiscMWA
		{
			// Token: 0x06007463 RID: 29795 RVA: 0x001DAA84 File Offset: 0x001D8C84
			private static ConfigurationElement TryFindServiceByName(ConfigurationElementCollection services, string name)
			{
				if (services != null)
				{
					foreach (ConfigurationElement configurationElement in services)
					{
						if (string.Equals(configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.NameAttribute] as string, name, StringComparison.OrdinalIgnoreCase))
						{
							return configurationElement;
						}
					}
				}
				return null;
			}

			// Token: 0x06007464 RID: 29796 RVA: 0x001DAAE8 File Offset: 0x001D8CE8
			private static ConfigurationElement TryFindEndpointByBindingConfiguration(ConfigurationElementCollection endpoints, string bindingConfiguration)
			{
				foreach (ConfigurationElement configurationElement in endpoints)
				{
					if (string.Equals(configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBindingConfiguration] as string, bindingConfiguration, StringComparison.OrdinalIgnoreCase))
					{
						return configurationElement;
					}
				}
				return null;
			}

			// Token: 0x06007465 RID: 29797 RVA: 0x001DAB4C File Offset: 0x001D8D4C
			private static ConfigurationElement TryFindEndpointByNameAndContract(ConfigurationElementCollection endpoints, string name, string contract)
			{
				foreach (ConfigurationElement configurationElement in endpoints)
				{
					if (string.Equals(configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName] as string, name, StringComparison.OrdinalIgnoreCase) && string.Equals(configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointContract] as string, contract, StringComparison.OrdinalIgnoreCase))
					{
						return configurationElement;
					}
				}
				return null;
			}

			// Token: 0x06007466 RID: 29798 RVA: 0x001DABC8 File Offset: 0x001D8DC8
			private static void EnableOrDisableWSSecurityEndpoint(Configuration configuration, bool enableEndpoint, Task.TaskErrorLoggingDelegate errorHandler, bool isEWS)
			{
				string name = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSServiceName : ExchangeServiceVDirHelper.EwsAutodiscMWA.AutoDServiceName;
				string text = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSWSSecurityHttpBinding : ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSecurityHttpBinding;
				string text2 = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSWSSecurityHttpsBinding : ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSecurityHttpsBinding;
				string text3 = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSWSSecuritySymmetricKeyHttpBinding : ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSecuritySymmetricKeyHttpBinding;
				string text4 = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSWSSecuritySymmetricKeyHttpsBinding : ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSecuritySymmetricKeyHttpsBinding;
				string text5 = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSWSSecurityX509CertHttpBinding : ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSecurityX509CertHttpBinding;
				string text6 = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSWSSecurityX509CertHttpsBinding : ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSecurityX509CertHttpsBinding;
				string contract = isEWS ? ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSContract : ExchangeServiceVDirHelper.EwsAutodiscMWA.AutoDContract;
				ConfigurationSection section = configuration.GetSection(ExchangeServiceVDirHelper.EwsAutodiscMWA.ServicesSectionName);
				ConfigurationElement configurationElement = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindServiceByName(section.GetCollection(), name);
				if (configurationElement == null)
				{
					errorHandler(new LocalizedException(isEWS ? Strings.CouldNotFindEWSService : Strings.CouldNotFindAutodiscoverService, new ArgumentNullException("serviceElement")), ErrorCategory.InvalidOperation, null);
				}
				ConfigurationElementCollection collection = configurationElement.GetCollection();
				if (collection == null)
				{
					return;
				}
				ConfigurationElement configurationElement2 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByBindingConfiguration(collection, text);
				ConfigurationElement configurationElement3 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByBindingConfiguration(collection, text2);
				ConfigurationElement configurationElement4 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByBindingConfiguration(collection, text3);
				ConfigurationElement configurationElement5 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByBindingConfiguration(collection, text4);
				ConfigurationElement configurationElement6 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByBindingConfiguration(collection, text5);
				ConfigurationElement configurationElement7 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByBindingConfiguration(collection, text6);
				if (!enableEndpoint)
				{
					if (configurationElement2 != null)
					{
						collection.Remove(configurationElement2);
					}
					if (configurationElement3 != null)
					{
						collection.Remove(configurationElement3);
					}
					if (configurationElement4 != null)
					{
						collection.Remove(configurationElement4);
					}
					if (configurationElement5 != null)
					{
						collection.Remove(configurationElement5);
					}
					if (configurationElement6 != null)
					{
						collection.Remove(configurationElement6);
					}
					if (configurationElement7 != null)
					{
						collection.Remove(configurationElement7);
						return;
					}
				}
				else
				{
					if (configurationElement2 == null)
					{
						ExchangeServiceVDirHelper.EwsAutodiscMWA.AddEndpointElement(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSEndpointUri, text, contract);
					}
					if (configurationElement3 == null)
					{
						ExchangeServiceVDirHelper.EwsAutodiscMWA.AddEndpointElement(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSEndpointUri, text2, contract);
					}
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.WsSecuritySymmetricAndX509Cert.Enabled)
					{
						if (configurationElement4 == null)
						{
							ExchangeServiceVDirHelper.EwsAutodiscMWA.AddEndpointElement(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSSymmetricKeyEndpointUri, text3, contract);
						}
						if (configurationElement5 == null)
						{
							ExchangeServiceVDirHelper.EwsAutodiscMWA.AddEndpointElement(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSSymmetricKeyEndpointUri, text4, contract);
						}
						if (configurationElement6 == null)
						{
							ExchangeServiceVDirHelper.EwsAutodiscMWA.AddEndpointElement(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSX509CertEndpointUri, text5, contract);
						}
						if (configurationElement7 == null)
						{
							ExchangeServiceVDirHelper.EwsAutodiscMWA.AddEndpointElement(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.WSSX509CertEndpointUri, text6, contract);
						}
					}
				}
			}

			// Token: 0x06007467 RID: 29799 RVA: 0x001DADD8 File Offset: 0x001D8FD8
			private static void EnableOrDisableCafeEndpoint(Configuration configuration, string endpointName, bool enableEndpoint)
			{
				string text = enableEndpoint.ToString(CultureInfo.InvariantCulture);
				ConfigurationElement configurationElement = null;
				ConfigurationSection section = configuration.GetSection("appSettings");
				ConfigurationElementCollection collection = section.GetCollection();
				if (collection != null)
				{
					foreach (ConfigurationElement configurationElement2 in collection)
					{
						if (configurationElement2["key"].ToString().Equals(endpointName, StringComparison.OrdinalIgnoreCase))
						{
							configurationElement = configurationElement2;
							break;
						}
					}
				}
				if (configurationElement == null)
				{
					configurationElement = collection.CreateElement("add");
					configurationElement["key"] = endpointName;
					configurationElement["value"] = text;
					collection.Add(configurationElement);
					return;
				}
				configurationElement["value"] = text;
			}

			// Token: 0x06007468 RID: 29800 RVA: 0x001DAEA0 File Offset: 0x001D90A0
			private static void AddEndpointElement(ConfigurationElementCollection endpoints, Uri endpointUri, string httpsEndpointBindingConfiguration, string contract)
			{
				ConfigurationElement configurationElement = endpoints.CreateElement(ExchangeServiceVDirHelper.EwsAutodiscMWA.Endpoint);
				configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointAddress] = endpointUri.ToString();
				configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBinding] = ExchangeServiceVDirHelper.EwsAutodiscMWA.CustomBindingString;
				configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBindingConfiguration] = httpsEndpointBindingConfiguration;
				configurationElement[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointContract] = contract;
				endpoints.Add(configurationElement);
			}

			// Token: 0x06007469 RID: 29801 RVA: 0x001DAEFC File Offset: 0x001D90FC
			private static bool GetAuthenticationMethodSetting(ExchangeVirtualDirectory adVirtualDirectory, AuthenticationMethodFlags authMethod)
			{
				bool result;
				using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adVirtualDirectory.MetabasePath))
				{
					if (authMethod == AuthenticationMethodFlags.WSSecurity)
					{
						result = (adVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.WSSecurity) && IisUtility.CheckForAuthenticationMethod(directoryEntry, authMethod));
					}
					else if (authMethod == AuthenticationMethodFlags.OAuth)
					{
						result = (adVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.OAuth) && IisUtility.CheckForAuthenticationMethod(directoryEntry, authMethod));
					}
					else
					{
						result = IisUtility.CheckForAuthenticationMethod(directoryEntry, authMethod);
					}
				}
				return result;
			}

			// Token: 0x0600746A RID: 29802 RVA: 0x001DAF84 File Offset: 0x001D9184
			internal static void OnSetManageWCFEndpoints(Task task, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol protocol, bool enableWSSecurity, ExchangeVirtualDirectory adVirtualDirectory)
			{
				try
				{
					using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adVirtualDirectory.MetabasePath))
					{
						using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(adVirtualDirectory.MetabasePath)))
						{
							Configuration webConfiguration = serverManager.GetWebConfiguration(IisUtility.GetWebSiteName(directoryEntry.Parent.Path), "/" + directoryEntry.Name);
							bool flag = task.Fields["WSSecurityAuthentication"] != null;
							bool flag2 = task.Fields["OAuthAuthentication"] != null;
							if (ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(adVirtualDirectory))
							{
								if (flag || flag2)
								{
									if (flag)
									{
										ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableWSSecurityEndpoint(webConfiguration, enableWSSecurity, new Task.TaskErrorLoggingDelegate(task.WriteError), protocol == ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.Ews);
									}
									serverManager.CommitChanges();
								}
							}
							else if (flag)
							{
								ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableCafeEndpoint(webConfiguration, "WsSecurityEndpointEnabled", enableWSSecurity);
								if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.WsSecuritySymmetricAndX509Cert.Enabled)
								{
									ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableCafeEndpoint(webConfiguration, "WsSecuritySymmetricKeyEndpointEnabled", enableWSSecurity);
									ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableCafeEndpoint(webConfiguration, "WsSecurityX509CertEndpointEnabled", enableWSSecurity);
								}
								serverManager.CommitChanges();
							}
						}
					}
				}
				catch (ServerManagerException exception)
				{
					task.WriteError(exception, ErrorCategory.InvalidData, adVirtualDirectory.Identity);
				}
			}

			// Token: 0x0600746B RID: 29803 RVA: 0x001DB0FC File Offset: 0x001D92FC
			internal static void OnNewManageWCFEndpoints(Task task, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol protocol, bool? basicAuthentication, bool? windowsAuthentication, bool enableWSSecurity, bool enableOAuth, ExchangeVirtualDirectory adVirtualDirectory, VirtualDirectoryRole role)
			{
				try
				{
					using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adVirtualDirectory.MetabasePath))
					{
						using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(adVirtualDirectory.MetabasePath)))
						{
							Configuration webConfiguration = serverManager.GetWebConfiguration(IisUtility.GetWebSiteName(directoryEntry.Parent.Path), "/" + directoryEntry.Name);
							if (role == VirtualDirectoryRole.ClientAccess)
							{
								ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableCafeEndpoint(webConfiguration, "WsSecurityEndpointEnabled", enableWSSecurity);
								if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.WsSecuritySymmetricAndX509Cert.Enabled)
								{
									ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableCafeEndpoint(webConfiguration, "WsSecuritySymmetricKeyEndpointEnabled", enableWSSecurity);
									ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableCafeEndpoint(webConfiguration, "WsSecurityX509CertEndpointEnabled", enableWSSecurity);
								}
							}
							else if (protocol != ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.OwaEws)
							{
								ExchangeServiceVDirHelper.EwsAutodiscMWA.EnableOrDisableWSSecurityEndpoint(webConfiguration, enableWSSecurity, new Task.TaskErrorLoggingDelegate(task.WriteError), protocol == ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.Ews);
							}
							serverManager.CommitChanges();
						}
					}
				}
				catch (ServerManagerException exception)
				{
					task.WriteError(exception, ErrorCategory.InvalidData, adVirtualDirectory.Identity);
				}
			}

			// Token: 0x0600746C RID: 29804 RVA: 0x001DB214 File Offset: 0x001D9414
			private static void UpgradeAutodiscoverEndpoints(Configuration configuration, AuthenticationSchemes schemeToEnable, Task.TaskErrorLoggingDelegate errorHandler)
			{
				ExchangeServiceVDirHelper.EwsAutodiscMWA.UpgradeAutoDiscoverEndpoints(configuration, ExchangeServiceVDirHelper.EwsAutodiscMWA.ServiceNameLegacyAutoD, ExchangeServiceVDirHelper.EwsAutodiscMWA.LegacyAutoDContract, "Autodiscover", schemeToEnable, errorHandler);
				ExchangeServiceVDirHelper.EwsAutodiscMWA.UpgradeAutoDiscoverEndpoints(configuration, ExchangeServiceVDirHelper.EwsAutodiscMWA.AutoDServiceName, ExchangeServiceVDirHelper.EwsAutodiscMWA.AutoDContract, "AutodiscoverSoap", schemeToEnable, errorHandler);
			}

			// Token: 0x0600746D RID: 29805 RVA: 0x001DB244 File Offset: 0x001D9444
			private static void UpgradeAutoDiscoverEndpoints(Configuration configuration, string serviceName, string contract, string bindingNameRoot, AuthenticationSchemes schemeToEnable, Task.TaskErrorLoggingDelegate errorHandler)
			{
				string str = schemeToEnable.ToString();
				ConfigurationElement section = configuration.GetSection(ExchangeServiceVDirHelper.EwsAutodiscMWA.ServicesSectionName);
				ConfigurationElement configurationElement = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindServiceByName(section.GetCollection(), serviceName);
				if (configurationElement == null)
				{
					errorHandler(new LocalizedException(Strings.CouldNotFindElementWithAttribute(ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointService, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName, serviceName), new ArgumentNullException("serviceElement")), ErrorCategory.InvalidOperation, null);
				}
				ConfigurationElementCollection collection = configurationElement.GetCollection();
				if (collection == null)
				{
					return;
				}
				ConfigurationElement configurationElement2 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByNameAndContract(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpsEndpointName, contract);
				if (configurationElement2 == null)
				{
					errorHandler(new LocalizedException(Strings.CouldNotFindElementWithTwoAttributes(ExchangeServiceVDirHelper.EwsAutodiscMWA.Endpoint, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpsEndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointContract, contract)), ErrorCategory.InvalidOperation, null);
				}
				configurationElement2[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBindingConfiguration] = bindingNameRoot + str + ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpsBindingSuffix;
				ConfigurationElement configurationElement3 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByNameAndContract(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpEndpointName, contract);
				if (configurationElement3 == null)
				{
					errorHandler(new LocalizedException(Strings.CouldNotFindElementWithTwoAttributes(ExchangeServiceVDirHelper.EwsAutodiscMWA.Endpoint, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpEndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointContract, contract)), ErrorCategory.InvalidOperation, null);
				}
				configurationElement3[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBindingConfiguration] = bindingNameRoot + str + ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpBindingSuffix;
			}

			// Token: 0x0600746E RID: 29806 RVA: 0x001DB354 File Offset: 0x001D9554
			private static void UpdateEWSEndpoints(Configuration configuration, AuthenticationSchemes schemeToEnable, Task.TaskErrorLoggingDelegate errorHandler)
			{
				string str = "EWS";
				string str2 = schemeToEnable.ToString();
				string ewsserviceName = ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSServiceName;
				ConfigurationSection section = configuration.GetSection(ExchangeServiceVDirHelper.EwsAutodiscMWA.ServicesSectionName);
				if (section == null)
				{
					return;
				}
				ConfigurationElement configurationElement = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindServiceByName(section.GetCollection(), ewsserviceName);
				if (configurationElement == null)
				{
					errorHandler(new LocalizedException(Strings.CouldNotFindElementWithAttribute(ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointService, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName, ewsserviceName), new ArgumentNullException("serviceElement")), ErrorCategory.InvalidOperation, null);
				}
				ConfigurationElementCollection collection = configurationElement.GetCollection();
				if (collection == null)
				{
					return;
				}
				ConfigurationElement configurationElement2 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByNameAndContract(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpsEndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSContract);
				if (configurationElement2 == null)
				{
					errorHandler(new LocalizedException(Strings.CouldNotFindElementWithTwoAttributes(ExchangeServiceVDirHelper.EwsAutodiscMWA.Endpoint, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpsEndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointContract, ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSContract)), ErrorCategory.InvalidOperation, null);
				}
				configurationElement2[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBindingConfiguration] = str + str2 + ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpsBindingSuffix;
				ConfigurationElement configurationElement3 = ExchangeServiceVDirHelper.EwsAutodiscMWA.TryFindEndpointByNameAndContract(collection, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpEndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSContract);
				if (configurationElement3 == null)
				{
					errorHandler(new LocalizedException(Strings.CouldNotFindElementWithTwoAttributes(ExchangeServiceVDirHelper.EwsAutodiscMWA.Endpoint, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpEndpointName, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointContract, ExchangeServiceVDirHelper.EwsAutodiscMWA.EWSContract)), ErrorCategory.InvalidOperation, null);
				}
				configurationElement3[ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointBindingConfiguration] = str + str2 + ExchangeServiceVDirHelper.EwsAutodiscMWA.HttpBindingSuffix;
			}

			// Token: 0x04003B22 RID: 15138
			private const string CertificateEndpointBindingConfiguration = "emwsCustomCertificateConfiguration";

			// Token: 0x04003B23 RID: 15139
			private const string CertificateBasicEndpointBindingConfiguration = "emwsBasicCertificateConfiguration";

			// Token: 0x04003B24 RID: 15140
			private const string WsSecurityEnabledKey = "WsSecurityEndpointEnabled";

			// Token: 0x04003B25 RID: 15141
			private const string WsSecuritySymmetricKeyEnabledKey = "WsSecuritySymmetricKeyEndpointEnabled";

			// Token: 0x04003B26 RID: 15142
			private const string WsSecurityX509CertEnabledKey = "WsSecurityX509CertEndpointEnabled";

			// Token: 0x04003B27 RID: 15143
			private static readonly Uri StandardEndpointUri = new Uri(string.Empty, UriKind.Relative);

			// Token: 0x04003B28 RID: 15144
			private static readonly Uri WSSEndpointUri = new Uri("wssecurity", UriKind.Relative);

			// Token: 0x04003B29 RID: 15145
			private static readonly Uri WSSSymmetricKeyEndpointUri = new Uri("wssecurity/symmetrickey", UriKind.Relative);

			// Token: 0x04003B2A RID: 15146
			private static readonly Uri WSSX509CertEndpointUri = new Uri("wssecurity/x509cert", UriKind.Relative);

			// Token: 0x04003B2B RID: 15147
			private static readonly string ServicesSectionName = "system.serviceModel/services";

			// Token: 0x04003B2C RID: 15148
			private static readonly string EWSWSSecurityHttpBinding = "EWSWSSecurityHttpBinding";

			// Token: 0x04003B2D RID: 15149
			private static readonly string EWSWSSecurityHttpsBinding = "EWSWSSecurityHttpsBinding";

			// Token: 0x04003B2E RID: 15150
			private static readonly string EWSWSSecuritySymmetricKeyHttpBinding = "EWSWSSecuritySymmetricKeyHttpBinding";

			// Token: 0x04003B2F RID: 15151
			private static readonly string EWSWSSecuritySymmetricKeyHttpsBinding = "EWSWSSecuritySymmetricKeyHttpsBinding";

			// Token: 0x04003B30 RID: 15152
			private static readonly string EWSWSSecurityX509CertHttpBinding = "EWSWSSecurityX509CertHttpBinding";

			// Token: 0x04003B31 RID: 15153
			private static readonly string EWSWSSecurityX509CertHttpsBinding = "EWSWSSecurityX509CertHttpsBinding";

			// Token: 0x04003B32 RID: 15154
			private static readonly string WSSecurityHttpBinding = "WSSecurityHttpBinding";

			// Token: 0x04003B33 RID: 15155
			private static readonly string WSSecurityHttpsBinding = "WSSecurityHttpsBinding";

			// Token: 0x04003B34 RID: 15156
			private static readonly string WSSecuritySymmetricKeyHttpBinding = "WSSecuritySymmetricKeyHttpBinding";

			// Token: 0x04003B35 RID: 15157
			private static readonly string WSSecuritySymmetricKeyHttpsBinding = "WSSecuritySymmetricKeyHttpsBinding";

			// Token: 0x04003B36 RID: 15158
			private static readonly string WSSecurityX509CertHttpBinding = "WSSecurityX509CertHttpBinding";

			// Token: 0x04003B37 RID: 15159
			private static readonly string WSSecurityX509CertHttpsBinding = "WSSecurityX509CertHttpsBinding";

			// Token: 0x04003B38 RID: 15160
			private static readonly string EWSServiceName = "Microsoft.Exchange.Services.Wcf.EWSService";

			// Token: 0x04003B39 RID: 15161
			private static readonly string AutoDServiceName = "Microsoft.Exchange.Autodiscover.WCF.AutodiscoverService";

			// Token: 0x04003B3A RID: 15162
			private static readonly string ServiceNameLegacyAutoD = "Microsoft.Exchange.Autodiscover.WCF.LegacyAutodiscoverService";

			// Token: 0x04003B3B RID: 15163
			private static readonly string EWSContract = "Microsoft.Exchange.Services.Wcf.IEWSContract";

			// Token: 0x04003B3C RID: 15164
			private static readonly string AutoDContract = "Microsoft.Exchange.Autodiscover.WCF.IAutodiscover";

			// Token: 0x04003B3D RID: 15165
			private static readonly string LegacyAutoDContract = "Microsoft.Exchange.Autodiscover.WCF.ILegacyAutodiscover";

			// Token: 0x04003B3E RID: 15166
			private static readonly string HttpEndpointName = "Http";

			// Token: 0x04003B3F RID: 15167
			private static readonly string HttpsEndpointName = "Https";

			// Token: 0x04003B40 RID: 15168
			private static readonly string HttpBindingSuffix = "HttpBinding";

			// Token: 0x04003B41 RID: 15169
			private static readonly string HttpsBindingSuffix = "HttpsBinding";

			// Token: 0x04003B42 RID: 15170
			private static readonly string CustomBindingString = "customBinding";

			// Token: 0x04003B43 RID: 15171
			private static readonly string Endpoint = "endpoint";

			// Token: 0x04003B44 RID: 15172
			private static readonly string EndpointBindingConfiguration = "bindingConfiguration";

			// Token: 0x04003B45 RID: 15173
			private static readonly string EndpointName = "name";

			// Token: 0x04003B46 RID: 15174
			private static readonly string EndpointContract = "contract";

			// Token: 0x04003B47 RID: 15175
			private static readonly string EndpointAddress = "address";

			// Token: 0x04003B48 RID: 15176
			private static readonly string EndpointBinding = "binding";

			// Token: 0x04003B49 RID: 15177
			private static readonly string EndpointService = "service";

			// Token: 0x04003B4A RID: 15178
			private static readonly string NameAttribute = "name";

			// Token: 0x04003B4B RID: 15179
			private static readonly Uri CertificateEndpointUri = new Uri("Certificate", UriKind.Relative);

			// Token: 0x04003B4C RID: 15180
			private static readonly Uri CertificateBasicEndpointUri = new Uri("basicHttpCertificate", UriKind.Relative);

			// Token: 0x04003B4D RID: 15181
			private static AuthenticationSchemes[] AnonymousScheme = new AuthenticationSchemes[]
			{
				AuthenticationSchemes.Anonymous
			};

			// Token: 0x04003B4E RID: 15182
			private static AuthenticationSchemes[] AnonymousBasicNegotiateSchemes = new AuthenticationSchemes[]
			{
				AuthenticationSchemes.Anonymous,
				AuthenticationSchemes.Basic,
				AuthenticationSchemes.Negotiate
			};

			// Token: 0x02000C06 RID: 3078
			internal enum EndpointProtocol
			{
				// Token: 0x04003B50 RID: 15184
				Autodiscover,
				// Token: 0x04003B51 RID: 15185
				Ews,
				// Token: 0x04003B52 RID: 15186
				OwaEws
			}
		}

		// Token: 0x02000C07 RID: 3079
		private class WebBinding
		{
			// Token: 0x04003B53 RID: 15187
			public string Protocol;

			// Token: 0x04003B54 RID: 15188
			public string Info;
		}
	}
}
