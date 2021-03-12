using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000081 RID: 129
	public class MimeRecipient : AddressItem
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x0001CB89 File Offset: 0x0001AD89
		public MimeRecipient()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001CB91 File Offset: 0x0001AD91
		public MimeRecipient(string displayName, string email) : base(displayName)
		{
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			this.emailAddressFragments.Append(new MimeString(email));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001CBB9 File Offset: 0x0001ADB9
		internal MimeRecipient(ref MimeStringList address, ref MimeStringList displayName) : base(ref displayName)
		{
			this.emailAddressFragments.TakeOverAppend(ref address);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		public string Email
		{
			get
			{
				byte[] sz = this.emailAddressFragments.GetSz();
				if (sz != null)
				{
					return ByteString.BytesToString(sz, true);
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!MimeAddressParser.IsWellFormedAddress(value, true))
				{
					throw new ArgumentException("Address string must be well-formed", "value");
				}
				this.emailAddressFragments.Reset();
				this.emailAddressFragments.Append(new MimeString(value));
				this.SetDirty();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001CC52 File Offset: 0x0001AE52
		public sealed override bool RequiresSMTPUTF8
		{
			get
			{
				return !MimeString.IsPureASCII(this.emailAddressFragments);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001CC64 File Offset: 0x0001AE64
		public static MimeRecipient Parse(string address, AddressParserFlags flags)
		{
			MimeRecipient mimeRecipient = new MimeRecipient();
			if (!string.IsNullOrEmpty(address))
			{
				byte[] array = ByteString.StringToBytes(address, true);
				MimeAddressParser mimeAddressParser = new MimeAddressParser();
				mimeAddressParser.Initialize(new MimeStringList(array, 0, array.Length), AddressParserFlags.None != (flags & AddressParserFlags.IgnoreComments), AddressParserFlags.None != (flags & AddressParserFlags.AllowSquareBrackets), true);
				MimeStringList displayNameFragments = default(MimeStringList);
				mimeAddressParser.ParseNextMailbox(ref displayNameFragments, ref mimeRecipient.emailAddressFragments);
				MimeRecipient.ConvertDisplayNameBack(mimeRecipient, displayNameFragments, true);
			}
			return mimeRecipient;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		public static bool IsEmailValid(string email)
		{
			return MimeRecipient.IsEmailValid(email, false);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001CCD9 File Offset: 0x0001AED9
		public static bool IsEmailValid(string email, bool allowUTF8)
		{
			return MimeAddressParser.IsWellFormedAddress(email, allowUTF8);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		public sealed override MimeNode Clone()
		{
			MimeRecipient mimeRecipient = new MimeRecipient();
			this.CopyTo(mimeRecipient);
			return mimeRecipient;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001CD00 File Offset: 0x0001AF00
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
			MimeRecipient mimeRecipient = destination as MimeRecipient;
			if (mimeRecipient == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
			mimeRecipient.emailAddressFragments = this.emailAddressFragments.Clone();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001CD50 File Offset: 0x0001AF50
		internal static void ConvertDisplayNameBack(AddressItem addressItem, MimeStringList displayNameFragments, bool allowUTF8)
		{
			byte[] sz = displayNameFragments.GetSz(4026531839U);
			if (sz == null)
			{
				addressItem.DecodedDisplayName = null;
				return;
			}
			string decodedDisplayName = ByteString.BytesToString(sz, allowUTF8);
			addressItem.DecodedDisplayName = decodedDisplayName;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001CD84 File Offset: 0x0001AF84
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			throw new NotSupportedException(Strings.RecipientsCannotHaveChildNodes);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001CD90 File Offset: 0x0001AF90
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			MimeStringList displayNameToWrite = base.GetDisplayNameToWrite(encodingOptions);
			long num = 0L;
			int num2 = 0;
			if (base.NextSibling != null)
			{
				num2++;
			}
			else if (base.Parent is MimeGroup)
			{
				num2++;
				if (base.Parent.NextSibling != null)
				{
					num2++;
				}
			}
			byte[] sz = this.emailAddressFragments.GetSz();
			int num3 = ByteString.BytesToCharCount(sz, encodingOptions.AllowUTF8);
			if (displayNameToWrite.GetLength(4026531839U) != 0)
			{
				num += Header.QuoteAndFold(stream, displayNameToWrite, 4026531839U, base.IsQuotingRequired(displayNameToWrite, encodingOptions.AllowUTF8), true, encodingOptions.AllowUTF8, (num3 == 0) ? num2 : 0, ref currentLineLength, ref scratchBuffer);
			}
			if (num3 != 0)
			{
				int num4 = (1 < currentLineLength.InChars) ? 1 : 0;
				if (1 < currentLineLength.InChars)
				{
					if (currentLineLength.InChars + num3 + 2 + num2 + num4 > 78)
					{
						num += Header.WriteLineEnd(stream, ref currentLineLength);
						stream.Write(Header.LineStartWhitespace, 0, Header.LineStartWhitespace.Length);
						num += (long)Header.LineStartWhitespace.Length;
						currentLineLength.IncrementBy(Header.LineStartWhitespace.Length);
					}
					else
					{
						stream.Write(MimeString.Space, 0, MimeString.Space.Length);
						num += (long)MimeString.Space.Length;
						currentLineLength.IncrementBy(MimeString.Space.Length);
					}
				}
				stream.Write(MimeString.LessThan, 0, MimeString.LessThan.Length);
				num += (long)MimeString.LessThan.Length;
				currentLineLength.IncrementBy(MimeString.LessThan.Length);
				stream.Write(sz, 0, sz.Length);
				num += (long)sz.Length;
				currentLineLength.IncrementBy(num3, sz.Length);
				stream.Write(MimeString.GreaterThan, 0, MimeString.GreaterThan.Length);
				num += (long)MimeString.GreaterThan.Length;
				currentLineLength.IncrementBy(MimeString.GreaterThan.Length);
			}
			return num;
		}

		// Token: 0x040003D2 RID: 978
		private MimeStringList emailAddressFragments;
	}
}
