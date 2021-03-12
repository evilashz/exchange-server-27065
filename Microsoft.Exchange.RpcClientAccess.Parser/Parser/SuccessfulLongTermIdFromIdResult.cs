using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200026C RID: 620
	internal sealed class SuccessfulLongTermIdFromIdResult : RopResult
	{
		// Token: 0x06000D5B RID: 3419 RVA: 0x00028C64 File Offset: 0x00026E64
		internal SuccessfulLongTermIdFromIdResult(StoreLongTermId longTermId) : base(RopId.LongTermIdFromId, ErrorCode.None, null)
		{
			this.longTermId = longTermId;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00028C77 File Offset: 0x00026E77
		internal SuccessfulLongTermIdFromIdResult(Reader reader) : base(reader)
		{
			this.longTermId = StoreLongTermId.Parse(reader);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00028C8C File Offset: 0x00026E8C
		public StoreLongTermId LongTermId
		{
			get
			{
				return this.longTermId;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00028C94 File Offset: 0x00026E94
		internal static SuccessfulLongTermIdFromIdResult Parse(Reader reader)
		{
			return new SuccessfulLongTermIdFromIdResult(reader);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00028C9C File Offset: 0x00026E9C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.longTermId.Serialize(writer);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00028CBF File Offset: 0x00026EBF
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
		}

		// Token: 0x0400070B RID: 1803
		private readonly StoreLongTermId longTermId;
	}
}
