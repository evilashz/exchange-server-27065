using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.PowerShell;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class LauncherBase
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x000122F0 File Offset: 0x000104F0
		protected LauncherBase()
		{
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000122F8 File Offset: 0x000104F8
		protected LauncherBase(ISetupContext context, MonadConnection connection)
		{
			this.RootDataHandler = new RootDataHandler(context, connection);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001230D File Offset: 0x0001050D
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00012315 File Offset: 0x00010515
		public RootDataHandler RootDataHandler { get; set; }

		// Token: 0x0600053E RID: 1342 RVA: 0x0001231E File Offset: 0x0001051E
		protected virtual void CreateRootDataHandler(Dictionary<string, object> arguemnts, MonadConnection connection)
		{
			this.RootDataHandler = new RootDataHandler(arguemnts, connection);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00012330 File Offset: 0x00010530
		protected int MainCore(string[] args, Dictionary<string, object> parsedArguments, SetupBase theApp)
		{
			int num = 0;
			Globals.InitializeSinglePerfCounterInstance();
			try
			{
				MonadRunspaceConfiguration.AddArray(CmdletConfigurationEntries.ExchangeCmdletConfigurationEntries);
				MonadRunspaceConfiguration.AddArray(CmdletConfigurationEntries.ExchangeNonEdgeCmdletConfigurationEntries);
				MonadRunspaceConfiguration.AddArray(CmdletConfigurationEntries.ExchangeEdgeCmdletConfigurationEntries);
				MonadRunspaceConfiguration.AddArray(CmdletConfigurationEntries.SetupCmdletConfigurationEntries);
				MonadRunspaceConfiguration.AddArray(CmdletConfigurationEntries.ExchangeNonGallatinCmdletConfigurationEntries);
			}
			catch (TypeInitializationException ex)
			{
				string text = ex.Message;
				if (ex.InnerException != null)
				{
					text += "\r\n";
					text += ex.InnerException.Message;
				}
				SetupLogger.LogError(ex);
				theApp.WriteError(text);
				SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:90: " + 1));
				return 1;
			}
			MonadRunspaceConfiguration.EnsureMonadScriptsAreRunnable();
			using (MonadConnection monadConnection = new MonadConnection("pooled=false"))
			{
				monadConnection.Open();
				this.CreateRootDataHandler(parsedArguments, monadConnection);
				try
				{
					this.RootDataHandler.Read(theApp.InteractionHandler, null);
				}
				catch (FileNotFoundException ex2)
				{
					SetupLogger.LogError(ex2);
					theApp.ReportError(Strings.CannotFindFile(ex2.FileName));
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:115: " + 1));
					return 1;
				}
				catch (TransientException e)
				{
					SetupLogger.LogError(e);
					theApp.ReportError(Strings.SetupExitsBecauseOfTransientException);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:122: " + 1));
					return 1;
				}
				catch (LPPathNotFoundException e2)
				{
					SetupLogger.LogError(e2);
					theApp.ReportError(Strings.SetupExitsBecauseOfLPPathNotFoundException);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:129: " + 1));
					return 1;
				}
				catch (LPVersioningValueException e3)
				{
					SetupLogger.LogError(e3);
					theApp.ReportError(Strings.LPVersioningInvalidValue);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:136: " + 1));
					return 1;
				}
				catch (LanguagePackBundleLoadException e4)
				{
					SetupLogger.LogError(e4);
					theApp.ReportError(Strings.UnableToFindLPVersioning);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:143: " + 1));
					return 1;
				}
				catch (CabUtilityWrapperException e5)
				{
					SetupLogger.LogError(e5);
					theApp.ReportError(Strings.CabUtilityWrapperError);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:150: " + 1));
					return 1;
				}
				catch (CabUtilityDuplicateKeyException e6)
				{
					SetupLogger.LogError(e6);
					theApp.ReportError(Strings.CabUtilityWrapperError);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:157: " + 1));
					return 1;
				}
				catch (InvalidOrganizationNameException ex3)
				{
					SetupLogger.LogError(ex3);
					theApp.ReportError(ex3.Message);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:164: " + 1));
					return 1;
				}
				catch (InvalidFqdnException ex4)
				{
					SetupLogger.LogError(ex4);
					theApp.ReportError(ex4.Message);
					SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:171: " + 1));
					return 1;
				}
				num = this.ProcessRunInternal(args, theApp);
				if (this.RootDataHandler.ModeDatahandler.RebootRequired && num == 0)
				{
					theApp.ReportWarning(Strings.SetupRebootRequired);
				}
			}
			SetupLogger.Log(new LocalizedString("CurrentResult launcherbase.maincore:90: " + num));
			return num;
		}

		// Token: 0x06000540 RID: 1344
		protected abstract int ProcessRunInternal(string[] args, SetupBase theApp);
	}
}
