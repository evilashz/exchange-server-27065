using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002BE RID: 702
	internal sealed class SuccessfulGetAllPerUserLongTermIdsResult : RopResult
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x0002DAAB File Offset: 0x0002BCAB
		internal SuccessfulGetAllPerUserLongTermIdsResult(PerUserDataCollector perUserDataCollector, bool finished) : base(RopId.GetAllPerUserLongTermIds, ErrorCode.None, null)
		{
			this.perUserDataEntries = perUserDataCollector.PerUserDataEntries;
			this.finished = finished;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0002DACC File Offset: 0x0002BCCC
		internal SuccessfulGetAllPerUserLongTermIdsResult(Reader reader) : base(reader)
		{
			this.perUserDataEntries = new List<PerUserData>();
			this.finished = reader.ReadBool();
			ushort num = reader.ReadUInt16();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				this.perUserDataEntries.Add(new PerUserData(reader));
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0002DB1C File Offset: 0x0002BD1C
		internal List<PerUserData> PerUserDataEntries
		{
			get
			{
				return this.perUserDataEntries;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0002DB24 File Offset: 0x0002BD24
		internal bool HasFinished
		{
			get
			{
				return this.finished;
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		public override string ToString()
		{
			return string.Format("SuccessfulGetAllPerUserLongTermIdsResult: [Finished: {0}, PerUserData: {1}]", this.finished, this.perUserDataEntries.Count);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0002DB53 File Offset: 0x0002BD53
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulGetAllPerUserLongTermIdsResult(reader);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0002DB5C File Offset: 0x0002BD5C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.finished, 1);
			int count = this.perUserDataEntries.Count;
			writer.WriteUInt16((ushort)count);
			foreach (PerUserData perUserData in this.perUserDataEntries)
			{
				perUserData.Serialize(writer);
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Finished=").Append(this.finished);
			stringBuilder.Append("\n PerUserDate entries=").Append(this.perUserDataEntries.Count);
		}

		// Token: 0x04000802 RID: 2050
		private readonly List<PerUserData> perUserDataEntries;

		// Token: 0x04000803 RID: 2051
		private readonly bool finished;
	}
}
