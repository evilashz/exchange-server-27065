using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000082 RID: 130
	public class TextHeader : Header
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x0001CF44 File Offset: 0x0001B144
		public TextHeader(string name, string value) : this(name, Header.GetHeaderId(name, true))
		{
			Type type = Header.TypeFromHeaderId(base.HeaderId);
			if (base.HeaderId != HeaderId.Unknown && type != typeof(TextHeader) && type != typeof(AsciiTextHeader))
			{
				throw new ArgumentException(Strings.NameNotValidForThisHeaderType(name, "TextHeader", type.Name));
			}
			this.Value = value;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001CFB5 File Offset: 0x0001B1B5
		internal TextHeader(string name, HeaderId headerId) : base(name, headerId)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0001D001 File Offset: 0x0001B201
		public sealed override string Value
		{
			get
			{
				if (this.decodedValue == null)
				{
					DecodingResults decodingResults;
					string text = this.GetDecodedValue(base.GetHeaderDecodingOptions(), out decodingResults);
					if (decodingResults.DecodingFailed)
					{
						MimeCommon.ThrowDecodingFailedException(ref decodingResults);
					}
					this.decodedValue = text;
				}
				return this.decodedValue;
			}
			set
			{
				base.SetRawValue(null, true);
				this.decodedValue = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001D014 File Offset: 0x0001B214
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001D076 File Offset: 0x0001B276
		internal override byte[] RawValue
		{
			get
			{
				MimeStringList mimeStringList;
				if (base.RawLength == 0 && this.decodedValue != null && this.decodedValue.Length != 0)
				{
					mimeStringList = this.GetEncodedValue(base.GetDocumentEncodingOptions(), ValueEncodingStyle.Normal);
				}
				else
				{
					mimeStringList = base.Lines;
				}
				if (mimeStringList.Length == 0)
				{
					return MimeString.EmptyByteArray;
				}
				byte[] array = mimeStringList.GetSz();
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

		// Token: 0x0600054B RID: 1355 RVA: 0x0001D07F File Offset: 0x0001B27F
		internal override void ForceParse()
		{
			string value = this.Value;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001D088 File Offset: 0x0001B288
		internal override void RawValueAboutToChange()
		{
			this.decodedValue = null;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001D094 File Offset: 0x0001B294
		public sealed override MimeNode Clone()
		{
			TextHeader textHeader = new TextHeader(base.Name, base.HeaderId);
			this.CopyTo(textHeader);
			return textHeader;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001D0BC File Offset: 0x0001B2BC
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
			TextHeader textHeader = destination as TextHeader;
			if (textHeader == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
			textHeader.decodedValue = this.decodedValue;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001D104 File Offset: 0x0001B304
		public override bool TryGetValue(out string value)
		{
			DecodingResults decodingResults;
			return this.TryGetValue(base.GetHeaderDecodingOptions(), out decodingResults, out value);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001D120 File Offset: 0x0001B320
		public bool TryGetValue(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string value)
		{
			value = this.GetDecodedValue(decodingOptions, out decodingResults);
			if (decodingResults.DecodingFailed)
			{
				value = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001D13C File Offset: 0x0001B33C
		internal string GetDecodedValue(DecodingOptions decodingOptions, out DecodingResults decodingResults)
		{
			string result = null;
			if (base.Lines.Length == 0)
			{
				result = ((this.decodedValue != null) ? this.decodedValue : string.Empty);
				decodingResults = default(DecodingResults);
				return result;
			}
			if (decodingOptions.Charset == null)
			{
				decodingOptions.Charset = base.GetDefaultHeaderDecodingCharset(null, null);
			}
			if (!MimeCommon.TryDecodeValue(base.Lines, 4026531840U, decodingOptions, out decodingResults, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001D1AB File Offset: 0x0001B3AB
		internal MimeStringList GetEncodedValue(EncodingOptions encodingOptions, ValueEncodingStyle encodingStyle)
		{
			if (string.IsNullOrEmpty(this.decodedValue))
			{
				return base.Lines;
			}
			return MimeCommon.EncodeValue(this.decodedValue, encodingOptions, encodingStyle);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001D1CE File Offset: 0x0001B3CE
		public sealed override bool IsValueValid(string value)
		{
			return true;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			long num = base.WriteName(stream, ref scratchBuffer);
			currentLineLength.IncrementBy((int)num);
			MimeStringList mimeStringList;
			if (base.RawLength == 0 && this.decodedValue != null && this.decodedValue.Length != 0)
			{
				mimeStringList = this.GetEncodedValue(encodingOptions, ValueEncodingStyle.Normal);
			}
			else if ((byte)(EncodingFlags.ForceReencode & encodingOptions.EncodingFlags) != 0)
			{
				this.ForceParse();
				mimeStringList = this.GetEncodedValue(encodingOptions, ValueEncodingStyle.Normal);
			}
			else
			{
				bool flag = false;
				if (!base.IsDirty && base.RawLength != 0)
				{
					if (base.IsProtected)
					{
						num += Header.WriteLines(base.Lines, stream);
						currentLineLength.SetAs(0);
						return num;
					}
					if (!base.IsHeaderLineTooLong(num, out flag))
					{
						num += Header.WriteLines(base.Lines, stream);
						currentLineLength.SetAs(0);
						return num;
					}
				}
				mimeStringList = base.Lines;
				if (flag)
				{
					mimeStringList = Header.MergeLines(mimeStringList);
				}
			}
			num += Header.QuoteAndFold(stream, mimeStringList, 4026531840U, false, mimeStringList.Length > 0, encodingOptions.AllowUTF8, 0, ref currentLineLength, ref scratchBuffer);
			return num + Header.WriteLineEnd(stream, ref currentLineLength);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001D2DA File Offset: 0x0001B4DA
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			throw new NotSupportedException(Strings.ChildrenCannotBeAddedToTextHeader);
		}

		// Token: 0x040003D3 RID: 979
		private string decodedValue;
	}
}
