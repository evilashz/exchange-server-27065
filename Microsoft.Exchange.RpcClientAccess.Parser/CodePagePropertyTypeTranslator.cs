using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class CodePagePropertyTypeTranslator
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00006FA0 File Offset: 0x000051A0
		private static bool IsValidCodePage(int codePage)
		{
			Encoding encoding;
			return String8Encodings.TryGetEncoding((int)((ushort)codePage), out encoding);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006FB8 File Offset: 0x000051B8
		public static int CodePageToPropertyTagEncodedCodePage(int codePage)
		{
			if (!CodePagePropertyTypeTranslator.IsValidCodePage(codePage))
			{
				return 4095;
			}
			if (codePage < 3584)
			{
				return codePage;
			}
			if (codePage < 10000)
			{
				return 4095;
			}
			if (codePage <= 10127)
			{
				return codePage - 10000 + 3584;
			}
			if (codePage < 20000)
			{
				return 4095;
			}
			if (codePage <= 20300)
			{
				return codePage - 20000 + 3712;
			}
			if (codePage == 65001)
			{
				return 4091;
			}
			if (codePage == 65000)
			{
				return 4092;
			}
			if (codePage == 20866)
			{
				return 4093;
			}
			if (codePage == 28592)
			{
				return 4094;
			}
			if (codePage == 28591)
			{
				return 4013;
			}
			if (codePage >= 28593 && codePage <= 28599)
			{
				return 4014 + (codePage - 28593);
			}
			if (codePage == 20269)
			{
				return 4021;
			}
			if (codePage == 21027)
			{
				return 4022;
			}
			if (codePage == 29001)
			{
				return 4023;
			}
			if (codePage == 21866)
			{
				return 4090;
			}
			if (codePage == 28605)
			{
				return 4089;
			}
			if (codePage == 54936)
			{
				return 4024;
			}
			return 4095;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000070E8 File Offset: 0x000052E8
		public static int PropertyTagEncodedCodePageToCodePage(int propertyTagEncodedCodePage)
		{
			if (propertyTagEncodedCodePage < 3584)
			{
				return propertyTagEncodedCodePage;
			}
			if (propertyTagEncodedCodePage <= 3711)
			{
				return propertyTagEncodedCodePage + 6416;
			}
			if (propertyTagEncodedCodePage < 3712)
			{
				return 4095;
			}
			if (propertyTagEncodedCodePage <= 4012)
			{
				return propertyTagEncodedCodePage + 16288;
			}
			if (propertyTagEncodedCodePage == 4091)
			{
				return 65001;
			}
			if (propertyTagEncodedCodePage == 4092)
			{
				return 65000;
			}
			if (propertyTagEncodedCodePage == 4093)
			{
				return 20866;
			}
			if (propertyTagEncodedCodePage == 4094)
			{
				return 28592;
			}
			if (propertyTagEncodedCodePage == 4013)
			{
				return 28591;
			}
			if (propertyTagEncodedCodePage >= 4014 && propertyTagEncodedCodePage <= 4020)
			{
				return 28593 + (propertyTagEncodedCodePage - 4014);
			}
			if (propertyTagEncodedCodePage == 4021)
			{
				return 20269;
			}
			if (propertyTagEncodedCodePage == 4022)
			{
				return 21027;
			}
			if (propertyTagEncodedCodePage == 4023)
			{
				return 29001;
			}
			if (propertyTagEncodedCodePage == 4090)
			{
				return 21866;
			}
			if (propertyTagEncodedCodePage == 4089)
			{
				return 28605;
			}
			if (propertyTagEncodedCodePage == 4024)
			{
				return 54936;
			}
			if (propertyTagEncodedCodePage == 54936)
			{
				return 54936;
			}
			return 4095;
		}

		// Token: 0x040000CC RID: 204
		private const int CodePageRange2Max = 20300;

		// Token: 0x040000CD RID: 205
		private const int CodePageRange2Min = 20000;

		// Token: 0x040000CE RID: 206
		private const int CodePageRange1Max = 10127;

		// Token: 0x040000CF RID: 207
		private const int CodePageRange1Min = 10000;

		// Token: 0x040000D0 RID: 208
		private const int PropertyTagEncodedCodePageRange2Max = 4012;

		// Token: 0x040000D1 RID: 209
		private const int PropertyTagEncodedCodePageRange2Min = 3712;

		// Token: 0x040000D2 RID: 210
		private const int PropertyTagEncodedCodePageRange1Max = 3711;

		// Token: 0x040000D3 RID: 211
		private const int PropertyTagEncodedCodePageRange1Min = 3584;
	}
}
