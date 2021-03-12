using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001A2 RID: 418
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class _Default : EcpPage, IThemable
	{
		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x0006B57B File Offset: 0x0006977B
		public bool HasHybridParameter
		{
			get
			{
				return !string.IsNullOrEmpty(base.Request.QueryString["xprs"]);
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x0006B59C File Offset: 0x0006979C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (string.Equals(base.Request.QueryString["flight"], "geminishellux", StringComparison.OrdinalIgnoreCase))
			{
				_Default.SetCookie(this.Context, "flight", "geminishellux", null);
			}
			if (!Util.IsDataCenter && this.HasHybridParameter)
			{
				base.Response.Headers.Remove("X-Frame-Options");
			}
			string text = base.Request.QueryString["xsysprobeid"];
			if (!string.IsNullOrEmpty(text))
			{
				Guid guid;
				SystemProbe.ActivityId = (Guid.TryParse(text.ToString(), out guid) ? guid : Guid.Empty);
				if (SystemProbe.ActivityId != Guid.Empty)
				{
					_Default.SetCookie(this.Context, "xsysprobeid", text, null);
				}
			}
			this.navigation.NavigationTree = this.CreateNavTree();
			this.navigation.CloudServer = this.GetCloudServer();
			this.navigation.ServerVersion = Util.ApplicationVersion;
			this.navigation.HasHybridParameter = this.HasHybridParameter;
			this.InitFeatureSetAndStartPage();
			this.InitTopNav();
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0006B6B8 File Offset: 0x000698B8
		private string GetCloudServer()
		{
			string text = string.Empty;
			if (!Util.IsDataCenter)
			{
				text = OrganizationCache.CrossPremiseServer;
				if (string.IsNullOrEmpty(text))
				{
					text = base.Request.QueryString["xprs"];
				}
			}
			return text;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0006B6F8 File Offset: 0x000698F8
		private static void SetCookie(HttpContext httpContext, string cookieName, string cookieValue, string cookieDomain)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.HttpOnly = true;
			httpCookie.Path = "/";
			httpCookie.Value = cookieValue;
			if (cookieDomain != null)
			{
				httpCookie.Domain = cookieDomain;
			}
			httpContext.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0006B740 File Offset: 0x00069940
		private void InitTopNav()
		{
			string text = base.Request.QueryString["topnav"];
			if (!string.IsNullOrEmpty(text))
			{
				int num;
				if (!int.TryParse(text, out num) || num < 0 || num > 3)
				{
					num = 1;
				}
				this.topNavType = (_Default.TopNav)num;
			}
			else
			{
				this.topNavType = (RbacPrincipal.Current.IsInRole("MailboxFullAccess") ? (Util.IsDataCenter ? _Default.TopNav.O365GNav : _Default.TopNav.Metro) : _Default.TopNav.NoTopNav);
			}
			if (this.topNavType == _Default.TopNav.Metro)
			{
				this.BuildNameDropDown();
			}
			else
			{
				this.helpControl.Visible = false;
				this.nameDropDown.Visible = false;
			}
			if (this.topNavType == _Default.TopNav.O365GNav || this.topNavType == _Default.TopNav.O365GNavFallback)
			{
				this.navBarClient = NavBarClientBase.Create(this.showAdminFeatures.Value, this.topNavType == _Default.TopNav.O365GNavFallback, false);
				if (this.navBarClient != null)
				{
					this.navBarClient.PrepareNavBarPack();
				}
			}
			if (this.notificationTemplate != null)
			{
				this.notificationTemplate.Visible = this.showAdminFeatures.Value;
			}
			if (this.ntfIcon != null)
			{
				this.ntfIcon.Visible = (this.showAdminFeatures.Value && this.topNavType == _Default.TopNav.Metro);
			}
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0006B868 File Offset: 0x00069A68
		protected void RenderAlertBarBegin()
		{
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			string text = null;
			string text2 = null;
			if (!rbacPrincipal.IsInRole("MailboxFullAccess"))
			{
				string logonUser = HttpUtility.HtmlEncode((rbacPrincipal.IsInRole("Enterprise") && rbacPrincipal.RbacConfiguration.SecurityAccessToken != null && rbacPrincipal.RbacConfiguration.SecurityAccessToken.LogonName != null && rbacPrincipal.RbacConfiguration.Impersonated) ? rbacPrincipal.RbacConfiguration.SecurityAccessToken.LogonName : rbacPrincipal.RbacConfiguration.LogonUserDisplayName);
				string accessedUser = HttpUtility.HtmlEncode(rbacPrincipal.RbacConfiguration.ExecutingUserDisplayName);
				text = Strings.OnbehalfOfAlert("<strong>", logonUser, "</strong>", accessedUser);
				text2 = Strings.OnbehalfOfAlert(string.Empty, logonUser, string.Empty, accessedUser);
			}
			else if (rbacPrincipal.IsInRole("DelegatedAdmin") || rbacPrincipal.IsInRole("ByoidAdmin"))
			{
				string targetTenant = HttpContext.Current.GetTargetTenant();
				text = Strings.ManageOtherTenantAlert("<strong>", targetTenant, "</strong>");
				text2 = Strings.ManageOtherTenantAlert(string.Empty, targetTenant, string.Empty);
			}
			if (text != null)
			{
				this.renderAlertBar = true;
				string cssClass = NavigationSprite.GetCssClass(NavigationSprite.SpriteId.EsoBarEdge);
				base.Response.Output.Write("<div id=\"EsoBar\" class=\"{0}\"><img id=\"EsoBarIcon\" class=\"{1}\" src=\"{6}\" alt=\"\" /><div id=\"EsoBarMsg\" title=\"{2}\">{3}</div><img id=\"EsoBarLeftEdge\" class=\"{4}\" src=\"{6}\" alt=\"\"/><img id=\"EsoBarRightEdge\" class=\"{5}\" src=\"{6}\" alt=\"\"></div></div><div id=\"EsoNavWrap\">", new object[]
				{
					HorizontalSprite.GetCssClass(HorizontalSprite.SpriteId.EsoBar),
					CommonSprite.GetCssClass(CommonSprite.SpriteId.Information),
					text2,
					text,
					cssClass,
					cssClass,
					Util.GetSpriteImageSrc(this)
				});
			}
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0006B9E9 File Offset: 0x00069BE9
		protected void RenderAlertBarClose()
		{
			if (this.renderAlertBar)
			{
				base.Response.Output.Write("</div>");
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0006BA08 File Offset: 0x00069C08
		protected void RenderTopNavBegin()
		{
			switch (this.topNavType)
			{
			case _Default.TopNav.NoTopNav:
				base.Response.Output.Write("<div id=\"topNavZone\" role=\"banner\" umc-topregion=\"true\" class=\"{0}\">", "hidden");
				return;
			case _Default.TopNav.O365GNav:
			case _Default.TopNav.O365GNavFallback:
				base.Response.Output.Write("<div id=\"topNavZone\" role=\"banner\" umc-topregion=\"true\" class=\"{0}\">", "o365TopNav");
				this.RenderO365TopNav();
				return;
			}
			base.Response.Output.Write("<div id=\"topNavZone\" role=\"banner\" umc-topregion=\"true\" class=\"{0}\">", "defaultTopNav");
			this.RenderMetroTopNav();
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0006BA90 File Offset: 0x00069C90
		private void RenderMetroTopNav()
		{
			if (this.showAdminFeatures.Value && RbacPrincipal.Current.IsInRole("ControlPanelAdmin"))
			{
				if (!Util.IsPartnerHostedOnly)
				{
					base.Response.Output.Write("<div class='topNav topNavO365Icon NavigationSprite Office365Icon'></div>");
				}
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Eac.Office365DIcon.Enabled)
				{
					base.Response.Output.Write("<a href='#' class='topNav topNavSelected' id='enterprise' tabindex='-1' title='{0}'><span class='topLeftNav'>{0}</span></a>", Strings.Office365D);
					return;
				}
				if (!Util.IsDataCenter)
				{
					base.Response.Output.Write("<a href='#' class='topNav topNavSelected' id='enterprise' tabindex='-1' title='{0}'><span class='topLeftNav'>{0}</span></a>", Strings.Enterprise);
					if (OrganizationCache.EntHasTargetDeliveryDomain && OrganizationCache.EntHasServiceInstance)
					{
						base.Response.Output.Write("<a href='#' id='{0}' class='topNav' onclick=\"return JumpTo('{1}',false, '{2}');\" title='{3}'><span class='topLeftNav'>{3}</span></a>", new object[]
						{
							"office365",
							"office365",
							CrossPremiseUtil.GetLinkToCrossPremise(this.Context, base.Request),
							Strings.Office365
						});
						return;
					}
					base.Response.Output.Write("<a href='{1}' id='{0}' class='topNav' target='_blank' title='{2}'><span class='topLeftNav'>{2}</span></a>", "office365", "http://go.microsoft.com/fwlink/p/?LinkId=258351", Strings.Office365);
					return;
				}
				else
				{
					if (Util.IsMicrosoftHostedOnly)
					{
						base.Response.Output.Write("<a href='#' class='topNav topNavSelected' id='enterprise' tabindex='-1' title='{0}'><span class='topLeftNav'>{0}</span></a>", Strings.Office365);
						return;
					}
					if (Util.IsPartnerHostedOnly)
					{
						base.Response.Output.Write("<a href='#' class='topNav topNavSelected' id='enterprise' tabindex='-1' title='{0}'><span class='topLeftNav'>{0}</span></a>", string.Empty);
						return;
					}
				}
			}
			else
			{
				base.Response.Output.Write("<div class=\"topLeftNav NavigationSprite OwaBrand\"></div>");
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0006BC2C File Offset: 0x00069E2C
		private void RenderO365TopNav()
		{
			if (this.navBarClient != null)
			{
				NavBarPack navBarPack = this.navBarClient.GetNavBarPack();
				if (navBarPack != null)
				{
					string str = "NavBar.Create(" + navBarPack.ToJsonString(null) + ");";
					string script = "Sys.Application.add_init(function(){" + str + "});";
					base.ClientScript.RegisterStartupScript(base.GetType(), "NavBarInfo", script, true);
				}
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0006BC90 File Offset: 0x00069E90
		protected void RenderFooterBar()
		{
			if (this.topNavType == _Default.TopNav.O365GNav || this.topNavType == _Default.TopNav.O365GNavFallback)
			{
				base.Response.Output.Write("<div id=\"footerBar\" class=\"footerBar\"></div>");
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0006BCB9 File Offset: 0x00069EB9
		protected void RenderTopNavEnd()
		{
			base.Response.Output.Write("</div>");
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0006BCD0 File Offset: 0x00069ED0
		protected void RenderTitleBar()
		{
			if (this.showAdminFeatures.Value)
			{
				base.Response.Output.Write("<div id=\"titleBar\" class=\" titleBar {0}\" ><div class=\"mainHeader\"><span>{1}</span></div></div>", (this.topNavType == _Default.TopNav.NoTopNav) ? "noTopNavTitleBar" : string.Empty, Util.IsDataCenter ? ClientStrings.DataCenterMainHeader : ClientStrings.EnterpriseMainHeader);
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0006BD28 File Offset: 0x00069F28
		protected void RenderMiddleNav()
		{
			switch (this.topNavType)
			{
			case _Default.TopNav.NoTopNav:
				return;
			case _Default.TopNav.O365GNav:
			case _Default.TopNav.O365GNavFallback:
				if (NavigationUtil.ShouldRenderOwaLink(RbacPrincipal.Current, this.showAdminFeatures.Value) && !string.IsNullOrWhiteSpace(HttpContext.Current.GetOwaNavigationParameter()))
				{
					base.Response.Output.Write("<div id=\"middleNavZone\" class=\"{3}\" ><div class=\"middleLeftNav\"><a id=\"returnToOWA\" onclick=\"return JumpTo('{2}', true);\" title=\"{0}\" href=\"#\"><img id=\"imgRetrunToOWA\" class=\"NavigationSprite ReturnToOWA\" src='{1}' alt=\"{0}\" title=\"{0}\"/></a></div></div>", new object[]
					{
						OwaOptionStrings.ReturnToOWA,
						Util.GetSpriteImageSrc(this),
						HttpUtility.HtmlEncode(HttpUtility.JavaScriptStringEncode(EcpUrl.GetOwaNavigateBackUrl())),
						"o365MiddleNav"
					});
					return;
				}
				return;
			}
			if ((!this.showAdminFeatures.Value || !RbacPrincipal.Current.IsInRole("ControlPanelAdmin")) && !string.IsNullOrWhiteSpace(HttpContext.Current.GetOwaNavigationParameter()))
			{
				base.Response.Output.Write("<div id=\"middleNavZone\" class=\"{3}\" ><div class=\"middleLeftNav\"><a id=\"returnToOWA\" onclick=\"return JumpTo('{2}', true);\" title=\"{0}\" href=\"#\"><img id=\"imgRetrunToOWA\" class=\"NavigationSprite ReturnToOWA\" src='{1}' alt=\"{0}\" title=\"{0}\"/></a></div></div>", new object[]
				{
					OwaOptionStrings.ReturnToOWA,
					Util.GetSpriteImageSrc(this),
					HttpUtility.HtmlEncode(HttpUtility.JavaScriptStringEncode(EcpUrl.GetOwaNavigateBackUrl())),
					"defaultMiddleNav"
				});
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0006BE4C File Offset: 0x0006A04C
		protected void RenderMainNavBegin()
		{
			string arg = string.Empty;
			switch (this.topNavType)
			{
			case _Default.TopNav.NoTopNav:
				arg = "noTopNavMainNav";
				goto IL_3B;
			case _Default.TopNav.O365GNav:
			case _Default.TopNav.O365GNavFallback:
				arg = "o365MainNav";
				goto IL_3B;
			}
			arg = "defaultMainNav";
			IL_3B:
			base.Response.Output.Write("<div id=\"MainNav\" class=\"{0}\">", arg);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0006BEAA File Offset: 0x0006A0AA
		protected void RenderMainNavEnd()
		{
			base.Response.Output.Write("</div>");
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0006BEC4 File Offset: 0x0006A0C4
		private void BuildNameDropDown()
		{
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			DropDownCommand dropDownCommand = this.nameDropDown.DropDownCommand;
			dropDownCommand.Name = "UserName";
			dropDownCommand.Text = rbacPrincipal.Name;
			if (NavigationUtil.ShouldRenderOwaLink(rbacPrincipal, this.showAdminFeatures.Value))
			{
				dropDownCommand.Commands.Add(new Command
				{
					Name = "MailLnk",
					Text = Strings.MyMail,
					OnClientClick = "JumpTo('" + EcpUrl.OwaVDir + "', true);"
				});
			}
			if (this.HasAccessTo("helpdesk"))
			{
				dropDownCommand.Commands.Add(new Command
				{
					Name = "Helpdesk",
					Text = Strings.EntryOnBehalfOf,
					OnClientClick = "JumpTo('helpdesk');"
				});
			}
			if (NavigationUtil.ShouldRenderLogoutLink(rbacPrincipal))
			{
				if (dropDownCommand.Commands.Count > 0)
				{
					dropDownCommand.Commands.Add(new SeparatorCommand());
				}
				dropDownCommand.Commands.Add(new Command
				{
					Name = "SignOff",
					Text = Strings.SignOff,
					OnClientClick = "Navigation.SignOut();"
				});
			}
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0006BFFC File Offset: 0x0006A1FC
		private bool HasAccessTo(string topNavId)
		{
			bool result = false;
			foreach (NavigationTreeNode navigationTreeNode in this.navigation.NavigationTree.Children)
			{
				if (navigationTreeNode.ID == topNavId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0006C068 File Offset: 0x0006A268
		protected void UpdateNavZoneHtml(int level)
		{
			if (level == 1 && !this.showAdminFeatures.Value)
			{
				base.Response.Output.Write(string.Format("<div id=\"priNavHeader\"><span>{0}</span></div>", OwaOptionStrings.Options));
			}
			NavigationTreeNode navigationTree = this.navigation.NavigationTree;
			string format = _Default.itemFormats[level];
			string text = _Default.selectedClasses[level];
			string text2 = _Default.normalClasses[level];
			NavigationTreeNode selectedNode = this.GetSelectedNode(level - 1, navigationTree);
			if (selectedNode != null && selectedNode.Children.Count > 0)
			{
				int selected = selectedNode.Selected;
				base.Response.Output.Write(_Default.startFormats[level]);
				for (int i = 0; i < selectedNode.Children.Count; i++)
				{
					NavigationTreeNode navigationTreeNode = selectedNode.Children[i];
					if (string.IsNullOrEmpty(navigationTreeNode.HybridRole))
					{
						bool flag = i == selected;
						string text3 = flag ? text : text2;
						string text4 = flag ? "true" : "false";
						string str = " " + ((level == 1) ? ClientStrings.PrimaryNavigation : ClientStrings.SecondaryNavigation);
						base.Response.Output.Write(format, new object[]
						{
							navigationTreeNode.ID,
							text3,
							navigationTreeNode.Title,
							text4,
							navigationTreeNode.Title + str
						});
					}
				}
				base.Response.Output.Write(_Default.endFormats[level]);
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0006C1FE File Offset: 0x0006A3FE
		protected void RenderCssLinks()
		{
			CssFiles.RenderCssLinks(this, _Default.NavigationCssFiles);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0006C20C File Offset: 0x0006A40C
		private NavigationTreeNode GetSelectedNode(int level, NavigationTreeNode navTree)
		{
			NavigationTreeNode navigationTreeNode = navTree;
			for (int i = 0; i <= level; i++)
			{
				if (navigationTreeNode.Children.Count == 0)
				{
					return null;
				}
				navigationTreeNode = navigationTreeNode.Children[navigationTreeNode.Selected];
			}
			return navigationTreeNode;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0006C24C File Offset: 0x0006A44C
		private void InitFeatureSetAndStartPage()
		{
			bool flag = false;
			NavigationTreeNode navigationTree = this.navigation.NavigationTree;
			if (navigationTree != null)
			{
				string text = base.Request.QueryString["p"];
				if (!string.IsNullOrEmpty(text) && text != "helpdesk")
				{
					flag = this.SelectStartPage(navigationTree, EcpUrl.EcpVDir + text, text);
					if (flag)
					{
						this.showAdminFeatures = new bool?(navigationTree.Children[navigationTree.Selected].ID == "myorg");
					}
				}
				else
				{
					this.referrer = base.Request.QueryString["rfr"];
					if (!string.IsNullOrEmpty(this.referrer))
					{
						this.referrer = this.referrer.ToLower();
						string a;
						if ((a = this.referrer) != null)
						{
							if (!(a == "owa") && !(a == "olk"))
							{
								if (a == "admin")
								{
									this.showAdminFeatures = new bool?(true);
								}
							}
							else
							{
								this.showAdminFeatures = new bool?(false);
							}
						}
					}
					if (this.showAdminFeatures == null)
					{
						this.showAdminFeatures = new bool?(RbacPrincipal.Current.IsInRole("ControlPanelAdmin"));
					}
					flag = this.SelectStartPage(navigationTree, null, this.showAdminFeatures.Value ? "myorg" : "myself");
				}
				if (flag)
				{
					if (navigationTree.Children[navigationTree.Selected].ID == "myself")
					{
						this.Context.ThrowIfViewOptionsWithBEParam(FeatureSet.Options);
						for (int i = navigationTree.Children.Count - 1; i > 1; i--)
						{
							navigationTree.Children.RemoveAt(i);
						}
					}
					else if (navigationTree.Children[0].ID == "myself")
					{
						navigationTree.Children.RemoveAt(0);
						navigationTree.Selected = 0;
					}
				}
			}
			if (!flag)
			{
				throw new UrlNotFoundOrNoAccessException(Strings.UrlNotFoundOrNoAccessMessage);
			}
			base.FeatureSet = (this.showAdminFeatures.Value ? FeatureSet.Admin : FeatureSet.Options);
			this.helpControl.InAdminUI = this.showAdminFeatures.Value;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0006C47C File Offset: 0x0006A67C
		private bool SelectStartPage(NavigationTreeNode node, string startPageUrl, string id)
		{
			bool flag = false;
			if (string.Compare(node.ID, id, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(node.Url, startPageUrl, StringComparison.OrdinalIgnoreCase) == 0)
			{
				flag = string.IsNullOrEmpty(node.HybridRole);
			}
			else
			{
				int i = 0;
				while (i < node.Children.Count)
				{
					NavigationTreeNode navigationTreeNode = node.Children[i];
					flag = this.SelectStartPage(navigationTreeNode, startPageUrl, id);
					if (flag)
					{
						node.Selected = i;
						if (!navigationTreeNode.Children.IsNullOrEmpty())
						{
							break;
						}
						string text = base.Request.QueryString["q"];
						if (!string.IsNullOrEmpty(text))
						{
							NavigationTreeNode navigationTreeNode2 = navigationTreeNode;
							navigationTreeNode2.Url = navigationTreeNode2.Url + "?" + text;
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0006C534 File Offset: 0x0006A734
		private NavigationTreeNode CreateNavTree()
		{
			NavigationTreeNode navigationTreeNode = null;
			using (SiteMapDataSource siteMapDataSource = new SiteMapDataSource())
			{
				SiteMapNode rootNode = siteMapDataSource.Provider.RootNode;
				navigationTreeNode = this.CreateDataContract(rootNode);
				if (navigationTreeNode == null || !navigationTreeNode.HasContentPage)
				{
					throw new CmdletAccessDeniedException(Strings.AccessDeniedMessage);
				}
			}
			return navigationTreeNode;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0006C590 File Offset: 0x0006A790
		private NavigationTreeNode CreateDataContract(SiteMapNode sNode)
		{
			NavigationTreeNode navigationTreeNode = null;
			if (sNode != null)
			{
				navigationTreeNode = new NavigationTreeNode(sNode);
				foreach (object obj in sNode.ChildNodes)
				{
					SiteMapNode sNode2 = (SiteMapNode)obj;
					NavigationTreeNode navigationTreeNode2 = this.CreateDataContract(sNode2);
					if (navigationTreeNode2.HasContentPage)
					{
						navigationTreeNode.AddChild(navigationTreeNode2);
					}
				}
				navigationTreeNode.AggregateHybridRole();
			}
			return navigationTreeNode;
		}

		// Token: 0x04001DC7 RID: 7623
		public const string IdEnterprise = "enterprise";

		// Token: 0x04001DC8 RID: 7624
		public const string IdOffice365 = "office365";

		// Token: 0x04001DC9 RID: 7625
		public const string NodeIdMyOrg = "myorg";

		// Token: 0x04001DCA RID: 7626
		public const string NodeIdMyself = "myself";

		// Token: 0x04001DCB RID: 7627
		public const string NodeIdHelpdesk = "helpdesk";

		// Token: 0x04001DCC RID: 7628
		public const string SystemProbeCookie = "xsysprobeid";

		// Token: 0x04001DCD RID: 7629
		private const string AlertBarFormatStart = "<div id=\"EsoBar\" class=\"{0}\"><img id=\"EsoBarIcon\" class=\"{1}\" src=\"{6}\" alt=\"\" /><div id=\"EsoBarMsg\" title=\"{2}\">{3}</div><img id=\"EsoBarLeftEdge\" class=\"{4}\" src=\"{6}\" alt=\"\"/><img id=\"EsoBarRightEdge\" class=\"{5}\" src=\"{6}\" alt=\"\"></div></div><div id=\"EsoNavWrap\">";

		// Token: 0x04001DCE RID: 7630
		private const string AlertBarFormatEnd = "</div>";

		// Token: 0x04001DCF RID: 7631
		private const string TopNavZoneFormat = "<div id=\"topNavZone\" role=\"banner\" umc-topregion=\"true\" class=\"{0}\">";

		// Token: 0x04001DD0 RID: 7632
		private const string TopNavBrandFormat = "<a href='#' class='topNav topNavSelected' id='enterprise' tabindex='-1' title='{0}'><span class='topLeftNav'>{0}</span></a>";

		// Token: 0x04001DD1 RID: 7633
		private const string TopNavO365Format = "<a href='#' id='{0}' class='topNav' onclick=\"return JumpTo('{1}',false, '{2}');\" title='{3}'><span class='topLeftNav'>{3}</span></a>";

		// Token: 0x04001DD2 RID: 7634
		internal const string TopNavAdFormat = "<a href='{1}' id='{0}' class='topNav' target='_blank' title='{2}'><span class='topLeftNav'>{2}</span></a>";

		// Token: 0x04001DD3 RID: 7635
		private const string TopNavOwaOptionFormat = "<div class=\"topLeftNav NavigationSprite OwaBrand\"></div>";

		// Token: 0x04001DD4 RID: 7636
		private const string MiddleNavFormat = "<div id=\"middleNavZone\" class=\"{3}\" ><div class=\"middleLeftNav\"><a id=\"returnToOWA\" onclick=\"return JumpTo('{2}', true);\" title=\"{0}\" href=\"#\"><img id=\"imgRetrunToOWA\" class=\"NavigationSprite ReturnToOWA\" src='{1}' alt=\"{0}\" title=\"{0}\"/></a></div></div>";

		// Token: 0x04001DD5 RID: 7637
		private static string[] startFormats = new string[]
		{
			null,
			"<ul>",
			string.Empty
		};

		// Token: 0x04001DD6 RID: 7638
		private static string[] itemFormats = new string[]
		{
			null,
			"<li id=\"Menu_{0}\" class=\"{1}\"><span><a class=\"priNavLnk\" href=\"#\" aria-expanded=\"{3}\" aria-label=\"{4}\">{2}</a></span></li>",
			"<div id=\"Menu_{0}\" class=\"{1}\"><a class=\"secNavLnk\" href=\"#\" aria-expanded=\"{3}\" aria-label=\"{4}\">{2}</a></div>"
		};

		// Token: 0x04001DD7 RID: 7639
		private static string[] endFormats = new string[]
		{
			null,
			"</ul>",
			string.Empty
		};

		// Token: 0x04001DD8 RID: 7640
		private static string[] selectedClasses = new string[]
		{
			null,
			"priSelected",
			"secNavBtn secSelected"
		};

		// Token: 0x04001DD9 RID: 7641
		private static string[] normalClasses = new string[]
		{
			string.Empty,
			string.Empty,
			"secNavBtn"
		};

		// Token: 0x04001DDA RID: 7642
		internal static readonly string[] NavigationCssFiles = new string[]
		{
			"NavCombine.css"
		};

		// Token: 0x04001DDB RID: 7643
		protected Navigation navigation;

		// Token: 0x04001DDC RID: 7644
		protected Label lblSeparator;

		// Token: 0x04001DDD RID: 7645
		protected HtmlForm mainForm;

		// Token: 0x04001DDE RID: 7646
		protected DropDownButton nameDropDown;

		// Token: 0x04001DDF RID: 7647
		protected Label ntfIcon;

		// Token: 0x04001DE0 RID: 7648
		protected UserControl notificationTemplate;

		// Token: 0x04001DE1 RID: 7649
		protected NavigationHelpControl helpControl;

		// Token: 0x04001DE2 RID: 7650
		private bool? showAdminFeatures;

		// Token: 0x04001DE3 RID: 7651
		private bool renderAlertBar;

		// Token: 0x04001DE4 RID: 7652
		private _Default.TopNav topNavType;

		// Token: 0x04001DE5 RID: 7653
		private string referrer;

		// Token: 0x04001DE6 RID: 7654
		private NavBarClientBase navBarClient;

		// Token: 0x020001A3 RID: 419
		private enum TopNav
		{
			// Token: 0x04001DE8 RID: 7656
			NoTopNav,
			// Token: 0x04001DE9 RID: 7657
			Metro,
			// Token: 0x04001DEA RID: 7658
			O365GNav,
			// Token: 0x04001DEB RID: 7659
			O365GNavFallback,
			// Token: 0x04001DEC RID: 7660
			MaxValue = 3
		}
	}
}
