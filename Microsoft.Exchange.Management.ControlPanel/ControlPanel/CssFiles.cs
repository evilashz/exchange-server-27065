using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200054C RID: 1356
	public static class CssFiles
	{
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000BFF3C File Offset: 0x000BE13C
		static CssFiles()
		{
			CssFiles.cultureAwareCssFileTable[31748] = (CssFiles.cultureAwareCssFileTable[3076] = (CssFiles.cultureAwareCssFileTable[5124] = (CssFiles.cultureAwareCssFileTable[1028] = new CssFiles.CssFile("main_zht.css", true))));
			CssFiles.cultureAwareCssFileTable[4] = (CssFiles.cultureAwareCssFileTable[4100] = (CssFiles.cultureAwareCssFileTable[2052] = new CssFiles.CssFile("main_zhs.css", true)));
			CssFiles.cultureAwareCssFileTable[17] = (CssFiles.cultureAwareCssFileTable[1041] = new CssFiles.CssFile("main_ja.css", true));
			CssFiles.cultureAwareCssFileTable[18] = (CssFiles.cultureAwareCssFileTable[1042] = new CssFiles.CssFile("main_ko.css", true));
			CssFiles.cultureAwareCssFileTable[1066] = new CssFiles.CssFile("main_vi.css", true);
			CssFiles.cultureAwareCssFileTable[1098] = (CssFiles.cultureAwareCssFileTable[1100] = new CssFiles.CssFile("main_in.css", true));
			CssFiles.cultureAwareCssFileTable[1056] = new CssFiles.CssFile("main_ur.css", true);
			CssFiles.nameToCssFileTable = new Dictionary<string, CssFiles.CssFile>(StringComparer.OrdinalIgnoreCase);
			CssFiles.nameToCssFileTable.Add(CssFiles.navCombine.FileName, CssFiles.navCombine);
			CssFiles.nameToCssFileTable.Add(CssFiles.homePageSpriteCss.FileName, CssFiles.homePageSpriteCss);
			CssFiles.nameToCssFileTable.Add(CssFiles.voicemailSpriteCss.FileName, CssFiles.voicemailSpriteCss);
			CssFiles.nameToCssFileTable.Add(CssFiles.editorSpriteCss.FileName, CssFiles.editorSpriteCss);
			CssFiles.nameToCssFileTable.Add(CssFiles.editorStyleCss.FileName, CssFiles.editorStyleCss);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000C0194 File Offset: 0x000BE394
		public static void RenderCssLinks(Control control, IEnumerable<string> cssFiles)
		{
			bool isRtl = RtlUtil.IsRtl;
			CssFiles.CssFile cssFile = null;
			if (!CssFiles.cultureAwareCssFileTable.TryGetValue(CultureInfo.CurrentUICulture.LCID, out cssFile))
			{
				cssFile = CssFiles.mainDefaultCss;
			}
			CssFiles.OutputCssLink(control, cssFile, isRtl);
			if (cssFiles != null)
			{
				foreach (string text in cssFiles)
				{
					CssFiles.CssFile cssFile2 = CssFiles.nameToCssFileTable[text];
					if (cssFile2 == null)
					{
						throw new InvalidOperationException(string.Format("File name {0} isn't map to any predefined CssFile. Make sure you type the correct css file name.", text));
					}
					CssFiles.OutputCssLink(control, cssFile2, isRtl);
				}
			}
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000C0234 File Offset: 0x000BE434
		private static void OutputCssLink(Control control, CssFiles.CssFile cssFile, bool isRtl)
		{
			CssFiles.OutputCssLink(control.Page.Response.Output, ThemeResource.GetThemeResource(control.Page, (isRtl && cssFile.RtlFileName != null) ? cssFile.RtlFileName : cssFile.FileName));
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000C026F File Offset: 0x000BE46F
		private static void OutputCssLink(TextWriter writer, string cssFileUrl)
		{
			writer.Write("<link href=\"");
			writer.Write(cssFileUrl);
			writer.Write("\" type=\"text/css\" rel=\"stylesheet\" />");
		}

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x000C028E File Offset: 0x000BE48E
		public static CssFiles.CssFile HighContrastCss
		{
			get
			{
				return CssFiles.highContrastCss;
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000C0295 File Offset: 0x000BE495
		public static string ToUrl(this CssFiles.CssFile cssFile, Control control)
		{
			return ThemeResource.GetThemeResource(control, (RtlUtil.IsRtl && cssFile.RtlFileName != null) ? cssFile.RtlFileName : cssFile.FileName);
		}

		// Token: 0x04002A2D RID: 10797
		public const string NavCombine = "NavCombine.css";

		// Token: 0x04002A2E RID: 10798
		public const string HomePageSprite = "HomePageSprite.css";

		// Token: 0x04002A2F RID: 10799
		public const string VoicemailSprite = "VoicemailSprite.css";

		// Token: 0x04002A30 RID: 10800
		public const string EditorSprite = "nbsprite1.mouse.css";

		// Token: 0x04002A31 RID: 10801
		public const string EditorStyle = "EditorStyles.mouse.css";

		// Token: 0x04002A32 RID: 10802
		public const string HighContrast = "HighContrast.css";

		// Token: 0x04002A33 RID: 10803
		private static CssFiles.CssFile mainDefaultCss = new CssFiles.CssFile("main_default.css", true);

		// Token: 0x04002A34 RID: 10804
		private static CssFiles.CssFile navCombine = new CssFiles.CssFile("NavCombine.css", true);

		// Token: 0x04002A35 RID: 10805
		private static CssFiles.CssFile homePageSpriteCss = new CssFiles.CssFile("HomePageSprite.css", true);

		// Token: 0x04002A36 RID: 10806
		private static CssFiles.CssFile voicemailSpriteCss = new CssFiles.CssFile("VoicemailSprite.css", true);

		// Token: 0x04002A37 RID: 10807
		private static CssFiles.CssFile editorSpriteCss = new CssFiles.CssFile("nbsprite1.mouse.css", false);

		// Token: 0x04002A38 RID: 10808
		private static CssFiles.CssFile editorStyleCss = new CssFiles.CssFile("EditorStyles.mouse.css", false);

		// Token: 0x04002A39 RID: 10809
		private static CssFiles.CssFile highContrastCss = new CssFiles.CssFile("HighContrast.css", true);

		// Token: 0x04002A3A RID: 10810
		private static Dictionary<int, CssFiles.CssFile> cultureAwareCssFileTable = new Dictionary<int, CssFiles.CssFile>();

		// Token: 0x04002A3B RID: 10811
		private static Dictionary<string, CssFiles.CssFile> nameToCssFileTable;

		// Token: 0x0200054D RID: 1357
		public class CssFile
		{
			// Token: 0x06003FAA RID: 16298 RVA: 0x000C02BA File Offset: 0x000BE4BA
			public CssFile(string fileName, bool hasRtlFile = true)
			{
				this.FileName = fileName;
				if (hasRtlFile)
				{
					this.RtlFileName = Path.GetFileNameWithoutExtension(fileName) + "-rtl" + Path.GetExtension(fileName);
				}
			}

			// Token: 0x170024CC RID: 9420
			// (get) Token: 0x06003FAB RID: 16299 RVA: 0x000C02E8 File Offset: 0x000BE4E8
			// (set) Token: 0x06003FAC RID: 16300 RVA: 0x000C02F0 File Offset: 0x000BE4F0
			public string FileName { get; private set; }

			// Token: 0x170024CD RID: 9421
			// (get) Token: 0x06003FAD RID: 16301 RVA: 0x000C02F9 File Offset: 0x000BE4F9
			// (set) Token: 0x06003FAE RID: 16302 RVA: 0x000C0301 File Offset: 0x000BE501
			public string RtlFileName { get; private set; }
		}
	}
}
