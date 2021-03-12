using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IMAPPAWJobSubscriptionSettings : IMAPJobSubscriptionSettings
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002843A File Offset: 0x0002663A
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00028442 File Offset: 0x00026642
		public ExDateTime? StartAfter { get; private set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002844B File Offset: 0x0002664B
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00028453 File Offset: 0x00026653
		public ExDateTime? CompleteAfter { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0002845C File Offset: 0x0002665C
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return IMAPPAWJobSubscriptionSettings.IMAPJobSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00028463 File Offset: 0x00026663
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteOrDeleteNullableProperty<ExDateTime?>(message, MigrationBatchMessageSchema.MigrationJobStartAfter, this.StartAfter);
			MigrationHelper.WriteOrDeleteNullableProperty<ExDateTime?>(message, MigrationBatchMessageSchema.MigrationJobCompleteAfter, this.CompleteAfter);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002848F File Offset: 0x0002668F
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.StartAfter = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobStartAfter);
			this.CompleteAfter = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobCompleteAfter);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000284BC File Offset: 0x000266BC
		public override void WriteToBatch(MigrationBatch batch)
		{
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
			base.WriteToBatch(batch);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002855E File Offset: 0x0002675E
		protected override void InitalizeFromBatch(MigrationBatch batch)
		{
			base.InitalizeFromBatch(batch);
			this.StartAfter = MigrationHelper.GetUniversalDateTime((ExDateTime?)batch.StartAfter);
			this.CompleteAfter = MigrationHelper.GetUniversalDateTime((ExDateTime?)batch.CompleteAfter);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00028594 File Offset: 0x00026794
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			base.AddDiagnosticInfoToElement(dataProvider, parent, argument);
			parent.Add(new object[]
			{
				new XElement("StartAfter", this.StartAfter),
				new XElement("CompleteAfter", this.CompleteAfter)
			});
		}

		// Token: 0x040003B3 RID: 947
		public static readonly PropertyDefinition[] IMAPJobSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobStartAfter,
				MigrationBatchMessageSchema.MigrationJobCompleteAfter
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
