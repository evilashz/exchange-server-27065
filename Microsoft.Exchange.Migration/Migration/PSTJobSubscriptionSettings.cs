using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B3 RID: 179
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSTJobSubscriptionSettings : JobSubscriptionSettingsBase
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000299DE File Offset: 0x00027BDE
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x000299E6 File Offset: 0x00027BE6
		public Unlimited<int>? BadItemLimit { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000299EF File Offset: 0x00027BEF
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x000299F7 File Offset: 0x00027BF7
		public Unlimited<int>? LargeItemLimit { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00029A00 File Offset: 0x00027C00
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return PSTJobSubscriptionSettings.JobSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00029A07 File Offset: 0x00027C07
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit, this.BadItemLimit);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit, this.LargeItemLimit);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00029A33 File Offset: 0x00027C33
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.BadItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit);
			this.LargeItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00029A60 File Offset: 0x00027C60
		public override void WriteToBatch(MigrationBatch batch)
		{
			batch.BadItemLimit = (this.BadItemLimit ?? Unlimited<int>.UnlimitedValue);
			batch.LargeItemLimit = (this.LargeItemLimit ?? Unlimited<int>.UnlimitedValue);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00029AB5 File Offset: 0x00027CB5
		protected override void InitalizeFromBatch(MigrationBatch batch)
		{
			this.BadItemLimit = new Unlimited<int>?(batch.BadItemLimit);
			this.LargeItemLimit = new Unlimited<int>?(batch.LargeItemLimit);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00029ADC File Offset: 0x00027CDC
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new object[]
			{
				new XElement("BadItemLimit", this.BadItemLimit),
				new XElement("LargeItemLimit", this.LargeItemLimit)
			});
		}

		// Token: 0x040003DE RID: 990
		public static readonly PropertyDefinition[] JobSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobBadItemLimit,
				MigrationBatchMessageSchema.MigrationJobLargeItemLimit
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
