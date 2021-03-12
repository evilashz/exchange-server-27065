using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Reflection;

namespace Microsoft.Exchange.Data.Directory.Permission
{
	// Token: 0x020001D5 RID: 469
	internal static class RecipientPermissionHelper
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0005B57F File Offset: 0x0005977F
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x0005B586 File Offset: 0x00059786
		private static Dictionary<RecipientAccessRight, Guid> RecipientAccessRightGuidMap { get; set; } = new Dictionary<RecipientAccessRight, Guid>();

		// Token: 0x060012FE RID: 4862 RVA: 0x0005B590 File Offset: 0x00059790
		static RecipientPermissionHelper()
		{
			foreach (FieldInfo fieldInfo in typeof(RecipientAccessRight).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField))
			{
				RightGuidAttribute[] array = (RightGuidAttribute[])fieldInfo.GetCustomAttributes(typeof(RightGuidAttribute), false);
				RecipientAccessRight key = (RecipientAccessRight)fieldInfo.GetValue(null);
				RecipientPermissionHelper.RecipientAccessRightGuidMap[key] = array[0].Guid;
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0005B60A File Offset: 0x0005980A
		internal static Guid GetRecipientAccessRightGuid(RecipientAccessRight right)
		{
			return RecipientPermissionHelper.RecipientAccessRightGuidMap[right];
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0005B618 File Offset: 0x00059818
		internal static RecipientAccessRight? GetRecipientAccessRight(ActiveDirectoryAccessRule ace)
		{
			if ((ace.ActiveDirectoryRights & ActiveDirectoryRights.ExtendedRight) == ActiveDirectoryRights.ExtendedRight)
			{
				foreach (RecipientAccessRight recipientAccessRight in RecipientPermissionHelper.RecipientAccessRightGuidMap.Keys)
				{
					if (ace.ObjectType == RecipientPermissionHelper.RecipientAccessRightGuidMap[recipientAccessRight])
					{
						return new RecipientAccessRight?(recipientAccessRight);
					}
				}
			}
			return null;
		}
	}
}
