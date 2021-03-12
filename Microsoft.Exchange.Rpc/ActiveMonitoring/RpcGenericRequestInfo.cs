using System;
using System.Text;

namespace Microsoft.Exchange.Rpc.ActiveMonitoring
{
	// Token: 0x02000133 RID: 307
	public sealed class RpcGenericRequestInfo
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x00007C70 File Offset: 0x00007070
		private void BuildDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.AppendFormat("RpcGenericRequestInfo: [ServerVersion='{0}', ", this.m_serverVersion);
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

		// Token: 0x06000886 RID: 2182 RVA: 0x00007D34 File Offset: 0x00007134
		public RpcGenericRequestInfo(int serverVersion, int commandId, int commandMajorVersion, int commandMinorVersion, byte[] attachedData)
		{
			this.BuildDebugString();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00007D74 File Offset: 0x00007174
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00007D88 File Offset: 0x00007188
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00007D9C File Offset: 0x0000719C
		public int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00007DB0 File Offset: 0x000071B0
		public int CommandMajorVersion
		{
			get
			{
				return this.m_commandMajorVersion;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00007DC4 File Offset: 0x000071C4
		public int CommandMinorVersion
		{
			get
			{
				return this.m_commandMinorVersion;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00007DD8 File Offset: 0x000071D8
		public byte[] AttachedData
		{
			get
			{
				return this.m_attachedData;
			}
		}

		// Token: 0x04000A6B RID: 2667
		private int m_serverVersion = serverVersion;

		// Token: 0x04000A6C RID: 2668
		private int m_commandId = commandId;

		// Token: 0x04000A6D RID: 2669
		private int m_commandMajorVersion = commandMajorVersion;

		// Token: 0x04000A6E RID: 2670
		private int m_commandMinorVersion = commandMinorVersion;

		// Token: 0x04000A6F RID: 2671
		private byte[] m_attachedData = attachedData;

		// Token: 0x04000A70 RID: 2672
		private string m_debugString;
	}
}
