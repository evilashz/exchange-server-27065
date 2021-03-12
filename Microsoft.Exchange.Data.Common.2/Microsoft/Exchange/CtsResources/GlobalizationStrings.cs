using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x020000F7 RID: 247
	internal static class GlobalizationStrings
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
		static GlobalizationStrings()
		{
			GlobalizationStrings.stringIDs.Add(1308081499U, "MaxCharactersCannotBeNegative");
			GlobalizationStrings.stringIDs.Add(1590522975U, "CountOutOfRange");
			GlobalizationStrings.stringIDs.Add(1083457927U, "PriorityListIncludesNonDetectableCodePage");
			GlobalizationStrings.stringIDs.Add(3590683541U, "OffsetOutOfRange");
			GlobalizationStrings.stringIDs.Add(1226301788U, "IndexOutOfRange");
			GlobalizationStrings.stringIDs.Add(2746482960U, "CountTooLarge");
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0003E057 File Offset: 0x0003C257
		public static string InvalidCodePage(int codePage)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("InvalidCodePage"), codePage);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0003E073 File Offset: 0x0003C273
		public static string NotInstalledCodePage(int codePage)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("NotInstalledCodePage"), codePage);
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0003E08F File Offset: 0x0003C28F
		public static string MaxCharactersCannotBeNegative
		{
			get
			{
				return GlobalizationStrings.ResourceManager.GetString("MaxCharactersCannotBeNegative");
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		public static string CountOutOfRange
		{
			get
			{
				return GlobalizationStrings.ResourceManager.GetString("CountOutOfRange");
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0003E0B1 File Offset: 0x0003C2B1
		public static string PriorityListIncludesNonDetectableCodePage
		{
			get
			{
				return GlobalizationStrings.ResourceManager.GetString("PriorityListIncludesNonDetectableCodePage");
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0003E0C2 File Offset: 0x0003C2C2
		public static string OffsetOutOfRange
		{
			get
			{
				return GlobalizationStrings.ResourceManager.GetString("OffsetOutOfRange");
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0003E0D3 File Offset: 0x0003C2D3
		public static string InvalidCultureName(string cultureName)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("InvalidCultureName"), cultureName);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0003E0EA File Offset: 0x0003C2EA
		public static string NotInstalledCharsetCodePage(int codePage, string charsetName)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("NotInstalledCharsetCodePage"), codePage, charsetName);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0003E107 File Offset: 0x0003C307
		public static string IndexOutOfRange
		{
			get
			{
				return GlobalizationStrings.ResourceManager.GetString("IndexOutOfRange");
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0003E118 File Offset: 0x0003C318
		public static string CountTooLarge
		{
			get
			{
				return GlobalizationStrings.ResourceManager.GetString("CountTooLarge");
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0003E129 File Offset: 0x0003C329
		public static string NotInstalledCharset(string charsetName)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("NotInstalledCharset"), charsetName);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0003E140 File Offset: 0x0003C340
		public static string InvalidLocaleId(int localeId)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("InvalidLocaleId"), localeId);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0003E15C File Offset: 0x0003C35C
		public static string InvalidCharset(string charsetName)
		{
			return string.Format(GlobalizationStrings.ResourceManager.GetString("InvalidCharset"), charsetName);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0003E173 File Offset: 0x0003C373
		public static string GetLocalizedString(GlobalizationStrings.IDs key)
		{
			return GlobalizationStrings.ResourceManager.GetString(GlobalizationStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000D76 RID: 3446
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x04000D77 RID: 3447
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.GlobalizationStrings", typeof(GlobalizationStrings).GetTypeInfo().Assembly);

		// Token: 0x020000F8 RID: 248
		public enum IDs : uint
		{
			// Token: 0x04000D79 RID: 3449
			MaxCharactersCannotBeNegative = 1308081499U,
			// Token: 0x04000D7A RID: 3450
			CountOutOfRange = 1590522975U,
			// Token: 0x04000D7B RID: 3451
			PriorityListIncludesNonDetectableCodePage = 1083457927U,
			// Token: 0x04000D7C RID: 3452
			OffsetOutOfRange = 3590683541U,
			// Token: 0x04000D7D RID: 3453
			IndexOutOfRange = 1226301788U,
			// Token: 0x04000D7E RID: 3454
			CountTooLarge = 2746482960U
		}

		// Token: 0x020000F9 RID: 249
		private enum ParamIDs
		{
			// Token: 0x04000D80 RID: 3456
			InvalidCodePage,
			// Token: 0x04000D81 RID: 3457
			NotInstalledCodePage,
			// Token: 0x04000D82 RID: 3458
			InvalidCultureName,
			// Token: 0x04000D83 RID: 3459
			NotInstalledCharsetCodePage,
			// Token: 0x04000D84 RID: 3460
			NotInstalledCharset,
			// Token: 0x04000D85 RID: 3461
			InvalidLocaleId,
			// Token: 0x04000D86 RID: 3462
			InvalidCharset
		}
	}
}
