using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000254 RID: 596
	internal sealed class SuccessfulGetLocalReplicationIdsResult : RopResult
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x000280C2 File Offset: 0x000262C2
		internal SuccessfulGetLocalReplicationIdsResult(StoreLongTermId localReplicationId) : base(RopId.GetLocalReplicationIds, ErrorCode.None, null)
		{
			this.localReplicationId = localReplicationId;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000280D5 File Offset: 0x000262D5
		internal SuccessfulGetLocalReplicationIdsResult(Reader reader) : base(reader)
		{
			this.localReplicationId = StoreLongTermId.Parse(reader, false);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x000280EB File Offset: 0x000262EB
		public StoreLongTermId LocalReplicationId
		{
			get
			{
				return this.localReplicationId;
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x000280F3 File Offset: 0x000262F3
		internal static SuccessfulGetLocalReplicationIdsResult Parse(Reader reader)
		{
			return new SuccessfulGetLocalReplicationIdsResult(reader);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000280FC File Offset: 0x000262FC
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.localReplicationId.Serialize(writer, false);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00028120 File Offset: 0x00026320
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ReplID=[").Append(this.localReplicationId).Append("]");
		}

		// Token: 0x040006F5 RID: 1781
		private readonly StoreLongTermId localReplicationId;
	}
}
