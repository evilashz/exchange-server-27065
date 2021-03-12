using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Runtime.Remoting
{
	// Token: 0x02000782 RID: 1922
	internal static class RemotingConfigHandler
	{
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x0012994B File Offset: 0x00127B4B
		// (set) Token: 0x06005401 RID: 21505 RVA: 0x00129970 File Offset: 0x00127B70
		internal static string ApplicationName
		{
			get
			{
				if (RemotingConfigHandler._applicationName == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Config_NoAppName"));
				}
				return RemotingConfigHandler._applicationName;
			}
			set
			{
				if (RemotingConfigHandler._applicationName != null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_AppNameSet"), RemotingConfigHandler._applicationName));
				}
				RemotingConfigHandler._applicationName = value;
				char[] trimChars = new char[]
				{
					'/'
				};
				if (RemotingConfigHandler._applicationName.StartsWith("/", StringComparison.Ordinal))
				{
					RemotingConfigHandler._applicationName = RemotingConfigHandler._applicationName.TrimStart(trimChars);
				}
				if (RemotingConfigHandler._applicationName.EndsWith("/", StringComparison.Ordinal))
				{
					RemotingConfigHandler._applicationName = RemotingConfigHandler._applicationName.TrimEnd(trimChars);
				}
			}
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00129A0B File Offset: 0x00127C0B
		internal static bool HasApplicationNameBeenSet()
		{
			return RemotingConfigHandler._applicationName != null;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x00129A17 File Offset: 0x00127C17
		internal static bool UrlObjRefMode
		{
			get
			{
				return RemotingConfigHandler._bUrlObjRefMode;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x00129A20 File Offset: 0x00127C20
		// (set) Token: 0x06005405 RID: 21509 RVA: 0x00129A29 File Offset: 0x00127C29
		internal static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfigHandler._errorMode;
			}
			set
			{
				if (RemotingConfigHandler._errorsModeSet)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Config_ErrorsModeSet"));
				}
				RemotingConfigHandler._errorMode = value;
				RemotingConfigHandler._errorsModeSet = true;
			}
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00129A54 File Offset: 0x00127C54
		[SecurityCritical]
		internal static IMessageSink FindDelayLoadChannelForCreateMessageSink(string url, object data, out string objectURI)
		{
			RemotingConfigHandler.LoadMachineConfigIfNecessary();
			objectURI = null;
			foreach (object obj in RemotingConfigHandler._delayLoadChannelConfigQueue)
			{
				DelayLoadClientChannelEntry delayLoadClientChannelEntry = (DelayLoadClientChannelEntry)obj;
				IChannelSender channel = delayLoadClientChannelEntry.Channel;
				if (channel != null)
				{
					IMessageSink messageSink = channel.CreateMessageSink(url, data, out objectURI);
					if (messageSink != null)
					{
						delayLoadClientChannelEntry.RegisterChannel();
						return messageSink;
					}
				}
			}
			return null;
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00129AD8 File Offset: 0x00127CD8
		[SecurityCritical]
		private static void LoadMachineConfigIfNecessary()
		{
			if (!RemotingConfigHandler._bMachineConfigLoaded)
			{
				RemotingConfigHandler.RemotingConfigInfo info = RemotingConfigHandler.Info;
				lock (info)
				{
					if (!RemotingConfigHandler._bMachineConfigLoaded)
					{
						RemotingXmlConfigFileData remotingXmlConfigFileData = RemotingXmlConfigFileParser.ParseDefaultConfiguration();
						if (remotingXmlConfigFileData != null)
						{
							RemotingConfigHandler.ConfigureRemoting(remotingXmlConfigFileData, false);
						}
						string machineDirectory = Config.MachineDirectory;
						string text = machineDirectory + "machine.config";
						new FileIOPermission(FileIOPermissionAccess.Read, text).Assert();
						remotingXmlConfigFileData = RemotingConfigHandler.LoadConfigurationFromXmlFile(text);
						if (remotingXmlConfigFileData != null)
						{
							RemotingConfigHandler.ConfigureRemoting(remotingXmlConfigFileData, false);
						}
						RemotingConfigHandler._bMachineConfigLoaded = true;
					}
				}
			}
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00129B70 File Offset: 0x00127D70
		[SecurityCritical]
		internal static void DoConfiguration(string filename, bool ensureSecurity)
		{
			RemotingConfigHandler.LoadMachineConfigIfNecessary();
			RemotingXmlConfigFileData remotingXmlConfigFileData = RemotingConfigHandler.LoadConfigurationFromXmlFile(filename);
			if (remotingXmlConfigFileData != null)
			{
				RemotingConfigHandler.ConfigureRemoting(remotingXmlConfigFileData, ensureSecurity);
			}
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00129B94 File Offset: 0x00127D94
		private static RemotingXmlConfigFileData LoadConfigurationFromXmlFile(string filename)
		{
			RemotingXmlConfigFileData result;
			try
			{
				if (filename != null)
				{
					result = RemotingXmlConfigFileParser.ParseConfigFile(filename);
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex.InnerException as FileNotFoundException;
				if (ex2 != null)
				{
					ex = ex2;
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ReadFailure"), filename, ex));
			}
			return result;
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00129BF0 File Offset: 0x00127DF0
		[SecurityCritical]
		private static void ConfigureRemoting(RemotingXmlConfigFileData configData, bool ensureSecurity)
		{
			try
			{
				string applicationName = configData.ApplicationName;
				if (applicationName != null)
				{
					RemotingConfigHandler.ApplicationName = applicationName;
				}
				if (configData.CustomErrors != null)
				{
					RemotingConfigHandler._errorMode = configData.CustomErrors.Mode;
				}
				RemotingConfigHandler.ConfigureChannels(configData, ensureSecurity);
				if (configData.Lifetime != null)
				{
					if (configData.Lifetime.IsLeaseTimeSet)
					{
						LifetimeServices.LeaseTime = configData.Lifetime.LeaseTime;
					}
					if (configData.Lifetime.IsRenewOnCallTimeSet)
					{
						LifetimeServices.RenewOnCallTime = configData.Lifetime.RenewOnCallTime;
					}
					if (configData.Lifetime.IsSponsorshipTimeoutSet)
					{
						LifetimeServices.SponsorshipTimeout = configData.Lifetime.SponsorshipTimeout;
					}
					if (configData.Lifetime.IsLeaseManagerPollTimeSet)
					{
						LifetimeServices.LeaseManagerPollTime = configData.Lifetime.LeaseManagerPollTime;
					}
				}
				RemotingConfigHandler._bUrlObjRefMode = configData.UrlObjRefMode;
				RemotingConfigHandler.Info.StoreRemoteAppEntries(configData);
				RemotingConfigHandler.Info.StoreActivatedExports(configData);
				RemotingConfigHandler.Info.StoreInteropEntries(configData);
				RemotingConfigHandler.Info.StoreWellKnownExports(configData);
				if (configData.ServerActivatedEntries.Count > 0)
				{
					ActivationServices.StartListeningForRemoteRequests();
				}
			}
			catch (Exception arg)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ConfigurationFailure"), arg));
			}
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00129D24 File Offset: 0x00127F24
		[SecurityCritical]
		private static void ConfigureChannels(RemotingXmlConfigFileData configData, bool ensureSecurity)
		{
			RemotingServices.RegisterWellKnownChannels();
			foreach (object obj in configData.ChannelEntries)
			{
				RemotingXmlConfigFileData.ChannelEntry channelEntry = (RemotingXmlConfigFileData.ChannelEntry)obj;
				if (!channelEntry.DelayLoad)
				{
					IChannel chnl = RemotingConfigHandler.CreateChannelFromConfigEntry(channelEntry);
					ChannelServices.RegisterChannel(chnl, ensureSecurity);
				}
				else
				{
					RemotingConfigHandler._delayLoadChannelConfigQueue.Enqueue(new DelayLoadClientChannelEntry(channelEntry, ensureSecurity));
				}
			}
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00129DA8 File Offset: 0x00127FA8
		[SecurityCritical]
		internal static IChannel CreateChannelFromConfigEntry(RemotingXmlConfigFileData.ChannelEntry entry)
		{
			Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(entry.TypeName, entry.AssemblyName);
			bool flag = typeof(IChannelReceiver).IsAssignableFrom(type);
			bool flag2 = typeof(IChannelSender).IsAssignableFrom(type);
			IClientChannelSinkProvider clientChannelSinkProvider = null;
			IServerChannelSinkProvider serverChannelSinkProvider = null;
			if (entry.ClientSinkProviders.Count > 0)
			{
				clientChannelSinkProvider = RemotingConfigHandler.CreateClientChannelSinkProviderChain(entry.ClientSinkProviders);
			}
			if (entry.ServerSinkProviders.Count > 0)
			{
				serverChannelSinkProvider = RemotingConfigHandler.CreateServerChannelSinkProviderChain(entry.ServerSinkProviders);
			}
			object[] args;
			if (flag && flag2)
			{
				args = new object[]
				{
					entry.Properties,
					clientChannelSinkProvider,
					serverChannelSinkProvider
				};
			}
			else if (flag)
			{
				args = new object[]
				{
					entry.Properties,
					serverChannelSinkProvider
				};
			}
			else
			{
				if (!flag2)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidChannelType"), type.FullName));
				}
				args = new object[]
				{
					entry.Properties,
					clientChannelSinkProvider
				};
			}
			IChannel result = null;
			try
			{
				result = (IChannel)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, null);
			}
			catch (MissingMethodException)
			{
				string arg = null;
				if (flag && flag2)
				{
					arg = "MyChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider, IServerChannelSinkProvider serverSinkProvider)";
				}
				else if (flag)
				{
					arg = "MyChannel(IDictionary properties, IServerChannelSinkProvider serverSinkProvider)";
				}
				else if (flag2)
				{
					arg = "MyChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider)";
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ChannelMissingCtor"), type.FullName, arg));
			}
			return result;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00129F18 File Offset: 0x00128118
		[SecurityCritical]
		private static IClientChannelSinkProvider CreateClientChannelSinkProviderChain(ArrayList entries)
		{
			IClientChannelSinkProvider clientChannelSinkProvider = null;
			IClientChannelSinkProvider clientChannelSinkProvider2 = null;
			foreach (object obj in entries)
			{
				RemotingXmlConfigFileData.SinkProviderEntry entry = (RemotingXmlConfigFileData.SinkProviderEntry)obj;
				if (clientChannelSinkProvider == null)
				{
					clientChannelSinkProvider = (IClientChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(entry, false);
					clientChannelSinkProvider2 = clientChannelSinkProvider;
				}
				else
				{
					clientChannelSinkProvider2.Next = (IClientChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(entry, false);
					clientChannelSinkProvider2 = clientChannelSinkProvider2.Next;
				}
			}
			return clientChannelSinkProvider;
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00129F9C File Offset: 0x0012819C
		[SecurityCritical]
		private static IServerChannelSinkProvider CreateServerChannelSinkProviderChain(ArrayList entries)
		{
			IServerChannelSinkProvider serverChannelSinkProvider = null;
			IServerChannelSinkProvider serverChannelSinkProvider2 = null;
			foreach (object obj in entries)
			{
				RemotingXmlConfigFileData.SinkProviderEntry entry = (RemotingXmlConfigFileData.SinkProviderEntry)obj;
				if (serverChannelSinkProvider == null)
				{
					serverChannelSinkProvider = (IServerChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(entry, true);
					serverChannelSinkProvider2 = serverChannelSinkProvider;
				}
				else
				{
					serverChannelSinkProvider2.Next = (IServerChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(entry, true);
					serverChannelSinkProvider2 = serverChannelSinkProvider2.Next;
				}
			}
			return serverChannelSinkProvider;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0012A020 File Offset: 0x00128220
		[SecurityCritical]
		private static object CreateChannelSinkProvider(RemotingXmlConfigFileData.SinkProviderEntry entry, bool bServer)
		{
			object result = null;
			Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(entry.TypeName, entry.AssemblyName);
			if (bServer)
			{
				if (!typeof(IServerChannelSinkProvider).IsAssignableFrom(type))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidSinkProviderType"), type.FullName, "IServerChannelSinkProvider"));
				}
			}
			else if (!typeof(IClientChannelSinkProvider).IsAssignableFrom(type))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidSinkProviderType"), type.FullName, "IClientChannelSinkProvider"));
			}
			if (entry.IsFormatter && ((bServer && !typeof(IServerFormatterSinkProvider).IsAssignableFrom(type)) || (!bServer && !typeof(IClientFormatterSinkProvider).IsAssignableFrom(type))))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_SinkProviderNotFormatter"), type.FullName));
			}
			object[] args = new object[]
			{
				entry.Properties,
				entry.ProviderData
			};
			try
			{
				result = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, null);
			}
			catch (MissingMethodException)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_SinkProviderMissingCtor"), type.FullName, "MySinkProvider(IDictionary properties, ICollection providerData)"));
			}
			return result;
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0012A168 File Offset: 0x00128368
		[SecurityCritical]
		internal static ActivatedClientTypeEntry IsRemotelyActivatedClientType(RuntimeType svrType)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
			string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.FullName, simpleAssemblyName);
			if (activatedClientTypeEntry == null)
			{
				string assemblyName = reflectionCachedData.AssemblyName;
				activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.FullName, assemblyName);
				if (activatedClientTypeEntry == null)
				{
					activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.Name, simpleAssemblyName);
				}
			}
			return activatedClientTypeEntry;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0012A1C7 File Offset: 0x001283C7
		internal static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.Info.QueryRemoteActivate(typeName, assemblyName);
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0012A1D8 File Offset: 0x001283D8
		[SecurityCritical]
		internal static WellKnownClientTypeEntry IsWellKnownClientType(RuntimeType svrType)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
			string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfigHandler.Info.QueryConnect(svrType.FullName, simpleAssemblyName);
			if (wellKnownClientTypeEntry == null)
			{
				wellKnownClientTypeEntry = RemotingConfigHandler.Info.QueryConnect(svrType.Name, simpleAssemblyName);
			}
			return wellKnownClientTypeEntry;
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0012A21B File Offset: 0x0012841B
		internal static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.Info.QueryConnect(typeName, assemblyName);
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0012A22C File Offset: 0x0012842C
		private static void ParseGenericType(string typeAssem, int indexStart, out string typeName, out string assemName)
		{
			int length = typeAssem.Length;
			int num = 1;
			int num2 = indexStart;
			while (num > 0 && ++num2 < length - 1)
			{
				if (typeAssem[num2] == '[')
				{
					num++;
				}
				else if (typeAssem[num2] == ']')
				{
					num--;
				}
			}
			if (num > 0 || num2 >= length)
			{
				typeName = null;
				assemName = null;
				return;
			}
			num2 = typeAssem.IndexOf(',', num2);
			if (num2 >= 0 && num2 < length - 1)
			{
				typeName = typeAssem.Substring(0, num2).Trim();
				assemName = typeAssem.Substring(num2 + 1).Trim();
				return;
			}
			typeName = null;
			assemName = null;
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0012A2C0 File Offset: 0x001284C0
		internal static void ParseType(string typeAssem, out string typeName, out string assemName)
		{
			int num = typeAssem.IndexOf("[");
			if (num >= 0 && num < typeAssem.Length - 1)
			{
				RemotingConfigHandler.ParseGenericType(typeAssem, num, out typeName, out assemName);
				return;
			}
			int num2 = typeAssem.IndexOf(",");
			if (num2 >= 0 && num2 < typeAssem.Length - 1)
			{
				typeName = typeAssem.Substring(0, num2).Trim();
				assemName = typeAssem.Substring(num2 + 1).Trim();
				return;
			}
			typeName = null;
			assemName = null;
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0012A338 File Offset: 0x00128538
		[SecurityCritical]
		internal static bool IsActivationAllowed(RuntimeType svrType)
		{
			if (svrType == null)
			{
				return false;
			}
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
			string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
			return RemotingConfigHandler.Info.ActivationAllowed(svrType.FullName, simpleAssemblyName);
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0012A370 File Offset: 0x00128570
		[SecurityCritical]
		internal static bool IsActivationAllowed(string TypeName)
		{
			string text = RemotingServices.InternalGetTypeNameFromQualifiedTypeName(TypeName);
			if (text == null)
			{
				return false;
			}
			string typeName;
			string text2;
			RemotingConfigHandler.ParseType(text, out typeName, out text2);
			if (text2 == null)
			{
				return false;
			}
			int num = text2.IndexOf(',');
			if (num != -1)
			{
				text2 = text2.Substring(0, num);
			}
			return RemotingConfigHandler.Info.ActivationAllowed(typeName, text2);
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0012A3BA File Offset: 0x001285BA
		internal static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			RemotingConfigHandler.Info.AddActivatedType(entry.TypeName, entry.AssemblyName, entry.ContextAttributes);
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0012A3D8 File Offset: 0x001285D8
		[SecurityCritical]
		internal static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			string typeName = entry.TypeName;
			string assemblyName = entry.AssemblyName;
			string objectUri = entry.ObjectUri;
			WellKnownObjectMode mode = entry.Mode;
			RemotingConfigHandler.RemotingConfigInfo info = RemotingConfigHandler.Info;
			lock (info)
			{
				RemotingConfigHandler.Info.AddWellKnownEntry(entry);
			}
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0012A440 File Offset: 0x00128640
		internal static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			RemotingConfigHandler.Info.AddActivatedClientType(entry);
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x0012A44D File Offset: 0x0012864D
		internal static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			RemotingConfigHandler.Info.AddWellKnownClientType(entry);
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0012A45A File Offset: 0x0012865A
		[SecurityCritical]
		internal static Type GetServerTypeForUri(string URI)
		{
			URI = Identity.RemoveAppNameOrAppGuidIfNecessary(URI);
			return RemotingConfigHandler.Info.GetServerTypeForUri(URI);
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0012A46F File Offset: 0x0012866F
		internal static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredActivatedServiceTypes();
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x0012A47B File Offset: 0x0012867B
		internal static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredWellKnownServiceTypes();
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0012A487 File Offset: 0x00128687
		internal static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredActivatedClientTypes();
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0012A493 File Offset: 0x00128693
		internal static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredWellKnownClientTypes();
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0012A49F File Offset: 0x0012869F
		[SecurityCritical]
		internal static ServerIdentity CreateWellKnownObject(string uri)
		{
			uri = Identity.RemoveAppNameOrAppGuidIfNecessary(uri);
			return RemotingConfigHandler.Info.StartupWellKnownObject(uri);
		}

		// Token: 0x0400267B RID: 9851
		private static volatile string _applicationName;

		// Token: 0x0400267C RID: 9852
		private static volatile CustomErrorsModes _errorMode = CustomErrorsModes.RemoteOnly;

		// Token: 0x0400267D RID: 9853
		private static volatile bool _errorsModeSet = false;

		// Token: 0x0400267E RID: 9854
		private static volatile bool _bMachineConfigLoaded = false;

		// Token: 0x0400267F RID: 9855
		private static volatile bool _bUrlObjRefMode = false;

		// Token: 0x04002680 RID: 9856
		private static Queue _delayLoadChannelConfigQueue = new Queue();

		// Token: 0x04002681 RID: 9857
		public static RemotingConfigHandler.RemotingConfigInfo Info = new RemotingConfigHandler.RemotingConfigInfo();

		// Token: 0x04002682 RID: 9858
		private const string _machineConfigFilename = "machine.config";

		// Token: 0x02000C35 RID: 3125
		internal class RemotingConfigInfo
		{
			// Token: 0x06006F5E RID: 28510 RVA: 0x0017EA90 File Offset: 0x0017CC90
			internal RemotingConfigInfo()
			{
				this._remoteTypeInfo = Hashtable.Synchronized(new Hashtable());
				this._exportableClasses = Hashtable.Synchronized(new Hashtable());
				this._remoteAppInfo = Hashtable.Synchronized(new Hashtable());
				this._wellKnownExportInfo = Hashtable.Synchronized(new Hashtable());
			}

			// Token: 0x06006F5F RID: 28511 RVA: 0x0017EAE3 File Offset: 0x0017CCE3
			private string EncodeTypeAndAssemblyNames(string typeName, string assemblyName)
			{
				return typeName + ", " + assemblyName.ToLower(CultureInfo.InvariantCulture);
			}

			// Token: 0x06006F60 RID: 28512 RVA: 0x0017EAFC File Offset: 0x0017CCFC
			internal void StoreActivatedExports(RemotingXmlConfigFileData configData)
			{
				foreach (object obj in configData.ServerActivatedEntries)
				{
					RemotingXmlConfigFileData.TypeEntry typeEntry = (RemotingXmlConfigFileData.TypeEntry)obj;
					RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(typeEntry.TypeName, typeEntry.AssemblyName)
					{
						ContextAttributes = RemotingConfigHandler.RemotingConfigInfo.CreateContextAttributesFromConfigEntries(typeEntry.ContextAttributes)
					});
				}
			}

			// Token: 0x06006F61 RID: 28513 RVA: 0x0017EB78 File Offset: 0x0017CD78
			[SecurityCritical]
			internal void StoreInteropEntries(RemotingXmlConfigFileData configData)
			{
				foreach (object obj in configData.InteropXmlElementEntries)
				{
					RemotingXmlConfigFileData.InteropXmlElementEntry interopXmlElementEntry = (RemotingXmlConfigFileData.InteropXmlElementEntry)obj;
					Assembly assembly = Assembly.Load(interopXmlElementEntry.UrtAssemblyName);
					Type type = assembly.GetType(interopXmlElementEntry.UrtTypeName);
					SoapServices.RegisterInteropXmlElement(interopXmlElementEntry.XmlElementName, interopXmlElementEntry.XmlElementNamespace, type);
				}
				foreach (object obj2 in configData.InteropXmlTypeEntries)
				{
					RemotingXmlConfigFileData.InteropXmlTypeEntry interopXmlTypeEntry = (RemotingXmlConfigFileData.InteropXmlTypeEntry)obj2;
					Assembly assembly2 = Assembly.Load(interopXmlTypeEntry.UrtAssemblyName);
					Type type2 = assembly2.GetType(interopXmlTypeEntry.UrtTypeName);
					SoapServices.RegisterInteropXmlType(interopXmlTypeEntry.XmlTypeName, interopXmlTypeEntry.XmlTypeNamespace, type2);
				}
				foreach (object obj3 in configData.PreLoadEntries)
				{
					RemotingXmlConfigFileData.PreLoadEntry preLoadEntry = (RemotingXmlConfigFileData.PreLoadEntry)obj3;
					Assembly assembly3 = Assembly.Load(preLoadEntry.AssemblyName);
					if (preLoadEntry.TypeName != null)
					{
						Type type3 = assembly3.GetType(preLoadEntry.TypeName);
						SoapServices.PreLoad(type3);
					}
					else
					{
						SoapServices.PreLoad(assembly3);
					}
				}
			}

			// Token: 0x06006F62 RID: 28514 RVA: 0x0017ECF4 File Offset: 0x0017CEF4
			internal void StoreRemoteAppEntries(RemotingXmlConfigFileData configData)
			{
				char[] trimChars = new char[]
				{
					'/'
				};
				foreach (object obj in configData.RemoteAppEntries)
				{
					RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = (RemotingXmlConfigFileData.RemoteAppEntry)obj;
					string text = remoteAppEntry.AppUri;
					if (text != null && !text.EndsWith("/", StringComparison.Ordinal))
					{
						text = text.TrimEnd(trimChars);
					}
					foreach (object obj2 in remoteAppEntry.ActivatedObjects)
					{
						RemotingXmlConfigFileData.TypeEntry typeEntry = (RemotingXmlConfigFileData.TypeEntry)obj2;
						RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(typeEntry.TypeName, typeEntry.AssemblyName, text)
						{
							ContextAttributes = RemotingConfigHandler.RemotingConfigInfo.CreateContextAttributesFromConfigEntries(typeEntry.ContextAttributes)
						});
					}
					foreach (object obj3 in remoteAppEntry.WellKnownObjects)
					{
						RemotingXmlConfigFileData.ClientWellKnownEntry clientWellKnownEntry = (RemotingXmlConfigFileData.ClientWellKnownEntry)obj3;
						RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(clientWellKnownEntry.TypeName, clientWellKnownEntry.AssemblyName, clientWellKnownEntry.Url)
						{
							ApplicationUrl = text
						});
					}
				}
			}

			// Token: 0x06006F63 RID: 28515 RVA: 0x0017EE8C File Offset: 0x0017D08C
			[SecurityCritical]
			internal void StoreWellKnownExports(RemotingXmlConfigFileData configData)
			{
				foreach (object obj in configData.ServerWellKnownEntries)
				{
					RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry = (RemotingXmlConfigFileData.ServerWellKnownEntry)obj;
					RemotingConfigHandler.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(serverWellKnownEntry.TypeName, serverWellKnownEntry.AssemblyName, serverWellKnownEntry.ObjectURI, serverWellKnownEntry.ObjectMode)
					{
						ContextAttributes = null
					});
				}
			}

			// Token: 0x06006F64 RID: 28516 RVA: 0x0017EF0C File Offset: 0x0017D10C
			private static IContextAttribute[] CreateContextAttributesFromConfigEntries(ArrayList contextAttributes)
			{
				int count = contextAttributes.Count;
				if (count == 0)
				{
					return null;
				}
				IContextAttribute[] array = new IContextAttribute[count];
				int num = 0;
				foreach (object obj in contextAttributes)
				{
					RemotingXmlConfigFileData.ContextAttributeEntry contextAttributeEntry = (RemotingXmlConfigFileData.ContextAttributeEntry)obj;
					Assembly assembly = Assembly.Load(contextAttributeEntry.AssemblyName);
					Hashtable properties = contextAttributeEntry.Properties;
					IContextAttribute contextAttribute;
					if (properties != null && properties.Count > 0)
					{
						object[] args = new object[]
						{
							properties
						};
						contextAttribute = (IContextAttribute)Activator.CreateInstance(assembly.GetType(contextAttributeEntry.TypeName, false, false), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, args, null, null);
					}
					else
					{
						contextAttribute = (IContextAttribute)Activator.CreateInstance(assembly.GetType(contextAttributeEntry.TypeName, false, false), true);
					}
					array[num++] = contextAttribute;
				}
				return array;
			}

			// Token: 0x06006F65 RID: 28517 RVA: 0x0017F000 File Offset: 0x0017D200
			internal bool ActivationAllowed(string typeName, string assemblyName)
			{
				return this._exportableClasses.ContainsKey(this.EncodeTypeAndAssemblyNames(typeName, assemblyName));
			}

			// Token: 0x06006F66 RID: 28518 RVA: 0x0017F018 File Offset: 0x0017D218
			internal ActivatedClientTypeEntry QueryRemoteActivate(string typeName, string assemblyName)
			{
				string key = this.EncodeTypeAndAssemblyNames(typeName, assemblyName);
				ActivatedClientTypeEntry activatedClientTypeEntry = this._remoteTypeInfo[key] as ActivatedClientTypeEntry;
				if (activatedClientTypeEntry == null)
				{
					return null;
				}
				if (activatedClientTypeEntry.GetRemoteAppEntry() == null)
				{
					RemoteAppEntry remoteAppEntry = (RemoteAppEntry)this._remoteAppInfo[activatedClientTypeEntry.ApplicationUrl];
					if (remoteAppEntry == null)
					{
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_MissingRemoteAppEntry"), activatedClientTypeEntry.ApplicationUrl));
					}
					activatedClientTypeEntry.CacheRemoteAppEntry(remoteAppEntry);
				}
				return activatedClientTypeEntry;
			}

			// Token: 0x06006F67 RID: 28519 RVA: 0x0017F090 File Offset: 0x0017D290
			internal WellKnownClientTypeEntry QueryConnect(string typeName, string assemblyName)
			{
				string key = this.EncodeTypeAndAssemblyNames(typeName, assemblyName);
				WellKnownClientTypeEntry wellKnownClientTypeEntry = this._remoteTypeInfo[key] as WellKnownClientTypeEntry;
				if (wellKnownClientTypeEntry == null)
				{
					return null;
				}
				return wellKnownClientTypeEntry;
			}

			// Token: 0x06006F68 RID: 28520 RVA: 0x0017F0C0 File Offset: 0x0017D2C0
			internal ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
			{
				ActivatedServiceTypeEntry[] array = new ActivatedServiceTypeEntry[this._exportableClasses.Count];
				int num = 0;
				foreach (object obj in this._exportableClasses)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					array[num++] = (ActivatedServiceTypeEntry)dictionaryEntry.Value;
				}
				return array;
			}

			// Token: 0x06006F69 RID: 28521 RVA: 0x0017F13C File Offset: 0x0017D33C
			internal WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
			{
				WellKnownServiceTypeEntry[] array = new WellKnownServiceTypeEntry[this._wellKnownExportInfo.Count];
				int num = 0;
				foreach (object obj in this._wellKnownExportInfo)
				{
					WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)((DictionaryEntry)obj).Value;
					WellKnownServiceTypeEntry wellKnownServiceTypeEntry2 = new WellKnownServiceTypeEntry(wellKnownServiceTypeEntry.TypeName, wellKnownServiceTypeEntry.AssemblyName, wellKnownServiceTypeEntry.ObjectUri, wellKnownServiceTypeEntry.Mode);
					wellKnownServiceTypeEntry2.ContextAttributes = wellKnownServiceTypeEntry.ContextAttributes;
					array[num++] = wellKnownServiceTypeEntry2;
				}
				return array;
			}

			// Token: 0x06006F6A RID: 28522 RVA: 0x0017F1F0 File Offset: 0x0017D3F0
			internal ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
			{
				int num = 0;
				foreach (object obj in this._remoteTypeInfo)
				{
					ActivatedClientTypeEntry activatedClientTypeEntry = ((DictionaryEntry)obj).Value as ActivatedClientTypeEntry;
					if (activatedClientTypeEntry != null)
					{
						num++;
					}
				}
				ActivatedClientTypeEntry[] array = new ActivatedClientTypeEntry[num];
				int num2 = 0;
				foreach (object obj2 in this._remoteTypeInfo)
				{
					ActivatedClientTypeEntry activatedClientTypeEntry2 = ((DictionaryEntry)obj2).Value as ActivatedClientTypeEntry;
					if (activatedClientTypeEntry2 != null)
					{
						string appUrl = null;
						RemoteAppEntry remoteAppEntry = activatedClientTypeEntry2.GetRemoteAppEntry();
						if (remoteAppEntry != null)
						{
							appUrl = remoteAppEntry.GetAppURI();
						}
						ActivatedClientTypeEntry activatedClientTypeEntry3 = new ActivatedClientTypeEntry(activatedClientTypeEntry2.TypeName, activatedClientTypeEntry2.AssemblyName, appUrl);
						activatedClientTypeEntry3.ContextAttributes = activatedClientTypeEntry2.ContextAttributes;
						array[num2++] = activatedClientTypeEntry3;
					}
				}
				return array;
			}

			// Token: 0x06006F6B RID: 28523 RVA: 0x0017F30C File Offset: 0x0017D50C
			internal WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
			{
				int num = 0;
				foreach (object obj in this._remoteTypeInfo)
				{
					WellKnownClientTypeEntry wellKnownClientTypeEntry = ((DictionaryEntry)obj).Value as WellKnownClientTypeEntry;
					if (wellKnownClientTypeEntry != null)
					{
						num++;
					}
				}
				WellKnownClientTypeEntry[] array = new WellKnownClientTypeEntry[num];
				int num2 = 0;
				foreach (object obj2 in this._remoteTypeInfo)
				{
					WellKnownClientTypeEntry wellKnownClientTypeEntry2 = ((DictionaryEntry)obj2).Value as WellKnownClientTypeEntry;
					if (wellKnownClientTypeEntry2 != null)
					{
						WellKnownClientTypeEntry wellKnownClientTypeEntry3 = new WellKnownClientTypeEntry(wellKnownClientTypeEntry2.TypeName, wellKnownClientTypeEntry2.AssemblyName, wellKnownClientTypeEntry2.ObjectUrl);
						RemoteAppEntry remoteAppEntry = wellKnownClientTypeEntry2.GetRemoteAppEntry();
						if (remoteAppEntry != null)
						{
							wellKnownClientTypeEntry3.ApplicationUrl = remoteAppEntry.GetAppURI();
						}
						array[num2++] = wellKnownClientTypeEntry3;
					}
				}
				return array;
			}

			// Token: 0x06006F6C RID: 28524 RVA: 0x0017F420 File Offset: 0x0017D620
			internal void AddActivatedType(string typeName, string assemblyName, IContextAttribute[] contextAttributes)
			{
				if (typeName == null)
				{
					throw new ArgumentNullException("typeName");
				}
				if (assemblyName == null)
				{
					throw new ArgumentNullException("assemblyName");
				}
				if (this.CheckForRedirectedClientType(typeName, assemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantUseRedirectedTypeForWellKnownService"), typeName, assemblyName));
				}
				ActivatedServiceTypeEntry activatedServiceTypeEntry = new ActivatedServiceTypeEntry(typeName, assemblyName);
				activatedServiceTypeEntry.ContextAttributes = contextAttributes;
				string key = this.EncodeTypeAndAssemblyNames(typeName, assemblyName);
				this._exportableClasses.Add(key, activatedServiceTypeEntry);
			}

			// Token: 0x06006F6D RID: 28525 RVA: 0x0017F494 File Offset: 0x0017D694
			private bool CheckForServiceEntryWithType(string typeName, string asmName)
			{
				return this.CheckForWellKnownServiceEntryWithType(typeName, asmName) || this.ActivationAllowed(typeName, asmName);
			}

			// Token: 0x06006F6E RID: 28526 RVA: 0x0017F4AC File Offset: 0x0017D6AC
			private bool CheckForWellKnownServiceEntryWithType(string typeName, string asmName)
			{
				foreach (object obj in this._wellKnownExportInfo)
				{
					WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)((DictionaryEntry)obj).Value;
					if (typeName == wellKnownServiceTypeEntry.TypeName)
					{
						bool flag = false;
						if (asmName == wellKnownServiceTypeEntry.AssemblyName)
						{
							flag = true;
						}
						else if (string.Compare(wellKnownServiceTypeEntry.AssemblyName, 0, asmName, 0, asmName.Length, StringComparison.OrdinalIgnoreCase) == 0 && wellKnownServiceTypeEntry.AssemblyName[asmName.Length] == ',')
						{
							flag = true;
						}
						if (flag)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06006F6F RID: 28527 RVA: 0x0017F56C File Offset: 0x0017D76C
			private bool CheckForRedirectedClientType(string typeName, string asmName)
			{
				int num = asmName.IndexOf(",");
				if (num != -1)
				{
					asmName = asmName.Substring(0, num);
				}
				return this.QueryRemoteActivate(typeName, asmName) != null || this.QueryConnect(typeName, asmName) != null;
			}

			// Token: 0x06006F70 RID: 28528 RVA: 0x0017F5AC File Offset: 0x0017D7AC
			internal void AddActivatedClientType(ActivatedClientTypeEntry entry)
			{
				if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TypeAlreadyRedirected"), entry.TypeName, entry.AssemblyName));
				}
				if (this.CheckForServiceEntryWithType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantRedirectActivationOfWellKnownService"), entry.TypeName, entry.AssemblyName));
				}
				string applicationUrl = entry.ApplicationUrl;
				RemoteAppEntry remoteAppEntry = (RemoteAppEntry)this._remoteAppInfo[applicationUrl];
				if (remoteAppEntry == null)
				{
					remoteAppEntry = new RemoteAppEntry(applicationUrl, applicationUrl);
					this._remoteAppInfo.Add(applicationUrl, remoteAppEntry);
				}
				if (remoteAppEntry != null)
				{
					entry.CacheRemoteAppEntry(remoteAppEntry);
				}
				string key = this.EncodeTypeAndAssemblyNames(entry.TypeName, entry.AssemblyName);
				this._remoteTypeInfo.Add(key, entry);
			}

			// Token: 0x06006F71 RID: 28529 RVA: 0x0017F688 File Offset: 0x0017D888
			internal void AddWellKnownClientType(WellKnownClientTypeEntry entry)
			{
				if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TypeAlreadyRedirected"), entry.TypeName, entry.AssemblyName));
				}
				if (this.CheckForServiceEntryWithType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantRedirectActivationOfWellKnownService"), entry.TypeName, entry.AssemblyName));
				}
				string applicationUrl = entry.ApplicationUrl;
				RemoteAppEntry remoteAppEntry = null;
				if (applicationUrl != null)
				{
					remoteAppEntry = (RemoteAppEntry)this._remoteAppInfo[applicationUrl];
					if (remoteAppEntry == null)
					{
						remoteAppEntry = new RemoteAppEntry(applicationUrl, applicationUrl);
						this._remoteAppInfo.Add(applicationUrl, remoteAppEntry);
					}
				}
				if (remoteAppEntry != null)
				{
					entry.CacheRemoteAppEntry(remoteAppEntry);
				}
				string key = this.EncodeTypeAndAssemblyNames(entry.TypeName, entry.AssemblyName);
				this._remoteTypeInfo.Add(key, entry);
			}

			// Token: 0x06006F72 RID: 28530 RVA: 0x0017F769 File Offset: 0x0017D969
			[SecurityCritical]
			internal void AddWellKnownEntry(WellKnownServiceTypeEntry entry)
			{
				this.AddWellKnownEntry(entry, true);
			}

			// Token: 0x06006F73 RID: 28531 RVA: 0x0017F774 File Offset: 0x0017D974
			[SecurityCritical]
			internal void AddWellKnownEntry(WellKnownServiceTypeEntry entry, bool fReplace)
			{
				if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantUseRedirectedTypeForWellKnownService"), entry.TypeName, entry.AssemblyName));
				}
				string key = entry.ObjectUri.ToLower(CultureInfo.InvariantCulture);
				if (fReplace)
				{
					this._wellKnownExportInfo[key] = entry;
					IdentityHolder.RemoveIdentity(entry.ObjectUri);
					return;
				}
				this._wellKnownExportInfo.Add(key, entry);
			}

			// Token: 0x06006F74 RID: 28532 RVA: 0x0017F7F8 File Offset: 0x0017D9F8
			[SecurityCritical]
			internal Type GetServerTypeForUri(string URI)
			{
				Type result = null;
				string key = URI.ToLower(CultureInfo.InvariantCulture);
				WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)this._wellKnownExportInfo[key];
				if (wellKnownServiceTypeEntry != null)
				{
					result = RemotingConfigHandler.RemotingConfigInfo.LoadType(wellKnownServiceTypeEntry.TypeName, wellKnownServiceTypeEntry.AssemblyName);
				}
				return result;
			}

			// Token: 0x06006F75 RID: 28533 RVA: 0x0017F83C File Offset: 0x0017DA3C
			[SecurityCritical]
			internal ServerIdentity StartupWellKnownObject(string URI)
			{
				string key = URI.ToLower(CultureInfo.InvariantCulture);
				ServerIdentity result = null;
				WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)this._wellKnownExportInfo[key];
				if (wellKnownServiceTypeEntry != null)
				{
					result = this.StartupWellKnownObject(wellKnownServiceTypeEntry.AssemblyName, wellKnownServiceTypeEntry.TypeName, wellKnownServiceTypeEntry.ObjectUri, wellKnownServiceTypeEntry.Mode);
				}
				return result;
			}

			// Token: 0x06006F76 RID: 28534 RVA: 0x0017F88C File Offset: 0x0017DA8C
			[SecurityCritical]
			internal ServerIdentity StartupWellKnownObject(string asmName, string svrTypeName, string URI, WellKnownObjectMode mode)
			{
				return this.StartupWellKnownObject(asmName, svrTypeName, URI, mode, false);
			}

			// Token: 0x06006F77 RID: 28535 RVA: 0x0017F89C File Offset: 0x0017DA9C
			[SecurityCritical]
			internal ServerIdentity StartupWellKnownObject(string asmName, string svrTypeName, string URI, WellKnownObjectMode mode, bool fReplace)
			{
				object obj = RemotingConfigHandler.RemotingConfigInfo.s_wkoStartLock;
				ServerIdentity result;
				lock (obj)
				{
					ServerIdentity serverIdentity = null;
					Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(svrTypeName, asmName);
					if (!type.IsMarshalByRef)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_WellKnown_MustBeMBR", new object[]
						{
							svrTypeName
						}));
					}
					serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
					if (serverIdentity != null && serverIdentity.IsRemoteDisconnected())
					{
						IdentityHolder.RemoveIdentity(URI);
						serverIdentity = null;
					}
					if (serverIdentity == null)
					{
						RemotingConfigHandler.RemotingConfigInfo.s_fullTrust.Assert();
						try
						{
							MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(type, true);
							if (RemotingServices.IsClientProxy(marshalByRefObject))
							{
								RemotingServices.MarshalInternal(new RedirectionProxy(marshalByRefObject, type)
								{
									ObjectMode = mode
								}, URI, type, true, true);
								serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
								serverIdentity.SetSingletonObjectMode();
							}
							else if (type.IsCOMObject && mode == WellKnownObjectMode.Singleton)
							{
								ComRedirectionProxy obj2 = new ComRedirectionProxy(marshalByRefObject, type);
								RemotingServices.MarshalInternal(obj2, URI, type, true, true);
								serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
								serverIdentity.SetSingletonObjectMode();
							}
							else
							{
								string objectUri = RemotingServices.GetObjectUri(marshalByRefObject);
								if (objectUri != null)
								{
									throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), URI));
								}
								RemotingServices.MarshalInternal(marshalByRefObject, URI, type, true, true);
								serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
								if (mode == WellKnownObjectMode.SingleCall)
								{
									serverIdentity.SetSingleCallObjectMode();
								}
								else
								{
									serverIdentity.SetSingletonObjectMode();
								}
							}
						}
						catch
						{
							throw;
						}
						finally
						{
							if (serverIdentity != null)
							{
								serverIdentity.IsInitializing = false;
							}
							CodeAccessPermission.RevertAssert();
						}
					}
					result = serverIdentity;
				}
				return result;
			}

			// Token: 0x06006F78 RID: 28536 RVA: 0x0017FA64 File Offset: 0x0017DC64
			[SecurityCritical]
			internal static Type LoadType(string typeName, string assemblyName)
			{
				Assembly assembly = null;
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				try
				{
					assembly = Assembly.Load(assemblyName);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (assembly == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", new object[]
					{
						assemblyName
					}));
				}
				Type type = assembly.GetType(typeName, false, false);
				if (type == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_BadType", new object[]
					{
						typeName + ", " + assemblyName
					}));
				}
				return type;
			}

			// Token: 0x040036EE RID: 14062
			private Hashtable _exportableClasses;

			// Token: 0x040036EF RID: 14063
			private Hashtable _remoteTypeInfo;

			// Token: 0x040036F0 RID: 14064
			private Hashtable _remoteAppInfo;

			// Token: 0x040036F1 RID: 14065
			private Hashtable _wellKnownExportInfo;

			// Token: 0x040036F2 RID: 14066
			private static char[] SepSpace = new char[]
			{
				' '
			};

			// Token: 0x040036F3 RID: 14067
			private static char[] SepPound = new char[]
			{
				'#'
			};

			// Token: 0x040036F4 RID: 14068
			private static char[] SepSemiColon = new char[]
			{
				';'
			};

			// Token: 0x040036F5 RID: 14069
			private static char[] SepEquals = new char[]
			{
				'='
			};

			// Token: 0x040036F6 RID: 14070
			private static object s_wkoStartLock = new object();

			// Token: 0x040036F7 RID: 14071
			private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
		}
	}
}
