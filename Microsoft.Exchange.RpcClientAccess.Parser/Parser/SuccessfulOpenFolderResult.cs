using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000276 RID: 630
	internal sealed class SuccessfulOpenFolderResult : RopResult
	{
		// Token: 0x06000D94 RID: 3476 RVA: 0x0002942D File Offset: 0x0002762D
		internal SuccessfulOpenFolderResult(IServerObject folder, bool hasRules, ReplicaServerInfo? replicaInfo) : base(RopId.OpenFolder, ErrorCode.None, folder)
		{
			this.hasRules = hasRules;
			this.replicaInfo = replicaInfo;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00029448 File Offset: 0x00027648
		internal SuccessfulOpenFolderResult(Reader reader) : base(reader)
		{
			this.hasRules = reader.ReadBool();
			bool flag = reader.ReadBool();
			if (flag)
			{
				this.replicaInfo = new ReplicaServerInfo?(ReplicaServerInfo.Parse(reader));
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00029483 File Offset: 0x00027683
		internal bool HasRules
		{
			get
			{
				return this.hasRules;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0002948C File Offset: 0x0002768C
		internal bool IsGhosted
		{
			get
			{
				return this.replicaInfo != null;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000294A7 File Offset: 0x000276A7
		internal ReplicaServerInfo? ReplicaInfo
		{
			get
			{
				return this.replicaInfo;
			}
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000294AF File Offset: 0x000276AF
		internal static SuccessfulOpenFolderResult Parse(Reader reader)
		{
			return new SuccessfulOpenFolderResult(reader);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000294B8 File Offset: 0x000276B8
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.hasRules, 1);
			writer.WriteBool(this.IsGhosted, 1);
			if (this.IsGhosted)
			{
				this.replicaInfo.Value.Serialize(writer);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00029508 File Offset: 0x00027708
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" HasRules=").Append(this.HasRules);
			stringBuilder.Append(" IsGhosted=").Append(this.IsGhosted);
			if (this.IsGhosted)
			{
				stringBuilder.Append(" ReplicaInfo=").Append(this.replicaInfo.Value.ToString());
			}
		}

		// Token: 0x0400071C RID: 1820
		private readonly bool hasRules;

		// Token: 0x0400071D RID: 1821
		private readonly ReplicaServerInfo? replicaInfo;
	}
}
