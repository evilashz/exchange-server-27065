using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000469 RID: 1129
	public class ReadADDistributionList : ReadADRecipientPage, IRegistryOnlyForm
	{
		// Token: 0x06002A67 RID: 10855 RVA: 0x000ED26C File Offset: 0x000EB46C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.distributionList = (base.ADRecipient as IADDistributionList);
			if (this.distributionList == null)
			{
				throw new OwaInvalidRequestException();
			}
			this.sMimeEnabled = Utilities.IsClientSMimeControlUsable(Utilities.CheckClientSMimeControlStatus(Utilities.GetQueryStringParameter(base.Request, "smime", false), base.OwaContext));
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000ED2C6 File Offset: 0x000EB4C6
		protected bool SMimeEnabled
		{
			get
			{
				return this.sMimeEnabled;
			}
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000ED2CE File Offset: 0x000EB4CE
		protected void RenderListLink()
		{
			if (base.ADRecipient.RecipientType == RecipientType.Group)
			{
				Utilities.HtmlEncode(base.ADRecipient.Alias, base.Response.Output);
				return;
			}
			this.RenderRecipientLink();
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000ED300 File Offset: 0x000EB500
		protected void RenderOwnerLink()
		{
			if (this.distributionList.ManagedBy != null)
			{
				ADObjectId managedBy = this.distributionList.ManagedBy;
				ADRecipient adrecipient = base.ADRecipientSession.Read(managedBy);
				if (adrecipient != null)
				{
					base.Response.Write("<div class=\"row\"><div class=\"lbl\">" + LocalizedStrings.GetHtmlEncoded(-1563830359) + "</div><div class=\"fld\">");
					this.RenderMemberLink(adrecipient, false);
					base.Response.Write("</div></div>");
				}
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000ED374 File Offset: 0x000EB574
		protected void RenderMemberList()
		{
			List<OWARecipient> list = Utilities.LoadAndSortDistributionListMembers(this.distributionList, this.SMimeEnabled);
			foreach (OWARecipient owarecipient in list)
			{
				RecipientType userRecipientType = owarecipient.UserRecipientType;
				base.Response.Write("<tr>");
				base.Response.Write("<td class='adDlMmbrsLft'>");
				if (owarecipient.IsDistributionList)
				{
					base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.DistributionListOther);
				}
				else
				{
					base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.DistributionListUser);
				}
				base.Response.Write("</td>");
				base.Response.Write("<td class=adDlMmbrsRt");
				if (this.SMimeEnabled)
				{
					if (owarecipient.IsDistributionList)
					{
						base.Response.Write(" _em=\"");
						Utilities.HtmlEncode(owarecipient.LegacyDN, base.Response.Output);
						base.Response.Write('"');
					}
					else if (!owarecipient.HasValidDigitalId)
					{
						base.Response.Write(" _nc=1");
					}
				}
				base.Response.Write(">");
				this.RenderMemberLink(owarecipient, false);
				base.Response.Write("</td>");
				if (base.UserContext.IsPhoneticNamesEnabled)
				{
					base.Response.Write("<td class=adDlMmbrsRt>");
					this.RenderMemberLink(owarecipient, true);
					base.Response.Write("</td>");
				}
				base.Response.Write("</tr>");
			}
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000ED530 File Offset: 0x000EB730
		private void RenderMemberLink(ADRecipient recipient, bool usePhoneticName)
		{
			this.RenderMemberLink(recipient.Id, usePhoneticName ? recipient.PhoneticDisplayName : recipient.DisplayName, recipient.RecipientType);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000ED555 File Offset: 0x000EB755
		private void RenderMemberLink(OWARecipient recipient, bool usePhoneticName)
		{
			this.RenderMemberLink(recipient.Id, usePhoneticName ? recipient.PhoneticDisplayName : recipient.DisplayName, recipient.UserRecipientType);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000ED57C File Offset: 0x000EB77C
		private void RenderMemberLink(ADObjectId id, string displayName, RecipientType recipientType)
		{
			string s;
			if (Utilities.IsADDistributionList(recipientType))
			{
				s = "ADDistList";
			}
			else
			{
				s = "AD.RecipientType.User";
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = LocalizedStrings.GetNonEncoded(-808148510);
			}
			string base64StringFromADObjectId = Utilities.GetBase64StringFromADObjectId(id);
			string handlerCode = string.Format("openItmRdFm(\"{0}\",\"{1}\");", Utilities.JavascriptEncode(s), Utilities.JavascriptEncode(base64StringFromADObjectId));
			base.Response.Write("<a class=lnk ");
			base.Response.Write(Utilities.GetScriptHandler("onclick", handlerCode));
			base.Response.Write(">");
			Utilities.HtmlEncode(displayName, base.Response.Output);
			base.Response.Write("</a>");
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000ED628 File Offset: 0x000EB828
		private void RenderRecipientLink()
		{
			string handlerCode = string.Format("opnNwMsg(\"{0}\",\"{1}\",\"\",\"{2}\");", Utilities.JavascriptEncode(base.ADRecipient.LegacyExchangeDN), Utilities.JavascriptEncode(base.ADRecipient.DisplayName), Utilities.JavascriptEncode(2.ToString()));
			base.Response.Write("<a class=lnk ");
			Utilities.RenderScriptHandler(base.Response.Output, "onclick", handlerCode);
			base.Response.Write(">");
			Utilities.HtmlEncode(base.ADRecipient.Alias, base.Response.Output);
			base.Response.Write("</a>");
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000ED6D0 File Offset: 0x000EB8D0
		protected void RenderNotes()
		{
			RecipientType recipientType = base.ADRecipient.RecipientType;
			if (recipientType == RecipientType.Group || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailNonUniversalGroup)
			{
				ADGroup adgroup = (ADGroup)this.distributionList;
				if (!string.IsNullOrEmpty(adgroup.Notes))
				{
					base.Response.Write("<div class=\"row2sp\"><div class=\"secCol\"><span class=spS>" + LocalizedStrings.GetHtmlEncoded(1601836855) + "</span></div><div class=\"fltBefore\"><textarea class=\"adNts\" readonly>");
					Utilities.HtmlEncode(adgroup.Notes, base.Response.Output);
					base.Response.Write("</textarea></div>");
				}
			}
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000ED760 File Offset: 0x000EB960
		protected void RenderSecureMessaging()
		{
			if (!this.SMimeEnabled || base.ADRecipient.RecipientType == RecipientType.DynamicDistributionGroup)
			{
				return;
			}
			bool hiddenGroupMembershipEnabled = ((ADGroup)base.ADRecipient).HiddenGroupMembershipEnabled;
			base.Response.Write("<div class=\"row2sp\"><div class=\"secCol\"><span class=spS>");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-2096722623));
			base.Response.Write("</span></div><div class=\"lbl noindent\"");
			if (!hiddenGroupMembershipEnabled)
			{
				base.Response.Write(" id=tdSM");
			}
			base.Response.Write('>');
			if (hiddenGroupMembershipEnabled)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(2141668304));
			}
			else
			{
				base.Response.Write("<span id=spGCPrg style=\"display:none\"><span class=t><img src=\"");
				base.UserContext.RenderThemeFileUrl(base.Response.Output, ThemeFileId.ProgressSmall);
				base.Response.Write("\"></span> <span id=\"spSPS\">");
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-695375226));
				base.Response.Write("</span></span>");
			}
			base.Response.Write("</div></div>");
		}

		// Token: 0x04001C98 RID: 7320
		private IADDistributionList distributionList;

		// Token: 0x04001C99 RID: 7321
		private bool sMimeEnabled;
	}
}
