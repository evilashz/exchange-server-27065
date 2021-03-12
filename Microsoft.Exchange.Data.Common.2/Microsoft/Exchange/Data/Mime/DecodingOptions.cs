using System;
using System.Text;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200002E RID: 46
	public struct DecodingOptions
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00009492 File Offset: 0x00007692
		public DecodingOptions(DecodingFlags decodingFlags)
		{
			this = new DecodingOptions(decodingFlags, null);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000949C File Offset: 0x0000769C
		public DecodingOptions(DecodingFlags decodingFlags, Encoding encoding)
		{
			this.decodingFlags = decodingFlags;
			this.charset = ((encoding == null) ? null : Charset.GetCharset(encoding));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000094B7 File Offset: 0x000076B7
		public DecodingOptions(DecodingFlags decodingFlags, string charsetName)
		{
			this.decodingFlags = decodingFlags;
			this.charset = ((charsetName == null) ? null : Charset.GetCharset(charsetName));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000094D4 File Offset: 0x000076D4
		internal DecodingOptions(string charsetName)
		{
			this = new DecodingOptions(DecodingOptions.Default.DecodingFlags, charsetName);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000094F8 File Offset: 0x000076F8
		internal static Charset DefaultCharset
		{
			get
			{
				if (DecodingOptions.defaultCharset == null)
				{
					Charset charset = Charset.DefaultMimeCharset;
					if (!charset.IsAvailable || charset.AsciiSupport < CodePageAsciiSupport.Fine)
					{
						charset = Charset.UTF8;
					}
					DecodingOptions.defaultCharset = charset;
				}
				return DecodingOptions.defaultCharset;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009534 File Offset: 0x00007734
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000953C File Offset: 0x0000773C
		public DecodingFlags DecodingFlags
		{
			get
			{
				return this.decodingFlags;
			}
			internal set
			{
				this.decodingFlags = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00009545 File Offset: 0x00007745
		public string CharsetName
		{
			get
			{
				if (this.charset != null)
				{
					return this.charset.Name;
				}
				return null;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000955C File Offset: 0x0000775C
		public Encoding CharsetEncoding
		{
			get
			{
				if (this.charset != null && this.charset.IsAvailable)
				{
					return this.charset.GetEncoding();
				}
				return null;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009580 File Offset: 0x00007780
		public bool AllowUTF8
		{
			get
			{
				return (this.decodingFlags & DecodingFlags.Utf8) == DecodingFlags.Utf8;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000958D File Offset: 0x0000778D
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00009595 File Offset: 0x00007795
		internal Charset Charset
		{
			get
			{
				return this.charset;
			}
			set
			{
				this.charset = value;
			}
		}

		// Token: 0x0400011A RID: 282
		private static Charset defaultCharset;

		// Token: 0x0400011B RID: 283
		public static readonly DecodingOptions None = default(DecodingOptions);

		// Token: 0x0400011C RID: 284
		public static readonly DecodingOptions Default = new DecodingOptions(DecodingFlags.AllEncodings);

		// Token: 0x0400011D RID: 285
		private DecodingFlags decodingFlags;

		// Token: 0x0400011E RID: 286
		private Charset charset;
	}
}
