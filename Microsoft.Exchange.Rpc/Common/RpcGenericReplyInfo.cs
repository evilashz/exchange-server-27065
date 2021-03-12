using System;
using System.Text;

namespace Microsoft.Exchange.Rpc.Common
{
	// Token: 0x02000117 RID: 279
	internal sealed class RpcGenericReplyInfo
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00002680 File Offset: 0x00001A80
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

		// Token: 0x06000694 RID: 1684 RVA: 0x0000277C File Offset: 0x00001B7C
		public RpcGenericReplyInfo(int serverVersion, int commandId, int commandMajorVersion, int commandMinorVersion, byte[] attachedData)
		{
			this.m_attachedData = attachedData;
			base..ctor();
			this.BuildDebugString();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00002744 File Offset: 0x00001B44
		public RpcGenericReplyInfo(int serverVersion, int commandId, int commandMajorVersion, int commandMinorVersion)
		{
			this.m_attachedData = null;
			base..ctor();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000027E0 File Offset: 0x00001BE0
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000027F4 File Offset: 0x00001BF4
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00002808 File Offset: 0x00001C08
		public int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000281C File Offset: 0x00001C1C
		public int CommandMajorVersion
		{
			get
			{
				return this.m_commandMajorVersion;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00002830 File Offset: 0x00001C30
		public int CommandMinorVersion
		{
			get
			{
				return this.m_commandMinorVersion;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00002844 File Offset: 0x00001C44
		public byte[] AttachedData
		{
			get
			{
				return this.m_attachedData;
			}
		}

		// Token: 0x04000970 RID: 2416
		private int m_serverVersion = serverVersion;

		// Token: 0x04000971 RID: 2417
		private int m_commandId = commandId;

		// Token: 0x04000972 RID: 2418
		private int m_commandMajorVersion = commandMajorVersion;

		// Token: 0x04000973 RID: 2419
		private int m_commandMinorVersion = commandMinorVersion;

		// Token: 0x04000974 RID: 2420
		private byte[] m_attachedData;

		// Token: 0x04000975 RID: 2421
		private string m_debugString;
	}
}
