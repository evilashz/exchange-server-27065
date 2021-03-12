using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000C0 RID: 192
	[FriendAccessAllowed]
	internal static class CompatibilitySwitches
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00022BBA File Offset: 0x00020DBA
		public static bool IsCompatibilityBehaviorDefined
		{
			get
			{
				return CompatibilitySwitches.s_AreSwitchesSet;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00022BC4 File Offset: 0x00020DC4
		private static bool IsCompatibilitySwitchSet(string compatibilitySwitch)
		{
			bool? flag = AppDomain.CurrentDomain.IsCompatibilitySwitchSet(compatibilitySwitch);
			return flag != null && flag.Value;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00022BEF File Offset: 0x00020DEF
		internal static void InitializeSwitches()
		{
			CompatibilitySwitches.s_isNetFx40TimeSpanLegacyFormatMode = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx40_TimeSpanLegacyFormatMode");
			CompatibilitySwitches.s_isNetFx40LegacySecurityPolicy = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx40_LegacySecurityPolicy");
			CompatibilitySwitches.s_isNetFx45LegacyManagedDeflateStream = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx45_LegacyManagedDeflateStream");
			CompatibilitySwitches.s_AreSwitchesSet = true;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00022C24 File Offset: 0x00020E24
		public static bool IsAppEarlierThanSilverlight4
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00022C27 File Offset: 0x00020E27
		public static bool IsAppEarlierThanWindowsPhone8
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00022C2A File Offset: 0x00020E2A
		public static bool IsAppEarlierThanWindowsPhoneMango
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00022C2D File Offset: 0x00020E2D
		public static bool IsNetFx40TimeSpanLegacyFormatMode
		{
			get
			{
				return CompatibilitySwitches.s_isNetFx40TimeSpanLegacyFormatMode;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00022C34 File Offset: 0x00020E34
		public static bool IsNetFx40LegacySecurityPolicy
		{
			get
			{
				return CompatibilitySwitches.s_isNetFx40LegacySecurityPolicy;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00022C3B File Offset: 0x00020E3B
		public static bool IsNetFx45LegacyManagedDeflateStream
		{
			get
			{
				return CompatibilitySwitches.s_isNetFx45LegacyManagedDeflateStream;
			}
		}

		// Token: 0x04000460 RID: 1120
		private static bool s_AreSwitchesSet;

		// Token: 0x04000461 RID: 1121
		private static bool s_isNetFx40TimeSpanLegacyFormatMode;

		// Token: 0x04000462 RID: 1122
		private static bool s_isNetFx40LegacySecurityPolicy;

		// Token: 0x04000463 RID: 1123
		private static bool s_isNetFx45LegacyManagedDeflateStream;
	}
}
