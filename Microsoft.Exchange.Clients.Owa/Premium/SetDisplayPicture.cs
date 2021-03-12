using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000477 RID: 1143
	public class SetDisplayPicture : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x06002BCC RID: 11212 RVA: 0x000F4EC0 File Offset: 0x000F30C0
		protected override void OnLoad(EventArgs e)
		{
			if (base.Request.HttpMethod == "POST")
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "a", false);
				if (!string.IsNullOrEmpty(queryStringParameter))
				{
					if (queryStringParameter.Equals("upload") && base.Request.Files.Count > 0)
					{
						HttpPostedFile file = base.Request.Files[0];
						this.setDisplayPictureResult = DisplayPictureUtility.UploadDisplayPicture(file, base.UserContext);
						this.currentState = ChangePictureDialogState.Upload;
						return;
					}
					if (queryStringParameter.Equals("clear"))
					{
						this.setDisplayPictureResult = DisplayPictureUtility.ClearDisplayPicture(base.UserContext);
						this.currentState = ChangePictureDialogState.Save;
						return;
					}
					if (queryStringParameter.Equals("change"))
					{
						this.setDisplayPictureResult = DisplayPictureUtility.SaveDisplayPicture(base.UserContext);
						this.currentState = ChangePictureDialogState.Save;
						return;
					}
				}
			}
			else
			{
				base.UserContext.UploadedDisplayPicture = null;
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000F4FA8 File Offset: 0x000F31A8
		protected override void OnError(EventArgs e)
		{
			Exception lastError = base.Server.GetLastError();
			if (lastError is HttpException)
			{
				base.Server.ClearError();
				Utilities.TransferToErrorPage(base.OwaContext, LocalizedStrings.GetNonEncoded(-1440270008));
				return;
			}
			base.OnError(e);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000F4FF1 File Offset: 0x000F31F1
		protected bool IsClosingWindow()
		{
			return this.currentState == ChangePictureDialogState.Save && this.setDisplayPictureResult.ResultCode == SetDisplayPictureResultCode.NoError;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000F500C File Offset: 0x000F320C
		protected void RenderTitle()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1336564560));
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000F5023 File Offset: 0x000F3223
		protected void RenderDescription()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-864891586));
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000F503A File Offset: 0x000F323A
		protected void RenderCurrentPictureTitle()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(765872725));
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000F5051 File Offset: 0x000F3251
		protected void RenderClearPictureTitle()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(-984343499));
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000F5068 File Offset: 0x000F3268
		protected void RenderClearPictureNote()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(2047483241));
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000F507F File Offset: 0x000F327F
		protected void RenderChangeLink()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(641627222));
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000F5096 File Offset: 0x000F3296
		protected void RenderFileUploaded()
		{
			if (base.UserContext.UploadedDisplayPicture != null)
			{
				base.SanitizingResponse.Write("1");
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000F50B8 File Offset: 0x000F32B8
		protected void RenderErrorInfobar()
		{
			if (this.setDisplayPictureResult.ResultCode != SetDisplayPictureResultCode.NoError)
			{
				Infobar infobar = new Infobar();
				infobar.AddMessage(this.setDisplayPictureResult.ErrorMessage, InfobarMessageType.Error, "divSetPicErr");
				infobar.Render(base.Response.Output);
			}
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000F5100 File Offset: 0x000F3300
		protected void RenderFormAction()
		{
			base.SanitizingResponse.Write("?ae=Dialog&t=SetDisplayPicture");
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000F5114 File Offset: 0x000F3314
		protected void RenderPicture(bool showDoughboy)
		{
			if (!showDoughboy && base.UserContext.UploadedDisplayPicture != null)
			{
				RenderingUtilities.RenderDisplayPicture(base.Response.Output, base.UserContext, RenderingUtilities.GetADPictureUrl(string.Empty, string.Empty, base.UserContext, true, true), 64, true, ThemeFileId.DoughboyPerson);
				return;
			}
			string srcUrl = showDoughboy ? string.Empty : RenderingUtilities.GetADPictureUrl(base.UserContext.ExchangePrincipal.LegacyDn, "EX", base.UserContext, true);
			RenderingUtilities.RenderDisplayPicture(base.Response.Output, base.UserContext, srcUrl, 64, true, ThemeFileId.DoughboyPerson);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000F51B2 File Offset: 0x000F33B2
		protected void RenderImageLargeHtml()
		{
			if (!string.IsNullOrEmpty(this.setDisplayPictureResult.ImageLargeHtml))
			{
				Utilities.JavascriptEncode(this.setDisplayPictureResult.ImageLargeHtml, base.Response.Output);
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000F51E1 File Offset: 0x000F33E1
		protected void RenderImageSmallHtml()
		{
			if (!string.IsNullOrEmpty(this.setDisplayPictureResult.ImageSmallHtml))
			{
				Utilities.JavascriptEncode(this.setDisplayPictureResult.ImageSmallHtml, base.Response.Output);
			}
		}

		// Token: 0x04001CFE RID: 7422
		private SetDisplayPictureResult setDisplayPictureResult = SetDisplayPictureResult.NoError;

		// Token: 0x04001CFF RID: 7423
		private ChangePictureDialogState currentState;
	}
}
