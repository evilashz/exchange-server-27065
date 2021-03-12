using System;
using System.Globalization;
using System.Web;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000210 RID: 528
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Navigation.js")]
	public class HybridPage : EcpPage
	{
		// Token: 0x060026EB RID: 9963 RVA: 0x0007979C File Offset: 0x0007799C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.LoadUrlParameters();
			int num = (!string.IsNullOrEmpty(this.onPremiseFeatures)) ? this.onPremiseFeatures.Length : 0;
			char c = (num > 1) ? this.onPremiseFeatures[1] : '0';
			string userFeatureAtCurrentOrg = CrossPremiseUtil.UserFeatureAtCurrentOrg;
			this.crossPremise.Feature = string.Format("0{0}-{1}", c, userFeatureAtCurrentOrg);
			this.crossPremise.LogoutHelperPage = string.Format("https://{0}/ecp/hybridlogouthelper.aspx?xprs={1}", this.onPremiseServer, this.Context.GetRequestUrl().Host);
			this.hasHelpdesk = (c == '1' || userFeatureAtCurrentOrg[1] == '1');
			this.BuildNameDropDown();
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00079854 File Offset: 0x00077A54
		protected void RenderIFrames()
		{
			this.LoadUrlParameters();
			string ietfLanguageTag = CultureInfo.CurrentCulture.IetfLanguageTag;
			bool flag = RbacPrincipal.Current.IsFederatedUser();
			if (!string.IsNullOrEmpty(this.onPremiseServer))
			{
				base.Response.Output.WriteLine("<iframe src='https://{0}/ecp/?xprs={1}{5}&p={2}&mkt={3}&topnav=0&exsvurl=1' class='hw100' id='onprem' style='display:{4}' frameborder='0'></iframe>", new object[]
				{
					this.onPremiseServer,
					this.Context.GetRequestUrl().Host,
					string.IsNullOrEmpty(this.onPremiseStartPageId) ? string.Empty : HttpUtility.UrlEncode(this.onPremiseStartPageId),
					ietfLanguageTag,
					this.crossPremise.OnPremiseFrameVisible ? "block" : "none",
					flag ? "&cross=1" : string.Empty
				});
			}
			base.Response.Output.WriteLine("<iframe src='default.aspx?xprs={0}{4}&p={1}&mkt={2}&topnav=0&exsvurl=1' class='hw100' id='cloud' style='display:{3}' frameborder='0'></iframe>", new object[]
			{
				this.onPremiseServer,
				string.IsNullOrEmpty(this.cloudStartPageId) ? string.Empty : HttpUtility.UrlEncode(this.cloudStartPageId),
				ietfLanguageTag,
				this.crossPremise.OnPremiseFrameVisible ? "none" : "block",
				flag ? "&cross=1" : string.Empty
			});
			base.Response.Output.WriteLine("<iframe src='' id='logouthelper' style='display:none' frameborder='0'></iframe>");
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000799AC File Offset: 0x00077BAC
		private void LoadUrlParameters()
		{
			if (!this.urlParamLoaded)
			{
				this.onPremiseServer = base.Request.QueryString["xprs"];
				this.onPremiseFeatures = base.Request.QueryString["xprf"];
				this.onPremiseStartPageId = base.Request.QueryString["op"];
				this.cloudStartPageId = base.Request.QueryString["cp"];
				string text = null;
				if (string.IsNullOrEmpty(this.onPremiseServer))
				{
					text = "xprs";
				}
				else if (string.IsNullOrEmpty(this.onPremiseFeatures))
				{
					text = "xprf";
				}
				if (!string.IsNullOrEmpty(text))
				{
					throw new BadQueryParameterException(text);
				}
				string a = base.Request.QueryString["ov"];
				this.crossPremise.OnPremiseFrameVisible = (a == "1" && !string.IsNullOrEmpty(this.onPremiseServer));
				this.urlParamLoaded = true;
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00079AAF File Offset: 0x00077CAF
		protected void RenderCssLinks()
		{
			CssFiles.RenderCssLinks(this, _Default.NavigationCssFiles);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00079ABC File Offset: 0x00077CBC
		protected void RenderTopNav()
		{
			this.LoadUrlParameters();
			base.Response.Output.Write("<div class='topNav NavigationSprite topNavO365Icon Office365Icon'></div>");
			base.Response.Output.Write("<a href='#' id='{0}' class='topNav {2}' onclick=\"return CrossPremise.NavTo('{0}');\" title='{1}'><span class='topLeftNav'>{1}</span></a>", "enterprise", Strings.Enterprise, this.crossPremise.OnPremiseFrameVisible ? "topNavSelected" : string.Empty);
			base.Response.Output.Write("<a href='#' id='{0}' class='topNav {2}' onclick=\"return CrossPremise.NavTo('{0}');\" title='{1}'><span class='topLeftNav'>{1}</span></a>", "office365", Strings.Office365, this.crossPremise.OnPremiseFrameVisible ? string.Empty : "topNavSelected");
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00079B60 File Offset: 0x00077D60
		private void BuildNameDropDown()
		{
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			DropDownCommand dropDownCommand = this.nameDropDown.DropDownCommand;
			dropDownCommand.Name = "UserName";
			dropDownCommand.Text = rbacPrincipal.Name;
			if (this.hasHelpdesk)
			{
				dropDownCommand.Commands.Add(new Command
				{
					Name = "Helpdesk",
					Text = Strings.EntryOnBehalfOf,
					OnClientClick = "CrossPremise.NavTo('helpdesk');"
				});
			}
			if (rbacPrincipal.IsInRole("MailboxFullAccess") && !rbacPrincipal.IsInRole("DelegatedAdmin") && !rbacPrincipal.IsInRole("ByoidAdmin"))
			{
				if (dropDownCommand.Commands.Count > 0)
				{
					dropDownCommand.Commands.Add(new SeparatorCommand());
				}
				dropDownCommand.Commands.Add(new Command
				{
					Name = "SignOff",
					Text = Strings.SignOff,
					OnClientClick = "CrossPremise.NavTo('dualLogout');"
				});
			}
		}

		// Token: 0x04001FAC RID: 8108
		private const string TopNavFormat = "<a href='#' id='{0}' class='topNav {2}' onclick=\"return CrossPremise.NavTo('{0}');\" title='{1}'><span class='topLeftNav'>{1}</span></a>";

		// Token: 0x04001FAD RID: 8109
		private string onPremiseServer;

		// Token: 0x04001FAE RID: 8110
		private string onPremiseFeatures;

		// Token: 0x04001FAF RID: 8111
		private string onPremiseStartPageId;

		// Token: 0x04001FB0 RID: 8112
		private string cloudStartPageId;

		// Token: 0x04001FB1 RID: 8113
		private bool urlParamLoaded;

		// Token: 0x04001FB2 RID: 8114
		private bool hasHelpdesk;

		// Token: 0x04001FB3 RID: 8115
		protected CrossPremise crossPremise;

		// Token: 0x04001FB4 RID: 8116
		protected DropDownButton nameDropDown;
	}
}
