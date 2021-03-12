using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000BF RID: 191
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ContactUtils
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x000219A8 File Offset: 0x0001FBA8
		public static FileAsMapping GetDefaultFileAs(int lcid)
		{
			FileAsMapping result;
			if (lcid == 5124 || lcid == 1028 || lcid == 2052 || lcid == 3076 || lcid == 4100 || lcid == 1042)
			{
				result = FileAsMapping.LastFirst;
			}
			else if (lcid == 1041 || lcid == 1037 || lcid == 1038 || lcid == 1086 || lcid == 1058 || lcid == 1066)
			{
				result = FileAsMapping.LastSpaceFirst;
			}
			else
			{
				result = FileAsMapping.LastCommaFirst;
			}
			return result;
		}
	}
}
