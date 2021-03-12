using System;
using System.ComponentModel;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200061D RID: 1565
	[ToolboxData("<{0}:NavigationHelpControl runat=\"server\" />")]
	public class NavigationHelpControl : DropDownButton
	{
		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x000D1DE3 File Offset: 0x000CFFE3
		// (set) Token: 0x06004576 RID: 17782 RVA: 0x000D1DEB File Offset: 0x000CFFEB
		[DefaultValue(false)]
		public bool IsInCrossPremise { get; set; }

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x000D1DF4 File Offset: 0x000CFFF4
		// (set) Token: 0x06004578 RID: 17784 RVA: 0x000D1DFC File Offset: 0x000CFFFC
		[DefaultValue(true)]
		public bool InAdminUI
		{
			get
			{
				return this.inAdminUI;
			}
			set
			{
				if (this.inAdminUI != value)
				{
					this.inAdminUI = value;
					string helpId = value ? EACHelpId.Default.ToString() : OptionsHelpId.OwaOptionsDefault.ToString();
					string arg = HttpUtility.JavaScriptStringEncode(HelpUtil.BuildEhcHref(helpId));
					Command command = base.DropDownCommand.Commands.FindCommandByName("ContextualHelp");
					if (command != null)
					{
						command.OnClientClick = string.Format("PopupWindowManager.showContextualHelp('{0}', {1});", arg, this.InAdminUI ? "false" : "true");
					}
					Command command2 = base.DropDownCommand.Commands.FindCommandByName("CmdletLogging");
					if (command2 != null && !value)
					{
						command2.Visible = false;
					}
				}
			}
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000D1EA8 File Offset: 0x000D00A8
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			DropDownCommand dropDownCommand = base.DropDownCommand;
			dropDownCommand.AllowAddSubCommandIcon = true;
			dropDownCommand.Name = "Help";
			dropDownCommand.DefaultCommandName = "ContextualHelp";
			dropDownCommand.Text = string.Empty;
			dropDownCommand.ImageAltText = Strings.Help;
			dropDownCommand.ImageId = CommandSprite.SpriteId.HelpCommand;
			string arg = HttpUtility.JavaScriptStringEncode(HelpUtil.BuildEhcHref(EACHelpId.Default.ToString()));
			Command command = new Command();
			command.Name = "ContextualHelp";
			command.Text = Strings.Help;
			command.OnClientClick = string.Format("PopupWindowManager.showContextualHelp('{0}', {1});", arg, this.InAdminUI ? "false" : "true");
			dropDownCommand.Commands.Add(command);
			Command command2 = new Command();
			command2.Name = "FVA";
			command2.Text = Strings.FVAToggleText;
			command2.ClientCommandHandler = "FVAHelpEnabledToggleCommandHandler";
			dropDownCommand.Commands.Add(command2);
			Command command3 = new Command();
			command3.Name = "PerformanceConsole";
			command3.Visible = (this.IsInCrossPremise || StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["ShowPerformanceConsole"]));
			command3.Text = Strings.PerformanceConsole;
			command3.ClientCommandHandler = "ShowPerfConsoleCommandHandler";
			dropDownCommand.Commands.Add(command3);
			Command command4 = new Command();
			command4.Name = "CmdletLogging";
			command4.Text = Strings.CmdLogButtonText;
			if (this.IsInCrossPremise)
			{
				command4.ClientCommandHandler = "HybridCmdletLoggingCommandHandler";
			}
			else
			{
				command4.Visible = EacFlightUtility.GetSnapshotForCurrentUser().Eac.CmdletLogging.Enabled;
				command4.OnClientClick = "CmdletLoggingNavHelper.OpenCmdletLoggingWindow('CmdletLogging');";
			}
			dropDownCommand.Commands.Add(command4);
			if (Util.IsDataCenter)
			{
				Command command5 = new Command();
				command5.Name = "Community";
				command5.Text = Strings.Community;
				command5.OnClientClick = string.Format("PopupWindowManager.showHelpClient('{0}');", HttpUtility.HtmlEncode(HelpUtil.BuildCommunitySiteHref()));
				dropDownCommand.Commands.Add(command5);
			}
			string text = HelpUtil.BuildPrivacyStatmentHref();
			if (!string.IsNullOrEmpty(text))
			{
				Command command6 = new Command();
				command6.Name = "Privacy";
				command6.Text = Strings.Privacy;
				command6.OnClientClick = string.Format("PopupWindowManager.showHelpClient('{0}');", HttpUtility.HtmlEncode(text));
				dropDownCommand.Commands.Add(command6);
			}
			Command command7 = new Command();
			command7.Name = "Copyright";
			command7.Text = Strings.CopyRight;
			command7.OnClientClick = string.Format("PopupWindowManager.showHelpClient('{0}');", "http://go.microsoft.com/fwlink/p/?LinkId=256676");
			dropDownCommand.Commands.Add(command7);
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x000D2178 File Offset: 0x000D0378
		public NavigationHelpControl() : base(HtmlTextWriterTag.Span)
		{
		}

		// Token: 0x04002E93 RID: 11923
		private const string ContextualHelpName = "ContextualHelp";

		// Token: 0x04002E94 RID: 11924
		private const string CmdletLoggingName = "CmdletLogging";

		// Token: 0x04002E95 RID: 11925
		private const string CopyRightName = "Copyright";

		// Token: 0x04002E96 RID: 11926
		private bool inAdminUI = true;
	}
}
