using System;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002A8 RID: 680
	public class AttachmentAddResult
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x0009751B File Offset: 0x0009571B
		public static AttachmentAddResult NoError
		{
			get
			{
				return new AttachmentAddResult(AttachmentAddResultCode.NoError, null);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00097524 File Offset: 0x00095724
		public AttachmentAddResultCode ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0009752C File Offset: 0x0009572C
		public SanitizedHtmlString Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00097534 File Offset: 0x00095734
		private AttachmentAddResult(AttachmentAddResultCode resultCode, SanitizedHtmlString message)
		{
			this.SetResult(resultCode, message);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00097544 File Offset: 0x00095744
		public void SetResult(AttachmentAddResultCode resultCode, SanitizedHtmlString message)
		{
			if (resultCode != AttachmentAddResultCode.NoError && SanitizedStringBase<OwaHtml>.IsNullOrEmpty(message))
			{
				throw new ArgumentException("Must specify a message if result code is not NoError.");
			}
			this.resultCode = resultCode;
			this.message = message;
		}

		// Token: 0x040012E2 RID: 4834
		private AttachmentAddResultCode resultCode;

		// Token: 0x040012E3 RID: 4835
		private SanitizedHtmlString message;
	}
}
