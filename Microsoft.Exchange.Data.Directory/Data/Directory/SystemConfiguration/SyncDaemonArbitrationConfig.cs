using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B3 RID: 1459
	[Serializable]
	public sealed class SyncDaemonArbitrationConfig : ADConfigurationObject
	{
		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x000FC6B9 File Offset: 0x000FA8B9
		internal override ADObjectSchema Schema
		{
			get
			{
				return SyncDaemonArbitrationConfig.schema;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x000FC6C0 File Offset: 0x000FA8C0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SyncDaemonArbitrationConfig.mostDerivedClass;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000FC6C7 File Offset: 0x000FA8C7
		// (set) Token: 0x06004327 RID: 17191 RVA: 0x000FC6DE File Offset: 0x000FA8DE
		[Parameter(Mandatory = false)]
		public Version MinVersion
		{
			get
			{
				return SyncDaemonArbitrationConfig.IntToVersion((int)this[SyncDaemonArbitrationConfigSchema.MinVersion]);
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.MinVersion] = SyncDaemonArbitrationConfig.VersionToInt(value);
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x06004329 RID: 17193 RVA: 0x000FC70D File Offset: 0x000FA90D
		[Parameter(Mandatory = false)]
		public Version MaxVersion
		{
			get
			{
				return SyncDaemonArbitrationConfig.IntToVersion((int)this[SyncDaemonArbitrationConfigSchema.MaxVersion]);
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.MaxVersion] = SyncDaemonArbitrationConfig.VersionToInt(value);
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x0600432A RID: 17194 RVA: 0x000FC725 File Offset: 0x000FA925
		// (set) Token: 0x0600432B RID: 17195 RVA: 0x000FC737 File Offset: 0x000FA937
		[Parameter(Mandatory = false)]
		public int ActiveInstanceSleepInterval
		{
			get
			{
				return (int)this[SyncDaemonArbitrationConfigSchema.ActiveInstanceSleepInterval];
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.ActiveInstanceSleepInterval] = value;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x0600432C RID: 17196 RVA: 0x000FC74A File Offset: 0x000FA94A
		// (set) Token: 0x0600432D RID: 17197 RVA: 0x000FC75C File Offset: 0x000FA95C
		[Parameter(Mandatory = false)]
		public int PassiveInstanceSleepInterval
		{
			get
			{
				return (int)this[SyncDaemonArbitrationConfigSchema.PassiveInstanceSleepInterval];
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.PassiveInstanceSleepInterval] = value;
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x000FC76F File Offset: 0x000FA96F
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x000FC781 File Offset: 0x000FA981
		[Parameter(Mandatory = false)]
		public bool IsEnabled
		{
			get
			{
				return (bool)this[SyncDaemonArbitrationConfigSchema.IsEnabled];
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.IsEnabled] = value;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x000FC794 File Offset: 0x000FA994
		// (set) Token: 0x06004331 RID: 17201 RVA: 0x000FC7A6 File Offset: 0x000FA9A6
		[Parameter(Mandatory = false)]
		public bool IsHalted
		{
			get
			{
				return (bool)this[SyncDaemonArbitrationConfigSchema.IsHalted];
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.IsHalted] = value;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x000FC7B9 File Offset: 0x000FA9B9
		// (set) Token: 0x06004333 RID: 17203 RVA: 0x000FC7CB File Offset: 0x000FA9CB
		[Parameter(Mandatory = false)]
		public bool IsHaltRecoveryDisabled
		{
			get
			{
				return (bool)this[SyncDaemonArbitrationConfigSchema.IsHaltRecoveryDisabled];
			}
			set
			{
				this[SyncDaemonArbitrationConfigSchema.IsHaltRecoveryDisabled] = value;
			}
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000FC7E0 File Offset: 0x000FA9E0
		private static Version IntToVersion(int value)
		{
			int revision = value & 255;
			int build = value >> 8 & 8191;
			int minor = value >> 21 & 15;
			int major = value >> 25 & 63;
			return new Version(major, minor, build, revision);
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000FC818 File Offset: 0x000FAA18
		private static int VersionToInt(Version value)
		{
			return (value.Revision & 255) | (value.Build & 8191) << 8 | (value.Minor & 15) << 21 | (value.Major & 63) << 25;
		}

		// Token: 0x04002D9A RID: 11674
		private static SyncDaemonArbitrationConfigSchema schema = ObjectSchema.GetInstance<SyncDaemonArbitrationConfigSchema>();

		// Token: 0x04002D9B RID: 11675
		private static string mostDerivedClass = "msExchSyncDaemonArbitrationConfig";
	}
}
