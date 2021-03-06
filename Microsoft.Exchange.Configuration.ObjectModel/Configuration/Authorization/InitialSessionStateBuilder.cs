using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.PowerShell.Commands;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000234 RID: 564
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class InitialSessionStateBuilder
	{
		// Token: 0x060013F6 RID: 5110 RVA: 0x00046540 File Offset: 0x00044740
		static InitialSessionStateBuilder()
		{
			InitialSessionStateBuilder.isCentralAdminCmdletInstalled = InitialSessionStateBuilder.CheckCentralAdminCmdletInstalled();
			InitialSessionStateBuilder.isFfoCentralAdminCmdletInstalled = InitialSessionStateBuilder.CheckFfoCentralAdminCmdletInstalled();
			string text = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.PowerShell.Configuration.dll");
			try
			{
				Assembly assembly = Assembly.LoadFrom(text);
				Type type = assembly.GetType("Microsoft.Exchange.Management.PowerShell.CmdletConfigurationEntries", true, true);
				MethodInfo method = type.GetMethod("PopulateISSCmdletConfigurationEntries", BindingFlags.Static | BindingFlags.Public);
				using (new MonitoredScope("InitialSessionStateBuilder", "populateMethod.Invoke(exchangeCmdletConfigurationEntriesType)", AuthZLogHelper.AuthZPerfMonitors))
				{
					method.Invoke(type, new object[0]);
					InitialSessionStateBuilder.PopulateTorusCmdletConfigurationEntries();
				}
				if (InitialSessionStateBuilder.isCentralAdminCmdletInstalled)
				{
					string assemblyFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.Powershell.CentralAdmin.dll");
					assembly = Assembly.LoadFrom(assemblyFile);
					Type type2 = assembly.GetType("Microsoft.Exchange.Management.Powershell.CentralAdmin.CmdletConfiguration", true, true);
					method = type2.GetMethod("PopulateISSCmdletConfigurationEntries", BindingFlags.Static | BindingFlags.Public);
					using (new MonitoredScope("InitialSessionStateBuilder", "populateMethod.Invoke(centralAdminCmdletConfigurationEntriesType)", AuthZLogHelper.AuthZPerfMonitors))
					{
						method.Invoke(type2, new object[0]);
					}
					if (InitialSessionStateBuilder.isFfoCentralAdminCmdletInstalled)
					{
						string assemblyFile2 = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.Powershell.FfoCentralAdmin.dll");
						assembly = Assembly.LoadFrom(assemblyFile2);
						Type type3 = assembly.GetType("Microsoft.Exchange.Management.Powershell.CentralAdmin.FfoCmdletConfiguration", true, true);
						method = type3.GetMethod("PopulateISSCmdletConfigurationEntries", BindingFlags.Static | BindingFlags.Public);
						using (new MonitoredScope("InitialSessionStateBuilder", "populateMethod.Invoke(ffoCentralAdminCmdletConfigurationEntriesType)", AuthZLogHelper.AuthZPerfMonitors))
						{
							method.Invoke(type3, new object[0]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.IssBuilderTracer.TraceError<string, string>(0L, "ISSBuilder.InitialSessionStateBuilder: Exception in Populating ISS cmdlet information {0}: {1}", text, ex.Message);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_RBACUnavailable_UnknownError, null, new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000468F0 File Offset: 0x00044AF0
		internal static void AddCmdletConfigurationEntries(CmdletConfigurationEntry[] cmdletEntries)
		{
			InitialSessionStateBuilder.exchangeCmdletConfigurationEntries.AddRange(cmdletEntries);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x000468FD File Offset: 0x00044AFD
		internal static void AddDynamicParameter(string cmdletType, Type dynamicParameterType)
		{
			InitialSessionStateBuilder.dynamicParameterTypes.Add(cmdletType, dynamicParameterType);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0004690C File Offset: 0x00044B0C
		private static Assembly FindAssembly(object sender, ResolveEventArgs args)
		{
			FileSearchUtil fileSearchUtil = new FileSearchUtil(ConfigurationContext.Setup.TorusPath, "*.dll");
			List<string> filesRecurse = fileSearchUtil.GetFilesRecurse();
			fileSearchUtil = new FileSearchUtil(ConfigurationContext.Setup.TorusPath, "*.exe");
			filesRecurse.AddRange(fileSearchUtil.GetFilesRecurse());
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in filesRecurse)
			{
				dictionary.Add(Path.GetFileNameWithoutExtension(text), text);
			}
			string name = args.Name;
			string name2 = new AssemblyName(name).Name;
			if (dictionary.Keys.Contains(name2))
			{
				return Assembly.LoadFrom(dictionary[name2]);
			}
			return null;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000469D4 File Offset: 0x00044BD4
		private static void PopulateTorusCmdletConfigurationEntries()
		{
			if (!File.Exists(Path.Combine(ConfigurationContext.Setup.TorusPath, ConfigurationContext.Setup.TorusCmdletAssembly)))
			{
				ExTraceGlobals.IssBuilderTracer.TraceError(0L, "Can't find Torus Cmdlet assemblies");
				return;
			}
			try
			{
				AppDomain.CurrentDomain.AssemblyResolve += InitialSessionStateBuilder.FindAssembly;
				Assembly assembly = Assembly.LoadFile(Path.Combine(ConfigurationContext.Setup.TorusPath, ConfigurationContext.Setup.TorusCmdletAssembly));
				foreach (Type type in assembly.GetExportedTypes())
				{
					if (InitialSessionStateBuilder.IsCmdlet(type))
					{
						IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(type);
						foreach (CustomAttributeData customAttributeData in customAttributes)
						{
							if (string.Equals(customAttributeData.Constructor.DeclaringType.Name, "CmdletAttribute"))
							{
								InitialSessionStateBuilder.torusCmdletConfigurationEntries.Add(new CmdletConfigurationEntry(string.Format("{0}-{1}", InitialSessionStateBuilder.UpperFirstChar((string)customAttributeData.ConstructorArguments[0].Value), InitialSessionStateBuilder.UpperFirstChar((string)customAttributeData.ConstructorArguments[1].Value)), type, Path.Combine(ConfigurationContext.Setup.TorusPath, "Microsoft.Office.Datacenter.Torus.Cmdlets.dll-Help.xml")));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.IssBuilderTracer.TraceError<string>(0L, "Failure to load Torus cmdlet, error: {0}", ex.Message);
			}
			finally
			{
				AppDomain.CurrentDomain.AssemblyResolve -= InitialSessionStateBuilder.FindAssembly;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00046BA4 File Offset: 0x00044DA4
		private static string UpperFirstChar(string s)
		{
			return s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00046BC8 File Offset: 0x00044DC8
		private static bool IsCmdlet(Type type)
		{
			while (type.BaseType != null)
			{
				if (string.Equals(type.Name, "PSCmdlet") || string.Equals(type.Name, "Cmdlet"))
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00046C14 File Offset: 0x00044E14
		internal static InitialSessionState Build(List<RoleEntry> allCmdlets, List<RoleEntry> allScripts, ExchangeRunspaceConfiguration runspaceConfig)
		{
			InitialSessionState result;
			using (new MonitoredScope("InitialSessionStateBuilder", "Build", AuthZLogHelper.AuthZPerfMonitors))
			{
				ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)runspaceConfig.GetHashCode(), "Entering InitialSessionStateBuilder.Build");
				InitialSessionStateBuilder.InitializeWellKnownSnapinsIfNeeded(runspaceConfig.ConfigurationSettings, runspaceConfig.IsPowerShellWebService);
				InitialSessionState initialSessionState = InitialSessionState.Create();
				int arg = 0;
				int arg2 = 0;
				int num = 0;
				InitialSessionStateBuilder.AddEntriesToIss(allCmdlets, initialSessionState, ref arg, ref arg2, ref num, ref runspaceConfig);
				InitialSessionStateBuilder.AddEntriesToIss(allScripts, initialSessionState, ref arg, ref arg2, ref num, ref runspaceConfig);
				if (InitialSessionStateBuilder.issHasScriptEntry)
				{
					InitialSessionStateBuilder.FileSystemProviderEntry = new SessionStateProviderEntry("FileSystem", typeof(FileSystemProvider), null);
					InitialSessionStateBuilder.FileSystemProviderEntry.Visibility = SessionStateEntryVisibility.Private;
					initialSessionState.Providers.AddNotNullItem(InitialSessionStateBuilder.FileSystemProviderEntry);
					InitialSessionStateBuilder.EnvironmentProviderEntry = new SessionStateProviderEntry("Environment", typeof(EnvironmentProvider), null);
					InitialSessionStateBuilder.EnvironmentProviderEntry.Visibility = SessionStateEntryVisibility.Private;
					initialSessionState.Providers.AddNotNullItem(InitialSessionStateBuilder.EnvironmentProviderEntry);
				}
				InitialSessionStateBuilder.PopulatePrivateEntries(initialSessionState, runspaceConfig);
				using (new MonitoredScope("InitialSessionStateBuilder", "ExchangePropertyContainer.InitExchangePropertyContainer", AuthZLogHelper.AuthZPerfMonitors))
				{
					ExchangePropertyContainer.InitExchangePropertyContainer(initialSessionState, runspaceConfig);
				}
				if (InitialSessionStateBuilder.IsFormatXMLEnabled(runspaceConfig.ConfigurationSettings, runspaceConfig.IsPowerShellWebService))
				{
					SessionStateTypeEntry sessionStateTypeEntry = null;
					if ((runspaceConfig.ConfigurationSettings.CurrentSerializationLevel == ExchangeRunspaceConfigurationSettings.SerializationLevel.Full && runspaceConfig.ConfigurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.DepthTwoTypeEntry.Enabled) || runspaceConfig.ConfigurationSettings.ProxyFullSerialization)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "DepthTwo Serialization for the current Runspace, reduced types entries added to the ISS");
						sessionStateTypeEntry = InitialSessionStateBuilder.DepthTwoTypesEntry;
					}
					else if (runspaceConfig.ConfigurationSettings.CurrentSerializationLevel == ExchangeRunspaceConfigurationSettings.SerializationLevel.Full && InitialSessionStateBuilder.isCentralAdminCmdletInstalled)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Enabled Serialization, adding cached Central Admin Types to the InitialSessionState");
						sessionStateTypeEntry = InitialSessionStateBuilder.CentralAdminTypesEntry;
					}
					else if (runspaceConfig.ConfigurationSettings.CurrentSerializationLevel == ExchangeRunspaceConfigurationSettings.SerializationLevel.Full)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Enabled Serialization, adding cached Types to the InitialSessionState");
						sessionStateTypeEntry = InitialSessionStateBuilder.TypesEntry;
					}
					else if (runspaceConfig.ConfigurationSettings.CurrentSerializationLevel == ExchangeRunspaceConfigurationSettings.SerializationLevel.Partial)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Limited Serialization for the current Runspace, reduced types entries added to the ISS");
						sessionStateTypeEntry = InitialSessionStateBuilder.PartialTypesEntry;
					}
					else
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Serialization Disabled for the current Runspace, no types entries added to the ISS");
					}
					if (sessionStateTypeEntry != null)
					{
						initialSessionState.Types.AddNotNullItem(sessionStateTypeEntry);
						runspaceConfig.TypeTable = sessionStateTypeEntry.TypeTable;
					}
				}
				if (InitialSessionStateBuilder.IsFormatXMLEnabled(runspaceConfig.ConfigurationSettings, runspaceConfig.IsPowerShellWebService))
				{
					ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Adding cached Formats to the InitialSessionState");
					initialSessionState.Formats.AddNotNullItem(InitialSessionStateBuilder.FormatEntry);
					if (InitialSessionStateBuilder.isCentralAdminCmdletInstalled)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Adding cached CentralAdminFormats to the InitialSessionState");
						initialSessionState.Formats.AddNotNullItem(InitialSessionStateBuilder.CentralAdminFormatEntry);
						if (InitialSessionStateBuilder.isFfoCentralAdminCmdletInstalled)
						{
							ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Adding cached FfoCentralAdminFormats to the InitialSessionState");
							initialSessionState.Formats.AddNotNullItem(InitialSessionStateBuilder.FfoCentralAdminFormatEntry);
						}
					}
					if (InitialSessionStateBuilder.TorusFormatEntry != null)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug((long)initialSessionState.GetHashCode(), "Adding cached TorusFormats to the InitialSessionState");
						initialSessionState.Formats.AddNotNullItem(InitialSessionStateBuilder.TorusFormatEntry);
					}
				}
				initialSessionState.Variables.AddNotNullItem(new SessionStateVariableEntry("ExchangeISSTotalParameters", num, "totalParametersInISS", ScopedItemOptions.ReadOnly));
				initialSessionState.Variables.AddNotNullItem(new SessionStateVariableEntry("ConfirmPreference", ConfirmImpact.High, "ConfirmPreference"));
				initialSessionState.Variables.AddNotNullItem(new SessionStateVariableEntry("LogEngineLifecycleEvent", false, "LogEngineLifecycleEvent"));
				if (ExchangeAuthorizationPlugin.ShouldShowFismaBanner)
				{
					initialSessionState.Variables.AddNotNullItem(new SessionStateVariableEntry("AuthenticationType", runspaceConfig.AuthenticationType, "AuthenticationType"));
					initialSessionState.Variables.AddNotNullItem(new SessionStateVariableEntry("PreferredLanguageMode", (int)runspaceConfig.ConfigurationSettings.LanguageMode, "PreferredLanguageMode"));
					initialSessionState.LanguageMode = PSLanguageMode.FullLanguage;
				}
				else
				{
					initialSessionState.LanguageMode = runspaceConfig.ConfigurationSettings.LanguageMode;
				}
				ExTraceGlobals.IssBuilderTracer.TracePerformance<int, int, int>((long)initialSessionState.GetHashCode(), "Built InitialSessionState: {0} cmdlets, {1} cached, {2} non-cached (generated)", allCmdlets.Count, arg, arg2);
				result = initialSessionState;
			}
			return result;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00047068 File Offset: 0x00045268
		internal static int CalculateHashForImplicitRemoting(InitialSessionState iss)
		{
			IList<SessionStateVariableEntry> list = iss.Variables["ExchangeISSTotalParameters"];
			int num = 0;
			if (list.Count > 0)
			{
				num = (int)list[0].Value;
			}
			string text = iss.Commands.Count.ToString() + "," + num.ToString();
			return text.GetHashCode();
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000470D0 File Offset: 0x000452D0
		private static bool TryAddPrivateScriptEntry(ScriptRoleEntry entry, InitialSessionState iss, out SessionStateCommandEntryWithMetadata commandEntryWithMetadata)
		{
			commandEntryWithMetadata = InitialSessionStateBuilder.GetScriptEntry(entry);
			if (commandEntryWithMetadata != null)
			{
				ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>(0L, "Adding private script entry {0}", entry.Name);
				iss.Commands.AddNotNullItem(commandEntryWithMetadata.SessionStateCommandEntry);
				InitialSessionStateBuilder.issHasScriptEntry = true;
				return true;
			}
			ExTraceGlobals.IssBuilderTracer.TraceError<string>(0L, "Cannot create private script entry {0}", entry.Name);
			return false;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00047134 File Offset: 0x00045334
		private static void AddNotNullItem<T>(this InitialSessionStateEntryCollection<T> collection, T item) where T : InitialSessionStateEntry
		{
			if (item != null)
			{
				collection.Add(item);
				return;
			}
			AuthZLogger.SafeAppendGenericError("ISS.AddNotNullItem", "Null " + typeof(T).FullName, false);
			TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_NullItemAddedIntoInitialSessionStateEntryCollection, null, new object[]
			{
				typeof(T).FullName
			});
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004719C File Offset: 0x0004539C
		private static void AddEntriesToIss(List<RoleEntry> roleEntries, InitialSessionState iss, ref int cachedIssEntriesUsed, ref int nonCachedIssEntriesGenerated, ref int totalParametersInISS, ref ExchangeRunspaceConfiguration runspaceConfiguration)
		{
			using (new MonitoredScope("InitialSessionStateBuilder", "AddEntriesToIss", AuthZLogHelper.AuthZPerfMonitors))
			{
				foreach (RoleEntry roleEntry in roleEntries)
				{
					if (!runspaceConfiguration.IsPowerShellWebService || PowerShellWebServiceExposedCmdlets.IsCmdletExposedToPSWS(runspaceConfiguration, roleEntry.Name))
					{
						if (roleEntry.CachedIssEntry != null)
						{
							ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>((long)iss.GetHashCode(), "Adding cached proxy command {0}", roleEntry.CachedIssEntry.Name);
							iss.Commands.AddNotNullItem(roleEntry.CachedIssEntry);
							cachedIssEntriesUsed++;
							totalParametersInISS += roleEntry.CachedIssEntryParameterCount;
							ScriptRoleEntry scriptRoleEntry = roleEntry as ScriptRoleEntry;
							if (scriptRoleEntry != null)
							{
								SessionStateCommandEntryWithMetadata cmdletEntry;
								InitialSessionStateBuilder.TryAddPrivateScriptEntry(scriptRoleEntry, iss, out cmdletEntry);
							}
						}
						else
						{
							CmdletRoleEntry cmdletRoleEntry = roleEntry as CmdletRoleEntry;
							SessionStateCommandEntryWithMetadata cmdletEntry;
							if (cmdletRoleEntry != null)
							{
								cmdletEntry = InitialSessionStateBuilder.GetCmdletEntry(cmdletRoleEntry);
								if (cmdletEntry == null)
								{
									ExTraceGlobals.IssBuilderTracer.TraceError<string>((long)iss.GetHashCode(), "Cannot find cmdlet {0} in cached snapin information", cmdletRoleEntry.FullName);
									continue;
								}
							}
							else if (!(roleEntry is ScriptRoleEntry) || !InitialSessionStateBuilder.TryAddPrivateScriptEntry((ScriptRoleEntry)roleEntry, iss, out cmdletEntry))
							{
								continue;
							}
							int num = 0;
							SessionStateFunctionEntry proxyCommandEntry = InitialSessionStateBuilder.GetProxyCommandEntry(roleEntry, cmdletEntry, out num);
							if (proxyCommandEntry != null)
							{
								ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>((long)iss.GetHashCode(), "Adding proxy command {0}", proxyCommandEntry.Name);
								iss.Commands.AddNotNullItem(proxyCommandEntry);
								roleEntry.CachedIssEntry = proxyCommandEntry;
								roleEntry.CachedIssEntryParameterCount = num;
								totalParametersInISS += num;
								nonCachedIssEntriesGenerated++;
							}
							else
							{
								ExTraceGlobals.IssBuilderTracer.TraceError<string>((long)iss.GetHashCode(), "Cannot create proxy command for {0}", roleEntry.Name);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0004738C File Offset: 0x0004558C
		private static void PopulatePrivateEntries(InitialSessionState iss, ExchangeRunspaceConfiguration runspaceConfig)
		{
			if (runspaceConfig.RestrictToFilteredCmdlet && runspaceConfig.SortedRoleEntryFilter != null)
			{
				int num = 0;
				foreach (RoleEntry roleEntry in runspaceConfig.SortedRoleEntryFilter)
				{
					CmdletRoleEntry cmdletRoleEntry = roleEntry as CmdletRoleEntry;
					if (cmdletRoleEntry != null && InitialSessionStateBuilder.SnapinCmdletMap.ContainsKey(cmdletRoleEntry.PSSnapinName))
					{
						Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary = InitialSessionStateBuilder.SnapinCmdletMap[cmdletRoleEntry.PSSnapinName];
						if (dictionary.ContainsKey(cmdletRoleEntry.Name))
						{
							SessionStateCommandEntryWithMetadata sessionStateCommandEntryWithMetadata = dictionary[cmdletRoleEntry.Name];
							ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>((long)iss.GetHashCode(), "Adding cmdlet {0}", sessionStateCommandEntryWithMetadata.SessionStateCommandEntry.Name);
							iss.Commands.AddNotNullItem(sessionStateCommandEntryWithMetadata.SessionStateCommandEntry);
							num++;
						}
					}
				}
				ExTraceGlobals.IssBuilderTracer.TraceDebug<int>((long)iss.GetHashCode(), "Added {0} private entries to the InitialSessionState", num);
				return;
			}
			foreach (KeyValuePair<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> keyValuePair in InitialSessionStateBuilder.SnapinCmdletMap)
			{
				if (!VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.GenericExchangeSnapin.Enabled || !"Exchange".Equals(keyValuePair.Key))
				{
					foreach (SessionStateCommandEntryWithMetadata sessionStateCommandEntryWithMetadata2 in keyValuePair.Value.Values)
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>((long)iss.GetHashCode(), "Adding cmdlet {0}", sessionStateCommandEntryWithMetadata2.SessionStateCommandEntry.Name);
						iss.Commands.AddNotNullItem(sessionStateCommandEntryWithMetadata2.SessionStateCommandEntry);
					}
					ExTraceGlobals.IssBuilderTracer.TraceDebug<int, string>((long)iss.GetHashCode(), "Added {0} private entries from snapin {1} to the InitialSessionState", keyValuePair.Value.Count, keyValuePair.Key);
				}
			}
			foreach (SessionStateAliasEntry sessionStateAliasEntry in InitialSessionStateBuilder.aliasList)
			{
				ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>((long)iss.GetHashCode(), "Adding alias {0}", sessionStateAliasEntry.Name);
				iss.Commands.AddNotNullItem(sessionStateAliasEntry);
			}
			ExTraceGlobals.IssBuilderTracer.TraceDebug<int>((long)iss.GetHashCode(), "Added {0} private aliases to the InitialSessionState", InitialSessionStateBuilder.aliasList.Count);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00047630 File Offset: 0x00045830
		internal static SessionStateCommandEntryWithMetadata GetCmdletEntry(CmdletRoleEntry roleEntry)
		{
			bool flag;
			Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary;
			do
			{
				Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> snapinCmdletMap = InitialSessionStateBuilder.SnapinCmdletMap;
				flag = false;
				if (!snapinCmdletMap.TryGetValue(roleEntry.PSSnapinName, out dictionary))
				{
					InitialSessionStateBuilder.TryDiscoverSnapin(roleEntry.PSSnapinName, out dictionary);
					Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> dictionary2 = new Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>>(snapinCmdletMap);
					dictionary2[roleEntry.PSSnapinName] = dictionary;
					Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> dictionary3 = Interlocked.CompareExchange<Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>>>(ref InitialSessionStateBuilder.SnapinCmdletMap, dictionary2, snapinCmdletMap);
					if (dictionary3 != snapinCmdletMap)
					{
						flag = true;
					}
				}
			}
			while (flag);
			SessionStateCommandEntryWithMetadata result;
			dictionary.TryGetValue(roleEntry.Name, out result);
			return result;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x000476A0 File Offset: 0x000458A0
		private static bool TryDiscoverSnapin(string snapinName, out Dictionary<string, SessionStateCommandEntryWithMetadata> snapinEntryMap)
		{
			bool result;
			using (new MonitoredScope("InitialSessionStateBuilder", "TryDiscoverSnapin", AuthZLogHelper.AuthZPerfMonitors))
			{
				InitialSessionState initialSessionState = InitialSessionState.Create();
				try
				{
					PSSnapInException ex;
					initialSessionState.ImportPSSnapIn(snapinName, out ex);
				}
				catch (PSArgumentException ex2)
				{
					ExTraceGlobals.IssBuilderTracer.TraceError<string, PSArgumentException>(0L, "Cannot load snapin {0}: {1}", snapinName, ex2);
					AuthZLogger.SafeAppendGenericError("ISS.TryDiscoverSnapin", ex2.ToString(), false);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_SnapinLoadFailed, null, new object[]
					{
						snapinName,
						ex2.Message
					});
					return false;
				}
				finally
				{
					snapinEntryMap = new Dictionary<string, SessionStateCommandEntryWithMetadata>(initialSessionState.Commands.Count, StringComparer.OrdinalIgnoreCase);
				}
				foreach (SessionStateCommandEntry sessionStateCommandEntry in ((IEnumerable<SessionStateCommandEntry>)initialSessionState.Commands))
				{
					SessionStateCmdletEntry sessionStateCmdletEntry = sessionStateCommandEntry as SessionStateCmdletEntry;
					if (sessionStateCmdletEntry != null)
					{
						sessionStateCmdletEntry.Visibility = SessionStateEntryVisibility.Private;
						CommandMetadata commandMetadata = InitialSessionStateBuilder.GenerateCmdletMetadata(sessionStateCmdletEntry.ImplementingType);
						SessionStateCommandEntryWithMetadata value = new SessionStateCommandEntryWithMetadata(sessionStateCmdletEntry, commandMetadata);
						snapinEntryMap.Add(sessionStateCmdletEntry.Name, value);
					}
				}
				ExTraceGlobals.IssBuilderTracer.TraceDebug<int, string>(0L, "Successfully loaded {0} cmdlets from {1}", snapinEntryMap.Count, snapinName);
				result = true;
			}
			return result;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0004783C File Offset: 0x00045A3C
		private static SessionStateFunctionEntry GetProxyCommandEntry(RoleEntry roleEntry, SessionStateCommandEntryWithMetadata entryWithMetadata, out int entryParamCount)
		{
			SessionStateFunctionEntry result;
			using (new MonitoredScope("InitialSessionStateBuilder", "GetProxyCommandEntry", AuthZLogHelper.AuthZPerfMonitors))
			{
				bool flag = roleEntry is ScriptRoleEntry;
				CommandMetadata commandMetadata = new CommandMetadata(entryWithMetadata.CommandMetadata);
				commandMetadata.Parameters.Clear();
				foreach (string text in roleEntry.Parameters)
				{
					ParameterMetadata parameterMetadata;
					if (flag && InitialSessionStateBuilder.commonCmdletParameters.Contains(text))
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceError<string, string>(0L, "Skipping parameter {0} defined in script {1}. It will be added by Powershell when creating public wrapper function for the script.", text, roleEntry.Name);
					}
					else if (!entryWithMetadata.CommandMetadata.Parameters.TryGetValue(text, out parameterMetadata))
					{
						ExTraceGlobals.IssBuilderDetailTracer.TraceError<string, string>(0L, "Complete metadata for cmdlet {0} does not contain parameter {1}, skipping it.", roleEntry.Name, text);
					}
					else
					{
						commandMetadata.Parameters.Add(parameterMetadata.Name, parameterMetadata);
					}
				}
				entryParamCount = commandMetadata.Parameters.Count;
				SessionStateFunctionEntry sessionStateFunctionEntry = null;
				try
				{
					string text2 = null;
					string helpComment;
					if (flag && commandMetadata.Name.IndexOf(' ') >= 0 && InitialSessionStateBuilder.TryCreateScriptHelpCommentSection(commandMetadata.Name, out helpComment))
					{
						text2 = ProxyCommand.Create(commandMetadata, helpComment);
					}
					if (text2 == null)
					{
						text2 = ProxyCommand.Create(commandMetadata);
					}
					if (typeof(GetCommandCommand) == commandMetadata.CommandType)
					{
						string str = text2.Remove(text2.IndexOf("begin"));
						text2 = str + InitialSessionStateBuilder.getCommandProxyBody;
					}
					else if (typeof(GetHelpCommand) == commandMetadata.CommandType)
					{
						string str2 = text2.Remove(text2.IndexOf("begin"));
						text2 = str2 + InitialSessionStateBuilder.getHelpProxyBody;
					}
					else if (flag)
					{
						string cmdletBindingAttribute = ProxyCommand.GetCmdletBindingAttribute(commandMetadata);
						if (cmdletBindingAttribute.Trim().Length == 0)
						{
							text2 = string.Format("[CmdletBinding()]\r\n{0}", text2);
						}
						if (text2.Contains("$steppablePipeline = $scriptCmd.GetSteppablePipeline($myInvocation.CommandOrigin)"))
						{
							text2 = text2.Replace("$steppablePipeline = $scriptCmd.GetSteppablePipeline($myInvocation.CommandOrigin)", "$steppablePipeline = $scriptCmd.GetSteppablePipeline('Internal')");
						}
					}
					if (InitialSessionStateBuilder.isCentralAdminCmdletInstalled)
					{
						int num = text2.IndexOf("$steppablePipeline.Process");
						if (num >= 0)
						{
							text2 = text2.Insert(num, flag ? InitialSessionStateBuilder.getCentralAdminScriptAuditProxyBody : InitialSessionStateBuilder.getCentralAdminCmdletAuditProxyBody);
						}
					}
					sessionStateFunctionEntry = new SessionStateFunctionEntry(roleEntry.Name, text2);
					sessionStateFunctionEntry.Visibility = SessionStateEntryVisibility.Public;
				}
				catch (Exception arg)
				{
					ExTraceGlobals.IssBuilderTracer.TraceError<string, string, Exception>(0L, "Could not generate proxy command for {0} {1}: {2}", (roleEntry is CmdletRoleEntry) ? "cmdlet" : "script", roleEntry.Name, arg);
				}
				result = sessionStateFunctionEntry;
			}
			return result;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00047B08 File Offset: 0x00045D08
		private static bool TryCreateScriptHelpCommentSection(string scriptPath, out string helpComment)
		{
			helpComment = null;
			string directoryName = Path.GetDirectoryName(scriptPath);
			string fileName = Path.GetFileName(scriptPath);
			StringBuilder stringBuilder = new StringBuilder(260);
			if (NativeMethods.GetShortPathName(directoryName, stringBuilder, 260U) == 0U)
			{
				ExTraceGlobals.IssBuilderTracer.TraceError<string>(0L, "Could not short path for script {0}. Get-Help will not work for this script in remote Powershell.", scriptPath);
				return false;
			}
			stringBuilder.AppendFormat("{0}{1}", Path.DirectorySeparatorChar, fileName);
			helpComment = string.Format(".ForwardHelpTargetName {0}{1}.ForwardHelpCategory ExternalScript", stringBuilder.ToString(), Environment.NewLine);
			return true;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00047B84 File Offset: 0x00045D84
		private static SessionStateCommandEntryWithMetadata GetScriptEntry(ScriptRoleEntry roleEntry)
		{
			if (InitialSessionStateBuilder.IsTimeToRefreshScriptMetadata)
			{
				InitialSessionStateBuilder.UpdateTimeToRefreshScriptMetadata();
				InitialSessionStateBuilder.ScriptMap = new Dictionary<string, SessionStateCommandEntryWithMetadata>(InitialSessionStateBuilder.ScriptMap.Count, InitialSessionStateBuilder.ScriptMap.Comparer);
			}
			bool flag;
			SessionStateCommandEntryWithMetadata sessionStateCommandEntryWithMetadata;
			do
			{
				flag = false;
				Dictionary<string, SessionStateCommandEntryWithMetadata> scriptMap = InitialSessionStateBuilder.ScriptMap;
				if (!scriptMap.TryGetValue(roleEntry.Name, out sessionStateCommandEntryWithMetadata))
				{
					string text = Path.Combine(ConfigurationContext.Setup.RemoteScriptPath, roleEntry.Name);
					string text2 = Path.Combine(ConfigurationContext.Setup.TorusRemoteScriptPath, roleEntry.Name);
					if (File.Exists(text2) && !File.Exists(text))
					{
						text = text2;
					}
					SessionStateScriptEntry sessionStateScriptEntry = new SessionStateScriptEntry(text);
					sessionStateScriptEntry.Visibility = SessionStateEntryVisibility.Private;
					try
					{
						CommandMetadata commandMetadata = new CommandMetadata(text);
						sessionStateCommandEntryWithMetadata = new SessionStateCommandEntryWithMetadata(sessionStateScriptEntry, commandMetadata);
					}
					catch (CommandNotFoundException ex)
					{
						sessionStateCommandEntryWithMetadata = null;
						ExTraceGlobals.IssBuilderTracer.TraceError<string, CommandNotFoundException, Exception>(0L, "Cannot load missing script {0}: got CommandNotFoundException {1}, inner: {2}", roleEntry.Name, ex, ex.InnerException);
						AuthZLogger.SafeAppendGenericError("GetScriptEntry-MissingScript", roleEntry.Name + " " + ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_ScriptNotFound, text, new object[]
						{
							text,
							(ex.InnerException == null) ? ex.Message : ex.InnerException.Message
						});
					}
					catch (ParseException ex2)
					{
						sessionStateCommandEntryWithMetadata = null;
						ExTraceGlobals.IssBuilderTracer.TraceError<string, ParseException>(0L, "Cannot load corrupted script {0}: got ParseException {1}", roleEntry.Name, ex2);
						AuthZLogger.SafeAppendGenericError("GetScriptEntry", string.Concat(new object[]
						{
							text,
							" ",
							roleEntry.Name,
							" ",
							ex2
						}), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_ScriptCorrupted, text, new object[]
						{
							text,
							ex2.Message
						});
					}
					catch (RuntimeException ex3)
					{
						sessionStateCommandEntryWithMetadata = null;
						ExTraceGlobals.IssBuilderTracer.TraceError<string, RuntimeException, Exception>(0L, "Cannot load data type in script {0}: got RuntimeException {1}, inner: {2}", roleEntry.Name, ex3, ex3.InnerException);
						AuthZLogger.SafeAppendGenericError("GetScriptEntry", string.Concat(new object[]
						{
							text,
							" ",
							roleEntry.Name,
							" ",
							ex3
						}), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_RuntimeException, text, new object[]
						{
							text,
							(ex3.InnerException == null) ? ex3.Message : ex3.InnerException.Message
						});
					}
					catch (Exception ex4)
					{
						sessionStateCommandEntryWithMetadata = null;
						ExTraceGlobals.IssBuilderTracer.TraceError<string, Exception, Exception>(0L, "Unknown exception in script {0}: got Exception {1}, inner: {2}", roleEntry.Name, ex4, ex4.InnerException);
						AuthZLogger.SafeAppendGenericError("GetScriptEntry", string.Concat(new object[]
						{
							text,
							" ",
							roleEntry.Name,
							" ",
							ex4
						}), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_RBACUnavailable_UnknownError, null, new object[]
						{
							ex4
						});
					}
					Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary = new Dictionary<string, SessionStateCommandEntryWithMetadata>(scriptMap);
					dictionary[roleEntry.Name] = sessionStateCommandEntryWithMetadata;
					Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary2 = Interlocked.CompareExchange<Dictionary<string, SessionStateCommandEntryWithMetadata>>(ref InitialSessionStateBuilder.ScriptMap, dictionary, scriptMap);
					if (dictionary2 != scriptMap)
					{
						flag = true;
					}
				}
			}
			while (flag);
			return sessionStateCommandEntryWithMetadata;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00047EEC File Offset: 0x000460EC
		private static void UpdateTimeToRefreshScriptMetadata()
		{
			InitialSessionStateBuilder.TimeToRefreshScriptMetadata = DateTime.UtcNow.Add(ExchangeExpiringRunspaceConfiguration.GetMaximumAgeLimit(ExpirationLimit.RunspaceRefresh));
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00047F11 File Offset: 0x00046111
		private static bool IsTimeToRefreshScriptMetadata
		{
			get
			{
				return DateTime.UtcNow >= InitialSessionStateBuilder.TimeToRefreshScriptMetadata;
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00047F22 File Offset: 0x00046122
		internal static void ClearScriptCache(object arg)
		{
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00047F24 File Offset: 0x00046124
		private static bool CheckCentralAdminCmdletInstalled()
		{
			bool result = false;
			RegistryKey localMachine = Registry.LocalMachine;
			using (RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CentralAdmin"))
			{
				if (registryKey != null)
				{
					string value = (string)registryKey.GetValue("DatabaseServer");
					if (!string.IsNullOrEmpty(value))
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00047F80 File Offset: 0x00046180
		private static bool CheckFfoCentralAdminCmdletInstalled()
		{
			bool result = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellSnapIns\\Microsoft.Exchange.Management.Powershell.FfoCentralAdmin"))
			{
				if (registryKey != null)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00047FC4 File Offset: 0x000461C4
		internal static void InitializeWellKnownSnapinsIfNeeded()
		{
			InitialSessionStateBuilder.InitializeWellKnownSnapinsIfNeeded(ExchangeRunspaceConfigurationSettings.GetDefaultInstance());
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00047FD0 File Offset: 0x000461D0
		internal static void InitializeWellKnownSnapinsIfNeeded(ExchangeRunspaceConfigurationSettings settings)
		{
			InitialSessionStateBuilder.InitializeWellKnownSnapinsIfNeeded(settings, false);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00047FDC File Offset: 0x000461DC
		internal static void InitializeWellKnownSnapinsIfNeeded(ExchangeRunspaceConfigurationSettings settings, bool isPowerShellWebServiceSession)
		{
			using (new MonitoredScope("InitialSessionStateBuilder", "InitializeWellKnownSnapinsIfNeeded", AuthZLogHelper.AuthZPerfMonitors))
			{
				if (InitialSessionStateBuilder.SnapinCmdletMap == null)
				{
					lock (InitialSessionStateBuilder.initSyncRoot)
					{
						if (InitialSessionStateBuilder.SnapinCmdletMap == null)
						{
							Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> dictionary = new Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>>(InitialSessionStateBuilder.exchangeCmdletConfigurationEntries.Count, StringComparer.OrdinalIgnoreCase);
							dictionary["Microsoft.Exchange.Management.PowerShell.E2010"] = InitialSessionStateBuilder.InitializeExchangeCmdlets();
							dictionary["Microsoft.Office.Datacenter.Torus.Cmdlets"] = InitialSessionStateBuilder.InitializeTorusCmdlets();
							dictionary["Exchange"] = dictionary["Microsoft.Exchange.Management.PowerShell.E2010"];
							List<SessionStateAliasEntry> localAliasList = new List<SessionStateAliasEntry>();
							InitialSessionStateBuilder.InitializeDefaultPowershellCmdlets(dictionary, isPowerShellWebServiceSession);
							InitialSessionStateBuilder.InitializeDefaultPowershellAliases(localAliasList);
							string text = Path.Combine(ConfigurationContext.Setup.BinPath, "exchange.format.ps1xml");
							ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>(0L, "Adding Format XML files {0} to the InitialSessionState table", text);
							InitialSessionStateBuilder.FormatEntry = new SessionStateFormatEntry(text);
							if (InitialSessionStateBuilder.isCentralAdminCmdletInstalled)
							{
								string fileName = Path.Combine(ConfigurationContext.Setup.BinPath, "exchange.centraladmin.format.ps1xml");
								ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>(0L, "Adding CentralAdminFormat XML files {0} to the InitialSessionState table", text);
								InitialSessionStateBuilder.CentralAdminFormatEntry = new SessionStateFormatEntry(fileName);
								if (InitialSessionStateBuilder.isFfoCentralAdminCmdletInstalled)
								{
									fileName = Path.Combine(ConfigurationContext.Setup.BinPath, "Exchange.FfoCentralAdmin.Format.ps1xml");
									ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>(0L, "Adding FfoCentralAdminFormat XML files {0} to the InitialSessionState table", text);
									InitialSessionStateBuilder.FfoCentralAdminFormatEntry = new SessionStateFormatEntry(fileName);
								}
							}
							string text2 = Path.Combine(ConfigurationContext.Setup.TorusPath, "TorusCmdlets.format.ps1xml");
							if (File.Exists(text2))
							{
								ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>(0L, "Adding Torus format XML files {0} to the InitialSessionState table", text2);
								InitialSessionStateBuilder.TorusFormatEntry = new SessionStateFormatEntry(text2);
							}
							using (new MonitoredScope("InitialSessionStateBuilder", "CmdletAssemblyHelper.LoadingAllCmdletAssembliesAndReference", AuthZLogHelper.AuthZPerfMonitors))
							{
								CmdletAssemblyHelper.LoadingAllCmdletAssembliesAndReference(ConfigurationContext.Setup.BinPath, new string[0]);
								if (File.Exists(Path.Combine(ConfigurationContext.Setup.TorusPath, ConfigurationContext.Setup.TorusCmdletAssembly)))
								{
									try
									{
										TaskHelper.LoadExchangeAssemblyAndReferencesFromSpecificPathForAssemblies(ConfigurationContext.Setup.TorusPath, new string[]
										{
											ConfigurationContext.Setup.TorusCmdletAssembly
										});
									}
									catch (IOException ex)
									{
										ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string>(0L, "Error when loading Torus cmdlets assemblies: {0}", ex.Message);
									}
								}
							}
							string text3 = Path.Combine(ConfigurationContext.Setup.PSHostPath, "types.ps1xml");
							string text4 = Path.Combine(ConfigurationContext.Setup.BinPath, "exchange.types.ps1xml");
							string text5 = Path.Combine(ConfigurationContext.Setup.BinPath, "exchange.partial.types.ps1xml");
							string text6 = Path.Combine(ConfigurationContext.Setup.BinPath, "exchange.DepthTwo.types.ps1xml");
							string text7 = Path.Combine(ConfigurationContext.Setup.BinPath, "exchange.centraladmin.types.ps1xml");
							ExTraceGlobals.IssBuilderDetailTracer.TraceDebug<string, string>(0L, "Adding Types XML files {0} {1} to the InitialSessionState table", text4, text3);
							try
							{
								InitialSessionStateBuilder.TypesEntry = new SessionStateTypeEntry(new TypeTable(new string[]
								{
									text5,
									text4,
									text3
								}));
								InitialSessionStateBuilder.PartialTypesEntry = new SessionStateTypeEntry(new TypeTable(new string[]
								{
									text5,
									text3
								}));
								InitialSessionStateBuilder.DepthTwoTypesEntry = new SessionStateTypeEntry(new TypeTable(new string[]
								{
									text6,
									text4,
									text3
								}));
								if (InitialSessionStateBuilder.isCentralAdminCmdletInstalled)
								{
									TaskHelper.LoadExchangeAssemblyAndReferences("Microsoft.Exchange.Management.Powershell.CentralAdmin.dll");
									InitialSessionStateBuilder.CentralAdminTypesEntry = new SessionStateTypeEntry(new TypeTable(new string[]
									{
										text5,
										text4,
										text7,
										text3
									}));
								}
							}
							catch (TypeTableLoadException ex2)
							{
								StringBuilder stringBuilder = new StringBuilder("The following errors occurred while loading the TypeTable:\n");
								if (ex2.Errors != null)
								{
									foreach (string value in ex2.Errors)
									{
										stringBuilder.AppendLine(value);
										if (stringBuilder.Length > 1024)
										{
											break;
										}
									}
								}
								throw new TypeTableLoadException(stringBuilder.ToString(), ex2);
							}
							InitialSessionStateBuilder.SnapinCmdletMap = dictionary;
							InitialSessionStateBuilder.aliasList = localAliasList;
						}
					}
				}
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00048430 File Offset: 0x00046630
		private static CommandMetadata GenerateCmdletMetadata(Type implementingType)
		{
			CommandMetadata commandMetadata = null;
			try
			{
				commandMetadata = new CommandMetadata(implementingType);
				if (InitialSessionStateBuilder.dynamicParameterTypes != null && InitialSessionStateBuilder.dynamicParameterTypes.ContainsKey(implementingType.FullName))
				{
					Dictionary<string, ParameterMetadata> parameterMetadata = ParameterMetadata.GetParameterMetadata(InitialSessionStateBuilder.dynamicParameterTypes[implementingType.FullName]);
					foreach (ParameterMetadata parameterMetadata2 in parameterMetadata.Values)
					{
						commandMetadata.Parameters.Add(parameterMetadata2.Name, parameterMetadata2);
					}
				}
			}
			catch (TargetInvocationException arg)
			{
				ExTraceGlobals.IssBuilderTracer.TraceError<Type, TargetInvocationException>(0L, "Could not create instance of {0}:{1}", implementingType, arg);
			}
			catch (PSSnapInException arg2)
			{
				ExTraceGlobals.IssBuilderTracer.TraceError<Type, PSSnapInException>(0L, "Could not create metadata for {0}:{1}", implementingType, arg2);
			}
			catch (PSArgumentException arg3)
			{
				ExTraceGlobals.IssBuilderTracer.TraceError<Type, PSArgumentException>(0L, "Could not create metadata for {0}:{1}", implementingType, arg3);
			}
			return commandMetadata;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00048534 File Offset: 0x00046734
		private static Dictionary<string, SessionStateCommandEntryWithMetadata> InitializeExchangeCmdlets()
		{
			Dictionary<string, SessionStateCommandEntryWithMetadata> result;
			using (new MonitoredScope("InitialSessionStateBuilder", "InitializeExchangeCmdlets", AuthZLogHelper.AuthZPerfMonitors))
			{
				CmdletConfigurationEntry[] array = InitialSessionStateBuilder.exchangeCmdletConfigurationEntries.ToArray();
				Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary = new Dictionary<string, SessionStateCommandEntryWithMetadata>(array.Length, StringComparer.OrdinalIgnoreCase);
				foreach (CmdletConfigurationEntry cmdletConfigurationEntry in array)
				{
					dictionary.Add(cmdletConfigurationEntry.Name, InitialSessionStateBuilder.PrivateEntryWithMetadataFromCmdletEntry(cmdletConfigurationEntry));
				}
				ExTraceGlobals.IssBuilderTracer.TraceDebug<int>(0L, "Discovered {0} Exchange cmdlets", dictionary.Count);
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000485D8 File Offset: 0x000467D8
		private static Dictionary<string, SessionStateCommandEntryWithMetadata> InitializeTorusCmdlets()
		{
			Dictionary<string, SessionStateCommandEntryWithMetadata> result;
			using (new MonitoredScope("InitialSessionStateBuilder", "InitializeTorusCmdlets", AuthZLogHelper.AuthZPerfMonitors))
			{
				CmdletConfigurationEntry[] array = InitialSessionStateBuilder.torusCmdletConfigurationEntries.ToArray();
				Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary = new Dictionary<string, SessionStateCommandEntryWithMetadata>(array.Length, StringComparer.OrdinalIgnoreCase);
				foreach (CmdletConfigurationEntry cmdletConfigurationEntry in array)
				{
					dictionary.Add(cmdletConfigurationEntry.Name, InitialSessionStateBuilder.PrivateEntryWithMetadataFromCmdletEntry(cmdletConfigurationEntry));
				}
				ExTraceGlobals.IssBuilderTracer.TraceDebug<int>(0L, "Discovered {0} Torus cmdlets", dictionary.Count);
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0004867C File Offset: 0x0004687C
		private static SessionStateCommandEntryWithMetadata PrivateEntryWithMetadataFromCmdletEntry(CmdletConfigurationEntry entry)
		{
			SessionStateCmdletEntry sessionStateCmdletEntry = new SessionStateCmdletEntry(entry.Name, entry.ImplementingType, entry.HelpFileName);
			sessionStateCmdletEntry.Visibility = SessionStateEntryVisibility.Private;
			CommandMetadata commandMetadata = InitialSessionStateBuilder.GenerateCmdletMetadata(entry.ImplementingType);
			return new SessionStateCommandEntryWithMetadata(sessionStateCmdletEntry, commandMetadata);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000486D8 File Offset: 0x000468D8
		private static void InitializeDefaultPowershellCmdlets(Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> cmdletMap, bool isPowerShellWebServiceSession)
		{
			using (new MonitoredScope("InitialSessionStateBuilder", "InitializeDefaultPowershellCmdlets", AuthZLogHelper.AuthZPerfMonitors))
			{
				IContainsErrorRecord arg = null;
				try
				{
					InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
					foreach (SessionStateCommandEntry sessionStateCommandEntry in ((IEnumerable<SessionStateCommandEntry>)initialSessionState.Commands))
					{
						SessionStateCmdletEntry cmd = sessionStateCommandEntry as SessionStateCmdletEntry;
						if (cmd != null)
						{
							string key = (cmd.PSSnapIn != null) ? cmd.PSSnapIn.Name : ((cmd.Module != null) ? cmd.Module.Name : string.Empty);
							Dictionary<string, SessionStateCommandEntryWithMetadata> dictionary;
							if (!cmdletMap.TryGetValue(key, out dictionary))
							{
								dictionary = new Dictionary<string, SessionStateCommandEntryWithMetadata>(initialSessionState.Commands.Count, StringComparer.OrdinalIgnoreCase);
								cmdletMap[key] = dictionary;
							}
							Type[] array = InitialSessionStateBuilder.alwaysPublicCommands;
							if (isPowerShellWebServiceSession)
							{
								array = InitialSessionStateBuilder.alwaysPublicCommandsForPSWS;
							}
							if (Array.TrueForAll<Type>(array, (Type publicType) => publicType != cmd.ImplementingType))
							{
								cmd.Visibility = SessionStateEntryVisibility.Private;
							}
							CommandMetadata commandMetadata = InitialSessionStateBuilder.GenerateCmdletMetadata(cmd.ImplementingType);
							SessionStateCommandEntryWithMetadata value = new SessionStateCommandEntryWithMetadata(cmd, commandMetadata);
							dictionary.Add(cmd.Name, value);
						}
					}
					ExTraceGlobals.IssBuilderTracer.TraceDebug<int>(0L, "Discovered {0} Powershell snapins", cmdletMap.Keys.Count);
					return;
				}
				catch (PSSnapInException ex)
				{
					arg = ex;
				}
				catch (PSArgumentException ex2)
				{
					arg = ex2;
				}
				ExTraceGlobals.IssBuilderTracer.TraceError<IContainsErrorRecord>(0L, "InitializeDefaultPowershellCmdlets threw {0}", arg);
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000488E8 File Offset: 0x00046AE8
		private static void InitializeDefaultPowershellAliases(List<SessionStateAliasEntry> localAliasList)
		{
			IContainsErrorRecord arg = null;
			try
			{
				InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
				foreach (SessionStateCommandEntry sessionStateCommandEntry in ((IEnumerable<SessionStateCommandEntry>)initialSessionState.Commands))
				{
					if (sessionStateCommandEntry is SessionStateAliasEntry)
					{
						SessionStateAliasEntry sessionStateAliasEntry = sessionStateCommandEntry as SessionStateAliasEntry;
						sessionStateAliasEntry.Visibility = SessionStateEntryVisibility.Private;
						localAliasList.Add(sessionStateAliasEntry);
					}
				}
				ExTraceGlobals.IssBuilderTracer.TraceDebug<int>(0L, "Discovered {0} Powershell aliases", localAliasList.Count);
				return;
			}
			catch (PSSnapInException ex)
			{
				arg = ex;
			}
			catch (PSArgumentException ex2)
			{
				arg = ex2;
			}
			ExTraceGlobals.IssBuilderTracer.TraceError<IContainsErrorRecord>(0L, "InitializeDefaultPowershellAliases threw {0}", arg);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000489AC File Offset: 0x00046BAC
		private static bool IsFormatXMLEnabled(ExchangeRunspaceConfigurationSettings settings, bool isPowerShellWebServiceSession)
		{
			return !isPowerShellWebServiceSession && settings.ClientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP && settings.ClientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP && settings.ClientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.MigrationService;
		}

		// Token: 0x04000583 RID: 1411
		private const string ExchangeTypesXMLFileName = "exchange.types.ps1xml";

		// Token: 0x04000584 RID: 1412
		private const string ExchangePartialTypesXMLFileName = "exchange.partial.types.ps1xml";

		// Token: 0x04000585 RID: 1413
		private const string ExchangeDepthTwoTypesXMLFileName = "exchange.DepthTwo.types.ps1xml";

		// Token: 0x04000586 RID: 1414
		private const string ExchangeCentralAdminTypesXMLFileName = "exchange.centraladmin.types.ps1xml";

		// Token: 0x04000587 RID: 1415
		private const string PowerShellTypesXMLFileName = "types.ps1xml";

		// Token: 0x04000588 RID: 1416
		private const string ExchangeFormatXMLFileName = "exchange.format.ps1xml";

		// Token: 0x04000589 RID: 1417
		private const string ExchangeCentralAdminFormatXMLFileName = "exchange.centraladmin.format.ps1xml";

		// Token: 0x0400058A RID: 1418
		private const string FfoCentralAdminFormatXMLFileName = "Exchange.FfoCentralAdmin.Format.ps1xml";

		// Token: 0x0400058B RID: 1419
		private const string TorusFormatXMLFileName = "TorusCmdlets.format.ps1xml";

		// Token: 0x0400058C RID: 1420
		private const string ExchangeISSTotalParameters = "ExchangeISSTotalParameters";

		// Token: 0x0400058D RID: 1421
		private const int ScriptRefreshIntervalMinutes = 1;

		// Token: 0x0400058E RID: 1422
		private static List<CmdletConfigurationEntry> exchangeCmdletConfigurationEntries = new List<CmdletConfigurationEntry>();

		// Token: 0x0400058F RID: 1423
		private static List<CmdletConfigurationEntry> torusCmdletConfigurationEntries = new List<CmdletConfigurationEntry>();

		// Token: 0x04000590 RID: 1424
		private static Dictionary<string, Type> dynamicParameterTypes = new Dictionary<string, Type>();

		// Token: 0x04000591 RID: 1425
		private static DateTime TimeToRefreshScriptMetadata = DateTime.UtcNow;

		// Token: 0x04000592 RID: 1426
		private static object initSyncRoot = new object();

		// Token: 0x04000593 RID: 1427
		private static Type[] alwaysPublicCommands = new Type[]
		{
			typeof(OutDefaultCommand),
			typeof(GetFormatDataCommand),
			typeof(SelectObjectCommand),
			typeof(MeasureObjectCommand),
			typeof(ExitPSSessionCommand)
		};

		// Token: 0x04000594 RID: 1428
		private static Type[] alwaysPublicCommandsForPSWS = new Type[]
		{
			typeof(OutDefaultCommand),
			typeof(GetFormatDataCommand),
			typeof(SelectObjectCommand),
			typeof(MeasureObjectCommand),
			typeof(ExitPSSessionCommand),
			typeof(ConvertToXmlCommand)
		};

		// Token: 0x04000595 RID: 1429
		private static Dictionary<string, Dictionary<string, SessionStateCommandEntryWithMetadata>> SnapinCmdletMap = null;

		// Token: 0x04000596 RID: 1430
		private static List<SessionStateAliasEntry> aliasList = null;

		// Token: 0x04000597 RID: 1431
		private static readonly HashSet<string> commonCmdletParameters = new HashSet<string>(new string[]
		{
			"Debug",
			"ErrorAction",
			"ErrorVariable",
			"OutBuffer",
			"OutVariable",
			"Verbose",
			"WarningAction",
			"WarningVariable"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000598 RID: 1432
		private static Dictionary<string, SessionStateCommandEntryWithMetadata> ScriptMap = new Dictionary<string, SessionStateCommandEntryWithMetadata>(64, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000599 RID: 1433
		private static SessionStateTypeEntry TypesEntry;

		// Token: 0x0400059A RID: 1434
		private static SessionStateTypeEntry PartialTypesEntry;

		// Token: 0x0400059B RID: 1435
		private static SessionStateTypeEntry DepthTwoTypesEntry;

		// Token: 0x0400059C RID: 1436
		private static SessionStateTypeEntry CentralAdminTypesEntry;

		// Token: 0x0400059D RID: 1437
		private static SessionStateFormatEntry FormatEntry;

		// Token: 0x0400059E RID: 1438
		private static SessionStateFormatEntry CentralAdminFormatEntry;

		// Token: 0x0400059F RID: 1439
		private static SessionStateFormatEntry FfoCentralAdminFormatEntry;

		// Token: 0x040005A0 RID: 1440
		private static SessionStateFormatEntry TorusFormatEntry;

		// Token: 0x040005A1 RID: 1441
		private static SessionStateProviderEntry FileSystemProviderEntry;

		// Token: 0x040005A2 RID: 1442
		private static SessionStateProviderEntry EnvironmentProviderEntry;

		// Token: 0x040005A3 RID: 1443
		private static bool isCentralAdminCmdletInstalled = false;

		// Token: 0x040005A4 RID: 1444
		private static bool isFfoCentralAdminCmdletInstalled = false;

		// Token: 0x040005A5 RID: 1445
		private static bool issHasScriptEntry = false;

		// Token: 0x040005A6 RID: 1446
		private static readonly string getHelpProxyBody = "\r\nbegin\r\n{\r\n    try\r\n    {\r\n        $outBuffer = $null\r\n        if ($PSBoundParameters.TryGetValue('OutBuffer', [ref]$outBuffer))\r\n        {\r\n            $PSBoundParameters['OutBuffer'] = 1\r\n        }\r\n\r\n        if ($Global:IsGetCommandExecution -NE $True)\r\n        {\r\n            $ISS = [Runspace]::DefaultRunspace.InitialSessionState.Clone()\r\n            $ISS.Commands.Remove('Get-Help', [System.Management.Automation.Runspaces.SessionStateFunctionEntry])\r\n            $ISS.Commands['Get-Help'][0].Visibility = 'Public'\r\n            $Runspace = [RunspaceFactory]::CreateRunspace([System.Management.Automation.Runspaces.InitialSessionState]$ISS)\r\n            $Runspace.Open()\r\n            \r\n            $GetHelpCommand = [Powershell]::Create().AddCommand('Get-Help').AddParameters($PSBoundParameters)\r\n            $GetHelpCommand.Runspace = $Runspace\r\n        }\r\n    }\r\n    catch\r\n    {\r\n        throw\r\n    }\r\n}\r\n\r\nprocess\r\n{\r\n}\r\n\r\nend\r\n{\r\n    try\r\n    {\r\n        if ($GetHelpCommand -NE $Null)\r\n        {\r\n            # This will run the get-help command in a new runspace and also spawn a new thread\r\n            # spawning a new thread is important as we dont want to change the TLS's Runspace\r\n            # reference\r\n            $GetHelpCommand.Invoke()\r\n\r\n            # write errors\r\n            foreach ($Error in $GetHelpCommand.Streams.Error)\r\n            {\r\n                $PSCmdlet.WriteError($Error)\r\n            }\r\n\r\n            # Dispose of the runspace, if it was created by this function call.\r\n            # Otherwise, let the caller dispose of it.\r\n            $GetHelpCommand.Runspace.Close()\r\n            $GetHelpCommand.Runspace.Dispose()\r\n            $GetHelpCommand.Dispose()\r\n            $GetHelpCommand = $null\r\n        }\r\n    }\r\n    catch\r\n    {\r\n        throw\r\n    }\r\n}\r\n";

		// Token: 0x040005A7 RID: 1447
		private static readonly string getCommandProxyBody = "\r\nbegin\r\n{\r\n    try {\r\n        $outBuffer = $null\r\n        if ($PSBoundParameters.TryGetValue('OutBuffer', [ref]$outBuffer))\r\n        {\r\n            $PSBoundParameters['OutBuffer'] = 1\r\n        }\r\n\r\n        # get-command is used by Import-PSSession (from the client) to pull\r\n        # metadata from the server including helpURI. running get-help is\r\n        # costly as it loads and parses help files.\r\n        $Global:IsGetCommandExecution = $True\r\n\r\n        $WrappedCmd = $ExecutionContext.InvokeCommand.GetCommand('Get-Command', [System.Management.Automation.CommandTypes]::Cmdlet)\r\n        $ScriptCmd = {& $WrappedCmd @PSBoundParameters }\r\n        $SteppablePipeline = $ScriptCmd.GetSteppablePipeline($MyInvocation.CommandOrigin)\r\n        $SteppablePipeline.Begin($PSCmdlet)\r\n    } catch {\r\n        throw\r\n    }\r\n}\r\n\r\nprocess\r\n{\r\n    try {\r\n        $steppablePipeline.Process($_)\r\n    } catch {\r\n        throw\r\n    }\r\n}\r\n\r\nend\r\n{\r\n    try {\r\n        $steppablePipeline.End()\r\n        $Global:IsGetCommandExecution = $False\r\n    } catch {\r\n        throw\r\n    }\r\n}\r\n<#\r\n\r\n.ForwardHelpTargetName Get-Command\r\n.ForwardHelpCategory Cmdlet\r\n\r\n#>\r\n";

		// Token: 0x040005A8 RID: 1448
		private static readonly string getCentralAdminScriptAuditProxyBody = "[Microsoft.Exchange.Management.Powershell.CentralAdmin.CentralAdminAudit]::AuditHandler.Log(1, $wrappedCmd.Name, $PSBoundParameters, $ExecutionContext)\r\n        ";

		// Token: 0x040005A9 RID: 1449
		private static readonly string getCentralAdminCmdletAuditProxyBody = "[Microsoft.Exchange.Management.Powershell.CentralAdmin.CentralAdminAudit]::AuditHandler.Log(0, $wrappedCmd.Name, $PSBoundParameters, $ExecutionContext)\r\n        ";
	}
}
