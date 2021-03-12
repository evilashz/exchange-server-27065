using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class DagNetConfig : IEquatable<DagNetConfig>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public int ReplicationPort
		{
			get
			{
				return this.replicationPort;
			}
			set
			{
				this.replicationPort = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		public NetworkOption NetworkCompression
		{
			get
			{
				return this.networkCompression;
			}
			set
			{
				this.networkCompression = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000002FA
		public NetworkOption NetworkEncryption
		{
			get
			{
				return this.networkEncryption;
			}
			set
			{
				this.networkEncryption = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002103 File Offset: 0x00000303
		public List<DagNetwork> Networks
		{
			get
			{
				return this.networks;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210B File Offset: 0x0000030B
		public List<DagNode> Nodes
		{
			get
			{
				return this.nodes;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		public bool Equals(DagNetConfig other)
		{
			return this.replicationPort == other.replicationPort && this.networkCompression == other.networkCompression && this.networkEncryption == other.networkEncryption && this.networks.SequenceEqual(other.networks) && this.nodes.SequenceEqual(other.nodes);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002171 File Offset: 0x00000371
		internal static DagNetConfig Deserialize(string xmlText)
		{
			return (DagNetConfig)Dependencies.Serializer.XmlToObject(xmlText, typeof(DagNetConfig));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218D File Offset: 0x0000038D
		internal string Serialize()
		{
			return Dependencies.Serializer.ObjectToXml(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000219C File Offset: 0x0000039C
		internal DagNetConfig Copy()
		{
			string xmlText = this.Serialize();
			return DagNetConfig.Deserialize(xmlText);
		}

		// Token: 0x04000006 RID: 6
		public const int DefaultReplicationPort = 64327;

		// Token: 0x04000007 RID: 7
		public const NetworkOption DefaultNetworkOption = NetworkOption.InterSubnetOnly;

		// Token: 0x04000008 RID: 8
		private int replicationPort = 64327;

		// Token: 0x04000009 RID: 9
		private NetworkOption networkCompression = NetworkOption.InterSubnetOnly;

		// Token: 0x0400000A RID: 10
		private NetworkOption networkEncryption = NetworkOption.InterSubnetOnly;

		// Token: 0x0400000B RID: 11
		private List<DagNetwork> networks = new List<DagNetwork>(3);

		// Token: 0x0400000C RID: 12
		private List<DagNode> nodes = new List<DagNode>(12);
	}
}
