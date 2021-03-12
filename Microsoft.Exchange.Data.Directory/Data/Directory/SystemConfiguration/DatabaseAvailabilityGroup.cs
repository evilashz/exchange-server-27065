using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E1 RID: 993
	[Serializable]
	public class DatabaseAvailabilityGroup : ADConfigurationObject
	{
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000BA81B File Offset: 0x000B8A1B
		internal override ADObjectSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					this.schema = ObjectSchema.GetInstance<DatabaseAvailabilityGroupSchema>();
				}
				return this.schema;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000BA836 File Offset: 0x000B8A36
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DatabaseAvailabilityGroup.mostDerivedClass;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000BA83D File Offset: 0x000B8A3D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000BA844 File Offset: 0x000B8A44
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x000BA856 File Offset: 0x000B8A56
		public new string Name
		{
			get
			{
				return (string)this[DatabaseAvailabilityGroupSchema.Name];
			}
			internal set
			{
				this[DatabaseAvailabilityGroupSchema.Name] = value;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000BA864 File Offset: 0x000B8A64
		public MultiValuedProperty<ADObjectId> Servers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[DatabaseAvailabilityGroupSchema.Servers];
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000BA876 File Offset: 0x000B8A76
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x000BA898 File Offset: 0x000B8A98
		public FileShareWitnessServerName WitnessServer
		{
			get
			{
				if (this.FileShareWitness != null)
				{
					return FileShareWitnessServerName.Parse(this.FileShareWitness.ServerName);
				}
				return null;
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.WitnessServer] = UncFileSharePath.Parse("\\\\" + value.RawData + "\\TempShare");
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000BA8BF File Offset: 0x000B8ABF
		internal UncFileSharePath FileShareWitness
		{
			get
			{
				if (this[DatabaseAvailabilityGroupSchema.WitnessServer] != null)
				{
					return (UncFileSharePath)this[DatabaseAvailabilityGroupSchema.WitnessServer];
				}
				return null;
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000BA8E0 File Offset: 0x000B8AE0
		internal static long EncodeNetworkSettings(DatabaseAvailabilityGroup.NetworkOption compress, DatabaseAvailabilityGroup.NetworkOption encrypt, bool manualNetConfig)
		{
			long num = (long)compress;
			long num2 = (long)encrypt << 4;
			long num3 = (manualNetConfig ? 1L : 0L) << 8;
			return num3 | num2 | num;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000BA908 File Offset: 0x000B8B08
		internal static void DecodeNetworkSettings(long networkSettings, out DatabaseAvailabilityGroup.NetworkOption compress, out DatabaseAvailabilityGroup.NetworkOption encrypt, out bool manualNetConfig)
		{
			int adVal = (int)(networkSettings & 15L);
			compress = DatabaseAvailabilityGroup.ConvertToNetworkOption(adVal);
			int adVal2 = (int)(networkSettings & 240L) >> 4;
			encrypt = DatabaseAvailabilityGroup.ConvertToNetworkOption(adVal2);
			if ((networkSettings & 256L) != 0L)
			{
				manualNetConfig = true;
				return;
			}
			manualNetConfig = false;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000BA94B File Offset: 0x000B8B4B
		internal void SetWitnessServer(UncFileSharePath witnessServer, NonRootLocalLongFullPath witnessDirectory)
		{
			this[DatabaseAvailabilityGroupSchema.WitnessServer] = witnessServer;
			this[DatabaseAvailabilityGroupSchema.WitnessDirectory] = witnessDirectory;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000BA965 File Offset: 0x000B8B65
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x000BA977 File Offset: 0x000B8B77
		public NonRootLocalLongFullPath WitnessDirectory
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseAvailabilityGroupSchema.WitnessDirectory];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.WitnessDirectory] = value;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000BA985 File Offset: 0x000B8B85
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x000BA9A7 File Offset: 0x000B8BA7
		public FileShareWitnessServerName AlternateWitnessServer
		{
			get
			{
				if (this.AlternateFileShareWitness != null)
				{
					return FileShareWitnessServerName.Parse(this.AlternateFileShareWitness.ServerName);
				}
				return null;
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AlternateWitnessServer] = UncFileSharePath.Parse("\\\\" + value.RawData + "\\TempShare");
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000BA9CE File Offset: 0x000B8BCE
		internal UncFileSharePath AlternateFileShareWitness
		{
			get
			{
				if (this[DatabaseAvailabilityGroupSchema.AlternateWitnessServer] != null)
				{
					return (UncFileSharePath)this[DatabaseAvailabilityGroupSchema.AlternateWitnessServer];
				}
				return null;
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000BA9EF File Offset: 0x000B8BEF
		internal void SetAlternateWitnessServer(UncFileSharePath alternateWitnessServer, NonRootLocalLongFullPath alternateWitnessDirectory)
		{
			this[DatabaseAvailabilityGroupSchema.AlternateWitnessServer] = alternateWitnessServer;
			this[DatabaseAvailabilityGroupSchema.AlternateWitnessDirectory] = alternateWitnessDirectory;
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000BAA09 File Offset: 0x000B8C09
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x000BAA1B File Offset: 0x000B8C1B
		public NonRootLocalLongFullPath AlternateWitnessDirectory
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseAvailabilityGroupSchema.AlternateWitnessDirectory];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AlternateWitnessDirectory] = value;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000BAA29 File Offset: 0x000B8C29
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000BAA3B File Offset: 0x000B8C3B
		internal long NetworkSettings
		{
			get
			{
				return (long)this[DatabaseAvailabilityGroupSchema.NetworkSettings];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.NetworkSettings] = value;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000BAA50 File Offset: 0x000B8C50
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x000BAA70 File Offset: 0x000B8C70
		public DatabaseAvailabilityGroup.NetworkOption NetworkCompression
		{
			get
			{
				DatabaseAvailabilityGroup.NetworkOption result;
				DatabaseAvailabilityGroup.NetworkOption networkOption;
				bool flag;
				DatabaseAvailabilityGroup.DecodeNetworkSettings(this.NetworkSettings, out result, out networkOption, out flag);
				return result;
			}
			set
			{
				DatabaseAvailabilityGroup.NetworkOption networkOption;
				DatabaseAvailabilityGroup.NetworkOption encrypt;
				bool manualNetConfig;
				DatabaseAvailabilityGroup.DecodeNetworkSettings(this.NetworkSettings, out networkOption, out encrypt, out manualNetConfig);
				long networkSettings = DatabaseAvailabilityGroup.EncodeNetworkSettings(value, encrypt, manualNetConfig);
				this.NetworkSettings = networkSettings;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000BAAA0 File Offset: 0x000B8CA0
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x000BAAC0 File Offset: 0x000B8CC0
		public DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
		{
			get
			{
				DatabaseAvailabilityGroup.NetworkOption networkOption;
				DatabaseAvailabilityGroup.NetworkOption result;
				bool flag;
				DatabaseAvailabilityGroup.DecodeNetworkSettings(this.NetworkSettings, out networkOption, out result, out flag);
				return result;
			}
			set
			{
				DatabaseAvailabilityGroup.NetworkOption compress;
				DatabaseAvailabilityGroup.NetworkOption networkOption;
				bool manualNetConfig;
				DatabaseAvailabilityGroup.DecodeNetworkSettings(this.NetworkSettings, out compress, out networkOption, out manualNetConfig);
				long networkSettings = DatabaseAvailabilityGroup.EncodeNetworkSettings(compress, value, manualNetConfig);
				this.NetworkSettings = networkSettings;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000BAAF0 File Offset: 0x000B8CF0
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x000BAB10 File Offset: 0x000B8D10
		public bool ManualDagNetworkConfiguration
		{
			get
			{
				DatabaseAvailabilityGroup.NetworkOption networkOption;
				DatabaseAvailabilityGroup.NetworkOption networkOption2;
				bool result;
				DatabaseAvailabilityGroup.DecodeNetworkSettings(this.NetworkSettings, out networkOption, out networkOption2, out result);
				return result;
			}
			set
			{
				DatabaseAvailabilityGroup.NetworkOption compress;
				DatabaseAvailabilityGroup.NetworkOption encrypt;
				bool flag;
				DatabaseAvailabilityGroup.DecodeNetworkSettings(this.NetworkSettings, out compress, out encrypt, out flag);
				long networkSettings = DatabaseAvailabilityGroup.EncodeNetworkSettings(compress, encrypt, value);
				this.NetworkSettings = networkSettings;
			}
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000BAB3E File Offset: 0x000B8D3E
		private static DatabaseAvailabilityGroup.NetworkOption ConvertToNetworkOption(int adVal)
		{
			if (adVal < 0 || adVal > 3)
			{
				return DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;
			}
			return (DatabaseAvailabilityGroup.NetworkOption)adVal;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000BAB4C File Offset: 0x000B8D4C
		internal bool IsDagEmpty()
		{
			using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = this.Servers.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ADObjectId adobjectId = enumerator.Current;
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000BABA0 File Offset: 0x000B8DA0
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x000BABB2 File Offset: 0x000B8DB2
		public DatacenterActivationModeOption DatacenterActivationMode
		{
			get
			{
				return (DatacenterActivationModeOption)this[DatabaseAvailabilityGroupSchema.DataCenterActivationMode];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.DataCenterActivationMode] = value;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000BABC5 File Offset: 0x000B8DC5
		// (set) Token: 0x06002D81 RID: 11649 RVA: 0x000BABD7 File Offset: 0x000B8DD7
		public MultiValuedProperty<string> StoppedMailboxServers
		{
			get
			{
				return (MultiValuedProperty<string>)this[DatabaseAvailabilityGroupSchema.StoppedMailboxServers];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.StoppedMailboxServers] = value;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000BABE5 File Offset: 0x000B8DE5
		// (set) Token: 0x06002D83 RID: 11651 RVA: 0x000BABF7 File Offset: 0x000B8DF7
		public MultiValuedProperty<string> StartedMailboxServers
		{
			get
			{
				return (MultiValuedProperty<string>)this[DatabaseAvailabilityGroupSchema.StartedMailboxServers];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.StartedMailboxServers] = value;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000BAC05 File Offset: 0x000B8E05
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x000BAC17 File Offset: 0x000B8E17
		public MultiValuedProperty<IPAddress> DatabaseAvailabilityGroupIpv4Addresses
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[DatabaseAvailabilityGroupSchema.DatabaseAvailabilityGroupIpv4Addresses];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.DatabaseAvailabilityGroupIpv4Addresses] = value;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000BAC25 File Offset: 0x000B8E25
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x000BAC2D File Offset: 0x000B8E2D
		public MultiValuedProperty<IPAddress> DatabaseAvailabilityGroupIpAddresses
		{
			get
			{
				return this.DatabaseAvailabilityGroupIpv4Addresses;
			}
			set
			{
				this.DatabaseAvailabilityGroupIpv4Addresses = value;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000BAC36 File Offset: 0x000B8E36
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000BAC4E File Offset: 0x000B8E4E
		public bool AllowCrossSiteRpcClientAccess
		{
			get
			{
				return (int)this[DatabaseAvailabilityGroupSchema.AllowCrossSiteRpcClientAccess] != 0;
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AllowCrossSiteRpcClientAccess] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000BAC67 File Offset: 0x000B8E67
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x000BAC6F File Offset: 0x000B8E6F
		public ADObjectId[] OperationalServers
		{
			get
			{
				return this.m_operationalServers;
			}
			internal set
			{
				this.m_operationalServers = value;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000BAC78 File Offset: 0x000B8E78
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x000BAC80 File Offset: 0x000B8E80
		public ADObjectId PrimaryActiveManager
		{
			get
			{
				return this.m_primaryActiveManager;
			}
			internal set
			{
				this.m_primaryActiveManager = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000BAC89 File Offset: 0x000B8E89
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x000BAC91 File Offset: 0x000B8E91
		public ADObjectId[] ServersInMaintenance
		{
			get
			{
				return this.m_serversInMaintenance;
			}
			internal set
			{
				this.m_serversInMaintenance = value;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000BAC9A File Offset: 0x000B8E9A
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000BACA2 File Offset: 0x000B8EA2
		public DeferredFailoverEntry[] ServersInDeferredRecovery
		{
			get
			{
				return this.m_serversInDeferredRecovery;
			}
			internal set
			{
				this.m_serversInDeferredRecovery = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000BACAB File Offset: 0x000B8EAB
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x000BACBD File Offset: 0x000B8EBD
		public ThirdPartyReplicationMode ThirdPartyReplication
		{
			get
			{
				return (ThirdPartyReplicationMode)this[DatabaseAvailabilityGroupSchema.ThirdPartyReplication];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.ThirdPartyReplication] = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000BACD0 File Offset: 0x000B8ED0
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000BACE3 File Offset: 0x000B8EE3
		public ushort ReplicationPort
		{
			get
			{
				return (ushort)((int)this[DatabaseAvailabilityGroupSchema.ReplicationPort]);
			}
			internal set
			{
				this[DatabaseAvailabilityGroupSchema.ReplicationPort] = (int)value;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000BACF6 File Offset: 0x000B8EF6
		public List<string> NetworkNames
		{
			get
			{
				return this.m_networkNames;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000BACFE File Offset: 0x000B8EFE
		// (set) Token: 0x06002D98 RID: 11672 RVA: 0x000BAD06 File Offset: 0x000B8F06
		public WitnessShareUsage? WitnessShareInUse
		{
			get
			{
				return this.m_witnessShareInUse;
			}
			internal set
			{
				this.m_witnessShareInUse = value;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000BAD0F File Offset: 0x000B8F0F
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x000BAD21 File Offset: 0x000B8F21
		public ADObjectId DatabaseAvailabilityGroupConfiguration
		{
			get
			{
				return (ADObjectId)this[DatabaseAvailabilityGroupSchema.DatabaseAvailabilityGroupConfiguration];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.DatabaseAvailabilityGroupConfiguration] = value;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000BAD2F File Offset: 0x000B8F2F
		// (set) Token: 0x06002D9C RID: 11676 RVA: 0x000BAD41 File Offset: 0x000B8F41
		internal DagAutoDagFlags AutoDagFlags
		{
			get
			{
				return (DagAutoDagFlags)this[DatabaseAvailabilityGroupSchema.AutoDagFlags];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagFlags] = value;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x000BAD54 File Offset: 0x000B8F54
		// (set) Token: 0x06002D9E RID: 11678 RVA: 0x000BAD66 File Offset: 0x000B8F66
		public Version AutoDagSchemaVersion
		{
			get
			{
				return (Version)this[DatabaseAvailabilityGroupSchema.AutoDagSchemaVersion];
			}
			internal set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagSchemaVersion] = value;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000BAD74 File Offset: 0x000B8F74
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x000BAD86 File Offset: 0x000B8F86
		[Parameter(Mandatory = false)]
		public int AutoDagDatabaseCopiesPerDatabase
		{
			get
			{
				return (int)this[DatabaseAvailabilityGroupSchema.AutoDagDatabaseCopiesPerDatabase];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagDatabaseCopiesPerDatabase] = value;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000BAD99 File Offset: 0x000B8F99
		// (set) Token: 0x06002DA2 RID: 11682 RVA: 0x000BADAB File Offset: 0x000B8FAB
		[Parameter(Mandatory = false)]
		public int AutoDagDatabaseCopiesPerVolume
		{
			get
			{
				return (int)this[DatabaseAvailabilityGroupSchema.AutoDagDatabaseCopiesPerVolume];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagDatabaseCopiesPerVolume] = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x000BADBE File Offset: 0x000B8FBE
		// (set) Token: 0x06002DA4 RID: 11684 RVA: 0x000BADD0 File Offset: 0x000B8FD0
		[Parameter(Mandatory = false)]
		public int AutoDagTotalNumberOfDatabases
		{
			get
			{
				return (int)this[DatabaseAvailabilityGroupSchema.AutoDagTotalNumberOfDatabases];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagTotalNumberOfDatabases] = value;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000BADE3 File Offset: 0x000B8FE3
		// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x000BADF5 File Offset: 0x000B8FF5
		[Parameter(Mandatory = false)]
		public int AutoDagTotalNumberOfServers
		{
			get
			{
				return (int)this[DatabaseAvailabilityGroupSchema.AutoDagTotalNumberOfServers];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagTotalNumberOfServers] = value;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000BAE08 File Offset: 0x000B9008
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x000BAE1A File Offset: 0x000B901A
		[Parameter(Mandatory = false)]
		public NonRootLocalLongFullPath AutoDagDatabasesRootFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseAvailabilityGroupSchema.AutoDagDatabasesRootFolderPath];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagDatabasesRootFolderPath] = value;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000BAE28 File Offset: 0x000B9028
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x000BAE3A File Offset: 0x000B903A
		[Parameter(Mandatory = false)]
		public NonRootLocalLongFullPath AutoDagVolumesRootFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseAvailabilityGroupSchema.AutoDagVolumesRootFolderPath];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagVolumesRootFolderPath] = value;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x000BAE48 File Offset: 0x000B9048
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x000BAE5A File Offset: 0x000B905A
		[Parameter(Mandatory = false)]
		public bool AutoDagAllServersInstalled
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.AutoDagAllServersInstalled];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagAllServersInstalled] = value;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000BAE6D File Offset: 0x000B906D
		// (set) Token: 0x06002DAE RID: 11694 RVA: 0x000BAE7F File Offset: 0x000B907F
		[Parameter(Mandatory = false)]
		public bool AutoDagAutoReseedEnabled
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.AutoDagAutoReseedEnabled];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagAutoReseedEnabled] = value;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000BAE92 File Offset: 0x000B9092
		// (set) Token: 0x06002DB0 RID: 11696 RVA: 0x000BAEA4 File Offset: 0x000B90A4
		[Parameter(Mandatory = false)]
		public bool AutoDagDiskReclaimerEnabled
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.AutoDagDiskReclaimerEnabled];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagDiskReclaimerEnabled] = value;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000BAEB7 File Offset: 0x000B90B7
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000BAEC9 File Offset: 0x000B90C9
		[Parameter(Mandatory = false)]
		public bool AutoDagBitlockerEnabled
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.AutoDagBitlockerEnabled];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagBitlockerEnabled] = value;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000BAEDC File Offset: 0x000B90DC
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000BAEEE File Offset: 0x000B90EE
		[Parameter(Mandatory = false)]
		public bool AutoDagFIPSCompliant
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.AutoDagFIPSCompliant];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.AutoDagFIPSCompliant] = value;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000BAF01 File Offset: 0x000B9101
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x000BAF13 File Offset: 0x000B9113
		[Parameter(Mandatory = false)]
		public bool ReplayLagManagerEnabled
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.ReplayLagManagerEnabled];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.ReplayLagManagerEnabled] = value;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000BAF26 File Offset: 0x000B9126
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x000BAF38 File Offset: 0x000B9138
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize
		{
			get
			{
				return (ByteQuantifiedSize?)this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceMaximumEdbFileSize];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceMaximumEdbFileSize] = value;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000BAF4B File Offset: 0x000B914B
		// (set) Token: 0x06002DBA RID: 11706 RVA: 0x000BAF5D File Offset: 0x000B915D
		[Parameter(Mandatory = false)]
		public int? MailboxLoadBalanceRelativeLoadCapacity
		{
			get
			{
				return (int?)this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceRelativeLoadCapacity];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceRelativeLoadCapacity] = value;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000BAF70 File Offset: 0x000B9170
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x000BAF82 File Offset: 0x000B9182
		[Parameter(Mandatory = false)]
		public int? MailboxLoadBalanceOverloadedThreshold
		{
			get
			{
				return (int?)this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceOverloadedThreshold];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceOverloadedThreshold] = value;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000BAF95 File Offset: 0x000B9195
		// (set) Token: 0x06002DBE RID: 11710 RVA: 0x000BAFA7 File Offset: 0x000B91A7
		[Parameter(Mandatory = false)]
		public int? MailboxLoadBalanceUnderloadedThreshold
		{
			get
			{
				return (int?)this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceUnderloadedThreshold];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceUnderloadedThreshold] = value;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000BAFBA File Offset: 0x000B91BA
		// (set) Token: 0x06002DC0 RID: 11712 RVA: 0x000BAFCC File Offset: 0x000B91CC
		[Parameter(Mandatory = false)]
		public bool MailboxLoadBalanceEnabled
		{
			get
			{
				return (bool)this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceEnabled];
			}
			set
			{
				this[DatabaseAvailabilityGroupSchema.MailboxLoadBalanceEnabled] = value;
			}
		}

		// Token: 0x04001E9E RID: 7838
		public const ushort DefaultReplicationPort = 64327;

		// Token: 0x04001E9F RID: 7839
		public const DatabaseAvailabilityGroup.NetworkOption DefaultNetworkOption = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x04001EA0 RID: 7840
		public const DatacenterActivationModeOption DefaultDatacenterActivationMode = DatacenterActivationModeOption.Off;

		// Token: 0x04001EA1 RID: 7841
		private static string mostDerivedClass = "msExchMDBAvailabilityGroup";

		// Token: 0x04001EA2 RID: 7842
		public static readonly NonRootLocalLongFullPath DefaultAutoDagDatabasesRootFolderPath = NonRootLocalLongFullPath.Parse("C:\\ExchangeDatabases");

		// Token: 0x04001EA3 RID: 7843
		public static readonly NonRootLocalLongFullPath DefaultAutoDagVolumesRootFolderPath = NonRootLocalLongFullPath.Parse("C:\\ExchangeVolumes");

		// Token: 0x04001EA4 RID: 7844
		[NonSerialized]
		private DatabaseAvailabilityGroupSchema schema;

		// Token: 0x04001EA5 RID: 7845
		private ADObjectId[] m_operationalServers;

		// Token: 0x04001EA6 RID: 7846
		private ADObjectId m_primaryActiveManager;

		// Token: 0x04001EA7 RID: 7847
		private ADObjectId[] m_serversInMaintenance;

		// Token: 0x04001EA8 RID: 7848
		private DeferredFailoverEntry[] m_serversInDeferredRecovery;

		// Token: 0x04001EA9 RID: 7849
		private List<string> m_networkNames = new List<string>();

		// Token: 0x04001EAA RID: 7850
		private WitnessShareUsage? m_witnessShareInUse = null;

		// Token: 0x020003E2 RID: 994
		public enum NetworkOption
		{
			// Token: 0x04001EAC RID: 7852
			Disabled,
			// Token: 0x04001EAD RID: 7853
			Enabled,
			// Token: 0x04001EAE RID: 7854
			InterSubnetOnly,
			// Token: 0x04001EAF RID: 7855
			SeedOnly
		}
	}
}
