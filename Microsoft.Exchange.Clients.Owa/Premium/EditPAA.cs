using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000453 RID: 1107
	public class EditPAA : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000E4440 File Offset: 0x000E2640
		protected static int UMCallStateIdle
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x000E4443 File Offset: 0x000E2643
		protected static int UMCallStateConnecting
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000E4446 File Offset: 0x000E2646
		protected static int UMCallStateConnected
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x000E4449 File Offset: 0x000E2649
		protected static int UMCallStateDisconnected
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x000E444C File Offset: 0x000E264C
		protected bool EnabledForOutDialing
		{
			get
			{
				return this.enabledForOutdialing;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x000E4454 File Offset: 0x000E2654
		protected bool HasMoreThenOneUMExtension
		{
			get
			{
				return this.extensions != null && this.extensions.Count > 1;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000E446E File Offset: 0x000E266E
		protected bool PaaHasExtension
		{
			get
			{
				return this.paa != null && this.paa.ExtensionList.Count > 0;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x000E448D File Offset: 0x000E268D
		protected bool IsNew
		{
			get
			{
				return this.paa == null;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x000E4498 File Offset: 0x000E2698
		protected bool CanBargeIn
		{
			get
			{
				return this.canBargeIn;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x000E44A0 File Offset: 0x000E26A0
		protected bool IsPersonalAutoAttendantValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x000E44A8 File Offset: 0x000E26A8
		protected bool IsPlayOnPhoneEnabled
		{
			get
			{
				return this.isPlayOnPhoneEnabled;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x000E44B0 File Offset: 0x000E26B0
		protected string PhoneNumber
		{
			get
			{
				string result = null;
				using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
				{
					if (umclientCommon.IsUMEnabled())
					{
						result = umclientCommon.GetUMProperties().PlayOnPhoneDialString;
					}
				}
				return result;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000E4504 File Offset: 0x000E2704
		protected Infobar PaaInfobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000E450C File Offset: 0x000E270C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.infobar = new Infobar();
			if (!base.UserContext.IsFeatureEnabled(Feature.UMIntegration))
			{
				throw new OwaSegmentationException("UM integration feature is disabled");
			}
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsUMEnabled())
				{
					throw new OwaInvalidRequestException("User is not UM enabled");
				}
				this.isPlayOnPhoneEnabled = umclientCommon.IsPlayOnPhoneEnabled();
			}
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				ipaastore.GetUserPermissions(out this.enabledForPersonalAutoAttendant, out this.enabledForOutdialing);
				if (!this.enabledForPersonalAutoAttendant)
				{
					throw new OwaInvalidRequestException("User is not Personal Auto Attendant enabled");
				}
				this.identity = Utilities.GetQueryStringParameter(base.Request, "id", false);
				if (!string.IsNullOrEmpty(this.identity))
				{
					ipaastore.TryGetAutoAttendant(new Guid(Convert.FromBase64String(this.identity)), PAAValidationMode.Full, out this.paa);
					if (this.paa == null)
					{
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					this.canBargeIn = this.paa.EnableBargeIn;
					this.isValid = this.paa.Valid;
				}
				this.extensions = ipaastore.GetExtensionsInPrimaryDialPlan();
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000E4668 File Offset: 0x000E2868
		protected void RenderJavascriptEncodedIdentity()
		{
			if (this.paa != null)
			{
				Utilities.JavascriptEncode(Convert.ToBase64String(this.paa.Identity.ToByteArray()), base.Response.Output);
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000E46A8 File Offset: 0x000E28A8
		protected void RenderButtons()
		{
			base.Response.Write("<div id=\"svCl\" class=\"" + (base.UserContext.IsRtl ? "l" : "r") + "\">");
			RenderingUtilities.RenderButton(base.Response.Output, "btnSv", string.Empty, "clkBtn('save')", LocalizedStrings.GetHtmlEncoded(-224317800));
			base.Response.Write("&nbsp;");
			RenderingUtilities.RenderButton(base.Response.Output, "btnCl", string.Empty, "clkBtn('close')", LocalizedStrings.GetHtmlEncoded(-1936577052));
			base.Response.Write("</div>");
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000E475C File Offset: 0x000E295C
		protected void RenderHelpButton()
		{
			string text = HelpIdsLight.OptionsLight.ToString();
			string empty = string.Empty;
			base.Response.Write("<a id=help class=btnDf name=lnkB target=\"_blank\" href=\"");
			base.Response.Write(Utilities.HtmlEncode(Utilities.BuildEhcHref(text)));
			base.Response.Write("\">");
			UserContextManager.GetUserContext().RenderBaseThemeImage(base.Response.Output, ThemeFileId.Help);
			base.Response.Write("<span id=spnhlpLnk style=\"display:none\">");
			base.Response.Write("./help/default.htm?");
			base.Response.Write(text);
			base.Response.Write((empty.Length != 0) ? ("#" + empty) : string.Empty);
			base.Response.Write("</span></a>");
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000E482C File Offset: 0x000E2A2C
		protected void RenderPlayOnPhoneImage()
		{
			base.Response.Write("<span id=\"popImg\">");
			UserContextManager.GetUserContext().RenderBaseThemeImage(base.Response.Output, ThemeFileId.PlayOnTelephone, null, new object[]
			{
				"id=imgPly"
			});
			base.Response.Write("</span>");
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000E4884 File Offset: 0x000E2A84
		protected void RenderConditionsAndActionsArea()
		{
			this.RenderAreaHeader(-49338867);
			if (this.HasMoreThenOneUMExtension)
			{
				this.RenderContextMenuHeader(1331172, "divCG1");
			}
			this.RenderContextMenuHeader(1795407116, "divCG2");
			this.RenderContextMenuHeader(-1716592511, "divCG3");
			this.RenderContextMenuHeader(988511814, "divCG4");
			this.RenderContextMenuHeader(922453471, "divCG5");
			base.Response.Write("<div id=\"CAsep\"/>");
			this.RenderAreaHeader(1806500630);
			this.RenderContextMenuHeader(-1690555995, "divAG1");
			this.RenderContextMenuHeader(814748729, "divAG2");
			this.RenderContextMenuHeader(498787883, "divAG3");
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000E4940 File Offset: 0x000E2B40
		protected void RenderDescriptionArea()
		{
			string text = "<a id=\"lnk\" href=\"#\">&nbsp;</a>";
			string value = "<span id=\"txt\">&nbsp;</span>";
			base.Response.Write("<div id=\"descCH\" class=\"divDescH\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(1143478553));
			base.Response.Write("</div>");
			this.RenderDescription("divC1", -19776829, true, new string[]
			{
				text
			});
			base.Response.Write("<div id=\"divC2\">");
			this.RenderInvalidImage();
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.DeleteSmall, null, new object[]
			{
				"id=\"del\""
			});
			base.Response.Output.Write("<a id=\"lnk\" href=\"#\">");
			base.Response.Output.Write(LocalizedStrings.GetHtmlEncoded(362731532));
			base.Response.Output.Write("</a> ");
			base.Response.Output.Write(value);
			base.Response.Output.Write("</div>");
			base.Response.Write("<div id=\"divC3\">");
			this.RenderClearImage();
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.DeleteSmall, null, new object[]
			{
				"id=\"del\""
			});
			base.Response.Write("<span id=\"durPrdWH\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(936883093));
			base.Response.Write("&nbsp;<a id=\"lnk0\" href=\"#\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1382797307));
			base.Response.Write("</a></span><span id=\"durPrdOH\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(990264721));
			base.Response.Write("&nbsp;<a id=\"lnk1\" href=\"#\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1382797307));
			base.Response.Write("</a></span><span id=\"durPrdCH\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(1562950183));
			base.Response.Write("&nbsp;<a id=\"lnk2\" href=\"#\"");
			base.Response.Write(" _am =\"");
			Utilities.HtmlEncode(Microsoft.Exchange.Clients.Owa.Core.Culture.AMDesignator, base.Response.Output);
			base.Response.Write("\" _pm =\"");
			Utilities.HtmlEncode(Microsoft.Exchange.Clients.Owa.Core.Culture.PMDesignator, base.Response.Output);
			base.Response.Write("\"><span id=spnSt></span>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(414748875));
			base.Response.Write("<span id=spnEnd></span>,&nbsp;");
			base.Response.Write("<span id=spnDays></span>");
			base.Response.Write("</a></span></div>");
			this.RenderDescription("divC4", -1297963808, new string[]
			{
				text
			});
			this.RenderDescription("divC5", -899712036, new string[0]);
			base.Response.Write("<div id=\"descAH\" class=\"divDescH\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(943062574));
			base.Response.Write("</div>");
			string htmlEncoded = LocalizedStrings.GetHtmlEncoded(-409293743);
			string htmlEncoded2 = LocalizedStrings.GetHtmlEncoded(604913675);
			string htmlEncoded3 = LocalizedStrings.GetHtmlEncoded(685336972);
			string htmlEncoded4 = LocalizedStrings.GetHtmlEncoded(-1845945127);
			for (int i = 1; i <= 9; i++)
			{
				base.Response.Output.Write("<div id=\"divF");
				base.Response.Output.Write(i);
				base.Response.Output.Write("\">");
				this.RenderInvalidImage();
				base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.DeleteSmall, null, new object[]
				{
					"id=\"del\""
				});
				base.Response.Output.Write("<span id=\"desc\">");
				base.Response.Output.Write(htmlEncoded, "<span id=\"txt\">&nbsp;</span>");
				base.Response.Output.Write("</span>");
				base.Response.Output.Write(htmlEncoded2, i);
				base.Response.Output.Write(htmlEncoded4, "<a id=\"lnk\" href=\"#\">", "</a>");
				base.Response.Output.Write("</div>");
				base.Response.Output.Write("<div id=\"divX");
				base.Response.Output.Write(i);
				base.Response.Output.Write("\">");
				this.RenderInvalidImage();
				base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.DeleteSmall, null, new object[]
				{
					"id=\"del\""
				});
				base.Response.Output.Write("<span id=\"desc\">");
				base.Response.Output.Write(htmlEncoded, "<span id=\"txt\">&nbsp;</span>");
				base.Response.Output.Write("</span>");
				base.Response.Output.Write(htmlEncoded2, i);
				base.Response.Output.Write(htmlEncoded3, "<a id=\"lnk\" href=\"#\">&nbsp;</a>");
				base.Response.Output.Write("</div>");
			}
			this.RenderDescription("divA1", -830535408, new string[0]);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000E4EBE File Offset: 0x000E30BE
		protected void RenderTitle()
		{
			if (this.paa != null)
			{
				Utilities.HtmlEncode(this.paa.Name, base.Response.Output);
				return;
			}
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(699355213));
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000E4EFC File Offset: 0x000E30FC
		protected void RenderInitializationScripts()
		{
			this.RenderFactoryInitializationScripts();
			base.Response.Write("g_oPAObj.keys = [\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\",\"8\",\"9\"];\n");
			base.Response.Write("g_oPAObj.id = \"");
			if (this.paa != null)
			{
				Utilities.JavascriptEncode(Convert.ToBase64String(this.paa.Identity.ToByteArray()), base.Response.Output);
			}
			base.Response.Write("\";\n");
			base.Response.Write("g_oPAObj.name = \"");
			if (this.paa != null)
			{
				Utilities.JavascriptEncode(this.paa.Name, base.Response.Output);
			}
			base.Response.Write("\";\n");
			if (this.paa == null)
			{
				this.RenderObjectAddScript("r");
				base.Response.Write("g_oPAObj.add(A);\n");
			}
			if (this.paa != null)
			{
				if (this.paa.ExtensionList.Count > 0)
				{
					base.Response.Write("g_oPAObj.e = g_oPAFac.gt(\"e\").clone();\n");
					foreach (string s in this.paa.ExtensionList)
					{
						base.Response.Write("g_oPAObj.e.sSelExt.push(\"");
						Utilities.JavascriptEncode(s, base.Response.Output);
						base.Response.Write("\");\n");
					}
					string text = null;
					if (this.paa.ExtensionList.ValidationResult != PAAValidationResult.Valid)
					{
						bool flag = true;
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string text2 in this.paa.ExtensionList)
						{
							if (!this.extensions.Contains(text2))
							{
								if (!flag)
								{
									stringBuilder.Append(LocalizedStrings.GetNonEncoded(53305273));
								}
								else
								{
									flag = false;
								}
								stringBuilder.Append(text2);
							}
						}
						text = this.CheckIsValid(this.paa.ExtensionList.ValidationResult, stringBuilder.ToString());
					}
					if (text != null)
					{
						base.Response.Write("g_oPAObj.e.err = \"");
						base.Response.Write(text);
						base.Response.Write("\";\n");
					}
				}
				if (this.paa.TimeOfDay != TimeOfDayEnum.None)
				{
					base.Response.Write("g_oPAObj.d = g_oPAFac.gt(\"d\").clone();\n");
					switch (this.paa.TimeOfDay)
					{
					case TimeOfDayEnum.WorkingHours:
						base.Response.Write("g_oPAObj.d.fW = 1;\n");
						break;
					case TimeOfDayEnum.NonWorkingHours:
						base.Response.Write("g_oPAObj.d.fNW = 1;\n");
						break;
					case TimeOfDayEnum.Custom:
						base.Response.Write("g_oPAObj.d.fCst = 1;\n");
						base.Response.Write("g_oPAObj.d.iSt = ");
						base.Response.Write(this.paa.WorkingPeriod.StartTimeInMinutes);
						base.Response.Write(";\n");
						base.Response.Write("g_oPAObj.d.iEnd = ");
						base.Response.Write(this.paa.WorkingPeriod.EndTimeInMinutes);
						base.Response.Write(";\n");
						base.Response.Write("g_oPAObj.d.eD = ");
						base.Response.Write((int)this.paa.WorkingPeriod.DayOfWeek);
						base.Response.Write(";\n");
						break;
					}
				}
				if (this.paa.FreeBusy != FreeBusyStatusEnum.None)
				{
					base.Response.Write("g_oPAObj.s = g_oPAFac.gt(\"s\").clone();\n");
					base.Response.Write("g_oPAObj.s.fb = ");
					base.Response.Write((int)this.paa.FreeBusy);
					base.Response.Write(";\n");
				}
				if (this.paa.EnableBargeIn)
				{
					base.Response.Write("g_oPAObj.CllrIntrpt = true;\n");
				}
				if (this.paa.OutOfOffice == OutOfOfficeStatusEnum.Oof)
				{
					base.Response.Write("g_oPAObj.OOF = g_oPAFac.gt(\"o\").clone();\n");
				}
				for (int i = 0; i < this.paa.KeyMappingList.Count; i++)
				{
					KeyMappingBase keyMappingBase = this.paa.KeyMappingList.Menu[i];
					this.RenderActionScript(keyMappingBase, EditPAA.ScriptSideActionName(keyMappingBase.KeyMappingType));
				}
				this.RenderCallerIdConditionScript();
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000E5378 File Offset: 0x000E3578
		protected void RenderUMExtensionsMenu()
		{
			int num = 0;
			foreach (string s in this.extensions)
			{
				base.Response.Write("<span><input type=\"checkbox\" id=\"ext");
				base.Response.Write(num);
				base.Response.Write("\" _ext=\"");
				base.Response.Write(s);
				base.Response.Write("\"><label for=\"ext");
				base.Response.Write(num);
				base.Response.Write("\">");
				base.Response.Write(s);
				base.Response.Write("</label></span>");
				num++;
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000E5454 File Offset: 0x000E3654
		protected void RenderScheduleStatusMenu()
		{
			base.Response.Write("<table cellSpacing=\"0\" cellPadding=\"0\">");
			this.RenderScheduleStatusMenuItem("stsfree", FreeBusyStatusEnum.Free);
			this.RenderScheduleStatusMenuItem("ststntv", FreeBusyStatusEnum.Tentative);
			this.RenderScheduleStatusMenuItem("stsbsy", FreeBusyStatusEnum.Busy);
			this.RenderScheduleStatusMenuItem("stsOOF", FreeBusyStatusEnum.OutOfOffice);
			base.Response.Write("</table>");
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000E54B4 File Offset: 0x000E36B4
		protected void RenderPhoneKeyDropDown()
		{
			DropDownList.RenderDropDownList(base.Response.Output, "tblphKeyDD", "1", new DropDownListItem[]
			{
				new DropDownListItem(1, "1"),
				new DropDownListItem(2, "2"),
				new DropDownListItem(3, "3"),
				new DropDownListItem(4, "4"),
				new DropDownListItem(5, "5"),
				new DropDownListItem(6, "6"),
				new DropDownListItem(7, "7"),
				new DropDownListItem(8, "8"),
				new DropDownListItem(9, "9")
			});
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000E5590 File Offset: 0x000E3790
		protected void RenderFindMePhoneDiv(string fieldNumber)
		{
			base.Response.Write("<div id=\"divPh");
			base.Response.Write(fieldNumber);
			base.Response.Write("\">");
			base.Response.Write(fieldNumber);
			base.Response.Write(". ");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(405905554));
			if (base.UserContext.IsRtl)
			{
				base.Response.Write("&#x200F;");
			}
			base.Response.Write("<input class=\"FndMePh\" id=\"inFndMePh");
			base.Response.Write(fieldNumber);
			base.Response.Write("\" type=\"text\" maxlength=\"255\"></input>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-779647650));
			base.Response.Write("<input class=\"FndMeTime\" id=\"inFndMeTime");
			base.Response.Write(fieldNumber);
			base.Response.Write("\" type=\"text\" maxlength=\"2\" value=\"25\"></input>");
			if (base.UserContext.IsRtl)
			{
				base.Response.Write("&#x200F;");
			}
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-460217248));
			base.Response.Write("</div>");
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000E56C8 File Offset: 0x000E38C8
		protected void RenderFindMeMenu()
		{
			base.Response.Write("<div id=\"divFndMeTxt\">");
			base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(1502599395) + "</span>");
			if (base.UserContext.IsRtl)
			{
				base.Response.Write("&#x200F;");
			}
			base.Response.Write("<input class=\"FndMeNm\" id=\"FndMeNm\" type=\"text\" maxlength=\"255\"></input>");
			base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(-1904832844) + "</span><span class=\"spnTblPhKey\">");
			this.RenderPhoneKeyDropDown();
			base.Response.Write("</span>");
			if (base.UserContext.IsRtl)
			{
				base.Response.Write("&#x200F;");
			}
			base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(-1356511266) + "</span>");
			base.Response.Write("</div>");
			this.RenderFindMePhoneDiv("1");
			this.RenderFindMePhoneDiv("2");
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000E57D8 File Offset: 0x000E39D8
		protected void RenderRadioItemAndLabel(string group, string value, string label)
		{
			base.Response.Write("<input class=\"");
			base.Response.Write(group);
			base.Response.Write("\" name=\"");
			base.Response.Write(group);
			base.Response.Write("\" id=\"");
			base.Response.Write(value);
			base.Response.Write("\" type=\"radio\" value=\"");
			base.Response.Write(value);
			base.Response.Write("\">");
			if (!string.IsNullOrEmpty(label))
			{
				base.Response.Write("<label for=\"");
				base.Response.Write(value);
				base.Response.Write("\">");
				base.Response.Write(label);
				base.Response.Write("</label>");
			}
			base.Response.Write("</input>");
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000E58C8 File Offset: 0x000E3AC8
		protected void RenderTransferToRadioButtons()
		{
			base.Response.Write("<table class=rdo><tr><td>");
			if (this.EnabledForOutDialing)
			{
				base.Response.Write("<div id=\"divXfrToPh\">");
				this.RenderRadioItemAndLabel("rdXfrTo", "XfrToPh", LocalizedStrings.GetHtmlEncoded(-1401156882));
				base.Response.Write("</div></td><td nowrap>");
				base.Response.Write("<div>");
				base.Response.Write("<input class=\"txtXfrToPh\" id=\"txtXfrToPh\" type=\"text\" maxlength=\"255\"></input></div>");
				base.Response.Write("</td></tr><tr><td>");
				base.Response.Write("<div id=\"divXfrToRcp\">");
				this.RenderRadioItemAndLabel("rdXfrTo", "XfrToRcp", LocalizedStrings.GetHtmlEncoded(-1729430071));
				base.Response.Write("</div></td>");
			}
			base.Response.Write("<td nowrap>");
			this.RenderRecipientPickerControl();
			base.Response.Write("</td></tr></table>");
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000E59BC File Offset: 0x000E3BBC
		protected void RenderTransferToMenu()
		{
			base.Response.Write("<div id=\"divXfrToTxt\">");
			base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(1502599395) + "</span>");
			if (base.UserContext.IsRtl)
			{
				base.Response.Write("&#x200F;");
			}
			base.Response.Write("<input class=\"XfrToNm\" id=\"XfrToNm\" type=\"text\" maxlength=\"255\"></input>");
			base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(-1904832844) + "</span><span class=\"spnTblPhKey\">");
			this.RenderPhoneKeyDropDown();
			base.Response.Write("</span>");
			if (base.UserContext.IsRtl)
			{
				base.Response.Write("&#x200F;");
			}
			if (this.EnabledForOutDialing)
			{
				base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(2013903913) + "</span>");
			}
			else
			{
				base.Response.Write("<span>" + LocalizedStrings.GetHtmlEncoded(1926682954) + "</span>");
			}
			base.Response.Write("</div>");
			this.RenderTransferToRadioButtons();
			if (this.EnabledForOutDialing)
			{
				base.Response.Write("<div id=\"divXfrToVM\">");
				base.Response.Write("<input type=\"checkbox\" id=\"XfrToVM\"><label id=\"lblXfrToVM\" for=\"XfrToVM\">");
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(620743059));
				base.Response.Write("</label>");
				base.Response.Write("</div>");
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000E5B48 File Offset: 0x000E3D48
		protected void RenderCallerIsMenu()
		{
			base.Response.Write("<div id=\"divCllrIdPh\">");
			base.Response.Write("<input type=\"checkbox\" id=\"chkCllrIdPh\"><label for=\"chkCllrIdPh\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(1344845447));
			base.Response.Write("</label></div>");
			base.Response.Write("<div class=cllrIdPhTxt>");
			base.Response.Write("<input class=\"txtCllrIsPh\" id=\"txtCllrIsPh\" type=\"text\" maxlength=\"255\"></input>");
			base.Response.Write("</div>");
			base.Response.Write("<div id=\"divCllrIdCnt\">");
			base.Response.Write("<input type=\"checkbox\" id=\"chkCllrIdCnt\"><label for=\"chkCllrIdCnt\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1500337747));
			base.Response.Write("</label></div>");
			this.RenderRecipientPickerControl();
			base.Response.Write("<div id=\"divCllrIdCntFld\">");
			base.Response.Write("<input type=\"checkbox\" id=\"chkCllrIdFld\"><label for=\"chkCllrIdFld\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(2078225409));
			base.Response.Write("</label></div>");
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000E5C5C File Offset: 0x000E3E5C
		protected void RenderWorkdayCheckBox(string id, DayOfWeek day)
		{
			base.Response.Write("<input type=\"checkbox\" id=\"" + id + "\"");
			base.Response.Write(" onclick=onClkWD()><label for=\"" + id + "\">");
			base.Response.Write(CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[(int)day] + "</label>");
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000E5CC8 File Offset: 0x000E3EC8
		protected void RenderDuringThisPeriodMenu()
		{
			ExDateTime minValue = ExDateTime.MinValue;
			ExDateTime minValue2 = ExDateTime.MinValue;
			this.SetStartAndEndTime(out minValue, out minValue2);
			base.Response.Write("<table class=collapsed><tr><td id=tdDurW width=20>");
			this.RenderRadioItemAndLabel("rdDur", "DurW", string.Empty);
			base.Response.Write("</td><td colspan=2>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-721178515));
			base.Response.Write("</td></tr><tr><td id=tdDurNonW>");
			this.RenderRadioItemAndLabel("rdDur", "DurNonW", string.Empty);
			base.Response.Write("</td><td colspan=2>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1185794532));
			base.Response.Write("</td></tr><tr><td rowspan=3 valign=top>");
			this.RenderRadioItemAndLabel("rdDur", "DurCust", LocalizedStrings.GetNonEncoded(-1018465893));
			base.Response.Write("</td><td height=25 id=tdFrom>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(2070713093));
			base.Response.Write("</td><td id=tdTo>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(594015424));
			base.Response.Write("</td></tr>");
			base.Response.Write("<tr><td>");
			this.RenderTimeDropdownList(minValue, "divSTime");
			base.Response.Write("</td><td>");
			this.RenderTimeDropdownList(minValue2, "divETime");
			base.Response.Write("</td></tr><tr><td colspan=2>");
			base.Response.Write("<table id=tblWk class=ngry><tr><td>");
			this.RenderWorkdayCheckBox("chkMon", DayOfWeek.Monday);
			base.Response.Write("</td><td>");
			this.RenderWorkdayCheckBox("chkTue", DayOfWeek.Tuesday);
			base.Response.Write("</td><td>");
			this.RenderWorkdayCheckBox("chkWed", DayOfWeek.Wednesday);
			base.Response.Write("</td></tr>");
			base.Response.Write("<tr><td>");
			this.RenderWorkdayCheckBox("chkThu", DayOfWeek.Thursday);
			base.Response.Write("</td><td>");
			this.RenderWorkdayCheckBox("chkFri", DayOfWeek.Friday);
			base.Response.Write("</td><td>");
			this.RenderWorkdayCheckBox("chkSat", DayOfWeek.Saturday);
			base.Response.Write("</td></tr>");
			base.Response.Write("<tr><td>");
			this.RenderWorkdayCheckBox("chkSun", DayOfWeek.Sunday);
			base.Response.Write("</td><td>");
			base.Response.Write("</td><td>");
			base.Response.Write("</td></tr></table>");
			base.Response.Write("</td></tr></table>");
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000E5F70 File Offset: 0x000E4170
		protected void RenderErrorInformationBar()
		{
			base.Response.Write("<div id=");
			base.Response.Write("divErr");
			base.Response.Write(" style=display:none;padding:2><div id=divEIB>");
			base.Response.Write("<table cellpadding=0 cellspacing=0><tr><td id=tdIcon valign=top>");
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.Error2);
			base.Response.Write("</td><td id=tdMsg style=width:100%></td>");
			base.Response.Write("</tr></table></div></div>");
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000E5FF8 File Offset: 0x000E41F8
		private static string ScriptSideActionName(KeyMappingTypeEnum type)
		{
			if (type < KeyMappingTypeEnum.TransferToNumber || type > KeyMappingTypeEnum.FindMe)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			string result = null;
			switch (type)
			{
			case KeyMappingTypeEnum.TransferToNumber:
				result = "x";
				break;
			case KeyMappingTypeEnum.TransferToADContactMailbox:
				result = "x";
				break;
			case KeyMappingTypeEnum.TransferToADContactPhone:
				result = "x";
				break;
			case KeyMappingTypeEnum.TransferToVoicemail:
				result = "r";
				break;
			case KeyMappingTypeEnum.FindMe:
				result = "f";
				break;
			}
			return result;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000E6064 File Offset: 0x000E4264
		private static ADRecipient CreateUnknownADRecipient(string legacyDN)
		{
			return new ADRecipient
			{
				DisplayName = LocalizedStrings.GetNonEncoded(-1626952556),
				LegacyExchangeDN = legacyDN
			};
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000E608F File Offset: 0x000E428F
		private void RenderAreaHeader(Strings.IDs stringID)
		{
			base.Response.Write("<div class=\"dvAH\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(stringID));
			base.Response.Write("</div>");
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000E60C2 File Offset: 0x000E42C2
		private void RenderDescription(string id, Strings.IDs formatID, params string[] args)
		{
			this.RenderDescription(id, formatID, false, args);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000E60D0 File Offset: 0x000E42D0
		private void RenderDescription(string id, Strings.IDs formatID, bool addInvalidImage, params string[] args)
		{
			base.Response.Output.Write("<div id=\"");
			base.Response.Output.Write(id);
			base.Response.Output.Write("\">");
			if (addInvalidImage)
			{
				this.RenderInvalidImage();
			}
			else
			{
				this.RenderClearImage();
			}
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.DeleteSmall, null, new object[]
			{
				"id=\"del\""
			});
			base.Response.Output.Write(LocalizedStrings.GetHtmlEncoded(formatID), args);
			base.Response.Output.Write("</div>");
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000E6184 File Offset: 0x000E4384
		private void RenderContextMenuHeader(Strings.IDs textID, string dialogId)
		{
			base.Response.Write("<div id=\"");
			base.Response.Write(dialogId);
			base.Response.Write("\"><span>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(textID));
			base.Response.Write("</span></div>");
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000E61E0 File Offset: 0x000E43E0
		private void RenderInvalidImage()
		{
			base.Response.Write("<span id=spnInv style=\"visibility:hidden;\">");
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.Error2, null, new object[]
			{
				"id=\"imgInv\""
			});
			base.Response.Output.Write("</span>");
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000E6240 File Offset: 0x000E4440
		private void RenderClearImage()
		{
			base.Response.Write("<span id=spnInv style=\"visibility:hidden;\">");
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.Clear, null, new object[]
			{
				"id=\"imgInv\""
			});
			base.Response.Output.Write("</span>");
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000E629C File Offset: 0x000E449C
		private void RenderFactoryInitializationScripts()
		{
			base.Response.Write("var t = g_oPAFac;\n");
			if (this.PaaHasExtension || this.HasMoreThenOneUMExtension)
			{
				base.Response.Write("t[\"e\"] = new ExtCon(\"divC1\");\n");
			}
			base.Response.Write("t[\"c\"] = new CllrIs(\"divC2\");\n");
			base.Response.Write("t[\"d\"] = new DurPrd(\"divC3\");\n");
			base.Response.Write("t[\"s\"] = new SchSt(\"divC4\");\n");
			base.Response.Write("t[\"o\"] = new OOF(\"divC5\");\n");
			base.Response.Write("t[\"f\"] = new FndMe(\"divF\");\n");
			base.Response.Write("t[\"x\"] = new XfrTo(\"divX\");\n");
			base.Response.Write("t[\"r\"] = new RcrdMsg(\"divA1\");\n");
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000E6349 File Offset: 0x000E4549
		private string CheckIsValid(PAAValidationResult validationResult, string data)
		{
			return this.CheckIsValid(validationResult, data, false);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000E6354 File Offset: 0x000E4554
		private string CheckIsValid(PAAValidationResult validationResult, string data, bool findAdRecipient)
		{
			string result = null;
			if (validationResult != PAAValidationResult.Valid)
			{
				if (findAdRecipient && validationResult != PAAValidationResult.NonExistentContact && validationResult != PAAValidationResult.NonExistentDirectoryUser)
				{
					AdRecipientBatchQuery adRecipientBatchQuery = new AdRecipientBatchQuery(base.UserContext, new string[]
					{
						data
					});
					ADRecipient adRecipient = adRecipientBatchQuery.GetAdRecipient(data);
					if (adRecipient == null || string.IsNullOrEmpty(adRecipient.DisplayName))
					{
						data = LocalizedStrings.GetNonEncoded(-1626952556);
					}
					else
					{
						data = adRecipient.DisplayName;
					}
				}
				result = UnifiedMessagingUtilities.GetErrorResourceId(validationResult, data);
			}
			return result;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000E63C0 File Offset: 0x000E45C0
		private void RenderCallerIdCondition()
		{
			bool flag = false;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<Contact> list3 = new List<Contact>();
			string text = null;
			foreach (CallerIdBase callerIdBase in this.paa.CallerIdList)
			{
				switch (callerIdBase.CallerIdType)
				{
				case CallerIdTypeEnum.Number:
				{
					string phoneNumberString = ((PhoneNumberCallerId)callerIdBase).PhoneNumberString;
					if (string.IsNullOrEmpty(phoneNumberString))
					{
						throw new OwaInvalidInputException("Phone number may not be null or empty string");
					}
					if (text == null)
					{
						text = this.CheckIsValid(callerIdBase.ValidationResult, phoneNumberString);
					}
					list.Add(phoneNumberString);
					break;
				}
				case CallerIdTypeEnum.ContactItem:
				{
					ContactItemCallerId contactItemCallerId = callerIdBase as ContactItemCallerId;
					if (contactItemCallerId.StoreObjectId == null)
					{
						throw new OwaInvalidInputException("Contact store object id may not be null");
					}
					Contact contact = UnifiedMessagingUtilities.GetContact(contactItemCallerId.StoreObjectId, base.UserContext);
					if (text == null)
					{
						string data;
						if (contact == null || string.IsNullOrEmpty(contact.DisplayName))
						{
							data = LocalizedStrings.GetNonEncoded(-1626952556);
						}
						else
						{
							data = contact.DisplayName;
						}
						text = this.CheckIsValid(callerIdBase.ValidationResult, data);
					}
					list3.Add(contact);
					break;
				}
				case CallerIdTypeEnum.DefaultContactFolder:
					flag = true;
					if (text == null)
					{
						text = this.CheckIsValid(callerIdBase.ValidationResult, null);
					}
					break;
				case CallerIdTypeEnum.ADContact:
				{
					string legacyExchangeDN = ((ADContactCallerId)callerIdBase).LegacyExchangeDN;
					if (string.IsNullOrEmpty(legacyExchangeDN))
					{
						throw new OwaInvalidInputException("Legacy DN may not be null or empty string");
					}
					if (text == null)
					{
						text = this.CheckIsValid(callerIdBase.ValidationResult, legacyExchangeDN, true);
					}
					list2.Add(legacyExchangeDN);
					break;
				}
				}
			}
			if (list.Count > 0)
			{
				this.RenderPhoneNumbers(list);
			}
			bool flag2 = false;
			if (list2.Count > 0)
			{
				base.Response.Write("A.rgRcp = new Array(");
				this.RenderCallerIdRecipientsScript(list2.ToArray());
				flag2 = true;
			}
			if (list3.Count > 0)
			{
				bool appendComma = flag2;
				if (!flag2)
				{
					base.Response.Write("A.rgRcp = new Array(");
					flag2 = true;
				}
				this.RenderCallerIdContactsScript(list3, appendComma);
			}
			if (flag2)
			{
				base.Response.Write(");\n");
			}
			if (flag)
			{
				base.Response.Write("A.fld = 1;\n");
			}
			if (text != null)
			{
				base.Response.Write("A.err = \"");
				base.Response.Write(text);
				base.Response.Write("\";\n");
			}
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000E663C File Offset: 0x000E483C
		private void RenderPhoneNumbers(List<string> numbers)
		{
			base.Response.Write("A.phs = new Array(");
			bool flag = false;
			foreach (string s in numbers)
			{
				if (flag)
				{
					base.Response.Write(",");
				}
				else
				{
					flag = true;
				}
				base.Response.Write("\"");
				Utilities.JavascriptEncode(s, base.Response.Output);
				base.Response.Write("\"");
			}
			base.Response.Write(");\n");
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000E66F0 File Offset: 0x000E48F0
		private void RenderCallerIdConditionScript()
		{
			if (this.paa.CallerIdList != null && this.paa.CallerIdList.Count > 0)
			{
				this.RenderObjectAddScript("c");
				this.RenderCallerIdCondition();
				base.Response.Write("g_oPAObj.add(A);\n");
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000E6740 File Offset: 0x000E4940
		private void RenderActionScript(KeyMappingBase action, string id)
		{
			this.RenderObjectAddScript(id);
			string text = null;
			if (action.KeyMappingType != KeyMappingTypeEnum.TransferToVoicemail)
			{
				this.RenderObjectPropertyScript("desc", action.Context);
				this.RenderObjectPropertyScript("key", action.Key);
				this.RenderRemoveKey(action.Key);
				base.Response.Write("A.id = A.sDivId + A.key;\n");
				switch (action.KeyMappingType)
				{
				case KeyMappingTypeEnum.TransferToNumber:
				{
					TransferToNumber transferToNumber = action as TransferToNumber;
					this.RenderObjectPropertyScript("ph", transferToNumber.PhoneNumberString);
					if (text == null)
					{
						text = this.CheckIsValid(action.ValidationResult, transferToNumber.PhoneNumberString);
					}
					break;
				}
				case KeyMappingTypeEnum.TransferToADContactMailbox:
				{
					TransferToADContact transferToADContact = action as TransferToADContact;
					this.RenderTransferToRecipientScript(transferToADContact.LegacyExchangeDN);
					this.RenderObjectPropertyScript("bToVM", 1);
					if (text == null)
					{
						text = this.CheckIsValid(action.ValidationResult, transferToADContact.LegacyExchangeDN, true);
					}
					break;
				}
				case KeyMappingTypeEnum.TransferToADContactPhone:
				{
					TransferToADContact transferToADContact2 = action as TransferToADContact;
					this.RenderTransferToRecipientScript(transferToADContact2.LegacyExchangeDN);
					if (text == null)
					{
						text = this.CheckIsValid(action.ValidationResult, transferToADContact2.LegacyExchangeDN, true);
					}
					break;
				}
				case KeyMappingTypeEnum.FindMe:
				{
					TransferToFindMe transferToFindMe = (TransferToFindMe)action;
					FindMeNumbers numbers = transferToFindMe.Numbers;
					for (int i = 0; i < numbers.NumberList.Length; i++)
					{
						FindMe findMe = numbers.NumberList[i];
						this.RenderObjectPropertyScript("ph" + (i + 1).ToString(CultureInfo.InvariantCulture), findMe.Number);
						this.RenderObjectPropertyScript("tm" + (i + 1).ToString(CultureInfo.InvariantCulture), findMe.Timeout);
						if (text == null)
						{
							text = this.CheckIsValid(findMe.ValidationResult, findMe.Number);
						}
					}
					break;
				}
				}
			}
			if (text != null)
			{
				base.Response.Write("A.err = \"");
				base.Response.Write(text);
				base.Response.Write("\";\n");
			}
			base.Response.Write("g_oPAObj.add(A);\n");
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000E693F File Offset: 0x000E4B3F
		private void RenderRemoveKey(int key)
		{
			base.Response.Write("rmvKey(");
			base.Response.Write(key);
			base.Response.Write(");\n");
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000E6974 File Offset: 0x000E4B74
		private void RenderObjectPropertyScript(string name, string value)
		{
			base.Response.Write("A.");
			base.Response.Write(name);
			base.Response.Write(" = \"");
			Utilities.JavascriptEncode(value, base.Response.Output);
			base.Response.Write("\";\n");
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000E69D0 File Offset: 0x000E4BD0
		private void RenderObjectPropertyScript(string name, int value)
		{
			base.Response.Write("A.");
			base.Response.Write(name);
			base.Response.Write(" = ");
			base.Response.Write(value);
			base.Response.Write(";\n");
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000E6A2A File Offset: 0x000E4C2A
		private void RenderObjectAddScript(string id)
		{
			base.Response.Write("A = g_oPAFac.gt(\"");
			base.Response.Write(id);
			base.Response.Write("\").clone();\n");
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000E6A58 File Offset: 0x000E4C58
		private void RenderRecipientScript(ADRecipient adRecipient, bool isUnknown)
		{
			if (adRecipient == null)
			{
				throw new ArgumentNullException("adRecipient");
			}
			base.Response.Write("new Recip(\"");
			string text = adRecipient.DisplayName;
			if (text != null)
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = LocalizedStrings.GetNonEncoded(-1626952556);
			}
			Utilities.JavascriptEncode(text, base.Response.Output);
			base.Response.Write("\",\"");
			if (!isUnknown)
			{
				Utilities.JavascriptEncode(adRecipient.LegacyExchangeDN, base.Response.Output);
			}
			base.Response.Write("\",\"");
			string s = string.Empty;
			string s2 = string.Empty;
			int num = 0;
			SmtpAddress primarySmtpAddress = adRecipient.PrimarySmtpAddress;
			s = adRecipient.PrimarySmtpAddress.ToString();
			Utilities.JavascriptEncode(s, base.Response.Output);
			base.Response.Write("\",\"");
			if (adRecipient.Alias != null)
			{
				Utilities.JavascriptEncode(adRecipient.Alias, base.Response.Output);
			}
			base.Response.Write("\",\"");
			if (!isUnknown)
			{
				base.Response.Write("EX");
			}
			base.Response.Write("\",");
			base.Response.Write(2);
			base.Response.Write(",\"");
			if (adRecipient.Id != null)
			{
				s2 = Utilities.GetBase64StringFromADObjectId(adRecipient.Id);
			}
			Utilities.JavascriptEncode(s2, base.Response.Output);
			base.Response.Write("\",");
			base.Response.Write(num);
			base.Response.Write(")");
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000E6C08 File Offset: 0x000E4E08
		private void RenderTransferToRecipientScript(string legacyDN)
		{
			if (string.IsNullOrEmpty(legacyDN))
			{
				throw new ArgumentNullException("legacyDN");
			}
			AdRecipientBatchQuery adRecipientBatchQuery = new AdRecipientBatchQuery(base.UserContext, new string[]
			{
				legacyDN
			});
			ADRecipient adrecipient = adRecipientBatchQuery.GetAdRecipient(legacyDN);
			bool isUnknown = false;
			if (adrecipient == null)
			{
				adrecipient = EditPAA.CreateUnknownADRecipient(legacyDN);
				isUnknown = true;
			}
			base.Response.Write("A.rcp = ");
			this.RenderRecipientScript(adrecipient, isUnknown);
			base.Response.Write(";");
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000E6C80 File Offset: 0x000E4E80
		private void RenderCallerIdRecipientsScript(string[] legacyDNs)
		{
			if (legacyDNs == null || legacyDNs.Length == 0)
			{
				throw new ArgumentNullException("legacyDNs");
			}
			AdRecipientBatchQuery adRecipientBatchQuery = new AdRecipientBatchQuery(base.UserContext, legacyDNs);
			bool flag = false;
			for (int i = 0; i < legacyDNs.Length; i++)
			{
				ADRecipient adrecipient = adRecipientBatchQuery.GetAdRecipient(legacyDNs[i]);
				bool isUnknown;
				if (adrecipient == null)
				{
					adrecipient = EditPAA.CreateUnknownADRecipient(legacyDNs[i]);
					isUnknown = true;
				}
				else
				{
					isUnknown = false;
				}
				if (flag)
				{
					base.Response.Write(",");
				}
				else
				{
					flag = true;
				}
				this.RenderRecipientScript(adrecipient, isUnknown);
			}
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000E6D00 File Offset: 0x000E4F00
		private void RenderCallerIdContactsScript(List<Contact> contacts, bool appendComma)
		{
			if (contacts == null)
			{
				throw new ArgumentNullException("contacts");
			}
			if (contacts.Count == 0)
			{
				throw new ArgumentException("contacts list may not be empty");
			}
			for (int i = 0; i < contacts.Count; i++)
			{
				using (Contact contact = contacts[i])
				{
					bool flag = contact == null;
					if (appendComma)
					{
						base.Response.Write(",");
					}
					else
					{
						appendComma = true;
					}
					base.Response.Write("new Recip(\"");
					string text = null;
					if (!flag)
					{
						text = contact.DisplayName;
					}
					if (text != null)
					{
						text = text.Trim();
					}
					if (string.IsNullOrEmpty(text))
					{
						text = LocalizedStrings.GetNonEncoded(-1626952556);
					}
					Utilities.JavascriptEncode(text, base.Response.Output);
					base.Response.Write("\",\"");
					Participant participant = null;
					EmailAddressIndex emailAddressIndex = EmailAddressIndex.None;
					if (!flag)
					{
						EmailAddressIndex[] array = new EmailAddressIndex[]
						{
							EmailAddressIndex.Email1,
							EmailAddressIndex.Email2,
							EmailAddressIndex.Email3,
							EmailAddressIndex.BusinessFax,
							EmailAddressIndex.HomeFax,
							EmailAddressIndex.OtherFax
						};
						foreach (EmailAddressIndex emailAddressIndex2 in array)
						{
							Participant participant2 = contact.EmailAddresses[emailAddressIndex2];
							if (participant2 != null && !string.IsNullOrEmpty(participant2.EmailAddress))
							{
								participant = participant2;
								emailAddressIndex = emailAddressIndex2;
								break;
							}
						}
					}
					if (participant != null)
					{
						Utilities.JavascriptEncode(participant.EmailAddress, base.Response.Output);
					}
					else if (!flag)
					{
						Utilities.JavascriptEncode(text, base.Response.Output);
					}
					base.Response.Write("\",\"");
					if (participant != null)
					{
						Utilities.JavascriptEncode(participant.EmailAddress, base.Response.Output);
					}
					else if (!flag)
					{
						Utilities.JavascriptEncode(text, base.Response.Output);
					}
					base.Response.Write("\",\"");
					base.Response.Write("\",\"");
					if (participant != null)
					{
						Utilities.JavascriptEncode("SMTP", base.Response.Output);
					}
					base.Response.Write("\",");
					base.Response.Write(1);
					base.Response.Write(",\"");
					if (!flag)
					{
						Utilities.JavascriptEncode(contact.Id.ObjectId.ToBase64String(), base.Response.Output);
					}
					base.Response.Write("\",");
					base.Response.Write(0);
					base.Response.Write(",");
					base.Response.Write((int)emailAddressIndex);
					base.Response.Write(")");
				}
			}
			contacts.Clear();
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000E6FE0 File Offset: 0x000E51E0
		private void RenderScheduleStatusMenuItem(string id, FreeBusyStatusEnum busyType)
		{
			Strings.IDs? ds = null;
			base.Response.Write("<tr><td><input type=\"checkbox\" id=\"");
			base.Response.Write(id);
			base.Response.Write("\" _v=");
			base.Response.Write((int)busyType);
			base.Response.Write("></td><td>");
			switch (busyType)
			{
			case FreeBusyStatusEnum.Free:
				base.Response.Write("<div class=free></div>");
				ds = new Strings.IDs?(-971703552);
				goto IL_101;
			case FreeBusyStatusEnum.Tentative:
			case FreeBusyStatusEnum.Free | FreeBusyStatusEnum.Tentative:
				break;
			case FreeBusyStatusEnum.Busy:
				base.Response.Write("<div class=busy></div>");
				ds = new Strings.IDs?(2052801377);
				goto IL_101;
			default:
				if (busyType == FreeBusyStatusEnum.OutOfOffice)
				{
					base.Response.Write("<div class=oof></div>");
					ds = new Strings.IDs?(2047193656);
					goto IL_101;
				}
				break;
			}
			base.UserContext.RenderBaseThemeImage(base.Response.Output, ThemeFileId.Tentative, "tntv", new object[0]);
			ds = new Strings.IDs?(1797669216);
			IL_101:
			base.Response.Write("</td><td><label for=\"");
			base.Response.Write(id);
			base.Response.Write("\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(ds.Value));
			base.Response.Write("</label></td></tr>");
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000E7144 File Offset: 0x000E5344
		private void SetStartAndEndTime(out ExDateTime startTime, out ExDateTime endTime)
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			startTime = new ExDateTime(base.UserContext.TimeZone, localTime.Year, localTime.Month, localTime.Day, localTime.Hour, localTime.Minute, 0);
			if (startTime.Minute != 0 && startTime.Minute != 30)
			{
				startTime = startTime.AddMinutes((double)(30 - startTime.Minute % 30));
			}
			if (startTime.Hour >= 23)
			{
				startTime.AddMinutes((double)(-(double)startTime.Minute));
				endTime = startTime.AddMinutes(59.0);
				return;
			}
			endTime = startTime.AddMinutes(60.0);
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000E7200 File Offset: 0x000E5400
		private void RenderTimeDropdownList(ExDateTime time, string id)
		{
			TimeDropDownList.RenderTimePicker(base.Response.Output, time, id);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000E7214 File Offset: 0x000E5414
		private void RenderRecipientPickerControl()
		{
			RecipientWellEventHandler.RenderRecipientWellControl(-1018465893, "RwPkr", base.UserContext, base.Response.Output);
		}

		// Token: 0x04001C00 RID: 7168
		private const string PAAIdQueryStringParameter = "id";

		// Token: 0x04001C01 RID: 7169
		private PersonalAutoAttendant paa;

		// Token: 0x04001C02 RID: 7170
		private bool enabledForOutdialing;

		// Token: 0x04001C03 RID: 7171
		private bool enabledForPersonalAutoAttendant;

		// Token: 0x04001C04 RID: 7172
		private bool isPlayOnPhoneEnabled;

		// Token: 0x04001C05 RID: 7173
		private IList<string> extensions;

		// Token: 0x04001C06 RID: 7174
		private string identity;

		// Token: 0x04001C07 RID: 7175
		private bool isValid = true;

		// Token: 0x04001C08 RID: 7176
		private bool canBargeIn = true;

		// Token: 0x04001C09 RID: 7177
		private Infobar infobar;
	}
}
