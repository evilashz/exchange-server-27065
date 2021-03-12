using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002CD RID: 717
	internal class ListViewContentsRenderingUtilities
	{
		// Token: 0x06001BD4 RID: 7124 RVA: 0x000A06F7 File Offset: 0x0009E8F7
		public static bool RenderItemIcon(TextWriter writer, UserContext userContext, ThemeFileId themeFileId)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			userContext.RenderThemeImageWithToolTip(writer, themeFileId, "sI", new string[0]);
			return true;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000A0729 File Offset: 0x0009E929
		public static bool RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass)
		{
			return ListViewContentsRenderingUtilities.RenderItemIcon(writer, userContext, itemClass, null);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000A0734 File Offset: 0x0009E934
		public static bool RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, string defaultItemClass)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (itemClass == null)
			{
				return false;
			}
			SmallIconManager.SmallIcon itemSmallIcon = SmallIconManager.GetItemSmallIcon(itemClass, defaultItemClass, false, false, -1);
			userContext.RenderThemeImageWithToolTip(writer, (ThemeFileId)itemSmallIcon.ThemeId, "sI", itemSmallIcon.AltId, new string[0]);
			return true;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000A078C File Offset: 0x0009E98C
		public static bool RenderMessageIcon(TextWriter writer, UserContext userContext, string itemClass, bool isRead, bool isInConflict, int iconFlag)
		{
			return ListViewContentsRenderingUtilities.RenderMessageIcon(writer, userContext, itemClass, isRead, isInConflict, iconFlag, false);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000A079C File Offset: 0x0009E99C
		public static bool RenderMessageIcon(TextWriter writer, UserContext userContext, string itemClass, bool isRead, bool isInConflict, int iconFlag, bool isIrmProtected)
		{
			if (itemClass == null)
			{
				return false;
			}
			if (string.Equals(itemClass, "IPM.Note", StringComparison.InvariantCultureIgnoreCase) && isIrmProtected)
			{
				itemClass += ".irm";
			}
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, false, null, isInConflict, isRead, iconFlag, string.Empty, new string[0]);
			return true;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000A07E8 File Offset: 0x0009E9E8
		public static bool RenderTaskIcon(TextWriter writer, UserContext userContext, string itemClass, int iconFlag, bool isRead, bool isIrmProtected)
		{
			if (itemClass == null)
			{
				return false;
			}
			if (string.Equals(itemClass, "IPM.Note", StringComparison.InvariantCultureIgnoreCase) && isIrmProtected)
			{
				itemClass += ".irm";
			}
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, false, null, false, isRead, iconFlag, string.Empty, new string[0]);
			return true;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000A0834 File Offset: 0x0009EA34
		public static bool RenderImportance(TextWriter writer, UserContext userContext, Importance importance)
		{
			switch (importance)
			{
			case Importance.Low:
				userContext.RenderThemeImage(writer, ThemeFileId.ImportanceLow, "imp", new object[0]);
				goto IL_4A;
			case Importance.High:
				userContext.RenderThemeImage(writer, ThemeFileId.ImportanceHigh, "imp", new object[0]);
				goto IL_4A;
			}
			return false;
			IL_4A:
			userContext.RenderThemeImage(writer, ThemeFileId.Clear1x1, "impMg", new object[0]);
			return true;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000A08A0 File Offset: 0x0009EAA0
		public static bool RenderHasAttachments(TextWriter writer, UserContext userContext, bool hasAttachments)
		{
			return ListViewContentsRenderingUtilities.RenderHasAttachments(writer, userContext, hasAttachments, null);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000A08AC File Offset: 0x0009EAAC
		public static bool RenderHasAttachments(TextWriter writer, UserContext userContext, bool hasAttachments, string itemClass)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (!hasAttachments)
			{
				return false;
			}
			if (ObjectClass.IsReport(itemClass) || ObjectClass.IsSmsMessage(itemClass))
			{
				return false;
			}
			userContext.RenderThemeImage(writer, ThemeFileId.Attachment2, "atch", new object[0]);
			return true;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000A0904 File Offset: 0x0009EB04
		public static void RenderCheckBox(TextWriter writer, string id)
		{
			writer.Write("<input type=\"checkbox\" name=\"chkmsg\" value=\"");
			writer.Write(id);
			writer.Write("\" title=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1126382593));
			writer.Write("\" ");
			if (UserContextManager.GetUserContext().IsBasicExperience)
			{
				writer.Write("onclick=\"onClkChkBx(this);\"");
			}
			else
			{
				Utilities.RenderScriptHandler(writer, "onclick", "onClkChkBx(_this);");
			}
			writer.Write(">");
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000A0980 File Offset: 0x0009EB80
		public static bool RenderSmartDate(TextWriter writer, UserContext userContext, ExDateTime date)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ExDateTime date2 = DateTimeUtilities.GetLocalTime(userContext).Date;
			int num = (7 + (date2.DayOfWeek - userContext.UserOptions.WeekStartDay)) % 7;
			ExDateTime exDateTime = date2.IncrementDays(-1 * num);
			ExDateTime exDateTime2 = exDateTime.IncrementDays(7);
			ExDateTime t = exDateTime.IncrementDays(-7);
			ExDateTime t2 = exDateTime2.IncrementDays(7);
			string s;
			if (date.Date.Equals(date2))
			{
				s = date.ToString(userContext.UserOptions.TimeFormat);
			}
			else if (exDateTime <= date.Date && date.Date < exDateTime2)
			{
				s = date.ToString(userContext.UserOptions.GetWeekdayTimeFormat(false));
			}
			else if ((t <= date.Date && date.Date < exDateTime) || (exDateTime2 <= date.Date && date.Date < t2))
			{
				s = date.ToString(userContext.UserOptions.GetWeekdayDateNoYearFormat(false));
			}
			else
			{
				if (!(date != ExDateTime.MinValue))
				{
					return false;
				}
				s = date.ToString(userContext.UserOptions.DateFormat);
			}
			writer.Write("<span>");
			Utilities.SanitizeHtmlEncode(s, writer);
			writer.Write("</span>");
			return true;
		}

		// Token: 0x040014A5 RID: 5285
		private const string IrmItemClass = ".irm";
	}
}
