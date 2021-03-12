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
	// Token: 0x02000099 RID: 153
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeJobSubscriptionSettings : JobSubscriptionSettingsBase
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00026340 File Offset: 0x00024540
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return ExchangeJobSubscriptionSettings.ExchangeJobSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00026347 File Offset: 0x00024547
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0002634F File Offset: 0x0002454F
		public ExDateTime? StartAfter { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00026358 File Offset: 0x00024558
		protected override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && this.StartAfter == null;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00026380 File Offset: 0x00024580
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteOrDeleteNullableProperty<ExDateTime?>(message, MigrationBatchMessageSchema.MigrationJobStartAfter, this.StartAfter);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002639C File Offset: 0x0002459C
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.StartAfter = message.GetValueOrDefault<ExDateTime?>(MigrationBatchMessageSchema.MigrationJobStartAfter, null);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000263CA File Offset: 0x000245CA
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new XElement("StartAfter", this.StartAfter));
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000263EC File Offset: 0x000245EC
		public override void WriteToBatch(MigrationBatch batch)
		{
			if (this.StartAfter != null)
			{
				batch.StartAfter = (DateTime?)MigrationHelper.GetLocalizedDateTime(this.StartAfter, batch.UserTimeZone.ExTimeZone);
				batch.StartAfterUTC = (DateTime?)MigrationHelper.GetUniversalDateTime(this.StartAfter);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00026440 File Offset: 0x00024640
		protected override void InitalizeFromBatch(MigrationBatch batch)
		{
			this.StartAfter = MigrationHelper.GetUniversalDateTime((ExDateTime?)batch.StartAfter);
		}

		// Token: 0x04000360 RID: 864
		public static readonly PropertyDefinition[] ExchangeJobSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobStartAfter
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
