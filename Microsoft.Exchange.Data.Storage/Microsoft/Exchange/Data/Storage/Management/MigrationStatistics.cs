using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A39 RID: 2617
	[Serializable]
	public class MigrationStatistics : ConfigurableObject
	{
		// Token: 0x06005FF1 RID: 24561 RVA: 0x0019505C File Offset: 0x0019325C
		public MigrationStatistics() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06005FF2 RID: 24562 RVA: 0x00195069 File Offset: 0x00193269
		// (set) Token: 0x06005FF3 RID: 24563 RVA: 0x00195076 File Offset: 0x00193276
		public new MigrationStatisticsId Identity
		{
			get
			{
				return (MigrationStatisticsId)base.Identity;
			}
			internal set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x06005FF4 RID: 24564 RVA: 0x00195084 File Offset: 0x00193284
		// (set) Token: 0x06005FF5 RID: 24565 RVA: 0x00195096 File Offset: 0x00193296
		public int TotalCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.TotalCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.TotalCount] = value;
			}
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x06005FF6 RID: 24566 RVA: 0x001950A9 File Offset: 0x001932A9
		// (set) Token: 0x06005FF7 RID: 24567 RVA: 0x001950BB File Offset: 0x001932BB
		public int ActiveCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.ActiveCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.ActiveCount] = value;
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x06005FF8 RID: 24568 RVA: 0x001950CE File Offset: 0x001932CE
		// (set) Token: 0x06005FF9 RID: 24569 RVA: 0x001950E0 File Offset: 0x001932E0
		public int StoppedCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.StoppedCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.StoppedCount] = value;
			}
		}

		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x06005FFA RID: 24570 RVA: 0x001950F3 File Offset: 0x001932F3
		// (set) Token: 0x06005FFB RID: 24571 RVA: 0x00195105 File Offset: 0x00193305
		public int SyncedCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.SyncedCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.SyncedCount] = value;
			}
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x06005FFC RID: 24572 RVA: 0x00195118 File Offset: 0x00193318
		// (set) Token: 0x06005FFD RID: 24573 RVA: 0x0019512A File Offset: 0x0019332A
		public int FinalizedCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.FinalizedCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.FinalizedCount] = value;
			}
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x06005FFE RID: 24574 RVA: 0x0019513D File Offset: 0x0019333D
		// (set) Token: 0x06005FFF RID: 24575 RVA: 0x0019514F File Offset: 0x0019334F
		public int FailedCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.FailedCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.FailedCount] = value;
			}
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x06006000 RID: 24576 RVA: 0x00195162 File Offset: 0x00193362
		// (set) Token: 0x06006001 RID: 24577 RVA: 0x00195174 File Offset: 0x00193374
		public int PendingCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.PendingCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.PendingCount] = value;
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x06006002 RID: 24578 RVA: 0x00195187 File Offset: 0x00193387
		// (set) Token: 0x06006003 RID: 24579 RVA: 0x00195199 File Offset: 0x00193399
		public int ProvisionedCount
		{
			get
			{
				return (int)this[MigrationStatistics.MigrationStatisticsSchema.ProvisionedCount];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.ProvisionedCount] = value;
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x001951AC File Offset: 0x001933AC
		// (set) Token: 0x06006005 RID: 24581 RVA: 0x001951BE File Offset: 0x001933BE
		public MigrationType MigrationType
		{
			get
			{
				return (MigrationType)this[MigrationStatistics.MigrationStatisticsSchema.MigrationType];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.MigrationType] = (int)value;
			}
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x001951D1 File Offset: 0x001933D1
		// (set) Token: 0x06006007 RID: 24583 RVA: 0x001951E3 File Offset: 0x001933E3
		public string DiagnosticInfo
		{
			get
			{
				return (string)this[MigrationStatistics.MigrationStatisticsSchema.DiagnosticInfo];
			}
			internal set
			{
				this[MigrationStatistics.MigrationStatisticsSchema.DiagnosticInfo] = value;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x001951F1 File Offset: 0x001933F1
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MigrationStatistics.MigrationStatisticsSchema>();
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x001951F8 File Offset: 0x001933F8
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x02000A3A RID: 2618
		private class MigrationStatisticsSchema : SimpleProviderObjectSchema
		{
			// Token: 0x04003663 RID: 13923
			public static readonly ProviderPropertyDefinition TotalCount = new SimpleProviderPropertyDefinition("TotalCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003664 RID: 13924
			public static readonly ProviderPropertyDefinition ActiveCount = new SimpleProviderPropertyDefinition("ActiveCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003665 RID: 13925
			public static readonly ProviderPropertyDefinition StoppedCount = new SimpleProviderPropertyDefinition("StoppedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003666 RID: 13926
			public static readonly ProviderPropertyDefinition SyncedCount = new SimpleProviderPropertyDefinition("SyncedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003667 RID: 13927
			public static readonly ProviderPropertyDefinition FinalizedCount = new SimpleProviderPropertyDefinition("FinalizedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003668 RID: 13928
			public static readonly ProviderPropertyDefinition FailedCount = new SimpleProviderPropertyDefinition("FailedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003669 RID: 13929
			public static readonly ProviderPropertyDefinition PendingCount = new SimpleProviderPropertyDefinition("PendingCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400366A RID: 13930
			public static readonly ProviderPropertyDefinition ProvisionedCount = new SimpleProviderPropertyDefinition("ProvisionedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400366B RID: 13931
			public static readonly ProviderPropertyDefinition MigrationType = new SimpleProviderPropertyDefinition("MigrationType", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400366C RID: 13932
			public static readonly ProviderPropertyDefinition DiagnosticInfo = new SimpleProviderPropertyDefinition("DiagnosticInfo", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
