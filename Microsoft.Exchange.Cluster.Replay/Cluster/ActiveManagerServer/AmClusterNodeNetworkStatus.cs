using System;
using Microsoft.Exchange.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class AmClusterNodeNetworkStatus
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000067CA File Offset: 0x000049CA
		public AmClusterNodeNetworkStatus()
		{
			this.IsHealthy = true;
			this.HasADAccess = true;
			this.ClusterErrorOverride = false;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000067E7 File Offset: 0x000049E7
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000067EF File Offset: 0x000049EF
		public bool IsHealthy { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000067F8 File Offset: 0x000049F8
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00006800 File Offset: 0x00004A00
		public bool HasADAccess
		{
			get
			{
				return this.m_hasADAccess;
			}
			set
			{
				this.m_hasADAccess = value;
				if (!value)
				{
					this.IsHealthy = false;
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006813 File Offset: 0x00004A13
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000681B File Offset: 0x00004A1B
		public bool ClusterErrorOverride { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006824 File Offset: 0x00004A24
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000682C File Offset: 0x00004A2C
		public DateTime LastUpdate { get; set; }

		// Token: 0x060000EF RID: 239 RVA: 0x00006838 File Offset: 0x00004A38
		public override string ToString()
		{
			return string.Format("IsHealthy={0},HasADAccess={1},ClusterErrorOverride{2},LastUpdate={3}UTC", new object[]
			{
				this.IsHealthy,
				this.HasADAccess,
				this.ClusterErrorOverride,
				this.LastUpdate
			});
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000688F File Offset: 0x00004A8F
		public bool IsEqual(AmClusterNodeNetworkStatus other)
		{
			return this.IsHealthy == other.IsHealthy && this.HasADAccess == other.HasADAccess && this.ClusterErrorOverride == other.ClusterErrorOverride;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000068BE File Offset: 0x00004ABE
		internal static AmClusterNodeNetworkStatus Deserialize(string xmlText)
		{
			return (AmClusterNodeNetworkStatus)SerializationUtil.XmlToObject(xmlText, typeof(AmClusterNodeNetworkStatus));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000068D5 File Offset: 0x00004AD5
		internal string Serialize()
		{
			return SerializationUtil.ObjectToXml(this);
		}

		// Token: 0x04000061 RID: 97
		private bool m_hasADAccess;
	}
}
