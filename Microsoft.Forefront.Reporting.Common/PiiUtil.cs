using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000010 RID: 16
	public static class PiiUtil
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000021F4 File Offset: 0x000003F4
		public static string GetHashPartFromPiiString(string piiString)
		{
			Match match = PiiUtil.PiiUnitRegex.Match(piiString);
			if (match.Success)
			{
				return match.Groups[1].Value;
			}
			return string.Empty;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000222C File Offset: 0x0000042C
		public static string ExtractLSH(string subjectPiiString)
		{
			Match match = PiiUtil.LshExtractRegex.Match(subjectPiiString);
			if (match.Success)
			{
				return match.Groups[1].Value;
			}
			return string.Empty;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002264 File Offset: 0x00000464
		public static string ExtractHashValue(string piiString)
		{
			Match match = PiiUtil.PiiUnitRegex.Match(piiString);
			if (match.Success)
			{
				return match.Groups[2].Value;
			}
			return string.Empty;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000229C File Offset: 0x0000049C
		public static bool IsValidPiiUnit(string piiString)
		{
			return PiiUtil.PiiCompleteRegex.IsMatch(piiString);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022A9 File Offset: 0x000004A9
		public static string PiiCleanse(string columnData)
		{
			return PiiUtil.PiiUnitRegex.Replace(columnData, "<PII$1>");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022BB File Offset: 0x000004BB
		public static bool MatchLSH(string target, string query, int percentageThreshold)
		{
			return PiiUtil.MatchLSH(Convert.FromBase64String(target), Convert.FromBase64String(query), percentageThreshold);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022D0 File Offset: 0x000004D0
		public static List<ushort> GetShinglesFromLSH(string lshString)
		{
			List<ushort> list = new List<ushort>();
			byte[] array = Convert.FromBase64String(lshString);
			byte[] array2 = new byte[2];
			for (int i = 0; i < (array.Length - 1) / 2; i++)
			{
				array2[PiiUtil.UshortIdx0] = array[1 + 2 * i];
				array2[PiiUtil.UshortIdx1] = array[2 + 2 * i];
				list.Add(BitConverter.ToUInt16(array2, 0));
			}
			return list;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002330 File Offset: 0x00000530
		private static bool MatchLSH(byte[] target, byte[] query, int percentageThreshold)
		{
			if (target.Length < 3 || query.Length < 3 || target[0] != query[0])
			{
				return false;
			}
			int num = Math.Max(percentageThreshold * (query.Length - 1) / 2 / 100, 1);
			int num2 = 1;
			int num3 = 1;
			int num4 = 0;
			while (target.Length - num2 >= (num - num4) * 2 && query.Length - num3 >= (num - num4) * 2)
			{
				if (target[num2] > query[num3])
				{
					num3 += 2;
				}
				else if (target[num2] < query[num3])
				{
					num2 += 2;
				}
				else if (target[num2 + 1] > query[num3 + 1])
				{
					num3 += 2;
				}
				else if (target[num2 + 1] < query[num3 + 1])
				{
					num2 += 2;
				}
				else
				{
					num4++;
					if (num4 >= num)
					{
						return true;
					}
					num3 += 2;
					num2 += 2;
				}
			}
			return false;
		}

		// Token: 0x0400003D RID: 61
		public const string HashValueRegexPattern = ":H[0-9]{1,5}\\(((?:[^\\)]){1,1000})\\)";

		// Token: 0x0400003E RID: 62
		public const string AnyValueGroupRegexPatter = ":[A-Z][0-9]{1,5}\\((?:[^\\)]){1,1000}\\)";

		// Token: 0x0400003F RID: 63
		public const string EncryptedValueRegexPattern = ":E[0-9]{1,5}\\((?:[^\\)]){1,10000}\\)";

		// Token: 0x04000040 RID: 64
		public const string LSHValueRegexPattern = ":L[0-9]{1,5}\\(((?:[^\\)]){1,10000})\\)";

		// Token: 0x04000041 RID: 65
		public const string PiiUnitRegexPattern = "<PII(:H[0-9]{1,5}\\(((?:[^\\)]){1,1000})\\))(?::[A-Z][0-9]{1,5}\\((?:[^\\)]){1,1000}\\))*>";

		// Token: 0x04000042 RID: 66
		public static readonly Regex PiiUnitRegex = new Regex("<PII(:H[0-9]{1,5}\\(((?:[^\\)]){1,1000})\\))(?::[A-Z][0-9]{1,5}\\((?:[^\\)]){1,1000}\\))*>", RegexOptions.Compiled);

		// Token: 0x04000043 RID: 67
		public static readonly Regex PiiCompleteRegex = new Regex("^<PII(:H[0-9]{1,5}\\(((?:[^\\)]){1,1000})\\))(?::[A-Z][0-9]{1,5}\\((?:[^\\)]){1,1000}\\))*>$", RegexOptions.Compiled);

		// Token: 0x04000044 RID: 68
		public static readonly Regex LshExtractRegex = new Regex(":L[0-9]{1,5}\\(((?:[^\\)]){1,10000})\\)", RegexOptions.Compiled);

		// Token: 0x04000045 RID: 69
		public static readonly Regex EncryptedExtractRegex = new Regex(":E[0-9]{1,5}\\((?:[^\\)]){1,10000}\\)", RegexOptions.Compiled);

		// Token: 0x04000046 RID: 70
		private static readonly int UshortIdx0 = BitConverter.IsLittleEndian ? 1 : 0;

		// Token: 0x04000047 RID: 71
		private static readonly int UshortIdx1 = BitConverter.IsLittleEndian ? 0 : 1;
	}
}
