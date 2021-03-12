using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.OOF;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000074 RID: 116
	public class OofOptions : OptionsBase
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0001C228 File Offset: 0x0001A428
		public OofOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.CommitAndLoad();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001C278 File Offset: 0x0001A478
		private void Load()
		{
			this.oofState = this.oofSettings.OofState;
			this.externalAudience = this.oofSettings.ExternalAudience;
			this.oofToInternal = this.oofSettings.InternalReply.Message;
			this.oofToExternal = this.oofSettings.ExternalReply.Message;
			this.oofEnabled = (this.oofState != OofState.Disabled);
			this.isScheduled = (this.oofState == OofState.Scheduled);
			this.isScheduledForExternal = (this.externalAudience != ExternalAudience.None);
			this.externalAllEnabled = (this.externalAudience != ExternalAudience.Known);
			if (this.oofSettings.Duration != null)
			{
				this.startTime = new ExDateTime(this.userContext.TimeZone, this.oofSettings.Duration.StartTime);
				this.endTime = new ExDateTime(this.userContext.TimeZone, this.oofSettings.Duration.EndTime);
				return;
			}
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			this.startTime = new ExDateTime(this.userContext.TimeZone, localTime.Year, localTime.Month, localTime.Day, localTime.Hour, 0, 0).AddHours(1.0);
			this.endTime = this.startTime.AddHours(1.0);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001C3D7 File Offset: 0x0001A5D7
		public override void Render()
		{
			this.RenderOofOptions();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		public override void RenderScript()
		{
			base.RenderJSVariable("a_fOof", this.oofEnabled.ToString().ToLowerInvariant());
			base.RenderJSVariable("a_fAll", this.externalAllEnabled.ToString().ToLowerInvariant());
			base.RenderJSVariable("a_fExtSnd", this.isScheduledForExternal.ToString().ToLowerInvariant());
			base.RenderJSVariable("a_fTmd", this.isScheduled.ToString().ToLowerInvariant());
			if (this.isInvalidDuration)
			{
				ExDateTime minValue = ExDateTime.MinValue;
				ExDateTime minValue2 = ExDateTime.MinValue;
				base.RenderJSVariable("a_iSM", minValue.Month.ToString());
				base.RenderJSVariable("a_iSD", minValue.Day.ToString());
				base.RenderJSVariable("a_iSY", minValue.Year.ToString());
				base.RenderJSVariable("a_iST", minValue.Hour.ToString());
				base.RenderJSVariable("a_iEM", minValue2.Month.ToString());
				base.RenderJSVariable("a_iED", minValue2.Day.ToString());
				base.RenderJSVariable("a_iEY", minValue2.Year.ToString());
				base.RenderJSVariable("a_iET", minValue2.Hour.ToString());
			}
			else
			{
				base.RenderJSVariable("a_iSM", this.startTime.Month.ToString());
				base.RenderJSVariable("a_iSD", this.startTime.Day.ToString());
				base.RenderJSVariable("a_iSY", this.startTime.Year.ToString());
				base.RenderJSVariable("a_iST", this.startTime.Hour.ToString());
				base.RenderJSVariable("a_iEM", this.endTime.Month.ToString());
				base.RenderJSVariable("a_iED", this.endTime.Day.ToString());
				base.RenderJSVariable("a_iEY", this.endTime.Year.ToString());
				base.RenderJSVariable("a_iET", this.endTime.Hour.ToString());
			}
			base.RenderJSVariable("a_fOofInt", !string.IsNullOrEmpty(this.oofToInternal));
			base.RenderJSVariable("a_fOofExt", !string.IsNullOrEmpty(this.oofToExternal));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001C678 File Offset: 0x0001A878
		private void CommitAndLoad()
		{
			this.oofSettings = UserOofSettings.GetUserOofSettings(this.userContext.MailboxSession);
			this.Load();
			bool flag = false;
			if (Utilities.IsPostRequest(this.request) && !string.IsNullOrEmpty(base.Command))
			{
				string formParameter = Utilities.GetFormParameter(this.request, "rdoOof", false);
				if (!string.IsNullOrEmpty(formParameter))
				{
					bool flag2 = formParameter == 1.ToString();
					bool flag3 = Utilities.GetFormParameter(this.request, "chkTmd", false) != null;
					if (!flag2)
					{
						if (this.oofSettings.OofState != OofState.Disabled)
						{
							this.oofSettings.OofState = OofState.Disabled;
							flag = true;
						}
					}
					else if (flag3)
					{
						if (this.oofSettings.OofState != OofState.Scheduled)
						{
							this.oofSettings.OofState = OofState.Scheduled;
							flag = true;
						}
						string formParameter2 = Utilities.GetFormParameter(this.request, "selSM", false);
						string formParameter3 = Utilities.GetFormParameter(this.request, "selSD", false);
						string formParameter4 = Utilities.GetFormParameter(this.request, "selSY", false);
						string formParameter5 = Utilities.GetFormParameter(this.request, "selST", false);
						string formParameter6 = Utilities.GetFormParameter(this.request, "selEM", false);
						string formParameter7 = Utilities.GetFormParameter(this.request, "selED", false);
						string formParameter8 = Utilities.GetFormParameter(this.request, "selEY", false);
						string formParameter9 = Utilities.GetFormParameter(this.request, "selET", false);
						if (string.IsNullOrEmpty(formParameter2) || string.IsNullOrEmpty(formParameter3) || string.IsNullOrEmpty(formParameter4) || string.IsNullOrEmpty(formParameter5) || string.IsNullOrEmpty(formParameter6) || string.IsNullOrEmpty(formParameter7) || string.IsNullOrEmpty(formParameter8) || string.IsNullOrEmpty(formParameter9))
						{
							base.SetInfobarMessage(string.Format(LocalizedStrings.GetNonEncoded(1140546334), this.userContext.UserOptions.DateFormat), InfobarMessageType.Error);
							return;
						}
						int num = int.Parse(formParameter3);
						if (num > ExDateTime.DaysInMonth(int.Parse(formParameter4), int.Parse(formParameter2)))
						{
							num = ExDateTime.DaysInMonth(int.Parse(formParameter4), int.Parse(formParameter2));
						}
						ExDateTime t = new ExDateTime(this.userContext.TimeZone, int.Parse(formParameter4), int.Parse(formParameter2), num);
						num = int.Parse(formParameter7);
						if (num > ExDateTime.DaysInMonth(int.Parse(formParameter8), int.Parse(formParameter6)))
						{
							num = ExDateTime.DaysInMonth(int.Parse(formParameter8), int.Parse(formParameter6));
						}
						ExDateTime t2 = new ExDateTime(this.userContext.TimeZone, int.Parse(formParameter8), int.Parse(formParameter6), num);
						t = t.AddHours((double)int.Parse(formParameter5));
						t2 = t2.AddHours((double)int.Parse(formParameter9));
						if (t > t2)
						{
							base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(107113300), InfobarMessageType.Error);
							this.isInvalidDuration = true;
						}
						if (this.oofSettings.Duration == null)
						{
							this.oofSettings.Duration = new Duration();
						}
						if (this.oofSettings.Duration.StartTime != (DateTime)t.ToUtc())
						{
							this.oofSettings.Duration.StartTime = (DateTime)t.ToUtc();
							flag = true;
						}
						if (this.oofSettings.Duration.EndTime != (DateTime)t2.ToUtc())
						{
							this.oofSettings.Duration.EndTime = (DateTime)t2.ToUtc();
							flag = true;
						}
					}
					else if (this.oofSettings.OofState != OofState.Enabled)
					{
						this.oofSettings.OofState = OofState.Enabled;
						flag = true;
					}
					string formParameter10 = Utilities.GetFormParameter(this.request, "txtInt", false);
					string formParameter11 = Utilities.GetFormParameter(this.request, "chkInt", false);
					if (((formParameter11 == null && string.IsNullOrEmpty(this.oofToInternal)) || !string.IsNullOrEmpty(formParameter11)) && !Utilities.WhiteSpaceOnlyOrNullEmpty(formParameter10))
					{
						this.oofSettings.InternalReply.Message = BodyConversionUtilities.ConvertTextToHtml(formParameter10);
						flag = true;
					}
					string formParameter12 = Utilities.GetFormParameter(this.request, "txtExt", false);
					string formParameter13 = Utilities.GetFormParameter(this.request, "chkExt", false);
					if (((formParameter13 == null && string.IsNullOrEmpty(this.oofToExternal)) || !string.IsNullOrEmpty(formParameter13)) && !Utilities.WhiteSpaceOnlyOrNullEmpty(formParameter12))
					{
						this.oofSettings.ExternalReply.Message = BodyConversionUtilities.ConvertTextToHtml(formParameter12);
						flag = true;
					}
					if (Utilities.GetFormParameter(this.request, "chkExtSnd", false) != null)
					{
						string formParameter14 = Utilities.GetFormParameter(this.request, "rdoAll", false);
						if (!string.IsNullOrEmpty(formParameter14))
						{
							if (formParameter14 == 3.ToString())
							{
								if (this.oofSettings.ExternalAudience != ExternalAudience.All)
								{
									this.oofSettings.ExternalAudience = ExternalAudience.All;
									flag = true;
								}
							}
							else if (this.oofSettings.ExternalAudience != ExternalAudience.Known)
							{
								this.oofSettings.ExternalAudience = ExternalAudience.Known;
								flag = true;
							}
						}
					}
					else if (this.oofSettings.ExternalAudience != ExternalAudience.None)
					{
						this.oofSettings.ExternalAudience = ExternalAudience.None;
						flag = true;
					}
					if (flag)
					{
						try
						{
							this.oofSettings.Save(this.userContext.MailboxSession);
							base.SetSavedSuccessfully(true);
						}
						catch (InvalidScheduledOofDuration)
						{
							base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(-561991348), InfobarMessageType.Error);
							this.isInvalidDuration = true;
						}
						this.Load();
					}
				}
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
		private void RenderOofOptions()
		{
			base.RenderHeaderRow(ThemeFileId.Oof, 917218743);
			this.RenderOofInternalOptions();
			this.writer.Write("<tr><td class=\"dsh\"><img src=\"");
			this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
			this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
			this.RenderOofExternalOptions();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		private void RenderOofInternalOptions()
		{
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\">");
			this.writer.Write("<tr><td>");
			this.writer.Write("<input type=\"radio\" name=\"{0}\"{2} id=\"{5}\" onclick=\"return onClkOofRdo({1});\" value=\"{1}\"{3}><label for=\"{5}\">{4}</label>", new object[]
			{
				"rdoOof",
				0.ToString(),
				(!this.oofEnabled) ? " checked" : string.Empty,
				string.Empty,
				LocalizedStrings.GetHtmlEncoded(-640476466),
				"rdoOof1"
			});
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.writer.Write("<input type=\"radio\" name=\"{0}\"{2} id=\"{5}\" onclick=\"return onClkOofRdo({1});\" value=\"{1}\"{3}><label for=\"{5}\">{4}</label>", new object[]
			{
				"rdoOof",
				1.ToString(),
				this.oofEnabled ? " checked" : string.Empty,
				string.Empty,
				LocalizedStrings.GetHtmlEncoded(1328100008),
				"rdoOof2"
			});
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.RenderTimeDuration();
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1004950132));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.RenderOofInternalMessage();
			this.writer.Write("</td></tr>");
			this.writer.Write("</table>");
			this.writer.Write("</td></tr>");
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001CDFC File Offset: 0x0001AFFC
		private void RenderOofExternalOptions()
		{
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\">");
			this.writer.Write("<tr><td>");
			string text = string.Format(LocalizedStrings.GetHtmlEncoded(980899065), "<b>" + LocalizedStrings.GetHtmlEncoded(119471705) + "</b>");
			this.writer.Write("<input type=\"checkbox\" name=\"{0}\"{2} id=\"{0}\" onclick=\"return onClkOofChkBx({1});\" value=\"\"{3}><label for=\"{0}\">{4}</label>", new object[]
			{
				"chkExtSnd",
				1.ToString(),
				this.isScheduledForExternal ? " checked" : string.Empty,
				this.oofEnabled ? string.Empty : " disabled",
				text
			});
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.RenderOofExternalSenderSelection();
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1973556468));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.RenderOofExternalMessage();
			this.writer.Write("</td></tr>");
			this.writer.Write("</table>");
			this.writer.Write("</td></tr>");
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001CF74 File Offset: 0x0001B174
		private void RenderOofExternalSenderSelection()
		{
			this.writer.Write("<table class=\"tm\">");
			this.writer.Write("<tr><td>");
			this.writer.Write("<input type=\"radio\" name=\"{0}\"{2} id=\"{5}\" onclick=\"return onClkOofRdo({1});\" value=\"{1}\"{3}><label for=\"{5}\">{4}</label>", new object[]
			{
				"rdoAll",
				2.ToString(),
				(!this.externalAllEnabled) ? " checked" : string.Empty,
				(this.oofEnabled && this.isScheduledForExternal) ? string.Empty : " disabled",
				LocalizedStrings.GetHtmlEncoded(-1839461610),
				"rdoAll1"
			});
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.writer.Write("<input type=\"radio\" name=\"{0}\"{2} id=\"{5}\" onclick=\"return onClkOofRdo({1});\" value=\"{1}\"{3}><label for=\"{5}\">{4}</label>", new object[]
			{
				"rdoAll",
				3.ToString(),
				this.externalAllEnabled ? " checked" : string.Empty,
				(this.oofEnabled && this.isScheduledForExternal) ? string.Empty : " disabled",
				LocalizedStrings.GetHtmlEncoded(-689266169),
				"rdoAll2"
			});
			this.writer.Write("</td></tr>");
			this.writer.Write("</table>");
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		private void RenderOofExternalMessage()
		{
			if (!string.IsNullOrEmpty(this.oofToExternal))
			{
				this.writer.Write("<table class=\"oof\" cellpadding=0 cellspacing=0>");
				this.writer.Write("<tr><td class=\"dsl\" rowspan=3><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\" class=\"wh1\"></td><td class=\"w100\"></td><td class=\"dsr\" rowspan=3><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
				this.writer.Write("<tr><td class=\"ds\">");
				this.writer.Write(Utilities.SanitizeHtml(this.oofToExternal));
				this.writer.Write("</td></tr>");
				this.writer.Write("<tr><td></td></tr></table>");
				this.writer.Write("<table class=\"oofnb\"><tr><td class=\"df\"><input type=\"checkbox\" name=\"");
				this.writer.Write("chkExt");
				this.writer.Write("\" id=\"");
				this.writer.Write("chkExt");
				this.writer.Write("\" onclick=\"return onClkChkBx(this);\" value=\"1\"");
				this.writer.Write((this.oofEnabled && this.isScheduledForExternal) ? ">" : " disabled>");
				this.writer.Write("<label for=\"");
				this.writer.Write("chkExt");
				this.writer.Write("\">");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1886172398));
				this.writer.Write("</label></td></tr>");
				this.writer.Write("<tr><td class=\"w100\">");
				this.writer.Write("<textarea name=\"");
				this.writer.Write("txtExt");
				this.writer.Write("\" class=\"w100\" title=\"");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(1640929204));
				this.writer.Write("\" rows=12 cols=61 onfocus=\"onFcsTxt('");
				this.writer.Write("chkExt");
				this.writer.Write("');\"");
				this.writer.Write((this.oofEnabled && this.isScheduledForExternal) ? ">" : " disabled>");
				this.writer.Write("</textarea>");
				this.writer.Write("</td></tr>");
				this.writer.Write("</table>");
				return;
			}
			this.writer.Write("<table class=\"oofnb\"><tr><td class=\"w100\"><textarea name=\"");
			this.writer.Write("txtExt");
			this.writer.Write("\" class=\"w100\" title=\"");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1640929204));
			this.writer.Write("\" rows=12 cols=61");
			this.writer.Write((this.oofEnabled && this.isScheduledForExternal) ? ">" : " disabled>");
			this.writer.Write("</textarea>");
			this.writer.Write("</td></tr>");
			this.writer.Write("</table>");
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		private void RenderOofInternalMessage()
		{
			if (!string.IsNullOrEmpty(this.oofToInternal))
			{
				this.writer.Write("<table class=\"oof\" cellpadding=0 cellspacing=0>");
				this.writer.Write("<tr><td class=\"dsl\" rowspan=3><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\" class=\"wh1\"></td><td class=\"w100\"></td><td class=\"dsr\" rowspan=3><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
				this.writer.Write("<tr><td class=\"ds\">");
				this.writer.Write(Utilities.SanitizeHtml(this.oofToInternal));
				this.writer.Write("</td></tr>");
				this.writer.Write("<tr><td></td></tr></table>");
				this.writer.Write("<table class=\"oofnb\"><tr><td class=\"df\"><input type=\"checkbox\" name=\"");
				this.writer.Write("chkInt");
				this.writer.Write("\" id=\"");
				this.writer.Write("chkInt");
				this.writer.Write("\" onclick=\"return onClkChkBx(this);\" value=\"1\"");
				this.writer.Write(this.oofEnabled ? ">" : " disabled>");
				this.writer.Write("<label for=\"");
				this.writer.Write("chkInt");
				this.writer.Write("\">");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(626160585));
				this.writer.Write("<label></td></tr>");
				this.writer.Write("<tr><td class=\"w100\">");
				this.writer.Write("<textarea name=\"");
				this.writer.Write("txtInt");
				this.writer.Write("\" class=\"w100\" title=\"");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1268432338));
				this.writer.Write("\" rows=12 cols=61 onfocus=\"onFcsTxt('");
				this.writer.Write("chkInt");
				this.writer.Write("');\"");
				this.writer.Write(this.oofEnabled ? ">" : " disabled>");
				this.writer.Write("</textarea>");
				this.writer.Write("</td></tr>");
				this.writer.Write("</table>");
				return;
			}
			this.writer.Write("<table class=\"oofnb\"><tr><td class=\"w100\"><textarea name=\"");
			this.writer.Write("txtInt");
			this.writer.Write("\" class=\"w100\" title=\"");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1268432338));
			this.writer.Write("\" rows=12 cols=61");
			this.writer.Write(this.oofEnabled ? ">" : " disabled>");
			this.writer.Write("</textarea>");
			this.writer.Write("</td></tr>");
			this.writer.Write("</table>");
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
		private void RenderTimeDuration()
		{
			this.writer.Write("<table class=\"tm\">");
			this.writer.Write("<tr><td colspan=5>");
			this.writer.Write("<input type=\"checkbox\" name=\"{0}\"{2} id=\"{0}\" onclick=\"return onClkOofChkBx({1});\" value=\"\"{3}><label for=\"{0}\">{4}</label>", new object[]
			{
				"chkTmd",
				0.ToString(),
				this.isScheduled ? " checked" : string.Empty,
				this.oofEnabled ? string.Empty : " disabled",
				LocalizedStrings.GetHtmlEncoded(192681489)
			});
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-694533162));
			this.writer.Write("</td>");
			this.RenderTime(true);
			this.writer.Write("</tr><tr><td>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-521769177));
			this.writer.Write("</td>");
			this.RenderTime(false);
			this.writer.Write("</tr></table>");
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001D824 File Offset: 0x0001BA24
		private void RenderTime(bool isStartTime)
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
			this.writer.Write("<td>");
			this.writer.Write("<select name=\"{0}\" onchange=\"{1}\"{2}>", isStartTime ? "selSM" : "selEM", "onChgSel(this);", (this.oofEnabled && this.isScheduled) ? string.Empty : " disabled");
			int num = isStartTime ? this.startTime.Month : this.endTime.Month;
			for (int i = 1; i < monthNames.Length; i++)
			{
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (i == num) ? " selected" : string.Empty, i, Utilities.HtmlEncode(monthNames[i - 1]));
			}
			this.writer.Write("</select></td>");
			this.writer.Write("<td>");
			this.writer.Write("<select name=\"{0}\" onchange=\"{1}\"{2}>", isStartTime ? "selSD" : "selED", "onChgSel(this);", (this.oofEnabled && this.isScheduled) ? string.Empty : " disabled");
			num = (isStartTime ? this.startTime.Day : this.endTime.Day);
			for (int j = 1; j <= 31; j++)
			{
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (j == num) ? " selected" : string.Empty, j, j);
			}
			this.writer.Write("</select></td>");
			this.writer.Write("<td>");
			this.writer.Write("<select name=\"{0}\" onchange=\"{1}\"{2}>", isStartTime ? "selSY" : "selEY", "onChgSel(this);", (this.oofEnabled && this.isScheduled) ? string.Empty : " disabled");
			num = (isStartTime ? this.startTime.Year : this.endTime.Year);
			int num2 = localTime.Year - 2;
			int num3 = localTime.Year + 4;
			for (int k = num2; k <= num3; k++)
			{
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (k == num) ? " selected" : string.Empty, k, k);
			}
			this.writer.Write("</select></td>");
			this.writer.Write("<td>");
			num = (isStartTime ? this.startTime.Hour : this.endTime.Hour);
			this.writer.Write("<select name=\"");
			this.writer.Write(isStartTime ? "selST" : "selET");
			this.writer.Write("\" onchange=\"onChgSel(this);\"");
			this.writer.Write((this.oofEnabled && this.isScheduled) ? ">" : " disabled>");
			for (int l = 0; l < 24; l++)
			{
				DateTime dateTime = new DateTime(1, 1, 1, l, 0, 0);
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (l == num) ? " selected" : string.Empty, l, dateTime.ToString(this.userContext.UserOptions.TimeFormat, CultureInfo.CurrentUICulture.DateTimeFormat));
			}
			this.writer.Write("</select></td>");
		}

		// Token: 0x04000255 RID: 597
		private const string Option = "<option value=\"{1}\"{0}>{2}</option>";

		// Token: 0x04000256 RID: 598
		private const string FormCheckBox = "<input type=\"checkbox\" name=\"{0}\"{2} id=\"{0}\" onclick=\"return onClkOofChkBx({1});\" value=\"\"{3}><label for=\"{0}\">{4}</label>";

		// Token: 0x04000257 RID: 599
		private const string FormRadioButton = "<input type=\"radio\" name=\"{0}\"{2} id=\"{5}\" onclick=\"return onClkOofRdo({1});\" value=\"{1}\"{3}><label for=\"{5}\">{4}</label>";

		// Token: 0x04000258 RID: 600
		private const string FormSelect = "<select name=\"{0}\" onchange=\"{1}\"{2}>";

		// Token: 0x04000259 RID: 601
		private const string FormOofRadioButton = "rdoOof";

		// Token: 0x0400025A RID: 602
		private const string FormOofExternalAllRadioButton = "rdoAll";

		// Token: 0x0400025B RID: 603
		private const string FormTimeDurationCheckbox = "chkTmd";

		// Token: 0x0400025C RID: 604
		private const string FormStartMonthSelect = "selSM";

		// Token: 0x0400025D RID: 605
		private const string FormStartDaySelect = "selSD";

		// Token: 0x0400025E RID: 606
		private const string FormStartYearSelect = "selSY";

		// Token: 0x0400025F RID: 607
		private const string FormStartTimeSelect = "selST";

		// Token: 0x04000260 RID: 608
		private const string FormEndMonthSelect = "selEM";

		// Token: 0x04000261 RID: 609
		private const string FormEndDaySelect = "selED";

		// Token: 0x04000262 RID: 610
		private const string FormEndYearSelect = "selEY";

		// Token: 0x04000263 RID: 611
		private const string FormEndTimeSelect = "selET";

		// Token: 0x04000264 RID: 612
		private const string FormReplaceInternal = "chkInt";

		// Token: 0x04000265 RID: 613
		private const string FormReplaceExternal = "chkExt";

		// Token: 0x04000266 RID: 614
		private const string FormSendExternal = "chkExtSnd";

		// Token: 0x04000267 RID: 615
		private const string FormReplaceInternalText = "txtInt";

		// Token: 0x04000268 RID: 616
		private const string FormReplaceExternalText = "txtExt";

		// Token: 0x04000269 RID: 617
		private const string FormJavaScriptOofRadioButton = "a_fOof";

		// Token: 0x0400026A RID: 618
		private const string FormJavaScriptOofExternalAllRadioButton = "a_fAll";

		// Token: 0x0400026B RID: 619
		private const string FormJavaScriptTimeDurationCheckbox = "a_fTmd";

		// Token: 0x0400026C RID: 620
		private const string FormJavaScriptStartMonthSelect = "a_iSM";

		// Token: 0x0400026D RID: 621
		private const string FormJavaScriptStartDaySelect = "a_iSD";

		// Token: 0x0400026E RID: 622
		private const string FormJavaScriptStartYearSelect = "a_iSY";

		// Token: 0x0400026F RID: 623
		private const string FormJavaScriptStartTimeSelect = "a_iST";

		// Token: 0x04000270 RID: 624
		private const string FormJavaScriptEndMonthSelect = "a_iEM";

		// Token: 0x04000271 RID: 625
		private const string FormJavaScriptEndDaySelect = "a_iED";

		// Token: 0x04000272 RID: 626
		private const string FormJavaScriptEndYearSelect = "a_iEY";

		// Token: 0x04000273 RID: 627
		private const string FormJavaScriptEndTimeSelect = "a_iET";

		// Token: 0x04000274 RID: 628
		private const string FormJavaScriptSendExternal = "a_fExtSnd";

		// Token: 0x04000275 RID: 629
		private const string FormJavaScriptOofInternalPresent = "a_fOofInt";

		// Token: 0x04000276 RID: 630
		private const string FormJavaScriptOofExternalPresent = "a_fOofExt";

		// Token: 0x04000277 RID: 631
		private UserOofSettings oofSettings;

		// Token: 0x04000278 RID: 632
		private OofState oofState;

		// Token: 0x04000279 RID: 633
		private ExternalAudience externalAudience;

		// Token: 0x0400027A RID: 634
		private ExDateTime startTime = ExDateTime.MinValue;

		// Token: 0x0400027B RID: 635
		private ExDateTime endTime = ExDateTime.MinValue;

		// Token: 0x0400027C RID: 636
		private string oofToInternal = string.Empty;

		// Token: 0x0400027D RID: 637
		private string oofToExternal = string.Empty;

		// Token: 0x0400027E RID: 638
		private bool oofEnabled;

		// Token: 0x0400027F RID: 639
		private bool isScheduled;

		// Token: 0x04000280 RID: 640
		private bool isScheduledForExternal;

		// Token: 0x04000281 RID: 641
		private bool externalAllEnabled = true;

		// Token: 0x04000282 RID: 642
		private bool isInvalidDuration;

		// Token: 0x02000075 RID: 117
		private enum OofStatus
		{
			// Token: 0x04000284 RID: 644
			DoNotSend,
			// Token: 0x04000285 RID: 645
			Send,
			// Token: 0x04000286 RID: 646
			SendToExternalContactsInListOnly,
			// Token: 0x04000287 RID: 647
			SendToExternalAll,
			// Token: 0x04000288 RID: 648
			Scheduled = 0,
			// Token: 0x04000289 RID: 649
			ScheduledForExternal
		}
	}
}
