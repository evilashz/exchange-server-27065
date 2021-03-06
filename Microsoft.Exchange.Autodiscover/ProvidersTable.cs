using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Schema;
using Microsoft.Exchange.Autodiscover.Providers;
using Microsoft.Exchange.Autodiscover.Providers.MobileSync;
using Microsoft.Exchange.Autodiscover.Providers.Outlook;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000005 RID: 5
	internal static class ProvidersTable
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000028C4 File Offset: 0x00000AC4
		static ProvidersTable()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in ProvidersTable.availableProviderTypes.Member.Keys)
			{
				if (ProvidersTable.TryAddProviderType(type, ProvidersTable.availableProviderTypes.Member[type]))
				{
					stringBuilder.Append(string.Format("[{0}]", type.AssemblyQualifiedName));
				}
				else
				{
					ExTraceGlobals.FrameworkTracer.TraceError<string>(0L, "[ProvidersTable] 'Could not load provider \"{0}\"", type.AssemblyQualifiedName);
				}
			}
			ProvidersTable.CompileRequestSchemaSet();
			if (ProvidersTable.providersList.Count == 0)
			{
				ExTraceGlobals.FrameworkTracer.TraceError(0L, "[ProvidersTable] No providers loaded.");
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrCoreNoProvidersFound, Common.PeriodicKey, new object[0]);
				return;
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<StringBuilder>(0L, "[ProvidersTable] {0} providers found", stringBuilder);
			Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_InfoCoreProvidersLoaded, Common.PeriodicKey, new object[]
			{
				stringBuilder.ToString()
			});
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A31 File Offset: 0x00000C31
		private static void CompileRequestSchemaSet()
		{
			Common.SendWatsonReportOnUnhandledException(delegate
			{
				if (ProvidersTable.RequestSchemaSet.Count > 0)
				{
					ProvidersTable.RequestSchemaSet.Compile();
				}
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A58 File Offset: 0x00000C58
		private static bool TryAddProviderType(Type type, CreateProviderDelegate createProvider)
		{
			try
			{
				foreach (object obj in type.GetCustomAttributes(typeof(ProviderAttribute), false))
				{
					ProviderAttribute attributes = obj as ProviderAttribute;
					ProviderInfo providerInfo = new ProviderInfo(type, attributes, createProvider);
					ProvidersTable.providersList.Add(providerInfo);
					XmlSchema xmlSchema = ProvidersTable.LoadSchemaFromResource(type.Assembly, providerInfo.Attributes.RequestSchemaFile);
					if (!ProvidersTable.RequestSchemaSet.Contains(xmlSchema.TargetNamespace))
					{
						ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>(0L, "[ProvidersTable::TryAddProviderType] 'adding request schema to ProvidersTable' TargetNamespace=\"{0}\";SourceUri=\"{1}\"", xmlSchema.TargetNamespace, xmlSchema.SourceUri);
						ProvidersTable.RequestSchemaSet.Add(xmlSchema);
						return true;
					}
				}
			}
			catch (FormatException ex)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, string>(0L, "[ProvidersTable::TryAddProviderType] Message=\"{0}\";StackTrace=\"{1}\"", ex.Message, ex.StackTrace);
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreProviderAttributeException, Common.PeriodicKey, new object[]
				{
					type.FullName,
					ex.Message
				});
			}
			catch (ArgumentNullException ex2)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, string>(0L, "[ProvidersTable::TryAddProviderType] Message=\"{0}\";StackTrace=\"{1}\"", ex2.Message, ex2.StackTrace);
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreProviderAttributeException, Common.PeriodicKey, new object[]
				{
					type.FullName,
					ex2.Message
				});
			}
			return false;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public static XmlSchemaSet RequestSchemaSet
		{
			get
			{
				return ProvidersTable.requestSchemaSet;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public static Provider LoadProvider(RequestData requestData)
		{
			Provider result = null;
			ProviderInfo providerInfo = ProvidersTable.FindProvider(requestData);
			if (providerInfo != null)
			{
				result = providerInfo.CreateProvider(requestData);
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C10 File Offset: 0x00000E10
		private static ProviderInfo FindProvider(RequestData requestData)
		{
			foreach (ProviderInfo providerInfo in ProvidersTable.providersList)
			{
				if (providerInfo.Match(requestData))
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "[ProvidersTable::FindProvider] 'provider \"{0}\" is found'", providerInfo.SystemType.AssemblyQualifiedName);
					return providerInfo;
				}
			}
			string text = (requestData.RequestSchemas.Count > 0) ? requestData.RequestSchemas[0] : string.Empty;
			string text2 = (requestData.ResponseSchemas.Count > 0) ? requestData.ResponseSchemas[0] : string.Empty;
			ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>(0L, "[ProvidersTable::FindProvider] 'no provider is found for the \"{0}\" request schema and the \"{1}\" response schema'", text, text2);
			Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreProviderNotFound, Common.PeriodicKey, new object[]
			{
				text,
				text2
			});
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D0C File Offset: 0x00000F0C
		private static XmlSchema LoadSchemaFromResource(Assembly assembly, string schemaFileName)
		{
			if (!string.IsNullOrEmpty(schemaFileName))
			{
				AssemblyName name = assembly.GetName(false);
				string name2 = name.Name + "." + schemaFileName;
				using (Stream manifestResourceStream = assembly.GetManifestResourceStream(name2))
				{
					if (manifestResourceStream != null)
					{
						return SafeXmlSchema.Read(manifestResourceStream, null);
					}
				}
			}
			return null;
		}

		// Token: 0x04000014 RID: 20
		private static List<ProviderInfo> providersList = new List<ProviderInfo>();

		// Token: 0x04000015 RID: 21
		private static XmlSchemaSet requestSchemaSet = new XmlSchemaSet();

		// Token: 0x04000016 RID: 22
		private static LazyMember<Dictionary<Type, CreateProviderDelegate>> availableProviderTypes = new LazyMember<Dictionary<Type, CreateProviderDelegate>>(delegate()
		{
			Dictionary<Type, CreateProviderDelegate> dictionary = new Dictionary<Type, CreateProviderDelegate>();
			dictionary.Add(typeof(OutlookAutoDiscoverProvider), (RequestData reqData) => new OutlookAutoDiscoverProvider(reqData));
			dictionary.Add(typeof(MobileSyncProvider), (RequestData reqData) => new MobileSyncProvider(reqData));
			return dictionary;
		});
	}
}
