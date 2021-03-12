using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.Parser;

namespace Microsoft.Exchange.Setup.CommonBase
{
	// Token: 0x02000005 RID: 5
	internal abstract class SetupBase : DisposeTrackableBase, ISetupBase
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000020D0 File Offset: 0x000002D0
		protected SetupBase()
		{
			this.SourceDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			this.TargetDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup");
			this.IsExchangeInstalled = SetupLauncherHelper.IsExchangeInstalled();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000210F File Offset: 0x0000030F
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002116 File Offset: 0x00000316
		public static string[] SetupArgs { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002125 File Offset: 0x00000325
		public static SetupBase TheApp { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002135 File Offset: 0x00000335
		public Dictionary<string, object> ParsedArguments { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27
		public abstract CommandLineParser Parser { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000213E File Offset: 0x0000033E
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002146 File Offset: 0x00000346
		public string SourceDir { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000214F File Offset: 0x0000034F
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002157 File Offset: 0x00000357
		public string TargetDir { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002160 File Offset: 0x00000360
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002168 File Offset: 0x00000368
		public ExitCode HasValidArgs { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002171 File Offset: 0x00000371
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002179 File Offset: 0x00000379
		public CommandInteractionHandler InteractionHandler { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002182 File Offset: 0x00000382
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000218A File Offset: 0x0000038A
		public bool IsExchangeInstalled { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002193 File Offset: 0x00000393
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000219B File Offset: 0x0000039B
		public ISetupLogger Logger { get; internal set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000021A4 File Offset: 0x000003A4
		public virtual void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			SetupLauncherHelper.LogInstalledExchangeDirAcl();
			if (eventArgs.ExceptionObject is Exception)
			{
				Exception e = (Exception)eventArgs.ExceptionObject;
				this.Logger.LogError(e);
				return;
			}
			string errMsg = eventArgs.ExceptionObject.ToString();
			SetupLogger.Log(Strings.UnhandledErrorMessage(errMsg));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000021F3 File Offset: 0x000003F3
		public virtual void ReportException(Exception e)
		{
			this.Logger.LogError(e);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002201 File Offset: 0x00000401
		public virtual void ReportError(string error)
		{
			this.Logger.LogError(new Exception(error));
		}

		// Token: 0x0600002B RID: 43
		public abstract void ReportMessage(string message);

		// Token: 0x0600002C RID: 44
		public abstract void ReportMessage();

		// Token: 0x0600002D RID: 45
		public abstract void ReportWarning(string warning);

		// Token: 0x0600002E RID: 46
		public abstract void WriteError(string error);

		// Token: 0x0600002F RID: 47 RVA: 0x00002214 File Offset: 0x00000414
		public virtual ExitCode SetupChecks()
		{
			this.HasValidArgs = this.ProcessArguments();
			if (this.HasValidArgs == ExitCode.Success && this.ParsedArguments.ContainsKey("sourcedir"))
			{
				this.SourceDir = this.ParsedArguments["sourcedir"].ToString();
			}
			return this.HasValidArgs;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002268 File Offset: 0x00000468
		public virtual int Run()
		{
			return (int)this.SetupChecks();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002270 File Offset: 0x00000470
		public virtual ExitCode ProcessArguments()
		{
			this.ParsedArguments = null;
			ExitCode result = ExitCode.Success;
			try
			{
				this.ParsedArguments = this.Parser.ParseCommandLine(SetupBase.SetupArgs);
				this.Logger.Log(Strings.CommandLine(Assembly.GetExecutingAssembly().GetType().Name, string.Join(" ", SetupBase.SetupArgs)));
			}
			catch (ParseException e)
			{
				this.Logger.Log(Strings.CommandLine(Assembly.GetExecutingAssembly().GetType().Name, string.Join(" ", SetupBase.SetupArgs)));
				this.ReportException(e);
				result = ExitCode.Error;
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002318 File Offset: 0x00000518
		protected static int MainCore<T>(string[] args, ISetupLogger logger) where T : SetupBase, new()
		{
			SetupLogger.Logger = logger;
			SetupBase.SetupArgs = args;
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeBackupPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeRestorePrivilege",
				"SeSecurityPrivilege",
				"SeShutdownPrivilege",
				"SeTakeOwnershipPrivilege",
				"SeDebugPrivilege",
				"SeCreateSymbolicLinkPrivilege"
			});
			SecurityException ex = null;
			try
			{
				ExWatson.Register();
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (TypeInitializationException ex3)
			{
				if (!(ex3.InnerException is SecurityException))
				{
					throw;
				}
				ex = (SecurityException)ex3.InnerException;
			}
			bool flag;
			int result;
			using (new Mutex(true, "Microsoft.Exchange.Setup", ref flag))
			{
				using (SetupBase.TheApp = Activator.CreateInstance<T>())
				{
					SetupBase.TheApp.Logger = logger;
					if (num != 0)
					{
						SetupBase.TheApp.WriteError(Strings.RemovePrivileges(num).ToString());
					}
					if (ex != null)
					{
						SetupBase.TheApp.WriteError(Strings.SecurityIssueFoundWhenInit(ex.Message));
					}
					if (!flag)
					{
						SetupBase.TheApp.WriteError(Strings.CannotRunMultipleInstances);
					}
					try
					{
						logger.StartLogging();
					}
					catch (SetupLogInitializeException ex4)
					{
						SetupBase.TheApp.WriteError(ex4.Message);
						logger.Log(new LocalizedString("CurrentResult setupbase.maincore:353: " + ExitCode.Error));
						return 1;
					}
					int num2 = 0;
					if (num != 0)
					{
						logger.Log(Strings.RemovePrivileges(num));
						num2 = 1;
					}
					if (ex != null)
					{
						logger.Log(Strings.SecurityIssueFoundWhenInit(ex.Message));
						num2 = 1;
					}
					if (!flag)
					{
						logger.Log(Strings.CannotRunMultipleInstances);
						num2 = 1;
					}
					if (num2 != 0)
					{
						logger.Log(new LocalizedString("CurrentResult setupbase.maincore:380: " + num2));
						result = num2;
					}
					else
					{
						SetupLauncherHelper.LogInstalledExchangeDirAcl();
						AppDomain.CurrentDomain.UnhandledException += SetupBase.TheApp.UnhandledExceptionHandler;
						num2 = SetupBase.TheApp.Run();
						SetupLauncherHelper.LogInstalledExchangeDirAcl();
						logger.Log(new LocalizedString("CurrentResult setupbase.maincore:396: " + num2));
						result = num2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025DC File Offset: 0x000007DC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SetupBase>(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025E4 File Offset: 0x000007E4
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				this.Logger.StopLogging();
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x04000009 RID: 9
		public const string Indent1 = " ";

		// Token: 0x0400000A RID: 10
		private const string SetupMutexName = "Microsoft.Exchange.Setup";
	}
}
