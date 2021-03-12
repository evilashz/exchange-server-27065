using System;
using System.Text;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x0200011D RID: 285
	internal class AsciiEncoderFallback : EncoderFallback
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00068CAE File Offset: 0x00066EAE
		public override int MaxCharCount
		{
			get
			{
				return AsciiEncoderFallback.AsciiFallbackBuffer.MaxCharCount;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00068CB8 File Offset: 0x00066EB8
		public static string GetCharacterFallback(char charUnknown)
		{
			if (charUnknown <= 'œ' && charUnknown >= '\u0082')
			{
				if (charUnknown <= 'æ')
				{
					switch (charUnknown)
					{
					case '\u0082':
					case '\u0091':
					case '\u0092':
						return "'";
					case '\u0083':
						return "f";
					case '\u0084':
					case '\u0093':
					case '\u0094':
						return "\"";
					case '\u0085':
						return "...";
					case '\u0086':
					case '\u0087':
					case '\u0088':
					case '\u0089':
					case '\u008a':
					case '\u008d':
					case '\u008e':
					case '\u008f':
					case '\u0090':
					case '\u009a':
					case '\u009d':
					case '\u009e':
					case '\u009f':
					case '¡':
					case '£':
					case '§':
					case '¨':
					case 'ª':
					case '¬':
					case '¯':
					case '°':
					case '±':
					case '´':
					case 'µ':
					case '¶':
					case 'º':
					case '¿':
					case 'À':
					case 'Á':
					case 'Â':
					case 'Ã':
					case 'Ä':
					case 'Å':
						break;
					case '\u008b':
						return "<";
					case '\u008c':
						return "OE";
					case '\u0095':
						return "*";
					case '\u0096':
						return "-";
					case '\u0097':
						return "-";
					case '\u0098':
						return "~";
					case '\u0099':
						return "(tm)";
					case '\u009b':
						return ">";
					case '\u009c':
						return "oe";
					case '\u00a0':
						return " ";
					case '¢':
						return "c";
					case '¤':
						return "$";
					case '¥':
						return "Y";
					case '¦':
						return "|";
					case '©':
						return "(c)";
					case '«':
						return "<";
					case '­':
						return string.Empty;
					case '®':
						return "(r)";
					case '²':
						return "^2";
					case '³':
						return "^3";
					case '·':
						return "*";
					case '¸':
						return ",";
					case '¹':
						return "^1";
					case '»':
						return ">";
					case '¼':
						return "(1/4)";
					case '½':
						return "(1/2)";
					case '¾':
						return "(3/4)";
					case 'Æ':
						return "AE";
					default:
						if (charUnknown == 'æ')
						{
							return "ae";
						}
						break;
					}
				}
				else
				{
					switch (charUnknown)
					{
					case 'Ĳ':
						return "IJ";
					case 'ĳ':
						return "ij";
					default:
						switch (charUnknown)
						{
						case 'Œ':
							return "OE";
						case 'œ':
							return "oe";
						}
						break;
					}
				}
			}
			else if (charUnknown >= '\u2002' && charUnknown <= '™')
			{
				if (charUnknown <= '…')
				{
					switch (charUnknown)
					{
					case '\u2002':
					case '\u2003':
						return " ";
					default:
						switch (charUnknown)
						{
						case '‑':
							return "-";
						case '‒':
						case '―':
						case '‖':
						case '‗':
						case '‛':
						case '‟':
						case '†':
						case '‡':
							break;
						case '–':
						case '—':
							return "-";
						case '‘':
						case '’':
						case '‚':
							return "'";
						case '“':
						case '”':
						case '„':
							return "\"";
						case '•':
							return "*";
						default:
							if (charUnknown == '…')
							{
								return "...";
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (charUnknown)
					{
					case '‹':
						return "<";
					case '›':
						return ">";
					default:
						if (charUnknown == '€')
						{
							return "EUR";
						}
						if (charUnknown == '™')
						{
							return "(tm)";
						}
						break;
					}
				}
			}
			else if (charUnknown >= '☹' && charUnknown <= '☺')
			{
				switch (charUnknown)
				{
				case '☹':
					return ":(";
				case '☺':
					return ":)";
				}
			}
			return null;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00069055 File Offset: 0x00067255
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new AsciiEncoderFallback.AsciiFallbackBuffer();
		}

		// Token: 0x0200011E RID: 286
		private class AsciiFallbackBuffer : EncoderFallbackBuffer
		{
			// Token: 0x1700038A RID: 906
			// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00069064 File Offset: 0x00067264
			public static int MaxCharCount
			{
				get
				{
					return 5;
				}
			}

			// Token: 0x1700038B RID: 907
			// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00069067 File Offset: 0x00067267
			public override int Remaining
			{
				get
				{
					if (this.fallbackString != null)
					{
						return this.fallbackString.Length - this.fallbackIndex;
					}
					return 0;
				}
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x00069085 File Offset: 0x00067285
			public override bool Fallback(char charUnknown, int index)
			{
				this.fallbackIndex = 0;
				this.fallbackString = AsciiEncoderFallback.GetCharacterFallback(charUnknown);
				if (this.fallbackString == null)
				{
					this.fallbackString = "?";
				}
				return true;
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x000690AE File Offset: 0x000672AE
			public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
			{
				this.fallbackIndex = 0;
				this.fallbackString = "?";
				return true;
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x000690C4 File Offset: 0x000672C4
			public override char GetNextChar()
			{
				if (this.fallbackString == null || this.fallbackIndex == this.fallbackString.Length)
				{
					return '\0';
				}
				return this.fallbackString[this.fallbackIndex++];
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x0006910A File Offset: 0x0006730A
			public override bool MovePrevious()
			{
				if (this.fallbackIndex > 0)
				{
					this.fallbackIndex--;
					return true;
				}
				return false;
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00069126 File Offset: 0x00067326
			public override void Reset()
			{
				this.fallbackString = "?";
				this.fallbackIndex = 0;
			}

			// Token: 0x04000E4D RID: 3661
			private int fallbackIndex;

			// Token: 0x04000E4E RID: 3662
			private string fallbackString;
		}
	}
}
