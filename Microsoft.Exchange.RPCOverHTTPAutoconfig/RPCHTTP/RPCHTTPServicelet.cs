using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.RPCHTTP.Messages;
using Microsoft.Web.Administration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x02000006 RID: 6
	public class RPCHTTPServicelet : Servicelet
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000024E0 File Offset: 0x000006E0
		private static void AddFqdnToValidPorts(StringBuilder sb, string fqdn, string[] validPortTemplates)
		{
			int num = fqdn.IndexOf('.');
			if (num != -1 && num > 0)
			{
				string machineName = fqdn.Substring(0, num);
				RPCHTTPServicelet.AddMachineNameToValidPorts(sb, machineName, validPortTemplates);
			}
			RPCHTTPServicelet.AddMachineNameToValidPorts(sb, fqdn, validPortTemplates);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002518 File Offset: 0x00000718
		private static void AddMachineNameToValidPorts(StringBuilder sb, string machineName, string[] validPortTemplates)
		{
			foreach (string format in validPortTemplates)
			{
				sb.AppendFormat(format, machineName);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002544 File Offset: 0x00000744
		private static string ComputeValidPortsRegkey(IEnumerable<string> fqdns, params string[] validPortTemplates)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RPCHTTPServicelet.AddMachineNameToValidPorts(stringBuilder, "localhost", validPortTemplates);
			string[] array = fqdns.ToArray<string>();
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			foreach (string fqdn in array)
			{
				RPCHTTPServicelet.AddFqdnToValidPorts(stringBuilder, fqdn, validPortTemplates);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025B8 File Offset: 0x000007B8
		private ADRpcHttpVirtualDirectory FindADRpcHttpVirtualDirectory()
		{
			Server server = this.session.FindLocalServer();
			if (server != null)
			{
				ADRpcHttpVirtualDirectory[] array = this.session.Find<ADRpcHttpVirtualDirectory>((ADObjectId)server.Identity, QueryScope.SubTree, null, null, 1);
				if (array != null && array.Length > 0)
				{
					return array[0];
				}
			}
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025FD File Offset: 0x000007FD
		private void RemoveRpcProxyLbsSettings()
		{
			RegistryUtilities.RemoveRegValue("Software\\Microsoft\\Rpc\\RpcProxy", "RedirectorDLL_EXRDRLBS");
			RegistryUtilities.RemoveRegValue("Software\\Microsoft\\Rpc\\RpcProxy", "RedirectorDLL_EXRDRLBSCallOrder");
			RegistryUtilities.RemoveRegkey(string.Format("{0}\\{1:D}", "Software\\Microsoft\\Rpc\\RpcProxy\\LBSConfiguration", RPCHTTPServicelet.CasResourcePool));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000263C File Offset: 0x0000083C
		private bool UpdateAuthenticationSettings(ServerManager serverManager, string siteName, string virtualPath, ADRpcHttpVirtualDirectory adRpcVdir)
		{
			string text = siteName + virtualPath;
			MultiValuedProperty<AuthenticationMethod> iisauthenticationMethods = adRpcVdir.IISAuthenticationMethods;
			bool flag = IISConfigurationUtilities.UpdateAuthenticationSettings(serverManager, siteName, virtualPath, iisauthenticationMethods);
			if (flag)
			{
				this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedAuthenticationSettings, string.Empty, new object[]
				{
					text
				});
			}
			return flag;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000268C File Offset: 0x0000088C
		private bool UpdateConcurrentRequestLimit(ServerManager serverManager, string location)
		{
			bool flag = false;
			Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
			ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/serverRuntime", location);
			return flag | IISConfigurationUtilities.UpdateSectionAttribute(section, "appConcurrentRequestLimit", 120000);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026C8 File Offset: 0x000008C8
		private bool UpdateSslSettings(ServerManager serverManager, string location, ADRpcHttpVirtualDirectory rpcHttpVirtualDirectory)
		{
			bool flag = false;
			Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
			ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/security/access", location);
			string value = rpcHttpVirtualDirectory.SSLOffloading ? "None" : "Ssl,Ssl128";
			return flag | IISConfigurationUtilities.UpdateSectionAttribute(section, "sslFlags", value);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002710 File Offset: 0x00000910
		private bool UpdateCustomHttpErrors(ServerManager serverManager, string location)
		{
			bool flag = false;
			Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
			ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/httpErrors", location);
			flag |= IISConfigurationUtilities.UpdateSectionAttribute(section, "errorMode", "DetailedLocalOnly");
			ConfigurationElementCollection collection = section.GetCollection();
			if (collection.Count != 0)
			{
				collection.Clear();
				flag = true;
			}
			if (flag)
			{
				this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedHTTPErrors, string.Empty, null);
			}
			return flag;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002778 File Offset: 0x00000978
		private void CopyCustomRpcProxyFiles()
		{
			string[] array = new string[]
			{
				"web.config",
				"RpcProxyShim.dll"
			};
			foreach (string text in array)
			{
				string text2 = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\RpcProxy\\" + text);
				string text3 = Environment.GetEnvironmentVariable("windir") + "\\System32\\RpcProxy\\" + text;
				if (File.Exists(text2))
				{
					bool flag = true;
					if (File.Exists(text3))
					{
						DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(text2);
						DateTime lastWriteTimeUtc2 = File.GetLastWriteTimeUtc(text3);
						flag = (lastWriteTimeUtc > lastWriteTimeUtc2);
					}
					if (flag)
					{
						File.Copy(text2, text3, true);
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002824 File Offset: 0x00000A24
		private void UpdateIISSettings(ADRpcHttpVirtualDirectory rpcHttpVirtualDirectory, string siteName, string virtualPath)
		{
			bool flag = false;
			string location = siteName + virtualPath;
			using (ServerManager serverManager = new ServerManager())
			{
				flag |= this.UpdateAuthenticationSettings(serverManager, siteName, virtualPath, rpcHttpVirtualDirectory);
				flag |= this.UpdateConcurrentRequestLimit(serverManager, location);
				flag |= this.UpdateCustomHttpErrors(serverManager, location);
				flag |= this.UpdateSslSettings(serverManager, location, rpcHttpVirtualDirectory);
				if (flag)
				{
					serverManager.CommitChanges();
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002898 File Offset: 0x00000A98
		private void UpdateRpcProxyAllowAnonymousRegKey()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Rpc\\RpcProxy", true))
			{
				if (!Convert.ToBoolean(registryKey.GetValue("AllowAnonymous", 0)))
				{
					registryKey.SetValue("AllowAnonymous", 1);
					this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedAllowAnon, string.Empty, new object[]
					{
						true
					});
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002924 File Offset: 0x00000B24
		private void UpdateValidPortsRegistrySettings()
		{
			Server server = this.session.FindLocalServer();
			string text = RPCHTTPServicelet.ComputeValidPortsRegkey(new string[]
			{
				server.Fqdn
			}, new string[]
			{
				RPCHTTPServicelet.ConsolidatedRcaPortTemplate
			});
			object obj = null;
			RegistryUtilities.RegistryValueAction registryValueAction = RegistryUtilities.UpdateRegValue("Software\\Microsoft\\Rpc\\RpcProxy", "ValidPorts_AutoConfig_Exchange", text, out obj);
			if (registryValueAction != RegistryUtilities.RegistryValueAction.None)
			{
				ExEventLog.EventTuple tuple = MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_DisabledValidPorts;
				switch (registryValueAction)
				{
				case RegistryUtilities.RegistryValueAction.Enabled:
					tuple = MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_EnabledValidPorts;
					break;
				case RegistryUtilities.RegistryValueAction.Disabled:
					tuple = MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_DisabledValidPorts;
					break;
				case RegistryUtilities.RegistryValueAction.Updated:
					tuple = MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedValidPorts;
					break;
				}
				string text2 = (obj != null) ? obj.ToString() : string.Empty;
				string text3 = (text != null) ? text.ToString() : string.Empty;
				if (text2.Length > 30000)
				{
					text2 = text2.Substring(0, 30000);
					text3 = string.Empty;
				}
				else if (text2.Length + text3.Length > 30000)
				{
					text3 = text3.Substring(0, 30000 - text2.Length);
				}
				this.delayedLogger.AddEvent(tuple, this.iterationStartTime.Ticks.ToString(), new object[]
				{
					text2,
					text3,
					"ValidPorts_AutoConfig_Exchange"
				});
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A78 File Offset: 0x00000C78
		private bool ConfigureRpcProxyRegistrySettings()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Rpc", true))
			{
				if (registryKey != null)
				{
					int registryValueOrUseDefault = RegistryUtilities.GetRegistryValueOrUseDefault<int>(registryKey, "DisableTcpLoopbackToNpfsMapping", RegistryValueKind.DWord, 0);
					if (registryValueOrUseDefault != 1)
					{
						registryKey.SetValue("DisableTcpLoopbackToNpfsMapping", 1);
					}
				}
			}
			using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Rpc\\RpcProxy", true))
			{
				if (registryKey2 == null)
				{
					this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_ProxyNotInstalled, string.Empty, new object[0]);
					return false;
				}
				int registryValueOrUseDefault2 = RegistryUtilities.GetRegistryValueOrUseDefault<int>(registryKey2, "Enabled", RegistryValueKind.DWord, 0);
				if (registryValueOrUseDefault2 != 1)
				{
					this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_ProxyNotEnabled, string.Empty, new object[]
					{
						"Software\\Microsoft\\Rpc\\RpcProxy",
						"Enabled"
					});
					return false;
				}
				registryKey2.SetValue("WebSite", VirtualDirectoryConfiguration.BackEndWebSiteName, RegistryValueKind.String);
			}
			this.RemoveRpcProxyLbsSettings();
			this.UpdateValidPortsRegistrySettings();
			this.UpdateRpcProxyAllowAnonymousRegKey();
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B94 File Offset: 0x00000D94
		private void ConfigureRpcForMailboxServer(bool isAlsoFrontEnd)
		{
			this.CopyCustomRpcProxyFiles();
			this.EnsureRpcProxyConfigured();
			if (!this.ConfigureRpcProxyRegistrySettings())
			{
				return;
			}
			using (VirtualDirectoryConfiguration virtualDirectoryConfiguration = new VirtualDirectoryConfiguration(RpcHandlerMode.RpcProxy))
			{
				VirtualDirectorySecuritySettings securitySettings = new VirtualDirectorySecuritySettings
				{
					NegotiateProvider = true,
					NtlmProvider = true
				};
				virtualDirectoryConfiguration.ConfigureRpc(VirtualDirectoryConfiguration.BackEndWebSiteName, RpcVirtualDirectoryName.Rpc);
				virtualDirectoryConfiguration.ConfigureRpc(VirtualDirectoryConfiguration.BackEndWebSiteName, RpcVirtualDirectoryName.RpcWithCert);
				virtualDirectoryConfiguration.ConfigureRpcSecurity(VirtualDirectoryConfiguration.BackEndWebSiteName, RpcVirtualDirectoryName.Rpc, securitySettings);
				virtualDirectoryConfiguration.ConfigureRpcSecurity(VirtualDirectoryConfiguration.BackEndWebSiteName, RpcVirtualDirectoryName.RpcWithCert, securitySettings);
				if (!isAlsoFrontEnd)
				{
					virtualDirectoryConfiguration.RemoveRpc(virtualDirectoryConfiguration.DefaultWebSiteName, RpcVirtualDirectoryName.Rpc);
					virtualDirectoryConfiguration.RemoveRpc(virtualDirectoryConfiguration.DefaultWebSiteName, RpcVirtualDirectoryName.RpcWithCert);
				}
				if (virtualDirectoryConfiguration.Commit())
				{
					this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedRpcVirtualDirectory, string.Empty, new object[0]);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C68 File Offset: 0x00000E68
		private void ConfigureRpcVirtualDirectoryForCafeServer()
		{
			string defaultWebSiteName;
			using (VirtualDirectoryConfiguration virtualDirectoryConfiguration = new VirtualDirectoryConfiguration(RpcHandlerMode.HttpProxy))
			{
				defaultWebSiteName = virtualDirectoryConfiguration.DefaultWebSiteName;
				virtualDirectoryConfiguration.ConfigureRpc(virtualDirectoryConfiguration.DefaultWebSiteName, RpcVirtualDirectoryName.Rpc);
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Rpc\\RpcProxy", true))
				{
					bool flag = false;
					if (registryKey != null)
					{
						int registryValueOrUseDefault = RegistryUtilities.GetRegistryValueOrUseDefault<int>(registryKey, "EnableRpcWithCert", RegistryValueKind.DWord, 0);
						flag = (registryValueOrUseDefault == 1);
					}
					if (flag)
					{
						VirtualDirectorySecuritySettings securitySettings = new VirtualDirectorySecuritySettings
						{
							ClientCertificateMapping = true,
							IisClientCertificateMapping = true
						};
						virtualDirectoryConfiguration.ConfigureRpc(virtualDirectoryConfiguration.DefaultWebSiteName, RpcVirtualDirectoryName.RpcWithCert);
						virtualDirectoryConfiguration.ConfigureRpcSecurity(virtualDirectoryConfiguration.DefaultWebSiteName, RpcVirtualDirectoryName.RpcWithCert, securitySettings);
					}
					else
					{
						virtualDirectoryConfiguration.RemoveRpc(virtualDirectoryConfiguration.DefaultWebSiteName, RpcVirtualDirectoryName.RpcWithCert);
					}
				}
				if (virtualDirectoryConfiguration.Commit())
				{
					this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedRpcVirtualDirectory, string.Empty, new object[0]);
				}
			}
			ADRpcHttpVirtualDirectory adrpcHttpVirtualDirectory = this.FindADRpcHttpVirtualDirectory();
			if (adrpcHttpVirtualDirectory != null)
			{
				this.UpdateIISSettings(adrpcHttpVirtualDirectory, defaultWebSiteName, "/Rpc");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002D78 File Offset: 0x00000F78
		public override void Work()
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			this.session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 663, "Work", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\RPCOverHTTP\\Program\\RPCHTTPServicelet.cs");
			while (!base.StopEvent.WaitOne(timeSpan, false))
			{
				Exception ex = null;
				bool flag = false;
				this.iterationStartTime = DateTime.UtcNow;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchangeServiceHost\\RpcHttpConfigurator", true))
				{
					timeSpan = TimeSpan.FromMinutes((double)RegistryUtilities.GetRegistryValueOrUseDefault<int>(registryKey, "PeriodicPollingMinutes", RegistryValueKind.DWord, 15));
				}
				if (timeSpan == TimeSpan.Zero)
				{
					timeSpan = TimeSpan.FromMinutes(15.0);
				}
				else
				{
					try
					{
						bool flag2 = (base.InstalledServerRoles & ServerRole.Cafe) != ServerRole.None;
						bool flag3 = (base.InstalledServerRoles & ServerRole.ClientAccess) != ServerRole.None;
						if (flag3)
						{
							this.ConfigureRpcForMailboxServer(flag2);
						}
						if (flag2)
						{
							this.ConfigureRpcVirtualDirectoryForCafeServer();
						}
						if (!RPCHTTPServicelet.haveLoggedStartUpEvent)
						{
							this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_RpcHttpServiceletSuccessfullyCheckedForUpdatedSettings, string.Empty, new object[0]);
							RPCHTTPServicelet.haveLoggedStartUpEvent = true;
						}
					}
					catch (FileLoadException ex2)
					{
						ex = ex2;
						if (Marshal.GetHRForException(ex2) != -2147024864)
						{
							throw;
						}
						this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_FileNotAccessible, string.Empty, new object[]
						{
							ex2.FileName,
							ex2.Message
						});
					}
					catch (WebSiteNotFoundException ex3)
					{
						this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_WebSiteNotFound, string.Empty, new object[]
						{
							ex3.WebSiteName
						});
						flag = true;
						ex = ex3;
					}
					catch (WebSitesNotConfiguredException ex4)
					{
						this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_WebSitesNotConfigured, string.Empty, new object[0]);
						flag = true;
						ex = ex4;
					}
					catch (ADTransientException ex5)
					{
						ex = ex5;
					}
					catch (DataValidationException ex6)
					{
						flag = true;
						ex = ex6;
					}
					catch (LocalServerNotFoundException ex7)
					{
						flag = true;
						ex = ex7;
					}
					catch (COMException ex8)
					{
						ex = ex8;
					}
					catch (IISGeneralCOMException ex9)
					{
						ex = ex9;
					}
					catch (ExClusTransientException ex10)
					{
						ex = ex10;
					}
					catch (DataSourceTransientException ex11)
					{
						ex = ex11;
					}
					catch (DataSourceOperationException ex12)
					{
						flag = true;
						ex = ex12;
					}
					catch (IOException ex13)
					{
						flag = true;
						ex = ex13;
					}
					finally
					{
						if (ex != null)
						{
							if (flag)
							{
								this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_PermanentException, string.Empty, new object[]
								{
									timeSpan.ToString(),
									ex.StackTrace,
									ex.Message
								});
							}
							else
							{
								timeSpan = RPCHTTPServicelet.transientErrorBackoff;
								this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_TransientException, string.Empty, new object[]
								{
									timeSpan.ToString(),
									ex.Message
								});
							}
						}
						this.delayedLogger.Flush();
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003170 File Offset: 0x00001370
		private void EnsureRpcProxyConfigured()
		{
			bool flag = false;
			using (ServerManager serverManager = new ServerManager())
			{
				string environmentVariable = Environment.GetEnvironmentVariable("SystemRoot");
				string text = environmentVariable + "\\System32\\RpcProxy\\RpcProxyShim.dll";
				Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
				ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/globalModules");
				ConfigurationElementCollection collection = section.GetCollection();
				if (IISConfigurationUtilities.FindElement(collection, "add", "name", "PasswordExpiryModule") == null)
				{
					ConfigurationElement configurationElement = collection.CreateElement("add");
					configurationElement["name"] = "PasswordExpiryModule";
					configurationElement["image"] = "%SystemRoot%\\System32\\RpcProxy\\RpcProxy.dll";
					configurationElement["precondition"] = "bitness64";
					collection.Add(configurationElement);
					flag = true;
				}
				ConfigurationSection section2 = applicationHostConfiguration.GetSection("system.webServer/security/applicationDependencies");
				ConfigurationElementCollection collection2 = section2.GetCollection();
				if (IISConfigurationUtilities.FindElement(collection2, "application", "name", text) == null)
				{
					ConfigurationElement configurationElement2 = collection2.CreateElement("application");
					configurationElement2["name"] = text;
					configurationElement2["groupId"] = "RPCProxy";
					collection2.Add(configurationElement2);
					flag = true;
				}
				ConfigurationSection section3 = applicationHostConfiguration.GetSection("system.webServer/security/isapiCgiRestriction");
				ConfigurationElementCollection collection3 = section3.GetCollection();
				if (IISConfigurationUtilities.FindElement(collection3, "add", "path", text) == null)
				{
					ConfigurationElement configurationElement3 = collection3.CreateElement("add");
					configurationElement3["path"] = text;
					configurationElement3["allowed"] = true;
					configurationElement3["groupId"] = "RPCProxy";
					configurationElement3["description"] = "RPC Proxy Server Extension";
					collection3.Add(configurationElement3);
					flag = true;
				}
				if (flag)
				{
					serverManager.CommitChanges();
					this.delayedLogger.AddEvent(MSExchangeRPCHTTPAutoconfigEventLogConstants.Tuple_UpdatedRpcHttpGeneralSettings, string.Empty, new object[0]);
				}
			}
		}

		// Token: 0x0400000C RID: 12
		private const int EventLogTextLimit = 30000;

		// Token: 0x0400000D RID: 13
		private const string RegkeyRpcHttpConfig = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeServiceHost\\RpcHttpConfigurator";

		// Token: 0x0400000E RID: 14
		private const string RegvalPeriodicPollingMinutes = "PeriodicPollingMinutes";

		// Token: 0x0400000F RID: 15
		private const string RegkeyRpc = "Software\\Microsoft\\Rpc";

		// Token: 0x04000010 RID: 16
		private const string RegvalDisableLoopbackOptimization = "DisableTcpLoopbackToNpfsMapping";

		// Token: 0x04000011 RID: 17
		private const string RegkeyRpcProxy = "Software\\Microsoft\\Rpc\\RpcProxy";

		// Token: 0x04000012 RID: 18
		private const string RegvalRpcProxyEnabled = "Enabled";

		// Token: 0x04000013 RID: 19
		private const string RegvalRpcWithCertEnabled = "EnableRpcWithCert";

		// Token: 0x04000014 RID: 20
		private const string RegvalValidPortsExchange = "ValidPorts_AutoConfig_Exchange";

		// Token: 0x04000015 RID: 21
		private const string RegkeyResourcePools = "Software\\Microsoft\\Rpc\\RpcProxy\\LBSConfiguration";

		// Token: 0x04000016 RID: 22
		private const string RegvalWebSite = "WebSite";

		// Token: 0x04000017 RID: 23
		private const string AllowAnonymousRpcProxyRegKey = "AllowAnonymous";

		// Token: 0x04000018 RID: 24
		public const string EventLogProviderName = "MSExchange RPC Over HTTP Autoconfig";

		// Token: 0x04000019 RID: 25
		private const int PeriodicPollingDefault = 15;

		// Token: 0x0400001A RID: 26
		private const string EmsmdbPort = "6001";

		// Token: 0x0400001B RID: 27
		private const string RfriPort = "6002";

		// Token: 0x0400001C RID: 28
		private const string NspiPort = "6004";

		// Token: 0x0400001D RID: 29
		private static readonly Guid CasResourcePool = new Guid("CA5B08E5-4A52-5701-0000-000000000000");

		// Token: 0x0400001E RID: 30
		private static readonly Guid ComponentGuid = new Guid("fa3cc2e7-342c-471d-a788-81d7e8404d52");

		// Token: 0x0400001F RID: 31
		private static bool haveLoggedStartUpEvent = false;

		// Token: 0x04000020 RID: 32
		private static readonly string ConsolidatedRcaPortTemplate = string.Format("{{0}}:{0};", "6001");

		// Token: 0x04000021 RID: 33
		private readonly FlushableLogger delayedLogger = new FlushableLogger(new ExEventLog(RPCHTTPServicelet.ComponentGuid, "MSExchange RPC Over HTTP Autoconfig"));

		// Token: 0x04000022 RID: 34
		private ITopologyConfigurationSession session;

		// Token: 0x04000023 RID: 35
		private DateTime iterationStartTime;

		// Token: 0x04000024 RID: 36
		private static readonly TimeSpan transientErrorBackoff = TimeSpan.FromSeconds(15.0);
	}
}
