using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020002BE RID: 702
	public class SetDisplayPictureResult
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0009D41F File Offset: 0x0009B61F
		public static SetDisplayPictureResult NoError
		{
			get
			{
				return new SetDisplayPictureResult(SetDisplayPictureResultCode.NoError);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0009D427 File Offset: 0x0009B627
		public SetDisplayPictureResultCode ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0009D42F File Offset: 0x0009B62F
		public SanitizedHtmlString ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0009D437 File Offset: 0x0009B637
		public string ImageSmallHtml
		{
			get
			{
				return this.imageSmallHtml;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0009D43F File Offset: 0x0009B63F
		public string ImageLargeHtml
		{
			get
			{
				return this.imageLargeHtml;
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0009D447 File Offset: 0x0009B647
		private SetDisplayPictureResult(SetDisplayPictureResultCode resultCode)
		{
			this.resultCode = resultCode;
			this.errorMessage = new SanitizedHtmlString();
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0009D477 File Offset: 0x0009B677
		public void SetErrorResult(SetDisplayPictureResultCode resultCode, SanitizedHtmlString errorMessage)
		{
			if (resultCode != SetDisplayPictureResultCode.NoError && SanitizedStringBase<OwaHtml>.IsNullOrEmpty(errorMessage))
			{
				throw new ArgumentException("Must specify an error message if result code is not NoError.");
			}
			this.resultCode = resultCode;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0009D49D File Offset: 0x0009B69D
		public void SetSuccessResult(string imageSmallHtml, string imageLargeHtml)
		{
			if (string.IsNullOrEmpty(imageSmallHtml))
			{
				throw new ArgumentException("Must specify small image html for successful result.");
			}
			if (string.IsNullOrEmpty(imageLargeHtml))
			{
				throw new ArgumentException("Must specify large image html for successful result.");
			}
			this.imageSmallHtml = imageSmallHtml;
			this.imageLargeHtml = imageLargeHtml;
		}

		// Token: 0x040013EE RID: 5102
		private SetDisplayPictureResultCode resultCode;

		// Token: 0x040013EF RID: 5103
		private SanitizedHtmlString errorMessage;

		// Token: 0x040013F0 RID: 5104
		private string imageSmallHtml = string.Empty;

		// Token: 0x040013F1 RID: 5105
		private string imageLargeHtml = string.Empty;
	}
}
