using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200006A RID: 106
	public class MessagingOptions : OptionsBase
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x000191C3 File Offset: 0x000173C3
		public MessagingOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.CommitAndLoad();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000191DC File Offset: 0x000173DC
		private void Load()
		{
			this.displayItemsPerPage = this.userContext.UserOptions.BasicViewRowCount;
			this.nextSelection = this.userContext.UserOptions.NextSelection;
			if (this.userContext.IsFeatureEnabled(Feature.Signature))
			{
				this.autoAddSignature = this.userContext.UserOptions.AutoAddSignature;
				this.signatureText = this.userContext.UserOptions.SignatureText;
				if (!Utilities.WhiteSpaceOnlyOrNullEmpty(this.signatureText))
				{
					this.signatureHtml = this.userContext.UserOptions.SignatureHtml;
				}
			}
			this.readReceiptResponse = this.userContext.UserOptions.ReadReceipt;
			this.checkNameInContactsFirst = this.userContext.UserOptions.CheckNameInContactsFirst;
			this.addRecentRecipientsToMrr = this.userContext.UserOptions.AddRecipientsToAutoCompleteCache;
			this.emptyDeletedItemsOnLogoff = this.userContext.UserOptions.EmptyDeletedItemsOnLogoff;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000192CF File Offset: 0x000174CF
		public override void Render()
		{
			this.RenderMessagingOptions();
			if (this.userContext.IsFeatureEnabled(Feature.Signature))
			{
				this.RenderEmailSignature();
			}
			this.RenderMessageTracking();
			this.RenderEmailNameResolution();
			this.RenderDeletedItems();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00019304 File Offset: 0x00017504
		public override void RenderScript()
		{
			base.RenderJSVariable("a_iRwCnt", this.userContext.UserOptions.BasicViewRowCount.ToString());
			base.RenderJSVariable("a_iNxt", ((int)this.userContext.UserOptions.NextSelection).ToString());
			if (this.userContext.IsFeatureEnabled(Feature.Signature))
			{
				base.RenderJSVariable("a_fAddSg", this.userContext.UserOptions.AutoAddSignature);
				base.RenderJSVariable("a_fSgPr", !string.IsNullOrEmpty(this.signatureHtml));
			}
			base.RenderJSVariable("a_fEDI", this.userContext.UserOptions.EmptyDeletedItemsOnLogoff);
			base.RenderJSVariable("a_iRdRcpt", ((int)this.userContext.UserOptions.ReadReceipt).ToString());
			base.RenderJSVariable("a_fAddMRR", this.userContext.UserOptions.AddRecipientsToAutoCompleteCache);
			if (this.userContext.IsFeatureEnabled(Feature.Contacts))
			{
				base.RenderJSVariable("a_iAnrFst", this.userContext.UserOptions.CheckNameInContactsFirst ? "1" : "0");
			}
			base.RenderJSVariableWithQuotes("a_sPrClrMRR", LocalizedStrings.GetNonEncoded(136482375));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00019444 File Offset: 0x00017644
		private void CommitAndLoad()
		{
			this.Load();
			if (Utilities.IsPostRequest(this.request) && !string.IsNullOrEmpty(base.Command))
			{
				if (base.Command.Equals("ClrMRR", StringComparison.Ordinal))
				{
					AutoCompleteCache autoCompleteCache = AutoCompleteCache.TryGetCache(OwaContext.Current.UserContext);
					if (autoCompleteCache != null)
					{
						autoCompleteCache.ClearCache();
						autoCompleteCache.Commit(false);
					}
					this.isClearMrrRequest = true;
				}
				string formParameter = Utilities.GetFormParameter(this.request, "selRwCnt");
				if (string.IsNullOrEmpty(formParameter) || !int.TryParse(formParameter, out this.displayItemsPerPage))
				{
					throw new OwaInvalidRequestException("Row count must be a valid number");
				}
				string formParameter2 = Utilities.GetFormParameter(this.request, "selNxt");
				int num;
				if (string.IsNullOrEmpty(formParameter2) || !int.TryParse(formParameter2, out num) || num < 0 || num > 2)
				{
					throw new OwaInvalidRequestException("Next selection must be a valid number");
				}
				this.nextSelection = (NextSelectionDirection)num;
				if (this.userContext.IsFeatureEnabled(Feature.Signature))
				{
					this.autoAddSignature = (Utilities.GetFormParameter(this.request, "chkAddSg", false) != null);
					this.signatureText = Utilities.GetFormParameter(this.request, "txtSg", false);
				}
				this.emptyDeletedItemsOnLogoff = (Utilities.GetFormParameter(this.request, "chkEmDel", false) != null);
				string formParameter3 = Utilities.GetFormParameter(this.request, "rdRcpt", false);
				if (!string.IsNullOrEmpty(formParameter3))
				{
					this.readReceiptResponse = (ReadReceiptResponse)int.Parse(formParameter3);
				}
				this.addRecentRecipientsToMrr = (Utilities.GetFormParameter(this.request, "chkAddMRR", false) != null);
				if (this.userContext.IsFeatureEnabled(Feature.Contacts))
				{
					string formParameter4 = Utilities.GetFormParameter(this.request, "anrFst");
					this.checkNameInContactsFirst = (!string.IsNullOrEmpty(formParameter4) && formParameter4 == "1");
				}
				if (!this.isClearMrrRequest)
				{
					bool flag = false;
					if (this.displayItemsPerPage != this.userContext.UserOptions.BasicViewRowCount)
					{
						this.userContext.UserOptions.BasicViewRowCount = this.displayItemsPerPage;
						flag = true;
					}
					if (this.nextSelection != this.userContext.UserOptions.NextSelection)
					{
						this.userContext.UserOptions.NextSelection = this.nextSelection;
						flag = true;
					}
					if (this.userContext.IsFeatureEnabled(Feature.Signature))
					{
						if (this.autoAddSignature != this.userContext.UserOptions.AutoAddSignature)
						{
							this.userContext.UserOptions.AutoAddSignature = this.autoAddSignature;
							flag = true;
						}
						if (!Utilities.WhiteSpaceOnlyOrNullEmpty(this.signatureText) && (Utilities.WhiteSpaceOnlyOrNullEmpty(this.userContext.UserOptions.SignatureText) || !string.IsNullOrEmpty(Utilities.GetFormParameter(this.request, "chkRplSg", false))))
						{
							this.userContext.UserOptions.SignatureText = this.signatureText;
							this.signatureHtml = BodyConversionUtilities.ConvertTextToHtml(this.signatureText);
							this.userContext.UserOptions.SignatureHtml = this.signatureHtml;
							flag = true;
						}
					}
					if (this.userContext.UserOptions.EmptyDeletedItemsOnLogoff != this.emptyDeletedItemsOnLogoff)
					{
						this.userContext.UserOptions.EmptyDeletedItemsOnLogoff = this.emptyDeletedItemsOnLogoff;
						flag = true;
					}
					if (this.userContext.UserOptions.ReadReceipt != this.readReceiptResponse)
					{
						this.userContext.UserOptions.ReadReceipt = this.readReceiptResponse;
						flag = true;
					}
					if (this.userContext.UserOptions.AddRecipientsToAutoCompleteCache != this.addRecentRecipientsToMrr)
					{
						this.userContext.UserOptions.AddRecipientsToAutoCompleteCache = this.addRecentRecipientsToMrr;
						flag = true;
					}
					if (this.userContext.IsFeatureEnabled(Feature.Contacts) && this.userContext.UserOptions.CheckNameInContactsFirst != this.checkNameInContactsFirst)
					{
						this.userContext.UserOptions.CheckNameInContactsFirst = this.checkNameInContactsFirst;
						flag = true;
					}
					if (flag)
					{
						try
						{
							this.userContext.UserOptions.CommitChanges();
							base.SetSavedSuccessfully(true);
						}
						catch (StoragePermanentException)
						{
							base.SetSavedSuccessfully(false);
						}
						catch (StorageTransientException)
						{
							base.SetSavedSuccessfully(false);
						}
					}
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001985C File Offset: 0x00017A5C
		private void RenderMessagingOptions()
		{
			base.RenderHeaderRow(ThemeFileId.EMailLarge, 1847724788);
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\">");
			this.writer.Write("<tr><td nowrap>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1884547222));
			this.writer.Write("</td><td class=\"w100\">");
			this.RenderViewerRowCount();
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td nowrap>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(888173523));
			this.writer.Write("</td><td class=\"w100\">");
			this.RenderNextSelection();
			this.writer.Write("</td></tr>");
			this.writer.Write("</table>");
			this.writer.Write("</td></tr>");
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001994C File Offset: 0x00017B4C
		private void RenderViewerRowCount()
		{
			int num = 100;
			this.writer.Write("<select name=\"");
			this.writer.Write("selRwCnt");
			this.writer.Write("\" onchange=\"onChgSel(this);\">");
			int i;
			for (i = 5; i < 50; i += 5)
			{
				if (i >= num)
				{
					break;
				}
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (i == this.displayItemsPerPage) ? " selected" : string.Empty, i, i);
			}
			while (i <= num)
			{
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (i == this.displayItemsPerPage) ? " selected" : string.Empty, i, i);
				i += 25;
			}
			this.writer.Write("</select>");
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00019A1C File Offset: 0x00017C1C
		private void RenderNextSelection()
		{
			this.writer.Write("<select name=\"");
			this.writer.Write("selNxt");
			this.writer.Write("\" onchange=\"onChgSel(this);\">");
			this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (this.nextSelection == NextSelectionDirection.Previous) ? " selected" : string.Empty, 0, LocalizedStrings.GetHtmlEncoded(1192767596));
			this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (this.nextSelection == NextSelectionDirection.Next) ? " selected" : string.Empty, 1, LocalizedStrings.GetHtmlEncoded(-1347357868));
			this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (this.nextSelection == NextSelectionDirection.ReturnToView) ? " selected" : string.Empty, 2, LocalizedStrings.GetHtmlEncoded(21771688));
			this.writer.Write("</select>");
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00019B08 File Offset: 0x00017D08
		private void RenderEmailSignature()
		{
			base.RenderHeaderRow(ThemeFileId.SignatureLarge, -735243648);
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<input type=\"checkbox\" name=\"{0}\"{1} id=\"{0}\" onclick=\"return onClkChkBx(this);\" value=\"1\"><label for=\"{0}\">{2}</label>", "chkAddSg", this.autoAddSignature ? " checked" : string.Empty, LocalizedStrings.GetHtmlEncoded(780915179));
			this.writer.Write("</td></tr>");
			if (!string.IsNullOrEmpty(this.signatureHtml))
			{
				this.writer.Write("<tr><td>");
				this.writer.Write("<table class=\"csg\" cellpadding=0 cellspacing=0>");
				this.writer.Write("<tr><td class=\"dsl\" rowspan=3><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\" class=\"wh1\"></td><td class=\"w100\"></td><td class=\"dsr\" rowspan=3><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\" class=\"wh1\"></td></tr>");
				this.writer.Write("<tr><td class=\"ds\">");
				this.writer.Write(Utilities.SanitizeHtml(this.signatureHtml));
				this.writer.Write("</td></tr>");
				this.writer.Write("<tr><td></td></tr></table>");
				this.writer.Write("<table class=\"csgnb\"><tr><td class=\"df\"><input type=\"checkbox\" name=\"");
				this.writer.Write("chkRplSg");
				this.writer.Write("\" id=\"");
				this.writer.Write("chkRplSg");
				this.writer.Write("\" onclick=\"return onClkChkBx(this);\" value=\"1\"");
				this.writer.Write((this.isClearMrrRequest && Utilities.GetFormParameter(this.request, "chkRplSg", false) != null) ? " checked" : string.Empty);
				this.writer.Write("><label for=\"");
				this.writer.Write("chkRplSg");
				this.writer.Write("\">");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1257264716));
				this.writer.Write("</label></td></tr>");
				this.writer.Write("<tr><td class=\"w100\">");
				this.writer.Write("<textarea name=\"");
				this.writer.Write("txtSg");
				this.writer.Write("\" class=\"w100\" title=\"");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-977498918));
				this.writer.Write("\" rows=6 cols=61 onfocus=\"onFcsTxt('");
				this.writer.Write("chkRplSg");
				this.writer.Write("');\">");
				if (this.isClearMrrRequest && !string.IsNullOrEmpty(this.signatureText))
				{
					Utilities.HtmlEncode(this.signatureText, this.writer);
				}
				this.writer.Write("</textarea>");
				this.writer.Write("</td></tr>");
				this.writer.Write("</table></td></tr>");
				return;
			}
			this.writer.Write("<tr><td>");
			this.writer.Write("<table class=\"csg\" cellpadding=0 cellspacing=0>");
			this.writer.Write("<tr><td class=\"w100\">");
			this.writer.Write("<textarea name=\"");
			this.writer.Write("txtSg");
			this.writer.Write("\" class=\"w100\" title=\"");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-977498918));
			this.writer.Write("\" rows=6 cols=61>");
			if (this.isClearMrrRequest && !string.IsNullOrEmpty(this.signatureText))
			{
				Utilities.HtmlEncode(this.signatureText, this.writer);
			}
			this.writer.Write("</textarea>");
			this.writer.Write("</table></td></tr>");
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00019EC4 File Offset: 0x000180C4
		private void RenderDeletedItems()
		{
			base.RenderHeaderRow(ThemeFileId.EmptyDeletedItems, -681344097);
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<input type=\"checkbox\" name=\"{0}\"{1} id=\"{0}\" onclick=\"return onClkChkBx(this);\" value=\"1\"><label for=\"{0}\">{2}</label>", "chkEmDel", this.emptyDeletedItemsOnLogoff ? " checked" : string.Empty, LocalizedStrings.GetHtmlEncoded(1812531684));
			this.writer.Write("</td></tr>");
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00019F34 File Offset: 0x00018134
		private void RenderMessageTracking()
		{
			base.RenderHeaderRow(ThemeFileId.EMailExtraLarge, 1399211590);
			this.writer.Write("<tr><td class=\"bd\"><table class=\"fmt\">");
			this.writer.Write("<tr><td><input type=\"radio\" id=\"rdoAskRsp\" name=\"rdRcpt\" value=\"0\"");
			if (this.readReceiptResponse == ReadReceiptResponse.DoNotAutomaticallySend)
			{
				this.writer.Write(" checked");
			}
			this.writer.Write("><label for=\"rdoAskRsp\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-416380764));
			this.writer.Write("</label></td></tr>");
			this.writer.Write("<tr><td><input type=\"radio\" id=\"rdoYesRsp\" name=\"rdRcpt\" value=\"1\"");
			if (this.readReceiptResponse == ReadReceiptResponse.AlwaysSend)
			{
				this.writer.Write(" checked");
			}
			this.writer.Write("><label for=\"rdoYesRsp\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1477686773));
			this.writer.Write("</label></td></tr>");
			this.writer.Write("<tr><td><input type=\"radio\" id=\"rdoNoRsp\" name=\"rdRcpt\" value=\"2\"");
			if (this.readReceiptResponse == ReadReceiptResponse.NeverSend)
			{
				this.writer.Write(" checked");
			}
			this.writer.Write("><label for=\"rdoNoRsp\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1975510762));
			this.writer.Write("</label></td></tr>");
			this.writer.Write("</table></td></tr>");
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001A088 File Offset: 0x00018288
		private void RenderEmailNameResolution()
		{
			base.RenderHeaderRow(ThemeFileId.AnrOptions, 780065525);
			this.writer.Write("<tr><td class=\"bd\"><table class=\"fmt\">");
			this.writer.Write("<tr><td><input type=\"checkbox\" id=\"chkAddMRR\" name=\"chkAddMRR\"");
			if (this.addRecentRecipientsToMrr)
			{
				this.writer.Write(" checked");
			}
			this.writer.Write("><label for=\"chkAddMRR\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1675794646));
			this.writer.Write("</label></td></tr>");
			this.writer.Write("<tr><td id=\"cmrr\"><a href=\"#\" onclick=\"return onClkClrMRR();\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(480488025));
			this.writer.Write("</a></td></tr></table></td></tr>");
			if (this.userContext.IsFeatureEnabled(Feature.Contacts))
			{
				this.writer.Write("<tr><td class=\"bd\"><table class=\"fmt\"><tr><td>");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1914279991));
				this.writer.Write("</td></tr>");
				this.writer.Write("<tr><td><input type=\"radio\" id=\"rdoGAL\" name=\"anrFst\" value=\"0\"");
				if (!this.checkNameInContactsFirst)
				{
					this.writer.Write(" checked");
				}
				this.writer.Write("><label for=\"rdoGAL\">");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(1164140307));
				this.writer.Write("</label></td></tr>");
				this.writer.Write("<tr><td><input type=\"radio\" id=\"rdoCtcts\" name=\"anrFst\" value=\"1\"");
				if (this.checkNameInContactsFirst)
				{
					this.writer.Write(" checked");
				}
				this.writer.Write("><label for=\"rdoCtcts\">");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(1716044995));
				this.writer.Write("</label></td></tr></table></td></tr>");
			}
		}

		// Token: 0x0400021A RID: 538
		private const string FormRowCount = "selRwCnt";

		// Token: 0x0400021B RID: 539
		private const string FormJavaScriptRowCount = "a_iRwCnt";

		// Token: 0x0400021C RID: 540
		private const string FormNextSelection = "selNxt";

		// Token: 0x0400021D RID: 541
		private const string FormJavaScriptNextSelection = "a_iNxt";

		// Token: 0x0400021E RID: 542
		private const string FormAddSignature = "chkAddSg";

		// Token: 0x0400021F RID: 543
		private const string FormJavaScriptAddSignature = "a_fAddSg";

		// Token: 0x04000220 RID: 544
		private const string FormReplaceSignature = "chkRplSg";

		// Token: 0x04000221 RID: 545
		private const string FormReplaceSignatureText = "txtSg";

		// Token: 0x04000222 RID: 546
		private const string FormEmptyDeletedItemsOnLogOff = "chkEmDel";

		// Token: 0x04000223 RID: 547
		private const string FormJavaScriptEmptyDeletedItemsOnLogOff = "a_fEDI";

		// Token: 0x04000224 RID: 548
		private const string FormJavaScriptSignaturePresent = "a_fSgPr";

		// Token: 0x04000225 RID: 549
		private const string FormReadReceipt = "rdRcpt";

		// Token: 0x04000226 RID: 550
		private const string FormJavaScriptReadReceipt = "a_iRdRcpt";

		// Token: 0x04000227 RID: 551
		private const string FormAddToMrr = "chkAddMRR";

		// Token: 0x04000228 RID: 552
		private const string FormJavaScriptAddToMrr = "a_fAddMRR";

		// Token: 0x04000229 RID: 553
		private const string FormAnrFirst = "anrFst";

		// Token: 0x0400022A RID: 554
		private const string FormJavaScriptAnrFirst = "a_iAnrFst";

		// Token: 0x0400022B RID: 555
		private const string FormJavaScriptClearMrrPrompt = "a_sPrClrMRR";

		// Token: 0x0400022C RID: 556
		private const string AnrInContactFirstValue = "1";

		// Token: 0x0400022D RID: 557
		private const string ClearMostRecentReceiptCommand = "ClrMRR";

		// Token: 0x0400022E RID: 558
		private const string Option = "<option value=\"{1}\"{0}>{2}</option>";

		// Token: 0x0400022F RID: 559
		private const string FormCheckBox = "<input type=\"checkbox\" name=\"{0}\"{1} id=\"{0}\" onclick=\"return onClkChkBx(this);\" value=\"1\"><label for=\"{0}\">{2}</label>";

		// Token: 0x04000230 RID: 560
		private int displayItemsPerPage;

		// Token: 0x04000231 RID: 561
		private NextSelectionDirection nextSelection = NextSelectionDirection.Next;

		// Token: 0x04000232 RID: 562
		private bool autoAddSignature;

		// Token: 0x04000233 RID: 563
		private string signatureText;

		// Token: 0x04000234 RID: 564
		private string signatureHtml;

		// Token: 0x04000235 RID: 565
		private ReadReceiptResponse readReceiptResponse;

		// Token: 0x04000236 RID: 566
		private bool checkNameInContactsFirst;

		// Token: 0x04000237 RID: 567
		private bool addRecentRecipientsToMrr;

		// Token: 0x04000238 RID: 568
		private bool emptyDeletedItemsOnLogoff;

		// Token: 0x04000239 RID: 569
		private bool isClearMrrRequest;
	}
}
