using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.CommonHelpProvider;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200027C RID: 636
	internal class ExchangeHelpService
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x00077AE3 File Offset: 0x00075CE3
		protected ExchangeHelpService()
		{
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00077AEB File Offset: 0x00075CEB
		public static void Initialize()
		{
			Microsoft.Exchange.CommonHelpProvider.HelpProvider.InitializeViaCmdlet(Microsoft.Exchange.CommonHelpProvider.HelpProvider.HelpAppName.Toolbox, null, PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo());
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00077AFE File Offset: 0x00075CFE
		public static void ShowHelpFromHelpTopicId(string helpTopicId)
		{
			ExchangeHelpService.ShowHelpFromHelpTopicId(null, helpTopicId);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00077B08 File Offset: 0x00075D08
		public static void ShowHelpFromHelpTopicId(Control control, string helpTopicId)
		{
			string helpUrlForTopic = ExchangeHelpService.GetHelpUrlForTopic(helpTopicId);
			ExchangeHelpService.ShowHelpFromUrl(control, helpUrlForTopic);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00077B24 File Offset: 0x00075D24
		public static void ShowHelpFromPage(ExchangePage page)
		{
			string helpUrlFromPage = ExchangeHelpService.GetHelpUrlFromPage(page);
			ExchangeHelpService.ShowHelpFromUrl(page, helpUrlFromPage);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00077B3F File Offset: 0x00075D3F
		internal static string GetHelpUrlForTopic(string helpTopicId)
		{
			return Microsoft.Exchange.CommonHelpProvider.HelpProvider.ConstructHelpRenderingUrl(helpTopicId).ToString();
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00077B4C File Offset: 0x00075D4C
		internal static string GetHelpUrlFromPage(ExchangePage page)
		{
			ExchangeForm exchangeForm = page.ParentForm as ExchangeForm;
			string result;
			if (exchangeForm == null || string.IsNullOrEmpty(exchangeForm.HelpTopic))
			{
				result = Microsoft.Exchange.CommonHelpProvider.HelpProvider.ConstructHelpRenderingUrl(page.HelpTopic).ToString();
			}
			else
			{
				result = Microsoft.Exchange.CommonHelpProvider.HelpProvider.ConstructHelpRenderingUrl(exchangeForm.HelpTopic).ToString();
			}
			return result;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00077B9C File Offset: 0x00075D9C
		private static void ShowHelpFromUrl(string helpUrl)
		{
			ExchangeHelpService.ShowHelpFromUrl(null, helpUrl);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00077BA8 File Offset: 0x00075DA8
		private static void ShowHelpFromUrl(Control control, string helpUrl)
		{
			if (!string.IsNullOrEmpty(helpUrl))
			{
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<string>(0L, "*--ExchangeHelpService.ShowHelpFromUrl: {0}", helpUrl);
				string url = helpUrl.Substring(helpUrl.IndexOf("http"));
				ExchangeHelpService.ieHelper.NavigateInSingleIE(url, ExchangeHelpService.GetUIService(control));
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00077BF4 File Offset: 0x00075DF4
		private static IUIService GetUIService(Control control)
		{
			IServiceProvider serviceProvider = control as IServiceProvider;
			IUIService result;
			if (serviceProvider != null)
			{
				result = (((IUIService)serviceProvider.GetService(typeof(IUIService))) ?? new UIService(control));
			}
			else
			{
				result = new UIService(control);
			}
			return result;
		}

		// Token: 0x04000A19 RID: 2585
		private static IEHelper ieHelper = new IEHelper();
	}
}
