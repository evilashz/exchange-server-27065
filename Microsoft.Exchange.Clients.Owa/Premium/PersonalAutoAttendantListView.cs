using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003DD RID: 989
	internal class PersonalAutoAttendantListView
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x000D3965 File Offset: 0x000D1B65
		internal PersonalAutoAttendantListView(UserContext userContext, IList<PersonalAutoAttendant> personalAutoAttendants)
		{
			this.personalAutoAttendants = personalAutoAttendants;
			this.userContext = userContext;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000D397C File Offset: 0x000D1B7C
		internal void Render(TextWriter writer)
		{
			writer.Write("<table class=\"dlIL\" id=\"tblIL\">");
			int num = 1;
			foreach (PersonalAutoAttendant personalAutoAttendant in this.personalAutoAttendants)
			{
				bool isCompatible = personalAutoAttendant.IsCompatible;
				writer.Write("<tr id=\"us\" _id=\"");
				Utilities.HtmlEncode(Convert.ToBase64String(personalAutoAttendant.Identity.ToByteArray()), writer);
				writer.Write("\" isUSpt=");
				writer.Write(isCompatible ? 0 : 1);
				string value = null;
				if (personalAutoAttendant.Valid && isCompatible)
				{
					value = UnifiedMessagingUtilities.CreatePAAPreviewString(personalAutoAttendant, this.userContext);
				}
				else if (!personalAutoAttendant.Valid)
				{
					value = LocalizedStrings.GetHtmlEncoded(2099558169);
				}
				else if (!isCompatible)
				{
					value = LocalizedStrings.GetHtmlEncoded(1688035379);
				}
				writer.Write(" _ps=\"");
				writer.Write(value);
				writer.Write("\"><td class=\"c paaOrd\"><div class=\"paaOrd\">");
				writer.Write(num++);
				writer.Write("</div></td><td class=\"c paaChk\"><div class=\"paaChk\">");
				if (personalAutoAttendant.Valid)
				{
					writer.Write("<input tabindex=-1 id=chkEnb type=checkbox class=rulChk ");
					Utilities.RenderScriptHandler(writer, "onclick", "onEnbl(_this);");
					if (personalAutoAttendant.Enabled)
					{
						writer.Write(" checked");
					}
					writer.Write(">");
				}
				else
				{
					this.userContext.RenderThemeImage(writer, ThemeFileId.Error2);
				}
				writer.Write("</div></td><td title=\"");
				Utilities.HtmlEncode(personalAutoAttendant.Name, writer);
				writer.Write("\" class=\"d w100");
				if (!isCompatible)
				{
					writer.Write(" rulNS");
				}
				else if (!personalAutoAttendant.Valid)
				{
					writer.Write(" rulErr");
				}
				writer.Write("\" nowrap>");
				Utilities.HtmlEncode(personalAutoAttendant.Name, writer);
				writer.Write("</td>");
				writer.Write("</tr>");
			}
			writer.Write("</table>");
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000D3B70 File Offset: 0x000D1D70
		protected void RenderPersonalAutoAttendantPreviewString(TextWriter writer, PersonalAutoAttendant personalAutoAttendant)
		{
			writer.Write(UnifiedMessagingUtilities.CreatePAAPreviewString(personalAutoAttendant, this.userContext));
		}

		// Token: 0x04001940 RID: 6464
		private UserContext userContext;

		// Token: 0x04001941 RID: 6465
		private IList<PersonalAutoAttendant> personalAutoAttendants;
	}
}
