using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000002 RID: 2
	internal class MessageBody : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private MessageBody(BodyFormat bodyFormat, Stream stream, int codePage)
		{
			TextConverter converter;
			switch (bodyFormat)
			{
			case BodyFormat.Text:
				converter = new TextToText
				{
					InputEncoding = Charset.GetEncoding(codePage)
				};
				break;
			case BodyFormat.Rtf:
			{
				RtfToText rtfToText = new RtfToText(TextExtractionMode.ExtractText);
				converter = rtfToText;
				break;
			}
			case BodyFormat.Html:
				converter = new HtmlToText(TextExtractionMode.ExtractText)
				{
					InputEncoding = Charset.GetEncoding(codePage)
				};
				break;
			default:
				throw new InvalidOperationException();
			}
			this.reader = new ConverterReader(stream, converter);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002148 File Offset: 0x00000348
		public void Dispose()
		{
			this.reader.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000215B File Offset: 0x0000035B
		public override string ToString()
		{
			return this.reader.ReadToEnd();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002168 File Offset: 0x00000368
		internal static MessageBody Create(Body body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			BodyFormat bodyFormat = body.BodyFormat;
			if (bodyFormat != BodyFormat.Text && bodyFormat != BodyFormat.Html && bodyFormat != BodyFormat.Rtf)
			{
				return null;
			}
			Stream rawContentReadStream;
			if (!body.TryGetContentReadStream(out rawContentReadStream))
			{
				if (body.MimePart == null)
				{
					return null;
				}
				rawContentReadStream = body.MimePart.GetRawContentReadStream();
			}
			string charsetName = body.CharsetName;
			Encoding ascii;
			if (charsetName == null || !Charset.TryGetEncoding(charsetName, out ascii))
			{
				ascii = Encoding.ASCII;
			}
			return new MessageBody(bodyFormat, rawContentReadStream, ascii.CodePage);
		}

		// Token: 0x04000001 RID: 1
		private readonly TextReader reader;
	}
}
