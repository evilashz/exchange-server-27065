using System;
using System.IO;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200024C RID: 588
	internal static class SessionContextUtilities
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x00079373 File Offset: 0x00077573
		public static void RenderThemeFileUrl(TextWriter writer, ThemeFileId themeFileId, ISessionContext sessionContext)
		{
			ThemeManager.RenderThemeFileUrl(writer, sessionContext.Theme.Id, themeFileId, sessionContext.IsBasicExperience);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0007938D File Offset: 0x0007758D
		public static void RenderThemeFileUrl(TextWriter writer, int themeFileIndex, ISessionContext sessionContext)
		{
			ThemeManager.RenderThemeFileUrl(writer, sessionContext.Theme.Id, themeFileIndex, sessionContext.IsBasicExperience);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000793A8 File Offset: 0x000775A8
		public static void RenderThemeImage(StringBuilder builder, ThemeFileId themeFileId, string styleClass, ISessionContext sessionContext, params object[] extraAttributes)
		{
			using (StringWriter stringWriter = new StringWriter(builder))
			{
				SessionContextUtilities.RenderThemeImage(stringWriter, themeFileId, styleClass, sessionContext, extraAttributes);
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000793E4 File Offset: 0x000775E4
		public static void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId, ISessionContext sessionContext)
		{
			SessionContextUtilities.RenderThemeImage(writer, themeFileId, null, sessionContext, new object[0]);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x000793F8 File Offset: 0x000775F8
		public static void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, ISessionContext sessionContext, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, sessionContext);
			foreach (object obj in extraAttributes)
			{
				if (obj != null)
				{
					writer.Write(obj);
					writer.Write(" ");
				}
			}
			SessionContextUtilities.RenderThemeImageEnd(writer, themeFileId);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0007943F File Offset: 0x0007763F
		public static void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId, ISessionContext sessionContext)
		{
			SessionContextUtilities.RenderBaseThemeImage(writer, themeFileId, null, sessionContext, new object[0]);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00079450 File Offset: 0x00077650
		public static void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, ISessionContext sessionContext, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, true, sessionContext);
			foreach (object obj in extraAttributes)
			{
				if (obj != null)
				{
					writer.Write(obj);
					writer.Write(" ");
				}
			}
			SessionContextUtilities.RenderThemeImageEnd(writer, themeFileId);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00079498 File Offset: 0x00077698
		public static void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, ISessionContext sessionContext, params string[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageWithToolTip(writer, themeFileId, styleClass, -1018465893, sessionContext, extraAttributes);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000794AC File Offset: 0x000776AC
		public static void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, Strings.IDs tooltipStringId, ISessionContext sessionContext, params string[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, sessionContext);
			foreach (string value in extraAttributes)
			{
				if (!string.IsNullOrEmpty(value))
				{
					writer.Write(value);
					writer.Write(" ");
				}
			}
			Utilities.RenderImageAltAttribute(writer, sessionContext, themeFileId, tooltipStringId);
			SessionContextUtilities.RenderThemeImageEnd(writer, themeFileId);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00079503 File Offset: 0x00077703
		public static void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass, ISessionContext sessionContext)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, false, sessionContext);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00079510 File Offset: 0x00077710
		public static void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass, bool renderBaseTheme, ISessionContext sessionContext)
		{
			Theme theme = renderBaseTheme ? ThemeManager.BaseTheme : sessionContext.Theme;
			if (!sessionContext.IsBasicExperience && theme.ShouldUseCssSprites(themeFileId))
			{
				writer.Write("<img src=\"");
				ThemeManager.RenderThemeFileUrl(writer, theme.Id, ThemeFileId.Clear1x1);
				writer.Write("\" class=\"csimg ");
				writer.Write(theme.GetThemeFileClass(themeFileId));
				if (!string.IsNullOrEmpty(styleClass))
				{
					writer.Write(" ");
					writer.Write(styleClass);
				}
			}
			else
			{
				writer.Write("<img src=\"");
				ThemeManager.RenderThemeFileUrl(writer, theme.Id, themeFileId, sessionContext.IsBasicExperience);
				if (!string.IsNullOrEmpty(styleClass))
				{
					writer.Write("\" class=\"");
					writer.Write(styleClass);
				}
			}
			writer.Write("\" ");
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000795D2 File Offset: 0x000777D2
		public static void RenderThemeImageEnd(TextWriter writer, ThemeFileId themeFileId)
		{
			writer.Write(">");
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000795DF File Offset: 0x000777DF
		public static string GetThemeFileUrl(ThemeFileId themeFileId, ISessionContext sessionContext)
		{
			return ThemeManager.GetThemeFileUrl(sessionContext.Theme.Id, themeFileId, sessionContext.IsBasicExperience);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000795F8 File Offset: 0x000777F8
		public static void RenderCssFontThemeFileUrl(TextWriter writer, ISessionContext sessionContext)
		{
			ThemeManager.RenderCssFontThemeFileUrl(writer, sessionContext.IsBasicExperience);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00079608 File Offset: 0x00077808
		public static void RenderCssLink(TextWriter writer, HttpRequest request, ISessionContext sessionContext, bool phase1Only)
		{
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			SessionContextUtilities.RenderThemeFileUrl(writer, ThemeFileId.PremiumCss, sessionContext);
			writer.Write("\">");
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			SessionContextUtilities.RenderCssFontThemeFileUrl(writer, sessionContext);
			writer.Write("\">");
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			ThemeManager.RenderThemeFileUrl(writer, sessionContext.Theme.Id, ThemeFileId.CssSpritesCss);
			writer.Write("\">");
			if (!phase1Only)
			{
				writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
				ThemeManager.RenderThemeFileUrl(writer, sessionContext.Theme.Id, ThemeFileId.CssSpritesCss2);
				writer.Write("\">");
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000796A3 File Offset: 0x000778A3
		public static string GetDirectionMark(this ISessionContext sessionContext)
		{
			if (!sessionContext.IsRtl)
			{
				return "&#x200E;";
			}
			return "&#x200F;";
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000796B8 File Offset: 0x000778B8
		public static string GetBlankPage(this ISessionContext sessionContext)
		{
			return "about:blank";
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000796BF File Offset: 0x000778BF
		public static string GetBlankPage(this ISessionContext sessionContext, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Utilities.HtmlEncode(path) + "blank.htm";
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000796DF File Offset: 0x000778DF
		public static void RenderCssLink(TextWriter writer, HttpRequest request, ISessionContext sessionContext)
		{
			SessionContextUtilities.RenderCssLink(writer, request, sessionContext, false);
		}

		// Token: 0x04000D94 RID: 3476
		internal const string CssLinkStartMarkup = "<link type=\"text/css\" rel=\"stylesheet\" href=\"";

		// Token: 0x04000D95 RID: 3477
		internal const string CssLinkEndMarkup = "\">";
	}
}
