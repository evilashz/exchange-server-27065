using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000021 RID: 33
	public class AddressHeader : Header, IEnumerable<AddressItem>, IEnumerable
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00006E64 File Offset: 0x00005064
		public AddressHeader(string name) : this(name, Header.GetHeaderId(name, true))
		{
			Type type = Header.TypeFromHeaderId(base.HeaderId);
			if (base.HeaderId != HeaderId.Unknown && type != typeof(AddressHeader))
			{
				throw new ArgumentException(Strings.NameNotValidForThisHeaderType(name, "AddressHeader", type.Name));
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006EBC File Offset: 0x000050BC
		internal AddressHeader(string name, HeaderId headerId) : base(name, headerId)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006EC6 File Offset: 0x000050C6
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006EC9 File Offset: 0x000050C9
		public sealed override string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException(Strings.UnicodeMimeHeaderAddressNotSupported);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006ED8 File Offset: 0x000050D8
		public sealed override bool RequiresSMTPUTF8
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				for (MimeNode mimeNode = base.FirstChild; mimeNode != null; mimeNode = mimeNode.NextSibling)
				{
					AddressItem addressItem = mimeNode as AddressItem;
					if (addressItem != null && addressItem.RequiresSMTPUTF8)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006F1B File Offset: 0x0000511B
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00006F1E File Offset: 0x0000511E
		internal override byte[] RawValue
		{
			get
			{
				return null;
			}
			set
			{
				base.RawValue = value;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006F27 File Offset: 0x00005127
		internal override void RawValueAboutToChange()
		{
			this.parsed = true;
			base.InternalRemoveAll();
			if (this.parser != null)
			{
				this.parser.Reset();
			}
			this.parsed = false;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006F50 File Offset: 0x00005150
		public override bool TryGetValue(out string value)
		{
			value = null;
			return false;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006F56 File Offset: 0x00005156
		public new MimeNode.Enumerator<AddressItem> GetEnumerator()
		{
			return new MimeNode.Enumerator<AddressItem>(this);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006F5E File Offset: 0x0000515E
		IEnumerator<AddressItem> IEnumerable<AddressItem>.GetEnumerator()
		{
			return new MimeNode.Enumerator<AddressItem>(this);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006F6B File Offset: 0x0000516B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MimeNode.Enumerator<AddressItem>(this);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006F78 File Offset: 0x00005178
		public sealed override MimeNode Clone()
		{
			AddressHeader addressHeader = new AddressHeader(base.Name, base.HeaderId);
			this.CopyTo(addressHeader);
			return addressHeader;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006FA0 File Offset: 0x000051A0
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
			AddressHeader addressHeader = destination as AddressHeader;
			if (addressHeader == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
			addressHeader.parsed = this.parsed;
			addressHeader.parser = ((this.parser == null) ? null : new MimeAddressParser(addressHeader.Lines, this.parser));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000700C File Offset: 0x0000520C
		public static AddressHeader Parse(string name, string value, AddressParserFlags flags)
		{
			AddressHeader addressHeader = new AddressHeader(name);
			if (!string.IsNullOrEmpty(value))
			{
				byte[] array = ByteString.StringToBytes(value, true);
				addressHeader.parser = new MimeAddressParser();
				addressHeader.parser.Initialize(new MimeStringList(array, 0, array.Length), AddressParserFlags.None != (flags & AddressParserFlags.IgnoreComments), AddressParserFlags.None != (flags & AddressParserFlags.AllowSquareBrackets), true);
				addressHeader.staticParsing = true;
				addressHeader.Parse();
			}
			return addressHeader;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007071 File Offset: 0x00005271
		public sealed override bool IsValueValid(string value)
		{
			return false;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007074 File Offset: 0x00005274
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			long num = base.WriteName(stream, ref scratchBuffer);
			currentLineLength.IncrementBy((int)num);
			if (!base.IsDirty && base.RawLength != 0)
			{
				if (base.IsProtected)
				{
					num += Header.WriteLines(base.Lines, stream);
					currentLineLength.SetAs(0);
					return num;
				}
				if (base.InternalLastChild == null)
				{
					bool flag = false;
					if (!base.IsHeaderLineTooLong(num, out flag))
					{
						num += Header.WriteLines(base.Lines, stream);
						currentLineLength.SetAs(0);
						return num;
					}
				}
			}
			if (!this.parsed)
			{
				this.Parse();
			}
			MimeNode mimeNode = base.FirstChild;
			int num2 = 0;
			while (mimeNode != null)
			{
				if (1 < ++num2)
				{
					stream.Write(MimeString.Comma, 0, MimeString.Comma.Length);
					num += (long)MimeString.Comma.Length;
					currentLineLength.IncrementBy(MimeString.Comma.Length);
				}
				num += mimeNode.WriteTo(stream, encodingOptions, filter, ref currentLineLength, ref scratchBuffer);
				mimeNode = mimeNode.NextSibling;
			}
			return num + Header.WriteLineEnd(stream, ref currentLineLength);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000716B File Offset: 0x0000536B
		internal override void RemoveAllUnparsed()
		{
			this.parsed = true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007174 File Offset: 0x00005374
		internal override MimeNode ParseNextChild()
		{
			if (this.parsed)
			{
				return null;
			}
			MimeNode internalLastChild = base.InternalLastChild;
			MimeNode mimeNode;
			if (internalLastChild is MimeGroup)
			{
				while (internalLastChild.ParseNextChild() != null)
				{
				}
				mimeNode = internalLastChild.InternalNextSibling;
			}
			else
			{
				mimeNode = this.ParseNextMailBox(false);
			}
			this.parsed = (mimeNode == null);
			return mimeNode;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000071C0 File Offset: 0x000053C0
		internal override void CheckChildrenLimit(int countLimit, int bytesLimit)
		{
			if (this.parser == null)
			{
				this.parser = new MimeAddressParser();
			}
			if (!this.parser.Initialized)
			{
				DecodingOptions headerDecodingOptions = base.GetHeaderDecodingOptions();
				this.parser.Initialize(base.Lines, false, false, headerDecodingOptions.AllowUTF8);
			}
			int i;
			for (i = 0; i <= countLimit; i++)
			{
				MimeStringList mimeStringList = default(MimeStringList);
				MimeStringList mimeStringList2 = default(MimeStringList);
				if (AddressParserResult.End == this.parser.ParseNextMailbox(ref mimeStringList, ref mimeStringList2))
				{
					this.parser.Reset();
					return;
				}
				if (mimeStringList.Length > bytesLimit)
				{
					throw new MimeException(Strings.TooManyTextValueBytes(mimeStringList.Length, bytesLimit));
				}
			}
			throw new MimeException(Strings.TooManyAddressItems(i, countLimit));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007271 File Offset: 0x00005471
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			if (!(newChild is MimeRecipient) && !(newChild is MimeGroup))
			{
				throw new ArgumentException(Strings.NewChildNotRecipientOrGroup, "newChild");
			}
			return refChild;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007294 File Offset: 0x00005494
		internal override void AppendLine(MimeString line, bool markDirty)
		{
			base.AppendLine(line, markDirty);
			this.parsed = false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000072A8 File Offset: 0x000054A8
		internal MimeNode ParseNextMailBox(bool fromGroup)
		{
			if (this.parsed)
			{
				return null;
			}
			DecodingOptions headerDecodingOptions = base.GetHeaderDecodingOptions();
			if (this.parser == null)
			{
				this.parser = new MimeAddressParser();
			}
			if (!this.parser.Initialized)
			{
				this.parser.Initialize(base.Lines, false, false, headerDecodingOptions.AllowUTF8);
			}
			MimeStringList displayNameFragments = default(MimeStringList);
			MimeStringList mimeStringList = default(MimeStringList);
			AddressParserResult addressParserResult = this.parser.ParseNextMailbox(ref displayNameFragments, ref mimeStringList);
			switch (addressParserResult)
			{
			case AddressParserResult.Mailbox:
			case AddressParserResult.GroupInProgress:
			{
				MimeRecipient mimeRecipient = new MimeRecipient(ref mimeStringList, ref displayNameFragments);
				if (this.staticParsing)
				{
					MimeRecipient.ConvertDisplayNameBack(mimeRecipient, displayNameFragments, headerDecodingOptions.AllowUTF8);
				}
				if (addressParserResult == AddressParserResult.GroupInProgress)
				{
					MimeGroup mimeGroup = base.InternalLastChild as MimeGroup;
					mimeGroup.InternalInsertAfter(mimeRecipient, mimeGroup.InternalLastChild);
					return mimeRecipient;
				}
				base.InternalInsertAfter(mimeRecipient, base.InternalLastChild);
				if (!fromGroup)
				{
					return mimeRecipient;
				}
				return null;
			}
			case AddressParserResult.GroupStart:
			{
				MimeGroup mimeGroup = new MimeGroup(ref displayNameFragments);
				if (this.staticParsing)
				{
					MimeRecipient.ConvertDisplayNameBack(mimeGroup, displayNameFragments, headerDecodingOptions.AllowUTF8);
				}
				base.InternalInsertAfter(mimeGroup, base.InternalLastChild);
				return mimeGroup;
			}
			case AddressParserResult.End:
				return null;
			default:
				return null;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000073D2 File Offset: 0x000055D2
		private void Parse()
		{
			while (!this.parsed)
			{
				this.ParseNextChild();
			}
			if (this.staticParsing)
			{
				this.staticParsing = false;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000073F4 File Offset: 0x000055F4
		internal override void ForceParse()
		{
			this.Parse();
		}

		// Token: 0x040000D4 RID: 212
		internal const bool AllowUTF8Value = true;

		// Token: 0x040000D5 RID: 213
		private bool staticParsing;

		// Token: 0x040000D6 RID: 214
		private bool parsed;

		// Token: 0x040000D7 RID: 215
		private MimeAddressParser parser;
	}
}
