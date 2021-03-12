using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory.ABProviderFramework;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ABFactory
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002AF0 File Offset: 0x00000CF0
		internal static ABSession CreateABSession(IABSessionSettings addressBookSessionSettings)
		{
			if (addressBookSessionSettings == null)
			{
				throw new ArgumentNullException("addressBookSessionSettings");
			}
			string providerName = ABFactory.GetProviderName(addressBookSessionSettings);
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "Creating session for provider '{0}'.", providerName);
			ABFactory.ThrowIfDisabled(addressBookSessionSettings);
			IABProviderFactory providerFactory = ABFactory.GetProviderFactory(providerName);
			return providerFactory.Create(addressBookSessionSettings);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B38 File Offset: 0x00000D38
		internal static ABProviderCapabilities GetProviderCapabilities(IABSessionSettings addressBookSessionSettings)
		{
			if (addressBookSessionSettings == null)
			{
				throw new ArgumentNullException("addressBookSessionSettings");
			}
			string providerName = ABFactory.GetProviderName(addressBookSessionSettings);
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "Getting provider capabilities for provider '{0}'.", providerName);
			IABProviderFactory providerFactory = ABFactory.GetProviderFactory(providerName);
			return providerFactory.GetProviderCapabilities(addressBookSessionSettings);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B7C File Offset: 0x00000D7C
		private static void ThrowIfDisabled(IABSessionSettings addressBookSessionSettings)
		{
			bool flag;
			if (addressBookSessionSettings.TryGet<bool>("Disabled", out flag) && flag)
			{
				Guid guid = addressBookSessionSettings.Get<Guid>("SubscriptionGuid");
				throw new ABSubscriptionDisabledException(DirectoryStrings.SessionSubscriptionDisabled(guid));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private static string GetProviderName(IABSessionSettings addressBookSessionSettings)
		{
			string text = addressBookSessionSettings.Get<string>("Provider");
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("addressBookSessionSettings[Provider]");
			}
			return text;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BE4 File Offset: 0x00000DE4
		private static IABProviderFactory GetProviderFactory(string providerName)
		{
			if (string.IsNullOrEmpty(providerName))
			{
				throw new ArgumentNullException("providerName");
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "Getting provider factor for provider '{0}'.", providerName);
			IABProviderFactory iabproviderFactory;
			if (!ABFactory.addressBookProviders.TryGetValue(providerName, out iabproviderFactory))
			{
				lock (ABFactory.loadABProviderSync)
				{
					if (!ABFactory.addressBookProviders.TryGetValue(providerName, out iabproviderFactory))
					{
						string assemblyFullPath = ABFactory.GetAssemblyFullPath(Assembly.GetExecutingAssembly().Location, providerName);
						ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>(0L, "First time to create a session for AB Provider '{0}', loading it from '{1}'.", providerName, assemblyFullPath);
						iabproviderFactory = ABFactory.LoadProviderFactory(providerName, assemblyFullPath);
						Dictionary<string, IABProviderFactory> dictionary = new Dictionary<string, IABProviderFactory>(ABFactory.addressBookProviders, StringComparer.OrdinalIgnoreCase);
						dictionary[providerName] = iabproviderFactory;
						ABFactory.addressBookProviders = dictionary;
					}
				}
			}
			return iabproviderFactory;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CB0 File Offset: 0x00000EB0
		private static IABProviderFactory LoadProviderFactory(string providerName, string assemblyPath)
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFrom(assemblyPath);
			}
			catch (FileNotFoundException ex)
			{
				ExTraceGlobals.FrameworkTracer.TraceError(0L, "Failed to load ABProvider: FileNotFound exception.");
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UnableToLoadABProvider, assemblyPath, new object[]
				{
					providerName,
					assemblyPath,
					ex
				});
				throw new ABProviderLoadException(DirectoryStrings.ProviderFileNotFoundLoadException(providerName, assemblyPath), ex);
			}
			catch (FileLoadException ex2)
			{
				ExTraceGlobals.FrameworkTracer.TraceError(0L, "Failed to load ABProvider: FileLoadException exception.");
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UnableToLoadABProvider, assemblyPath, new object[]
				{
					providerName,
					assemblyPath,
					ex2
				});
				throw new ABProviderLoadException(DirectoryStrings.ProviderFileLoadException(providerName, assemblyPath), ex2);
			}
			catch (BadImageFormatException ex3)
			{
				ExTraceGlobals.FrameworkTracer.TraceError(0L, "Failed to load ABProvider: BadImageFormatException exception.");
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UnableToLoadABProvider, assemblyPath, new object[]
				{
					providerName,
					assemblyPath,
					ex3
				});
				throw new ABProviderLoadException(DirectoryStrings.ProviderBadImpageFormatLoadException(providerName, assemblyPath), ex3);
			}
			string abfactoryClassNameFromProviderName = ABFactory.GetABFactoryClassNameFromProviderName(providerName);
			IABProviderFactory iabproviderFactory = (IABProviderFactory)assembly.CreateInstance(abfactoryClassNameFromProviderName, true);
			if (iabproviderFactory == null)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Failed to load ABProvider: Factory class '{0}' not found.", new object[]
				{
					abfactoryClassNameFromProviderName
				});
				ExTraceGlobals.FrameworkTracer.TraceError(0L, text);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UnableToLoadABProvider, assemblyPath, new object[]
				{
					providerName,
					assemblyPath,
					text
				});
				throw new ABProviderLoadException(DirectoryStrings.ProviderFactoryClassNotFoundLoadException(providerName, assemblyPath, abfactoryClassNameFromProviderName));
			}
			return iabproviderFactory;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E44 File Offset: 0x00001044
		private static string GetAssemblyFullPath(string executingAssemblyPath, string providerName)
		{
			return Path.Combine(Path.GetDirectoryName(executingAssemblyPath), ABFactory.GetAssemblyNameFromProviderName(providerName));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E58 File Offset: 0x00001058
		private static string GetABFactoryClassNameFromProviderName(string providerName)
		{
			return string.Concat(new string[]
			{
				"Microsoft.Exchange.ABProviders.",
				providerName,
				".",
				providerName,
				"ABProviderFactory"
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E92 File Offset: 0x00001092
		private static string GetAssemblyNameFromProviderName(string providerName)
		{
			return "Microsoft.Exchange.ABProviders." + providerName + ".dll";
		}

		// Token: 0x0400001E RID: 30
		private const string NamespacePrefix = "Microsoft.Exchange.ABProviders.";

		// Token: 0x0400001F RID: 31
		private const string AssemblyNamePrefix = "Microsoft.Exchange.ABProviders.";

		// Token: 0x04000020 RID: 32
		private const string FactorySuffix = "ABProviderFactory";

		// Token: 0x04000021 RID: 33
		private static Dictionary<string, IABProviderFactory> addressBookProviders = new Dictionary<string, IABProviderFactory>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000022 RID: 34
		private static object loadABProviderSync = new object();
	}
}
