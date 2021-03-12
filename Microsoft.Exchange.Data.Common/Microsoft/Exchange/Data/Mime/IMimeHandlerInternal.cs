using System;
using System.IO;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000072 RID: 114
	internal interface IMimeHandlerInternal
	{
		// Token: 0x06000451 RID: 1105
		void PartStart(bool isInline, string inlineFileName, out PartParseOptionInternal partParseOption, out Stream outerContentWriteStream);

		// Token: 0x06000452 RID: 1106
		void HeaderStart(HeaderId headerId, string name, out HeaderParseOptionInternal headerParseOption);

		// Token: 0x06000453 RID: 1107
		void Header(Header header);

		// Token: 0x06000454 RID: 1108
		void EndOfHeaders(string mediaType, ContentTransferEncoding cte, out PartContentParseOptionInternal partContentParseOption);

		// Token: 0x06000455 RID: 1109
		void PartContent(byte[] buffer, int offset, int length);

		// Token: 0x06000456 RID: 1110
		void PartEnd();

		// Token: 0x06000457 RID: 1111
		void EndOfFile();
	}
}
