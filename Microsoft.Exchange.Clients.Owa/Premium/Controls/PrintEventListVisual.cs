using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003ED RID: 1005
	internal class PrintEventListVisual : PrintWeeklyAgendaVisual
	{
		// Token: 0x060024CB RID: 9419 RVA: 0x000D5808 File Offset: 0x000D3A08
		public PrintEventListVisual(ISessionContext sessionContext, int index, ICalendarDataSource dataSource, bool isFirst) : base(sessionContext, index, dataSource, isFirst)
		{
			if (base.SessionContext is UserContext)
			{
				this.invitees = dataSource.GetInviteesDisplayNames(index);
				OwaStoreObjectId itemId = dataSource.GetItemId(index);
				if (itemId == null)
				{
					return;
				}
				using (Item item = Utilities.GetItem<Item>((UserContext)base.SessionContext, itemId, new PropertyDefinition[0]))
				{
					using (TextReader textReader = item.Body.OpenTextReader(BodyFormat.TextPlain))
					{
						this.notes = textReader.ReadToEnd();
					}
					return;
				}
			}
			PublishedCalendarDataSource publishedCalendarDataSource = (PublishedCalendarDataSource)dataSource;
			if (publishedCalendarDataSource.DetailLevel == DetailLevelEnumType.FullDetails)
			{
				PublishedCalendarItemData? item2 = publishedCalendarDataSource.GetItem(index);
				if (item2 != null)
				{
					this.notes = item2.Value.BodyText;
				}
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000D58F8 File Offset: 0x000D3AF8
		public override void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.isFirstRow = true;
			this.RenderProperty(writer, this.TimeDescription, new string[]
			{
				base.Subject
			});
			this.RenderProperty(writer, -1134349396, new string[]
			{
				base.Location
			});
			this.RenderProperty(writer, -2098425358, new string[]
			{
				base.Organizer
			});
			if (base.SessionContext is UserContext)
			{
				this.RenderProperty(writer, 1379219406, new string[]
				{
					this.invitees
				});
			}
			if (!base.IsPrivate)
			{
				this.RenderProperty(writer, -2053657454, new string[]
				{
					"<pre>",
					this.notes,
					"</pre>"
				});
			}
			writer.Write("<tr><td>&nbsp;</td></tr>");
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000D59E1 File Offset: 0x000D3BE1
		private void RenderProperty(TextWriter writer, Strings.IDs captionId, params string[] values)
		{
			this.RenderProperty(writer, LocalizedStrings.GetNonEncoded(captionId), values);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000D59F4 File Offset: 0x000D3BF4
		private void RenderProperty(TextWriter writer, string caption, params string[] values)
		{
			writer.Write("<tr><td class=\"eventListFBIcon\">");
			if (this.isFirstRow)
			{
				base.RenderFreeBusy(writer, true);
			}
			writer.Write("</td><td><span class=\"eventListCaption\">");
			writer.Write(caption);
			if (this.isFirstRow)
			{
				base.RenderIcons(writer, true);
			}
			writer.Write(" ");
			writer.Write("</span>");
			writer.Write(base.SessionContext.GetDirectionMark());
			foreach (string value in values)
			{
				writer.Write(value);
			}
			writer.Write("</td></tr>");
			this.isFirstRow = false;
		}

		// Token: 0x04001988 RID: 6536
		private string invitees;

		// Token: 0x04001989 RID: 6537
		private string notes = string.Empty;

		// Token: 0x0400198A RID: 6538
		private bool isFirstRow = true;
	}
}
