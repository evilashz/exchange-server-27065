using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	public class PersistentDagNetworkConfig
	{
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00062EE1 File Offset: 0x000610E1
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x00062EE9 File Offset: 0x000610E9
		public ushort ReplicationPort
		{
			get
			{
				return this.m_replicationPort;
			}
			set
			{
				this.m_replicationPort = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x00062EF2 File Offset: 0x000610F2
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x00062EFA File Offset: 0x000610FA
		public DatabaseAvailabilityGroup.NetworkOption NetworkCompression
		{
			get
			{
				return this.m_networkCompression;
			}
			set
			{
				this.m_networkCompression = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00062F03 File Offset: 0x00061103
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x00062F0B File Offset: 0x0006110B
		public DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
		{
			get
			{
				return this.m_networkEncryption;
			}
			set
			{
				this.m_networkEncryption = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00062F14 File Offset: 0x00061114
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x00062F1C File Offset: 0x0006111C
		public bool ManualDagNetworkConfiguration
		{
			get
			{
				return this.m_manualDagNetworkConfiguration;
			}
			set
			{
				this.m_manualDagNetworkConfiguration = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00062F25 File Offset: 0x00061125
		public List<PersistentDagNetwork> Networks
		{
			get
			{
				return this.m_networks;
			}
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00062F2D File Offset: 0x0006112D
		internal static PersistentDagNetworkConfig Deserialize(string xmlText)
		{
			return (PersistentDagNetworkConfig)SerializationUtil.XmlToObject(xmlText, typeof(PersistentDagNetworkConfig));
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00062F44 File Offset: 0x00061144
		internal string Serialize()
		{
			return SerializationUtil.ObjectToXml(this);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00062F4C File Offset: 0x0006114C
		internal PersistentDagNetworkConfig Copy()
		{
			string xmlText = this.Serialize();
			return PersistentDagNetworkConfig.Deserialize(xmlText);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00062F68 File Offset: 0x00061168
		internal bool RemoveEmptyNetworks()
		{
			List<PersistentDagNetwork> list = new List<PersistentDagNetwork>();
			foreach (PersistentDagNetwork persistentDagNetwork in this.Networks)
			{
				if (persistentDagNetwork.Subnets.Count == 0)
				{
					list.Add(persistentDagNetwork);
				}
			}
			foreach (PersistentDagNetwork item in list)
			{
				this.Networks.Remove(item);
			}
			return list.Count > 0;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0006301C File Offset: 0x0006121C
		internal PersistentDagNetwork FindNetwork(string nameToFind)
		{
			foreach (PersistentDagNetwork persistentDagNetwork in this.Networks)
			{
				if (DatabaseAvailabilityGroupNetwork.NameComparer.Equals(nameToFind, persistentDagNetwork.Name))
				{
					return persistentDagNetwork;
				}
			}
			return null;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00063084 File Offset: 0x00061284
		internal bool RemoveNetwork(string nameToRemove)
		{
			PersistentDagNetwork persistentDagNetwork = this.FindNetwork(nameToRemove);
			if (persistentDagNetwork != null)
			{
				this.Networks.Remove(persistentDagNetwork);
				return true;
			}
			return false;
		}

		// Token: 0x04000971 RID: 2417
		private ushort m_replicationPort = 64327;

		// Token: 0x04000972 RID: 2418
		private DatabaseAvailabilityGroup.NetworkOption m_networkCompression = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x04000973 RID: 2419
		private DatabaseAvailabilityGroup.NetworkOption m_networkEncryption = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x04000974 RID: 2420
		private List<PersistentDagNetwork> m_networks = new List<PersistentDagNetwork>();

		// Token: 0x04000975 RID: 2421
		private bool m_manualDagNetworkConfiguration;
	}
}
