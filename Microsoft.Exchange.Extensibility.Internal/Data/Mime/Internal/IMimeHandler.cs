using System;
using System.IO;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x0200001A RID: 26
	internal interface IMimeHandler
	{
		// Token: 0x0600008A RID: 138
		void PartStart(bool isInline, string inlineFileName, out PartParseOption partParseOption, out Stream outerContentWriteStream);

		// Token: 0x0600008B RID: 139
		void HeaderStart(HeaderId headerId, string name, out HeaderParseOption headerParseOption);

		// Token: 0x0600008C RID: 140
		void Header(Header header);

		// Token: 0x0600008D RID: 141
		void EndOfHeaders(string mediaType, ContentTransferEncoding cte, out PartContentParseOption partContentParseOption);

		// Token: 0x0600008E RID: 142
		void PartContent(byte[] buffer, int offset, int length);

		// Token: 0x0600008F RID: 143
		void PartEnd();

		// Token: 0x06000090 RID: 144
		void EndOfFile();
	}
}
