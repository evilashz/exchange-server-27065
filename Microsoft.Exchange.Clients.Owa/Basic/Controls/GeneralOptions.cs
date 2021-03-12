using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200004B RID: 75
	public class GeneralOptions : OptionsBase
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00012D50 File Offset: 0x00010F50
		public GeneralOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.CommitAndLoad();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00012D60 File Offset: 0x00010F60
		private void Load()
		{
			this.isOptimizedForAccessibility = this.userContext.UserOptions.IsOptimizedForAccessibility;
			this.isRichClientFeatureEnabled = this.userContext.IsFeatureEnabled(Feature.RichClient);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00012D90 File Offset: 0x00010F90
		private void CommitAndLoad()
		{
			this.Load();
			bool flag = false;
			if (Utilities.IsPostRequest(this.request) && !string.IsNullOrEmpty(base.Command) && this.isRichClientFeatureEnabled)
			{
				this.isOptimizedForAccessibility = (Utilities.GetFormParameter(this.request, "chkOptAcc", false) != null);
				if (this.userContext.UserOptions.IsOptimizedForAccessibility != this.isOptimizedForAccessibility)
				{
					this.userContext.UserOptions.IsOptimizedForAccessibility = this.isOptimizedForAccessibility;
					flag = true;
				}
				if (flag)
				{
					try
					{
						this.userContext.UserOptions.CommitChanges();
						base.SetSavedSuccessfully(true);
					}
					catch (StorageTransientException)
					{
						base.SetSavedSuccessfully(false);
					}
					catch (StoragePermanentException)
					{
						base.SetSavedSuccessfully(false);
					}
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00012E68 File Offset: 0x00011068
		public override void Render()
		{
			this.RenderAccessibilityOptions();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00012E70 File Offset: 0x00011070
		public override void RenderScript()
		{
			base.RenderJSVariable("a_fOptAcc", this.isOptimizedForAccessibility.ToString().ToLowerInvariant());
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00012E90 File Offset: 0x00011090
		private void RenderAccessibilityOptions()
		{
			string format = "<input type=\"checkbox\" name=\"{0}\"{1}{3} id=\"{0}\" onclick=\"return onClkChkBx(this);\" value=\"1\"><label for=\"{0}\">{2}</label>";
			base.RenderHeaderRow(ThemeFileId.AboutOwa, 951662406);
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1435977365));
			this.writer.Write("<ul><li>{0}</li><li>{1}</li><li>{2}</li></ul>", LocalizedStrings.GetHtmlEncoded(2267445), LocalizedStrings.GetHtmlEncoded(405551972), LocalizedStrings.GetHtmlEncoded(-1160531969));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write(format, new object[]
			{
				"chkOptAcc",
				this.isOptimizedForAccessibility ? " checked" : string.Empty,
				LocalizedStrings.GetHtmlEncoded(-2119250240),
				this.isRichClientFeatureEnabled ? string.Empty : " disabled"
			});
			this.writer.Write("<div id=\"olo\">{0}</div>", LocalizedStrings.GetHtmlEncoded(this.isRichClientFeatureEnabled ? -1771373774 : 1767653808));
			this.writer.Write("</td></tr>");
		}

		// Token: 0x04000180 RID: 384
		private const string FormOptimizeForAccessibility = "chkOptAcc";

		// Token: 0x04000181 RID: 385
		private const string FormJavaScriptOptimizeForAccessibility = "a_fOptAcc";

		// Token: 0x04000182 RID: 386
		private bool isOptimizedForAccessibility;

		// Token: 0x04000183 RID: 387
		private bool isRichClientFeatureEnabled;
	}
}
