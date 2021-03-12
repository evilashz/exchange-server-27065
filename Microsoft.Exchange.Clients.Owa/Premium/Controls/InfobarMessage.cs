using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000393 RID: 915
	public class InfobarMessage
	{
		// Token: 0x060022AF RID: 8879 RVA: 0x000C68E4 File Offset: 0x000C4AE4
		internal InfobarMessage(SanitizedHtmlString messageHtml, InfobarMessageType type)
		{
			this.message = messageHtml;
			this.type = type;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000C6901 File Offset: 0x000C4B01
		internal InfobarMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, string id)
		{
			this.message = messageHtml;
			this.type = type;
			this.tagId = id;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000C6925 File Offset: 0x000C4B25
		internal InfobarMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, string id, bool hideMessage)
		{
			this.message = messageHtml;
			this.type = type;
			this.tagId = id;
			this.hideMessage = hideMessage;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000C6951 File Offset: 0x000C4B51
		internal InfobarMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, string id, SanitizedHtmlString linkText, SanitizedHtmlString expandSection)
		{
			this.message = messageHtml;
			this.linkText = linkText;
			this.tagId = id;
			this.expandSection = expandSection;
			this.type = type;
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000C6985 File Offset: 0x000C4B85
		internal SanitizedHtmlString Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000C698D File Offset: 0x000C4B8D
		internal InfobarMessageType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000C6995 File Offset: 0x000C4B95
		internal string TagId
		{
			get
			{
				return this.tagId;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x000C699D File Offset: 0x000C4B9D
		internal SanitizedHtmlString LinkText
		{
			get
			{
				return this.linkText;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x000C69A5 File Offset: 0x000C4BA5
		internal SanitizedHtmlString ExpandSection
		{
			get
			{
				return this.expandSection;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x000C69AD File Offset: 0x000C4BAD
		internal bool HideMessage
		{
			get
			{
				return this.hideMessage;
			}
		}

		// Token: 0x04001873 RID: 6259
		private SanitizedHtmlString message;

		// Token: 0x04001874 RID: 6260
		private SanitizedHtmlString linkText;

		// Token: 0x04001875 RID: 6261
		private SanitizedHtmlString expandSection;

		// Token: 0x04001876 RID: 6262
		private InfobarMessageType type = InfobarMessageType.Informational;

		// Token: 0x04001877 RID: 6263
		private string tagId;

		// Token: 0x04001878 RID: 6264
		private bool hideMessage;
	}
}
