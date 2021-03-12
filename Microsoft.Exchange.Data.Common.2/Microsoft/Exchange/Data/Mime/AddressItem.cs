using System;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000022 RID: 34
	public abstract class AddressItem : MimeNode
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000073FC File Offset: 0x000055FC
		internal AddressItem()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007404 File Offset: 0x00005604
		internal AddressItem(string displayName)
		{
			this.decodedDisplayName = displayName;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007413 File Offset: 0x00005613
		internal AddressItem(ref MimeStringList displayName)
		{
			this.displayNameFragments.TakeOver(ref displayName);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007428 File Offset: 0x00005628
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00007460 File Offset: 0x00005660
		public string DisplayName
		{
			get
			{
				DecodingResults decodingResults;
				if (this.decodedDisplayName == null && !this.TryGetDisplayName(base.GetHeaderDecodingOptions(), out decodingResults, out this.decodedDisplayName))
				{
					MimeCommon.ThrowDecodingFailedException(ref decodingResults);
				}
				return this.decodedDisplayName;
			}
			set
			{
				this.displayNameFragments.Reset();
				this.decodedDisplayName = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000747A File Offset: 0x0000567A
		public virtual bool RequiresSMTPUTF8
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009A RID: 154
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000747D File Offset: 0x0000567D
		internal string DecodedDisplayName
		{
			set
			{
				this.decodedDisplayName = value;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007488 File Offset: 0x00005688
		public override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			AddressItem addressItem = destination as AddressItem;
			if (addressItem == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
			addressItem.displayNameFragments = this.displayNameFragments.Clone();
			addressItem.decodedDisplayName = this.decodedDisplayName;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000074E4 File Offset: 0x000056E4
		public bool TryGetDisplayName(out string displayName)
		{
			DecodingResults decodingResults;
			return this.TryGetDisplayName(base.GetHeaderDecodingOptions(), out decodingResults, out displayName);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007500 File Offset: 0x00005700
		public bool TryGetDisplayName(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string displayName)
		{
			if (this.displayNameFragments.Count == 0)
			{
				displayName = this.decodedDisplayName;
				decodingResults = default(DecodingResults);
				return true;
			}
			if (decodingOptions.Charset == null)
			{
				decodingOptions.Charset = base.GetDefaultHeaderDecodingCharset(null, null);
			}
			return MimeCommon.TryDecodeValue(this.displayNameFragments, 4026531839U, decodingOptions, out decodingResults, out displayName);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007558 File Offset: 0x00005758
		private bool IsQuotingRequired(string displayName, bool allowUTF8)
		{
			MimeString mimeStr = new MimeString(displayName);
			return this.IsQuotingRequired(mimeStr, allowUTF8);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007578 File Offset: 0x00005778
		private bool IsQuotingRequired(MimeString mimeStr, bool allowUTF8)
		{
			AddressItem.WriteState writeState = AddressItem.WriteState.Begin;
			MimeString mimeString = new MimeString(AddressItem.WordBreakBytes, 0, AddressItem.WordBreakBytes.Length);
			int num;
			int num2;
			byte[] data = mimeStr.GetData(out num, out num2);
			while (num2 != 0)
			{
				switch (writeState)
				{
				case AddressItem.WriteState.Begin:
				{
					int num3 = 0;
					int num4 = MimeScan.FindEndOf(MimeScan.Token.Atom, data, num, num2, out num3, allowUTF8);
					if (num4 == 0)
					{
						if (num2 <= 3 || num != 0 || !mimeString.HasPrefixEq(data, 0, 3))
						{
							return true;
						}
						num += 3;
						num2 -= 3;
						writeState = AddressItem.WriteState.Begin;
					}
					else
					{
						num += num4;
						num2 -= num4;
						writeState = AddressItem.WriteState.Atom;
					}
					break;
				}
				case AddressItem.WriteState.Atom:
					if ((num2 < 2 || data[num] != 32) && (num2 < 1 || data[num] != 46))
					{
						return true;
					}
					num++;
					num2--;
					writeState = AddressItem.WriteState.Begin;
					break;
				}
			}
			return false;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007640 File Offset: 0x00005840
		internal bool IsQuotingRequired(MimeStringList displayNameFragments, bool allowUTF8)
		{
			for (int num = 0; num != displayNameFragments.Count; num++)
			{
				MimeString mimeStr = displayNameFragments[num];
				if ((mimeStr.Mask & 4026531839U) != 0U && this.IsQuotingRequired(mimeStr, allowUTF8))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007684 File Offset: 0x00005884
		internal string QuoteString(string inputString)
		{
			StringBuilder stringBuilder = new StringBuilder(inputString.Length + 2);
			stringBuilder.Append("\"");
			foreach (char c in inputString)
			{
				if (c < '\u0080' && MimeScan.IsEscapingRequired((byte)c))
				{
					stringBuilder.Append("\\");
				}
				stringBuilder.Append(c);
			}
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000076FB File Offset: 0x000058FB
		internal void ResetDisplayNameFragments()
		{
			this.displayNameFragments.Reset();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007708 File Offset: 0x00005908
		internal MimeStringList GetDisplayNameToWrite(EncodingOptions encodingOptions)
		{
			MimeStringList result = this.displayNameFragments;
			if (result.GetLength(4026531839U) == 0 && this.decodedDisplayName != null && this.decodedDisplayName.Length != 0)
			{
				string value;
				if ((byte)(encodingOptions.EncodingFlags & EncodingFlags.QuoteDisplayNameBeforeRfc2047Encoding) != 0 && this.IsQuotingRequired(this.decodedDisplayName, encodingOptions.AllowUTF8) && MimeCommon.IsEncodingRequired(this.decodedDisplayName, encodingOptions.AllowUTF8))
				{
					value = this.QuoteString(this.decodedDisplayName);
				}
				else
				{
					value = this.decodedDisplayName;
				}
				result = MimeCommon.EncodeValue(value, encodingOptions, ValueEncodingStyle.Phrase);
				this.displayNameFragments = result;
			}
			else if ((byte)(EncodingFlags.ForceReencode & encodingOptions.EncodingFlags) != 0)
			{
				result = MimeCommon.EncodeValue(this.DisplayName, encodingOptions, ValueEncodingStyle.Phrase);
			}
			return result;
		}

		// Token: 0x040000D8 RID: 216
		internal static readonly byte[] WordBreakBytes = ByteString.StringToBytes(" =?", true);

		// Token: 0x040000D9 RID: 217
		private MimeStringList displayNameFragments;

		// Token: 0x040000DA RID: 218
		private string decodedDisplayName;

		// Token: 0x02000023 RID: 35
		private enum WriteState
		{
			// Token: 0x040000DC RID: 220
			Begin,
			// Token: 0x040000DD RID: 221
			Atom
		}
	}
}
