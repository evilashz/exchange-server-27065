using System;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	internal sealed class LegacyDN : IEquatable<LegacyDN>
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x000419F0 File Offset: 0x0003FBF0
		public LegacyDN(LegacyDN parentLegacyDN, string rdnPrefix, string legacyCommonName)
		{
			if (parentLegacyDN == null)
			{
				throw new ArgumentNullException("parentLegacyDN");
			}
			if (string.IsNullOrEmpty(rdnPrefix))
			{
				throw new ArgumentNullException("rdnPrefix");
			}
			if (string.IsNullOrEmpty(legacyCommonName))
			{
				throw new ArgumentNullException("legacyCommonName");
			}
			if (!LegacyDN.IsValidRdnPrefix(rdnPrefix))
			{
				throw new FormatException(DirectoryStrings.ErrorInvalidLegacyRdnPrefix(rdnPrefix));
			}
			if (!LegacyDN.IsValidLegacyCommonName(legacyCommonName))
			{
				throw new FormatException(DirectoryStrings.ErrorInvalidLegacyCommonName(legacyCommonName));
			}
			this.legacyDNString = string.Concat(new object[]
			{
				parentLegacyDN,
				"/",
				rdnPrefix,
				"=",
				legacyCommonName
			});
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00041A98 File Offset: 0x0003FC98
		private LegacyDN(string legacyDNString)
		{
			this.legacyDNString = legacyDNString;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00041AA8 File Offset: 0x0003FCA8
		public static bool TryParse(string legacyDN, out LegacyDN result)
		{
			bool flag = LegacyDN.TryParse(legacyDN, LegacyDN.NullParserCallback.Instance);
			result = (flag ? new LegacyDN(legacyDN) : null);
			return flag;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00041AD0 File Offset: 0x0003FCD0
		public static LegacyDN Parse(string legacyDN)
		{
			LegacyDN result;
			if (!LegacyDN.TryParse(legacyDN, out result))
			{
				throw new FormatException(DirectoryStrings.ErrorInvalidLegacyDN(legacyDN));
			}
			return result;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00041AF9 File Offset: 0x0003FCF9
		public static bool IsValidLegacyDN(string legacyDN)
		{
			return LegacyDN.TryParse(legacyDN, LegacyDN.NullParserCallback.Instance);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00041B06 File Offset: 0x0003FD06
		public static bool IsValidLegacyCommonName(string cn)
		{
			return LegacyDN.IsValidLegacyCommonName(cn, 0, cn.Length);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00041B15 File Offset: 0x0003FD15
		public static string FormatAddressListDN(Guid guid)
		{
			return "/guid=" + HexConverter.ByteArrayToHexString(guid.ToByteArray());
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00041B2D File Offset: 0x0003FD2D
		public static string FormatTemplateGuid(Guid guid)
		{
			return LegacyDN.FormatLegacyDnFromGuid(Guid.Empty, guid);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00041B3A File Offset: 0x0003FD3A
		public static string FormatLegacyDnFromGuid(Guid namingContext, Guid guid)
		{
			return "/o=NT5/ou=" + HexConverter.ByteArrayToHexString(namingContext.ToByteArray()) + "/cn=" + HexConverter.ByteArrayToHexString(guid.ToByteArray());
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00041B64 File Offset: 0x0003FD64
		public static bool TryParseNspiDN(string dn, out Guid guid)
		{
			if (!string.IsNullOrEmpty(dn))
			{
				try
				{
					if (dn.Length == 38 && dn.StartsWith("/guid=", StringComparison.OrdinalIgnoreCase))
					{
						guid = new Guid(HexConverter.HexStringToByteArray(dn, 6, 32));
						return true;
					}
					if (dn.Length == 78 && dn.StartsWith("/o=NT5/ou=", StringComparison.OrdinalIgnoreCase) && string.Compare(dn, 42, "/cn=", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
					{
						new Guid(HexConverter.HexStringToByteArray(dn, 10, 32));
						guid = new Guid(HexConverter.HexStringToByteArray(dn, 46, 32));
						return true;
					}
				}
				catch (FormatException)
				{
				}
			}
			guid = Guid.Empty;
			return false;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00041C24 File Offset: 0x0003FE24
		public static string NormalizeDN(string dn)
		{
			if (string.IsNullOrEmpty(dn))
			{
				return dn;
			}
			return dn.Replace("//", "/");
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00041C40 File Offset: 0x0003FE40
		private static bool IsValidLegacyCommonName(string cn, int startIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				char c = cn[i + startIndex];
				if (c < '\0' || c >= 'Ā')
				{
					return false;
				}
				if (LegacyDN.AnsiLegacyDNMap[(int)c] != c)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00041C80 File Offset: 0x0003FE80
		private static bool IsValidRdnPrefix(string prefix)
		{
			for (int i = 0; i < LegacyDN.RdnPrefixTable.Length; i++)
			{
				if (LegacyDN.RdnPrefixTable[i].Equals(prefix, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00041CB4 File Offset: 0x0003FEB4
		private static bool IsValidRdnPrefix(string field, int prefixStartIndex, int prefixLength)
		{
			for (int i = 0; i < LegacyDN.RdnPrefixTable.Length; i++)
			{
				if (prefixLength == LegacyDN.RdnPrefixTable[i].Length && string.Compare(LegacyDN.RdnPrefixTable[i], 0, field, prefixStartIndex, prefixLength, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00041CF8 File Offset: 0x0003FEF8
		private static bool TryParse(string legacyDN, LegacyDN.IParserCallback callback)
		{
			if (string.IsNullOrEmpty(legacyDN) || legacyDN.Length < 4)
			{
				return false;
			}
			if (legacyDN[0] != '/')
			{
				return false;
			}
			int num = 1;
			int num2 = legacyDN.IndexOf('/', num);
			if (num2 == -1)
			{
				num2 = legacyDN.Length;
			}
			for (;;)
			{
				int num3 = num2 - num;
				if (num3 < 3)
				{
					break;
				}
				int num4 = legacyDN.IndexOf('=', num, num3);
				if (num4 <= num || num4 == num2 - 1)
				{
					return false;
				}
				if (!LegacyDN.IsValidRdnPrefix(legacyDN, num, num4 - num))
				{
					return false;
				}
				if (!LegacyDN.IsValidLegacyCommonName(legacyDN, num4 + 1, num2 - num4 - 1))
				{
					return false;
				}
				callback.NewSection(legacyDN, num, num3, num, num4 - num, num4 + 1, num2 - num4 - 1);
				if (num2 >= legacyDN.Length)
				{
					return true;
				}
				num = num2 + 1;
				num2 = legacyDN.IndexOf('/', num);
				if (num2 == -1)
				{
					num2 = legacyDN.Length;
				}
			}
			return false;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00041DB8 File Offset: 0x0003FFB8
		public static string LegitimizeCN(string cn)
		{
			if (!LegacyDN.IsValidLegacyCommonName(cn))
			{
				cn = Convert.ToBase64String(Encoding.UTF8.GetBytes(cn)).Replace('=', '!').Replace('/', '&');
				if (cn.Length > 64)
				{
					cn = cn.Substring(0, 64);
				}
			}
			return cn;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00041E07 File Offset: 0x00040007
		public static string GenerateLegacyDN(string parentLegacyDN, ADObject obj, bool checkInvalidChar, LegacyDN.LegacyDNIsUnique dnIsUnique)
		{
			return LegacyDN.GenerateLegacyDN(parentLegacyDN, 0, obj, checkInvalidChar, dnIsUnique, null);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00041E14 File Offset: 0x00040014
		public static string GenerateLegacyDN(string parentLegacyDN, int suggestedMaxLength, ADObject obj, bool checkInvalidChar, LegacyDN.LegacyDNIsUnique dnIsUnique)
		{
			return LegacyDN.GenerateLegacyDN(parentLegacyDN, suggestedMaxLength, obj, checkInvalidChar, dnIsUnique, null);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00041E24 File Offset: 0x00040024
		public static string GenerateLegacyDN(string parentLegacyDN, int suggestedMaxLength, ADObject obj, bool checkInvalidChar, LegacyDN.LegacyDNIsUnique dnIsUnique, string cnName)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			StringBuilder stringBuilder2 = new StringBuilder(parentLegacyDN).Append("/cn=");
			int length = stringBuilder2.Length;
			string text = cnName ?? obj.Name;
			if (checkInvalidChar && !LegacyDN.IsValidLegacyCommonName(text))
			{
				stringBuilder.Append(obj.MostDerivedObjectClass).Append(LegacyDN.GenerateRandomString(8));
			}
			else
			{
				stringBuilder.Append(text);
			}
			if (suggestedMaxLength > 0 && length + stringBuilder.Length > suggestedMaxLength && length <= suggestedMaxLength - 8)
			{
				stringBuilder.Length = suggestedMaxLength - length;
			}
			stringBuilder2.Append(stringBuilder.ToString());
			if (dnIsUnique != null)
			{
				int num = 0;
				while (num < 1000 && !dnIsUnique(stringBuilder2.ToString().Trim()))
				{
					string text2 = LegacyDN.GenerateRandomString(8);
					int num2 = stringBuilder.Length + text2.Length;
					if (num2 > 64)
					{
						stringBuilder.Length -= num2 - 64;
						num2 = 64;
					}
					if (suggestedMaxLength > 0 && length + num2 > suggestedMaxLength && length <= suggestedMaxLength - 8)
					{
						stringBuilder.Length = suggestedMaxLength - length - 8;
					}
					stringBuilder.Append(text2);
					stringBuilder2.Length = length;
					stringBuilder2.Append(stringBuilder.ToString());
					num++;
				}
				if (num >= 1000)
				{
					throw new GenerateUniqueLegacyDnException(DirectoryStrings.ErrorCannotFindUnusedLegacyDN);
				}
			}
			return stringBuilder2.ToString().Trim();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00041F77 File Offset: 0x00040177
		public static string GenerateLegacyDN(string parentLegacyDN, ADObject obj)
		{
			return LegacyDN.GenerateLegacyDN(parentLegacyDN, obj, false, null);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00041F84 File Offset: 0x00040184
		private static string GenerateRandomString(int len)
		{
			if (len > 0 && len <= 32)
			{
				return Guid.NewGuid().ToString("N").Substring(0, len);
			}
			return Guid.NewGuid().ToString("N");
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00041FC6 File Offset: 0x000401C6
		public override string ToString()
		{
			return this.legacyDNString;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00041FCE File Offset: 0x000401CE
		public LegacyDN GetChildLegacyDN(string rdnPrefix, string legacyCommonName)
		{
			return new LegacyDN(this, rdnPrefix, legacyCommonName);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00041FD8 File Offset: 0x000401D8
		public LegacyDN GetParentLegacyDN()
		{
			string text;
			string text2;
			return this.GetParentLegacyDN(out text, out text2);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00041FF0 File Offset: 0x000401F0
		public LegacyDN GetParentLegacyDN(out string childRdnPrefix, out string childCommonName)
		{
			LegacyDN.GetParentLegacyDNParserCallback getParentLegacyDNParserCallback = new LegacyDN.GetParentLegacyDNParserCallback(this.legacyDNString);
			LegacyDN.TryParse(this.legacyDNString, getParentLegacyDNParserCallback);
			childRdnPrefix = getParentLegacyDNParserCallback.ChildRdnPrefix;
			childCommonName = getParentLegacyDNParserCallback.ChildCommonName;
			return new LegacyDN(getParentLegacyDNParserCallback.ParentLegacyDN);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00042031 File Offset: 0x00040231
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LegacyDN);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0004203F File Offset: 0x0004023F
		public override int GetHashCode()
		{
			return LegacyDN.StringComparer.GetHashCode(this.legacyDNString);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00042051 File Offset: 0x00040251
		public bool Equals(LegacyDN other)
		{
			return other != null && LegacyDN.StringComparer.Equals(this.legacyDNString, other.legacyDNString);
		}

		// Token: 0x04000730 RID: 1840
		public const int MaximumCommonNameLength = 64;

		// Token: 0x04000731 RID: 1841
		private const char CharX400Spec = 'ÿ';

		// Token: 0x04000732 RID: 1842
		public const string OrganizationRdnPrefix = "o";

		// Token: 0x04000733 RID: 1843
		public const string OrganizationalUnitRdnPrefix = "ou";

		// Token: 0x04000734 RID: 1844
		public const string CommonNameRdnPrefix = "cn";

		// Token: 0x04000735 RID: 1845
		public const string AlternateMailboxRdnPrefix = "guid";

		// Token: 0x04000736 RID: 1846
		public const string AddressListPrefix = "/guid=";

		// Token: 0x04000737 RID: 1847
		public const int AddressListPrefixLength = 6;

		// Token: 0x04000738 RID: 1848
		private const int StringGuidLength = 32;

		// Token: 0x04000739 RID: 1849
		private const int AddressListDnLength = 38;

		// Token: 0x0400073A RID: 1850
		private const string NspiDnPrefix = "/o=NT5/ou=";

		// Token: 0x0400073B RID: 1851
		private const int NspiDnPrefixLength = 10;

		// Token: 0x0400073C RID: 1852
		private const string NspiDnSeparator = "/cn=";

		// Token: 0x0400073D RID: 1853
		private const int NspiDnSeparatorLength = 4;

		// Token: 0x0400073E RID: 1854
		private const int NspiDnSeparatorPosition = 42;

		// Token: 0x0400073F RID: 1855
		private const int NspiDnLength = 78;

		// Token: 0x04000740 RID: 1856
		private const int SuffixLen = 8;

		// Token: 0x04000741 RID: 1857
		private static readonly char[] AnsiLegacyDNMap = new char[]
		{
			'\0',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			' ',
			'!',
			'"',
			'?',
			'?',
			'%',
			'&',
			'\'',
			'(',
			')',
			'*',
			'+',
			',',
			'-',
			'.',
			'?',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			':',
			'?',
			'<',
			'?',
			'>',
			'?',
			'@',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'[',
			'?',
			']',
			'?',
			'_',
			'?',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'{',
			'|',
			'}',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'S',
			'?',
			'ÿ',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			'?',
			's',
			'?',
			'ÿ',
			'?',
			'?',
			'Y',
			'?',
			'?',
			'C',
			'L',
			'P',
			'Y',
			'I',
			'S',
			'?',
			'C',
			'A',
			'?',
			'?',
			'?',
			'R',
			'?',
			'?',
			'?',
			'2',
			'3',
			'?',
			'M',
			'P',
			'?',
			'?',
			'1',
			'O',
			'?',
			'?',
			'?',
			'?',
			'?',
			'A',
			'A',
			'A',
			'A',
			'ÿ',
			'A',
			'ÿ',
			'C',
			'E',
			'E',
			'E',
			'E',
			'I',
			'I',
			'I',
			'I',
			'D',
			'N',
			'O',
			'O',
			'O',
			'O',
			'ÿ',
			'X',
			'0',
			'U',
			'U',
			'U',
			'ÿ',
			'Y',
			'T',
			'ÿ',
			'a',
			'a',
			'a',
			'a',
			'ÿ',
			'a',
			'ÿ',
			'c',
			'e',
			'e',
			'e',
			'e',
			'i',
			'i',
			'i',
			'i',
			'd',
			'n',
			'o',
			'o',
			'o',
			'o',
			'ÿ',
			'?',
			'o',
			'u',
			'u',
			'u',
			'ÿ',
			'y',
			'T',
			'y'
		};

		// Token: 0x04000742 RID: 1858
		private static readonly string[] RdnPrefixTable = new string[]
		{
			"o",
			"ou",
			"cn",
			"guid"
		};

		// Token: 0x04000743 RID: 1859
		public static readonly StringComparer StringComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x04000744 RID: 1860
		private readonly string legacyDNString;

		// Token: 0x0200014A RID: 330
		private interface IParserCallback
		{
			// Token: 0x06000E1D RID: 3613
			void NewSection(string buffer, int startIndex, int length, int rdnPrefixStartIndex, int rdnPrefixLength, int commonNameStartIndex, int commonNameLength);
		}

		// Token: 0x0200014B RID: 331
		private sealed class NullParserCallback : LegacyDN.IParserCallback
		{
			// Token: 0x06000E1E RID: 3614 RVA: 0x000422CE File Offset: 0x000404CE
			private NullParserCallback()
			{
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x000422D6 File Offset: 0x000404D6
			void LegacyDN.IParserCallback.NewSection(string buffer, int startIndex, int length, int rdnPrefixStartIndex, int rdnPrefixLength, int commonNameStartIndex, int commonNameLength)
			{
			}

			// Token: 0x04000745 RID: 1861
			public static readonly LegacyDN.NullParserCallback Instance = new LegacyDN.NullParserCallback();
		}

		// Token: 0x0200014C RID: 332
		private sealed class GetParentLegacyDNParserCallback : LegacyDN.IParserCallback
		{
			// Token: 0x06000E21 RID: 3617 RVA: 0x000422E4 File Offset: 0x000404E4
			public GetParentLegacyDNParserCallback(string sourceString)
			{
				this.sourceString = sourceString;
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000E22 RID: 3618 RVA: 0x000422F3 File Offset: 0x000404F3
			public string ParentLegacyDN
			{
				get
				{
					return this.sourceString.Substring(0, this.parentLegacyDNLength);
				}
			}

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00042307 File Offset: 0x00040507
			public string ChildCommonName
			{
				get
				{
					return this.sourceString.Substring(this.currentCommonNameStartIndex, this.currentCommonNameLength);
				}
			}

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00042320 File Offset: 0x00040520
			public string ChildRdnPrefix
			{
				get
				{
					return this.sourceString.Substring(this.currentRdnPrefixStartIndex, this.currentRdnPrefixLength);
				}
			}

			// Token: 0x06000E25 RID: 3621 RVA: 0x00042339 File Offset: 0x00040539
			void LegacyDN.IParserCallback.NewSection(string buffer, int startIndex, int length, int rdnPrefixStartIndex, int rdnPrefixLength, int commonNameStartIndex, int commonNameLength)
			{
				this.parentLegacyDNLength = this.currentLegacyDNLength;
				this.currentLegacyDNLength = startIndex + length;
				this.currentRdnPrefixStartIndex = rdnPrefixStartIndex;
				this.currentRdnPrefixLength = rdnPrefixLength;
				this.currentCommonNameStartIndex = commonNameStartIndex;
				this.currentCommonNameLength = commonNameLength;
			}

			// Token: 0x04000746 RID: 1862
			private readonly string sourceString;

			// Token: 0x04000747 RID: 1863
			private int parentLegacyDNLength;

			// Token: 0x04000748 RID: 1864
			private int currentLegacyDNLength;

			// Token: 0x04000749 RID: 1865
			private int currentRdnPrefixStartIndex;

			// Token: 0x0400074A RID: 1866
			private int currentRdnPrefixLength;

			// Token: 0x0400074B RID: 1867
			private int currentCommonNameStartIndex;

			// Token: 0x0400074C RID: 1868
			private int currentCommonNameLength;
		}

		// Token: 0x0200014D RID: 333
		// (Invoke) Token: 0x06000E27 RID: 3623
		public delegate bool LegacyDNIsUnique(string legacyDN);
	}
}
