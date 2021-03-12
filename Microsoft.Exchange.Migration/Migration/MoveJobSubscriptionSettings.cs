using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B0 RID: 176
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MoveJobSubscriptionSettings : JobSubscriptionSettingsBase
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x00028D42 File Offset: 0x00026F42
		public MoveJobSubscriptionSettings(bool isLocalMove)
		{
			this.isLocalMove = isLocalMove;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00028D51 File Offset: 0x00026F51
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00028D59 File Offset: 0x00026F59
		public string[] TargetDatabases { get; private set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00028D62 File Offset: 0x00026F62
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00028D6A File Offset: 0x00026F6A
		public string[] TargetArchiveDatabases { get; private set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00028D73 File Offset: 0x00026F73
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00028D7B File Offset: 0x00026F7B
		public string TargetDeliveryDomain { get; private set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00028D84 File Offset: 0x00026F84
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00028D8C File Offset: 0x00026F8C
		public Unlimited<int>? BadItemLimit { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00028D95 File Offset: 0x00026F95
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00028D9D File Offset: 0x00026F9D
		public Unlimited<int>? LargeItemLimit { get; private set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00028DA6 File Offset: 0x00026FA6
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00028DAE File Offset: 0x00026FAE
		public bool? PrimaryOnly { get; private set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00028DB7 File Offset: 0x00026FB7
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00028DBF File Offset: 0x00026FBF
		public bool? ArchiveOnly { get; private set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00028DC8 File Offset: 0x00026FC8
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00028DD0 File Offset: 0x00026FD0
		public ExDateTime? StartAfter { get; private set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00028DD9 File Offset: 0x00026FD9
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00028DE1 File Offset: 0x00026FE1
		public ExDateTime? CompleteAfter { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00028DEA File Offset: 0x00026FEA
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MoveJobSubscriptionSettings.MoveJobSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00028DF4 File Offset: 0x00026FF4
		protected override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && this.BadItemLimit == null && this.LargeItemLimit == null && this.TargetDatabases == null && this.TargetArchiveDatabases == null && this.PrimaryOnly == null && this.ArchiveOnly == null;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00028E5C File Offset: 0x0002705C
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteOrDeleteNullableProperty<string[]>(message, MigrationBatchMessageSchema.MigrationJobTargetDatabase, this.TargetDatabases);
			MigrationHelper.WriteOrDeleteNullableProperty<string[]>(message, MigrationBatchMessageSchema.MigrationJobTargetArchiveDatabase, this.TargetArchiveDatabases);
			MigrationHelper.WriteOrDeleteNullableProperty<string>(message, MigrationBatchMessageSchema.MigrationJobTargetDeliveryDomain, this.TargetDeliveryDomain);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit, this.BadItemLimit);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit, this.LargeItemLimit);
			MigrationHelper.WriteOrDeleteNullableProperty<bool?>(message, MigrationBatchMessageSchema.MigrationJobPrimaryOnly, this.PrimaryOnly);
			MigrationHelper.WriteOrDeleteNullableProperty<bool?>(message, MigrationBatchMessageSchema.MigrationJobArchiveOnly, this.ArchiveOnly);
			MigrationHelper.WriteOrDeleteNullableProperty<ExDateTime?>(message, MigrationBatchMessageSchema.MigrationJobStartAfter, this.StartAfter);
			MigrationHelper.WriteOrDeleteNullableProperty<ExDateTime?>(message, MigrationBatchMessageSchema.MigrationJobCompleteAfter, this.CompleteAfter);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00028F0C File Offset: 0x0002710C
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.BadItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit);
			this.LargeItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit);
			this.TargetDatabases = message.GetValueOrDefault<string[]>(MigrationBatchMessageSchema.MigrationJobTargetDatabase, null);
			this.TargetArchiveDatabases = message.GetValueOrDefault<string[]>(MigrationBatchMessageSchema.MigrationJobTargetArchiveDatabase, null);
			this.PrimaryOnly = message.GetValueOrDefault<bool?>(MigrationBatchMessageSchema.MigrationJobPrimaryOnly, null);
			this.ArchiveOnly = message.GetValueOrDefault<bool?>(MigrationBatchMessageSchema.MigrationJobArchiveOnly, null);
			this.TargetDeliveryDomain = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobTargetDeliveryDomain, null);
			this.StartAfter = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobStartAfter);
			this.CompleteAfter = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobCompleteAfter);
			JobSubscriptionSettingsBase.ValidatePrimaryArchiveExclusivity(this.PrimaryOnly, this.ArchiveOnly);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00028FE0 File Offset: 0x000271E0
		public override void WriteToBatch(MigrationBatch batch)
		{
			batch.TargetDatabases = this.TargetDatabases;
			batch.TargetArchiveDatabases = this.TargetArchiveDatabases;
			batch.BadItemLimit = (this.BadItemLimit ?? Unlimited<int>.UnlimitedValue);
			batch.LargeItemLimit = (this.LargeItemLimit ?? Unlimited<int>.UnlimitedValue);
			batch.PrimaryOnly = this.PrimaryOnly;
			batch.ArchiveOnly = this.ArchiveOnly;
			batch.TargetDeliveryDomain = this.TargetDeliveryDomain;
			if (this.StartAfter != null)
			{
				batch.StartAfter = (DateTime?)MigrationHelper.GetLocalizedDateTime(this.StartAfter, batch.UserTimeZone.ExTimeZone);
				batch.StartAfterUTC = (DateTime?)MigrationHelper.GetUniversalDateTime(this.StartAfter);
			}
			if (this.CompleteAfter != null)
			{
				batch.CompleteAfter = (DateTime?)MigrationHelper.GetLocalizedDateTime(this.CompleteAfter, batch.UserTimeZone.ExTimeZone);
				batch.CompleteAfterUTC = (DateTime?)MigrationHelper.GetUniversalDateTime(this.CompleteAfter);
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002910C File Offset: 0x0002730C
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new object[]
			{
				new XElement("TargetDatabases", string.Join(",", this.TargetDatabases ?? new string[0])),
				new XElement("TargetArchiveDatabases", string.Join(",", this.TargetArchiveDatabases ?? new string[0])),
				new XElement("BadItemLimit", this.BadItemLimit),
				new XElement("LargeItemLimit", this.LargeItemLimit),
				new XElement("PrimaryOnly", this.PrimaryOnly),
				new XElement("ArchiveOnly", this.ArchiveOnly),
				new XElement("TargetDeliveryDomain", this.TargetDeliveryDomain),
				new XElement("StartAfter", this.StartAfter),
				new XElement("CompleteAfter", this.CompleteAfter)
			});
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00029248 File Offset: 0x00027448
		protected override void InitalizeFromBatch(MigrationBatch batch)
		{
			this.TargetDatabases = (batch.TargetDatabases ?? new string[0]).ToArray();
			this.TargetArchiveDatabases = (batch.TargetArchiveDatabases ?? new string[0]).ToArray();
			this.BadItemLimit = new Unlimited<int>?(batch.BadItemLimit);
			this.LargeItemLimit = (this.isLocalMove ? null : new Unlimited<int>?(batch.LargeItemLimit));
			this.PrimaryOnly = batch.PrimaryOnly;
			this.ArchiveOnly = batch.ArchiveOnly;
			this.TargetDeliveryDomain = batch.TargetDeliveryDomain;
			this.StartAfter = MigrationHelper.GetUniversalDateTime((ExDateTime?)batch.StartAfter);
			this.CompleteAfter = MigrationHelper.GetUniversalDateTime((ExDateTime?)batch.CompleteAfter);
			JobSubscriptionSettingsBase.ValidatePrimaryArchiveExclusivity(this.PrimaryOnly, this.ArchiveOnly);
		}

		// Token: 0x040003C3 RID: 963
		public static readonly PropertyDefinition[] MoveJobSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobTargetDatabase,
				MigrationBatchMessageSchema.MigrationJobTargetArchiveDatabase,
				MigrationBatchMessageSchema.MigrationJobBadItemLimit,
				MigrationBatchMessageSchema.MigrationJobLargeItemLimit,
				MigrationBatchMessageSchema.MigrationJobPrimaryOnly,
				MigrationBatchMessageSchema.MigrationJobArchiveOnly,
				MigrationBatchMessageSchema.MigrationJobTargetDeliveryDomain,
				MigrationBatchMessageSchema.MigrationJobStartAfter,
				MigrationBatchMessageSchema.MigrationJobCompleteAfter
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});

		// Token: 0x040003C4 RID: 964
		private readonly bool isLocalMove;
	}
}
