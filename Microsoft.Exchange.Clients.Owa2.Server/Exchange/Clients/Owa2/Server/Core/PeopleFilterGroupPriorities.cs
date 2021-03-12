using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000215 RID: 533
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class PeopleFilterGroupPriorities
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x00049116 File Offset: 0x00047316
		public static bool IsMyContactsFolder(int sortGroupPriority)
		{
			return sortGroupPriority == 1;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0004911C File Offset: 0x0004731C
		public static bool ShouldBeIncludedInMyContactsFolder(int sortGroupPriority)
		{
			return sortGroupPriority > 1 && sortGroupPriority < 10;
		}

		// Token: 0x04000B20 RID: 2848
		public const int CurrentVersion = 2;

		// Token: 0x04000B21 RID: 2849
		public const int MyContactsFolder = 1;

		// Token: 0x04000B22 RID: 2850
		public const int DefaultContactsFolder = 2;

		// Token: 0x04000B23 RID: 2851
		public const int NetworkFolder = 3;

		// Token: 0x04000B24 RID: 2852
		public const int QuickContactsFolder = 4;

		// Token: 0x04000B25 RID: 2853
		public const int UserCreatedFolder = 5;

		// Token: 0x04000B26 RID: 2854
		public const int OtherFolder = 10;

		// Token: 0x04000B27 RID: 2855
		public const int GlobalAddressList = 1000;

		// Token: 0x04000B28 RID: 2856
		public const int AllRoomsAddressList = 1001;

		// Token: 0x04000B29 RID: 2857
		public const int AllUsersAddressList = 1002;

		// Token: 0x04000B2A RID: 2858
		public const int AllDistributionListsAddressList = 1003;

		// Token: 0x04000B2B RID: 2859
		public const int AllContactsAddressList = 1004;

		// Token: 0x04000B2C RID: 2860
		public const int AllModernGroupsAddressList = 1009;

		// Token: 0x04000B2D RID: 2861
		public const int OtherAddressList = 1010;

		// Token: 0x04000B2E RID: 2862
		public const int PublicFolders = 2000;
	}
}
