using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x0200011B RID: 283
	public class OutboundCodePageDetector
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x000686D8 File Offset: 0x000668D8
		public OutboundCodePageDetector()
		{
			this.detector.Initialize();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000686EB File Offset: 0x000668EB
		internal static bool IsCodePageDetectable(int cpid, bool onlyValid)
		{
			return CodePageDetect.IsCodePageDetectable(cpid, onlyValid);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000686F4 File Offset: 0x000668F4
		internal static int[] GetDefaultCodePagePriorityList()
		{
			return CodePageDetect.GetDefaultPriorityList();
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000686FB File Offset: 0x000668FB
		internal static char[] GetCommonExceptionCharacters()
		{
			return CodePageDetect.GetCommonExceptionCharacters();
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00068702 File Offset: 0x00066902
		public void Reset()
		{
			this.detector.Reset();
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0006870F File Offset: 0x0006690F
		public void AddText(char ch)
		{
			this.detector.AddData(ch);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00068720 File Offset: 0x00066920
		public void AddText(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index", GlobalizationStrings.IndexOutOfRange);
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", GlobalizationStrings.CountOutOfRange);
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count", GlobalizationStrings.CountTooLarge);
			}
			this.detector.AddData(buffer, index, count);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00068795 File Offset: 0x00066995
		public void AddText(char[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.detector.AddData(buffer, 0, buffer.Length);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000687B8 File Offset: 0x000669B8
		public void AddText(string value, int index, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index < 0 || index > value.Length)
			{
				throw new ArgumentOutOfRangeException("index", GlobalizationStrings.IndexOutOfRange);
			}
			if (count < 0 || count > value.Length)
			{
				throw new ArgumentOutOfRangeException("count", GlobalizationStrings.CountOutOfRange);
			}
			if (value.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count", GlobalizationStrings.CountTooLarge);
			}
			this.detector.AddData(value, index, count);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00068836 File Offset: 0x00066A36
		public void AddText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.detector.AddData(value, 0, value.Length);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00068859 File Offset: 0x00066A59
		public void AddText(TextReader reader, int maxCharacters)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (maxCharacters < 0)
			{
				throw new ArgumentOutOfRangeException("maxCharacters", GlobalizationStrings.MaxCharactersCannotBeNegative);
			}
			this.detector.AddData(reader, maxCharacters);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0006888A File Offset: 0x00066A8A
		public void AddText(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.detector.AddData(reader, int.MaxValue);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000688AB File Offset: 0x00066AAB
		public int GetCodePage()
		{
			return this.detector.GetCodePage(Culture.Default.CodepageDetectionPriorityOrder, false, false, true);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000688C5 File Offset: 0x00066AC5
		public int GetCodePage(Culture culture, bool allowCommonFallbackExceptions)
		{
			if (culture == null)
			{
				culture = Culture.Default;
			}
			return this.detector.GetCodePage(culture.CodepageDetectionPriorityOrder, allowCommonFallbackExceptions, false, true);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000688E8 File Offset: 0x00066AE8
		public int GetCodePage(Charset preferredCharset, bool allowCommonFallbackExceptions)
		{
			int[] array = Culture.Default.CodepageDetectionPriorityOrder;
			if (preferredCharset != null)
			{
				array = CultureCharsetDatabase.GetAdjustedCodepageDetectionPriorityOrder(preferredCharset, array);
			}
			return this.detector.GetCodePage(array, allowCommonFallbackExceptions, false, true);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0006891C File Offset: 0x00066B1C
		internal int GetCodePage(int[] codePagePriorityList, FallbackExceptions fallbackExceptions, bool onlyValidCodePages)
		{
			if (codePagePriorityList != null)
			{
				for (int i = 0; i < codePagePriorityList.Length; i++)
				{
					if (!CodePageDetect.IsCodePageDetectable(codePagePriorityList[i], false))
					{
						throw new ArgumentException(GlobalizationStrings.PriorityListIncludesNonDetectableCodePage, "codePagePriorityList");
					}
				}
			}
			return this.detector.GetCodePage(codePagePriorityList, fallbackExceptions > FallbackExceptions.None, fallbackExceptions > FallbackExceptions.Common, onlyValidCodePages);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0006896A File Offset: 0x00066B6A
		public int[] GetCodePages()
		{
			return this.detector.GetCodePages(Culture.Default.CodepageDetectionPriorityOrder, false, false, true);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00068984 File Offset: 0x00066B84
		public int[] GetCodePages(Culture culture, bool allowCommonFallbackExceptions)
		{
			if (culture == null)
			{
				culture = Culture.Default;
			}
			return this.detector.GetCodePages(culture.CodepageDetectionPriorityOrder, allowCommonFallbackExceptions, false, true);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000689A4 File Offset: 0x00066BA4
		public int[] GetCodePages(Charset preferredCharset, bool allowCommonFallbackExceptions)
		{
			int[] array = Culture.Default.CodepageDetectionPriorityOrder;
			if (preferredCharset != null)
			{
				array = CultureCharsetDatabase.GetAdjustedCodepageDetectionPriorityOrder(preferredCharset, array);
			}
			return this.detector.GetCodePages(array, allowCommonFallbackExceptions, false, true);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000689D8 File Offset: 0x00066BD8
		internal int[] GetCodePages(int[] codePagePriorityList, FallbackExceptions fallbackExceptions, bool onlyValidCodePages)
		{
			if (codePagePriorityList != null)
			{
				for (int i = 0; i < codePagePriorityList.Length; i++)
				{
					if (!CodePageDetect.IsCodePageDetectable(codePagePriorityList[i], false))
					{
						throw new ArgumentException(GlobalizationStrings.PriorityListIncludesNonDetectableCodePage, "codePagePriorityList");
					}
				}
			}
			return this.detector.GetCodePages(codePagePriorityList, fallbackExceptions > FallbackExceptions.None, fallbackExceptions > FallbackExceptions.Common, onlyValidCodePages);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00068A28 File Offset: 0x00066C28
		public int GetCodePageCoverage(int codePage)
		{
			Charset charset = Charset.GetCharset(codePage);
			if (charset.UnicodeCoverage == CodePageUnicodeCoverage.Complete)
			{
				return 100;
			}
			if (!charset.IsDetectable)
			{
				if (charset.DetectableCodePageWithEquivalentCoverage == 0)
				{
					throw new ArgumentException("codePage is not detectable");
				}
				codePage = charset.DetectableCodePageWithEquivalentCoverage;
			}
			return this.detector.GetCodePageCoverage(codePage);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00068A77 File Offset: 0x00066C77
		public int GetBestWindowsCodePage()
		{
			return this.detector.GetBestWindowsCodePage(false, false);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00068A86 File Offset: 0x00066C86
		internal int GetBestWindowsCodePage(int preferredCodePage)
		{
			return this.detector.GetBestWindowsCodePage(false, false, preferredCodePage);
		}

		// Token: 0x04000E4A RID: 3658
		private CodePageDetect detector;
	}
}
