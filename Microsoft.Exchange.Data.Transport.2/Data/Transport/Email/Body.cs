using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C3 RID: 195
	public class Body
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0000A128 File Offset: 0x00008328
		internal Body(EmailMessage message)
		{
			this.Message = message;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000A137 File Offset: 0x00008337
		public BodyFormat BodyFormat
		{
			get
			{
				return this.Message.Body_GetBodyFormat();
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000A144 File Offset: 0x00008344
		public bool ConversionNeeded(int[] validCodepages)
		{
			return this.Message.Body_ConversionNeeded(validCodepages);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000A152 File Offset: 0x00008352
		public string CharsetName
		{
			get
			{
				if (this.BodyFormat == BodyFormat.None)
				{
					return Charset.DefaultMimeCharset.Name;
				}
				return this.Message.Body_GetCharsetName();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000A172 File Offset: 0x00008372
		public MimePart MimePart
		{
			get
			{
				if (this.BodyFormat == BodyFormat.None)
				{
					return null;
				}
				return this.Message.Body_GetMimePart();
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000A18C File Offset: 0x0000838C
		public Stream GetContentReadStream()
		{
			if (this.BodyFormat == BodyFormat.None)
			{
				return DataStorage.NewEmptyReadStream();
			}
			return this.Message.Body_GetContentReadStream();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public Stream GetContentReadStreamOrNull()
		{
			if (this.BodyFormat == BodyFormat.None)
			{
				return null;
			}
			return this.Message.Body_GetContentReadStream();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000A1CC File Offset: 0x000083CC
		public bool TryGetContentReadStream(out Stream stream)
		{
			if (this.BodyFormat == BodyFormat.None)
			{
				stream = DataStorage.NewEmptyReadStream();
				return true;
			}
			return this.Message.Body_TryGetContentReadStream(out stream);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public Stream GetContentWriteStream()
		{
			if (this.BodyFormat == BodyFormat.None)
			{
				throw new InvalidOperationException(EmailMessageStrings.CannotWriteBodyDoesNotExist);
			}
			return this.Message.Body_GetContentWriteStream(null);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000A21C File Offset: 0x0000841C
		public Stream GetContentWriteStream(string charsetName)
		{
			BodyFormat bodyFormat = this.BodyFormat;
			if (bodyFormat == BodyFormat.None)
			{
				throw new InvalidOperationException(EmailMessageStrings.CannotWriteBodyDoesNotExist);
			}
			if (BodyFormat.Rtf == bodyFormat)
			{
				throw new InvalidOperationException(EmailMessageStrings.NotSupportedForRtfBody);
			}
			Charset charset;
			Charset.TryGetCharset(this.CharsetName, out charset);
			Charset charset2 = Charset.GetCharset(charsetName);
			bool flag = charset == null || charset.CodePage != charset2.CodePage;
			if (flag)
			{
				charset2.GetEncoding();
			}
			return this.Message.Body_GetContentWriteStream(charset2);
		}

		// Token: 0x0400027D RID: 637
		internal EmailMessage Message;
	}
}
