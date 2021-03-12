using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B5 RID: 181
	internal class PublicFolderJobSubscriptionSettings : JobSubscriptionSettingsBase
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00029CD8 File Offset: 0x00027ED8
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00029CE0 File Offset: 0x00027EE0
		public Unlimited<int>? BadItemLimit { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00029CE9 File Offset: 0x00027EE9
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00029CF1 File Offset: 0x00027EF1
		public Unlimited<int>? LargeItemLimit { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00029CFA File Offset: 0x00027EFA
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00029D02 File Offset: 0x00027F02
		public string SourcePublicFolderDatabase { get; private set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00029D0B File Offset: 0x00027F0B
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return PublicFolderJobSubscriptionSettings.DefaultPropertyDefinitions;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00029D14 File Offset: 0x00027F14
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new object[]
			{
				new XElement("BadItemLimit", this.BadItemLimit),
				new XElement("LargeItemLimit", this.LargeItemLimit),
				new XElement("SourcePublicFolderDatabase", this.SourcePublicFolderDatabase)
			});
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00029D81 File Offset: 0x00027F81
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit, this.BadItemLimit);
			MigrationHelper.WriteUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit, this.LargeItemLimit);
			MigrationHelper.WriteOrDeleteNullableProperty<string>(message, MigrationBatchMessageSchema.MigrationJobSourcePublicFolderDatabase, this.SourcePublicFolderDatabase);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00029DBE File Offset: 0x00027FBE
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.BadItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobBadItemLimit);
			this.LargeItemLimit = MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationJobLargeItemLimit);
			this.SourcePublicFolderDatabase = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobSourcePublicFolderDatabase, null);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00029DFC File Offset: 0x00027FFC
		public override void WriteToBatch(MigrationBatch batch)
		{
			batch.BadItemLimit = (this.BadItemLimit ?? Unlimited<int>.UnlimitedValue);
			batch.LargeItemLimit = (this.LargeItemLimit ?? Unlimited<int>.UnlimitedValue);
			batch.SourcePublicFolderDatabase = this.SourcePublicFolderDatabase;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00029E5D File Offset: 0x0002805D
		protected override void InitalizeFromBatch(MigrationBatch batch)
		{
			this.BadItemLimit = new Unlimited<int>?(batch.BadItemLimit);
			this.LargeItemLimit = new Unlimited<int>?(batch.LargeItemLimit);
			this.SourcePublicFolderDatabase = batch.SourcePublicFolderDatabase;
		}

		// Token: 0x040003E7 RID: 999
		public static readonly PropertyDefinition[] DefaultPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions,
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobBadItemLimit,
				MigrationBatchMessageSchema.MigrationJobLargeItemLimit,
				MigrationBatchMessageSchema.MigrationJobSourcePublicFolderDatabase
			}
		});
	}
}
