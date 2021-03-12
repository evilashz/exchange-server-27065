using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxLoadBalance.Providers
{
	// Token: 0x020000C6 RID: 198
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxTablePropertyDefinitions
	{
		// Token: 0x0400025F RID: 607
		public static readonly MapiPropertyDefinition ItemsPendingUpgrade = new MapiPropertyDefinition("ItemsPendingUpgrade", typeof(int), PropTag.ItemsPendingUpgrade, MapiPropertyDefinitionFlags.ReadOnly, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000260 RID: 608
		public static readonly MapiPropertyDefinition CreationTime = MapiPropertyDefinitions.CreationTime;

		// Token: 0x04000261 RID: 609
		public static readonly MapiPropertyDefinition MailboxGuid = MapiPropertyDefinitions.MailboxGuid;

		// Token: 0x04000262 RID: 610
		public static readonly MapiPropertyDefinition PersistableTenantPartitionHint = MapiPropertyDefinitions.PersistableTenantPartitionHint;

		// Token: 0x04000263 RID: 611
		public static readonly MapiPropertyDefinition MessageTableTotalSize = MapiPropertyDefinitions.MessageTableTotalSize;

		// Token: 0x04000264 RID: 612
		public static readonly MapiPropertyDefinition AttachmentTableTotalSize = MapiPropertyDefinitions.AttachmentTableTotalSize;

		// Token: 0x04000265 RID: 613
		public static readonly MapiPropertyDefinition OtherTablesTotalSize = MapiPropertyDefinitions.OtherTablesTotalSize;

		// Token: 0x04000266 RID: 614
		public static readonly MapiPropertyDefinition IsQuarantined = MapiPropertyDefinitions.IsQuarantined;

		// Token: 0x04000267 RID: 615
		public static readonly MapiPropertyDefinition MailboxMiscFlags = MapiPropertyDefinitions.MailboxMiscFlags;

		// Token: 0x04000268 RID: 616
		public static readonly MapiPropertyDefinition TotalItemSize = MapiPropertyDefinitions.TotalItemSize;

		// Token: 0x04000269 RID: 617
		public static readonly MapiPropertyDefinition TotalDeletedItemSize = MapiPropertyDefinitions.TotalDeletedItemSize;

		// Token: 0x0400026A RID: 618
		public static readonly MapiPropertyDefinition StoreMailboxType = MailboxStatisticsSchema.StoreMailboxType;

		// Token: 0x0400026B RID: 619
		public static readonly MapiPropertyDefinition ItemCount = MailboxStatisticsSchema.ItemCount;

		// Token: 0x0400026C RID: 620
		public static readonly MapiPropertyDefinition DeletedItemCount = MailboxStatisticsSchema.DeletedItemCount;

		// Token: 0x0400026D RID: 621
		public static readonly MapiPropertyDefinition LastLogonTime = MailboxStatisticsSchema.LastLogonTime;

		// Token: 0x0400026E RID: 622
		public static readonly MapiPropertyDefinition DisconnectDate = MailboxStatisticsSchema.DisconnectDate;

		// Token: 0x0400026F RID: 623
		internal static readonly PropTag[] MailboxTablePropertiesToLoad = (from prop in new MapiPropertyDefinition[]
		{
			MailboxTablePropertyDefinitions.MailboxGuid,
			MailboxTablePropertyDefinitions.PersistableTenantPartitionHint,
			MailboxTablePropertyDefinitions.MessageTableTotalSize,
			MailboxTablePropertyDefinitions.AttachmentTableTotalSize,
			MailboxTablePropertyDefinitions.OtherTablesTotalSize,
			MailboxTablePropertyDefinitions.IsQuarantined,
			MailboxTablePropertyDefinitions.MailboxMiscFlags,
			MailboxTablePropertyDefinitions.TotalItemSize,
			MailboxTablePropertyDefinitions.TotalDeletedItemSize,
			MailboxTablePropertyDefinitions.StoreMailboxType,
			MailboxTablePropertyDefinitions.ItemCount,
			MailboxTablePropertyDefinitions.DeletedItemCount,
			MailboxTablePropertyDefinitions.LastLogonTime,
			MailboxTablePropertyDefinitions.DisconnectDate,
			MailboxTablePropertyDefinitions.CreationTime,
			MailboxTablePropertyDefinitions.ItemsPendingUpgrade
		}
		select prop.PropertyTag).ToArray<PropTag>();
	}
}
