using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200004F RID: 79
	public class InfobarMessage
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00013560 File Offset: 0x00011760
		public static InfobarMessage CreateLocalized(Strings.IDs stringId, InfobarMessageType type)
		{
			return new InfobarMessage(SanitizedHtmlString.FromStringId(stringId), type);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0001356E File Offset: 0x0001176E
		public static InfobarMessage CreateLocalized(Strings.IDs stringId, InfobarMessageType type, string id)
		{
			return new InfobarMessage(SanitizedHtmlString.FromStringId(stringId), type, id);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0001357D File Offset: 0x0001177D
		public static InfobarMessage CreateText(string messageText, InfobarMessageType type)
		{
			return new InfobarMessage(new SanitizedHtmlString(messageText), type);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001358B File Offset: 0x0001178B
		public static InfobarMessage CreateText(string messageText, InfobarMessageType type, string id)
		{
			return new InfobarMessage(new SanitizedHtmlString(messageText), type, id);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0001359A File Offset: 0x0001179A
		public static InfobarMessage CreateHtml(SanitizedHtmlString messageHtml, InfobarMessageType type)
		{
			if (messageHtml == null)
			{
				throw new ArgumentNullException("messageHtml");
			}
			return new InfobarMessage(messageHtml, type);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000135B4 File Offset: 0x000117B4
		public static InfobarMessage CreateErrorMessageFromException(Exception e, UserContext userContext)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ErrorInformation exceptionHandlingInformation = Utilities.GetExceptionHandlingInformation(e, userContext.MailboxIdentity);
			return InfobarMessage.CreateText(exceptionHandlingInformation.Message, InfobarMessageType.Error);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000135F6 File Offset: 0x000117F6
		public static InfobarMessage CreatePromptHtml(SanitizedHtmlString messageHtml, SanitizedHtmlString bodyHtml, SanitizedHtmlString footerHtml)
		{
			return new InfobarMessage(messageHtml, bodyHtml, footerHtml);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00013600 File Offset: 0x00011800
		public static InfobarMessage CreateExpandingHtml(SanitizedHtmlString messageHtml, SanitizedHtmlString expandSectionHtml, bool isExpanding)
		{
			return new InfobarMessage(messageHtml, InfobarMessageType.Expanding, expandSectionHtml, isExpanding);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001360B File Offset: 0x0001180B
		private InfobarMessage(SanitizedHtmlString message, InfobarMessageType type)
		{
			this.message = message;
			this.type = type;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00013628 File Offset: 0x00011828
		private InfobarMessage(SanitizedHtmlString message, InfobarMessageType type, string id)
		{
			this.message = message;
			this.type = type;
			this.tagId = id;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001364C File Offset: 0x0001184C
		private InfobarMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, SanitizedHtmlString expandSectionHtml, bool isExpanding)
		{
			this.message = messageHtml;
			this.expandSectionHtml = expandSectionHtml;
			this.type = type;
			this.isExpanding = isExpanding;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00013678 File Offset: 0x00011878
		private InfobarMessage(SanitizedHtmlString messageHtml, SanitizedHtmlString bodyHtml, SanitizedHtmlString footerHtml)
		{
			this.message = messageHtml;
			this.type = InfobarMessageType.Prompt;
			this.bodyHtml = bodyHtml;
			this.footerHtml = footerHtml;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000136A3 File Offset: 0x000118A3
		public static void PutExceptionInfoIntoContextInfobarMessage(Exception e, OwaContext owaContext)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			owaContext[OwaContextProperty.InfobarMessage] = InfobarMessage.CreateErrorMessageFromException(e, owaContext.UserContext);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000136C6 File Offset: 0x000118C6
		public SanitizedHtmlString BodyHtml
		{
			get
			{
				return this.bodyHtml;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000136CE File Offset: 0x000118CE
		public SanitizedHtmlString FooterHtml
		{
			get
			{
				return this.footerHtml;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000136D6 File Offset: 0x000118D6
		public InfobarMessageType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000136DE File Offset: 0x000118DE
		public string TagId
		{
			get
			{
				return this.tagId;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000136E6 File Offset: 0x000118E6
		public SanitizedHtmlString ExpandSectionHtml
		{
			get
			{
				return this.expandSectionHtml;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000136EE File Offset: 0x000118EE
		public bool IsExpanding
		{
			get
			{
				return this.isExpanding;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000136F6 File Offset: 0x000118F6
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000136FE File Offset: 0x000118FE
		public bool IsActionResult
		{
			get
			{
				return this.isActionResult;
			}
			set
			{
				this.isActionResult = value;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00013707 File Offset: 0x00011907
		public void RenderMessageString(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(this.message);
		}

		// Token: 0x0400018E RID: 398
		private SanitizedHtmlString message;

		// Token: 0x0400018F RID: 399
		private SanitizedHtmlString bodyHtml;

		// Token: 0x04000190 RID: 400
		private SanitizedHtmlString footerHtml;

		// Token: 0x04000191 RID: 401
		private InfobarMessageType type = InfobarMessageType.Informational;

		// Token: 0x04000192 RID: 402
		private SanitizedHtmlString expandSectionHtml;

		// Token: 0x04000193 RID: 403
		private bool isExpanding;

		// Token: 0x04000194 RID: 404
		private string tagId;

		// Token: 0x04000195 RID: 405
		private bool isActionResult;
	}
}
