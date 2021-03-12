using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000285 RID: 645
	internal struct RecipientEncoding : IEquatable<RecipientEncoding>, IComparable<RecipientEncoding>
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x0007214D File Offset: 0x0007034D
		public RecipientEncoding(bool? tnefEnabled, int? internetEncoding)
		{
			this = new RecipientEncoding(tnefEnabled, internetEncoding, null);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00072158 File Offset: 0x00070358
		public RecipientEncoding(bool? tnefEnabled, int? internetEncoding, string characterSet)
		{
			this.tnefEnabled = tnefEnabled;
			this.encoding = internetEncoding;
			this.displaySenderName = false;
			this.useSimpleDisplayName = false;
			this.byteEncoderTypeFor7BitCharsets = ByteEncoderTypeFor7BitCharsetsEnum.Undefined;
			this.preferredInternetCodePageForShiftJis = PreferredInternetCodePageForShiftJisEnum.Undefined;
			this.requiredCharsetCoverage = null;
			this.characterSet = ((characterSet == null) ? string.Empty : characterSet);
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x000721AD File Offset: 0x000703AD
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x000721B5 File Offset: 0x000703B5
		public ByteEncoderTypeFor7BitCharsetsEnum ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return this.byteEncoderTypeFor7BitCharsets;
			}
			set
			{
				this.byteEncoderTypeFor7BitCharsets = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x000721BE File Offset: 0x000703BE
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x000721C6 File Offset: 0x000703C6
		public string CharacterSet
		{
			get
			{
				return this.characterSet;
			}
			set
			{
				this.characterSet = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000721CF File Offset: 0x000703CF
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x000721D7 File Offset: 0x000703D7
		public bool? TNEFEnabled
		{
			get
			{
				return this.tnefEnabled;
			}
			set
			{
				this.tnefEnabled = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000721E0 File Offset: 0x000703E0
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x000721E8 File Offset: 0x000703E8
		public int? InternetEncoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x000721F1 File Offset: 0x000703F1
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x000721F9 File Offset: 0x000703F9
		public bool DisplaySenderName
		{
			get
			{
				return this.displaySenderName;
			}
			set
			{
				this.displaySenderName = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00072202 File Offset: 0x00070402
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x0007220A File Offset: 0x0007040A
		public PreferredInternetCodePageForShiftJisEnum PreferredInternetCodePageForShiftJis
		{
			get
			{
				return this.preferredInternetCodePageForShiftJis;
			}
			set
			{
				this.preferredInternetCodePageForShiftJis = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00072213 File Offset: 0x00070413
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0007221B File Offset: 0x0007041B
		public int? RequiredCharsetCoverage
		{
			get
			{
				return this.requiredCharsetCoverage;
			}
			set
			{
				this.requiredCharsetCoverage = value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00072224 File Offset: 0x00070424
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0007222C File Offset: 0x0007042C
		public bool UseSimpleDisplayName
		{
			get
			{
				return this.useSimpleDisplayName;
			}
			set
			{
				this.useSimpleDisplayName = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x00072235 File Offset: 0x00070435
		public bool IsMimeEncoding
		{
			get
			{
				return this.encoding != null && (this.encoding.Value & 262144) != 0;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0007225D File Offset: 0x0007045D
		public InternetMessageFormat InternetMessageFormat
		{
			get
			{
				if (this.encoding == null)
				{
					return InternetMessageFormat.Mime;
				}
				if ((this.encoding.Value & 262144) != 0)
				{
					return InternetMessageFormat.Mime;
				}
				if ((this.encoding.Value & 2228224) != 0)
				{
					return InternetMessageFormat.Uuencode;
				}
				return InternetMessageFormat.Binhex;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00072299 File Offset: 0x00070499
		public InternetTextFormat InternetTextFormat
		{
			get
			{
				if (this.encoding != null)
				{
					if (this.encoding.Value == 917504)
					{
						return InternetTextFormat.HtmlOnly;
					}
					if (this.encoding.Value == 1441792)
					{
						return InternetTextFormat.HtmlAndTextAlternative;
					}
				}
				return InternetTextFormat.TextOnly;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000722D4 File Offset: 0x000704D4
		public bool Equals(RecipientEncoding other)
		{
			return this.tnefEnabled.Equals(other.tnefEnabled) && this.displaySenderName == other.displaySenderName && this.useSimpleDisplayName == other.useSimpleDisplayName && this.encoding.Equals(other.encoding) && string.Equals(this.characterSet, other.characterSet) && this.requiredCharsetCoverage == other.requiredCharsetCoverage && this.preferredInternetCodePageForShiftJis == other.preferredInternetCodePageForShiftJis && this.byteEncoderTypeFor7BitCharsets == other.byteEncoderTypeFor7BitCharsets;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000723B0 File Offset: 0x000705B0
		public int CompareTo(RecipientEncoding other)
		{
			int num = Nullable.Compare<bool>(this.tnefEnabled, other.tnefEnabled);
			if (num != 0)
			{
				return num;
			}
			num = Nullable.Compare<int>(this.encoding, other.encoding);
			if (num != 0)
			{
				return num;
			}
			if (this.displaySenderName != other.displaySenderName)
			{
				if (!this.displaySenderName)
				{
					return -1;
				}
				return 1;
			}
			else if (this.useSimpleDisplayName != other.useSimpleDisplayName)
			{
				if (!this.useSimpleDisplayName)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				num = Nullable.Compare<int>(this.requiredCharsetCoverage, other.requiredCharsetCoverage);
				if (num != 0)
				{
					return num;
				}
				num = this.byteEncoderTypeFor7BitCharsets.CompareTo(other.byteEncoderTypeFor7BitCharsets);
				if (num != 0)
				{
					return num;
				}
				num = this.preferredInternetCodePageForShiftJis.CompareTo(other.preferredInternetCodePageForShiftJis);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(this.characterSet, other.characterSet);
			}
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00072491 File Offset: 0x00070691
		public override bool Equals(object obj)
		{
			return obj != null && obj is RecipientEncoding && this.Equals((RecipientEncoding)obj);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000724AC File Offset: 0x000706AC
		public override int GetHashCode()
		{
			int num = this.encoding ?? 0;
			if (this.tnefEnabled != null)
			{
				num <<= 1;
				num |= (this.tnefEnabled.Value ? 1 : 0);
			}
			num <<= 1;
			num |= (this.displaySenderName ? 1 : 0);
			num <<= 1;
			num |= (this.useSimpleDisplayName ? 1 : 0);
			if (this.characterSet != null)
			{
				num = (num << 1 ^ this.characterSet.GetHashCode());
			}
			num = (num << 1 ^ this.byteEncoderTypeFor7BitCharsets.GetHashCode());
			num = (num << 1 ^ this.preferredInternetCodePageForShiftJis.GetHashCode());
			if (this.requiredCharsetCoverage != null)
			{
				num = (num << 1 ^ this.requiredCharsetCoverage.GetHashCode());
			}
			return num;
		}

		// Token: 0x04000D1C RID: 3356
		private string characterSet;

		// Token: 0x04000D1D RID: 3357
		private ByteEncoderTypeFor7BitCharsetsEnum byteEncoderTypeFor7BitCharsets;

		// Token: 0x04000D1E RID: 3358
		private bool? tnefEnabled;

		// Token: 0x04000D1F RID: 3359
		private int? encoding;

		// Token: 0x04000D20 RID: 3360
		private bool displaySenderName;

		// Token: 0x04000D21 RID: 3361
		private PreferredInternetCodePageForShiftJisEnum preferredInternetCodePageForShiftJis;

		// Token: 0x04000D22 RID: 3362
		private int? requiredCharsetCoverage;

		// Token: 0x04000D23 RID: 3363
		private bool useSimpleDisplayName;
	}
}
