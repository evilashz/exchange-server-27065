using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003D7 RID: 983
	internal sealed class OptionsContextMenu : ContextMenu
	{
		// Token: 0x06002453 RID: 9299 RVA: 0x000D33ED File Offset: 0x000D15ED
		public OptionsContextMenu(UserContext userContext) : base("divOptionsContextMenu", userContext, false)
		{
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000D33FC File Offset: 0x000D15FC
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div class=\"alertPopupShading\"></div>");
			output.Write("<div id=\"divOptsMnuTopBorderLeft\" class=\"alertDialogTopBorder\"></div>");
			output.Write("<div id=\"divOptsMnuTopBorderRight\" class=\"alertDialogTopBorder\"></div>");
			base.RenderMenuHeader(output, null, 1511584348, null);
			base.RenderMenuItem(output, -1226104492, "navToOptOOF", "mnuItmTxtItmIndented");
			if (this.userContext.IsFeatureEnabled(Feature.ChangePassword))
			{
				base.RenderMenuItem(output, -1294384513, "navToOptPwd", "mnuItmTxtItmIndented");
			}
			if (this.userContext.IsFeatureEnabled(Feature.Rules))
			{
				base.RenderMenuItem(output, 1115834861, "navToOptRules", "mnuItmTxtItmIndented");
			}
			base.RenderMenuItem(output, -657439717, "navToOptions", "mnuItmTxtItmIndented mnuItmTxtItmSpaced");
			if (this.userContext.IsFeatureEnabled(Feature.Themes))
			{
				this.RenderThemeSelector(output);
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000D34E0 File Offset: 0x000D16E0
		private void RenderThemeSelector(TextWriter output)
		{
			base.RenderMenuHeader(output, "ThmTtl", 582309493, null);
			output.Write("<div class=\"dynamicSelector\">");
			output.Write("<span id=\"leftarrow\" class=\"thmSelArrw\">");
			this.userContext.RenderThemeImage(output, ThemeFileId.PreviousArrow);
			output.Write("</span>");
			output.Write("<span class=\"inlineContainer\">");
			output.Write("<div id=\"Themes\" class=\"container\">");
			this.RenderThemeThumbnails(output);
			output.Write("</div>");
			this.userContext.RenderThemeImage(output, ThemeFileId.Progress, "themePrg", new object[]
			{
				"id=\"ThemeProgress\"",
				"style=\"display:none\""
			});
			output.Write("</span>");
			output.Write("<span id=\"rightarrow\" class=\"thmSelArrw\">");
			this.userContext.RenderThemeImage(output, ThemeFileId.NextArrow);
			output.Write("</span>");
			output.Write("</div>");
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000D35C4 File Offset: 0x000D17C4
		private void RenderThemeThumbnails(TextWriter output)
		{
			BrandingUtilities.IsBranded();
			IDictionary dictionary = new Dictionary<string, string>();
			string storageId = base.UserContext.Theme.StorageId;
			for (int i = 0; i < ThemeManager.Themes.Length; i++)
			{
				Theme theme = ThemeManager.Themes[i];
				dictionary.Add(theme.StorageId, theme.DisplayName);
			}
			int value = 40 * (dictionary.Count / 2 + ((dictionary.Count % 2 > 0) ? 1 : 0));
			output.Write("<div id=\"divThemes\" class=\"scroller\" style=\"width:");
			output.Write(value);
			output.Write("px;\">");
			output.Write("<span class=\"ThmPreviews\">");
			int num = 0;
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (num == 2)
				{
					output.Write("</span><span class=\"ThmPreviews\">");
					num = 0;
				}
				output.Write("<span oV=\"" + dictionaryEntry.Key.ToString() + "\" ");
				output.Write("oP=\"" + ThemeManager.Themes[(int)((UIntPtr)ThemeManager.GetIdFromStorageId(dictionaryEntry.Key.ToString()))].Url + "\" ");
				if (storageId == dictionaryEntry.Key.ToString())
				{
					output.Write("class=\"selThm\" ");
				}
				output.Write("id=\"ThmPreview\">");
				output.Write("<img tabindex=\"0\" src=\"");
				ThemeManager.RenderThemePreviewUrl(output, (string)dictionaryEntry.Key);
				output.Write("\" alt=\"");
				if (!string.IsNullOrEmpty(dictionaryEntry.Value.ToString()))
				{
					Utilities.HtmlEncode(dictionaryEntry.Value.ToString(), output);
				}
				output.Write("\" title=\"");
				if (!string.IsNullOrEmpty(dictionaryEntry.Value.ToString()))
				{
					Utilities.HtmlEncode(dictionaryEntry.Value.ToString(), output);
				}
				output.Write("\">");
				output.Write("</span>");
				num++;
			}
			output.Write("</span></div>");
		}

		// Token: 0x0400193C RID: 6460
		private const int ThemePreviewWidth = 40;

		// Token: 0x0400193D RID: 6461
		private const int PreviewsPerColumn = 2;
	}
}
