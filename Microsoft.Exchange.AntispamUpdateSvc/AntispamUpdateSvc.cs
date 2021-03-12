using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.AntispamUpdate
{
	// Token: 0x02000003 RID: 3
	internal class AntispamUpdateSvc : ExServiceBase
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002865 File Offset: 0x00000A65
		public AntispamUpdateSvc()
		{
			base.ServiceName = "MSExchangeAntispamUpdate";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000289C File Offset: 0x00000A9C
		public static void Main(string[] args)
		{
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
			ExWatson.Register();
			AntispamUpdateSvc.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			int i = 0;
			while (i < args.Length)
			{
				string text = args[i];
				string a;
				if ((a = text.Trim()) == null)
				{
					goto IL_BD;
				}
				if (!(a == "-?"))
				{
					if (!(a == "-console"))
					{
						if (!(a == "-wait"))
						{
							if (!(a == "-60"))
							{
								goto IL_BD;
							}
							AntispamUpdateSvc.pollInterval = 60;
						}
						else
						{
							AntispamUpdateSvc.waitToContinue = true;
						}
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					AntispamUpdateSvc.Usage();
					Environment.Exit(0);
				}
				IL_D3:
				i++;
				continue;
				IL_BD:
				Console.WriteLine("{0}", text);
				AntispamUpdateSvc.Usage();
				Environment.Exit(0);
				goto IL_D3;
			}
			if (AntispamUpdateSvc.runningAsService)
			{
				ServiceBase.Run(new AntispamUpdateSvc());
				return;
			}
			if (flag)
			{
				AntispamUpdateSvc.RunConsole();
				return;
			}
			Console.WriteLine("Use the '-console' argument to run from the command line");
			AntispamUpdateSvc.Usage();
			Environment.Exit(0);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000029C0 File Offset: 0x00000BC0
		protected override void OnStartInternal(string[] args)
		{
			bool flag = false;
			try
			{
				this.hygieneUpdate = new HygieneUpdate(!AntispamUpdateSvc.runningAsService);
				this.timeSliceThread = new Thread(new ThreadStart(this.TimeSliceThreadMain));
				this.timeSliceThread.Start();
				UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_ServiceStartSuccess, null, new string[0]);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_ServiceStartFailure, null, new string[0]);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002A40 File Offset: 0x00000C40
		private static string GetMicrosoftUpdateCabPath()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					if (registryKey == null)
					{
						result = null;
					}
					else
					{
						string text = registryKey.GetValue("MsiInstallPath") as string;
						if (text != null)
						{
							try
							{
								return Path.Combine(text, "TransportRoles\\agents\\Hygiene\\muauth.cab");
							}
							catch (ArgumentException)
							{
							}
						}
						result = null;
					}
				}
			}
			catch (SecurityException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002AC8 File Offset: 0x00000CC8
		private void TimeSliceThreadMain()
		{
			int num = 0;
			AntispamUpdates antispamUpdates = new AntispamUpdates();
			string microsoftUpdateCabPath = AntispamUpdateSvc.GetMicrosoftUpdateCabPath();
			while (!this.timeSliceThreadShutdown.WaitOne(num, false))
			{
				try
				{
					antispamUpdates.LoadConfiguration(string.Empty);
					OptInStatus optInStatus = this.hygieneUpdate.SetMicrosoftUpdateOptinStatus(antispamUpdates.MicrosoftUpdate, microsoftUpdateCabPath);
					if (optInStatus != antispamUpdates.MicrosoftUpdate)
					{
						antispamUpdates.MicrosoftUpdate = optInStatus;
						antispamUpdates.SaveConfiguration(string.Empty);
					}
				}
				catch (SecurityException ex)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOptFail, null, new string[]
					{
						ex.Message
					});
				}
				catch (IOException ex2)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOptFail, null, new string[]
					{
						ex2.Message
					});
				}
				catch (UnauthorizedAccessException ex3)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOptFail, null, new string[]
					{
						ex3.Message
					});
				}
				if (antispamUpdates.MicrosoftUpdate == OptInStatus.Configured && antispamUpdates.UpdateMode == AntispamUpdateMode.Automatic)
				{
					this.hygieneUpdate.DoUpdate();
				}
				num = AntispamUpdateSvc.pollInterval * 1000;
				if (this.hygieneUpdate.ConsoleDiagnostics)
				{
					Console.WriteLine("Sleeping {0} Seconds", num / 1000);
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002C18 File Offset: 0x00000E18
		protected override TimeSpan StopTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(30.0);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002C28 File Offset: 0x00000E28
		protected override void OnStopInternal()
		{
			bool flag = false;
			try
			{
				this.timeSliceThreadShutdown.Set();
				this.timeSliceThread.Join();
				UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_ServiceStopSuccess, null, new string[0]);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_ServiceStopFailure, null, new string[0]);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002C8C File Offset: 0x00000E8C
		private static void Usage()
		{
			Console.WriteLine("Usage: {0} [-console] [-wait] [-60] [-?]", AntispamUpdateSvc.GetAssemblyName());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002CA0 File Offset: 0x00000EA0
		private static void RunConsole()
		{
			Console.WriteLine("Starting {0}, running in console mode.", AntispamUpdateSvc.GetAssemblyName());
			if (AntispamUpdateSvc.waitToContinue)
			{
				Console.WriteLine("Press ENTER to continue startup.");
				Console.ReadLine();
			}
			AntispamUpdateSvc service = new AntispamUpdateSvc();
			ExServiceBase.RunAsConsole(service);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002CDF File Offset: 0x00000EDF
		private static string GetAssemblyName()
		{
			return Assembly.GetExecutingAssembly().GetName().Name;
		}

		// Token: 0x04000008 RID: 8
		internal const string ShortName = "MSExchangeAntispamUpdate";

		// Token: 0x04000009 RID: 9
		internal const string DisplayName = "Microsoft Exchange Anti-spam Update";

		// Token: 0x0400000A RID: 10
		internal const string Description = "The Microsoft Forefront Security for Exchange Server anti-spam update service";

		// Token: 0x0400000B RID: 11
		public const string EventSource = "MSExchange Anti-spam Update";

		// Token: 0x0400000C RID: 12
		private const string HelpOption = "-?";

		// Token: 0x0400000D RID: 13
		private const string ConsoleOption = "-console";

		// Token: 0x0400000E RID: 14
		private const string WaitToContinueOption = "-wait";

		// Token: 0x0400000F RID: 15
		private const string OneMinutePollOption = "-60";

		// Token: 0x04000010 RID: 16
		public static readonly Guid ComponentGuid = new Guid("{a8d4f6f6-7597-429b-8b78-972cdf7ee6c6}");

		// Token: 0x04000011 RID: 17
		private static bool runningAsService;

		// Token: 0x04000012 RID: 18
		private static bool waitToContinue;

		// Token: 0x04000013 RID: 19
		private static int pollInterval = 1800;

		// Token: 0x04000014 RID: 20
		private HygieneUpdate hygieneUpdate;

		// Token: 0x04000015 RID: 21
		private Thread timeSliceThread;

		// Token: 0x04000016 RID: 22
		private EventWaitHandle timeSliceThreadShutdown = new EventWaitHandle(false, EventResetMode.ManualReset);
	}
}
