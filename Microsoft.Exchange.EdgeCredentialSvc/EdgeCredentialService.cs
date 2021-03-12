using System;
using System.Globalization;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessageSecurity;
using Microsoft.Exchange.MessageSecurity.EdgeSync;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000002 RID: 2
	internal class EdgeCredentialService : ExServiceBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public EdgeCredentialService()
		{
			base.ServiceName = "MSExchangeEdgeCredential";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 98, ".ctor", "f:\\15.00.1497\\sources\\dev\\MessageSecurity\\src\\Service\\EdgeCredentialService.cs");
			this.configReloadTimer = new Timer(new TimerCallback(this.PeriodicCheck), null, (int)EdgeCredentialService.refreshInterval.TotalMilliseconds, (int)EdgeCredentialService.refreshInterval.TotalMilliseconds);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000215C File Offset: 0x0000035C
		public static void Main(string[] args)
		{
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeSecurityPrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			ExWatson.Register();
			bool flag = false;
			bool waitToContinue = false;
			int i = 0;
			while (i < args.Length)
			{
				string text = args[i];
				string a;
				if ((a = text.Trim()) == null)
				{
					goto IL_9D;
				}
				if (!(a == "-?"))
				{
					if (!(a == "-console"))
					{
						if (!(a == "-wait"))
						{
							goto IL_9D;
						}
						waitToContinue = true;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					EdgeCredentialService.DisplayHelpAndQuit(true);
				}
				IL_A3:
				i++;
				continue;
				IL_9D:
				EdgeCredentialService.DisplayHelpAndQuit(false);
				goto IL_A3;
			}
			Globals.InitializeSinglePerfCounterInstance();
			if (!Environment.UserInteractive)
			{
				ServiceBase.Run(new EdgeCredentialService());
				return;
			}
			if (flag)
			{
				EdgeCredentialService.RunFromConsole(waitToContinue);
				return;
			}
			Console.WriteLine("Use the '-console' argument to run from the command line");
			EdgeCredentialService.DisplayHelpAndQuit(false);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022A8 File Offset: 0x000004A8
		protected override void OnStartInternal(string[] args)
		{
			if (!Environment.UserInteractive)
			{
				base.RequestAdditionalTime((int)TimeSpan.FromSeconds(90.0).TotalMilliseconds);
			}
			Server localServer = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localServer = this.configSession.ReadLocalServer();
			}, 3);
			if (!adoperationResult.Succeeded || localServer == null)
			{
				Exception exception = adoperationResult.Exception;
				ExTraceGlobals.EdgeCredentialServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to locate localServer. The service is stopping. Error: {0}", exception);
				if (exception != null)
				{
					Common.EventLogger.LogEvent(MessageSecurityEventLogConstants.Tuple_LocalServerNotFound, null, new object[]
					{
						exception.Message
					});
				}
				else
				{
					Common.EventLogger.LogEvent(MessageSecurityEventLogConstants.Tuple_LocalServerNotFoundNoException, null, new object[0]);
				}
				Environment.Exit(1);
			}
			AdamUserManagement.UpdateEdgeSyncCredentialsOnEdge(this.configSession, localServer);
			adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId childId = this.configSession.GetOrgContainerId().GetChildId("Administrative Groups");
				EdgeCredentialService.serverRequestCookie = ADNotificationAdapter.RegisterChangeNotification<Server>(childId, new ADNotificationCallback(this.ServerNotificationDispatch), null);
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				Exception exception2 = adoperationResult.Exception;
				ExTraceGlobals.EdgeCredentialServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to register change notification for servers config. The service is stopping. Error: {0}", exception2);
				Common.EventLogger.LogEvent((exception2 is ADTransientException) ? MessageSecurityEventLogConstants.Tuple_ReadServerConfigUnavail : MessageSecurityEventLogConstants.Tuple_ReadServerConfigFailed, null, new object[0]);
				Environment.Exit(1);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023EC File Offset: 0x000005EC
		protected override void OnStopInternal()
		{
			if (EdgeCredentialService.serverRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(EdgeCredentialService.serverRequestCookie);
			}
			if (this.configReloadTimer != null)
			{
				this.configReloadTimer.Dispose();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002412 File Offset: 0x00000612
		private static void DisplayHelpAndQuit(bool success)
		{
			if (!success)
			{
				Console.WriteLine("Incorrect argument.");
			}
			Console.WriteLine("Usage: EdgeCredentialService.exe [-console] [-wait] [-?]");
			Environment.Exit(success ? 0 : 1);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002438 File Offset: 0x00000638
		private static void RunFromConsole(bool waitToContinue)
		{
			Console.WriteLine("Starting {0}, running in console mode.", "MSExchangeEdgeCredential");
			if (waitToContinue)
			{
				Console.WriteLine("Press ENTER to continue startup.");
				Console.ReadLine();
			}
			EdgeCredentialService service = new EdgeCredentialService();
			ExServiceBase.RunAsConsole(service);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002494 File Offset: 0x00000694
		private void PeriodicCheck(object state)
		{
			Server localServer = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localServer = this.configSession.ReadLocalServer();
			}, 3);
			if (adoperationResult.Succeeded && localServer != null)
			{
				AdamUserManagement.UpdateEdgeSyncCredentialsOnEdge(this.configSession, localServer);
				this.RaiseAlertIfNeeded(localServer);
				return;
			}
			Exception exception = adoperationResult.Exception;
			ExTraceGlobals.EdgeCredentialServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to locate localServer during PeriodicCheck. Error: {0}", exception);
			if (exception != null)
			{
				Common.EventLogger.LogEvent(MessageSecurityEventLogConstants.Tuple_LocalServerNotFound, null, new object[]
				{
					exception.Message
				});
				return;
			}
			Common.EventLogger.LogEvent(MessageSecurityEventLogConstants.Tuple_LocalServerNotFoundNoException, null, new object[0]);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002554 File Offset: 0x00000754
		private void RaiseAlertIfNeeded(Server localServer)
		{
			if (localServer.EdgeSyncStatus == null || localServer.EdgeSyncStatus.Count == 0 || string.IsNullOrEmpty(localServer.EdgeSyncStatus[0]))
			{
				return;
			}
			if (localServer.EdgeSyncLease == null)
			{
				DateTime dateTime = DateTime.Parse(localServer.EdgeSyncStatus[0], CultureInfo.InvariantCulture);
				if (DateTime.UtcNow > dateTime)
				{
					Common.EventLogger.LogEvent(MessageSecurityEventLogConstants.Tuple_SubscriptionExpired, null, new object[]
					{
						dateTime
					});
				}
				return;
			}
			LeaseToken leaseToken = LeaseToken.Parse(Encoding.ASCII.GetString(localServer.EdgeSyncLease));
			if (DateTime.UtcNow > leaseToken.AlertTime)
			{
				Common.EventLogger.LogEvent(MessageSecurityEventLogConstants.Tuple_LeaseAlert, null, new object[]
				{
					leaseToken.LastSync
				});
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000262C File Offset: 0x0000082C
		private void ServerNotificationDispatch(ADNotificationEventArgs args)
		{
			try
			{
				if (Interlocked.Increment(ref this.notificationHandlerCount) == 1)
				{
					try
					{
						this.configReloadTimer.Change(EdgeCredentialService.notificationDelay, EdgeCredentialService.refreshInterval);
					}
					catch (ObjectDisposedException)
					{
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.notificationHandlerCount);
			}
		}

		// Token: 0x04000001 RID: 1
		public const string ServiceShortName = "MSExchangeEdgeCredential";

		// Token: 0x04000002 RID: 2
		public const string ServiceBinaryName = "Microsoft.Exchange.EdgeCredentialSvc.exe";

		// Token: 0x04000003 RID: 3
		private const string HelpOption = "-?";

		// Token: 0x04000004 RID: 4
		private const string ConsoleOption = "-console";

		// Token: 0x04000005 RID: 5
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000006 RID: 6
		private static readonly TimeSpan refreshInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000007 RID: 7
		private static readonly TimeSpan notificationDelay = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000008 RID: 8
		private static ADNotificationRequestCookie serverRequestCookie;

		// Token: 0x04000009 RID: 9
		private Timer configReloadTimer;

		// Token: 0x0400000A RID: 10
		private int notificationHandlerCount;

		// Token: 0x0400000B RID: 11
		private ITopologyConfigurationSession configSession;
	}
}
