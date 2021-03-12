using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200036E RID: 878
	internal sealed class SuccessfulSpoolerRulesResult : RopResult
	{
		// Token: 0x06001575 RID: 5493 RVA: 0x0003790B File Offset: 0x00035B0B
		internal SuccessfulSpoolerRulesResult(StoreId? folderId) : base(RopId.SpoolerRules, ErrorCode.None, null)
		{
			this.folderId = folderId;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0003791E File Offset: 0x00035B1E
		internal SuccessfulSpoolerRulesResult(Reader reader) : base(reader)
		{
			if (reader.ReadBool())
			{
				this.folderId = new StoreId?(StoreId.Parse(reader));
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00037940 File Offset: 0x00035B40
		public StoreId? FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00037948 File Offset: 0x00035B48
		internal static SuccessfulSpoolerRulesResult Parse(Reader reader)
		{
			return new SuccessfulSpoolerRulesResult(reader);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00037950 File Offset: 0x00035B50
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.folderId != null, 1);
			if (this.folderId != null)
			{
				this.folderId.Value.Serialize(writer);
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000379A0 File Offset: 0x00035BA0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Trigger=").Append(this.folderId != null);
			if (this.folderId != null)
			{
				stringBuilder.Append(" FID=").Append(this.folderId.Value.ToString());
			}
		}

		// Token: 0x04000B36 RID: 2870
		private readonly StoreId? folderId;
	}
}
