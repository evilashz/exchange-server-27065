using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x0200012D RID: 301
	internal static class SharedStrings
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0006B338 File Offset: 0x00069538
		static SharedStrings()
		{
			SharedStrings.stringIDs.Add(3996289637U, "InvalidFactory");
			SharedStrings.stringIDs.Add(1551326176U, "CannotSetNegativelength");
			SharedStrings.stringIDs.Add(1590522975U, "CountOutOfRange");
			SharedStrings.stringIDs.Add(2864662625U, "CannotSeekBeforeBeginning");
			SharedStrings.stringIDs.Add(2489963781U, "StringArgumentMustBeUTF8");
			SharedStrings.stringIDs.Add(3590683541U, "OffsetOutOfRange");
			SharedStrings.stringIDs.Add(2746482960U, "CountTooLarge");
			SharedStrings.stringIDs.Add(431486251U, "StringArgumentMustBeAscii");
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0006B413 File Offset: 0x00069613
		public static string InvalidFactory
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("InvalidFactory");
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0006B424 File Offset: 0x00069624
		public static string CannotSetNegativelength
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("CannotSetNegativelength");
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0006B435 File Offset: 0x00069635
		public static string CreateFileFailed(string filePath)
		{
			return string.Format(SharedStrings.ResourceManager.GetString("CreateFileFailed"), filePath);
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0006B44C File Offset: 0x0006964C
		public static string CountOutOfRange
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("CountOutOfRange");
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0006B45D File Offset: 0x0006965D
		public static string CannotSeekBeforeBeginning
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("CannotSeekBeforeBeginning");
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0006B46E File Offset: 0x0006966E
		public static string StringArgumentMustBeUTF8
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("StringArgumentMustBeUTF8");
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0006B47F File Offset: 0x0006967F
		public static string OffsetOutOfRange
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("OffsetOutOfRange");
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0006B490 File Offset: 0x00069690
		public static string CountTooLarge
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("CountTooLarge");
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0006B4A1 File Offset: 0x000696A1
		public static string StringArgumentMustBeAscii
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("StringArgumentMustBeAscii");
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0006B4B2 File Offset: 0x000696B2
		public static string GetLocalizedString(SharedStrings.IDs key)
		{
			return SharedStrings.ResourceManager.GetString(SharedStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000E97 RID: 3735
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(8);

		// Token: 0x04000E98 RID: 3736
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.SharedStrings", typeof(SharedStrings).GetTypeInfo().Assembly);

		// Token: 0x0200012E RID: 302
		public enum IDs : uint
		{
			// Token: 0x04000E9A RID: 3738
			InvalidFactory = 3996289637U,
			// Token: 0x04000E9B RID: 3739
			CannotSetNegativelength = 1551326176U,
			// Token: 0x04000E9C RID: 3740
			CountOutOfRange = 1590522975U,
			// Token: 0x04000E9D RID: 3741
			CannotSeekBeforeBeginning = 2864662625U,
			// Token: 0x04000E9E RID: 3742
			StringArgumentMustBeUTF8 = 2489963781U,
			// Token: 0x04000E9F RID: 3743
			OffsetOutOfRange = 3590683541U,
			// Token: 0x04000EA0 RID: 3744
			CountTooLarge = 2746482960U,
			// Token: 0x04000EA1 RID: 3745
			StringArgumentMustBeAscii = 431486251U
		}

		// Token: 0x0200012F RID: 303
		private enum ParamIDs
		{
			// Token: 0x04000EA3 RID: 3747
			CreateFileFailed
		}
	}
}
