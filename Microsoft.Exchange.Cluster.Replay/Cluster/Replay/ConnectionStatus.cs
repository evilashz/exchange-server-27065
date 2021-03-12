using System;
using System.Text;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000338 RID: 824
	[Serializable]
	public class ConnectionStatus
	{
		// Token: 0x0600217B RID: 8571 RVA: 0x0009B7D7 File Offset: 0x000999D7
		internal ConnectionStatus(string mailboxServer, string network, string lastFailure, ConnectionDirection direction, bool isSeeding)
		{
			this.m_partner = mailboxServer;
			this.m_network = network;
			this.m_lastFailure = lastFailure;
			this.m_isSeeding = isSeeding;
			this.m_direction = direction;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0009B804 File Offset: 0x00099A04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("{0},{1}", this.m_partner, this.m_network);
			if (this.m_isSeeding)
			{
				stringBuilder.AppendFormat(",{0}", this.m_direction);
			}
			if (this.m_lastFailure != null)
			{
				stringBuilder.AppendFormat(",{0}", this.m_lastFailure);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0009B886 File Offset: 0x00099A86
		public string Status
		{
			get
			{
				if (this.m_externalStatusString == null)
				{
					this.m_externalStatusString = this.ToString();
				}
				return this.m_externalStatusString;
			}
		}

		// Token: 0x04000DB6 RID: 3510
		private string m_partner;

		// Token: 0x04000DB7 RID: 3511
		private string m_network;

		// Token: 0x04000DB8 RID: 3512
		private string m_lastFailure;

		// Token: 0x04000DB9 RID: 3513
		private bool m_isSeeding;

		// Token: 0x04000DBA RID: 3514
		private ConnectionDirection m_direction;

		// Token: 0x04000DBB RID: 3515
		private string m_externalStatusString;
	}
}
