using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200006F RID: 111
	internal class MRRSelect
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0001B234 File Offset: 0x00019434
		public static void Render(MRRSelect.Type type, RecipientCache recipientCache, TextWriter writer)
		{
			if (recipientCache == null)
			{
				throw new ArgumentNullException("recipientCache");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int value = 3;
			int value2 = 19;
			int value3 = 1;
			bool flag = false;
			string value4 = "selmrr";
			if (type == MRRSelect.Type.CalendarRecipients)
			{
				value = 2;
			}
			else if (type == MRRSelect.Type.Resources)
			{
				value = 2;
				value2 = 10;
				value3 = 3;
				flag = true;
				value4 = "selmrs";
			}
			writer.Write("<table cellspacing=4 cellpadding=0 class=\"rcntRcpt\">");
			if (type != MRRSelect.Type.Resources)
			{
				writer.Write("<tr><td colspan=");
				writer.Write(value);
				writer.Write(" class=\"hd\">");
				writer.Write("<label for=selmrr>");
				writer.Write(LocalizedStrings.GetHtmlEncoded(131996110));
				writer.Write("</label></td></tr>");
			}
			writer.Write("<tr><td colspan=");
			writer.Write(value);
			writer.Write(" class=\"");
			bool flag2 = false;
			if (recipientCache.CacheLength > 0)
			{
				bool flag3 = false;
				for (int i = 0; i < recipientCache.CacheLength; i++)
				{
					bool flag4 = (recipientCache.CacheEntries[i].RecipientFlags & 2) != 0;
					if (flag4 == flag)
					{
						flag2 = true;
						if (!flag3)
						{
							writer.Write("sl\"><select size=");
							writer.Write(value2);
							writer.Write(" id=\"");
							writer.Write(value4);
							writer.Write("\"");
							if (type == MRRSelect.Type.Resources)
							{
								writer.Write(" title=\"");
								writer.Write(LocalizedStrings.GetHtmlEncoded(1734490793));
								writer.Write("\"");
							}
							writer.Write(" multiple onkeyup=\"return onKUMRR(");
							writer.Write(value3);
							writer.Write(", event);\" onDblClick=\"return onDbClkAddMRR(");
							writer.Write(value3);
							writer.Write(");\">");
							flag3 = true;
						}
						MRRSelect.RenderMostRecentRecipientEntry(writer, recipientCache.CacheEntries[i]);
					}
				}
				if (flag3)
				{
					writer.Write("</select>");
				}
			}
			if (!flag2)
			{
				writer.Write("msg\" align=\"center\">");
				if (type == MRRSelect.Type.Resources)
				{
					writer.Write(LocalizedStrings.GetHtmlEncoded(-821027971));
				}
				else
				{
					writer.Write(LocalizedStrings.GetHtmlEncoded(-320187802));
				}
			}
			writer.Write("</td></tr><tr>");
			switch (type)
			{
			case MRRSelect.Type.CalendarRecipients:
				MRRSelect.RenderButton(956546969, RecipientItemType.To, writer);
				MRRSelect.RenderButton(401962758, RecipientItemType.Cc, writer);
				break;
			case MRRSelect.Type.Resources:
				writer.Write("<td class=\"w50\">&nbsp;</td>");
				MRRSelect.RenderButton(2051574762, RecipientItemType.Bcc, writer);
				break;
			default:
				MRRSelect.RenderButton(-269710455, RecipientItemType.To, writer);
				MRRSelect.RenderButton(2055888382, RecipientItemType.Cc, writer);
				MRRSelect.RenderButton(198978688, RecipientItemType.Bcc, writer);
				break;
			}
			writer.Write("</tr>");
			if (type == MRRSelect.Type.MessageRecipients && flag2)
			{
				writer.Write("<tr><td colspan=");
				writer.Write(value);
				writer.Write(" class=\"drp\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(-290151629));
				writer.Write("</td></tr>");
			}
			writer.Write("</table>");
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001B504 File Offset: 0x00019704
		private static void RenderButton(Strings.IDs labelStringId, RecipientItemType recipientItemType, TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<td class=\"btn\" align=\"center\" nowrap><a href=\"#\" onClick=\"return onClkAddMRR(");
			writer.Write((int)recipientItemType);
			writer.Write(");\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(labelStringId));
			writer.Write("</a></td>");
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001B554 File Offset: 0x00019754
		private static void RenderMostRecentRecipientEntry(TextWriter writer, RecipientInfoCacheEntry entry)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			ResolvedRecipientDetail resolvedRecipientDetail = new ResolvedRecipientDetail(entry);
			writer.Write("<option title=\"");
			Utilities.HtmlEncode(resolvedRecipientDetail.SmtpAddress, writer);
			writer.Write("\"");
			writer.Write(" value=\"");
			resolvedRecipientDetail.RenderConcatenatedDetails(false, writer);
			writer.Write("\">");
			string text = string.Empty;
			if (!string.IsNullOrEmpty(resolvedRecipientDetail.DisplayName))
			{
				text = resolvedRecipientDetail.DisplayName;
			}
			else if (!string.IsNullOrEmpty(resolvedRecipientDetail.SmtpAddress))
			{
				text = resolvedRecipientDetail.SmtpAddress;
			}
			else if (!string.IsNullOrEmpty(resolvedRecipientDetail.RoutingAddress))
			{
				text = resolvedRecipientDetail.RoutingAddress;
			}
			Utilities.CropAndRenderText(writer, text, 32);
			writer.Write("</option>");
		}

		// Token: 0x04000240 RID: 576
		private const int MostRecentRecipientsSelectSize = 19;

		// Token: 0x04000241 RID: 577
		private const int MostRecentResourcesSelectSize = 10;

		// Token: 0x04000242 RID: 578
		private const string MrrId = "selmrr";

		// Token: 0x04000243 RID: 579
		private const string MrsId = "selmrs";

		// Token: 0x02000070 RID: 112
		public enum Type
		{
			// Token: 0x04000245 RID: 581
			MessageRecipients,
			// Token: 0x04000246 RID: 582
			CalendarRecipients,
			// Token: 0x04000247 RID: 583
			Resources
		}
	}
}
