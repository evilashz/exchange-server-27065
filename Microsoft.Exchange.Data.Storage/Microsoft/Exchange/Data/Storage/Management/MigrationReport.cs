using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A31 RID: 2609
	[Serializable]
	public class MigrationReport : ConfigurableObject
	{
		// Token: 0x06005FC2 RID: 24514 RVA: 0x001949BC File Offset: 0x00192BBC
		public MigrationReport() : base(new SimplePropertyBag(MigrationReport.MigrationReportSchema.Identity, MigrationReport.MigrationReportSchema.ObjectState, MigrationReport.MigrationReportSchema.ExchangeVersion))
		{
			base.ResetChangeTracking();
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x001949DE File Offset: 0x00192BDE
		// (set) Token: 0x06005FC4 RID: 24516 RVA: 0x001949EB File Offset: 0x00192BEB
		public new MigrationReportId Identity
		{
			get
			{
				return (MigrationReportId)base.Identity;
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.Identity] = value;
			}
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x06005FC5 RID: 24517 RVA: 0x001949F9 File Offset: 0x00192BF9
		// (set) Token: 0x06005FC6 RID: 24518 RVA: 0x00194A0B File Offset: 0x00192C0B
		public string ReportName
		{
			get
			{
				return (string)this[MigrationReport.MigrationReportSchema.MigrationReportName];
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.MigrationReportName] = value;
			}
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x06005FC7 RID: 24519 RVA: 0x00194A19 File Offset: 0x00192C19
		// (set) Token: 0x06005FC8 RID: 24520 RVA: 0x00194A2B File Offset: 0x00192C2B
		public Guid JobId
		{
			get
			{
				return (Guid)this[MigrationReport.MigrationReportSchema.MigrationReportId];
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.MigrationReportId] = value;
			}
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x00194A3E File Offset: 0x00192C3E
		// (set) Token: 0x06005FCA RID: 24522 RVA: 0x00194A50 File Offset: 0x00192C50
		public MigrationType MigrationType
		{
			get
			{
				return (MigrationType)this[MigrationReport.MigrationReportSchema.MigrationType];
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.MigrationType] = (int)value;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x00194A63 File Offset: 0x00192C63
		// (set) Token: 0x06005FCC RID: 24524 RVA: 0x00194A75 File Offset: 0x00192C75
		public MigrationReportType ReportType
		{
			get
			{
				return (MigrationReportType)this[MigrationReport.MigrationReportSchema.MigrationReportType];
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.MigrationReportType] = (int)value;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06005FCD RID: 24525 RVA: 0x00194A88 File Offset: 0x00192C88
		// (set) Token: 0x06005FCE RID: 24526 RVA: 0x00194A9A File Offset: 0x00192C9A
		public bool IsStagedMigration
		{
			get
			{
				return (bool)this[MigrationReport.MigrationReportSchema.IsStagedMigration];
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.IsStagedMigration] = value;
			}
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06005FCF RID: 24527 RVA: 0x00194AAD File Offset: 0x00192CAD
		// (set) Token: 0x06005FD0 RID: 24528 RVA: 0x00194ABF File Offset: 0x00192CBF
		public MultiValuedProperty<string> Rows
		{
			get
			{
				return (MultiValuedProperty<string>)this[MigrationReport.MigrationReportSchema.MigrationReportRows];
			}
			internal set
			{
				this[MigrationReport.MigrationReportSchema.MigrationReportRows] = value;
			}
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x00194ACD File Offset: 0x00192CCD
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MigrationReport.MigrationReportSchema>();
			}
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x00194AD4 File Offset: 0x00192CD4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x02000A32 RID: 2610
		private class MigrationReportSchema : ObjectSchema
		{
			// Token: 0x04003640 RID: 13888
			public static readonly ProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(MigrationReportId), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003641 RID: 13889
			public static readonly ProviderPropertyDefinition ObjectState = UserConfigurationObjectSchema.ObjectState;

			// Token: 0x04003642 RID: 13890
			public static readonly ProviderPropertyDefinition ExchangeVersion = UserConfigurationObjectSchema.ExchangeVersion;

			// Token: 0x04003643 RID: 13891
			public static readonly ProviderPropertyDefinition MigrationReportName = new SimpleProviderPropertyDefinition("MigrationReportName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003644 RID: 13892
			public static readonly ProviderPropertyDefinition MigrationReportId = new SimpleProviderPropertyDefinition("MigrationReportId", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.TaskPopulated, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003645 RID: 13893
			public static readonly ProviderPropertyDefinition MigrationType = new SimpleProviderPropertyDefinition("MigrationType", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003646 RID: 13894
			public static readonly ProviderPropertyDefinition IsStagedMigration = new SimpleProviderPropertyDefinition("IsStagedMigration", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003647 RID: 13895
			public static readonly ProviderPropertyDefinition MigrationReportType = new SimpleProviderPropertyDefinition("MigrationReportType", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003648 RID: 13896
			public static readonly ProviderPropertyDefinition MigrationReportRows = new SimpleProviderPropertyDefinition("MigrationReportRows", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
