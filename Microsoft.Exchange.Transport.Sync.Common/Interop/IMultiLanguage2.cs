using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Interop
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[InterfaceType(1)]
	[CoClass(typeof(CMultiLanguage))]
	[Guid("DCCFC164-2B38-11D2-B7EC-00C04F8F5D9A")]
	[ComImport]
	internal interface IMultiLanguage2
	{
		// Token: 0x0600030B RID: 779
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetNumberOfCodePageInfo(out uint pcCodePage);

		// Token: 0x0600030C RID: 780
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCodePageInfo();

		// Token: 0x0600030D RID: 781
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFamilyCodePage([In] uint uiCodePage, out uint puiFamilyCodePage);

		// Token: 0x0600030E RID: 782
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumCodePages();

		// Token: 0x0600030F RID: 783
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCharsetInfo();

		// Token: 0x06000310 RID: 784
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsConvertible([In] uint dwSrcEncoding, [In] uint dwDstEncoding);

		// Token: 0x06000311 RID: 785
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertString([In] [Out] ref uint pdwMode, [In] uint dwSrcEncoding, [In] uint dwDstEncoding, [In] ref byte pSrcStr, [In] [Out] ref uint pcSrcSize, [In] ref byte pDstStr, [In] [Out] ref uint pcDstSize);

		// Token: 0x06000312 RID: 786
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertStringToUnicode([In] [Out] ref uint pdwMode, [In] uint dwEncoding, [In] ref sbyte pSrcStr, [In] [Out] ref uint pcSrcSize, [In] ref ushort pDstStr, [In] [Out] ref uint pcDstSize);

		// Token: 0x06000313 RID: 787
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertStringFromUnicode([In] [Out] ref uint pdwMode, [In] uint dwEncoding, [In] ref ushort pSrcStr, [In] [Out] ref uint pcSrcSize, [In] ref sbyte pDstStr, [In] [Out] ref uint pcDstSize);

		// Token: 0x06000314 RID: 788
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertStringReset();

		// Token: 0x06000315 RID: 789
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRfc1766FromLcid([In] uint locale, [MarshalAs(UnmanagedType.BStr)] out string pbstrRfc1766);

		// Token: 0x06000316 RID: 790
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetLcidFromRfc1766(out uint plocale, [MarshalAs(UnmanagedType.BStr)] [In] string bstrRfc1766);

		// Token: 0x06000317 RID: 791
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumRfc1766();

		// Token: 0x06000318 RID: 792
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRfc1766Info();

		// Token: 0x06000319 RID: 793
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateConvertCharset();

		// Token: 0x0600031A RID: 794
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertStringInIStream();

		// Token: 0x0600031B RID: 795
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertStringToUnicodeEx([In] [Out] ref uint pdwMode, [In] uint dwEncoding, [In] ref sbyte pSrcStr, [In] [Out] ref uint pcSrcSize, [In] ref ushort pDstStr, [In] [Out] ref uint pcDstSize, [In] uint dwFlag, [In] ref ushort lpFallBack);

		// Token: 0x0600031C RID: 796
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConvertStringFromUnicodeEx([In] [Out] ref uint pdwMode, [In] uint dwEncoding, [In] ref ushort pSrcStr, [In] [Out] ref uint pcSrcSize, [In] ref sbyte pDstStr, [In] [Out] ref uint pcDstSize, [In] uint dwFlag, [In] ref ushort lpFallBack);

		// Token: 0x0600031D RID: 797
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DetectCodepageInIStream();

		// Token: 0x0600031E RID: 798
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DetectInputCodepage([In] MLDETECTCP flags, [In] uint dwPrefWinCodePage, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] [In] byte[] pSrcStr, [In] [Out] ref int pcSrcSize, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] [In] [Out] DetectEncodingInfo[] lpEncoding, [In] [Out] ref int pnScores);

		// Token: 0x0600031F RID: 799
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ValidateCodePage();

		// Token: 0x06000320 RID: 800
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCodePageDescription([In] uint uiCodePage, [In] uint lcid, [MarshalAs(UnmanagedType.LPWStr)] [In] [Out] string lpWideCharStr, [In] int cchWideChar);

		// Token: 0x06000321 RID: 801
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsCodePageInstallable();

		// Token: 0x06000322 RID: 802
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMimeDBSource();

		// Token: 0x06000323 RID: 803
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetNumberOfScripts(out uint pnScripts);

		// Token: 0x06000324 RID: 804
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumScripts();

		// Token: 0x06000325 RID: 805
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ValidateCodePageEx();
	}
}
