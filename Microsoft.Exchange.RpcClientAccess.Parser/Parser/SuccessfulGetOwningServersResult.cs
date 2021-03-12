using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000258 RID: 600
	internal sealed class SuccessfulGetOwningServersResult : RopResult
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00028353 File Offset: 0x00026553
		internal SuccessfulGetOwningServersResult(ReplicaServerInfo replicaInfo) : base(RopId.GetOwningServers, ErrorCode.None, null)
		{
			this.replicaInfo = replicaInfo;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00028366 File Offset: 0x00026566
		internal SuccessfulGetOwningServersResult(Reader reader) : base(reader)
		{
			this.replicaInfo = ReplicaServerInfo.Parse(reader);
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002837B File Offset: 0x0002657B
		internal ReplicaServerInfo ReplicaInfo
		{
			get
			{
				return this.replicaInfo;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00028383 File Offset: 0x00026583
		internal static SuccessfulGetOwningServersResult Parse(Reader reader)
		{
			return new SuccessfulGetOwningServersResult(reader);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002838C File Offset: 0x0002658C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.replicaInfo.Serialize(writer);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000283B0 File Offset: 0x000265B0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" OwningServers=").Append(this.replicaInfo.ToString());
		}

		// Token: 0x040006FB RID: 1787
		private readonly ReplicaServerInfo replicaInfo;
	}
}
