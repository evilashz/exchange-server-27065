using System;
using System.Text;

namespace Microsoft.Exchange.Rpc.Common
{
	// Token: 0x02000116 RID: 278
	internal sealed class RpcGenericRequestInfo
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x00002504 File Offset: 0x00001904
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

		// Token: 0x0600068C RID: 1676 RVA: 0x000025C8 File Offset: 0x000019C8
		public RpcGenericRequestInfo(int serverVersion, int commandId, int commandMajorVersion, int commandMinorVersion, byte[] attachedData)
		{
			this.BuildDebugString();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00002608 File Offset: 0x00001A08
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000261C File Offset: 0x00001A1C
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00002630 File Offset: 0x00001A30
		public int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00002644 File Offset: 0x00001A44
		public int CommandMajorVersion
		{
			get
			{
				return this.m_commandMajorVersion;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00002658 File Offset: 0x00001A58
		public int CommandMinorVersion
		{
			get
			{
				return this.m_commandMinorVersion;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000266C File Offset: 0x00001A6C
		public byte[] AttachedData
		{
			get
			{
				return this.m_attachedData;
			}
		}

		// Token: 0x0400096A RID: 2410
		private int m_serverVersion = serverVersion;

		// Token: 0x0400096B RID: 2411
		private int m_commandId = commandId;

		// Token: 0x0400096C RID: 2412
		private int m_commandMajorVersion = commandMajorVersion;

		// Token: 0x0400096D RID: 2413
		private int m_commandMinorVersion = commandMinorVersion;

		// Token: 0x0400096E RID: 2414
		private byte[] m_attachedData = attachedData;

		// Token: 0x0400096F RID: 2415
		private string m_debugString;
	}
}
