using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000085 RID: 133
	internal class RenderingFlags
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00021890 File Offset: 0x0001FA90
		public static bool EnableAllFolderNavigation(UserContext userContext)
		{
			return RenderingFlags.GetFlag(userContext, RenderingFlags.Flags.EnableAllFolderNavigation);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00021899 File Offset: 0x0001FA99
		public static bool HideOutOfOfficeInfoBar(UserContext userContext)
		{
			return RenderingFlags.GetFlag(userContext, RenderingFlags.Flags.HideOutOfOfficeInfobar);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000218A2 File Offset: 0x0001FAA2
		public static void EnableAllFolderNavigation(UserContext userContext, bool value)
		{
			RenderingFlags.SetFlag(userContext, RenderingFlags.Flags.EnableAllFolderNavigation, value);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000218AC File Offset: 0x0001FAAC
		public static void HideOutOfOfficeInfoBar(UserContext userContext, bool value)
		{
			RenderingFlags.SetFlag(userContext, RenderingFlags.Flags.HideOutOfOfficeInfobar, value);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000218B6 File Offset: 0x0001FAB6
		private static bool GetFlag(UserContext userContext, RenderingFlags.Flags flag)
		{
			return (userContext.RenderingFlags & (int)flag) != 0;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000218C6 File Offset: 0x0001FAC6
		private static void SetFlag(UserContext userContext, RenderingFlags.Flags flag, bool value)
		{
			if (value)
			{
				userContext.RenderingFlags |= (int)flag;
				return;
			}
			userContext.RenderingFlags &= (int)(~(int)flag);
		}

		// Token: 0x02000086 RID: 134
		[Flags]
		private enum Flags
		{
			// Token: 0x040002EA RID: 746
			EnableAllFolderNavigation = 1,
			// Token: 0x040002EB RID: 747
			HideOutOfOfficeInfobar = 2
		}
	}
}
