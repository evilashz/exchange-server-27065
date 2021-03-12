using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200043D RID: 1085
	internal class BifInfo
	{
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000C5110 File Offset: 0x000C3310
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x000C513B File Offset: 0x000C333B
		public bool? SendTNEF
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.TNEF))
				{
					return new bool?(this.sendTNEF);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.sendTNEF = false;
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.TNEF);
					return;
				}
				this.sendTNEF = value.Value;
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.TNEF);
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000C516C File Offset: 0x000C336C
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x000C5197 File Offset: 0x000C3397
		public uint? SendInternetEncoding
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.SendInternetEncoding))
				{
					return new uint?(this.sendInternetEncoding);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.sendInternetEncoding = 0U;
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.SendInternetEncoding);
					return;
				}
				this.sendInternetEncoding = value.Value;
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.SendInternetEncoding);
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000C51C8 File Offset: 0x000C33C8
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x000C51F3 File Offset: 0x000C33F3
		public bool? FriendlyNames
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.FriendlyNames))
				{
					return new bool?(this.friendlyNames);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.friendlyNames = false;
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.FriendlyNames);
					return;
				}
				this.friendlyNames = value.Value;
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.FriendlyNames);
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000C5224 File Offset: 0x000C3424
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x000C524F File Offset: 0x000C344F
		public int? LineWrap
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.LineWrap))
				{
					return new int?(this.lineWrap);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.lineWrap = 0;
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.LineWrap);
					return;
				}
				this.lineWrap = value.Value;
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.LineWrap);
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000C527D File Offset: 0x000C347D
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000C5295 File Offset: 0x000C3495
		public string CharSet
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.CharSet))
				{
					return this.charSet;
				}
				return string.Empty;
			}
			set
			{
				this.charSet = value;
				if (value == null)
				{
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.CharSet);
					return;
				}
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.CharSet);
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000C52B4 File Offset: 0x000C34B4
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000C52E3 File Offset: 0x000C34E3
		public uint? SuppressFlag
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.AutoResponseSuppress))
				{
					return new uint?(this.suppressFlag);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.suppressFlag = 0U;
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.AutoResponseSuppress);
					return;
				}
				this.suppressFlag = value.Value;
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.AutoResponseSuppress);
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000C531C File Offset: 0x000C351C
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000C534B File Offset: 0x000C354B
		public BifSenderType? SenderType
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.SenderType))
				{
					return new BifSenderType?(this.senderType);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.senderType = BifSenderType.Originator;
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.SenderType);
					return;
				}
				this.senderType = value.Value;
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.SenderType);
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000C5381 File Offset: 0x000C3581
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x000C539C File Offset: 0x000C359C
		public string DlOwnerDN
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.DlOwnerDN))
				{
					return this.dlOwnerDN;
				}
				return string.Empty;
			}
			set
			{
				this.dlOwnerDN = value;
				if (value == null)
				{
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.DlOwnerDN);
					return;
				}
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.DlOwnerDN);
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000C53BF File Offset: 0x000C35BF
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x000C53DA File Offset: 0x000C35DA
		public string SenderDN
		{
			get
			{
				if (this.HasField(BifInfo.BitInfoFieldFlag.SenderDN))
				{
					return this.senderDN;
				}
				return string.Empty;
			}
			set
			{
				this.senderDN = value;
				if (value == null)
				{
					this.ClearFieldFlag(BifInfo.BitInfoFieldFlag.SenderDN);
					return;
				}
				this.SetFieldFlag(BifInfo.BitInfoFieldFlag.SenderDN);
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000C5400 File Offset: 0x000C3600
		public bool HasContentConversionOptions
		{
			get
			{
				return this.SendTNEF != null || this.SendInternetEncoding != null || this.FriendlyNames != null || this.LineWrap != null || !string.IsNullOrEmpty(this.CharSet);
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000C5460 File Offset: 0x000C3660
		public static BifInfo FromByteArray(byte[] bifInfoBlob)
		{
			if (bifInfoBlob.Length != 916)
			{
				throw new BifInfoException("invalid data size");
			}
			BifInfo bifInfo = new BifInfo();
			int num = 0;
			bifInfo.fieldFlags = (BifInfo.BitInfoFieldFlag)BitConverter.ToUInt32(bifInfoBlob, num);
			num += 4;
			bifInfo.sendTNEF = (BitConverter.ToInt32(bifInfoBlob, num) != 0);
			num += 4;
			bifInfo.sendInternetEncoding = BitConverter.ToUInt32(bifInfoBlob, num);
			num += 4;
			bifInfo.friendlyNames = (BitConverter.ToInt32(bifInfoBlob, num) != 0);
			num += 4;
			bifInfo.lineWrap = BitConverter.ToInt32(bifInfoBlob, num);
			num += 4;
			bifInfo.charSet = BifInfo.ConvertUTF8BytesToString(bifInfoBlob, num, 256);
			num += 256;
			bifInfo.suppressFlag = BitConverter.ToUInt32(bifInfoBlob, num);
			num += 4;
			bifInfo.senderType = (BifSenderType)BitConverter.ToUInt32(bifInfoBlob, num);
			num += 4;
			bifInfo.dlOwnerDN = BifInfo.ConvertUTF8BytesToString(bifInfoBlob, num, 316);
			num += 316;
			bifInfo.senderDN = BifInfo.ConvertUTF8BytesToString(bifInfoBlob, num, 316);
			return bifInfo;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000C5554 File Offset: 0x000C3754
		public static bool IsValidDN(string dn)
		{
			return Encoding.UTF8.GetByteCount(dn) < 316;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000C5568 File Offset: 0x000C3768
		public static bool IsValidCharSetName(string charset)
		{
			return Encoding.UTF8.GetByteCount(charset) < 256;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000C557C File Offset: 0x000C377C
		public byte[] ToByteArray()
		{
			byte[] array = new byte[916];
			int num = 0;
			num += ExBitConverter.Write((uint)this.fieldFlags, array, num);
			num += ExBitConverter.Write(Convert.ToInt32(this.sendTNEF), array, num);
			num += ExBitConverter.Write(this.sendInternetEncoding, array, num);
			num += ExBitConverter.Write(Convert.ToInt32(this.friendlyNames), array, num);
			num += ExBitConverter.Write(this.lineWrap, array, num);
			num += BifInfo.WriteStringAsBytes(this.charSet, array, num, 256);
			num += ExBitConverter.Write(this.suppressFlag, array, num);
			num += ExBitConverter.Write((uint)this.senderType, array, num);
			num += BifInfo.WriteStringAsBytes(this.dlOwnerDN, array, num, 316);
			num += BifInfo.WriteStringAsBytes(this.senderDN, array, num, 316);
			return array;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000C5650 File Offset: 0x000C3850
		public override bool Equals(object obj)
		{
			BifInfo bifInfo = obj as BifInfo;
			return bifInfo != null && (this.fieldFlags == bifInfo.fieldFlags && this.sendTNEF == bifInfo.sendTNEF && this.sendInternetEncoding == bifInfo.sendInternetEncoding && this.friendlyNames == bifInfo.friendlyNames && this.lineWrap == bifInfo.lineWrap && this.suppressFlag == bifInfo.suppressFlag && this.senderType == bifInfo.senderType && string.Equals(this.CharSet, bifInfo.CharSet) && string.Equals(this.DlOwnerDN, bifInfo.DlOwnerDN)) && string.Equals(this.SenderDN, bifInfo.SenderDN);
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000C5707 File Offset: 0x000C3907
		public override int GetHashCode()
		{
			return (int)(this.sendInternetEncoding ^ (uint)this.lineWrap ^ this.suppressFlag);
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000C5720 File Offset: 0x000C3920
		public string GetContentConversionOptionsString()
		{
			if (!this.HasContentConversionOptions)
			{
				return string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0};{1};{2};{3};{4}", new object[]
			{
				this.SendTNEF.ToString(),
				(this.SendInternetEncoding != null) ? this.SendInternetEncoding.Value.ToString("X8") : string.Empty,
				this.FriendlyNames.ToString(),
				(this.LineWrap != null) ? this.LineWrap.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
				this.CharSet
			});
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000C57F8 File Offset: 0x000C39F8
		public void CopyFromContentConversionOptionsString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return;
			}
			string[] array = str.Split(BifInfo.FieldSeparator);
			if (array.Length != 5)
			{
				return;
			}
			bool value;
			if (bool.TryParse(array[0], out value))
			{
				this.SendTNEF = new bool?(value);
			}
			uint value2;
			if (uint.TryParse(array[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value2))
			{
				this.SendInternetEncoding = new uint?(value2);
			}
			bool value3;
			if (bool.TryParse(array[2], out value3))
			{
				this.FriendlyNames = new bool?(value3);
			}
			int value4;
			if (int.TryParse(array[3], out value4))
			{
				this.LineWrap = new int?(value4);
			}
			Charset charset;
			if (Charset.TryGetCharset(array[4], out charset))
			{
				this.CharSet = array[4];
			}
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000C58A4 File Offset: 0x000C3AA4
		private static int WriteStringAsBytes(string str, byte[] targetArray, int offset, int bytesToUseInTargetArray)
		{
			int num;
			if (str == null)
			{
				num = 0;
			}
			else
			{
				num = Encoding.UTF8.GetBytes(str, 0, str.Length, targetArray, offset);
			}
			if (num >= bytesToUseInTargetArray)
			{
				throw new ArgumentException("Input string is too long.");
			}
			for (int i = num; i < bytesToUseInTargetArray; i++)
			{
				targetArray[offset + i] = 0;
			}
			return bytesToUseInTargetArray;
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000C58F0 File Offset: 0x000C3AF0
		private static string ConvertUTF8BytesToString(byte[] byteArray, int offset, int maximumLength)
		{
			int num = Array.IndexOf<byte>(byteArray, 0, offset, maximumLength);
			return Encoding.UTF8.GetString(byteArray, offset, num - offset);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000C5916 File Offset: 0x000C3B16
		private void ClearFieldFlag(BifInfo.BitInfoFieldFlag fieldFlag)
		{
			this.fieldFlags &= ~fieldFlag;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000C5927 File Offset: 0x000C3B27
		private void SetFieldFlag(BifInfo.BitInfoFieldFlag fieldFlag)
		{
			this.fieldFlags |= fieldFlag;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000C5937 File Offset: 0x000C3B37
		private bool HasField(BifInfo.BitInfoFieldFlag fieldFlag)
		{
			return (this.fieldFlags & fieldFlag) != (BifInfo.BitInfoFieldFlag)0U;
		}

		// Token: 0x04001908 RID: 6408
		private const int MaximumDNLength = 316;

		// Token: 0x04001909 RID: 6409
		private const int MaximumCharsetLength = 256;

		// Token: 0x0400190A RID: 6410
		private const int BifInfoSize = 916;

		// Token: 0x0400190B RID: 6411
		private static readonly char[] FieldSeparator = new char[]
		{
			';'
		};

		// Token: 0x0400190C RID: 6412
		private BifInfo.BitInfoFieldFlag fieldFlags;

		// Token: 0x0400190D RID: 6413
		private bool sendTNEF;

		// Token: 0x0400190E RID: 6414
		private uint sendInternetEncoding;

		// Token: 0x0400190F RID: 6415
		private bool friendlyNames;

		// Token: 0x04001910 RID: 6416
		private int lineWrap;

		// Token: 0x04001911 RID: 6417
		private string charSet;

		// Token: 0x04001912 RID: 6418
		private uint suppressFlag;

		// Token: 0x04001913 RID: 6419
		private BifSenderType senderType;

		// Token: 0x04001914 RID: 6420
		private string dlOwnerDN;

		// Token: 0x04001915 RID: 6421
		private string senderDN;

		// Token: 0x0200043E RID: 1086
		[Flags]
		private enum BitInfoFieldFlag : uint
		{
			// Token: 0x04001917 RID: 6423
			TNEF = 1U,
			// Token: 0x04001918 RID: 6424
			SendInternetEncoding = 2U,
			// Token: 0x04001919 RID: 6425
			FriendlyNames = 4U,
			// Token: 0x0400191A RID: 6426
			LineWrap = 8U,
			// Token: 0x0400191B RID: 6427
			CharSet = 16U,
			// Token: 0x0400191C RID: 6428
			SenderType = 4096U,
			// Token: 0x0400191D RID: 6429
			DlOwnerDN = 8192U,
			// Token: 0x0400191E RID: 6430
			SenderDN = 16384U,
			// Token: 0x0400191F RID: 6431
			AutoResponseSuppress = 32768U,
			// Token: 0x04001920 RID: 6432
			ExpansionDsn = 16777216U
		}
	}
}
