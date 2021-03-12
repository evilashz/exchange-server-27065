using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000521 RID: 1313
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PeopleStringUtils
	{
		// Token: 0x06003820 RID: 14368 RVA: 0x000E5DF4 File Offset: 0x000E3FF4
		public static string ComputeSortKey(CultureInfo culture, string value)
		{
			if (value == null)
			{
				return null;
			}
			SortKey sortKey = culture.CompareInfo.GetSortKey(value, PeopleStringUtils.StringCompareOptions);
			byte[] keyData = sortKey.KeyData;
			StringBuilder stringBuilder = new StringBuilder(200);
			int num = 0;
			while (num < keyData.Length && num < 100)
			{
				stringBuilder.Append(keyData[num].ToString("X2"));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000E5E5C File Offset: 0x000E405C
		public static string ComputeSortVersion(CultureInfo culture)
		{
			PeopleStringUtils.NLSVERSIONINFO nlsversioninfo;
			nlsversioninfo.dwNLSVersionInfoSize = (uint)Marshal.SizeOf(typeof(PeopleStringUtils.NLSVERSIONINFO));
			if (!PeopleStringUtils.GetNLSVersion(PeopleStringUtils.NLS_FUNCTION.COMPARE_STRING, (uint)culture.LCID, out nlsversioninfo))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return string.Format("{0:X8},{1:X8}", nlsversioninfo.dwDefinedVersion, nlsversioninfo.dwNLSVersion);
		}

		// Token: 0x06003822 RID: 14370
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetNLSVersion(PeopleStringUtils.NLS_FUNCTION function, uint locale, out PeopleStringUtils.NLSVERSIONINFO lpVersionInformation);

		// Token: 0x04001DF8 RID: 7672
		public static readonly CompareOptions StringCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x02000522 RID: 1314
		internal enum NLS_FUNCTION : uint
		{
			// Token: 0x04001DFA RID: 7674
			COMPARE_STRING = 1U
		}

		// Token: 0x02000523 RID: 1315
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct NLSVERSIONINFO
		{
			// Token: 0x04001DFB RID: 7675
			public uint dwNLSVersionInfoSize;

			// Token: 0x04001DFC RID: 7676
			public uint dwNLSVersion;

			// Token: 0x04001DFD RID: 7677
			public uint dwDefinedVersion;
		}
	}
}
