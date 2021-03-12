using System;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A21 RID: 2593
	[Serializable]
	public class MigrationConfig : ConfigurableObject
	{
		// Token: 0x06005F38 RID: 24376 RVA: 0x00192D15 File Offset: 0x00190F15
		public MigrationConfig() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x00192D22 File Offset: 0x00190F22
		// (set) Token: 0x06005F3A RID: 24378 RVA: 0x00192D2F File Offset: 0x00190F2F
		public new MigrationConfigId Identity
		{
			get
			{
				return (MigrationConfigId)base.Identity;
			}
			internal set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x00192D3D File Offset: 0x00190F3D
		// (set) Token: 0x06005F3C RID: 24380 RVA: 0x00192D4F File Offset: 0x00190F4F
		public int MaxNumberOfBatches
		{
			get
			{
				return (int)this[MigrationConfig.MigrationConfigSchema.MaxNumberOfBatches];
			}
			internal set
			{
				this[MigrationConfig.MigrationConfigSchema.MaxNumberOfBatches] = value;
			}
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06005F3D RID: 24381 RVA: 0x00192D62 File Offset: 0x00190F62
		// (set) Token: 0x06005F3E RID: 24382 RVA: 0x00192D74 File Offset: 0x00190F74
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)this[MigrationConfig.MigrationConfigSchema.MaxConcurrentMigrations];
			}
			internal set
			{
				this[MigrationConfig.MigrationConfigSchema.MaxConcurrentMigrations] = value;
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x00192D87 File Offset: 0x00190F87
		// (set) Token: 0x06005F40 RID: 24384 RVA: 0x00192D99 File Offset: 0x00190F99
		public MigrationFeature Features
		{
			get
			{
				return (MigrationFeature)this[MigrationConfig.MigrationConfigSchema.MigrationFeatures];
			}
			internal set
			{
				this[MigrationConfig.MigrationConfigSchema.MigrationFeatures] = value;
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x00192DAC File Offset: 0x00190FAC
		// (set) Token: 0x06005F42 RID: 24386 RVA: 0x00192DBE File Offset: 0x00190FBE
		public bool CanSubmitNewBatch
		{
			get
			{
				return (bool)this[MigrationConfig.MigrationConfigSchema.CanSubmitNewBatch];
			}
			internal set
			{
				this[MigrationConfig.MigrationConfigSchema.CanSubmitNewBatch] = value;
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x00192DD1 File Offset: 0x00190FD1
		// (set) Token: 0x06005F44 RID: 24388 RVA: 0x00192DE3 File Offset: 0x00190FE3
		public bool SupportsCutover
		{
			get
			{
				return (bool)this[MigrationConfig.MigrationConfigSchema.SupportsCutover];
			}
			internal set
			{
				this[MigrationConfig.MigrationConfigSchema.SupportsCutover] = value;
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06005F45 RID: 24389 RVA: 0x00192DF6 File Offset: 0x00190FF6
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MigrationConfig.schema;
			}
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x06005F46 RID: 24390 RVA: 0x00192DFD File Offset: 0x00190FFD
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x00192E04 File Offset: 0x00191004
		public override string ToString()
		{
			return Strings.MigrationConfigString(this.MaxNumberOfBatches.ToString(), this.Features.ToString());
		}

		// Token: 0x040035F2 RID: 13810
		private static ObjectSchema schema = ObjectSchema.GetInstance<MigrationConfig.MigrationConfigSchema>();

		// Token: 0x02000A22 RID: 2594
		private class MigrationConfigSchema : SimpleProviderObjectSchema
		{
			// Token: 0x040035F3 RID: 13811
			public static readonly SimpleProviderPropertyDefinition MaxNumberOfBatches = new SimpleProviderPropertyDefinition("MaxNumberOfBatches", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 100, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040035F4 RID: 13812
			public static readonly SimpleProviderPropertyDefinition MigrationFeatures = new SimpleProviderPropertyDefinition("MigrationFeatures", ExchangeObjectVersion.Exchange2010, typeof(MigrationFeature), PropertyDefinitionFlags.None, MigrationFeature.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040035F5 RID: 13813
			public static readonly ProviderPropertyDefinition CanSubmitNewBatch = new SimpleProviderPropertyDefinition("CanSubmitNewBatch", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040035F6 RID: 13814
			public static readonly ProviderPropertyDefinition SupportsCutover = new SimpleProviderPropertyDefinition("SupportsCutover", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040035F7 RID: 13815
			public static readonly ProviderPropertyDefinition MaxConcurrentMigrations = new SimpleProviderPropertyDefinition("MaxConcurrentMigrations", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.TaskPopulated, Unlimited<int>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
