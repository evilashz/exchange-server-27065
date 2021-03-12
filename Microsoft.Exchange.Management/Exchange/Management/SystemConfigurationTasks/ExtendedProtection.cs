using System;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.ServiceModel.Configuration;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020004A8 RID: 1192
	internal class ExtendedProtection
	{
		// Token: 0x06002A10 RID: 10768 RVA: 0x000A6FB0 File Offset: 0x000A51B0
		public static void Validate(Task task, ExchangeVirtualDirectory exchangeVirtualDirectory)
		{
			if (exchangeVirtualDirectory.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				return;
			}
			if (task.Fields.Contains("ExtendedProtectionSPNList"))
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)task.Fields["ExtendedProtectionSPNList"];
				if (multiValuedProperty != null)
				{
					foreach (string text in multiValuedProperty)
					{
						if (!text.StartsWith("HTTP/", StringComparison.InvariantCultureIgnoreCase))
						{
							task.WriteError(new ArgumentException(Strings.ErrorExtendedProtectionSPNHasToStartWithHTTP(text), "ExtendedProtectionSPNList"), ErrorCategory.InvalidArgument, exchangeVirtualDirectory.Identity);
						}
					}
				}
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000A7068 File Offset: 0x000A5268
		public static void LoadFromMetabase(ExchangeVirtualDirectory exchangeVirtualDirectory, Task task)
		{
			if (exchangeVirtualDirectory.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				return;
			}
			string metabasePath = exchangeVirtualDirectory.MetabasePath;
			ExtendedProtectionTokenCheckingMode extendedProtectionTokenCheckingMode;
			MultiValuedProperty<ExtendedProtectionFlag> extendedProtectionFlags;
			MultiValuedProperty<string> extendedProtectionSPNList;
			ExtendedProtection.LoadFromMetabase(metabasePath, exchangeVirtualDirectory.Identity, task, out extendedProtectionTokenCheckingMode, out extendedProtectionFlags, out extendedProtectionSPNList);
			exchangeVirtualDirectory[ExchangeVirtualDirectorySchema.ExtendedProtectionTokenChecking] = extendedProtectionTokenCheckingMode;
			exchangeVirtualDirectory.ExtendedProtectionFlags = extendedProtectionFlags;
			exchangeVirtualDirectory.ExtendedProtectionSPNList = extendedProtectionSPNList;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000A70C4 File Offset: 0x000A52C4
		public static void LoadFromMetabase(string metabasePath, ObjectId identity, Task task, out ExtendedProtectionTokenCheckingMode extendedProtectionTokenChecking, out MultiValuedProperty<ExtendedProtectionFlag> extendedProtectionFlags, out MultiValuedProperty<string> extendedProtectionSPNList)
		{
			extendedProtectionTokenChecking = ExtendedProtectionTokenCheckingMode.None;
			extendedProtectionFlags = new MultiValuedProperty<ExtendedProtectionFlag>();
			extendedProtectionSPNList = new MultiValuedProperty<string>();
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(metabasePath, (task != null) ? new Task.TaskErrorLoggingReThrowDelegate(task.WriteError) : null, identity, false))
			{
				if (directoryEntry != null)
				{
					string text;
					string str;
					string str2;
					if (ExtendedProtection.GetServerWebSiteAndPath(metabasePath, out text, out str, out str2))
					{
						using (ServerManager serverManager = ServerManager.OpenRemote(text))
						{
							Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
							if (applicationHostConfiguration != null)
							{
								ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/security/authentication/windowsAuthentication", "/" + str + str2);
								if (section != null)
								{
									ConfigurationElement configurationElement = section.ChildElements["extendedProtection"];
									if (configurationElement != null)
									{
										object attributeValue = configurationElement.GetAttributeValue("tokenChecking");
										if (attributeValue != null && attributeValue is int)
										{
											extendedProtectionTokenChecking = (ExtendedProtectionTokenCheckingMode)attributeValue;
										}
										object attributeValue2 = configurationElement.GetAttributeValue("flags");
										if (attributeValue2 != null && attributeValue2 is int)
										{
											extendedProtectionFlags.Add((ExtendedProtectionFlag)attributeValue2);
										}
										ConfigurationElementCollection collection = configurationElement.GetCollection();
										if (collection != null)
										{
											foreach (ConfigurationElement configurationElement2 in collection)
											{
												if (configurationElement2.Schema.Name == "spn")
												{
													string item = configurationElement2.GetAttributeValue("name").ToString();
													extendedProtectionSPNList.Add(item);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000A7298 File Offset: 0x000A5498
		public static void CommitToMetabase(ExchangeVirtualDirectory exchangeVirtualDirectory, Task task)
		{
			if (exchangeVirtualDirectory.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				return;
			}
			bool flag = task.Fields.IsModified("ExtendedProtectionTokenChecking");
			bool flag2 = task.Fields.IsModified("ExtendedProtectionFlags");
			bool flag3 = task.Fields.IsModified("ExtendedProtectionSPNList");
			if (flag || flag2 || flag3)
			{
				string metabasePath = exchangeVirtualDirectory.MetabasePath;
				using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(metabasePath, new Task.TaskErrorLoggingReThrowDelegate(task.WriteError), exchangeVirtualDirectory.Identity))
				{
					if (directoryEntry != null)
					{
						string text;
						string text2;
						string text3;
						if (ExtendedProtection.GetServerWebSiteAndPath(metabasePath, out text, out text2, out text3))
						{
							if (!ExtendedProtection.WebConfigReflectionHelper.IsExtendedProtectionSupported(task))
							{
								TaskLogger.Trace("Warning: ExtendedProtectionPolicy has not been added to HttpTransportElement of web.config.  Install the operating system update(s) specified in KB {0} onto server {1} and try again.", new object[]
								{
									"981205",
									text
								});
								task.WriteWarning(Strings.WarnExtendedProtectionIsNotEnabled(text, "981205"));
							}
							else
							{
								string text4 = "/" + text2 + text3;
								using (ServerManager serverManager = ServerManager.OpenRemote(text))
								{
									Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
									if (applicationHostConfiguration != null)
									{
										ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/security/authentication/windowsAuthentication", text4);
										if (section != null)
										{
											ConfigurationElement configurationElement = section.ChildElements["extendedProtection"];
											if (configurationElement != null)
											{
												if (flag)
												{
													int num = (int)exchangeVirtualDirectory[ExchangeVirtualDirectorySchema.ExtendedProtectionTokenChecking];
													configurationElement.SetAttributeValue("tokenChecking", num);
												}
												if (flag2)
												{
													int num2 = (int)exchangeVirtualDirectory[ExchangeVirtualDirectorySchema.ExtendedProtectionFlags];
													configurationElement.SetAttributeValue("flags", num2);
												}
												if (flag3)
												{
													ConfigurationElementCollection collection = configurationElement.GetCollection();
													collection.Clear();
													foreach (string text5 in exchangeVirtualDirectory.ExtendedProtectionSPNList)
													{
														ConfigurationElement configurationElement2 = collection.CreateElement("spn");
														configurationElement2.SetAttributeValue("name", text5);
														collection.Add(configurationElement2);
													}
												}
												ExtendedProtection.WebConfigReflectionHelper.CommitToWebConfigMWA(exchangeVirtualDirectory, task, text3, text2, text, flag, flag3);
												serverManager.CommitChanges();
												return;
											}
											TaskLogger.Trace("Warning: Extended protection has not been enabled.  Install the operating system update specified in KB {0} onto server {1} and try again.", new object[]
											{
												"973917",
												text
											});
											task.WriteWarning(Strings.WarnExtendedProtectionIsNotEnabled(text, "973917"));
											return;
										}
									}
									TaskLogger.Trace("Error:ApplicationHost.config or {0} is not found for virtual directory with metabase path '{1}' and local path '{2}'.", new object[]
									{
										"system.webServer/security/authentication/windowsAuthentication",
										metabasePath,
										text4
									});
									task.WriteError(new ArgumentException(Strings.ErrorAppHostOrWindowsAuthenticationNotFound("system.webServer/security/authentication/windowsAuthentication", metabasePath, text4)), ErrorCategory.ObjectNotFound, exchangeVirtualDirectory.Identity);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000A7594 File Offset: 0x000A5794
		private static bool GetServerWebSiteAndPath(string metabasePath, out string serverName, out string webSiteName, out string appPath)
		{
			serverName = IisUtility.GetHostName(metabasePath);
			int num = metabasePath.IndexOf("/ROOT/");
			if (num > 0)
			{
				num = metabasePath.IndexOf("/", num + 1);
			}
			if (num > 0)
			{
				string text = metabasePath.Substring(0, num);
				if (!string.IsNullOrEmpty(text))
				{
					webSiteName = IisUtility.GetWebSiteName(text);
					appPath = metabasePath.Substring(num);
					return true;
				}
			}
			serverName = string.Empty;
			webSiteName = string.Empty;
			appPath = string.Empty;
			return false;
		}

		// Token: 0x04001EDC RID: 7900
		private const string WebConfigFileName = "web.config";

		// Token: 0x04001EDD RID: 7901
		private const string AddServiceNameElement = "add";

		// Token: 0x04001EDE RID: 7902
		private const string CustomServiceNamesElement = "customServiceNames";

		// Token: 0x04001EDF RID: 7903
		private const string PolicyEnforcementAttribute = "policyEnforcement";

		// Token: 0x04001EE0 RID: 7904
		private const string ExtendedProtectionPolicyElement = "extendedProtectionPolicy";

		// Token: 0x04001EE1 RID: 7905
		private const string AuthenticationSchemeAttribute = "authenticationScheme";

		// Token: 0x04001EE2 RID: 7906
		private const string HttpsTransportElement = "httpsTransport";

		// Token: 0x04001EE3 RID: 7907
		private const string HttpTransportElement = "httpTransport";

		// Token: 0x04001EE4 RID: 7908
		private const string CustomBindingElement = "customBinding";

		// Token: 0x04001EE5 RID: 7909
		private const string ExtendedProtectionElement = "extendedProtection";

		// Token: 0x04001EE6 RID: 7910
		private const string TokenCheckingAttribute = "tokenChecking";

		// Token: 0x04001EE7 RID: 7911
		private const string FlagsAttribute = "flags";

		// Token: 0x04001EE8 RID: 7912
		private const string SpnNameElement = "spn";

		// Token: 0x04001EE9 RID: 7913
		private const string SpnNameAttribute = "name";

		// Token: 0x04001EEA RID: 7914
		private const string SpnNamePrefix = "HTTP/";

		// Token: 0x04001EEB RID: 7915
		private const string KbForMetabase = "973917";

		// Token: 0x04001EEC RID: 7916
		private const string KbForWebConfig = "981205";

		// Token: 0x04001EED RID: 7917
		private static readonly string BindingsSectionName = "system.serviceModel/bindings";

		// Token: 0x020004A9 RID: 1193
		private class WebConfigReflectionHelper
		{
			// Token: 0x06002A17 RID: 10775 RVA: 0x000A761C File Offset: 0x000A581C
			internal static bool IsExtendedProtectionSupported(Task task)
			{
				if (!ExtendedProtection.WebConfigReflectionHelper.initialized)
				{
					try
					{
						ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyPropertyInfo = typeof(HttpTransportElement).GetProperty("ExtendedProtectionPolicy");
						if (ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyPropertyInfo == null)
						{
							return false;
						}
						ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyGetMethodInfo = ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyPropertyInfo.GetGetMethod();
						if (ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyGetMethodInfo == null)
						{
							return false;
						}
						ExtendedProtection.WebConfigReflectionHelper.policyEnforcementPropertyInfo = ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyPropertyInfo.PropertyType.GetProperty("PolicyEnforcement");
						if (ExtendedProtection.WebConfigReflectionHelper.policyEnforcementPropertyInfo == null)
						{
							return false;
						}
						ExtendedProtection.WebConfigReflectionHelper.never = new object[]
						{
							Enum.Parse(ExtendedProtection.WebConfigReflectionHelper.policyEnforcementPropertyInfo.PropertyType, "Never")
						};
						ExtendedProtection.WebConfigReflectionHelper.whenSupported = new object[]
						{
							Enum.Parse(ExtendedProtection.WebConfigReflectionHelper.policyEnforcementPropertyInfo.PropertyType, "WhenSupported")
						};
						ExtendedProtection.WebConfigReflectionHelper.always = new object[]
						{
							Enum.Parse(ExtendedProtection.WebConfigReflectionHelper.policyEnforcementPropertyInfo.PropertyType, "Always")
						};
						ExtendedProtection.WebConfigReflectionHelper.policyEnforcementSetMethodInfo = ExtendedProtection.WebConfigReflectionHelper.policyEnforcementPropertyInfo.GetSetMethod();
						if (ExtendedProtection.WebConfigReflectionHelper.policyEnforcementSetMethodInfo == null)
						{
							return false;
						}
						ExtendedProtection.WebConfigReflectionHelper.customServiceNamesPropertyInfo = ExtendedProtection.WebConfigReflectionHelper.extendedProtectionPolicyPropertyInfo.PropertyType.GetProperty("CustomServiceNames");
						if (ExtendedProtection.WebConfigReflectionHelper.customServiceNamesPropertyInfo == null)
						{
							return false;
						}
						ExtendedProtection.WebConfigReflectionHelper.customServiceNamesGetMethodInfo = ExtendedProtection.WebConfigReflectionHelper.customServiceNamesPropertyInfo.GetGetMethod();
						bool flag = false;
						bool flag2 = false;
						foreach (MethodInfo methodInfo in ExtendedProtection.WebConfigReflectionHelper.customServiceNamesPropertyInfo.PropertyType.GetMethods())
						{
							ParameterInfo[] parameters = methodInfo.GetParameters();
							if (methodInfo.Name == "Clear" && parameters.Length == 0)
							{
								ExtendedProtection.WebConfigReflectionHelper.customServiceNamesClearMethodInfo = methodInfo;
								flag2 = true;
							}
							else if (methodInfo.Name == "Add" && parameters.Length == 1 && parameters[0].ParameterType.Name == "ServiceNameElement")
							{
								ExtendedProtection.WebConfigReflectionHelper.customServiceNamesAddMethodInfo = methodInfo;
								ExtendedProtection.WebConfigReflectionHelper.serviceNameElementNamePropertyInfo = parameters[0].ParameterType.GetProperty("Name");
								if (ExtendedProtection.WebConfigReflectionHelper.serviceNameElementNamePropertyInfo == null)
								{
									return false;
								}
								ExtendedProtection.WebConfigReflectionHelper.serviceNameElementNameSetMethodInfo = ExtendedProtection.WebConfigReflectionHelper.serviceNameElementNamePropertyInfo.GetSetMethod();
								if (ExtendedProtection.WebConfigReflectionHelper.serviceNameElementNameSetMethodInfo == null)
								{
									return false;
								}
								ExtendedProtection.WebConfigReflectionHelper.serviceNameElementConstructorInfo = parameters[0].ParameterType.GetConstructor(new Type[0]);
								if (ExtendedProtection.WebConfigReflectionHelper.serviceNameElementConstructorInfo == null)
								{
									return false;
								}
								flag = true;
							}
						}
						ExtendedProtection.WebConfigReflectionHelper.isExtendedProtectionSupported = (flag2 && flag);
						ExtendedProtection.WebConfigReflectionHelper.initialized = true;
					}
					catch (Exception ex)
					{
						TaskLogger.Trace("Error: Unexpected exception on accessing ExtendedProtection properties: {0}, ExtendedProtection would not be handled", new object[]
						{
							ex.ToString()
						});
						task.WriteWarning(Strings.ExceptionOccured(ex.ToString()));
					}
				}
				return ExtendedProtection.WebConfigReflectionHelper.isExtendedProtectionSupported;
			}

			// Token: 0x06002A18 RID: 10776 RVA: 0x000A7914 File Offset: 0x000A5B14
			private static void SetPolicyEnforcement(object extendedProtectionPolicyProperty, ExtendedProtectionTokenCheckingMode tokenCheckingMode)
			{
				object[] parameters = (tokenCheckingMode == ExtendedProtectionTokenCheckingMode.Allow) ? ExtendedProtection.WebConfigReflectionHelper.whenSupported : ((tokenCheckingMode == ExtendedProtectionTokenCheckingMode.Require) ? ExtendedProtection.WebConfigReflectionHelper.always : ExtendedProtection.WebConfigReflectionHelper.never);
				ExtendedProtection.WebConfigReflectionHelper.policyEnforcementSetMethodInfo.Invoke(extendedProtectionPolicyProperty, parameters);
			}

			// Token: 0x06002A19 RID: 10777 RVA: 0x000A794C File Offset: 0x000A5B4C
			private static void SetServiceNames(object extendedProtectionPolicyProperty, MultiValuedProperty<string> spnList)
			{
				object obj = ExtendedProtection.WebConfigReflectionHelper.customServiceNamesGetMethodInfo.Invoke(extendedProtectionPolicyProperty, null);
				ExtendedProtection.WebConfigReflectionHelper.customServiceNamesClearMethodInfo.Invoke(obj, null);
				foreach (string text in spnList)
				{
					object obj2 = ExtendedProtection.WebConfigReflectionHelper.serviceNameElementConstructorInfo.Invoke(null);
					ExtendedProtection.WebConfigReflectionHelper.serviceNameElementNameSetMethodInfo.Invoke(obj2, new object[]
					{
						text
					});
					ExtendedProtection.WebConfigReflectionHelper.customServiceNamesAddMethodInfo.Invoke(obj, new object[]
					{
						obj2
					});
				}
			}

			// Token: 0x06002A1A RID: 10778 RVA: 0x000A79F0 File Offset: 0x000A5BF0
			internal static void CommitToWebConfigMWA(ExchangeVirtualDirectory exchangeVirtualDirectory, Task task, string path, string site, string server, bool isTokenCheckingSpecified, bool isSpnListSpecified)
			{
				if (ExtendedProtection.WebConfigReflectionHelper.isExtendedProtectionSupported && (isTokenCheckingSpecified || isSpnListSpecified))
				{
					using (ServerManager serverManager = ServerManager.OpenRemote(server))
					{
						Configuration webConfiguration = serverManager.GetWebConfiguration(site, path);
						string configurationFilePath = ExtendedProtection.WebConfigReflectionHelper.GetConfigurationFilePath(serverManager, site, path);
						if (!string.IsNullOrEmpty(configurationFilePath) && File.Exists(configurationFilePath))
						{
							ConfigurationSection section = webConfiguration.GetSection(ExtendedProtection.BindingsSectionName);
							if (section != null)
							{
								ConfigurationElement configurationElement = section.ChildElements["customBinding"];
								if (configurationElement != null)
								{
									ConfigurationElementCollection collection = configurationElement.GetCollection();
									if (collection != null)
									{
										bool flag = false;
										foreach (ConfigurationElement configurationElement2 in collection)
										{
											ConfigurationElement configurationElement3 = configurationElement2.ChildElements["httpsTransport"];
											ConfigurationAttribute attribute = configurationElement3.GetAttribute("authenticationScheme");
											if (attribute.IsInheritedFromDefaultValue)
											{
												configurationElement3 = configurationElement2.ChildElements["httpTransport"];
												attribute = configurationElement3.GetAttribute("authenticationScheme");
											}
											if (!attribute.IsInheritedFromDefaultValue && (int)attribute.Value == 2)
											{
												ConfigurationElement configurationElement4 = configurationElement3.ChildElements["extendedProtectionPolicy"];
												if (configurationElement4 != null)
												{
													if (isTokenCheckingSpecified)
													{
														ExtendedProtection.WebConfigReflectionHelper.SetPolicyEnforcementMWA(configurationElement4, exchangeVirtualDirectory.ExtendedProtectionTokenChecking);
													}
													if (isSpnListSpecified)
													{
														ExtendedProtection.WebConfigReflectionHelper.SetServiceNamesMWA(configurationElement4, exchangeVirtualDirectory.ExtendedProtectionSPNList);
													}
													flag = true;
												}
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
			}

			// Token: 0x06002A1B RID: 10779 RVA: 0x000A7BA0 File Offset: 0x000A5DA0
			private static void SetPolicyEnforcementMWA(ConfigurationElement extendedProtectionPolicyElement, ExtendedProtectionTokenCheckingMode tokenCheckingMode)
			{
				object[] array = (tokenCheckingMode == ExtendedProtectionTokenCheckingMode.Allow) ? ExtendedProtection.WebConfigReflectionHelper.whenSupported : ((tokenCheckingMode == ExtendedProtectionTokenCheckingMode.Require) ? ExtendedProtection.WebConfigReflectionHelper.always : ExtendedProtection.WebConfigReflectionHelper.never);
				extendedProtectionPolicyElement["policyEnforcement"] = array[0];
			}

			// Token: 0x06002A1C RID: 10780 RVA: 0x000A7BD8 File Offset: 0x000A5DD8
			private static void SetServiceNamesMWA(ConfigurationElement extendedProtectionPolicyElement, MultiValuedProperty<string> spnList)
			{
				ConfigurationElement configurationElement = extendedProtectionPolicyElement.ChildElements["customServiceNames"];
				if (configurationElement == null)
				{
					return;
				}
				ConfigurationElementCollection collection = configurationElement.GetCollection();
				if (collection == null)
				{
					return;
				}
				collection.Clear();
				foreach (string text in spnList)
				{
					ConfigurationElement configurationElement2 = collection.CreateElement("add");
					configurationElement2["name"] = text;
					collection.Add(configurationElement2);
				}
			}

			// Token: 0x06002A1D RID: 10781 RVA: 0x000A7C68 File Offset: 0x000A5E68
			private static string GetConfigurationFilePath(ServerManager serverManager, string site, string path)
			{
				if (serverManager.Sites == null || serverManager.Sites.Count == 0)
				{
					return null;
				}
				Site site2 = serverManager.Sites[site];
				if (site2 == null || site2.Applications == null || site2.Applications.Count == 0)
				{
					return null;
				}
				Application application = site2.Applications[path];
				if (application == null || application.VirtualDirectories == null || application.VirtualDirectories.Count == 0)
				{
					return null;
				}
				return Path.Combine(Environment.ExpandEnvironmentVariables(application.VirtualDirectories[0].PhysicalPath), "web.config");
			}

			// Token: 0x04001EEE RID: 7918
			private static bool isExtendedProtectionSupported = false;

			// Token: 0x04001EEF RID: 7919
			private static PropertyInfo extendedProtectionPolicyPropertyInfo;

			// Token: 0x04001EF0 RID: 7920
			private static MethodInfo extendedProtectionPolicyGetMethodInfo;

			// Token: 0x04001EF1 RID: 7921
			private static PropertyInfo policyEnforcementPropertyInfo;

			// Token: 0x04001EF2 RID: 7922
			private static MethodInfo policyEnforcementSetMethodInfo;

			// Token: 0x04001EF3 RID: 7923
			private static PropertyInfo customServiceNamesPropertyInfo;

			// Token: 0x04001EF4 RID: 7924
			private static MethodInfo customServiceNamesGetMethodInfo;

			// Token: 0x04001EF5 RID: 7925
			private static MethodInfo customServiceNamesClearMethodInfo;

			// Token: 0x04001EF6 RID: 7926
			private static MethodInfo customServiceNamesAddMethodInfo;

			// Token: 0x04001EF7 RID: 7927
			private static ConstructorInfo serviceNameElementConstructorInfo;

			// Token: 0x04001EF8 RID: 7928
			private static PropertyInfo serviceNameElementNamePropertyInfo;

			// Token: 0x04001EF9 RID: 7929
			private static MethodInfo serviceNameElementNameSetMethodInfo;

			// Token: 0x04001EFA RID: 7930
			private static object[] whenSupported;

			// Token: 0x04001EFB RID: 7931
			private static object[] always;

			// Token: 0x04001EFC RID: 7932
			private static object[] never;

			// Token: 0x04001EFD RID: 7933
			private static bool initialized = false;
		}
	}
}
