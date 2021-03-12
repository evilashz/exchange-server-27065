using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200005C RID: 92
	public class JunkEmailOptions : OptionsBase
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00016240 File Offset: 0x00014440
		public JunkEmailOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.junkEmailRule = this.userContext.MailboxSession.JunkEmailRule;
			this.isEnabled = this.userContext.IsJunkEmailEnabled;
			this.isContactsTrusted = this.junkEmailRule.IsContactsFolderTrusted;
			this.safeListsOnly = this.junkEmailRule.TrustedListsOnly;
			if (Utilities.IsPostRequest(this.request) && !string.IsNullOrEmpty(base.Command))
			{
				this.SaveChanges();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000162CC File Offset: 0x000144CC
		public override void Render()
		{
			base.RenderHeaderRow(ThemeFileId.JunkEmailBig, -2053927452);
			this.writer.Write("<tr><td class=\"bd\"><table class=\"fmt\">");
			this.writer.Write("<tr><td>");
			this.RenderLabeledInput("radio", "rdoDsbl", "rdoJnk", "enblJnk();", "0", !this.isEnabled, LocalizedStrings.GetHtmlEncoded(-847566036));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td>");
			this.RenderLabeledInput("radio", "rdoEnbl", "rdoJnk", "enblJnk();", "1", this.isEnabled, LocalizedStrings.GetHtmlEncoded(897595079));
			this.writer.Write("</td></tr>");
			this.writer.Write("</table></td></tr>");
			this.writer.Write("<tr><td class=\"jmt\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1711390873));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"jms\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1007937404), "<br>");
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"jms\">");
			this.RenderEmailListControlGroup("Ssl", this.junkEmailRule.TrustedSenderEmailCollection, this.junkEmailRule.TrustedSenderDomainCollection);
			this.writer.Write("</td></tr>");
			if (this.userContext.IsFeatureEnabled(Feature.Contacts))
			{
				this.writer.Write("<tr><td class=\"jms\">");
				this.RenderCheckbox("chkTrstCnt", this.isContactsTrusted, LocalizedStrings.GetHtmlEncoded(-611691961));
				this.writer.Write("</td></tr>");
			}
			this.writer.Write("<tr><td class=\"dsh\"><img src=\"");
			this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
			this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
			this.writer.Write("<tr><td class=\"jmt\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(271542056));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"jms\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-503651893));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"jms\">");
			this.RenderEmailListControlGroup("Bsl", this.junkEmailRule.BlockedSenderEmailCollection, this.junkEmailRule.BlockedSenderDomainCollection);
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"dsh\"><img src=\"");
			this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
			this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
			this.writer.Write("<tr><td class=\"jmt\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1813922675));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"jms\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(2043062686));
			this.writer.Write("<br>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1859829305));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"jms\">");
			this.RenderEmailListControlGroup("Srl", this.junkEmailRule.TrustedRecipientEmailCollection, this.junkEmailRule.TrustedRecipientDomainCollection);
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"dsh\"><img src=\"");
			this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
			this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
			this.writer.Write("<tr><td class=\"bd\">");
			this.RenderCheckbox("chkSfOnly", this.safeListsOnly, LocalizedStrings.GetHtmlEncoded(-1352873434));
			this.writer.Write("</td></tr>");
			this.writer.Write("<input type=\"hidden\" name=\"");
			this.writer.Write("hidlst");
			this.writer.Write("\" value=\"\"><input type=\"hidden\" name=\"");
			this.writer.Write("hidoldeml");
			this.writer.Write("\" value=\"\">");
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00016744 File Offset: 0x00014944
		public override void RenderScript()
		{
			base.RenderJSVariable("a_iRdJnk", this.userContext.IsJunkEmailEnabled ? "1" : "0");
			if (this.userContext.IsFeatureEnabled(Feature.Contacts))
			{
				base.RenderJSVariable("a_fTrstCnt", this.junkEmailRule.IsContactsFolderTrusted);
			}
			base.RenderJSVariable("a_fSfOly", this.junkEmailRule.TrustedListsOnly);
			base.RenderJSVariable("g_sbOptPg", "enblJnk");
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000167C0 File Offset: 0x000149C0
		private void SaveChanges()
		{
			string formParameter = Utilities.GetFormParameter(this.request, "rdoJnk", false);
			this.isEnabled = (formParameter != null && formParameter == "1");
			if (this.isEnabled)
			{
				this.isContactsTrusted = (Utilities.GetFormParameter(this.request, "chkTrstCnt", false) != null);
				this.safeListsOnly = (Utilities.GetFormParameter(this.request, "chkSfOnly", false) != null);
			}
			string command;
			if ((command = base.Command) != null)
			{
				if (command == "a")
				{
					this.Add();
					return;
				}
				if (command == "e")
				{
					this.Edit();
					return;
				}
				if (command == "r")
				{
					this.Remove();
					return;
				}
				if (command == "save")
				{
					JunkEmailUtilities.SaveOptions(this.isEnabled, this.isContactsTrusted, this.safeListsOnly, this.userContext);
					base.SetSavedSuccessfully(true);
					this.junkEmailRule = this.userContext.MailboxSession.JunkEmailRule;
					return;
				}
			}
			throw new OwaInvalidRequestException("Unknown command");
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000168D8 File Offset: 0x00014AD8
		private void Add()
		{
			string formParameter = Utilities.GetFormParameter(this.request, "hidlst");
			string text = Utilities.GetFormParameter(this.request, "txt" + formParameter).Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			string message;
			if (JunkEmailUtilities.Add(text, JunkEmailHelper.GetListType(formParameter), this.userContext, true, out message))
			{
				base.SetInfobarMessage(message, InfobarMessageType.Informational);
				this.junkEmailRule = this.userContext.MailboxSession.JunkEmailRule;
				return;
			}
			base.SetInfobarMessage(message, InfobarMessageType.Error);
			this.initialInputs[formParameter] = text;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00016968 File Offset: 0x00014B68
		private void Edit()
		{
			string formParameter = Utilities.GetFormParameter(this.request, "hidlst");
			string text = Utilities.GetFormParameter(this.request, "txt" + formParameter).Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			string formParameter2 = Utilities.GetFormParameter(this.request, "hidoldeml");
			string message;
			if (JunkEmailUtilities.Edit(formParameter2, text, JunkEmailHelper.GetListType(formParameter), this.userContext, true, out message))
			{
				base.SetInfobarMessage(message, InfobarMessageType.Informational);
				this.junkEmailRule = this.userContext.MailboxSession.JunkEmailRule;
				return;
			}
			base.SetInfobarMessage(message, InfobarMessageType.Error);
			this.initialInputs[formParameter] = text;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00016A08 File Offset: 0x00014C08
		private void Remove()
		{
			string formParameter = Utilities.GetFormParameter(this.request, "hidlst");
			string formParameter2 = Utilities.GetFormParameter(this.request, "sel" + formParameter);
			JunkEmailUtilities.Remove(formParameter2.Split(new char[]
			{
				','
			}), JunkEmailHelper.GetListType(formParameter), this.userContext);
			this.junkEmailRule = this.userContext.MailboxSession.JunkEmailRule;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00016A78 File Offset: 0x00014C78
		private void RenderEmailListControlGroup(string groupId, JunkEmailCollection emails, JunkEmailCollection domains)
		{
			this.writer.Write("<table class=\"jnkTbl\" cellpadding=0 cellspacing=0><tr>");
			this.writer.Write("<td><input class=\"w100 txt\" type=\"text\" id=\"txt");
			this.writer.Write(groupId);
			this.writer.Write("\" name=\"txt");
			this.writer.Write(groupId);
			if (this.initialInputs.ContainsKey(groupId))
			{
				this.writer.Write("\" value=\"");
				Utilities.HtmlEncode(this.initialInputs[groupId], this.writer);
			}
			this.writer.Write("\" onkeypress=\"onKPIpt('");
			this.writer.Write(groupId);
			this.writer.Write("', event)\" onfocus=\"onFcsIpt('");
			this.writer.Write(groupId);
			this.writer.Write("')\" onblur=\"onBlrIpt('");
			this.writer.Write(groupId);
			this.writer.Write("')\"></td>");
			this.RenderButton(groupId, 292745765, "Add");
			this.writer.Write("</tr><tr>");
			this.writer.Write("<td class=\"sel\">");
			this.writer.Write("<select size=6 class=\"w100 sel\" multiple id=\"sel");
			this.writer.Write(groupId);
			this.writer.Write("\" name=\"sel");
			this.writer.Write(groupId);
			this.writer.Write("\" onchange=\"onChgEmSel('");
			this.writer.Write(groupId);
			this.writer.Write("')\" onfocus=\"onFcsEmSel('");
			this.writer.Write(groupId);
			this.writer.Write("')\" onkeydown=\"onKDEmSel('");
			this.writer.Write(groupId);
			this.writer.Write("', event)\" onkeypress=\"onKPEmSel('");
			this.writer.Write(groupId);
			this.writer.Write("', event)\">");
			emails.Sort();
			domains.Sort();
			for (int i = 0; i < emails.Count; i++)
			{
				this.writer.Write("<option value=\"");
				this.writer.Write(emails[i]);
				this.writer.Write("\">");
				this.writer.Write(emails[i]);
				this.writer.Write("</option>");
			}
			for (int j = 0; j < domains.Count; j++)
			{
				this.writer.Write("<option value=\"");
				this.writer.Write(domains[j]);
				this.writer.Write("\">");
				this.writer.Write(domains[j]);
				this.writer.Write("</option>");
			}
			this.writer.Write("</select></td>");
			this.writer.Write("<td valign=\"bottom\"><table class=\"btn\" cellpadding=0 cellspacing=0>");
			this.writer.Write("<tr>");
			this.RenderButton(groupId, 2119799890, "Edt");
			this.writer.Write("</tr>");
			this.writer.Write("<tr><td class=\"w2\"></td></tr>");
			this.writer.Write("<tr>");
			this.RenderButton(groupId, 1388922078, "Rmv");
			this.writer.Write("</tr>");
			this.writer.Write("</table></td>");
			this.writer.Write("</tr></table>");
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00016DD8 File Offset: 0x00014FD8
		private void RenderButton(string groupId, Strings.IDs label, string type)
		{
			this.writer.Write("<td class=\"btn\" nowrap><table cellpadding=0 cellspacing=0><tr><td><a href=\"#\" id=\"btn");
			this.writer.Write(groupId);
			this.writer.Write(type);
			this.writer.Write("\" class=\"disabled\" onClick=\"return onClk");
			this.writer.Write(type);
			this.writer.Write("Btn('");
			this.writer.Write(groupId);
			this.writer.Write("');\" title=\"");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(label));
			this.writer.Write("\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(label));
			this.writer.Write("</a></td></tr></table></td>");
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00016E98 File Offset: 0x00015098
		private void RenderLabeledInput(string type, string id, string name, string onclick, string value, bool isChecked, string label)
		{
			this.writer.Write("<input type=\"");
			this.writer.Write(type);
			if (id != null)
			{
				this.writer.Write("\" id=\"");
				this.writer.Write(id);
			}
			if (name != null)
			{
				this.writer.Write("\" name=\"");
				this.writer.Write(name);
			}
			this.writer.Write("\"");
			if (onclick != null)
			{
				this.writer.Write(" onclick=\"");
				this.writer.Write(onclick);
				this.writer.Write("\"");
			}
			if (value != null)
			{
				this.writer.Write(" value=\"");
				this.writer.Write(value);
				this.writer.Write("\"");
			}
			if (isChecked)
			{
				this.writer.Write(" checked");
			}
			this.writer.Write("><label for=\"");
			this.writer.Write(id);
			this.writer.Write("\">");
			this.writer.Write(label);
			this.writer.Write("</label>");
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00016FCE File Offset: 0x000151CE
		private void RenderCheckbox(string name, bool isChecked, string label)
		{
			this.RenderLabeledInput("checkbox", name, name, null, null, isChecked, label);
		}

		// Token: 0x040001CC RID: 460
		private const string JunkEmailEnabledParameter = "rdoJnk";

		// Token: 0x040001CD RID: 461
		private const string JunkEmailEnabledValue = "1";

		// Token: 0x040001CE RID: 462
		private const string JunkEmailEnabledJavaScriptVar = "a_iRdJnk";

		// Token: 0x040001CF RID: 463
		private const string IsContactsTrustedParameter = "chkTrstCnt";

		// Token: 0x040001D0 RID: 464
		private const string IsContactsTrustedJavaScriptVar = "a_fTrstCnt";

		// Token: 0x040001D1 RID: 465
		private const string SafeListsOnlyParameter = "chkSfOnly";

		// Token: 0x040001D2 RID: 466
		private const string SafeListsOnlyJavaScriptVar = "a_fSfOly";

		// Token: 0x040001D3 RID: 467
		private const string SafeSendersListParameter = "selSsl";

		// Token: 0x040001D4 RID: 468
		private const string BlockedSendersListParameter = "selBsl";

		// Token: 0x040001D5 RID: 469
		private const string SafeRecipientsListParameter = "selSrl";

		// Token: 0x040001D6 RID: 470
		private const string ListNameParameter = "hidlst";

		// Token: 0x040001D7 RID: 471
		private const string OldEmailParameter = "hidoldeml";

		// Token: 0x040001D8 RID: 472
		private const string SaveCommand = "save";

		// Token: 0x040001D9 RID: 473
		private const string AddCommand = "a";

		// Token: 0x040001DA RID: 474
		private const string EditComand = "e";

		// Token: 0x040001DB RID: 475
		private const string RemoveCommand = "r";

		// Token: 0x040001DC RID: 476
		private JunkEmailRule junkEmailRule;

		// Token: 0x040001DD RID: 477
		private bool isEnabled;

		// Token: 0x040001DE RID: 478
		private bool isContactsTrusted;

		// Token: 0x040001DF RID: 479
		private bool safeListsOnly;

		// Token: 0x040001E0 RID: 480
		private Dictionary<string, string> initialInputs = new Dictionary<string, string>();
	}
}
