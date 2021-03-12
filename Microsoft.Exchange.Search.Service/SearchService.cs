using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Core.RpcEndpoint;
using Microsoft.Exchange.Search.Engine;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.Query;

namespace Microsoft.Exchange.Search.Service
{
	// Token: 0x02000002 RID: 2
	internal sealed class SearchService : ExServiceBase, IDiagnosable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static SearchService()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				SearchService.CurrentProcessId = currentProcess.Id;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002144 File Offset: 0x00000344
		internal SearchService()
		{
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			this.diagnosticCommands = new Dictionary<string, SearchService.DiagnosticHandler>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"Flush-IndexSystem",
					new SearchService.DiagnosticHandler(this.FlushIndexSystem)
				},
				{
					"Get-AllNodesHealthReport",
					new SearchService.DiagnosticHandler(this.GetAllNodesHealthReport)
				},
				{
					"Get-AllNodesInfo",
					new SearchService.DiagnosticHandler(this.GetAllNodesInfo)
				},
				{
					"Get-Flow",
					new SearchService.DiagnosticHandler(this.GetFlow)
				},
				{
					"Get-Flows",
					new SearchService.DiagnosticHandler(this.GetFlow)
				},
				{
					"Get-Command",
					new SearchService.DiagnosticHandler(this.GetCommand)
				},
				{
					"Get-IndexSystems",
					new SearchService.DiagnosticHandler(this.GetIndexSystems)
				},
				{
					"Get-Status",
					new SearchService.DiagnosticHandler(this.GetStatus)
				},
				{
					"Merge-IndexSystem",
					new SearchService.DiagnosticHandler(this.MergeIndexSystem)
				},
				{
					"Recrawl-Mailbox",
					new SearchService.DiagnosticHandler(this.RecrawlMailbox)
				},
				{
					"Get-Dictionary",
					new SearchService.DiagnosticHandler(this.GetDictionary)
				},
				{
					"Reset-Dictionary",
					new SearchService.DiagnosticHandler(this.ResetDictionary)
				},
				{
					"Get-RootControllerBreadCrumbs",
					new SearchService.DiagnosticHandler(this.GetRootControllerBreadCrumbs)
				}
			};
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000022D3 File Offset: 0x000004D3
		public static int CurrentProcessId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000022DB File Offset: 0x000004DB
		private static bool IsRunningAsService
		{
			get
			{
				return !Environment.UserInteractive;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022E5 File Offset: 0x000004E5
		public static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += SearchService.HandleUnhandledException;
			ExWatson.Register();
			if (SearchService.IsRunningAsService)
			{
				SearchService.RunService();
				return;
			}
			SearchService.RunConsole();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002314 File Offset: 0x00000514
		public string GetDiagnosticComponentName()
		{
			return "SearchService";
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000231C File Offset: 0x0000051C
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			string text = parameters.Argument;
			if (string.IsNullOrEmpty(text))
			{
				text = "Get-Status";
			}
			SearchService.diagnosticsSession.TraceDebug<string>("GetDiagnosticsInfo command: {0}", text);
			string[] array = text.Split(null, 2, StringSplitOptions.RemoveEmptyEntries);
			string key = (array.Length >= 1) ? array[0] : null;
			string remainingArgs = (array.Length >= 2) ? array[1] : null;
			SearchService.DiagnosticHandler diagnosticHandler;
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
				SearchService.diagnosticsSession.TraceError<Exception>("Caught exception executing Diagnostics command: {0}", ex);
				result = this.BuildDiagnosticsErrorNode(ex.Message);
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023E0 File Offset: 0x000005E0
		protected override void OnStartInternal(string[] args)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.DeferredStartup));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023F4 File Offset: 0x000005F4
		protected override void OnStopInternal()
		{
			DateTime utcNow = DateTime.UtcNow;
			SearchService.diagnosticsSession.TraceDebug("OnStopInternal: Stopping service", new object[0]);
			SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceStopping, new object[]
			{
				SearchService.CurrentProcessId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
			lock (this.lockObject)
			{
				this.stopEvent.Set();
				if (this.rootController != null)
				{
					this.rootController.CancelExecute();
					SearchService.diagnosticsSession.TraceDebug("OnStopInternal: Cancelling the RootController completed.", new object[0]);
					ProcessAccessManager.UnregisterComponent(this);
					ProcessAccessManager.UnregisterComponent(SettingOverrideSync.Instance);
					SettingOverrideSync.Instance.Stop();
					SearchService.diagnosticsSession.TraceDebug("OnStopInternal: Unregistering Diagnostics completed.", new object[0]);
				}
				SearchService.diagnosticsSession.TraceDebug("OnStopInternal: Begin waiting for the done stopping event.", new object[0]);
				this.doneStoppingEvent.WaitOne();
				SearchServiceRpcServer.StopServer();
				this.stopEvent.Dispose();
				this.doneStoppingEvent.Dispose();
			}
			SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceStopSuccess, new object[0]);
			SearchService.diagnosticsSession.TraceDebug<TimeSpan>("Successfully stopped the service in {0}.", DateTime.UtcNow.Subtract(utcNow));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002550 File Offset: 0x00000750
		private static void RunService()
		{
			using (SearchService searchService = new SearchService())
			{
				ServiceBase.Run(searchService);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002588 File Offset: 0x00000788
		private static void RunConsole()
		{
			Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
			if (SearchService.waitToContinue)
			{
				Console.WriteLine("Press <ENTER> to continue startup.");
				Console.ReadLine();
			}
			using (SearchService searchService = new SearchService())
			{
				Console.WriteLine("Running. Check the event log for failures...");
				searchService.OnStartInternal(null);
				Console.WriteLine("Press <ENTER> to shutdown.");
				Console.WriteLine("> ");
				string value;
				do
				{
					value = Console.ReadLine();
				}
				while (!string.IsNullOrEmpty(value));
				Console.WriteLine("Shutting down ...");
				searchService.OnStopInternal();
				Console.WriteLine("Done.");
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002634 File Offset: 0x00000834
		private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = (Exception)e.ExceptionObject;
			SearchService.diagnosticsSession.TraceError<string>("HandleUnhandledException: An unhandled exception was thrown\n{0}", Util.StringizeException(ex));
			SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceUnexpectedException, new object[]
			{
				ex
			});
			SearchService.diagnosticsSession.SendWatsonReport(ex);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002688 File Offset: 0x00000888
		private void ExecuteComplete(IAsyncResult asyncResult)
		{
			try
			{
				try
				{
					this.rootController.EndExecute(asyncResult);
				}
				finally
				{
					this.rootController.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (Util.ShouldRethrowException(ex))
				{
					throw;
				}
				SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceUnexpectedException, new object[]
				{
					ex
				});
			}
			RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(this.stopEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.CompleteDelayedRestartCallback)), null, this.config.MaxOperationTimeout, true);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002720 File Offset: 0x00000920
		private void DeferredStartup(object state)
		{
			SearchService.diagnosticsSession.TraceDebug("OnStartInternal: Starting service", new object[0]);
			SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceStarting, new object[]
			{
				SearchService.CurrentProcessId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
			Globals.InitializeSinglePerfCounterInstance();
			SettingOverrideSync.Instance.Start(true);
			ProcessAccessManager.RegisterComponent(SettingOverrideSync.Instance);
			lock (this.lockObject)
			{
				if (!this.stopEvent.WaitOne(0))
				{
					try
					{
						SearchServiceRpcServer.StartServer();
					}
					catch (ComponentException ex)
					{
						SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceUnexpectedException, new object[]
						{
							ex
						});
						base.Stop();
						return;
					}
					this.config = Factory.Current.CreateSearchServiceConfig();
					ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
					ProcessAccessManager.RegisterComponent(this);
					this.StartRootController();
				}
				SearchService.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_SearchServiceStartSuccess, new object[0]);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002848 File Offset: 0x00000A48
		private void StartRootController()
		{
			SearchService.diagnosticsSession.TraceDebug("Create/Start the RootController.", new object[0]);
			this.rootController = new SearchRootController(this.config, SearchServiceRpcServer.Server);
			this.doneStoppingEvent.Reset();
			this.rootController.BeginExecute(new AsyncCallback(this.ExecuteComplete), null);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028A5 File Offset: 0x00000AA5
		private void CompleteDelayedRestartCallback(object state, bool timerFired)
		{
			if (timerFired)
			{
				this.StartRootController();
				return;
			}
			this.doneStoppingEvent.Set();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028BD File Offset: 0x00000ABD
		private XElement GetAllNodesInfo(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			return NodeManagementClient.Instance.GetAllNodesInfoDiagnostics();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028DD File Offset: 0x00000ADD
		private XElement GetAllNodesHealthReport(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			return NodeManagementClient.Instance.GetAllNodesHealthReport();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028FD File Offset: 0x00000AFD
		private XElement GetFlow(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			return FlowManager.Instance.GetFlowDiagnostics();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000291D File Offset: 0x00000B1D
		private XElement GetIndexSystems(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			return IndexManager.Instance.GetIndexSystemsDiagnostics();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000293D File Offset: 0x00000B3D
		private XElement GetStatus(DiagnosableParameters parameters, string remainingArgs)
		{
			if (this.rootController == null)
			{
				return this.BuildDiagnosticsErrorNode("RootController: null");
			}
			return this.rootController.GetDiagnosticInfo(parameters, remainingArgs);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002960 File Offset: 0x00000B60
		private XElement FlushIndexSystem(DiagnosableParameters parameters, string remainingArgs)
		{
			if (this.rootController == null)
			{
				return this.BuildDiagnosticsErrorNode("RootController: null");
			}
			MdbInfo mdbInfo = this.rootController.FindMdbInfo(remainingArgs);
			string indexName = (mdbInfo != null) ? mdbInfo.IndexSystemName : remainingArgs;
			IndexManager.Instance.FlushCatalog(indexName);
			return new XElement("FlushIndexSystem");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029B8 File Offset: 0x00000BB8
		private XElement MergeIndexSystem(DiagnosableParameters parameters, string remainingArgs)
		{
			if (this.rootController == null)
			{
				return this.BuildDiagnosticsErrorNode("RootController: null");
			}
			MdbInfo mdbInfo = this.rootController.FindMdbInfo(remainingArgs);
			string indexName = (mdbInfo != null) ? mdbInfo.IndexSystemName : remainingArgs;
			IndexManager.Instance.TriggerMasterMerge(indexName);
			return new XElement("MergeIndexSystem");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A10 File Offset: 0x00000C10
		private XElement GetCommand(DiagnosableParameters parameters, string remainingArgs)
		{
			if (!string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Invalid arguments");
			}
			XElement xelement = new XElement("Commands");
			foreach (string content in this.diagnosticCommands.Keys)
			{
				xelement.Add(new XElement("Command", content));
			}
			return xelement;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A9C File Offset: 0x00000C9C
		private XElement RecrawlMailbox(DiagnosableParameters parameters, string remainingArgs)
		{
			if (string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode("Missing arguments");
			}
			this.rootController.RecrawlMailbox(parameters, remainingArgs);
			return new XElement("RecrawlMailbox");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AD0 File Offset: 0x00000CD0
		private XElement GetDictionary(DiagnosableParameters parameters, string remainingArgs)
		{
			string text = "Get-Dictionary requires specifying the DictionaryType (TopN | History), the DatabaseGuid and the MailboxGuid.";
			if (string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode(text);
			}
			if (this.rootController == null)
			{
				return this.BuildDiagnosticsErrorNode("RootController is not ready. Please try again.");
			}
			SearchService.DictionaryParameters dictionaryParameters;
			FlightingSearchConfig flightingSearchConfig;
			try
			{
				dictionaryParameters = this.ParseDictionaryParamenters(remainingArgs);
				flightingSearchConfig = new FlightingSearchConfig(dictionaryParameters.DatabaseInfo.Guid);
			}
			catch (ArgumentException arg)
			{
				return this.BuildDiagnosticsErrorNode(string.Format("{0} : Failure: {1}", text, arg));
			}
			catch (NullReferenceException)
			{
				return this.BuildDiagnosticsErrorNode("RootController is not ready. Please try again.");
			}
			return SearchDictionary.DiagnosticsDictionaryRetrieval(parameters, dictionaryParameters.DictionaryName, flightingSearchConfig, dictionaryParameters.DatabaseInfo, dictionaryParameters.MailboxGuid);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B84 File Offset: 0x00000D84
		private XElement ResetDictionary(DiagnosableParameters parameters, string remainingArgs)
		{
			string text = "Reset-Dictionary requires specifying the DictionaryType (TopN | History), the DatabaseGuid and the MailboxGuid.";
			if (string.IsNullOrEmpty(remainingArgs))
			{
				return this.BuildDiagnosticsErrorNode(text);
			}
			if (this.rootController == null)
			{
				return this.BuildDiagnosticsErrorNode("RootController is not ready. Please try again.");
			}
			SearchService.DictionaryParameters dictionaryParameters;
			FlightingSearchConfig flightingSearchConfig;
			try
			{
				dictionaryParameters = this.ParseDictionaryParamenters(remainingArgs);
				flightingSearchConfig = new FlightingSearchConfig(dictionaryParameters.DatabaseInfo.Guid);
			}
			catch (ArgumentException arg)
			{
				return this.BuildDiagnosticsErrorNode(string.Format("{0} : Failure: {1}", text, arg));
			}
			catch (NullReferenceException)
			{
				return this.BuildDiagnosticsErrorNode("RootController is not ready. Please try again.");
			}
			return SearchDictionary.DiagnosticsDictionaryReset(parameters, dictionaryParameters.DictionaryName, flightingSearchConfig, dictionaryParameters.DatabaseInfo, dictionaryParameters.MailboxGuid);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C38 File Offset: 0x00000E38
		private XElement GetRootControllerBreadCrumbs(DiagnosableParameters parameters, string remainingArgs)
		{
			if (this.rootController == null)
			{
				return this.BuildDiagnosticsErrorNode("RootController: null");
			}
			return this.rootController.GetBreadCrumbs();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C59 File Offset: 0x00000E59
		private XElement BuildDiagnosticsErrorNode(string reason)
		{
			SearchService.diagnosticsSession.TraceError<string>("Error executing Diagnostics command: {0}", reason);
			return new XElement("Error", reason);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C7C File Offset: 0x00000E7C
		private SearchService.DictionaryParameters ParseDictionaryParamenters(string remainingArgs)
		{
			int num = 6;
			string[] array = remainingArgs.Split(new char[]
			{
				' '
			});
			if (array.Length < num)
			{
				throw new ArgumentException("Wrong number of arguments.");
			}
			SearchService.DictionaryParameters dictionaryParameters = new SearchService.DictionaryParameters();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int i = 0;
			while (i < num - 1)
			{
				string text = array[i];
				string a;
				if ((a = text.ToLowerInvariant()) != null)
				{
					if (a == "-dictionarytype")
					{
						string text2 = array[i + 1].ToLowerInvariant();
						string a2;
						if ((a2 = text2) != null)
						{
							if (!(a2 == "topn"))
							{
								if (!(a2 == "history"))
								{
									goto IL_D3;
								}
								dictionaryParameters.DictionaryName = "Search.QueryHistoryInput";
							}
							else
							{
								dictionaryParameters.DictionaryName = "Search.TopN";
							}
							flag = true;
							goto IL_12A;
						}
						IL_D3:
						throw new ArgumentException("Currently supported DictionaryType values: TopN | History");
					}
					if (!(a == "-databaseguid"))
					{
						if (!(a == "-mailboxguid"))
						{
							goto IL_118;
						}
						dictionaryParameters.MailboxGuid = Guid.Parse(array[i + 1]);
						flag3 = true;
					}
					else
					{
						string database = array[i + 1];
						dictionaryParameters.DatabaseInfo = this.rootController.FindMdbInfo(database);
						flag2 = true;
					}
					IL_12A:
					i += 2;
					continue;
				}
				IL_118:
				throw new ArgumentException("Unexpected Argument: " + text);
			}
			if (!flag || !flag2 || !flag3)
			{
				throw new ArgumentException("Missing Arguments.");
			}
			return dictionaryParameters;
		}

		// Token: 0x04000001 RID: 1
		private const string RootControllerNotReadyError = "RootController is not ready. Please try again.";

		// Token: 0x04000002 RID: 2
		private static readonly bool waitToContinue = true;

		// Token: 0x04000003 RID: 3
		private static readonly IDiagnosticsSession diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession(ComponentInstance.Globals.Search.ServiceName, ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.ServiceTracer, (long)ComponentInstance.Globals.Search.ServiceName.GetHashCode());

		// Token: 0x04000004 RID: 4
		private readonly Dictionary<string, SearchService.DiagnosticHandler> diagnosticCommands;

		// Token: 0x04000005 RID: 5
		private SearchRootController rootController;

		// Token: 0x04000006 RID: 6
		private ISearchServiceConfig config;

		// Token: 0x04000007 RID: 7
		private ManualResetEvent stopEvent = new ManualResetEvent(false);

		// Token: 0x04000008 RID: 8
		private ManualResetEvent doneStoppingEvent = new ManualResetEvent(true);

		// Token: 0x04000009 RID: 9
		private object lockObject = new object();

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000021 RID: 33
		private delegate XElement DiagnosticHandler(DiagnosableParameters parameters, string remainingArgs);

		// Token: 0x02000004 RID: 4
		private class DictionaryParameters
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00002DDA File Offset: 0x00000FDA
			// (set) Token: 0x06000025 RID: 37 RVA: 0x00002DE2 File Offset: 0x00000FE2
			public string DictionaryName { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000026 RID: 38 RVA: 0x00002DEB File Offset: 0x00000FEB
			// (set) Token: 0x06000027 RID: 39 RVA: 0x00002DF3 File Offset: 0x00000FF3
			public MdbInfo DatabaseInfo { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002DFC File Offset: 0x00000FFC
			// (set) Token: 0x06000029 RID: 41 RVA: 0x00002E04 File Offset: 0x00001004
			public Guid MailboxGuid { get; set; }
		}
	}
}
