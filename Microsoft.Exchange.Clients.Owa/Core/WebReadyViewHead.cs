using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000292 RID: 658
	public class WebReadyViewHead : OwaPage
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x00092DD8 File Offset: 0x00090FD8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (Utilities.GetQueryStringParameter(base.Request, "pn", false) != null)
			{
				throw new OwaInvalidRequestException("Page number (pn) parameter is not permitted in the current URL");
			}
			this.utilities = new WebReadyViewUtilities(base.OwaContext);
			this.utilities.LoadDocument(false, out this.decryptionStatus);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00092E30 File Offset: 0x00091030
		protected void RenderMessage(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			AttachmentPolicy.Level levelForAttachment = AttachmentLevelLookup.GetLevelForAttachment(this.utilities.FileExtension, this.utilities.MimeType, base.UserContext);
			if (AttachmentPolicy.Level.Block == levelForAttachment)
			{
				if (base.UserContext.IsBasicExperience)
				{
					output.Write("<table class=tbWIB><tr><td class=\"");
					output.Write(Utilities.GetTDClassForWebReadyViewHead(base.UserContext.IsBasicExperience));
					output.Write("\"><img  class=\"iei errInfo\" src=\"");
					base.OwaContext.UserContext.RenderThemeFileUrl(output, ThemeFileId.Exclaim);
					output.Write("\" alt=\"\"><span class=\"errInfo\">");
				}
				else
				{
					output.Write("<table class=tbWIB><tr><td>");
					base.OwaContext.UserContext.RenderThemeImage(output, ThemeFileId.Exclaim, "iei errInfo", new object[0]);
					output.Write("<span class=\"errInfo\">");
				}
				output.Write(LocalizedStrings.GetHtmlEncoded(437967712));
				output.Write("</span></td></tr></table>");
				return;
			}
			if (AttachmentPolicy.Level.ForceSave == levelForAttachment)
			{
				output.Write("<table class=tbNIB><tr><td class=\"msg ");
				output.Write(Utilities.GetTDClassForWebReadyViewHead(base.UserContext.IsBasicExperience));
				output.Write("\">");
				this.RenderHtmlEncodedSaveAttachmentToDiskMessage();
				output.Write("</td></tr></table>");
				return;
			}
			if (AttachmentPolicy.Level.Allow == levelForAttachment)
			{
				output.Write("<table class=tbNIB><tr><td class=\"msg ");
				output.Write(Utilities.GetTDClassForWebReadyViewHead(base.UserContext.IsBasicExperience));
				output.Write("\">");
				output.Write(base.UserContext.IsBasicExperience ? LocalizedStrings.GetHtmlEncoded(94137446) : LocalizedStrings.GetHtmlEncoded(2080319064));
				output.Write("</td></tr></table>");
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00092FC3 File Offset: 0x000911C3
		protected void RenderHtmlEncodedSaveAttachmentToDiskMessage()
		{
			if (base.UserContext.IsBasicExperience)
			{
				base.SanitizingResponse.Write(SanitizedHtmlString.FromStringId(-353246432));
				return;
			}
			Utilities.SanitizeHtmlEncode(base.GetSaveAttachmentToDiskMessage(687430467), base.SanitizingResponse);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00093000 File Offset: 0x00091200
		protected void RenderVariableSaveAttachmentToDiskMessage()
		{
			string input = string.Empty;
			if (base.UserContext.IsBasicExperience)
			{
				input = LocalizedStrings.GetNonEncoded(-353246432);
			}
			else
			{
				input = base.GetSaveAttachmentToDiskMessage(687430467);
			}
			RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "a_sL2Aw", input);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0009304A File Offset: 0x0009124A
		protected void RenderOpenLink(TextWriter output)
		{
			this.utilities.RenderOpenLink(output);
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00093058 File Offset: 0x00091258
		protected bool IsSupportPaging
		{
			get
			{
				return this.utilities.IsSupportPaging;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00093065 File Offset: 0x00091265
		protected string FileName
		{
			get
			{
				return this.utilities.FileName;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00093072 File Offset: 0x00091272
		protected bool HasIrmError
		{
			get
			{
				return this.decryptionStatus.Failed;
			}
		}

		// Token: 0x0400126A RID: 4714
		private WebReadyViewUtilities utilities;

		// Token: 0x0400126B RID: 4715
		private RightsManagedMessageDecryptionStatus decryptionStatus;
	}
}
