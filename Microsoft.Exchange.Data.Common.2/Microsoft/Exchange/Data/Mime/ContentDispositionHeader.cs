using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000026 RID: 38
	public class ContentDispositionHeader : ComplexHeader
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00007EE0 File Offset: 0x000060E0
		public ContentDispositionHeader() : base("Content-Disposition", HeaderId.ContentDisposition)
		{
			this.disp = "attachment";
			this.parsed = true;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007F01 File Offset: 0x00006101
		public ContentDispositionHeader(string value) : base("Content-Disposition", HeaderId.ContentDisposition)
		{
			this.Value = value;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007F17 File Offset: 0x00006117
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00007F30 File Offset: 0x00006130
		public sealed override string Value
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.disp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (ValueParser.ParseToken(value, 0, false) != value.Length)
				{
					throw new ArgumentException("Value should be a valid token", "value");
				}
				if (!this.parsed)
				{
					base.Parse();
				}
				if (value != this.disp)
				{
					base.SetRawValue(null, true);
					this.parsed = true;
					this.disp = Header.NormalizeString(value);
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007FA0 File Offset: 0x000061A0
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00007FD2 File Offset: 0x000061D2
		internal override byte[] RawValue
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				byte[] array = ByteString.StringToBytes(this.disp, false);
				if (array == null)
				{
					array = MimeString.EmptyByteArray;
				}
				return array;
			}
			set
			{
				base.RawValue = value;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007FDB File Offset: 0x000061DB
		internal override void RawValueAboutToChange()
		{
			this.Reset();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007FE4 File Offset: 0x000061E4
		public sealed override MimeNode Clone()
		{
			ContentDispositionHeader contentDispositionHeader = new ContentDispositionHeader();
			this.CopyTo(contentDispositionHeader);
			return contentDispositionHeader;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008000 File Offset: 0x00006200
		public sealed override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			ContentDispositionHeader contentDispositionHeader = destination as ContentDispositionHeader;
			if (contentDispositionHeader == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType, "destination");
			}
			base.CopyTo(destination);
			contentDispositionHeader.parsed = this.parsed;
			contentDispositionHeader.disp = this.disp;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008059 File Offset: 0x00006259
		public sealed override bool IsValueValid(string value)
		{
			return value != null && ValueParser.ParseToken(value, 0, false) == value.Length;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008073 File Offset: 0x00006273
		internal override long WriteValue(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			if (this.disp.Length == 0)
			{
				this.disp = "attachment";
			}
			return base.WriteValue(stream, encodingOptions, filter, ref currentLineLength, ref scratchBuffer);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000809C File Offset: 0x0000629C
		internal override void ParseValue(ValueParser parser, bool storeValue)
		{
			MimeStringList mimeStringList = default(MimeStringList);
			parser.ParseCFWS(false, ref mimeStringList, true);
			MimeString mimeString = parser.ParseToken();
			if (storeValue)
			{
				if (mimeString.Length == 0)
				{
					this.disp = string.Empty;
					return;
				}
				this.disp = Header.NormalizeString(mimeString.Data, mimeString.Offset, mimeString.Length, false);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000080FD File Offset: 0x000062FD
		internal override void AppendLine(MimeString line, bool markDirty)
		{
			if (this.parsed)
			{
				this.Reset();
			}
			base.AppendLine(line, markDirty);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008115 File Offset: 0x00006315
		private void Reset()
		{
			base.InternalRemoveAll();
			this.parsed = false;
			this.disp = null;
		}

		// Token: 0x040000E1 RID: 225
		internal const bool AllowUTF8Value = false;

		// Token: 0x040000E2 RID: 226
		private const string DefaultDisposition = "attachment";

		// Token: 0x040000E3 RID: 227
		private string disp;
	}
}
