using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x0200009D RID: 157
	internal class OperatorDiagnosticsFactory : IDiagnosable
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		private OperatorDiagnosticsFactory()
		{
			this.diagnosticCommands = new Dictionary<string, OperatorDiagnosticsFactory.DiagnosticHandler>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"Get-Breadcrumbs",
					new OperatorDiagnosticsFactory.DiagnosticHandler(this.GetBreadcrumbs)
				},
				{
					"Get-Status",
					new OperatorDiagnosticsFactory.DiagnosticHandler(this.GetStatus)
				}
			};
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000E90E File Offset: 0x0000CB0E
		public static OperatorDiagnosticsFactory Instance
		{
			[DebuggerStepThrough]
			get
			{
				return OperatorDiagnosticsFactory.instance;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000E918 File Offset: 0x0000CB18
		public static DiagnosticsLogConfig.LogDefaults LanguageDetectionLogDefaults
		{
			get
			{
				if (OperatorDiagnosticsFactory.languageDetectionLogDefaults == null)
				{
					string path;
					try
					{
						path = ExchangeSetupContext.InstallPath;
					}
					catch (SetupVersionInformationCorruptException)
					{
						path = string.Empty;
					}
					OperatorDiagnosticsFactory.languageDetectionLogDefaults = new DiagnosticsLogConfig.LogDefaults(Guid.Parse("9f2dd9a4-0b30-4240-8321-f1028f9b583f"), ComponentInstance.Globals.Search.ServiceName, "Search Language Detection Logs", Path.Combine(path, "Logging\\Search\\LanguageDetection"), "LanguageDetection_", "SearchLogs");
				}
				return OperatorDiagnosticsFactory.languageDetectionLogDefaults;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000E98C File Offset: 0x0000CB8C
		private static DiagnosticsLogConfig.LogDefaults FailedItemsLogDefaults
		{
			get
			{
				if (OperatorDiagnosticsFactory.failedItemsLogDefaults == null)
				{
					string path;
					try
					{
						path = ExchangeSetupContext.InstallPath;
					}
					catch (SetupVersionInformationCorruptException)
					{
						path = string.Empty;
					}
					OperatorDiagnosticsFactory.failedItemsLogDefaults = new DiagnosticsLogConfig.LogDefaults(Guid.Parse("19615f4c-11b4-4e4c-97e9-8ceb5f70e860"), ComponentInstance.Globals.Search.ServiceName, "Search Failed Items Logs", Path.Combine(path, "Logging\\Search"), "SearchFailedItems_", "SearchLogs");
				}
				return OperatorDiagnosticsFactory.failedItemsLogDefaults;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000EA00 File Offset: 0x0000CC00
		public static void EnableGetExchangeDiagnosticsInfo()
		{
			if (!OperatorDiagnosticsFactory.diagnosticsEndpointEnabled)
			{
				Globals.InitializeMultiPerfCounterInstance("noderunner");
				OperatorDiagnosticsFactory.diagnosticsEndpointEnabled = true;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000EA19 File Offset: 0x0000CC19
		public string GetDiagnosticComponentName()
		{
			return base.GetType().Name;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000EA28 File Offset: 0x0000CC28
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			string text = parameters.Argument;
			ExTraceGlobals.OperatorDiagnosticsTracer.TraceDebug<string>((long)this.GetHashCode(), "GetDiagnosticsInfo command: {0}", text);
			if (string.IsNullOrEmpty(text))
			{
				text = "Get-Status";
			}
			string[] array = text.Split(null, 2, StringSplitOptions.RemoveEmptyEntries);
			string key = (array.Length >= 1) ? array[0] : null;
			string remainingArgs = (array.Length >= 2) ? array[1] : null;
			OperatorDiagnosticsFactory.DiagnosticHandler diagnosticHandler;
			if (!this.diagnosticCommands.TryGetValue(key, out diagnosticHandler))
			{
				return this.BuildDiagnosticsErrorNode("Unknown command");
			}
			XElement result;
			try
			{
				result = diagnosticHandler(parameters, remainingArgs);
			}
			catch (Exception ex)
			{
				if (Util.ShouldRethrowException(ex))
				{
					throw;
				}
				ExTraceGlobals.OperatorDiagnosticsTracer.TraceError<Exception>((long)this.GetHashCode(), "Caught exception executing Diagnostics command: {0}", ex);
				result = this.BuildDiagnosticsErrorNode(ex.Message);
			}
			return result;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public OperatorDiagnostics GetDiagnosticsContext(string flowId)
		{
			OperatorDiagnostics result;
			lock (this.contexts)
			{
				if (OperatorDiagnosticsFactory.diagnosticsEndpointEnabled && this.contexts.Count == 0)
				{
					this.RegisterDiagnosticsEndpoint();
				}
				OperatorDiagnosticsFactory.ContextAndRefCount contextAndRefCount;
				if (!this.contexts.TryGetValue(flowId, out contextAndRefCount))
				{
					contextAndRefCount = new OperatorDiagnosticsFactory.ContextAndRefCount(flowId, OperatorDiagnosticsFactory.FailedItemsLogDefaults);
					this.contexts.Add(flowId, contextAndRefCount);
				}
				contextAndRefCount.RefCount++;
				result = contextAndRefCount;
			}
			return result;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public void ReleaseDiagnosticsContext(OperatorDiagnostics context)
		{
			lock (this.contexts)
			{
				OperatorDiagnosticsFactory.ContextAndRefCount contextAndRefCount;
				if (!this.contexts.TryGetValue(context.FlowIdentifier, out contextAndRefCount))
				{
					throw new InvalidOperationException("context not found");
				}
				if (--contextAndRefCount.RefCount == 0)
				{
					this.contexts.Remove(context.FlowIdentifier);
				}
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000EC08 File Offset: 0x0000CE08
		private static void OnDomainUnload(object source, EventArgs args)
		{
			ProcessAccessManager.UnregisterComponent(OperatorDiagnosticsFactory.Instance);
			ProcessAccessManager.UnregisterComponent(SettingOverrideSync.Instance);
			SettingOverrideSync.Instance.Stop();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000EC28 File Offset: 0x0000CE28
		private void RegisterDiagnosticsEndpoint()
		{
			ProcessAccessManager.RegisterComponent(OperatorDiagnosticsFactory.Instance);
			AppDomain.CurrentDomain.DomainUnload += OperatorDiagnosticsFactory.OnDomainUnload;
			SettingOverrideSync.Instance.Start(true);
			ProcessAccessManager.RegisterComponent(SettingOverrideSync.Instance);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		private XElement GetStatus(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			return this.GetStatusFromAllContexts(false);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		private XElement GetBreadcrumbs(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			return this.GetStatusFromAllContexts(true);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		private XElement GetStatusFromAllContexts(bool verboseBreadcrumbs)
		{
			XElement result;
			lock (this.contexts)
			{
				List<OperatorDiagnostics> list = new List<OperatorDiagnostics>(this.contexts.Count);
				foreach (OperatorDiagnosticsFactory.ContextAndRefCount item in this.contexts.Values)
				{
					list.Add(item);
				}
				list.Sort();
				XElement xelement = new XElement("FeedingGroups");
				XElement xelement2 = null;
				OperatorDiagnosticsFactory.ContextAndRefCount contextAndRefCount = null;
				foreach (OperatorDiagnostics operatorDiagnostics in list)
				{
					OperatorDiagnosticsFactory.ContextAndRefCount contextAndRefCount2 = (OperatorDiagnosticsFactory.ContextAndRefCount)operatorDiagnostics;
					if (contextAndRefCount == null || contextAndRefCount.InstanceName != contextAndRefCount2.InstanceName || contextAndRefCount.InstanceGuid != contextAndRefCount2.InstanceGuid)
					{
						XElement xelement3 = new XElement("FeedingGroup");
						xelement.Add(xelement3);
						xelement3.Add(new XElement("InstanceName", contextAndRefCount2.InstanceName ?? contextAndRefCount2.InstanceGuid.ToString()));
						xelement3.Add(new XElement("InstanceGuid", contextAndRefCount2.InstanceGuid));
						xelement2 = new XElement("Sessions");
						xelement3.Add(xelement2);
						contextAndRefCount = contextAndRefCount2;
					}
					xelement2.Add(contextAndRefCount2.GetBreadcrumbs(verboseBreadcrumbs));
				}
				result = xelement;
			}
			return result;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000EE88 File Offset: 0x0000D088
		private XElement BuildDiagnosticsErrorNode(string reason)
		{
			ExTraceGlobals.OperatorDiagnosticsTracer.TraceError<string>((long)this.GetHashCode(), "Error executing Diagnostics command: {0}", reason);
			return new XElement("Error", reason);
		}

		// Token: 0x0400021E RID: 542
		private static OperatorDiagnosticsFactory instance = new OperatorDiagnosticsFactory();

		// Token: 0x0400021F RID: 543
		private static bool diagnosticsEndpointEnabled;

		// Token: 0x04000220 RID: 544
		private static DiagnosticsLogConfig.LogDefaults failedItemsLogDefaults;

		// Token: 0x04000221 RID: 545
		private static DiagnosticsLogConfig.LogDefaults languageDetectionLogDefaults;

		// Token: 0x04000222 RID: 546
		private readonly Dictionary<string, OperatorDiagnosticsFactory.ContextAndRefCount> contexts = new Dictionary<string, OperatorDiagnosticsFactory.ContextAndRefCount>();

		// Token: 0x04000223 RID: 547
		private readonly Dictionary<string, OperatorDiagnosticsFactory.DiagnosticHandler> diagnosticCommands;

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x060004BB RID: 1211
		private delegate XElement DiagnosticHandler(DiagnosableParameters parameters, string remainingArgs);

		// Token: 0x0200009F RID: 159
		private class ContextAndRefCount : OperatorDiagnostics
		{
			// Token: 0x060004BE RID: 1214 RVA: 0x0000EEBD File Offset: 0x0000D0BD
			public ContextAndRefCount(string flowIdentifier, DiagnosticsLogConfig.LogDefaults logDefaults) : base(flowIdentifier, logDefaults)
			{
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
			// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000EECF File Offset: 0x0000D0CF
			public int RefCount { get; set; }
		}
	}
}
