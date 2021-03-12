using System;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000027 RID: 39
	public class ContentTypeHeader : ComplexHeader
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000812B File Offset: 0x0000632B
		public ContentTypeHeader() : base("Content-Type", HeaderId.ContentType)
		{
			this.value = "text/plain";
			this.type = "text";
			this.subType = "plain";
			this.parsed = true;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008162 File Offset: 0x00006362
		public ContentTypeHeader(string value) : base("Content-Type", HeaderId.ContentType)
		{
			this.Value = value;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008178 File Offset: 0x00006378
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00008190 File Offset: 0x00006390
		public string MediaType
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (ValueParser.ParseToken(value, 0, false) != value.Length)
				{
					throw new ArgumentException("Value should be valid MIME token", "value");
				}
				if (!this.parsed)
				{
					base.Parse();
				}
				if (value != this.type)
				{
					string text = this.subType;
					base.SetRawValue(null, true);
					this.parsed = true;
					this.type = Header.NormalizeString(value);
					this.subType = text;
					this.value = ContentTypeHeader.ComposeContentTypeValue(this.type, this.subType);
					if (this.type == "multipart")
					{
						this.EnsureBoundary();
					}
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008235 File Offset: 0x00006435
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000824C File Offset: 0x0000644C
		public string SubType
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.subType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (ValueParser.ParseToken(value, 0, false) != value.Length)
				{
					throw new ArgumentException("Value should be valid MIME token", "value");
				}
				if (!this.parsed)
				{
					base.Parse();
				}
				if (value != this.subType)
				{
					string text = this.type;
					base.SetRawValue(null, true);
					this.parsed = true;
					this.type = text;
					this.subType = Header.NormalizeString(value);
					this.value = ContentTypeHeader.ComposeContentTypeValue(this.type, this.subType);
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000082DE File Offset: 0x000064DE
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000082F4 File Offset: 0x000064F4
		public sealed override string Value
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int num = ValueParser.ParseToken(value, 0, false);
				if (num == 0 || num > value.Length - 2 || value[num] != '/' || ValueParser.ParseToken(value, num + 1, false) != value.Length - num - 1)
				{
					throw new ArgumentException("Value should be a valid content type in the form 'token/token'", "value");
				}
				if (!this.parsed)
				{
					base.Parse();
				}
				if (value != this.value)
				{
					base.SetRawValue(null, true);
					this.parsed = true;
					this.value = Header.NormalizeString(value);
					this.type = Header.NormalizeString(this.value, 0, num);
					this.subType = Header.NormalizeString(this.value, num + 1, this.value.Length - num - 1);
					if (this.type == "multipart")
					{
						this.EnsureBoundary();
					}
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000083D2 File Offset: 0x000065D2
		internal bool IsMultipart
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.type == "multipart";
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000083EF File Offset: 0x000065EF
		internal bool IsEmbeddedMessage
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.value == "message/rfc822";
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000840C File Offset: 0x0000660C
		internal bool IsAnyMessage
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				return this.type == "message";
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000842C File Offset: 0x0000662C
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000845E File Offset: 0x0000665E
		internal override byte[] RawValue
		{
			get
			{
				if (!this.parsed)
				{
					base.Parse();
				}
				byte[] array = ByteString.StringToBytes(this.value, false);
				if (array == null)
				{
					array = MimeString.EmptyByteArray;
				}
				return array;
			}
			set
			{
				base.RawValue = value;
				base.Parse();
				if (this.type == "multipart")
				{
					this.EnsureBoundary();
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008480 File Offset: 0x00006680
		public sealed override MimeNode Clone()
		{
			ContentTypeHeader contentTypeHeader = new ContentTypeHeader();
			this.CopyTo(contentTypeHeader);
			return contentTypeHeader;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000849C File Offset: 0x0000669C
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
			ContentTypeHeader contentTypeHeader = destination as ContentTypeHeader;
			if (contentTypeHeader == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType, "destination");
			}
			base.CopyTo(destination);
			contentTypeHeader.type = this.type;
			contentTypeHeader.subType = this.subType;
			contentTypeHeader.value = this.value;
			contentTypeHeader.parsed = this.parsed;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008510 File Offset: 0x00006710
		public sealed override bool IsValueValid(string value)
		{
			if (value == null)
			{
				return false;
			}
			int num = ValueParser.ParseToken(value, 0, false);
			return num != 0 && num <= value.Length - 2 && value[num] == '/' && ValueParser.ParseToken(value, num + 1, false) == value.Length - num - 1;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008560 File Offset: 0x00006760
		internal static byte[] CreateBoundary()
		{
			string text = Guid.NewGuid().ToString();
			byte[] array = new byte[text.Length + 2];
			array[0] = 95;
			ByteString.StringToBytes(text, array, 1, false);
			array[1 + text.Length] = 95;
			return array;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000085AB File Offset: 0x000067AB
		internal override void RawValueAboutToChange()
		{
			this.Reset();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000085B4 File Offset: 0x000067B4
		internal override void ParseValue(ValueParser parser, bool storeValue)
		{
			MimeStringList empty = MimeStringList.Empty;
			parser.ParseCFWS(false, ref empty, true);
			MimeString mimeString = parser.ParseToken();
			MimeString mimeString2 = MimeString.Empty;
			parser.ParseCFWS(false, ref empty, true);
			byte b = parser.ParseGet();
			if (b == 47)
			{
				parser.ParseCFWS(false, ref empty, true);
				mimeString2 = parser.ParseToken();
			}
			else if (b != 0)
			{
				parser.ParseUnget();
			}
			if (storeValue)
			{
				if (mimeString.Length == 0)
				{
					this.type = "text";
				}
				else
				{
					this.type = Header.NormalizeString(mimeString.Data, mimeString.Offset, mimeString.Length, false);
				}
				if (mimeString2.Length == 0)
				{
					if (this.type == "multipart")
					{
						this.subType = "mixed";
					}
					else if (this.type == "text")
					{
						this.subType = "plain";
					}
					else
					{
						this.type = "application";
						this.subType = "octet-stream";
					}
				}
				else
				{
					this.subType = Header.NormalizeString(mimeString2.Data, mimeString2.Offset, mimeString2.Length, false);
				}
				this.value = ContentTypeHeader.ComposeContentTypeValue(this.type, this.subType);
			}
			if (this.type == "multipart")
			{
				this.handleISO2022 = false;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000086FF File Offset: 0x000068FF
		internal override void AppendLine(MimeString line, bool markDirty)
		{
			if (this.parsed)
			{
				this.Reset();
			}
			base.AppendLine(line, markDirty);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008718 File Offset: 0x00006918
		private static string ComposeContentTypeValue(string type, string subType)
		{
			int num = type.Length + 1 + subType.Length;
			if (num >= 2 && num <= 32)
			{
				int num2 = 0;
				num2 = MimeData.HashValueAdd(num2, type);
				num2 = MimeData.HashValueAdd(num2, "/");
				num2 = MimeData.HashValueAdd(num2, subType);
				num2 = MimeData.HashValueFinish(num2);
				int num3 = MimeData.valueHashTable[num2];
				if (num3 > 0)
				{
					string text;
					for (;;)
					{
						text = MimeData.values[num3].value;
						if (text.Length == num && text.StartsWith(type, StringComparison.OrdinalIgnoreCase) && text[type.Length] == '/' && text.EndsWith(subType, StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						num3++;
						if ((int)MimeData.values[num3].hash != num2)
						{
							goto IL_A7;
						}
					}
					return text;
				}
			}
			IL_A7:
			return type + "/" + subType;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000087D8 File Offset: 0x000069D8
		private void EnsureBoundary()
		{
			if (base["boundary"] == null)
			{
				MimeParameter mimeParameter = new MimeParameter("boundary");
				base.InternalAppendChild(mimeParameter);
				mimeParameter.RawValue = ContentTypeHeader.CreateBoundary();
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008812 File Offset: 0x00006A12
		private void Reset()
		{
			base.InternalRemoveAll();
			this.parsed = false;
			this.value = null;
			this.type = null;
			this.subType = null;
		}

		// Token: 0x040000E4 RID: 228
		internal const bool AllowUTF8Value = false;

		// Token: 0x040000E5 RID: 229
		internal const bool AllowUTF8Boundary = false;

		// Token: 0x040000E6 RID: 230
		internal const bool AllowUTF8Charset = false;

		// Token: 0x040000E7 RID: 231
		private string value;

		// Token: 0x040000E8 RID: 232
		private string type;

		// Token: 0x040000E9 RID: 233
		private string subType;
	}
}
