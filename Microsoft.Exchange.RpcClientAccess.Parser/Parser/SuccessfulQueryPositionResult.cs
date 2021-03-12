using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000319 RID: 793
	internal sealed class SuccessfulQueryPositionResult : RopResult
	{
		// Token: 0x060012B4 RID: 4788 RVA: 0x00032BED File Offset: 0x00030DED
		internal SuccessfulQueryPositionResult(uint numerator, uint denominator) : base(RopId.QueryPosition, ErrorCode.None, null)
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00032C07 File Offset: 0x00030E07
		internal SuccessfulQueryPositionResult(Reader reader) : base(reader)
		{
			this.numerator = reader.ReadUInt32();
			this.denominator = reader.ReadUInt32();
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00032C28 File Offset: 0x00030E28
		internal uint Numerator
		{
			get
			{
				return this.numerator;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00032C30 File Offset: 0x00030E30
		internal uint Denominator
		{
			get
			{
				return this.denominator;
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00032C38 File Offset: 0x00030E38
		internal static SuccessfulQueryPositionResult Parse(Reader reader)
		{
			return new SuccessfulQueryPositionResult(reader);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00032C40 File Offset: 0x00030E40
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.numerator);
			writer.WriteUInt32(this.denominator);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00032C61 File Offset: 0x00030E61
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Numerator=").Append(this.numerator);
			stringBuilder.Append(" Denominator=").Append(this.denominator);
		}

		// Token: 0x04000A02 RID: 2562
		private readonly uint numerator;

		// Token: 0x04000A03 RID: 2563
		private readonly uint denominator;
	}
}
