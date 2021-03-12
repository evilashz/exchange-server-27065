using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F69 RID: 3945
	public class HtmlReaderWrapper : IHtmlReader, IDisposable
	{
		// Token: 0x060063EE RID: 25582 RVA: 0x00137D0C File Offset: 0x00135F0C
		internal HtmlReaderWrapper(string html)
		{
			StringReader input = new StringReader(html);
			this.reader = new HtmlReader(input);
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x00137D32 File Offset: 0x00135F32
		public void SetNormalizeHtml(bool value)
		{
			this.reader.NormalizeHtml = value;
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00137D40 File Offset: 0x00135F40
		public bool ParseNext()
		{
			return this.reader.ReadNextToken();
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x00137D50 File Offset: 0x00135F50
		public TokenKind GetTokenKind()
		{
			switch (this.reader.TokenKind)
			{
			case HtmlTokenKind.Text:
				return TokenKind.Text;
			case HtmlTokenKind.StartTag:
				return TokenKind.StartTag;
			case HtmlTokenKind.EndTag:
				return TokenKind.EndTag;
			case HtmlTokenKind.EmptyElementTag:
				return TokenKind.EmptyTag;
			case HtmlTokenKind.OverlappedClose:
				return TokenKind.OverlappedClose;
			case HtmlTokenKind.OverlappedReopen:
				return TokenKind.OverlappedReopen;
			}
			return TokenKind.IgnorableTag;
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x00137D9A File Offset: 0x00135F9A
		public string GetTagName()
		{
			return this.reader.ReadTagName();
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x00137DA7 File Offset: 0x00135FA7
		public int GetCurrentOffset()
		{
			return this.reader.CurrentOffset;
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x00137DB4 File Offset: 0x00135FB4
		public bool ParseNextAttribute()
		{
			return this.reader.AttributeReader.ReadNext();
		}

		// Token: 0x060063F5 RID: 25589 RVA: 0x00137DD4 File Offset: 0x00135FD4
		public string GetAttributeName()
		{
			return this.reader.AttributeReader.ReadName();
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x00137DF4 File Offset: 0x00135FF4
		public string GetAttributeValue()
		{
			return this.reader.AttributeReader.ReadValue();
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x00137E14 File Offset: 0x00136014
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x04003529 RID: 13609
		private HtmlReader reader;
	}
}
