using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Common.Sniff
{
	// Token: 0x02000033 RID: 51
	public sealed class DataSniff
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000056D0 File Offset: 0x000038D0
		public DataSniff(int sampleSize)
		{
			this.sampleSize = ((sampleSize <= 256) ? sampleSize : 256);
			this.guessedMimeType = "application/octet-stream";
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000056FC File Offset: 0x000038FC
		public string FindMimeFromData(Stream file)
		{
			if (file == null || this.sampleSize <= 0)
			{
				return this.guessedMimeType;
			}
			this.ReadBuffer(file);
			this.SampleData();
			if (this.sampleSize <= 0)
			{
				return this.guessedMimeType;
			}
			if (!this.FoundMimeType())
			{
				if (this.countCtrl > 0 || this.countText + this.countFF >= 16 * (this.countCtrl + this.countHigh))
				{
					if (!this.CheckTextHeaders() && !this.CheckBinaryHeaders())
					{
						this.guessedMimeType = "text/plain";
					}
				}
				else if (!this.CheckBinaryHeaders() && !this.CheckTextHeaders())
				{
					this.guessedMimeType = "application/octet-stream";
				}
			}
			return this.guessedMimeType;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000057A7 File Offset: 0x000039A7
		public DataSniff.TypeOfStream TypeOfSniffedStream
		{
			get
			{
				return this.typeOfStream;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000057B0 File Offset: 0x000039B0
		private bool FoundMimeType()
		{
			bool result = false;
			if (this.foundCDF)
			{
				this.guessedMimeType = "application/x-cdf";
				result = true;
			}
			else if (this.foundXML)
			{
				this.guessedMimeType = "text/xml";
				result = true;
			}
			else if (this.foundHTML)
			{
				this.guessedMimeType = "text/html";
				result = true;
			}
			else if (this.foundXBitMap)
			{
				this.guessedMimeType = "image/x-xbitmap";
				result = true;
			}
			else if (this.foundMacBinhex)
			{
				this.guessedMimeType = "application/macbinhex40";
				result = true;
			}
			else if (this.foundTextScriptlet)
			{
				this.guessedMimeType = "text/scriptlet";
				result = true;
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005848 File Offset: 0x00003A48
		private void ReadBuffer(Stream stream)
		{
			bool flag = false;
			this.buffer = new byte[this.sampleSize];
			int num = stream.Read(this.buffer, 0, this.sampleSize);
			if (num >= 2)
			{
				if (this.buffer[0] == 255 && this.buffer[1] == 254)
				{
					this.typeOfStream = DataSniff.TypeOfStream.UCS16LittleEndian;
				}
				else if (this.buffer[0] == 254 && this.buffer[1] == 255)
				{
					this.typeOfStream = DataSniff.TypeOfStream.UCS16BigEndian;
				}
			}
			if (num == this.sampleSize && this.typeOfStream != DataSniff.TypeOfStream.Default)
			{
				byte[] array = new byte[this.sampleSize];
				int num2 = stream.Read(array, 0, this.sampleSize);
				if (num2 > 0 && num2 % 2 == 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.sampleSize = num;
				return;
			}
			Encoding srcEncoding = Encoding.Unicode;
			if (this.typeOfStream == DataSniff.TypeOfStream.UCS16BigEndian)
			{
				srcEncoding = Encoding.BigEndianUnicode;
			}
			Encoding ascii = Encoding.ASCII;
			byte[] array2 = Encoding.Convert(srcEncoding, ascii, this.buffer, 0, num);
			this.sampleSize = ((array2.Length > this.sampleSize) ? this.sampleSize : array2.Length);
			this.buffer = new byte[this.sampleSize];
			Array.Copy(array2, this.buffer, this.sampleSize);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005984 File Offset: 0x00003B84
		private void SampleData()
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			this.countNL = 0;
			this.countCR = 0;
			this.countFF = 0;
			this.countText = 0;
			this.countCtrl = 0;
			this.countHigh = 0;
			for (int i = 0; i < this.sampleSize - 1; i++)
			{
				byte b = this.buffer[i];
				bool flag3 = false;
				if (b == DataSniff.NLChar.Value)
				{
					this.countNL++;
				}
				else if (b == DataSniff.CRChar.Value)
				{
					this.countCR++;
				}
				else if (b == DataSniff.FFChar.Value)
				{
					this.countFF++;
				}
				else if (b == DataSniff.TabChar.Value)
				{
					this.countText++;
				}
				else if (b < 32)
				{
					this.countCtrl++;
				}
				else if (b >= 32 && b < 128)
				{
					this.countText++;
					flag3 = true;
				}
				else
				{
					this.countHigh++;
				}
				if (flag3)
				{
					if (b == DataSniff.AnchorChar.Value)
					{
						if (i + 5 < this.buffer.Length && DataSniff.AsciiString.EqualsNCI(this.buffer, i + 1, DataSniff.XMLStr) && (this.buffer[i + 5] == DataSniff.ColonChar.Value || this.buffer[i + 5] == DataSniff.SpaceChar.Value || this.buffer[i + 5] == DataSniff.TabChar.Value))
						{
							this.foundXML = true;
						}
						if (DataSniff.AsciiString.EqualsNCI(this.buffer, i + 1, DataSniff.ScripletStr))
						{
							this.foundTextScriptlet = true;
							return;
						}
						if (this.IsHtml(i, ref num))
						{
							return;
						}
						if (DataSniff.AsciiString.EqualsNCI(this.buffer, i + 1, DataSniff.ChannelStr))
						{
							this.foundCDF = true;
							return;
						}
					}
					else if (DataSniff.AsciiString.EqualsNCI(this.buffer, i, DataSniff.ChannelStr))
					{
						num += 50;
						if (num >= 100 && i == this.sampleSize - 1 && this.countText > this.sampleSize * 2 / 3)
						{
							this.foundHTML = true;
							return;
						}
					}
					else if (b == DataSniff.SharpChar.Value)
					{
						if (DataSniff.AsciiString.EqualsNC(this.buffer, i + 1, DataSniff.FileMagic.XbmMagic1))
						{
							flag = true;
						}
					}
					else if (b == DataSniff.UnderScoreChar.Value && flag2)
					{
						if (DataSniff.AsciiString.EqualsNC(this.buffer, i + 1, DataSniff.FileMagic.XbmMagic3))
						{
							this.foundXBitMap = true;
							return;
						}
					}
					else if (b == DataSniff.UnderScoreChar.Value && flag)
					{
						if (DataSniff.AsciiString.EqualsNC(this.buffer, i + 1, DataSniff.FileMagic.XbmMagic2))
						{
							flag2 = true;
						}
					}
					else if (b == DataSniff.CChar.Value && DataSniff.AsciiString.EqualsNC(this.buffer, i + 1, DataSniff.FileMagic.BinHexMagic))
					{
						this.foundMacBinhex = true;
						return;
					}
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005C84 File Offset: 0x00003E84
		private bool IsHtml(int index, ref int htmlConfidence)
		{
			if (DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.HTMLStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.HeadStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.TitleStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.BodyStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.ScriptStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.AHRefStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.PreStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.ImgStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.AHRefStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.PlainTextStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.TableStr))
			{
				this.foundHTML = true;
			}
			else if (DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.HrStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.AStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.SlashAStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.BStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.SlashBStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.PStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.SlashPStr) || DataSniff.AsciiString.EqualsNCI(this.buffer, index + 1, DataSniff.CommentStr))
			{
				htmlConfidence += 50;
				if (htmlConfidence >= 100 && index == this.sampleSize - 1 && this.countText >= this.sampleSize * 2 / 3)
				{
					this.foundHTML = true;
				}
			}
			return this.foundHTML;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005E74 File Offset: 0x00004074
		private bool IsBMP()
		{
			bool result = true;
			if (this.sampleSize < 2)
			{
				result = false;
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.BmpMagic))
			{
				result = false;
			}
			else if (this.sampleSize < 14)
			{
				result = false;
			}
			else if (this.buffer[6] != 0 || this.buffer[7] != 0 || this.buffer[8] != 0 || this.buffer[9] != 0)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005EE4 File Offset: 0x000040E4
		private bool MatchDWordAtOffset(uint magic, int offset)
		{
			bool result = true;
			if (this.sampleSize < offset + 4)
			{
				return false;
			}
			int num = (int)this.buffer[offset] << 24 | (int)this.buffer[offset + 1] << 16 | (int)this.buffer[offset + 2] << 8 | (int)this.buffer[offset + 3];
			if ((ulong)magic != (ulong)((long)num))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005F40 File Offset: 0x00004140
		private bool CheckTextHeaders()
		{
			bool result = true;
			if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.PdfMagic))
			{
				this.guessedMimeType = "application/pdf";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.PostscriptMagic))
			{
				this.guessedMimeType = "application/postscript";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.RichTextMagic))
			{
				this.guessedMimeType = "text/richtext";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.Base64Magic))
			{
				this.guessedMimeType = "application/base64";
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005FD4 File Offset: 0x000041D4
		private bool CheckBinaryHeaders()
		{
			bool result = true;
			if (DataSniff.AsciiString.EqualsNCI(this.buffer, 0, DataSniff.FileMagic.Gif87Magic) || DataSniff.AsciiString.EqualsNCI(this.buffer, 0, DataSniff.FileMagic.Gif89Magic))
			{
				this.guessedMimeType = "image/gif";
			}
			else if (this.buffer[0] == 255 && this.buffer[1] == 216)
			{
				this.guessedMimeType = "image/pjpeg";
			}
			else if (this.IsBMP())
			{
				this.guessedMimeType = "image/bmp";
			}
			else if (this.MatchDWordAtOffset(1380533830U, 0) && this.MatchDWordAtOffset(1463899717U, 8))
			{
				this.guessedMimeType = "audio/wav";
			}
			else if (this.MatchDWordAtOffset(779314176U, 0) || this.MatchDWordAtOffset(779316836U, 0) || this.MatchDWordAtOffset(6583086U, 0) || this.MatchDWordAtOffset(1684960046U, 0))
			{
				this.guessedMimeType = "audio/basic";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.TiffMagic) && this.sampleSize >= 3 && this.buffer[2] == 0)
			{
				this.guessedMimeType = "image/tiff";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.ExeMagic))
			{
				this.guessedMimeType = "application/x-msdownload";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.PngMagic))
			{
				this.guessedMimeType = "image/x-png";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.JGMagic) && this.sampleSize >= 3 && this.buffer[2] >= 3 && this.buffer[2] <= 31 && this.sampleSize >= 5 && this.buffer[4] == 0)
			{
				this.guessedMimeType = "image/x-jg";
			}
			else if (this.MatchDWordAtOffset(1297239878U, 0))
			{
				this.guessedMimeType = "audio/x-aiff";
			}
			else if (this.MatchDWordAtOffset(1179603533U, 0) && (this.MatchDWordAtOffset(1095321158U, 8) || this.MatchDWordAtOffset(1095321155U, 8)))
			{
				this.guessedMimeType = "audio/x-aiff";
			}
			else if (this.MatchDWordAtOffset(1380533830U, 0) && this.MatchDWordAtOffset(1096173856U, 8))
			{
				this.guessedMimeType = "video/avi";
			}
			else if (this.MatchDWordAtOffset(435U, 0) || this.MatchDWordAtOffset(442U, 0))
			{
				this.guessedMimeType = "video/mpeg";
			}
			else if (this.MatchDWordAtOffset(16777216U, 0) && this.MatchDWordAtOffset(541412678U, 40))
			{
				this.guessedMimeType = "image/x-emf";
			}
			else if (this.MatchDWordAtOffset(3620587162U, 0))
			{
				this.guessedMimeType = "image/x-wmf";
			}
			else if (this.MatchDWordAtOffset(3405691582U, 0))
			{
				this.guessedMimeType = "application/java";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.ZipMagic))
			{
				this.guessedMimeType = "application/x-zip-compressed";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.CompressMagic))
			{
				this.guessedMimeType = "application/x-compressed";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.GzipMagic))
			{
				this.guessedMimeType = "application/x-compressed";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.MIDMagic) && this.buffer[4] == 0)
			{
				this.guessedMimeType = "audio/mid";
			}
			else if (DataSniff.AsciiString.EqualsNC(this.buffer, 0, DataSniff.FileMagic.PdfMagic))
			{
				this.guessedMimeType = "application/pdf";
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040000B7 RID: 183
		private const int MaxSampleSize = 256;

		// Token: 0x040000B8 RID: 184
		private static readonly DataSniff.AsciiChar NLChar = new DataSniff.AsciiChar('\n');

		// Token: 0x040000B9 RID: 185
		private static readonly DataSniff.AsciiChar CRChar = new DataSniff.AsciiChar('\r');

		// Token: 0x040000BA RID: 186
		private static readonly DataSniff.AsciiChar FFChar = new DataSniff.AsciiChar('\f');

		// Token: 0x040000BB RID: 187
		private static readonly DataSniff.AsciiChar TabChar = new DataSniff.AsciiChar('\t');

		// Token: 0x040000BC RID: 188
		private static readonly DataSniff.AsciiChar AnchorChar = new DataSniff.AsciiChar('<');

		// Token: 0x040000BD RID: 189
		private static readonly DataSniff.AsciiChar ColonChar = new DataSniff.AsciiChar(':');

		// Token: 0x040000BE RID: 190
		private static readonly DataSniff.AsciiChar SpaceChar = new DataSniff.AsciiChar(' ');

		// Token: 0x040000BF RID: 191
		private static readonly DataSniff.AsciiChar SharpChar = new DataSniff.AsciiChar('#');

		// Token: 0x040000C0 RID: 192
		private static readonly DataSniff.AsciiChar UnderScoreChar = new DataSniff.AsciiChar('_');

		// Token: 0x040000C1 RID: 193
		private static readonly DataSniff.AsciiChar CChar = new DataSniff.AsciiChar('c');

		// Token: 0x040000C2 RID: 194
		private static readonly DataSniff.AsciiString XMLStr = new DataSniff.AsciiString("?XML");

		// Token: 0x040000C3 RID: 195
		private static readonly DataSniff.AsciiString ScripletStr = new DataSniff.AsciiString("SCRIPTLET");

		// Token: 0x040000C4 RID: 196
		private static readonly DataSniff.AsciiString HTMLStr = new DataSniff.AsciiString("HTML");

		// Token: 0x040000C5 RID: 197
		private static readonly DataSniff.AsciiString HeadStr = new DataSniff.AsciiString("HEAD");

		// Token: 0x040000C6 RID: 198
		private static readonly DataSniff.AsciiString TitleStr = new DataSniff.AsciiString("TITLE");

		// Token: 0x040000C7 RID: 199
		private static readonly DataSniff.AsciiString BodyStr = new DataSniff.AsciiString("BODY");

		// Token: 0x040000C8 RID: 200
		private static readonly DataSniff.AsciiString ScriptStr = new DataSniff.AsciiString("SCRIPT");

		// Token: 0x040000C9 RID: 201
		private static readonly DataSniff.AsciiString AHRefStr = new DataSniff.AsciiString("A HREF");

		// Token: 0x040000CA RID: 202
		private static readonly DataSniff.AsciiString PreStr = new DataSniff.AsciiString("PRE");

		// Token: 0x040000CB RID: 203
		private static readonly DataSniff.AsciiString ImgStr = new DataSniff.AsciiString("IMG");

		// Token: 0x040000CC RID: 204
		private static readonly DataSniff.AsciiString PlainTextStr = new DataSniff.AsciiString("PLAINTEXT");

		// Token: 0x040000CD RID: 205
		private static readonly DataSniff.AsciiString TableStr = new DataSniff.AsciiString("TABLE");

		// Token: 0x040000CE RID: 206
		private static readonly DataSniff.AsciiString HrStr = new DataSniff.AsciiString("HR");

		// Token: 0x040000CF RID: 207
		private static readonly DataSniff.AsciiString AStr = new DataSniff.AsciiString("A");

		// Token: 0x040000D0 RID: 208
		private static readonly DataSniff.AsciiString SlashAStr = new DataSniff.AsciiString("/A");

		// Token: 0x040000D1 RID: 209
		private static readonly DataSniff.AsciiString BStr = new DataSniff.AsciiString("B");

		// Token: 0x040000D2 RID: 210
		private static readonly DataSniff.AsciiString SlashBStr = new DataSniff.AsciiString("/B");

		// Token: 0x040000D3 RID: 211
		private static readonly DataSniff.AsciiString PStr = new DataSniff.AsciiString("P");

		// Token: 0x040000D4 RID: 212
		private static readonly DataSniff.AsciiString SlashPStr = new DataSniff.AsciiString("/P");

		// Token: 0x040000D5 RID: 213
		private static readonly DataSniff.AsciiString CommentStr = new DataSniff.AsciiString("!--");

		// Token: 0x040000D6 RID: 214
		private static readonly DataSniff.AsciiString ChannelStr = new DataSniff.AsciiString("CHANNEL");

		// Token: 0x040000D7 RID: 215
		private DataSniff.TypeOfStream typeOfStream;

		// Token: 0x040000D8 RID: 216
		private bool foundHTML;

		// Token: 0x040000D9 RID: 217
		private bool foundXBitMap;

		// Token: 0x040000DA RID: 218
		private bool foundMacBinhex;

		// Token: 0x040000DB RID: 219
		private bool foundCDF;

		// Token: 0x040000DC RID: 220
		private bool foundTextScriptlet;

		// Token: 0x040000DD RID: 221
		private bool foundXML;

		// Token: 0x040000DE RID: 222
		private int countNL;

		// Token: 0x040000DF RID: 223
		private int countCR;

		// Token: 0x040000E0 RID: 224
		private int countFF;

		// Token: 0x040000E1 RID: 225
		private int countText;

		// Token: 0x040000E2 RID: 226
		private int countCtrl;

		// Token: 0x040000E3 RID: 227
		private int countHigh;

		// Token: 0x040000E4 RID: 228
		private byte[] buffer;

		// Token: 0x040000E5 RID: 229
		private int sampleSize;

		// Token: 0x040000E6 RID: 230
		private string guessedMimeType;

		// Token: 0x02000034 RID: 52
		public enum TypeOfStream
		{
			// Token: 0x040000E8 RID: 232
			Default,
			// Token: 0x040000E9 RID: 233
			UCS16LittleEndian,
			// Token: 0x040000EA RID: 234
			UCS16BigEndian
		}

		// Token: 0x02000035 RID: 53
		private sealed class FileMagic
		{
			// Token: 0x060000FF RID: 255 RVA: 0x00006530 File Offset: 0x00004730
			private FileMagic()
			{
			}

			// Token: 0x040000EB RID: 235
			public const uint AU_SUN_MAGIC = 779316836U;

			// Token: 0x040000EC RID: 236
			public const uint AU_SUN_INV_MAGIC = 1684960046U;

			// Token: 0x040000ED RID: 237
			public const uint AU_DEC_MAGIC = 779314176U;

			// Token: 0x040000EE RID: 238
			public const uint AU_DEC_INV_MAGIC = 6583086U;

			// Token: 0x040000EF RID: 239
			public const uint AIFF_MAGIC = 1179603533U;

			// Token: 0x040000F0 RID: 240
			public const uint AIFF_INV_MAGIC = 1297239878U;

			// Token: 0x040000F1 RID: 241
			public const uint AIFF_MAGIC_MORE_1 = 1095321158U;

			// Token: 0x040000F2 RID: 242
			public const uint AIFF_MAGIC_MORE_2 = 1095321155U;

			// Token: 0x040000F3 RID: 243
			public const uint RIFF_MAGIC = 1380533830U;

			// Token: 0x040000F4 RID: 244
			public const uint AVI_MAGIC = 1096173856U;

			// Token: 0x040000F5 RID: 245
			public const uint WAV_MAGIC = 1463899717U;

			// Token: 0x040000F6 RID: 246
			public const uint JAVA_MAGIC = 3405691582U;

			// Token: 0x040000F7 RID: 247
			public const uint MPEG_MAGIC = 435U;

			// Token: 0x040000F8 RID: 248
			public const uint MPEG_MAGIC_2 = 442U;

			// Token: 0x040000F9 RID: 249
			public const uint EMF_MAGIC_1 = 16777216U;

			// Token: 0x040000FA RID: 250
			public const uint EMF_MAGIC_2 = 541412678U;

			// Token: 0x040000FB RID: 251
			public const uint WMF_MAGIC = 3620587162U;

			// Token: 0x040000FC RID: 252
			public const uint JPEG_MAGIC_1 = 255U;

			// Token: 0x040000FD RID: 253
			public const uint JPEG_MAGIC_2 = 216U;

			// Token: 0x040000FE RID: 254
			public static readonly DataSniff.AsciiString RichTextMagic = new DataSniff.AsciiString("{\\rtf");

			// Token: 0x040000FF RID: 255
			public static readonly DataSniff.AsciiString PostscriptMagic = new DataSniff.AsciiString("%!");

			// Token: 0x04000100 RID: 256
			public static readonly DataSniff.AsciiString BinHexMagic = new DataSniff.AsciiString("onverted with BinHex");

			// Token: 0x04000101 RID: 257
			public static readonly DataSniff.AsciiString Base64Magic = new DataSniff.AsciiString("begin");

			// Token: 0x04000102 RID: 258
			public static readonly DataSniff.AsciiString Gif87Magic = new DataSniff.AsciiString("GIF87");

			// Token: 0x04000103 RID: 259
			public static readonly DataSniff.AsciiString Gif89Magic = new DataSniff.AsciiString("GIF89");

			// Token: 0x04000104 RID: 260
			public static readonly DataSniff.AsciiString TiffMagic = new DataSniff.AsciiString("MM");

			// Token: 0x04000105 RID: 261
			public static readonly DataSniff.AsciiString BmpMagic = new DataSniff.AsciiString("BM");

			// Token: 0x04000106 RID: 262
			public static readonly DataSniff.AsciiString ZipMagic = new DataSniff.AsciiString("PK");

			// Token: 0x04000107 RID: 263
			public static readonly DataSniff.AsciiString ExeMagic = new DataSniff.AsciiString("MZ");

			// Token: 0x04000108 RID: 264
			public static readonly DataSniff.AsciiString PngMagic = new DataSniff.AsciiString("\\211PNG\\r\\n\\032\\n");

			// Token: 0x04000109 RID: 265
			public static readonly DataSniff.AsciiString CompressMagic = new DataSniff.AsciiString("\\037\\235");

			// Token: 0x0400010A RID: 266
			public static readonly DataSniff.AsciiString GzipMagic = new DataSniff.AsciiString("\\037\\213");

			// Token: 0x0400010B RID: 267
			public static readonly DataSniff.AsciiString XbmMagic1 = new DataSniff.AsciiString("define");

			// Token: 0x0400010C RID: 268
			public static readonly DataSniff.AsciiString XbmMagic2 = new DataSniff.AsciiString("width");

			// Token: 0x0400010D RID: 269
			public static readonly DataSniff.AsciiString XbmMagic3 = new DataSniff.AsciiString("bits");

			// Token: 0x0400010E RID: 270
			public static readonly DataSniff.AsciiString PdfMagic = new DataSniff.AsciiString("%PDF");

			// Token: 0x0400010F RID: 271
			public static readonly DataSniff.AsciiString JGMagic = new DataSniff.AsciiString("JG");

			// Token: 0x04000110 RID: 272
			public static readonly DataSniff.AsciiString MIDMagic = new DataSniff.AsciiString("MThd");
		}

		// Token: 0x02000036 RID: 54
		public sealed class MimeType
		{
			// Token: 0x06000101 RID: 257 RVA: 0x00006662 File Offset: 0x00004862
			private MimeType()
			{
			}

			// Token: 0x04000111 RID: 273
			public const string NULL = "(null)";

			// Token: 0x04000112 RID: 274
			public const string TextPlain = "text/plain";

			// Token: 0x04000113 RID: 275
			public const string TextRichText = "text/richtext";

			// Token: 0x04000114 RID: 276
			public const string ImageXBitmap = "image/x-xbitmap";

			// Token: 0x04000115 RID: 277
			public const string ApplicationPostscript = "application/postscript";

			// Token: 0x04000116 RID: 278
			public const string ApplicationBase64 = "application/base64";

			// Token: 0x04000117 RID: 279
			public const string ApplicationMacBinhex = "application/macbinhex40";

			// Token: 0x04000118 RID: 280
			public const string ApplicationPdf = "application/pdf";

			// Token: 0x04000119 RID: 281
			public const string ApplicationCDF = "application/x-cdf";

			// Token: 0x0400011A RID: 282
			public const string ApplicationNETCDF = "application/x-netcdf";

			// Token: 0x0400011B RID: 283
			public const string Multipartmixedreplace = "multipart/x-mixed-replace";

			// Token: 0x0400011C RID: 284
			public const string Multipartmixed = "multipart/mixed";

			// Token: 0x0400011D RID: 285
			public const string TextScriptlet = "text/scriptlet";

			// Token: 0x0400011E RID: 286
			public const string TextComponent = "text/x-component";

			// Token: 0x0400011F RID: 287
			public const string TextXML = "text/xml";

			// Token: 0x04000120 RID: 288
			public const string ApplicationHTA = "application/hta";

			// Token: 0x04000121 RID: 289
			public const string AudioAiff = "audio/x-aiff";

			// Token: 0x04000122 RID: 290
			public const string AudioBasic = "audio/basic";

			// Token: 0x04000123 RID: 291
			public const string AudioWav = "audio/wav";

			// Token: 0x04000124 RID: 292
			public const string AudioMID = "audio/mid";

			// Token: 0x04000125 RID: 293
			public const string ImageGif = "image/gif";

			// Token: 0x04000126 RID: 294
			public const string ImagePJpeg = "image/pjpeg";

			// Token: 0x04000127 RID: 295
			public const string ImageJpeg = "image/jpeg";

			// Token: 0x04000128 RID: 296
			public const string ImageTiff = "image/tiff";

			// Token: 0x04000129 RID: 297
			public const string ImagePng = "image/x-png";

			// Token: 0x0400012A RID: 298
			public const string ImageBmp = "image/bmp";

			// Token: 0x0400012B RID: 299
			public const string ImageJG = "image/x-jg";

			// Token: 0x0400012C RID: 300
			public const string ImageEmf = "image/x-emf";

			// Token: 0x0400012D RID: 301
			public const string ImageWmf = "image/x-wmf";

			// Token: 0x0400012E RID: 302
			public const string VideoAvi = "video/avi";

			// Token: 0x0400012F RID: 303
			public const string VideoMpeg = "video/mpeg";

			// Token: 0x04000130 RID: 304
			public const string ApplicationCompressed = "application/x-compressed";

			// Token: 0x04000131 RID: 305
			public const string ApplicationJava = "application/java";

			// Token: 0x04000132 RID: 306
			public const string ApplicationMSDownload = "application/x-msdownload";

			// Token: 0x04000133 RID: 307
			public const string ApplicationCommonDataFormat = "application/x-netcdf";

			// Token: 0x04000134 RID: 308
			public const string ApplicationZipCompressed = "application/x-zip-compressed";

			// Token: 0x04000135 RID: 309
			public const string ApplicationGzipCompressed = "application/x-gzip-compressed";

			// Token: 0x04000136 RID: 310
			public const string TextHTML = "text/html";

			// Token: 0x04000137 RID: 311
			public const string ApplicationOctetStream = "application/octet-stream";
		}

		// Token: 0x02000037 RID: 55
		private class AsciiString
		{
			// Token: 0x06000102 RID: 258 RVA: 0x0000666C File Offset: 0x0000486C
			public AsciiString(string str)
			{
				char[] array = str.ToCharArray();
				this.asciiValue = new byte[array.Length];
				DataSniff.AsciiString.encoder.GetBytes(array, 0, array.Length, this.asciiValue, 0);
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000103 RID: 259 RVA: 0x000066AB File Offset: 0x000048AB
			public byte[] Value
			{
				get
				{
					return this.asciiValue;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000104 RID: 260 RVA: 0x000066B3 File Offset: 0x000048B3
			public int Length
			{
				get
				{
					return this.asciiValue.Length;
				}
			}

			// Token: 0x06000105 RID: 261 RVA: 0x000066BD File Offset: 0x000048BD
			public static bool EqualsNCI(byte[] str1, int start, DataSniff.AsciiString str2)
			{
				return DataSniff.AsciiString.EqualsNCI(str1, start, str2.Value, str2.Length);
			}

			// Token: 0x06000106 RID: 262 RVA: 0x000066D4 File Offset: 0x000048D4
			private static bool EqualsNCI(byte[] str1, int start, byte[] str2, int count)
			{
				if (start < 0)
				{
					return false;
				}
				for (int num = 0; num != count; num++)
				{
					if (start + num >= str1.Length)
					{
						return num >= str2.Length;
					}
					if (num == str2.Length)
					{
						return false;
					}
					if (DataSniff.AsciiChar.ToLower(str1[start + num]) != DataSniff.AsciiChar.ToLower(str2[num]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00006724 File Offset: 0x00004924
			public static bool EqualsNC(byte[] str1, int start, DataSniff.AsciiString str2)
			{
				return DataSniff.AsciiString.EqualsNC(str1, start, str2.Value, str2.Length);
			}

			// Token: 0x06000108 RID: 264 RVA: 0x0000673C File Offset: 0x0000493C
			private static bool EqualsNC(byte[] str1, int start, byte[] str2, int count)
			{
				if (start < 0)
				{
					return false;
				}
				for (int num = 0; num != count; num++)
				{
					if (start + num >= str1.Length)
					{
						return num >= str2.Length;
					}
					if (num == str2.Length)
					{
						return false;
					}
					if (str1[start + num] != str2[num])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000138 RID: 312
			private static ASCIIEncoding encoder = new ASCIIEncoding();

			// Token: 0x04000139 RID: 313
			private byte[] asciiValue;
		}

		// Token: 0x02000038 RID: 56
		private class AsciiChar
		{
			// Token: 0x0600010A RID: 266 RVA: 0x00006790 File Offset: 0x00004990
			public AsciiChar(char ch)
			{
				char[] array = new char[1];
				byte[] array2 = new byte[2];
				array[0] = ch;
				DataSniff.AsciiChar.encoder.GetBytes(array, 0, 1, array2, 0);
				this.asciiValue = array2[0];
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600010B RID: 267 RVA: 0x000067CE File Offset: 0x000049CE
			public byte Value
			{
				get
				{
					return this.asciiValue;
				}
			}

			// Token: 0x0600010C RID: 268 RVA: 0x000067D6 File Offset: 0x000049D6
			public static byte ToLower(byte ch)
			{
				return DataSniff.AsciiChar.LowerC[(int)ch];
			}

			// Token: 0x0400013A RID: 314
			internal static readonly byte[] LowerC = new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23,
				24,
				25,
				26,
				27,
				28,
				29,
				30,
				31,
				32,
				33,
				34,
				35,
				36,
				37,
				38,
				39,
				40,
				41,
				42,
				43,
				44,
				45,
				46,
				47,
				48,
				49,
				50,
				51,
				52,
				53,
				54,
				55,
				56,
				57,
				58,
				59,
				60,
				61,
				62,
				63,
				64,
				97,
				98,
				99,
				100,
				101,
				102,
				103,
				104,
				105,
				106,
				107,
				108,
				109,
				110,
				111,
				112,
				113,
				114,
				115,
				116,
				117,
				118,
				119,
				120,
				121,
				122,
				91,
				92,
				93,
				94,
				95,
				96,
				97,
				98,
				99,
				100,
				101,
				102,
				103,
				104,
				105,
				106,
				107,
				108,
				109,
				110,
				111,
				112,
				113,
				114,
				115,
				116,
				117,
				118,
				119,
				120,
				121,
				122,
				123,
				124,
				125,
				126,
				127,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};

			// Token: 0x0400013B RID: 315
			private static ASCIIEncoding encoder = new ASCIIEncoding();

			// Token: 0x0400013C RID: 316
			private byte asciiValue;
		}
	}
}
