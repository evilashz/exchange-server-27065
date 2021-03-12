using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200036B RID: 875
	internal class SuccessfulSetReadFlagResult : RopResult
	{
		// Token: 0x06001567 RID: 5479 RVA: 0x0003777D File Offset: 0x0003597D
		internal SuccessfulSetReadFlagResult(bool hasChanged, byte logonIndex, StoreLongTermId longTermId) : base(RopId.SetReadFlag, ErrorCode.None, null)
		{
			this.hasChanged = hasChanged;
			this.logonIndex = logonIndex;
			this.longTermId = longTermId;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000377A9 File Offset: 0x000359A9
		internal SuccessfulSetReadFlagResult(Reader reader) : base(reader)
		{
			this.hasChanged = reader.ReadBool();
			if (this.hasChanged)
			{
				this.logonIndex = reader.ReadByte();
				this.longTermId = StoreLongTermId.Parse(reader);
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000377E9 File Offset: 0x000359E9
		internal static SuccessfulSetReadFlagResult Parse(Reader reader)
		{
			return new SuccessfulSetReadFlagResult(reader);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000377F1 File Offset: 0x000359F1
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.hasChanged, 1);
			if (this.hasChanged)
			{
				writer.WriteByte(this.logonIndex);
				this.longTermId.Serialize(writer);
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00037828 File Offset: 0x00035A28
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Changed=").Append(this.hasChanged);
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
		}

		// Token: 0x04000B31 RID: 2865
		private bool hasChanged;

		// Token: 0x04000B32 RID: 2866
		private byte logonIndex;

		// Token: 0x04000B33 RID: 2867
		private StoreLongTermId longTermId = StoreLongTermId.Null;
	}
}
