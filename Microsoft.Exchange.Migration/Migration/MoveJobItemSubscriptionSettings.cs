using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MoveJobItemSubscriptionSettings : JobItemSubscriptionSettingsBase
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00028829 File Offset: 0x00026A29
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00028831 File Offset: 0x00026A31
		public string TargetDatabase { get; private set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002883A File Offset: 0x00026A3A
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00028842 File Offset: 0x00026A42
		public string TargetArchiveDatabase { get; private set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0002884B File Offset: 0x00026A4B
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00028853 File Offset: 0x00026A53
		public Unlimited<int>? BadItemLimit { get; private set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0002885C File Offset: 0x00026A5C
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00028864 File Offset: 0x00026A64
		public Unlimited<int>? LargeItemLimit { get; private set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0002886D File Offset: 0x00026A6D
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00028875 File Offset: 0x00026A75
		public bool? PrimaryOnly { get; private set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0002887E File Offset: 0x00026A7E
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00028886 File Offset: 0x00026A86
		public bool? ArchiveOnly { get; private set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0002888F File Offset: 0x00026A8F
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MoveJobItemSubscriptionSettings.MoveJobItemSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00028898 File Offset: 0x00026A98
		protected override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && this.BadItemLimit == null && this.LargeItemLimit == null && this.TargetDatabase == null && this.TargetArchiveDatabase == null && this.PrimaryOnly == null && this.ArchiveOnly == null;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00028900 File Offset: 0x00026B00
		public override JobItemSubscriptionSettingsBase Clone()
		{
			return new MoveJobItemSubscriptionSettings
			{
				TargetDatabase = this.TargetDatabase,
				TargetArchiveDatabase = this.TargetArchiveDatabase,
				BadItemLimit = this.BadItemLimit,
				LargeItemLimit = this.LargeItemLimit,
				PrimaryOnly = this.PrimaryOnly,
				ArchiveOnly = this.ArchiveOnly,
				LastModifiedTime = base.LastModifiedTime
			};
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00028968 File Offset: 0x00026B68
		public override void UpdateFromDataRow(IMigrationDataRow request)
		{
			bool flag = false;
			MoveMigrationDataRow moveMigrationDataRow = request as MoveMigrationDataRow;
			if (moveMigrationDataRow == null)
			{
				throw new ArgumentException("expected a MoveMigrationDataRow", "request");
			}
			if (!object.Equals(this.TargetDatabase, moveMigrationDataRow.TargetDatabase))
			{
				this.TargetDatabase = moveMigrationDataRow.TargetDatabase;
				flag = true;
			}
			if (!object.Equals(this.TargetArchiveDatabase, moveMigrationDataRow.TargetArchiveDatabase))
			{
				this.TargetArchiveDatabase = moveMigrationDataRow.TargetArchiveDatabase;
				flag = true;
			}
			if (!object.Equals(this.BadItemLimit, moveMigrationDataRow.BadItemLimit))
			{
				this.BadItemLimit = moveMigrationDataRow.BadItemLimit;
				flag = true;
			}
			if (!object.Equals(this.LargeItemLimit, moveMigrationDataRow.LargeItemLimit))
			{
				this.LargeItemLimit = moveMigrationDataRow.LargeItemLimit;
				flag = true;
			}
			if (!object.Equals(this.PrimaryOnly, moveMigrationDataRow.PrimaryOnly) || !object.Equals(this.ArchiveOnly, moveMigrationDataRow.ArchiveOnly))
			{
				this.PrimaryOnly = moveMigrationDataRow.PrimaryOnly;
				this.ArchiveOnly = moveMigrationDataRow.ArchiveOnly;
				JobSubscriptionSettingsBase.ValidatePrimaryArchiveExclusivity(this.PrimaryOnly, this.ArchiveOnly);
				flag = true;
			}
			if (flag || base.LastModifiedTime == ExDateTime.MinValue)
			{
				base.LastModifiedTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00028AB0 File Offset: 0x00026CB0
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteOrDeleteNullableProperty<string[]>(message, MigrationBatchMessageSchema.MigrationJobTargetDatabase, (this.TargetDatabase == null) ? null : new string[]
			{
				this.TargetDatabase
			});
			MigrationHelper.WriteOrDeleteNullableProperty<string[]>(message, MigrationBatchMessageSchema.MigrationJobTargetArchiveDatabase, (this.TargetArchiveDatabase == null) ? null : new string[]
			{
				this.TargetArchiveDatabase
			});
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit, this.BadItemLimit);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit, this.LargeItemLimit);
			MigrationHelper.WriteOrDeleteNullableProperty<bool?>(message, MigrationBatchMessageSchema.MigrationJobPrimaryOnly, this.PrimaryOnly);
			MigrationHelper.WriteOrDeleteNullableProperty<bool?>(message, MigrationBatchMessageSchema.MigrationJobArchiveOnly, this.ArchiveOnly);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00028B58 File Offset: 0x00026D58
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.BadItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit);
			this.LargeItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit);
			string[] valueOrDefault = message.GetValueOrDefault<string[]>(MigrationBatchMessageSchema.MigrationJobTargetDatabase, null);
			this.TargetDatabase = ((valueOrDefault == null || valueOrDefault.Length == 0) ? null : valueOrDefault[0]);
			string[] valueOrDefault2 = message.GetValueOrDefault<string[]>(MigrationBatchMessageSchema.MigrationJobTargetArchiveDatabase, null);
			this.TargetArchiveDatabase = ((valueOrDefault2 == null || valueOrDefault2.Length == 0) ? null : valueOrDefault2[0]);
			this.PrimaryOnly = message.GetValueOrDefault<bool?>(MigrationBatchMessageSchema.MigrationJobPrimaryOnly, null);
			this.ArchiveOnly = message.GetValueOrDefault<bool?>(MigrationBatchMessageSchema.MigrationJobArchiveOnly, null);
			JobSubscriptionSettingsBase.ValidatePrimaryArchiveExclusivity(this.PrimaryOnly, this.ArchiveOnly);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00028C18 File Offset: 0x00026E18
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new object[]
			{
				new XElement("TargetDatabase", this.TargetDatabase),
				new XElement("TargetArchiveDatabase", this.TargetArchiveDatabase),
				new XElement("BadItemLimit", this.BadItemLimit),
				new XElement("LargeItemLimit", this.LargeItemLimit),
				new XElement("PrimaryOnly", this.PrimaryOnly),
				new XElement("ArchiveOnly", this.ArchiveOnly)
			});
		}

		// Token: 0x040003BC RID: 956
		public static readonly PropertyDefinition[] MoveJobItemSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobTargetDatabase,
				MigrationBatchMessageSchema.MigrationJobTargetArchiveDatabase,
				MigrationBatchMessageSchema.MigrationJobBadItemLimit,
				MigrationBatchMessageSchema.MigrationJobLargeItemLimit,
				MigrationBatchMessageSchema.MigrationJobPrimaryOnly,
				MigrationBatchMessageSchema.MigrationJobArchiveOnly,
				MigrationBatchMessageSchema.MigrationJobTargetDeliveryDomain
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
