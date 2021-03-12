using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005DB RID: 1499
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class IcalSquigglieFixHtmlReader : TextReader
	{
		// Token: 0x06003DAE RID: 15790 RVA: 0x000FEE1C File Offset: 0x000FD01C
		internal IcalSquigglieFixHtmlReader(Stream bodyStream, Charset charset, bool trustMetaTag)
		{
			IcalSquigglieFixHtmlReader <>4__this = this;
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "IcalSquigglieFixHtmlReader::Constructor", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				Encoding unicodeEncoding = ConvertUtils.UnicodeEncoding;
				HtmlToHtml htmlToHtml = new HtmlToHtml();
				htmlToHtml.InputEncoding = charset.GetEncoding();
				htmlToHtml.OutputEncoding = unicodeEncoding;
				htmlToHtml.DetectEncodingFromMetaTag = trustMetaTag;
				<>4__this.htmlReader = new ConverterReader(bodyStream, htmlToHtml);
			});
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000FEE78 File Offset: 0x000FD078
		public override int Peek()
		{
			if (this.IsSquigglieFound() && !this.IsSquigglieInsertWrittenOut())
			{
				return (int)"<BR>"[this.squigglieInsertPosition];
			}
			return this.htmlReader.Peek();
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000FEEA8 File Offset: 0x000FD0A8
		public override int Read()
		{
			if (this.IsSquigglieFound() && !this.IsSquigglieInsertWrittenOut())
			{
				return (int)"<BR>"[this.squigglieInsertPosition++];
			}
			int num = this.htmlReader.Read();
			if (num != -1 && !this.IsSquigglieFound())
			{
				int num2 = this.squigglieTextPosition;
				while (num2 != -1 && num != (int)"*~*~*~*~*~*~*~*~*~*"[num2])
				{
					num2 = IcalSquigglieFixHtmlReader.squigglieRollback[num2];
				}
				this.squigglieTextPosition = num2 + 1;
			}
			if (num != 126 && num != 42)
			{
				this.lastCharacterRead = num;
			}
			return num;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000FEF38 File Offset: 0x000FD138
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			int num;
			for (num = 0; num != count; num++)
			{
				if (this.IsSquigglieFound() && this.IsSquigglieInsertWrittenOut())
				{
					return num + this.htmlReader.ReadBlock(buffer, index + num, count - num);
				}
				int num2 = this.Read();
				if (num2 == -1)
				{
					break;
				}
				buffer[index + num] = (char)num2;
			}
			return num;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000FEF89 File Offset: 0x000FD189
		private bool IsSquigglieFound()
		{
			if (this.squigglieTextPosition == "*~*~*~*~*~*~*~*~*~*".Length)
			{
				if (this.lastCharacterRead != 32)
				{
					return true;
				}
				this.squigglieTextPosition = 0;
			}
			return false;
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000FEFB1 File Offset: 0x000FD1B1
		private bool IsSquigglieInsertWrittenOut()
		{
			return this.squigglieInsertPosition == "<BR>".Length;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000FEFC5 File Offset: 0x000FD1C5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.htmlReader != null)
			{
				this.htmlReader.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04002121 RID: 8481
		private const string SquigglieInsert = "<BR>";

		// Token: 0x04002122 RID: 8482
		private const string SquigglieText = "*~*~*~*~*~*~*~*~*~*";

		// Token: 0x04002123 RID: 8483
		private static int[] squigglieRollback = new int[]
		{
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1,
			0,
			-1
		};

		// Token: 0x04002124 RID: 8484
		private int squigglieTextPosition;

		// Token: 0x04002125 RID: 8485
		private int squigglieInsertPosition;

		// Token: 0x04002126 RID: 8486
		private int lastCharacterRead;

		// Token: 0x04002127 RID: 8487
		private ConverterReader htmlReader;
	}
}
