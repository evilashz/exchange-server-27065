using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.Parser;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x02000002 RID: 2
	internal class ExSetup : SetupBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public override CommandLineParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public ExSetup()
		{
			base.InteractionHandler = this.GetInteractionHandler();
			this.parser = new ConsoleCommandLineParser(SetupLogger.Logger);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020FC File Offset: 0x000002FC
		[STAThread]
		public static int Main(string[] args)
		{
			return SetupBase.MainCore<ExSetup>(args, new SetupLoggerImpl());
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002109 File Offset: 0x00000309
		public override void ReportException(Exception e)
		{
			base.ReportException(e);
			Console.WriteLine(e.Message);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211D File Offset: 0x0000031D
		public override void ReportError(string error)
		{
			base.ReportError(error);
			this.WriteError(base.InteractionHandler.WrapText(" " + error));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002142 File Offset: 0x00000342
		public override void ReportWarning(string warning)
		{
			this.ReportMessage(warning);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		public override void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			base.UnhandledExceptionHandler(sender, eventArgs);
			string message = (eventArgs.ExceptionObject is Exception) ? ((Exception)eventArgs.ExceptionObject).Message : eventArgs.ExceptionObject.ToString();
			Console.WriteLine();
			Console.Error.WriteLine(Strings.UnhandledErrorMessage(message));
			Console.WriteLine(Strings.AdditionalErrorDetails);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B5 File Offset: 0x000003B5
		public override void ReportMessage(string message)
		{
			SetupLogger.Log(new LocalizedString(message));
			Console.WriteLine(base.InteractionHandler.WrapText(message));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D3 File Offset: 0x000003D3
		public override void ReportMessage()
		{
			Console.WriteLine();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DA File Offset: 0x000003DA
		public override void WriteError(string error)
		{
			Console.Error.WriteLine(error);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E8 File Offset: 0x000003E8
		public override int Run()
		{
			ExitCode exitCode = this.SetupChecks();
			if (exitCode == ExitCode.Error)
			{
				SetupLogger.Log(new LocalizedString("CurrentResult main.run:185: " + ExitCode.Error));
				return 1;
			}
			if (SetupBase.TheApp.ParsedArguments.ContainsKey("updatesdir"))
			{
				if (base.IsExchangeInstalled)
				{
					this.ReportMessage(Strings.UpdatesNotOnFreshInstall);
					SetupLogger.Log(new LocalizedString("CurrentResult main.run:196: " + ExitCode.Error));
					return 1;
				}
				if (SetupLauncherHelper.IsRestart(SetupBase.TheApp.ParsedArguments))
				{
					SetupLauncherHelper.CopyMspFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup\\MspTemp"), Path.Combine(SetupHelper.WindowsDir, "Temp\\ExchangeSetup"));
				}
				else
				{
					ExitCode exitCode2 = this.CheckForUpdates(SetupBase.TheApp.ParsedArguments);
					if (exitCode2 != ExitCode.Success)
					{
						SetupLogger.Log(new LocalizedString("CurrentResult main.run:214: " + exitCode2));
						return (int)exitCode2;
					}
				}
			}
			if (!this.CopyFiles())
			{
				this.ReportMessage(Strings.FileCopyFailed(SetupBase.TheApp.SourceDir, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup")));
				SetupLogger.Log(new LocalizedString("CurrentResult main.run:223: " + ExitCode.Error));
				return 1;
			}
			int num = SetupLauncherHelper.LoadAssembly(SetupBase.SetupArgs, SetupBase.TheApp.ParsedArguments, SetupBase.TheApp, "Microsoft.Exchange.Setup.Console.dll", "Microsoft.Exchange.Setup.Console.Console");
			if (num == 1)
			{
				this.ReportMessage();
				this.ReportMessage(Strings.AdditionalErrorDetails);
			}
			SetupLogger.Log(new LocalizedString("CurrentResult main.run:235: " + num));
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000237C File Offset: 0x0000057C
		public override ExitCode SetupChecks()
		{
			base.SetupChecks();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = SetupLauncherHelper.AreEarlierVersionsOfServerRolesInstalled();
			if (flag)
			{
				stringBuilder.Append(Strings.EarlierVersionsExist);
			}
			if (SetupBase.TheApp.HasValidArgs == ExitCode.Error)
			{
				stringBuilder.Append(Strings.InvalidCommandLineArgs);
			}
			if (flag || SetupBase.TheApp.HasValidArgs == ExitCode.Error)
			{
				this.ReportMessage(Strings.SetupChecksFailed(stringBuilder.ToString()));
				this.ReportMessage(Strings.MoreInformationText);
				return ExitCode.Error;
			}
			return ExitCode.Success;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000241C File Offset: 0x0000061C
		internal ExitCode CheckForUpdates(Dictionary<string, object> parsedArguments)
		{
			string pathName = ((LongPath)parsedArguments["sourcedir"]).PathName;
			if (!Directory.Exists(pathName))
			{
				this.ReportMessage(Strings.SetupChecksFailed(Strings.NoUpdates));
				SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:283: " + ExitCode.Error));
				return ExitCode.Error;
			}
			string pathName2 = ((LongPath)parsedArguments["updatesdir"]).PathName;
			if (!Directory.Exists(pathName2))
			{
				this.ReportMessage(Strings.SetupChecksFailed(Strings.NoUpdates));
				SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:292: " + ExitCode.Error));
				return ExitCode.Error;
			}
			string text = Path.Combine(pathName, "ExchangeServer.msi");
			if (!File.Exists(text))
			{
				this.ReportMessage(Strings.SetupChecksFailed(Strings.NoUpdates));
				SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:300: " + ExitCode.Error));
				return ExitCode.Error;
			}
			string[] files = Directory.GetFiles(pathName2, "*.msp", SearchOption.TopDirectoryOnly);
			if (files.Length == 0)
			{
				this.ReportMessage(Strings.SetupChecksFailed(Strings.NoUpdates));
				SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:308: " + ExitCode.Error));
				return ExitCode.Error;
			}
			using (MspValidator mspValidator = new MspValidator(files, text, null, null, new Action<object>(this.ReportMessageCallback)))
			{
				if (!mspValidator.Validate())
				{
					this.ReportMessage(Strings.SetupChecksFailed(Strings.InvalidUpdates));
					SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:344: " + ExitCode.Error));
					return ExitCode.Error;
				}
				List<string> validatedFiles = mspValidator.ValidatedFiles;
				string text2 = validatedFiles.LastOrDefault((string x) => !string.IsNullOrEmpty(x) && MsiHelper.IsMspFileExtension(x));
				if (!string.IsNullOrEmpty(text2))
				{
					string text3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup\\MspTemp");
					MspUtility.UnpackMspCabs(text2, text3);
					if (SetupLauncherHelper.SetupRequiredFilesUpdated(SetupChecksFileConstant.GetSetupRequiredFiles(), SetupHelper.GetSetupRequiredFilesFromAssembly(text3), text3))
					{
						this.ReportMessage(Strings.RestartSetup);
						SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:336: " + ExitCode.Restart));
						return ExitCode.Restart;
					}
				}
			}
			SetupLogger.Log(new LocalizedString("CurrentResult main.checkforupdates:349: " + ExitCode.Success));
			return ExitCode.Success;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002698 File Offset: 0x00000898
		private bool CopyFiles()
		{
			bool flag = SetupLauncherHelper.IsFromInstalledExchangeDir();
			if (!base.IsExchangeInstalled || !flag)
			{
				this.ReportMessage(Strings.FileCopyText);
				bool flag2 = SetupLauncherHelper.CopyExSetupFiles(SetupBase.TheApp.SourceDir);
				if (!flag2)
				{
					return false;
				}
				if (Datacenter.IsMicrosoftHostedOnly(true))
				{
					string path = "Setup\\ServerRoles\\ClientAccess\\ServicePlans".Split(new char[]
					{
						'\\'
					}, 3)[2];
					try
					{
						SetupHelper.CopyFiles(Path.Combine(SetupBase.TheApp.SourceDir, "Setup\\ServerRoles\\ClientAccess\\ServicePlans"), Path.Combine(SetupBase.TheApp.TargetDir, path), false);
					}
					catch (InsufficientDiskSpaceException)
					{
						SetupLogger.Log(Strings.InsufficientDiskSpace);
						flag2 = false;
					}
					catch (FileNotExistsException ex)
					{
						SetupLogger.Log(Strings.FileCopyException(ex.Message));
						flag2 = false;
					}
					if (!flag2)
					{
						return false;
					}
					path = "Setup\\ServerRoles\\Mailbox".Split(new char[]
					{
						'\\'
					})[2];
					try
					{
						SetupHelper.CopyFiles(Path.Combine(SetupBase.TheApp.SourceDir, "Setup\\ServerRoles\\Mailbox"), Path.Combine(SetupBase.TheApp.TargetDir, path), false);
					}
					catch (InsufficientDiskSpaceException)
					{
						SetupLogger.Log(Strings.InsufficientDiskSpace);
						flag2 = false;
					}
					catch (FileNotExistsException ex2)
					{
						SetupLogger.Log(Strings.FileCopyException(ex2.Message));
						flag2 = false;
					}
					if (!flag2)
					{
						return false;
					}
					this.ReportMessage(Strings.FileCopyComplete);
					return true;
				}
				else
				{
					this.ReportMessage(Strings.FileCopyComplete);
				}
			}
			return true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002828 File Offset: 0x00000A28
		private CommandInteractionHandler GetInteractionHandler()
		{
			CommandInteractionHandler result = new ConsoleInteractionHandler();
			try
			{
				Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
			}
			catch (IOException)
			{
				result = new TextInteractionHandler();
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002868 File Offset: 0x00000A68
		private void ReportMessageCallback(object message)
		{
			if (message is LocalizedString)
			{
				this.ReportMessage((LocalizedString)message);
			}
		}

		// Token: 0x04000001 RID: 1
		private const string ConsoleAssemblyFileName = "Microsoft.Exchange.Setup.Console.dll";

		// Token: 0x04000002 RID: 2
		private const string ConsoleAssemblyTypeFullName = "Microsoft.Exchange.Setup.Console.Console";

		// Token: 0x04000003 RID: 3
		private readonly CommandLineParser parser;
	}
}
