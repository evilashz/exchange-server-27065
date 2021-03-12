using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DeliveryService;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000002 RID: 2
	internal sealed class MailboxTransportDeliveryService : ExServiceBase, IDiagnosable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MailboxTransportDeliveryService()
		{
			base.ServiceName = "Microsoft Exchange Mailbox Transport Delivery";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public static void Main(string[] args)
		{
			CommonDiagnosticsLog.Initialize(HostId.MailboxDeliveryService);
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StartProcess);
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.RegisterWatson);
			ExWatson.Init("E12IIS");
			AppDomain.CurrentDomain.UnhandledException += MailboxTransportDeliveryService.MainUnhandledExceptionHandler;
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.RegisterWatsonAction);
			ExWatson.RegisterReportAction(new WatsonRegKeyReportAction(MailboxTransportDeliveryService.watsonRegKeyReportActionString), WatsonActionScope.Process);
			MailboxTransportDeliveryService.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					MailboxTransportDeliveryService.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-console"))
				{
					flag = true;
				}
				else if (text.StartsWith("-wait"))
				{
					flag2 = true;
				}
			}
			if (!MailboxTransportDeliveryService.runningAsService)
			{
				if (!flag)
				{
					MailboxTransportDeliveryService.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.InitializePerformanceCounterInstance);
			Globals.InitializeSinglePerfCounterInstance();
			SettingOverrideSync.Instance.Start(true);
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.LoadTransportAppConfig);
			string text2;
			if (!Components.TryLoadTransportAppConfig(out text2))
			{
				MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryServiceStartFailure, null, new string[]
				{
					text2
				});
				MailboxTransportDeliveryService.PublishServiceStartFailureNotification(text2);
				Environment.Exit(1);
			}
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.CreateService);
			MailboxTransportDeliveryService.mailboxTransportDeliveryService = new MailboxTransportDeliveryService();
			if (!MailboxTransportDeliveryService.runningAsService)
			{
				ExServiceBase.RunAsConsole(MailboxTransportDeliveryService.mailboxTransportDeliveryService);
				return;
			}
			ServiceBase.Run(MailboxTransportDeliveryService.mailboxTransportDeliveryService);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022A8 File Offset: 0x000004A8
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "MailboxDelivery";
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022B0 File Offset: 0x000004B0
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			MailboxTransportDeliveryService.logData[0] = new KeyValuePair<string, object>("DiagnosticsRequest", parameters.Argument);
			CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.MailboxDeliveryService, MailboxTransportDeliveryService.logData);
			return new XElement("MailboxDelivery", new object[]
			{
				new XElement("runningAsService", MailboxTransportDeliveryService.runningAsService),
				new XElement("stage", MailboxTransportDeliveryService.stage)
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002340 File Offset: 0x00000540
		protected override void OnStartInternal(string[] args)
		{
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StartService);
			MailboxTransportDeliveryService.diag.TracePfd<int, DateTime>(0L, "PFD EMS {0} Starting MailboxDeliveryService ({1})", 24475, DateTime.UtcNow);
			bool flag = false;
			bool flag2 = false;
			string text = string.Empty;
			try
			{
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.RegisterPamComponent);
				ProcessAccessManager.RegisterComponent(this);
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StartMessageTracking);
				MessageTrackingLog.Start("MSGTRKMD");
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.LoadConfiguration);
				StorageExceptionHandler.Init();
				DeliveryConfiguration.Instance.Load(MailboxTransportDeliveryService.messageListener);
				try
				{
					ProcessAccessManager.RegisterComponent(SettingOverrideSync.Instance);
					this.isDiagnosticHandlerRegisteredForSettingOverrideSync = true;
				}
				catch (RpcException ex)
				{
					MailboxTransportDeliveryService.diag.TraceError<string>(0L, "Failed to register SettingOverride component with Rpc Server. Error : {0}", ex.ToString());
				}
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StartBackgroundThread);
				MailboxTransportDeliveryService.backgroundThread = new BackgroundThreadDelivery();
				MailboxTransportDeliveryService.backgroundThread.Start(false, ServiceState.Active);
				MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryServiceStartSuccess, null, new string[0]);
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.ServiceStarted);
				MailboxTransportDeliveryService.diag.TracePfd<int>(0L, "PFD EMS {0} MailTransportDeliveryService Started", 26523);
				flag = true;
			}
			catch (ADTransientException ex2)
			{
				MailboxTransportDeliveryService.diag.TraceError<string>(0L, "Failed to start MailboxDeliveryService. Error: {0}", ex2.ToString());
				MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryServiceStartFailure, null, new string[]
				{
					ex2.ToString()
				});
				MailboxTransportDeliveryService.PublishServiceStartFailureNotification(ex2.Message);
				Environment.Exit(1);
			}
			catch (ConfigurationErrorsException ex3)
			{
				text = ex3.Message;
				flag2 = true;
			}
			catch (HandlerParseException ex4)
			{
				text = ex4.Message;
				flag2 = true;
			}
			finally
			{
				if (!flag)
				{
					MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryServiceStartFailure, null, new string[]
					{
						text
					});
					MailboxTransportDeliveryService.PublishServiceStartFailureNotification(text);
					MailboxTransportDeliveryService.diag.TraceError(0L, "Failed to start MailboxDeliveryService");
					if (flag2)
					{
						base.Stop();
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002558 File Offset: 0x00000758
		protected override void OnStopInternal()
		{
			MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StopService);
			MailboxTransportDeliveryService.isExiting = true;
			ADNotificationListener.Stop();
			MailboxTransportDeliveryService.diag.TraceDebug<DateTime>(0L, "Stopping MailTransportDeliveryService ({0})", DateTime.UtcNow);
			bool flag = false;
			try
			{
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StopBackgroundThread);
				if (MailboxTransportDeliveryService.backgroundThread != null)
				{
					MailboxTransportDeliveryService.backgroundThread.Stop();
				}
				if (this.isDiagnosticHandlerRegisteredForSettingOverrideSync)
				{
					try
					{
						ProcessAccessManager.UnregisterComponent(SettingOverrideSync.Instance);
					}
					catch (RpcException ex)
					{
						MailboxTransportDeliveryService.diag.TraceError<string>(0L, "Failed to unregister SettingOverride component with Rpc Server. Error : {0}", ex.ToString());
					}
				}
				SettingOverrideSync.Instance.Stop();
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StopMessageTracking);
				MessageTrackingLog.Stop();
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.StopConfiguration);
				DeliveryConfiguration.Instance.Unload();
				MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryServiceStopSuccess, null, new string[0]);
				MailboxTransportDeliveryService.diag.TraceDebug(0L, "Stopped MailboxDeliveryService");
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.UngregisterPamComponent);
				ProcessAccessManager.UnregisterComponent(this);
				MailboxTransportDeliveryService.LogStage(MailboxTransportDeliveryService.Stage.ServiceStopped);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryServiceStopFailure, null, new string[0]);
					MailboxTransportDeliveryService.diag.TraceError(0L, "Failed to stop MailboxDeliveryService");
				}
			}
			DeliveryThrottling.Instance.Dispose();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002684 File Offset: 0x00000884
		protected override void OnCustomCommandInternal(int command)
		{
			if (command != 201)
			{
				return;
			}
			DeliveryConfiguration.Instance.ConfigUpdate();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000026A6 File Offset: 0x000008A6
		private static void Usage()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000026C8 File Offset: 0x000008C8
		private static void LogStage(MailboxTransportDeliveryService.Stage stage)
		{
			MailboxTransportDeliveryService.stage = stage;
			MailboxTransportDeliveryService.logData[0] = new KeyValuePair<string, object>("Stage", stage.ToString());
			CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.MailboxDeliveryService, MailboxTransportDeliveryService.logData);
			ExWatson.AddExtraData(string.Format("{0:u}: Stage={1}", DateTime.UtcNow, stage));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002730 File Offset: 0x00000930
		private static void MainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			MailboxTransportDeliveryService.diag.TraceDebug(0L, "MainUnhandledExceptionHandler");
			if (MailboxTransportDeliveryService.isExiting)
			{
				Environment.Exit(0);
			}
			if (Components.IsActive)
			{
				Components.OnUnhandledException((Exception)eventArgs.ExceptionObject);
				PoisonMessage.SavePoisonContext((Exception)eventArgs.ExceptionObject);
				DeliveryConfiguration.Instance.PoisonHandler.SavePoisonContext();
				MailboxTransportEventLogger.LogEvent(MSExchangeDeliveryEventLogConstants.Tuple_DeliveryPoisonMessage, null, new string[]
				{
					PoisonHandler<DeliveryPoisonContext>.Context.ToString(),
					eventArgs.ExceptionObject.ToString()
				});
			}
			int num = Interlocked.Exchange(ref MailboxTransportDeliveryService.busyUnhandledException, 1);
			if (num == 1)
			{
				return;
			}
			ExWatson.HandleException(sender, eventArgs);
			MailboxTransportDeliveryService.diag.TraceDebug(0L, "Done processing unhandled exception. Return to CLR.");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000027EC File Offset: 0x000009EC
		private static void PublishServiceStartFailureNotification(string exceptionMessage = null)
		{
			string text = "Mailbox Transport Delivery service did not successfully initialize.";
			if (!string.IsNullOrEmpty(exceptionMessage))
			{
				text += string.Format(" Exception was: {0}", exceptionMessage);
			}
			EventNotificationItem.Publish(ExchangeComponent.MailboxTransport.Name, "DeliveryServiceStartFailure", null, text, ResultSeverityLevel.Warning, false);
		}

		// Token: 0x04000001 RID: 1
		public const string MailboxTransportDeliveryServiceName = "Microsoft Exchange Mailbox Transport Delivery";

		// Token: 0x04000002 RID: 2
		private const string ProcessAccessManagerComponentName = "MailboxDelivery";

		// Token: 0x04000003 RID: 3
		private const string HelpOption = "-?";

		// Token: 0x04000004 RID: 4
		private const string ConsoleOption = "-console";

		// Token: 0x04000005 RID: 5
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000006 RID: 6
		public static readonly Guid MailboxTransportServiceComponentGuid = new Guid("{AFADB38E-21D5-4937-B5A1-E30ED4615958}");

		// Token: 0x04000007 RID: 7
		private static readonly Trace diag = ExTraceGlobals.ServiceTracer;

		// Token: 0x04000008 RID: 8
		private static KeyValuePair<string, object>[] logData = new KeyValuePair<string, object>[1];

		// Token: 0x04000009 RID: 9
		private static MailboxTransportDeliveryService.Stage stage;

		// Token: 0x0400000A RID: 10
		private static bool runningAsService;

		// Token: 0x0400000B RID: 11
		private static MailboxTransportDeliveryService mailboxTransportDeliveryService;

		// Token: 0x0400000C RID: 12
		private static DeliveryListener messageListener = new DeliveryListener();

		// Token: 0x0400000D RID: 13
		private static string watsonRegKeyReportActionString = "HKLM\\SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ImagePath";

		// Token: 0x0400000E RID: 14
		private static BackgroundThreadDelivery backgroundThread;

		// Token: 0x0400000F RID: 15
		private static bool isExiting = false;

		// Token: 0x04000010 RID: 16
		private static int busyUnhandledException;

		// Token: 0x04000011 RID: 17
		private bool isDiagnosticHandlerRegisteredForSettingOverrideSync;

		// Token: 0x02000003 RID: 3
		internal enum Stage
		{
			// Token: 0x04000013 RID: 19
			NotStarted,
			// Token: 0x04000014 RID: 20
			StartProcess,
			// Token: 0x04000015 RID: 21
			RegisterWatson,
			// Token: 0x04000016 RID: 22
			RegisterWatsonAction,
			// Token: 0x04000017 RID: 23
			InitializePerformanceCounterInstance,
			// Token: 0x04000018 RID: 24
			LoadTransportAppConfig,
			// Token: 0x04000019 RID: 25
			CreateService,
			// Token: 0x0400001A RID: 26
			RunService,
			// Token: 0x0400001B RID: 27
			StartService,
			// Token: 0x0400001C RID: 28
			RegisterPamComponent,
			// Token: 0x0400001D RID: 29
			StartMessageTracking,
			// Token: 0x0400001E RID: 30
			LoadConfiguration,
			// Token: 0x0400001F RID: 31
			StartBackgroundThread,
			// Token: 0x04000020 RID: 32
			ServiceStarted,
			// Token: 0x04000021 RID: 33
			StopService,
			// Token: 0x04000022 RID: 34
			StopBackgroundThread,
			// Token: 0x04000023 RID: 35
			StopConfiguration,
			// Token: 0x04000024 RID: 36
			UngregisterPamComponent,
			// Token: 0x04000025 RID: 37
			StopMessageTracking,
			// Token: 0x04000026 RID: 38
			ShutdownConnectionCache,
			// Token: 0x04000027 RID: 39
			ServiceStopped
		}
	}
}
