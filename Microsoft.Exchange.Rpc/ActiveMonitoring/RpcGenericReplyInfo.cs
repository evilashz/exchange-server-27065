using System;
using System.Text;

namespace Microsoft.Exchange.Rpc.ActiveMonitoring
{
	// Token: 0x02000134 RID: 308
	public sealed class RpcGenericReplyInfo
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00007DEC File Offset: 0x000071EC
		private void BuildDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.AppendFormat("RpcGenericReplyInfo: [ServerVersion='{0}', ", this.m_serverVersion);
			int commandId = this.m_commandId;
			stringBuilder.AppendFormat("CommandId='{0}', ", commandId.ToString());
			int commandMajorVersion = this.m_commandMajorVersion;
			stringBuilder.AppendFormat("MajorVersion='{0}', ", commandMajorVersion.ToString());
			int commandMinorVersion = this.m_commandMinorVersion;
			stringBuilder.AppendFormat("MinorVersion='{0}', ", commandMinorVersion.ToString());
			byte[] attachedData = this.m_attachedData;
			string arg;
			if (attachedData == null)
			{
				arg = "<null>";
			}
			else
			{
				arg = attachedData.Length.ToString();
			}
			stringBuilder.AppendFormat("AttachedDataSize='{0}', ", arg);
			stringBuilder.Append("]");
			this.m_debugString = stringBuilder.ToString();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00007EE8 File Offset: 0x000072E8
		public RpcGenericReplyInfo(int serverVersion, int commandId, int commandMajorVersion, int commandMinorVersion, byte[] attachedData)
		{
			this.m_attachedData = attachedData;
			base..ctor();
			this.BuildDebugString();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00007EB0 File Offset: 0x000072B0
		public RpcGenericReplyInfo(int serverVersion, int commandId, int commandMajorVersion, int commandMinorVersion)
		{
			this.m_attachedData = null;
			base..ctor();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00007F28 File Offset: 0x00007328
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00007F3C File Offset: 0x0000733C
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00007F50 File Offset: 0x00007350
		public int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00007F64 File Offset: 0x00007364
		public int CommandMajorVersion
		{
			get
			{
				return this.m_commandMajorVersion;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00007F78 File Offset: 0x00007378
		public int CommandMinorVersion
		{
			get
			{
				return this.m_commandMinorVersion;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00007F8C File Offset: 0x0000738C
		public byte[] AttachedData
		{
			get
			{
				return this.m_attachedData;
			}
		}

		// Token: 0x04000A71 RID: 2673
		private int m_serverVersion = serverVersion;

		// Token: 0x04000A72 RID: 2674
		private int m_commandId = commandId;

		// Token: 0x04000A73 RID: 2675
		private int m_commandMajorVersion = commandMajorVersion;

		// Token: 0x04000A74 RID: 2676
		private int m_commandMinorVersion = commandMinorVersion;

		// Token: 0x04000A75 RID: 2677
		private byte[] m_attachedData;

		// Token: 0x04000A76 RID: 2678
		private string m_debugString;
	}
}
