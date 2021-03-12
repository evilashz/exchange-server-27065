using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C1 RID: 193
	internal class FolderRow
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x00039C04 File Offset: 0x00037E04
		public static FolderRow FromRawData(object[] rawData)
		{
			if (rawData == null)
			{
				throw new ArgumentNullException("rawData");
			}
			if (rawData.Length != FolderRow.CalendarSyncAssistantFolderProps.Length)
			{
				throw new ArgumentOutOfRangeException("rawData", string.Format("The raw data array needs to have {0} elements. The passed in array had {1}.", FolderRow.CalendarSyncAssistantFolderProps.Length, rawData.Length));
			}
			return new FolderRow(rawData);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00039C5C File Offset: 0x00037E5C
		public static FolderRow FromFolder(Folder folder)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			object[] rawData = new object[]
			{
				folder.Id,
				folder.ClassName,
				folder.TryGetProperty(FolderSchema.ExtendedFolderFlags),
				folder.LastSuccessfulSyncTime,
				folder.DisplayName,
				folder.ParentId,
				folder.LastAttemptedSyncTime
			};
			return FolderRow.FromRawData(rawData);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00039CD4 File Offset: 0x00037ED4
		public StoreObjectId FolderId
		{
			get
			{
				return FolderRow.ToStoreObjectId(this.RawData[0]);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00039CE3 File Offset: 0x00037EE3
		public StoreObjectId ParentItemId
		{
			get
			{
				return FolderRow.ToStoreObjectId(this.RawData[5]);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00039CF2 File Offset: 0x00037EF2
		public ExDateTime LastSuccessfulSyncTime
		{
			get
			{
				return FolderRow.ToExDateTime(this.RawData[3]);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00039D01 File Offset: 0x00037F01
		public ExDateTime LastAttemptedSyncTime
		{
			get
			{
				return FolderRow.ToExDateTime(this.RawData[6]);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00039D10 File Offset: 0x00037F10
		public string DisplayName
		{
			get
			{
				return FolderRow.ToStringOrEmpty(this.RawData[4]);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00039D1F File Offset: 0x00037F1F
		public object ExtendedFolderFlags
		{
			get
			{
				return this.RawData[2];
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00039D29 File Offset: 0x00037F29
		public string ContainerClass
		{
			get
			{
				return FolderRow.ToStringOrEmpty(this.RawData[1]);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00039D38 File Offset: 0x00037F38
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00039D40 File Offset: 0x00037F40
		public object[] RawData { get; set; }

		// Token: 0x06000825 RID: 2085 RVA: 0x00039D49 File Offset: 0x00037F49
		private static StoreObjectId ToStoreObjectId(object value)
		{
			return StoreId.GetStoreObjectId((StoreId)value);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00039D56 File Offset: 0x00037F56
		private FolderRow(object[] rawData)
		{
			this.RawData = rawData;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00039D68 File Offset: 0x00037F68
		private static string ToStringOrEmpty(object value)
		{
			string text = value as string;
			return text ?? string.Empty;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00039D88 File Offset: 0x00037F88
		private static ExDateTime ToExDateTime(object value)
		{
			ExDateTime result = ExDateTime.MinValue;
			if (value != null && value is ExDateTime)
			{
				result = (ExDateTime)value;
			}
			return result;
		}

		// Token: 0x040005C9 RID: 1481
		private const int FolderIdIdx = 0;

		// Token: 0x040005CA RID: 1482
		private const int ContainerClassIdx = 1;

		// Token: 0x040005CB RID: 1483
		private const int ExtendedFolderFlagsIdx = 2;

		// Token: 0x040005CC RID: 1484
		private const int SubscriptionLastSuccessfulSyncTimeIdx = 3;

		// Token: 0x040005CD RID: 1485
		private const int DisplayNameIdx = 4;

		// Token: 0x040005CE RID: 1486
		private const int ParentItemIdIdx = 5;

		// Token: 0x040005CF RID: 1487
		private const int SubscriptionLastAttemptedSyncTime = 6;

		// Token: 0x040005D0 RID: 1488
		public static readonly PropertyDefinition[] CalendarSyncAssistantFolderProps = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ContainerClass,
			FolderSchema.ExtendedFolderFlags,
			FolderSchema.SubscriptionLastSuccessfulSyncTime,
			FolderSchema.DisplayName,
			StoreObjectSchema.ParentItemId,
			FolderSchema.SubscriptionLastAttemptedSyncTime
		};
	}
}
