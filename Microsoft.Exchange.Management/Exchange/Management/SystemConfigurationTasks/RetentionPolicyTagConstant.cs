using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000318 RID: 792
	public class RetentionPolicyTagConstant
	{
		// Token: 0x04000B91 RID: 2961
		public static readonly string OneMonthDelete = "1 Month Delete";

		// Token: 0x04000B92 RID: 2962
		public static readonly string OneWeekDelete = "1 Week Delete";

		// Token: 0x04000B93 RID: 2963
		public static readonly string OneYearDelete = "1 Year Delete";

		// Token: 0x04000B94 RID: 2964
		public static readonly string FiveYearDelete = "5 Year Delete";

		// Token: 0x04000B95 RID: 2965
		public static readonly string SixMonthDelete = "6 Month Delete";

		// Token: 0x04000B96 RID: 2966
		public static readonly string DeletedItems = "Deleted Items";

		// Token: 0x04000B97 RID: 2967
		public static readonly string NeverDelete = "Never Delete";

		// Token: 0x04000B98 RID: 2968
		public static readonly string AutoGroup = "AutoGroup";

		// Token: 0x04000B99 RID: 2969
		public static readonly string ModeratedRecipients = "ModeratedRecipients";

		// Token: 0x04000B9A RID: 2970
		public static readonly string AsyncOperationNotification = "AsyncOperationNotification";

		// Token: 0x04000B9B RID: 2971
		public static readonly string DefaultTwoYearMoveToArchive = "Default 2 year move to archive";

		// Token: 0x04000B9C RID: 2972
		public static readonly string PersonalOneYearMoveToArchive = "Personal 1 year move to archive";

		// Token: 0x04000B9D RID: 2973
		public static readonly string PersonalFiveYearMoveToArchive = "Personal 5 year move to archive";

		// Token: 0x04000B9E RID: 2974
		public static readonly string PersonalNeverMoveToArchive = "Personal never move to archive";

		// Token: 0x04000B9F RID: 2975
		public static readonly string RecoverableItemsFourteenDaysMoveToArchive = "Recoverable Items 14 days move to archive";

		// Token: 0x04000BA0 RID: 2976
		public static readonly string JunkEmail = "Junk Email";

		// Token: 0x04000BA1 RID: 2977
		public static Dictionary<string, Guid> RetentionTagGuidMap = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase)
		{
			{
				RetentionPolicyTagConstant.OneMonthDelete,
				new Guid("fc92a703-51e3-411f-b850-4966668ee49c")
			},
			{
				RetentionPolicyTagConstant.OneWeekDelete,
				new Guid("2d26745c-8566-4dc3-85b7-a88f56443b7b")
			},
			{
				RetentionPolicyTagConstant.OneYearDelete,
				new Guid("9a3c36dd-c0eb-424a-a3ec-fd08bd17f855")
			},
			{
				RetentionPolicyTagConstant.FiveYearDelete,
				new Guid("1d128b50-14f2-4417-87a4-dbcb7d015fd7")
			},
			{
				RetentionPolicyTagConstant.SixMonthDelete,
				new Guid("d94993b5-e987-4275-8707-072057cfb2b8")
			},
			{
				RetentionPolicyTagConstant.DeletedItems,
				new Guid("05d4c642-352c-4228-ad32-6d4d1e9a77c1")
			},
			{
				RetentionPolicyTagConstant.NeverDelete,
				new Guid("414c6a14-3ed5-432e-9edb-c6620a8278f0")
			},
			{
				RetentionPolicyTagConstant.AutoGroup,
				new Guid("ccc2afb9-a91b-41ad-b4a4-f7b7752faf0b")
			},
			{
				RetentionPolicyTagConstant.ModeratedRecipients,
				new Guid("4a329da1-7757-4adb-bfb4-3b4a252311d9")
			},
			{
				RetentionPolicyTagConstant.AsyncOperationNotification,
				AsyncOperationNotificationDataProvider.NotifcationRetentionPolicyTagGuid
			},
			{
				RetentionPolicyTagConstant.DefaultTwoYearMoveToArchive,
				new Guid("9acbb441-faf8-40dd-b967-b3867bdf399c")
			},
			{
				RetentionPolicyTagConstant.PersonalOneYearMoveToArchive,
				new Guid("66904d6f-bf73-4460-9abe-8ebfad894a0f")
			},
			{
				RetentionPolicyTagConstant.PersonalFiveYearMoveToArchive,
				new Guid("790300f2-2ffd-4380-95d8-962daadc05c8")
			},
			{
				RetentionPolicyTagConstant.PersonalNeverMoveToArchive,
				new Guid("c6c9ad12-9e46-4b29-a053-e1ea189ab0cc")
			},
			{
				RetentionPolicyTagConstant.RecoverableItemsFourteenDaysMoveToArchive,
				new Guid("80feb2e9-8e6f-4f67-a006-c806ae585982")
			},
			{
				RetentionPolicyTagConstant.JunkEmail,
				new Guid("3dbb5f39-dc76-4916-9db3-fa9191760a55")
			}
		};
	}
}
