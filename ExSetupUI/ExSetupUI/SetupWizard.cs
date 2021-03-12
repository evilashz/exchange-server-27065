using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.CommonHelpProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.Parser;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000025 RID: 37
	internal partial class SetupWizard : SetupFormBase, IHelpUrlGenerator
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public SetupWizard(SetupBase theApp, bool updates, bool isExchangeInstalled)
		{
			base.Name = "SetupWizard";
			this.Text = Strings.SetupWizardCaption;
			this.InitializeComponent();
			base.SetBtnNextText(Strings.btnNext);
			base.SetBtnPreviousText(Strings.btnPrevious);
			base.SetCancelMessageBoxMessage(Strings.CancelMessageBoxMessage);
			base.SetPrintErrorMessageBoxMessage(Strings.SetupFailedPrintDocument);
			base.SetBrowserLaunchErrorMessage(Strings.BrowserLaunchError);
			int num = 23;
			if (num > 0)
			{
				base.SetExchangeServerLabel(Strings.MESICumulativeUpdate(Strings.MicrosoftExchangeServer, num));
			}
			else
			{
				base.SetExchangeServerLabel(Strings.MESI(Strings.MicrosoftExchangeServer));
			}
			HelpProvider.Initialize(HelpProvider.HelpAppName.Setup);
			base.HelpUrlGenerator = this;
			SetupWizard.IsPartiallyConfigured = this.IsRolesPartiallyConfigured(isExchangeInstalled);
			if (SetupWizard.IsPartiallyConfigured && ((SetupOperations)theApp.ParsedArguments["mode"] == SetupOperations.Install || (SetupOperations)theApp.ParsedArguments["mode"] == SetupOperations.Upgrade))
			{
				SetupWizard.PopulateWizard(base.Pages, theApp);
				return;
			}
			if (theApp.ParsedArguments.ContainsKey("mode") && (((SetupOperations)theApp.ParsedArguments["mode"] == SetupOperations.Install && !isExchangeInstalled) || (SetupOperations)theApp.ParsedArguments["mode"] == SetupOperations.Upgrade))
			{
				if (updates)
				{
					base.Pages.Add(new CheckForUpdatesPage());
					base.Pages.Add(new UpdatesDownloadsPage(theApp));
				}
				base.Pages.Add(new CopyFilesPage(theApp));
				base.Pages.Add(new InitializingSetupPage(base.Pages, theApp));
				return;
			}
			SetupWizard.PopulateWizard(base.Pages, theApp);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000ABA7 File Offset: 0x00008DA7
		internal static bool IsPartiallyConfigured { get; private set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x0000ABAF File Offset: 0x00008DAF
		string IHelpUrlGenerator.GetHelpUrl(string helpId)
		{
			return HelpProvider.ConstructHelpRenderingUrlWithQualifierHelpId("ms.exch.setup.", helpId).ToString();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		internal static void PopulateWizard(IList<SetupWizardPage> pages, SetupBase theApp)
		{
			AssemblyResolver[] array = null;
			string text = string.Empty;
			try
			{
				if (SetupWizard.setupGUIAssembly == null)
				{
					text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
					array = AssemblyResolver.Install(new AssemblyResolver[]
					{
						new FileSearchAssemblyResolver
						{
							SearchPaths = new string[]
							{
								text
							},
							Recursive = true
						}
					});
					text = Path.Combine(text, "Microsoft.Exchange.Setup.GUI.dll");
					object[] parameters = new object[]
					{
						pages,
						theApp
					};
					SetupWizard.setupGUIAssembly = Assembly.LoadFile(text);
					foreach (Type type in SetupWizard.setupGUIAssembly.GetTypes())
					{
						if (type.IsClass && type.FullName == "Microsoft.Exchange.Setup.GUI.SetupWizard")
						{
							object obj = Activator.CreateInstance(type);
							MethodInfo method = type.GetMethod("Main");
							method.Invoke(obj, parameters);
							break;
						}
					}
				}
			}
			catch (FileNotFoundException e)
			{
				SetupLogger.LogError(e);
				SetupWizard.ThrowAssemblyLoadFileNotFoundException(theApp, text);
			}
			catch (Exception ex)
			{
				SetupLogger.LogError(ex);
				if (ex.InnerException != null && ex.InnerException is FileNotFoundException)
				{
					SetupWizard.ThrowAssemblyLoadFileNotFoundException(theApp, text);
				}
				throw;
			}
			finally
			{
				if (array != null)
				{
					AssemblyResolver.Uninstall(array);
				}
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000AD58 File Offset: 0x00008F58
		private static void ThrowAssemblyLoadFileNotFoundException(SetupBase theApp, string exSetupGUIDllLocation)
		{
			string setupArgs = string.Empty;
			if (SetupBase.SetupArgs != null && SetupBase.SetupArgs.Length > 0)
			{
				setupArgs = string.Join(".", SetupBase.SetupArgs);
			}
			SetupLogger.Log(Strings.AssemblyLoadFileNotFound(exSetupGUIDllLocation, setupArgs, theApp.SourceDir, theApp.TargetDir, theApp.IsExchangeInstalled));
			throw new AssemblyLoadFileNotFoundException();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		private bool IsRolesPartiallyConfigured(bool isExchangeInstalled)
		{
			if (isExchangeInstalled)
			{
				RoleCollection installedRoles = RoleManager.GetInstalledRoles();
				if (installedRoles.Count == 0)
				{
					return true;
				}
				foreach (Role role in RoleManager.Roles)
				{
					if (role.IsPartiallyInstalled || (role.IsUnpacked && !role.IsInstalled))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x040000FD RID: 253
		private const string ExSetupGUIDllName = "Microsoft.Exchange.Setup.GUI.dll";

		// Token: 0x040000FE RID: 254
		private static Assembly setupGUIAssembly;
	}
}
