using System;
using System.Configuration;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Servicelets.BatchCreator
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BatchCreatorConfig : AnchorConfig
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000028B5 File Offset: 0x00000AB5
		public BatchCreatorConfig() : base("BatchCreator")
		{
			if (!CommonUtils.IsMultiTenantEnabled())
			{
				base.SetDefaultConfigValue<bool>("IsEnabled", false);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000028D5 File Offset: 0x00000AD5
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000028E2 File Offset: 0x00000AE2
		[ConfigurationProperty("OccupantTypes", DefaultValue = MigrationOccupantType.Regular)]
		public MigrationOccupantType OccupantTypes
		{
			get
			{
				return this.InternalGetConfig<MigrationOccupantType>("OccupantTypes");
			}
			set
			{
				this.InternalSetConfig<MigrationOccupantType>(value, "OccupantTypes");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000028F0 File Offset: 0x00000AF0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000028FD File Offset: 0x00000AFD
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MigrationMailboxRefreshInterval", DefaultValue = "04:00:00")]
		public TimeSpan MigrationMailboxRefreshInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationMailboxRefreshInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationMailboxRefreshInterval");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000290B File Offset: 0x00000B0B
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002918 File Offset: 0x00000B18
		[ConfigurationProperty("RunInterval", DefaultValue = "04:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		public TimeSpan RunInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("RunInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "RunInterval");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002926 File Offset: 0x00000B26
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002933 File Offset: 0x00000B33
		[ConfigurationProperty("MailboxRunLimit", DefaultValue = 5000)]
		public int MigrationMailboxRunLimit
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationMailboxRunLimit");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationMailboxRunLimit");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002941 File Offset: 0x00000B41
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000294E File Offset: 0x00000B4E
		[ConfigurationProperty("ConstraintId")]
		public int? ConstraintId
		{
			get
			{
				return this.InternalGetConfig<int?>("ConstraintId");
			}
			set
			{
				this.InternalSetConfig<int?>(value, "ConstraintId");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000295C File Offset: 0x00000B5C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002969 File Offset: 0x00000B69
		[ConfigurationProperty("MaximumTotalMailboxSize")]
		public int? MaximumTotalMailboxSize
		{
			get
			{
				return this.InternalGetConfig<int?>("MaximumTotalMailboxSize");
			}
			set
			{
				this.InternalSetConfig<int?>(value, "MaximumTotalMailboxSize");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002977 File Offset: 0x00000B77
		protected override ExchangeConfigurationSection ScopeSchema
		{
			get
			{
				return BatchCreatorConfig.scopeSchema;
			}
		}

		// Token: 0x0400000F RID: 15
		private static readonly BatchCreatorConfig.BatchCreatorScopeSchema scopeSchema = new BatchCreatorConfig.BatchCreatorScopeSchema();

		// Token: 0x02000008 RID: 8
		public new static class Setting
		{
			// Token: 0x04000010 RID: 16
			public const string MigrationMailboxRefreshInterval = "MigrationMailboxRefreshInterval";

			// Token: 0x04000011 RID: 17
			public const string OccupantTypes = "OccupantTypes";

			// Token: 0x04000012 RID: 18
			public const string MailboxRunLimit = "MailboxRunLimit";

			// Token: 0x04000013 RID: 19
			public const string RunInterval = "RunInterval";

			// Token: 0x04000014 RID: 20
			public const string ConstraintId = "ConstraintId";

			// Token: 0x04000015 RID: 21
			public const string MaximumTotalMailboxSize = "MaximumTotalMailboxSize";
		}

		// Token: 0x02000009 RID: 9
		[Serializable]
		public static class Scope
		{
			// Token: 0x04000016 RID: 22
			public const string MigrationOccupantType = "MigrationOccupantType";
		}

		// Token: 0x0200000A RID: 10
		private class BatchCreatorScopeSchema : ExchangeConfigurationSection
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600002D RID: 45 RVA: 0x0000298A File Offset: 0x00000B8A
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002997 File Offset: 0x00000B97
			[ConfigurationProperty("MigrationOccupantType", DefaultValue = MigrationOccupantType.Regular)]
			public MigrationOccupantType MigrationOccupantType
			{
				get
				{
					return this.InternalGetConfig<MigrationOccupantType>("MigrationOccupantType");
				}
				set
				{
					this.InternalSetConfig<MigrationOccupantType>(value, "MigrationOccupantType");
				}
			}
		}
	}
}
