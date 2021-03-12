using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000078 RID: 120
	internal sealed class PerUserData
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000BBE7 File Offset: 0x00009DE7
		public PerUserData(StoreLongTermId longTermId, Guid replicaGuid)
		{
			this.longTermId = longTermId;
			this.replicaGuid = replicaGuid;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000BBFD File Offset: 0x00009DFD
		internal PerUserData(Reader reader)
		{
			this.longTermId = StoreLongTermId.Parse(reader);
			this.replicaGuid = reader.ReadGuid();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BC20 File Offset: 0x00009E20
		internal void Serialize(Writer writer)
		{
			this.longTermId.Serialize(writer, true);
			writer.WriteGuid(this.replicaGuid);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000BC49 File Offset: 0x00009E49
		internal StoreLongTermId LongTermId
		{
			get
			{
				return this.longTermId;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000BC51 File Offset: 0x00009E51
		internal Guid ReplicaGuid
		{
			get
			{
				return this.replicaGuid;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BC5C File Offset: 0x00009E5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000BC80 File Offset: 0x00009E80
		internal void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append("[LongTermId=").Append(this.longTermId);
			stringBuilder.Append(" ReplicaGuid=").Append(this.replicaGuid).Append("]");
		}

		// Token: 0x0400018A RID: 394
		private readonly StoreLongTermId longTermId;

		// Token: 0x0400018B RID: 395
		private readonly Guid replicaGuid;
	}
}
