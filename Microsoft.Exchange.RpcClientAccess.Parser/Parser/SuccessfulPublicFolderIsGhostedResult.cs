using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200027B RID: 635
	internal sealed class SuccessfulPublicFolderIsGhostedResult : RopResult
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x00029951 File Offset: 0x00027B51
		internal SuccessfulPublicFolderIsGhostedResult(ReplicaServerInfo? replicaInfo) : base(RopId.PublicFolderIsGhosted, ErrorCode.None, null)
		{
			this.replicaInfo = replicaInfo;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00029964 File Offset: 0x00027B64
		internal SuccessfulPublicFolderIsGhostedResult(Reader reader) : base(reader)
		{
			bool flag = reader.ReadBool();
			if (flag)
			{
				this.replicaInfo = new ReplicaServerInfo?(ReplicaServerInfo.Parse(reader));
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00029994 File Offset: 0x00027B94
		internal bool IsGhosted
		{
			get
			{
				return this.replicaInfo != null;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000299AF File Offset: 0x00027BAF
		internal ReplicaServerInfo? ReplicaInfo
		{
			get
			{
				return this.replicaInfo;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000299B7 File Offset: 0x00027BB7
		internal static SuccessfulPublicFolderIsGhostedResult Parse(Reader reader)
		{
			return new SuccessfulPublicFolderIsGhostedResult(reader);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000299C0 File Offset: 0x00027BC0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.IsGhosted, 1);
			if (this.IsGhosted)
			{
				this.replicaInfo.Value.Serialize(writer);
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00029A00 File Offset: 0x00027C00
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" IsGhosted=").Append(this.IsGhosted);
			if (this.IsGhosted)
			{
				stringBuilder.Append(" ReplicaInfo=").Append(this.replicaInfo.Value.ToString());
			}
		}

		// Token: 0x04000726 RID: 1830
		private readonly ReplicaServerInfo? replicaInfo;
	}
}
