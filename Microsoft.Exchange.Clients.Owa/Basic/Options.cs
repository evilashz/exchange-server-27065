using System;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000A0 RID: 160
	public class Options : OwaPage
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0002D0A2 File Offset: 0x0002B2A2
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0002D0A8 File Offset: 0x0002B2A8
		public Options()
		{
			this.applicationElement = Convert.ToString(base.OwaContext.FormsRegistryContext.ApplicationElement);
			if (base.OwaContext.FormsRegistryContext.Type != null)
			{
				this.type = base.OwaContext.FormsRegistryContext.Type;
				return;
			}
			this.type = "Messaging";
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0002D10F File Offset: 0x0002B30F
		public string ApplicationElement
		{
			get
			{
				return this.applicationElement;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0002D117 File Offset: 0x0002B317
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002D120 File Offset: 0x0002B320
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(NavigationModule.Options, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0002D14C File Offset: 0x0002B34C
		public void RenderNavigationOptions()
		{
			OptionsNavigation optionsNavigation = new OptionsNavigation(this.type);
			optionsNavigation.Render(base.Response.Output, base.UserContext);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0002D17C File Offset: 0x0002B37C
		public void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.None, OptionsBar.RenderingFlags.OptionsSelected, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		public void RenderSubOptions()
		{
			string key;
			switch (key = this.type)
			{
			case "Regional":
				this.RenderOptions(HelpIdsLight.OptionsRegionalSettingsLight.ToString());
				return;
			case "Messaging":
				this.RenderOptions(HelpIdsLight.OptionsMessagingLight.ToString());
				return;
			case "JunkEmail":
				this.RenderOptions(HelpIdsLight.OptionsJunkEmailLight.ToString());
				return;
			case "Calendar":
				this.RenderOptions(HelpIdsLight.OptionsCalendarLight.ToString());
				return;
			case "Oof":
				this.RenderOptions(HelpIdsLight.OptionsOofLight.ToString());
				return;
			case "ChangePassword":
				this.RenderOptions(HelpIdsLight.OptionsChangePasswordLight.ToString());
				return;
			case "General":
				this.RenderOptions(HelpIdsLight.OptionsAccessibilityLight.ToString());
				return;
			case "About":
				this.RenderOptions(HelpIdsLight.OptionsAboutLight.ToString());
				return;
			case "Eas":
				this.RenderOptions(HelpIdsLight.OptionsMobileDevicesLight.ToString());
				return;
			}
			this.RenderOptions(HelpIdsLight.OptionsLight.ToString());
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0002D345 File Offset: 0x0002B545
		public void RenderOptionsControl()
		{
			if (this.optionsControl != null)
			{
				this.optionsControl.Render();
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002D35C File Offset: 0x0002B55C
		public void RenderHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			if (this.type == "About" || this.type == "Eas")
			{
				toolbar.RenderButton(ToolbarButtons.CloseText);
			}
			else
			{
				toolbar.RenderButton(ToolbarButtons.Save);
			}
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		public void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002D408 File Offset: 0x0002B608
		protected override void OnInit(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "opturl", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				this.type = queryStringParameter;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002D438 File Offset: 0x0002B638
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string key;
			switch (key = this.type)
			{
			case "Messaging":
				this.optionsControl = new MessagingOptions(base.OwaContext, base.Response.Output);
				break;
			case "Regional":
				this.optionsControl = new RegionalOptions(base.OwaContext, base.Response.Output);
				break;
			case "JunkEmail":
				if (!base.UserContext.IsFeatureEnabled(Feature.JunkEMail))
				{
					throw new OwaSegmentationException("Junk e-mail filtering configuration is disabled");
				}
				this.optionsControl = new JunkEmailOptions(base.OwaContext, base.Response.Output);
				break;
			case "Calendar":
				if (!base.UserContext.IsFeatureEnabled(Feature.Calendar))
				{
					throw new OwaSegmentationException("Calendar is disabled");
				}
				this.optionsControl = new CalendarOptions(base.OwaContext, base.Response.Output);
				break;
			case "Oof":
				this.optionsControl = new OofOptions(base.OwaContext, base.Response.Output);
				break;
			case "ChangePassword":
				if (!base.UserContext.IsFeatureEnabled(Feature.ChangePassword))
				{
					throw new OwaSegmentationException("Change password is disabled");
				}
				this.optionsControl = new ChangePassword(base.OwaContext, base.Response.Output);
				break;
			case "General":
				this.optionsControl = new GeneralOptions(base.OwaContext, base.Response.Output);
				break;
			case "About":
				this.optionsControl = new AboutOptions(base.OwaContext, base.Response.Output);
				break;
			case "Eas":
				if (!base.UserContext.IsMobileSyncEnabled())
				{
					throw new OwaSegmentationException("Eas is disabled");
				}
				this.optionsControl = new MobileOptions(base.OwaContext, base.Response.Output);
				break;
			case "":
				this.optionsControl = new MessagingOptions(base.OwaContext, base.Response.Output);
				break;
			}
			if (this.optionsControl == null)
			{
				throw new OwaInvalidRequestException("Invalid application type querystring parameter");
			}
		}

		// Token: 0x04000439 RID: 1081
		internal const string About = "About";

		// Token: 0x0400043A RID: 1082
		internal const string Calendar = "Calendar";

		// Token: 0x0400043B RID: 1083
		internal const string ChangePassword = "ChangePassword";

		// Token: 0x0400043C RID: 1084
		internal const string General = "General";

		// Token: 0x0400043D RID: 1085
		internal const string JunkEmail = "JunkEmail";

		// Token: 0x0400043E RID: 1086
		internal const string Messaging = "Messaging";

		// Token: 0x0400043F RID: 1087
		internal const string Mobile = "Eas";

		// Token: 0x04000440 RID: 1088
		internal const string Oof = "Oof";

		// Token: 0x04000441 RID: 1089
		internal const string Regional = "Regional";

		// Token: 0x04000442 RID: 1090
		private string applicationElement;

		// Token: 0x04000443 RID: 1091
		private string type;

		// Token: 0x04000444 RID: 1092
		protected OptionsBase optionsControl;
	}
}
