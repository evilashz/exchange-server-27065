using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADDatabaseAvailabilityGroupWrapper : ADObjectWrapperBase, IADDatabaseAvailabilityGroup, IADObjectCommon
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000427C File Offset: 0x0000247C
		private ADDatabaseAvailabilityGroupWrapper(DatabaseAvailabilityGroup dag) : base(dag)
		{
			this.DatacenterActivationMode = (DatacenterActivationModeOption)dag[DatabaseAvailabilityGroupSchema.DataCenterActivationMode];
			this.ThirdPartyReplication = (ThirdPartyReplicationMode)dag[DatabaseAvailabilityGroupSchema.ThirdPartyReplication];
			this.Servers = (MultiValuedProperty<ADObjectId>)dag[DatabaseAvailabilityGroupSchema.Servers];
			this.StoppedMailboxServers = (MultiValuedProperty<string>)dag[DatabaseAvailabilityGroupSchema.StoppedMailboxServers];
			this.StartedMailboxServers = (MultiValuedProperty<string>)dag[DatabaseAvailabilityGroupSchema.StartedMailboxServers];
			this.AutoDagVolumesRootFolderPath = (NonRootLocalLongFullPath)dag[DatabaseAvailabilityGroupSchema.AutoDagVolumesRootFolderPath];
			this.AutoDagDatabasesRootFolderPath = (NonRootLocalLongFullPath)dag[DatabaseAvailabilityGroupSchema.AutoDagDatabasesRootFolderPath];
			this.AutoDagDatabaseCopiesPerVolume = (int)dag[DatabaseAvailabilityGroupSchema.AutoDagDatabaseCopiesPerVolume];
			this.AutoDagDatabaseCopiesPerDatabase = (int)dag[DatabaseAvailabilityGroupSchema.AutoDagDatabaseCopiesPerDatabase];
			this.AutoDagTotalNumberOfDatabases = (int)dag[DatabaseAvailabilityGroupSchema.AutoDagTotalNumberOfDatabases];
			this.AutoDagTotalNumberOfServers = (int)dag[DatabaseAvailabilityGroupSchema.AutoDagTotalNumberOfServers];
			this.ReplayLagManagerEnabled = (bool)dag[DatabaseAvailabilityGroupSchema.ReplayLagManagerEnabled];
			this.AutoDagAutoReseedEnabled = (bool)dag[DatabaseAvailabilityGroupSchema.AutoDagAutoReseedEnabled];
			this.AutoDagDiskReclaimerEnabled = (bool)dag[DatabaseAvailabilityGroupSchema.AutoDagDiskReclaimerEnabled];
			this.AutoDagBitlockerEnabled = (bool)dag[DatabaseAvailabilityGroupSchema.AutoDagBitlockerEnabled];
			this.AutoDagFIPSCompliant = (bool)dag[DatabaseAvailabilityGroupSchema.AutoDagFIPSCompliant];
			this.AllowCrossSiteRpcClientAccess = ((int)dag[DatabaseAvailabilityGroupSchema.AllowCrossSiteRpcClientAccess] != 0);
			this.ReplicationPort = dag.ReplicationPort;
			long networkSettings = (long)dag[DatabaseAvailabilityGroupSchema.NetworkSettings];
			DatabaseAvailabilityGroup.NetworkOption networkCompression;
			DatabaseAvailabilityGroup.NetworkOption networkEncryption;
			bool manualDagNetworkConfiguration;
			DatabaseAvailabilityGroup.DecodeNetworkSettings(networkSettings, out networkCompression, out networkEncryption, out manualDagNetworkConfiguration);
			this.NetworkCompression = networkCompression;
			this.NetworkEncryption = networkEncryption;
			this.ManualDagNetworkConfiguration = manualDagNetworkConfiguration;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000444A File Offset: 0x0000264A
		public static ADDatabaseAvailabilityGroupWrapper CreateWrapper(DatabaseAvailabilityGroup dag)
		{
			if (dag == null)
			{
				return null;
			}
			return new ADDatabaseAvailabilityGroupWrapper(dag);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004457 File Offset: 0x00002657
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000445F File Offset: 0x0000265F
		public DatacenterActivationModeOption DatacenterActivationMode { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004468 File Offset: 0x00002668
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00004470 File Offset: 0x00002670
		public ThirdPartyReplicationMode ThirdPartyReplication { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004479 File Offset: 0x00002679
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00004481 File Offset: 0x00002681
		public MultiValuedProperty<string> StartedMailboxServerFqdns { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000448A File Offset: 0x0000268A
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004492 File Offset: 0x00002692
		public MultiValuedProperty<ADObjectId> Servers { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000449B File Offset: 0x0000269B
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000044A3 File Offset: 0x000026A3
		public NonRootLocalLongFullPath AutoDagVolumesRootFolderPath { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000044AC File Offset: 0x000026AC
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000044B4 File Offset: 0x000026B4
		public NonRootLocalLongFullPath AutoDagDatabasesRootFolderPath { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000044BD File Offset: 0x000026BD
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000044C5 File Offset: 0x000026C5
		public int AutoDagDatabaseCopiesPerVolume { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000044CE File Offset: 0x000026CE
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000044D6 File Offset: 0x000026D6
		public int AutoDagDatabaseCopiesPerDatabase { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000044DF File Offset: 0x000026DF
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000044E7 File Offset: 0x000026E7
		public int AutoDagTotalNumberOfDatabases { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000044F0 File Offset: 0x000026F0
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000044F8 File Offset: 0x000026F8
		public int AutoDagTotalNumberOfServers { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004501 File Offset: 0x00002701
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004509 File Offset: 0x00002709
		public bool ReplayLagManagerEnabled { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004512 File Offset: 0x00002712
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000451A File Offset: 0x0000271A
		public bool AutoDagAutoReseedEnabled { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004523 File Offset: 0x00002723
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000452B File Offset: 0x0000272B
		public bool AutoDagDiskReclaimerEnabled { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004534 File Offset: 0x00002734
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000453C File Offset: 0x0000273C
		public bool AutoDagBitlockerEnabled { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004545 File Offset: 0x00002745
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000454D File Offset: 0x0000274D
		public bool AutoDagFIPSCompliant { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004556 File Offset: 0x00002756
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000455E File Offset: 0x0000275E
		public MultiValuedProperty<string> StoppedMailboxServers { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004567 File Offset: 0x00002767
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000456F File Offset: 0x0000276F
		public MultiValuedProperty<string> StartedMailboxServers { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00004578 File Offset: 0x00002778
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00004580 File Offset: 0x00002780
		public bool AllowCrossSiteRpcClientAccess { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00004589 File Offset: 0x00002789
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00004591 File Offset: 0x00002791
		public bool ManualDagNetworkConfiguration { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000459A File Offset: 0x0000279A
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000045A2 File Offset: 0x000027A2
		public ushort ReplicationPort { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000045AB File Offset: 0x000027AB
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000045B3 File Offset: 0x000027B3
		public DatabaseAvailabilityGroup.NetworkOption NetworkCompression { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000045BC File Offset: 0x000027BC
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000045C4 File Offset: 0x000027C4
		public DatabaseAvailabilityGroup.NetworkOption NetworkEncryption { get; private set; }
	}
}
