using System;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200002C RID: 44
	public class EncodingOptions
	{
		// Token: 0x060001FF RID: 511 RVA: 0x000093DA File Offset: 0x000075DA
		public EncodingOptions(string charsetName, string cultureName, EncodingFlags encodingFlags)
		{
			this.cultureName = cultureName;
			this.encodingFlags = encodingFlags;
			this.charset = ((charsetName == null) ? null : Charset.GetCharset(charsetName));
			if (this.charset != null)
			{
				this.charset.GetEncoding();
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009416 File Offset: 0x00007616
		internal EncodingOptions(Charset charset)
		{
			this.charset = charset;
			this.cultureName = null;
			this.encodingFlags = EncodingFlags.None;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009433 File Offset: 0x00007633
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000944A File Offset: 0x0000764A
		public string CultureName
		{
			get
			{
				return this.cultureName;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00009452 File Offset: 0x00007652
		public EncodingFlags EncodingFlags
		{
			get
			{
				return this.encodingFlags;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000945A File Offset: 0x0000765A
		public bool AllowUTF8
		{
			get
			{
				return (byte)(this.encodingFlags & EncodingFlags.AllowUTF8) == 8;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009468 File Offset: 0x00007668
		internal Charset GetEncodingCharset()
		{
			Charset defaultCharset = this.charset;
			if (defaultCharset == null)
			{
				defaultCharset = EncodingOptions.DefaultCharset;
			}
			return defaultCharset;
		}

		// Token: 0x0400010C RID: 268
		internal static readonly Charset DefaultCharset = DecodingOptions.DefaultCharset;

		// Token: 0x0400010D RID: 269
		private EncodingFlags encodingFlags;

		// Token: 0x0400010E RID: 270
		private Charset charset;

		// Token: 0x0400010F RID: 271
		private string cultureName;
	}
}
